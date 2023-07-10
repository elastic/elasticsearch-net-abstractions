// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Managed;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using static Elastic.Elasticsearch.Ephemeral.ClusterAuthentication;
using static Elastic.Elasticsearch.Ephemeral.ClusterFeatures;
using HttpMethod = Elastic.Transport.HttpMethod;


var config = new EphemeralClusterConfiguration("8.7.0", XPack | Security | SSL);
using var cluster = new EphemeralCluster(config);

var exitEvent = new ManualResetEvent(false);
Console.CancelKeyPress += (sender, eventArgs) => {
	cluster.Dispose();
	eventArgs.Cancel = true;
	exitEvent.Set();
};
using var started = cluster.Start();

var pool = new StaticNodePool(cluster.NodesUris());
var transportConfig = new TransportConfiguration(pool, productRegistration: ElasticsearchProductRegistration.Default)
	.Authentication(new BasicAuthentication(Admin.Username, Admin.Password))
	.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
if (cluster.DetectedProxy != DetectedProxySoftware.None)
	transportConfig = transportConfig.Proxy(new Uri("http://localhost:8080"), null!, null!);

var transport = new DefaultHttpTransport(transportConfig);

var response = await transport.RequestAsync<StringResponse>(HttpMethod.GET, "/");
Console.WriteLine(response);


exitEvent.WaitOne();
