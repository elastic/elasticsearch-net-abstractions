using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Elastic.ProcessManagement.Std;

namespace Elastic.ProcessManagement
{
	public class LineByLineObservableProcess : BufferedObservableProcess
	{
		private char[] _bufferStdOut = { };
		private char[] _bufferStdErr = { };
		private readonly object _copyLock = new object();

		public LineByLineObservableProcess(ObservableProcessArguments arguments) : base(arguments) { }

		protected override IObservable<ConsoleOut> CreateConsoleOutObservable()
		{
			return Observable.Create<ConsoleOut>(observer =>
			{
				base.CreateConsoleOutObservable()
					.Subscribe(c => OnNextConsoleOut(c, observer), observer.OnError, observer.OnCompleted);
				return Disposable.Empty;
			});
		}

		private void OnNextConsoleOut(ConsoleOut c, IObserver<ConsoleOut> observer)
		{
			lock (_copyLock)
			{
				c.OutOrErrrorCharacters(OutCharacters, ErrorCharacters);
				if (c.Error)
					YieldNewLinesToOnNext(ref _bufferStdErr, buffer => observer.OnNext(ConsoleOut.ErrorOut(buffer)));
				else
					YieldNewLinesToOnNext(ref _bufferStdOut, buffer => observer.OnNext(ConsoleOut.Out(buffer)));
			}
		}

		protected override void OnCompleted(IObserver<ConsoleOut> observer)
		{
			Flush(observer); //make sure we flush our buffers before calling OnCompleted
			base.OnCompleted(observer);
		}

		protected override void OnError(IObserver<ConsoleOut> observer, Exception e)
		{
			Flush(observer); //make sure we flush our buffers before erroring
			base.OnError(observer, e);
		}

		private void Flush(IObserver<ConsoleOut> observer)
		{
			YieldNewLinesToOnNext(ref _bufferStdErr, buffer => observer.OnNext(ConsoleOut.ErrorOut(buffer)));
			YieldNewLinesToOnNext(ref _bufferStdOut, buffer => observer.OnNext(ConsoleOut.Out(buffer)));
		}

		private static void YieldNewLinesToOnNext(ref char[] buffer, Action<char[]> onNext)
		{
			var newLineOffset = ReadLinesFromBuffer(buffer, onNext);
			var endOfArrayOffset = FindEndOffSet(buffer, newLineOffset);
			CopyRemainderToGlobalBuffer(ref buffer, endOfArrayOffset, newLineOffset);
		}

		/// <summary>
		/// Copies the remainder of the local buffer to the global buffer to consider in the next observable push
		/// </summary>
		private static void CopyRemainderToGlobalBuffer(ref char[] buffer, int endOfArrayOffset, int newLineOffset)
		{
			var remainder = endOfArrayOffset - newLineOffset;
			var newBuffer = new char[remainder];
			Array.Copy(buffer, newLineOffset, newBuffer, 0, remainder);
			buffer = newBuffer;
		}

		/// <summary>
		/// Finds the offset of the first null byte character or the <paramref name="buffer"/>'s <see cref="Array.Length"/>
		/// </summary>
		/// <returns>Offset of the first null byte character or the <paramref name="buffer"/>'s <see cref="Array.Length"/></returns>
		private static int FindEndOffSet(char[] buffer, int from)
		{
			var zeroByteOffset = buffer.Length;
			for (var i = from; i < buffer.Length; i++)
			{
				var ch = buffer[i];
				if (ch != '\0') continue;
				zeroByteOffset = i + 1;
				break;
			}
			return zeroByteOffset;
		}

		/// <summary>
		/// Reads all the new lines inside <paramref name="buffer"/>
		/// </summary>
		/// <returns>the last new line character offset</returns>
		private static int ReadLinesFromBuffer(char[] buffer, Action<char[]> onNext)
		{
			var newLineOffset = 0;
			for (var i = 0; i < buffer.Length; i++)
			{
				var ch = buffer[i];
				if (ch != '\n') continue;

				var count = i - newLineOffset + 1;
				var ret = new char[count];
				Array.Copy(buffer, newLineOffset, ret, 0, count);
				onNext(ret);

				newLineOffset = i + 1;
			}
			return newLineOffset;
		}

		private void OutCharacters(char[] data) => Combine(ref _bufferStdOut, data);
		private void ErrorCharacters(char[] data) => Combine(ref _bufferStdErr, data);

		public static void Combine(ref char[] first, char[] second)
		{
			var ret = new char[first.Length + second.Length];
			Array.Copy(first, 0, ret, 0, first.Length);
			Array.Copy(second, 0, ret, first.Length, second.Length);
			first = ret;
		}
	}
}
