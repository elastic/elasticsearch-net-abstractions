
function class_serializer(ns: string) {return function (ns: any){}}
function prop_serializer(ns: string) {return function (ns: any, x:any){}}
function request_parameter() {return function (ns: any, x:any){}}
function namespace(ns: string) {return function (ns: any){}}

interface Uri {}
interface Date {}
interface TimeSpan {}
interface SourceDocument {}
@namespace("common")
class ErrorCause {
	reason: string;
	type: string;
	caused_by: ErrorCause;
	stack_trace: string;
	metadata: ErrorCauseMetadata;
}
@namespace("common")
class Error extends ErrorCause {
	root_cause: ErrorCause[];
	headers: Map<string, string>;
}
interface short {}
interface byte {}
interface integer {}
interface long {}
interface float {}
interface double {}

/** namespace:aggregations.bucket.date_histogram **/
enum DateInterval {
	second = 0,
	minute = 1,
	hour = 2,
	day = 3,
	week = 4,
	month = 5,
	quarter = 6,
	year = 7
}
/** namespace:aggregations.bucket.geo_hash_grid **/
enum GeoHashPrecision {
	Precision1 = 1,
	Precision2 = 2,
	Precision3 = 3,
	Precision4 = 4,
	Precision5 = 5,
	Precision6 = 6,
	Precision7 = 7,
	Precision8 = 8,
	Precision9 = 9,
	Precision10 = 10,
	Precision11 = 11,
	Precision12 = 12
}
/** namespace:aggregations.bucket.sampler **/
enum SamplerAggregationExecutionHint {
	map = 0,
	global_ordinals = 1,
	bytes_hash = 2
}
/** namespace:aggregations.bucket.terms **/
enum TermsAggregationCollectMode {
	depth_first = 0,
	breadth_first = 1
}
/** namespace:aggregations.bucket.terms **/
enum TermsAggregationExecutionHint {
	map = 0,
	global_ordinals = 1,
	global_ordinals_hash = 2,
	global_ordinals_low_cardinality = 3
}
/** namespace:aggregations.matrix.matrix_stats **/
enum MatrixStatsMode {
	avg = 0,
	min = 1,
	max = 2,
	sum = 3,
	median = 4
}
/** namespace:aggregations.pipeline **/
enum GapPolicy {
	skip = 0,
	insert_zeros = 1
}
/** namespace:aggregations.pipeline.moving_average.models **/
enum HoltWintersType {
	add = 0,
	mult = 1
}
/** namespace:aggregations.visitor **/
enum AggregationVisitorScope {
	Unknown = 0,
	Aggregation = 1,
	Bucket = 2
}
/** namespace:analysis.languages **/
enum Language {
	Arabic = 0,
	Armenian = 1,
	Basque = 2,
	Brazilian = 3,
	Bulgarian = 4,
	Catalan = 5,
	Chinese = 6,
	Cjk = 7,
	Czech = 8,
	Danish = 9,
	Dutch = 10,
	English = 11,
	Finnish = 12,
	French = 13,
	Galician = 14,
	German = 15,
	Greek = 16,
	Hindi = 17,
	Hungarian = 18,
	Indonesian = 19,
	Irish = 20,
	Italian = 21,
	Latvian = 22,
	Norwegian = 23,
	Persian = 24,
	Portuguese = 25,
	Romanian = 26,
	Russian = 27,
	Sorani = 28,
	Spanish = 29,
	Swedish = 30,
	Turkish = 31,
	Thai = 32
}
/** namespace:analysis.languages **/
enum SnowballLanguage {
	Armenian = 0,
	Basque = 1,
	Catalan = 2,
	Danish = 3,
	Dutch = 4,
	English = 5,
	Finnish = 6,
	French = 7,
	German = 8,
	German2 = 9,
	Hungarian = 10,
	Italian = 11,
	Kp = 12,
	Lovins = 13,
	Norwegian = 14,
	Porter = 15,
	Portuguese = 16,
	Romanian = 17,
	Russian = 18,
	Spanish = 19,
	Swedish = 20,
	Turkish = 21
}
/** namespace:analysis.plugins.icu.collation **/
enum IcuCollationAlternate {
	shifted = 0,
	'non-ignorable' = 1
}
/** namespace:analysis.plugins.icu.collation **/
enum IcuCollationCaseFirst {
	lower = 0,
	upper = 1
}
/** namespace:analysis.plugins.icu.collation **/
enum IcuCollationDecomposition {
	no = 0,
	identical = 1
}
/** namespace:analysis.plugins.icu.collation **/
enum IcuCollationStrength {
	primary = 0,
	secondary = 1,
	tertiary = 2,
	quaternary = 3,
	identical = 4
}
/** namespace:analysis.plugins.icu.normalization **/
enum IcuNormalizationMode {
	decompose = 0,
	compose = 1
}
/** namespace:analysis.plugins.icu.normalization **/
enum IcuNormalizationType {
	nfc = 0,
	nfkc = 1,
	nfkc_cf = 2
}
/** namespace:analysis.plugins.icu.transform **/
enum IcuTransformDirection {
	forward = 0,
	reverse = 1
}
/** namespace:analysis.plugins.kuromoji **/
enum KuromojiTokenizationMode {
	normal = 0,
	search = 1,
	extended = 2
}
/** namespace:analysis.plugins.phonetic **/
enum PhoneticEncoder {
	metaphone = 0,
	doublemetaphone = 1,
	soundex = 2,
	refinedsoundex = 3,
	caverphone1 = 4,
	caverphone2 = 5,
	cologne = 6,
	nysiis = 7,
	koelnerphonetik = 8,
	haasephonetik = 9,
	beidermorse = 10
}
/** namespace:analysis.token_filters.delimited_payload **/
enum DelimitedPayloadEncoding {
	int = 0,
	float = 1,
	identity = 2
}
/** namespace:analysis.token_filters.edge_n_gram **/
enum EdgeNGramSide {
	front = 0,
	back = 1
}
/** namespace:analysis.token_filters.synonym **/
enum SynonymFormat {
	solr = 0,
	wordnet = 1
}
/** namespace:analysis.tokenizers.n_gram **/
enum TokenChar {
	letter = 0,
	digit = 1,
	whitespace = 2,
	punctuation = 3,
	symbol = 4
}
/** namespace:cluster.cluster_allocation_explain **/
enum AllocationExplainDecision {
	NO = 0,
	YES = 1,
	THROTTLE = 2,
	ALWAYS = 3
}
/** namespace:cluster.cluster_allocation_explain **/
enum Decision {
	yes = 0,
	no = 1
}
/** namespace:cluster.cluster_allocation_explain **/
enum StoreCopy {
	NONE = 0,
	AVAILABLE = 1,
	CORRUPT = 2,
	IO_ERROR = 3,
	STALE = 4,
	UNKNOWN = 5
}
/** namespace:cluster.cluster_allocation_explain **/
enum UnassignedInformationReason {
	INDEX_CREATED = 0,
	CLUSTER_RECOVERED = 1,
	INDEX_REOPENED = 2,
	DANGLING_INDEX_IMPORTED = 3,
	NEW_INDEX_RESTORED = 4,
	EXISTING_INDEX_RESTORED = 5,
	REPLICA_ADDED = 6,
	ALLOCATION_FAILED = 7,
	NODE_LEFT = 8,
	REROUTE_CANCELLED = 9,
	REINITIALIZED = 10,
	REALLOCATED_REPLICA = 11
}
/** namespace:cluster **/
enum ClusterStatus {
	green = 0,
	yellow = 1,
	red = 2
}
/** namespace:cluster.nodes_info **/
enum NodeRole {
	master = 0,
	data = 1,
	client = 2,
	ingest = 3
}
/** namespace:common_options.date_math **/
enum DateMathOperation {
	'+' = 0,
	'-' = 1
}
/** namespace:common_options.date_math **/
enum DateMathTimeUnit {
	s = 0,
	m = 1,
	h = 2,
	d = 3,
	w = 4,
	M = 5,
	y = 6
}
/** namespace:common_options.geo **/
enum DistanceUnit {
	in = 0,
	ft = 1,
	yd = 2,
	mi = 3,
	nmi = 4,
	km = 5,
	m = 6,
	cm = 7,
	mm = 8
}
/** namespace:common_options.geo **/
enum GeoDistanceType {
	arc = 0,
	plane = 1
}
/** namespace:common_options.geo **/
enum GeoShapeRelation {
	intersects = 0,
	disjoint = 1,
	within = 2,
	contains = 3
}
/** namespace:common_options.time_unit **/
enum TimeUnit {
	nanos = 0,
	micros = 1,
	ms = 2,
	s = 3,
	m = 4,
	h = 5,
	d = 6
}
/** namespace:document **/
enum Result {
	Error = 0,
	created = 1,
	updated = 2,
	deleted = 3,
	not_found = 4,
	noop = 5
}
/** namespace:index_modules.index_settings.settings **/
enum RecoveryInitialShards {
	quorem = 0,
	'quorem-1' = 1,
	full = 2,
	'full-1' = 3
}
/** namespace:index_modules.index_settings.slow_log **/
enum LogLevel {
	error = 0,
	warn = 1,
	info = 2,
	debug = 3,
	trace = 4
}
/** namespace:index_modules.index_settings.sorting **/
enum IndexSortMode {
	min = 0,
	max = 1
}
/** namespace:index_modules.index_settings.sorting **/
enum IndexSortMissing {
	_first = 0,
	_last = 1
}
/** namespace:index_modules.index_settings.sorting **/
enum IndexSortOrder {
	asc = 0,
	desc = 1
}
/** namespace:index_modules.index_settings.store **/
enum FileSystemStorageImplementation {
	simplefs = 0,
	niofs = 1,
	mmapfs = 2,
	default_fs = 3
}
/** namespace:index_modules.index_settings.translog **/
enum TranslogDurability {
	request = 0,
	async = 1
}
/** namespace:index_modules.similarity.d_f_i **/
enum DFIIndependenceMeasure {
	standardized = 0,
	saturated = 1,
	chisquared = 2
}
/** namespace:index_modules.similarity.d_f_r **/
enum DFRAfterEffect {
	no = 0,
	b = 1,
	l = 2
}
/** namespace:index_modules.similarity.d_f_r **/
enum DFRBasicModel {
	be = 0,
	d = 1,
	g = 2,
	if = 3,
	in = 4,
	ine = 5,
	p = 6
}
/** namespace:index_modules.similarity.i_b **/
enum IBDistribution {
	ll = 0,
	spl = 1
}
/** namespace:index_modules.similarity.i_b **/
enum IBLambda {
	df = 0,
	ttf = 1
}
/** namespace:index_modules.similarity **/
enum Normalization {
	no = 0,
	h1 = 1,
	h2 = 2,
	h3 = 3,
	z = 4
}
/** namespace:indices.monitoring.indices_shard_stores **/
enum ShardStoreAllocation {
	primary = 0,
	replica = 1,
	unused = 2
}
/** namespace:ingest.processors **/
enum ConvertProcessorType {
	integer = 0,
	float = 1,
	string = 2,
	boolean = 3,
	auto = 4
}
/** namespace:ingest.processors **/
enum DateRounding {
	s = 0,
	m = 1,
	h = 2,
	d = 3,
	w = 4,
	M = 5,
	y = 6
}
/** namespace:ingest.processors.plugins.user_agent **/
enum UserAgentProperty {
	NAME = 0,
	MAJOR = 1,
	MINOR = 2,
	PATCH = 3,
	OS = 4,
	OS_NAME = 5,
	OS_MAJOR = 6,
	OS_MINOR = 7,
	DEVICE = 8,
	BUILD = 9
}
/** namespace:mapping **/
enum DynamicMapping {
	strict = 0
}
/** namespace:common **/
enum FieldIndexOption {
	analyzed = 0,
	not_analyzed = 1,
	no = 2
}
/** namespace:mapping **/
enum SimilarityOption {
	classic = 0,
	BM25 = 1
}
/** namespace:mapping **/
enum TermVectorOption {
	no = 0,
	yes = 1,
	with_offsets = 2,
	with_positions = 3,
	with_positions_offsets = 4,
	with_positions_offsets_payloads = 5
}
/** namespace:mapping.types.core.number **/
enum NumberType {
	float = 0,
	half_float = 1,
	scaled_float = 2,
	double = 3,
	integer = 4,
	long = 5,
	short = 6,
	byte = 7
}
/** namespace:mapping.types.core.range **/
enum RangeType {
	integer_range = 0,
	float_range = 1,
	long_range = 2,
	double_range = 3,
	date_range = 4
}
/** namespace:mapping.types.core.text **/
enum IndexOptions {
	docs = 0,
	freqs = 1,
	positions = 2,
	offsets = 3
}
/** namespace:mapping.types **/
enum FieldType {
	none = 0,
	geo_point = 1,
	geo_shape = 2,
	ip = 3,
	binary = 4,
	keyword = 5,
	text = 6,
	date = 7,
	boolean = 8,
	completion = 9,
	nested = 10,
	object = 11,
	murmur3 = 12,
	token_count = 13,
	percolator = 14,
	integer = 15,
	long = 16,
	short = 17,
	byte = 18,
	float = 19,
	half_float = 20,
	scaled_float = 21,
	double = 22,
	integer_range = 23,
	float_range = 24,
	long_range = 25,
	double_range = 26,
	date_range = 27,
	join = 28
}
/** namespace:mapping.types.geo.geo_shape **/
enum GeoOrientation {
	cw = 0,
	ccw = 1
}
/** namespace:mapping.types.geo.geo_shape **/
enum GeoStrategy {
	recursive = 0,
	term = 1
}
/** namespace:mapping.types.geo.geo_shape **/
enum GeoTree {
	geohash = 0,
	quadtree = 1
}
/** namespace:modules.cluster.shard_allocation **/
enum AllocationEnable {
	all = 0,
	primaries = 1,
	new_primaries = 2,
	none = 3
}
/** namespace:modules.cluster.shard_allocation **/
enum AllowRebalance {
	always = 0,
	indices_primaries_active = 1,
	indices_all_active = 2
}
/** namespace:modules.cluster.shard_allocation **/
enum RebalanceEnable {
	all = 0,
	primaries = 1,
	replicas = 2,
	none = 3
}
/** namespace:modules.indices.fielddata **/
enum FielddataLoading {
	eager = 0,
	eager_global_ordinals = 1
}
/** namespace:modules.indices.fielddata.geo_point **/
enum GeoPointFielddataFormat {
	array = 0,
	doc_values = 1,
	compressed = 2,
	disabled = 3
}
/** namespace:modules.indices.fielddata.numeric **/
enum NumericFielddataFormat {
	array = 0,
	disabled = 1
}
/** namespace:modules.indices.fielddata.string **/
enum StringFielddataFormat {
	paged_bytes = 0,
	disabled = 1
}
/** namespace:modules.scripting **/
enum ScriptLang {
	painless = 0,
	expression = 1,
	mustache = 2
}
/** namespace:query_dsl.compound.function_score.functions.decay **/
enum MultiValueMode {
	min = 0,
	max = 1,
	avg = 2,
	sum = 3
}
/** namespace:query_dsl.compound.function_score.functions.field_value **/
enum FieldValueFactorModifier {
	none = 0,
	log = 1,
	log1p = 2,
	log2p = 3,
	ln = 4,
	ln1p = 5,
	ln2p = 6,
	square = 7,
	sqrt = 8,
	reciprocal = 9
}
/** namespace:query_dsl.compound.function_score.functions **/
enum FunctionBoostMode {
	multiply = 0,
	replace = 1,
	sum = 2,
	avg = 3,
	max = 4,
	min = 5
}
/** namespace:query_dsl.compound.function_score.functions **/
enum FunctionScoreMode {
	multiply = 0,
	sum = 1,
	avg = 2,
	first = 3,
	max = 4,
	min = 5
}
/** namespace:query_dsl.full_text.multi_match **/
enum TextQueryType {
	best_fields = 0,
	most_fields = 1,
	cross_fields = 2,
	phrase = 3,
	phrase_prefix = 4
}
/** namespace:query_dsl.full_text.multi_match **/
enum ZeroTermsQuery {
	all = 0,
	none = 1
}
/** namespace:query_dsl.full_text.simple_query_string **/
enum SimpleQueryStringFlags {
	NONE = 1,
	AND = 2,
	OR = 4,
	NOT = 8,
	PREFIX = 16,
	PHRASE = 32,
	PRECEDENCE = 64,
	ESCAPE = 128,
	WHITESPACE = 256,
	FUZZY = 512,
	NEAR = 1024,
	SLOP = 2048,
	ALL = 4096
}
/** namespace:query_dsl.geo.bounding_box **/
enum GeoExecution {
	memory = 0,
	indexed = 1
}
/** namespace:query_dsl.geo **/
enum GeoValidationMethod {
	coerce = 0,
	ignore_malformed = 1,
	strict = 2
}
/** namespace:query_dsl.joining.has_child **/
enum ChildScoreMode {
	none = 0,
	avg = 1,
	sum = 2,
	max = 3,
	min = 4
}
/** namespace:query_dsl.joining.nested **/
enum NestedScoreMode {
	avg = 0,
	sum = 1,
	min = 2,
	max = 3,
	none = 4
}
/** namespace:query_dsl.multi_term_query_rewrite **/
enum RewriteMultiTerm {
	constant_score = 0,
	scoring_boolean = 1,
	constant_score_boolean = 2,
	top_terms_N = 3,
	top_terms_boost_N = 4,
	top_terms_blended_freqs_N = 5
}
/** namespace:query_dsl **/
enum Operator {
	and = 0,
	or = 1
}
/** namespace:query_dsl.term_level.range **/
enum RangeRelation {
	within = 0,
	contains = 1,
	intersects = 2
}
/** namespace:query_dsl.visitor **/
enum VisitorScope {
	Unknown = 0,
	Query = 1,
	Filter = 2,
	Must = 3,
	MustNot = 4,
	Should = 5,
	PositiveQuery = 6,
	NegativeQuery = 7,
	Span = 8
}
/** namespace:search.search.highlighting **/
enum BoundaryScanner {
	chars = 0,
	sentence = 1,
	word = 2
}
/** namespace:search.search.highlighting **/
enum HighlighterEncoder {
	default = 0,
	html = 1
}
/** namespace:search.search.highlighting **/
enum HighlighterFragmenter {
	simple = 0,
	span = 1
}
/** namespace:search.search.highlighting **/
enum HighlighterOrder {
	score = 0
}
/** namespace:search.search.highlighting **/
enum HighlighterTagsSchema {
	styled = 0
}
/** namespace:search.search.highlighting **/
enum HighlighterType {
	plain = 0,
	fvh = 1,
	unified = 2
}
/** namespace:search.search.rescoring **/
enum ScoreMode {
	avg = 0,
	max = 1,
	min = 2,
	multiply = 3,
	total = 4
}
/** namespace:search.search.sort **/
enum SortMode {
	min = 0,
	max = 1,
	sum = 2,
	avg = 3
}
/** namespace:search.search.sort **/
enum SortOrder {
	asc = 0,
	desc = 1
}
/** namespace:search.search.sort **/
enum SortSpecialField {
	_score = 0,
	_doc = 1
}
/** namespace:search.suggesters.term_suggester **/
enum StringDistance {
	internal = 0,
	damerau_levenshtein = 1,
	levenstein = 2,
	jarowinkler = 3,
	ngram = 4
}
/** namespace:search.suggesters.term_suggester **/
enum SuggestSort {
	score = 0,
	frequency = 1
}
/** namespace:x_pack.migration.deprecation_info **/
enum DeprecationWarningLevel {
	none = 0,
	info = 1,
	warning = 2,
	critical = 3
}
/** namespace:x_pack.license.get_license **/
enum LicenseStatus {
	active = 0,
	valid = 1,
	invalid = 2,
	expired = 3
}
/** namespace:x_pack.license.get_license **/
enum LicenseType {
	missing = 0,
	trial = 1,
	basic = 2,
	standard = 3,
	dev = 4,
	silver = 5,
	gold = 6,
	platinum = 7
}
/** namespace:x_pack.machine_learning.datafeed **/
enum ChunkingMode {
	auto = 0,
	manual = 1,
	off = 2
}
/** namespace:x_pack.machine_learning.datafeed **/
enum DatafeedState {
	started = 0,
	stopped = 1,
	starting = 2,
	stopping = 3
}
/** namespace:x_pack.machine_learning.job.config **/
enum JobState {
	closing = 0,
	closed = 1,
	opened = 2,
	failed = 3,
	opening = 4
}
/** namespace:x_pack.machine_learning.job.config **/
enum MemoryStatus {
	ok = 0,
	soft_limit = 1,
	hard_limit = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum CountFunction {
	Count = 0,
	HighCount = 1,
	LowCount = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum NonZeroCountFunction {
	NonZeroCount = 0,
	LowNonZeroCount = 1,
	HighNonZeroCount = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum DistinctCountFunction {
	DistinctCount = 0,
	LowDistinctCount = 1,
	HighDistinctCount = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum GeographicFunction {
	LatLong = 0
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum InfoContentFunction {
	InfoContent = 0,
	HighInfoContent = 1,
	LowInfoContent = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum MetricFunction {
	Min = 0,
	Max = 1,
	Median = 2,
	HighMedian = 3,
	LowMedian = 4,
	Mean = 5,
	HighMean = 6,
	LowMean = 7,
	Metric = 8,
	Varp = 9,
	HighVarp = 10,
	LowVarp = 11
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum RareFunction {
	Rare = 0,
	FreqRare = 1
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum SumFunction {
	Sum = 0,
	HighSum = 1,
	LowSum = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum NonNullSumFunction {
	NonNullSum = 0,
	HighNonNullSum = 1,
	LowNonNullSum = 2
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum TimeFunction {
	TimeOfDay = 0,
	TimeOfWeek = 1
}
/** namespace:x_pack.machine_learning.put_job **/
enum ExcludeFrequent {
	all = 0,
	none = 1,
	by = 2,
	over = 3
}
/** namespace:x_pack.security.user.get_user_access_token **/
enum AccessTokenGrantType {
	password = 0
}
/** namespace:x_pack.watcher.acknowledge_watch **/
enum AcknowledgementState {
	awaits_successful_execution = 0,
	ackable = 1,
	acked = 2
}
/** namespace:x_pack.watcher.action **/
enum ActionType {
	email = 0,
	webhook = 1,
	index = 2,
	logging = 3,
	hipchat = 4,
	slack = 5,
	pagerduty = 6
}
/** namespace:x_pack.watcher.action.email **/
enum DataAttachmentFormat {
	json = 0,
	yaml = 1
}
/** namespace:x_pack.watcher.action.email **/
enum EmailPriority {
	lowest = 0,
	low = 1,
	normal = 2,
	high = 3,
	highest = 4
}
/** namespace:x_pack.watcher.action.hip_chat **/
enum HipChatMessageFormat {
	html = 0,
	text = 1
}
/** namespace:x_pack.watcher.action.hip_chat **/
enum HipChatMessageColor {
	gray = 0,
	green = 1,
	purple = 2,
	red = 3,
	yellow = 4
}
/** namespace:x_pack.watcher.action.pager_duty **/
enum PagerDutyEventType {
	trigger = 0,
	resolve = 1,
	acknowledge = 2
}
/** namespace:x_pack.watcher.action.pager_duty **/
enum PagerDutyContextType {
	link = 0,
	image = 1
}
/** namespace:x_pack.watcher.condition **/
enum Quantifier {
	some = 0,
	all = 1
}
/** namespace:x_pack.watcher.condition **/
enum ConditionType {
	always = 0,
	never = 1,
	script = 2,
	compare = 3,
	array_compare = 4
}
/** namespace:x_pack.watcher.execute_watch **/
enum ActionExecutionState {
	awaits_execution = 0,
	checking = 1,
	execution_not_needed = 2,
	throttled = 3,
	executed = 4,
	failed = 5,
	deleted_while_queued = 6,
	not_executed_already_queued = 7
}
/** namespace:x_pack.watcher.execution **/
enum ActionExecutionMode {
	simulate = 0,
	force_simulate = 1,
	execute = 2,
	force_execute = 3,
	skip = 4
}
/** namespace:x_pack.watcher.execution **/
enum Status {
	success = 0,
	failure = 1,
	simulated = 2,
	throttled = 3
}
/** namespace:x_pack.watcher.input **/
enum ConnectionScheme {
	http = 0,
	https = 1
}
/** namespace:x_pack.watcher.input **/
enum HttpInputMethod {
	head = 0,
	get = 1,
	post = 2,
	put = 3,
	delete = 4
}
/** namespace:x_pack.watcher.input **/
enum InputType {
	http = 0,
	search = 1,
	simple = 2
}
/** namespace:x_pack.watcher.input **/
enum ResponseContentType {
	json = 0,
	yaml = 1,
	text = 2
}
/** namespace:x_pack.watcher.schedule **/
enum Day {
	sunday = 0,
	monday = 1,
	tuesday = 2,
	wednesday = 3,
	thursday = 4,
	friday = 5,
	saturday = 6
}
/** namespace:x_pack.watcher.schedule **/
enum IntervalUnit {
	s = 0,
	m = 1,
	h = 2,
	d = 3,
	w = 4
}
/** namespace:x_pack.watcher.schedule **/
enum Month {
	january = 0,
	february = 1,
	march = 2,
	april = 3,
	may = 4,
	june = 5,
	july = 6,
	august = 7,
	september = 8,
	october = 9,
	november = 10,
	december = 11
}
/** namespace:x_pack.watcher.watcher_stats **/
enum WatcherState {
	stopped = 0,
	starting = 1,
	started = 2,
	stopping = 3
}
/** namespace:x_pack.watcher.watcher_stats **/
enum ExecutionPhase {
	awaits_execution = 0,
	started = 1,
	input = 2,
	condition = 3,
	actions = 4,
	watch_transform = 5,
	aborted = 6,
	finished = 7
}
@namespace("modules.indices.circuit_breaker")
class CircuitBreakerSettings {
	total_limit: string;
	fielddata_limit: string;
	fielddata_overhead: float;
	request_limit: string;
	request_overhead: float;
}
@namespace("modules.indices.recovery")
class IndicesRecoverySettings {
	concurrent_streams: integer;
	concurrent_small_file_streams: integer;
	file_chunk_size: string;
	translog_operations: integer;
	translog_size: string;
	compress: boolean;
	max_bytes_per_second: string;
}
@namespace("mapping")
class TypeMapping {
	dynamic_date_formats: string[];
	date_detection: boolean;
	numeric_detection: boolean;
	_source: SourceField;
	_all: AllField;
	_routing: RoutingField;
	_index: IndexField;
	_size: SizeField;
	_field_names: FieldNamesField;
	_meta: Map<string, any>;
	dynamic_templates: Map<string, DynamicTemplate>;
	dynamic: Union<boolean, DynamicMapping>;
	properties: Map<PropertyName, Property>;
}
@namespace("mapping.dynamic_template")
class DynamicTemplate {
	match: string;
	unmatch: string;
	match_mapping_type: string;
	path_match: string;
	path_unmatch: string;
	mapping: Property;
}
@namespace("indices.alias_management")
class Alias {
	filter: QueryContainer;
	routing: Routing;
	index_routing: Routing;
	search_routing: Routing;
}
@namespace("document.multiple.bulk.bulk_response_item")
@class_serializer("BulkResponseItemJsonConverter")
class BulkResponseItem {
	operation: string;
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	status: integer;
	error: BulkError;
	_shards: ShardStatistics;
	_seq_no: long;
	_primary_term: long;
	is_valid: boolean;
}
@namespace("document.multiple.multi_get.response")
class MultiGetHit<TDocument> {
	source: TDocument;
	index: string;
	found: boolean;
	type: string;
	version: long;
	id: string;
	parent: string;
	routing: string;
	error: Error;
}
@namespace("document.single.term_vectors")
class TermVectors {
	index: string;
	type: string;
	id: string;
	version: long;
	found: boolean;
	took: long;
	term_vectors: Map<Field, TermVector>;
}
@namespace("common_abstractions.lazy_document")
class LazyDocument {
}
@namespace("modules.scripting")
class StoredScript {
	lang: string;
	source: string;
}
@namespace("modules.snapshot_and_restore.repositories")
class SnapshotRepository {
	type: string;
}
@namespace("aggregations")
class Aggregate {
	meta: Map<string, any>;
}
@namespace("aggregations")
class AggregationContainer {
	meta: Map<string, any>;
	avg: AverageAggregation;
	date_histogram: DateHistogramAggregation;
	percentiles: PercentilesAggregation;
	date_range: DateRangeAggregation;
	extended_stats: ExtendedStatsAggregation;
	filter: FilterAggregation;
	filters: FiltersAggregation;
	geo_distance: GeoDistanceAggregation;
	geohash_grid: GeoHashGridAggregation;
	geo_bounds: GeoBoundsAggregation;
	histogram: HistogramAggregation;
	global: GlobalAggregation;
	ip_range: IpRangeAggregation;
	max: MaxAggregation;
	min: MinAggregation;
	cardinality: CardinalityAggregation;
	missing: MissingAggregation;
	nested: NestedAggregation;
	reverse_nested: ReverseNestedAggregation;
	range: RangeAggregation;
	stats: StatsAggregation;
	sum: SumAggregation;
	terms: TermsAggregation;
	significant_terms: SignificantTermsAggregation;
	value_count: ValueCountAggregation;
	percentile_ranks: PercentileRanksAggregation;
	top_hits: TopHitsAggregation;
	children: ChildrenAggregation;
	scripted_metric: ScriptedMetricAggregation;
	avg_bucket: AverageBucketAggregation;
	derivative: DerivativeAggregation;
	max_bucket: MaxBucketAggregation;
	min_bucket: MinBucketAggregation;
	sum_bucket: SumBucketAggregation;
	stats_bucket: StatsBucketAggregation;
	extended_stats_bucket: ExtendedStatsBucketAggregation;
	percentiles_bucket: PercentilesBucketAggregation;
	moving_avg: MovingAverageAggregation;
	cumulative_sum: CumulativeSumAggregation;
	serial_diff: SerialDifferencingAggregation;
	bucket_script: BucketScriptAggregation;
	bucket_selector: BucketSelectorAggregation;
	sampler: SamplerAggregation;
	geo_centroid: GeoCentroidAggregation;
	matrix_stats: MatrixStatsAggregation;
	adjacency_matrix: AdjacencyMatrixAggregation;
	aggs: Map<string, AggregationContainer>;
}
@namespace("common_options.scripting")
@class_serializer("ScriptJsonConverter")
class Script {
	params: Map<string, any>;
	lang: string;
}
@namespace("aggregations.metric.percentiles.methods")
class PercentilesMethod {
}
@namespace("aggregations.bucket.date_range")
class DateRangeExpression {
	from: DateMath;
	to: DateMath;
	key: string;
}
@namespace("query_dsl.abstractions.container")
class QueryContainer {
	is_conditionless: boolean;
	is_strict: boolean;
	is_verbatim: boolean;
	is_writable: boolean;
	raw_query: RawQuery;
	bool: BoolQuery;
	match_all: MatchAllQuery;
	match_none: MatchNoneQuery;
	term: TermQuery;
	wildcard: WildcardQuery;
	prefix: PrefixQuery;
	boosting: BoostingQuery;
	ids: IdsQuery;
	constant_score: ConstantScoreQuery;
	dis_max: DisMaxQuery;
	multi_match: MultiMatchQuery;
	match: MatchQuery;
	match_phrase: MatchPhraseQuery;
	match_phrase_prefix: MatchPhrasePrefixQuery;
	fuzzy: FuzzyQuery;
	geo_shape: GeoShapeQuery;
	common: CommonTermsQuery;
	terms: TermsQuery;
	range: RangeQuery;
	regexp: RegexpQuery;
	has_child: HasChildQuery;
	has_parent: HasParentQuery;
	parent_id: ParentIdQuery;
	span_term: SpanTermQuery;
	simple_query_string: SimpleQueryStringQuery;
	query_string: QueryStringQuery;
	more_like_this: MoreLikeThisQuery;
	span_first: SpanFirstQuery;
	span_or: SpanOrQuery;
	span_near: SpanNearQuery;
	span_not: SpanNotQuery;
	span_containing: SpanContainingQuery;
	span_within: SpanWithinQuery;
	span_multi: SpanMultiTermQuery;
	field_masking_span: SpanFieldMaskingQuery;
	nested: NestedQuery;
	function_score: FunctionScoreQuery;
	geo_bounding_box: GeoBoundingBoxQuery;
	geo_distance: GeoDistanceQuery;
	geo_polygon: GeoPolygonQuery;
	script: ScriptQuery;
	exists: ExistsQuery;
	type: TypeQuery;
	percolate: PercolateQuery;
}
@namespace("common_options.fuzziness")
@class_serializer("FuzzinessJsonConverter")
class Fuzziness {
	auto: boolean;
	edit_distance: integer;
	ratio: double;
}
@namespace("query_dsl.abstractions.field_lookup")
class FieldLookup {
	index: IndexName;
	type: TypeName;
	id: Id;
	path: Field;
	routing: Routing;
}
@namespace("search.search.inner_hits")
class InnerHits {
	name: string;
	from: integer;
	size: integer;
	sort: Sort[];
	highlight: Highlight;
	explain: boolean;
	_source: Union<boolean, SourceFilter>;
	version: boolean;
	script_fields: Map<string, ScriptField>;
	docvalue_fields: Field[];
}
@namespace("search.search.sort")
class Sort {
	sort_key: Field;
	missing: any;
	order: SortOrder;
	mode: SortMode;
	nested_filter: QueryContainer;
	nested_path: Field;
}
@namespace("search.search.highlighting")
class Highlight {
	pre_tags: string[];
	post_tags: string[];
	fragment_size: integer;
	no_match_size: integer;
	number_of_fragments: integer;
	fragment_offset: integer;
	boundary_max_scan: integer;
	encoder: HighlighterEncoder;
	order: HighlighterOrder;
	tags_schema: HighlighterTagsSchema;
	fields: Map<Field, HighlightField>;
	require_field_match: boolean;
	boundary_chars: string;
	max_fragment_length: integer;
	boundary_scanner: BoundaryScanner;
	boundary_scanner_locale: string;
	fragmenter: HighlighterFragmenter;
}
@namespace("search.search.highlighting")
class HighlightField {
	field: Field;
	pre_tags: string[];
	post_tags: string[];
	fragment_size: integer;
	no_match_size: integer;
	number_of_fragments: integer;
	fragment_offset: integer;
	boundary_max_scan: integer;
	order: HighlighterOrder;
	tags_schema: HighlighterTagsSchema;
	require_field_match: boolean;
	boundary_chars: string;
	max_fragment_length: integer;
	boundary_scanner: BoundaryScanner;
	boundary_scanner_locale: string;
	fragmenter: HighlighterFragmenter;
	type: Union<HighlighterType, string>;
	force_source: boolean;
	matched_fields: Field[];
	highlight_query: QueryContainer;
	phrase_limit: integer;
}
@namespace("search.search.source_filtering")
@class_serializer("SourceFilterJsonConverter")
class SourceFilter {
	includes: Field[];
	excludes: Field[];
}
@namespace("common_options.scripting")
class ScriptField {
	script: Script;
}
@namespace("query_dsl.specialized.more_like_this.like")
class LikeDocument {
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	fields: Field[];
	_routing: Routing;
	@prop_serializer("SourceConverter")
	doc: any;
	per_field_analyzer: Map<Field, string>;
}
@namespace("query_dsl.compound.function_score.functions")
class ScoreFunction {
	filter: QueryContainer;
	weight: double;
}
@namespace("query_dsl.geo.bounding_box")
class BoundingBox {
	top_left: GeoLocation;
	bottom_right: GeoLocation;
}
@namespace("common_options.range")
class AggregationRange {
	from: double;
	to: double;
	key: string;
}
@namespace("common")
class IpRange {
	from: string;
	to: string;
	mask: string;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class MutualInformationHeuristic {
	include_negatives: boolean;
	background_is_superset: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class ChiSquareHeuristic {
	include_negatives: boolean;
	background_is_superset: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class GoogleNormalizedDistanceHeuristic {
	background_is_superset: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class PercentageScoreHeuristic {
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class ScriptedHeuristic {
	script: Script;
}
@namespace("aggregations.pipeline.moving_average.models")
class MovingAverageModel {
	name: string;
}
@namespace("x_pack.machine_learning.datafeed")
class ChunkingConfig {
	mode: ChunkingMode;
	time_span: Time;
}
@namespace("x_pack.machine_learning.job.config")
class AnalysisConfig {
	bucket_span: Time;
	categorization_field_name: Field;
	categorization_filters: string[];
	detectors: Detector[];
	influencers: Field[];
	latency: Time;
	multivariate_by_fields: boolean;
	summary_count_field_name: Field;
}
@namespace("x_pack.machine_learning.job.detectors")
class Detector {
	detector_description: string;
	exclude_frequent: ExcludeFrequent;
	function: string;
	use_null: boolean;
	detector_index: integer;
}
@namespace("x_pack.machine_learning.job.config")
class AnalysisLimits {
	categorization_examples_limit: long;
	model_memory_limit: string;
}
@namespace("x_pack.machine_learning.job.config")
class DataDescription {
	format: string;
	time_field: Field;
	time_format: string;
}
@namespace("x_pack.watcher.trigger")
class TriggerEventContainer {
	schedule: ScheduleTriggerEvent;
}
@namespace("x_pack.watcher.input")
class HttpInputAuthentication {
	basic: HttpInputBasicAuthentication;
}
@namespace("x_pack.watcher.input")
class HttpInputBasicAuthentication {
	username: string;
	password: string;
}
@namespace("x_pack.watcher.input")
class HttpInputProxy {
	host: string;
	port: integer;
}
@namespace("x_pack.watcher.action.pager_duty")
class PagerDutyContext {
	type: PagerDutyContextType;
	href: string;
	src: string;
}
@namespace("x_pack.watcher.action.hip_chat")
class HipChatMessage {
	body: string;
	format: HipChatMessageFormat;
	color: HipChatMessageColor;
	notify: boolean;
	from: string;
	room: string | string[];
	user: string | string[];
}
@namespace("x_pack.watcher.action.slack")
class SlackMessage {
	from: string;
	to: string[];
	icon: string;
	text: string;
	attachments: SlackAttachment[];
	dynamic_attachments: SlackDynamicAttachment;
}
@namespace("x_pack.watcher.action.slack")
class SlackAttachment {
	fallback: string;
	color: string;
	pretext: string;
	author_name: string;
	author_link: string;
	author_icon: string;
	title: string;
	title_link: string;
	text: string;
	fields: SlackAttachmentField[];
	image_url: string;
	thumb_url: string;
	footer: string;
	footer_icon: string;
	@prop_serializer("EpochSecondsDateTimeJsonConverter")
	ts: Date;
}
@namespace("x_pack.watcher.action.slack")
class SlackAttachmentField {
	title: string;
	value: string;
	short: boolean;
}
@namespace("x_pack.watcher.action.slack")
class SlackDynamicAttachment {
	list_path: string;
	attachment_template: SlackAttachment;
}
@namespace("x_pack.watcher.input")
class InputContainer {
	http: HttpInput;
	search: SearchInput;
	simple: SimpleInput;
	chain: ChainInput;
}
@namespace("x_pack.watcher.input")
class HttpInputRequest {
	scheme: ConnectionScheme;
	port: integer;
	host: string;
	path: string;
	method: HttpInputMethod;
	headers: Map<string, string>;
	params: Map<string, string>;
	url: string;
	auth: HttpInputAuthentication;
	proxy: HttpInputProxy;
	connection_timeout: Time;
	read_timeout: Time;
	body: string;
}
@namespace("x_pack.watcher.input")
class SearchInputRequest {
	indices: IndexName[];
	types: TypeName[];
	search_type: SearchType;
	indices_options: IndicesOptions;
	body: SearchRequest;
	template: SearchTemplateRequest;
}
@namespace("x_pack.watcher.input")
class IndicesOptions {
	expand_wildcards: ExpandWildcards;
	ignore_unavailable: boolean;
	allow_no_indices: boolean;
}
@namespace("search.suggesters")
class SuggestBucket {
	text: string;
	prefix: string;
	regex: string;
	term: TermSuggester;
	phrase: PhraseSuggester;
	completion: CompletionSuggester;
}
@namespace("search.suggesters.phrase_suggester")
class DirectGenerator {
	field: Field;
	size: integer;
	prefix_length: integer;
	suggest_mode: SuggestMode;
	min_word_length: integer;
	max_edits: integer;
	max_inspections: double;
	min_doc_freq: double;
	max_term_freq: double;
	pre_filter: string;
	post_filter: string;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestHighlight {
	pre_tag: string;
	post_tag: string;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestCollate {
	query: PhraseSuggestCollateQuery;
	prune: boolean;
	params: Map<string, any>;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestCollateQuery {
	source: string;
	id: Id;
}
@namespace("search.suggesters.completion_suggester")
class FuzzySuggester {
	transpositions: boolean;
	min_length: integer;
	prefix_length: integer;
	fuzziness: Fuzziness;
	unicode_aware: boolean;
}
@namespace("search.suggesters.context_suggester")
class SuggestContextQuery {
	context: Context;
	boost: double;
	prefix: boolean;
	precision: Union<Distance, integer>;
	neighbours: Union<Distance[], integer[]>;
}
@namespace("search.search.collapsing")
class FieldCollapse {
	field: Field;
	inner_hits: InnerHits;
	max_concurrent_group_searches: integer;
}
@namespace("search.search.rescoring")
class Rescore {
	window_size: integer;
	query: RescoreQuery;
}
@namespace("search.search.rescoring")
class RescoreQuery {
	rescore_query: QueryContainer;
	query_weight: double;
	rescore_query_weight: double;
	score_mode: ScoreMode;
}
@namespace("search.scroll.scroll")
class SlicedScroll {
	id: integer;
	max: integer;
	field: Field;
}
@namespace("x_pack.watcher.condition")
class ConditionContainer {
	always: AlwaysCondition;
	never: NeverCondition;
	compare: CompareCondition;
	array_compare: ArrayCompareCondition;
	script: ScriptCondition;
}
@namespace("x_pack.watcher.condition")
@class_serializer("ArrayCompareConditionConverter")
class ArrayCompareCondition {
	array_path: string;
	path: string;
	comparison: string;
	value: any;
	quantifier: Quantifier;
}
@namespace("x_pack.watcher.trigger")
class TriggerContainer {
	schedule: ScheduleContainer;
}
@namespace("x_pack.watcher.schedule")
class ScheduleContainer {
	hourly: HourlySchedule;
	daily: DailySchedule;
	weekly: TimeOfWeek[];
	monthly: TimeOfMonth[];
	yearly: TimeOfYear[];
	cron: CronExpression;
	interval: Interval;
}
@namespace("x_pack.watcher.schedule")
class TimeOfDay {
	hour: integer[];
	minute: integer[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfWeek {
	on: Day | Day[];
	at: string | string[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfMonth {
	on: integer | integer[];
	at: string | string[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfYear {
	int: Month | Month[];
	on: integer | integer[];
	at: string | string[];
}
@namespace("x_pack.watcher.transform")
class TransformContainer {
	search: SearchTransform;
	script: ScriptTransform;
	chain: ChainTransform;
}
@namespace("x_pack.watcher.action")
class Action {
	name: string;
	action_type: ActionType;
	transform: TransformContainer;
	throttle_period: Time;
}
@namespace("cluster.cluster_reroute.commands")
class ClusterRerouteCommand {
	name: string;
}
@namespace("document.multiple.bulk.bulk_operation")
class BulkOperation {
	operation: string;
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	version: long;
	version_type: VersionType;
	routing: Routing;
	parent: Id;
	retry_on_conflict: integer;
}
@namespace("document.multiple.multi_get.request")
class MultiGetOperation {
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	stored_fields: Field[];
	routing: string;
	_source: Union<boolean, SourceFilter>;
	version: long;
	version_type: VersionType;
	can_be_flattened: boolean;
}
@namespace("document.multiple.multi_term_vectors")
class MultiTermVectorOperation {
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	@prop_serializer("SourceConverter")
	doc: any;
	fields: Field[];
	offsets: boolean;
	payloads: boolean;
	positions: boolean;
	term_statistics: boolean;
	field_statistics: boolean;
	filter: TermVectorFilter;
	version: long;
	version_type: VersionType;
	routing: Routing;
}
@namespace("document.single.term_vectors")
class TermVectorFilter {
	max_num_terms: integer;
	min_term_freq: integer;
	max_term_freq: integer;
	min_doc_freq: integer;
	max_doc_freq: integer;
	min_word_length: integer;
	max_word_length: integer;
}
@namespace("document.multiple.reindex_on_server")
class ReindexSource {
	query: QueryContainer;
	sort: Sort[];
	index: Indices;
	type: Types;
	size: integer;
	remote: RemoteSource;
}
@namespace("document.multiple.reindex_on_server")
class RemoteSource {
	host: Uri;
	username: string;
	password: string;
}
@namespace("document.multiple.reindex_on_server")
class ReindexDestination {
	index: IndexName;
	type: TypeName;
	routing: ReindexRouting;
	op_type: OpType;
	version_type: VersionType;
}
@namespace("indices.alias_management.alias.actions")
class AliasAction {
}
@namespace("analysis.tokenizers")
class Tokenizer {
	version: string;
	type: string;
}
@namespace("analysis.char_filters")
class CharFilter {
	version: string;
	type: string;
}
@namespace("analysis.token_filters")
class TokenFilter {
	version: string;
	type: string;
}
@namespace("ingest")
@class_serializer("PipelineJsonConverter")
class Pipeline {
	description: string;
	processors: Processor[];
	on_failure: Processor[];
}
@namespace("ingest")
class Processor {
	name: string;
	on_failure: Processor[];
}
@namespace("ingest.simulate_pipeline")
class SimulatePipelineDocument {
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	@prop_serializer("SourceConverter")
	_source: any;
}
@namespace("x_pack.machine_learning.job")
class Page {
	from: integer;
	size: integer;
}
@namespace("x_pack.machine_learning.job.config")
class AnalysisMemoryLimit {
	model_memory_limit: string;
}
@namespace("x_pack.machine_learning.job.config")
class ModelPlotConfigEnabled {
	enabled: boolean;
}
@namespace("x_pack.security.role.put_role")
class IndicesPrivileges {
	@prop_serializer("IndicesJsonConverter")
	names: Indices;
	privileges: string[];
	field_security: FieldSecurity;
	query: QueryContainer;
}
@namespace("x_pack.security.role")
class FieldSecurity {
	grant: Field[];
	except: Field[];
}
@namespace("indices.index_management.rollover_index")
class RolloverConditions {
	max_age: Time;
	max_docs: long;
}
@namespace("mapping.meta_fields")
class FieldMapping {
}
@namespace("x_pack.graph.explore.request")
class GraphVertexDefinition {
	field: Field;
	size: integer;
	min_doc_count: long;
	shard_min_doc_count: long;
	exclude: string[];
	include: GraphVertexInclude[];
}
@namespace("x_pack.graph.explore.request")
class Hop {
	query: QueryContainer;
	vertices: GraphVertexDefinition[];
	connections: Hop;
}
@namespace("x_pack.graph.explore.request")
class GraphExploreControls {
	use_significance: boolean;
	sample_size: integer;
	timeout: Time;
	sample_diversity: SampleDiversity;
}
@namespace("modules.indices.fielddata")
class FielddataFrequencyFilter {
	min: double;
	max: double;
	min_segment_size: integer;
}
@namespace("search.suggesters.context_suggester")
@class_serializer("SuggestContextJsonConverter")
class SuggestContext {
	name: string;
	type: string;
	path: Field;
}
@namespace("mapping.meta_fields.source")
class SourceField {
	enabled: boolean;
	compress: boolean;
	compress_threshold: string;
	includes: string[];
	excludes: string[];
}
@namespace("mapping.meta_fields.all")
class AllField {
	enabled: boolean;
	store: boolean;
	store_term_vectors: boolean;
	store_term_vector_offsets: boolean;
	store_term_vector_positions: boolean;
	store_term_vector_payloads: boolean;
	omit_norms: boolean;
	analyzer: string;
	search_analyzer: string;
	similarity: string;
}
@namespace("mapping.meta_fields.routing")
class RoutingField {
	required: boolean;
}
@namespace("mapping.meta_fields.index")
class IndexField {
	enabled: boolean;
}
@namespace("mapping.meta_fields.size")
class SizeField {
	enabled: boolean;
}
@namespace("mapping.meta_fields.field_names")
class FieldNamesField {
	enabled: boolean;
}
@namespace("mapping.types")
class Property {
	name: PropertyName;
	type: string;
	local_metadata: Map<string, any>;
}
@namespace("common_abstractions.response")
class Response {
	server_error: ServerError;
}
@namespace("search.search.hits")
class Hit<TDocument> {
	score: double;
	fields: Map<string, LazyDocument>;
	sorts: any[];
	highlights: Map<string, HighlightHit>;
	explanation: Explanation;
	matched_queries: string[];
	inner_hits: Map<string, InnerHitsResult>;
}
@namespace("query_dsl.nest_specific")
class RawQuery {
	raw: string;
}
@namespace("query_dsl.compound.bool")
class BoolQuery {
	must: QueryContainer[];
	must_not: QueryContainer[];
	should: QueryContainer[];
	filter: QueryContainer[];
	minimum_should_match: MinimumShouldMatch;
	locked: boolean;
}
@namespace("query_dsl")
class MatchAllQuery {
	norm_field: string;
}
@namespace("query_dsl")
class MatchNoneQuery {
}
@namespace("query_dsl.compound.boosting")
class BoostingQuery {
	positive: QueryContainer;
	negative: QueryContainer;
	negative_boost: double;
}
@namespace("query_dsl.term_level.ids")
class IdsQuery {
	type: Types;
	values: Id[];
}
@namespace("query_dsl.compound.constant_score")
class ConstantScoreQuery {
	filter: QueryContainer;
}
@namespace("query_dsl.compound.dismax")
class DisMaxQuery {
	tie_breaker: double;
	queries: QueryContainer[];
}
@namespace("query_dsl.full_text.multi_match")
class MultiMatchQuery {
	type: TextQueryType;
	query: string;
	analyzer: string;
	fuzzy_rewrite: MultiTermQueryRewrite;
	fuzziness: Fuzziness;
	cutoff_frequency: double;
	prefix_length: integer;
	max_expansions: integer;
	slop: integer;
	lenient: boolean;
	use_dis_max: boolean;
	tie_breaker: double;
	minimum_should_match: MinimumShouldMatch;
	operator: Operator;
	fields: Field[];
	zero_terms_query: ZeroTermsQuery;
}
@namespace("query_dsl.joining.has_child")
class HasChildQuery {
	type: TypeName;
	score_mode: ChildScoreMode;
	min_children: integer;
	max_children: integer;
	query: QueryContainer;
	inner_hits: InnerHits;
	ignore_unmapped: boolean;
}
@namespace("query_dsl.joining.has_parent")
class HasParentQuery {
	parent_type: TypeName;
	score: boolean;
	query: QueryContainer;
	inner_hits: InnerHits;
	ignore_unmapped: boolean;
}
@namespace("query_dsl.joining.parent_id")
class ParentIdQuery {
	type: RelationName;
	id: Id;
	ignore_unmapped: boolean;
}
@namespace("query_dsl.full_text.simple_query_string")
class SimpleQueryStringQuery {
	fields: Field[];
	query: string;
	analyzer: string;
	default_operator: Operator;
	flags: SimpleQueryStringFlags;
	lenient: boolean;
	analyze_wildcard: boolean;
	minimum_should_match: MinimumShouldMatch;
	quote_field_suffix: string;
}
@namespace("query_dsl.full_text.query_string")
class QueryStringQuery {
	type: TextQueryType;
	query: string;
	default_field: Field;
	default_operator: Operator;
	analyzer: string;
	quote_analyzer: string;
	allow_leading_wildcard: boolean;
	fuzzy_max_expansions: integer;
	fuzziness: Fuzziness;
	fuzzy_prefix_length: integer;
	phrase_slop: double;
	analyze_wildcard: boolean;
	max_determinized_states: integer;
	minimum_should_match: MinimumShouldMatch;
	lenient: boolean;
	fields: Field[];
	tie_breaker: double;
	rewrite: MultiTermQueryRewrite;
	fuzzy_rewrite: MultiTermQueryRewrite;
	quote_field_suffix: string;
	escape: boolean;
}
@namespace("query_dsl.specialized.more_like_this")
class MoreLikeThisQuery {
	fields: Field[];
	like: Like[];
	unlike: Like[];
	max_query_terms: integer;
	min_term_freq: integer;
	min_doc_freq: integer;
	max_doc_freq: integer;
	min_word_length: integer;
	max_word_length: integer;
	stop_words: StopWords;
	analyzer: string;
	minimum_should_match: MinimumShouldMatch;
	boost_terms: double;
	include: boolean;
	per_field_analyzer: Map<Field, string>;
	version: long;
	version_type: VersionType;
	routing: Routing;
}
@namespace("query_dsl.span")
class SpanQuery {
	span_term: SpanTermQuery;
	span_first: SpanFirstQuery;
	span_near: SpanNearQuery;
	span_or: SpanOrQuery;
	span_not: SpanNotQuery;
	span_containing: SpanContainingQuery;
	span_within: SpanWithinQuery;
	span_multi: SpanMultiTermQuery;
	field_masking_span: SpanFieldMaskingQuery;
}
@namespace("query_dsl.joining.nested")
class NestedQuery {
	score_mode: NestedScoreMode;
	query: QueryContainer;
	path: Field;
	inner_hits: InnerHits;
	ignore_unmapped: boolean;
}
@namespace("query_dsl.compound.function_score")
class FunctionScoreQuery {
	query: QueryContainer;
	functions: ScoreFunction[];
	max_boost: double;
	score_mode: FunctionScoreMode;
	boost_mode: FunctionBoostMode;
	min_score: double;
}
@namespace("query_dsl.specialized.script")
@class_serializer("ScriptQueryConverter")
class ScriptQuery {
	source: string;
	inline: string;
	id: Id;
	params: Map<string, any>;
	lang: string;
}
@namespace("query_dsl.term_level.exists")
class ExistsQuery {
	field: Field;
}
@namespace("query_dsl.term_level.type")
class TypeQuery {
	value: TypeName;
}
@namespace("query_dsl.specialized.percolate")
class PercolateQuery {
	field: Field;
	document_type: TypeName;
	@prop_serializer("SourceConverter")
	document: any;
	id: Id;
	index: IndexName;
	type: TypeName;
	routing: Routing;
	preference: string;
	version: long;
}
@namespace("x_pack.machine_learning.job.config")
class ModelPlotConfig {
	terms: Field[];
}
@namespace("x_pack.watcher.schedule")
class ScheduleTriggerEvent {
	triggered_time: Union<Date, string>;
	scheduled_time: Union<Date, string>;
}
@namespace("x_pack.watcher.input")
class HttpInput {
	extract: string[];
	request: HttpInputRequest;
	response_content_type: ResponseContentType;
}
@namespace("x_pack.watcher.input")
class SearchInput {
	extract: string[];
	request: SearchInputRequest;
	timeout: Time;
}
@namespace("search.suggesters.term_suggester")
class TermSuggester {
	text: string;
	shard_size: integer;
	prefix_length: integer;
	suggest_mode: SuggestMode;
	min_word_length: integer;
	max_edits: integer;
	max_inspections: integer;
	min_doc_freq: double;
	max_term_freq: double;
	sort: SuggestSort;
	lowercase_terms: boolean;
	string_distance: StringDistance;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggester {
	text: string;
	shard_size: integer;
	gram_size: integer;
	real_word_error_likelihood: double;
	confidence: double;
	max_errors: double;
	separator: string;
	direct_generator: DirectGenerator[];
	highlight: PhraseSuggestHighlight;
	collate: PhraseSuggestCollate;
	smoothing: SmoothingModelContainer;
}
@namespace("search.suggesters.completion_suggester")
class CompletionSuggester {
	prefix: string;
	regex: string;
	fuzzy: FuzzySuggester;
	contexts: Map<string, SuggestContextQuery[]>;
}
@namespace("x_pack.watcher.input")
@class_serializer("SimpleInputJsonConverter")
class SimpleInput {
	payload: Map<string, any>;
}
@namespace("x_pack.watcher.input")
@class_serializer("ChainInputJsonConverter")
class ChainInput {
	inputs: Map<string, InputContainer>;
}
@namespace("x_pack.watcher.condition")
class AlwaysCondition {
}
@namespace("x_pack.watcher.condition")
class NeverCondition {
}
@namespace("x_pack.watcher.condition")
@class_serializer("CompareConditionConverter")
class CompareCondition {
	path: string;
	comparison: string;
	value: any;
}
@namespace("x_pack.watcher.condition")
@class_serializer("ScriptConditionJsonConverter")
class ScriptCondition {
	lang: string;
	params: Map<string, any>;
}
@namespace("x_pack.watcher.schedule")
class HourlySchedule {
	minute: integer[];
}
@namespace("x_pack.watcher.schedule")
class DailySchedule {
	at: Union<string[], TimeOfDay>;
}
@namespace("x_pack.watcher.transform")
class SearchTransform {
	request: SearchInputRequest;
	timeout: Time;
}
@namespace("x_pack.watcher.transform")
@class_serializer("ScriptTransformJsonConverter")
class ScriptTransform {
	params: Map<string, any>;
	lang: string;
}
@namespace("x_pack.watcher.transform")
@class_serializer("ChainTransformJsonConverter")
class ChainTransform {
	transforms: TransformContainer[];
}
@namespace("modules.indices.fielddata.numeric")
class NumericFielddata {
	format: NumericFielddataFormat;
}
@namespace("modules.indices.fielddata.string")
class StringFielddata {
	format: StringFielddataFormat;
}
@namespace("aggregations.metric.average")
class AverageAggregation {
}
@namespace("aggregations.bucket.date_histogram")
class DateHistogramAggregation {
	field: Field;
	script: Script;
	params: Map<string, any>;
	interval: Union<DateInterval, Time>;
	format: string;
	min_doc_count: integer;
	time_zone: string;
	offset: string;
	order: HistogramOrder;
	extended_bounds: ExtendedBounds<DateMath>;
	missing: Date;
}
@namespace("aggregations.metric.percentiles")
class PercentilesAggregation {
	percents: double[];
	method: PercentilesMethod;
}
@namespace("aggregations.bucket.date_range")
class DateRangeAggregation {
	field: Field;
	format: string;
	ranges: DateRangeExpression[];
	time_zone: string;
}
@namespace("aggregations.metric.extended_stats")
class ExtendedStatsAggregation {
	sigma: double;
}
@namespace("aggregations.bucket.filter")
class FilterAggregation {
	filter: QueryContainer;
}
@namespace("aggregations.bucket.filters")
class FiltersAggregation {
	filters: Union<Map<string, QueryContainer>, QueryContainer[]>;
	other_bucket: boolean;
	other_bucket_key: string;
}
@namespace("query_dsl.term_level.term")
@class_serializer("FieldNameQueryJsonConverter`1")
class TermQuery {
	@prop_serializer("SourceValueWriteConverter")
	value: any;
}
@namespace("query_dsl.full_text.match")
@class_serializer("FieldNameQueryJsonConverter`1")
class MatchQuery {
	query: string;
	analyzer: string;
	fuzzy_rewrite: MultiTermQueryRewrite;
	fuzziness: Fuzziness;
	fuzzy_transpositions: boolean;
	cutoff_frequency: double;
	prefix_length: integer;
	max_expansions: integer;
	lenient: boolean;
	minimum_should_match: MinimumShouldMatch;
	operator: Operator;
	zero_terms_query: ZeroTermsQuery;
}
@namespace("query_dsl.full_text.match_phrase")
@class_serializer("FieldNameQueryJsonConverter`1")
class MatchPhraseQuery {
	query: string;
	analyzer: string;
	slop: integer;
}
@namespace("query_dsl.full_text.match_phrase_prefix")
@class_serializer("FieldNameQueryJsonConverter`1")
class MatchPhrasePrefixQuery {
	query: string;
	analyzer: string;
	max_expansions: integer;
	slop: integer;
}
@namespace("query_dsl.term_level.fuzzy")
@class_serializer("FuzzyQueryJsonConverter")
class FuzzyQuery {
	prefix_length: integer;
	rewrite: MultiTermQueryRewrite;
	max_expansions: integer;
	transpositions: boolean;
}
@namespace("query_dsl.geo.shape")
@class_serializer("CompositeJsonConverter`2")
class GeoShapeQuery {
	relation: GeoShapeRelation;
}
@namespace("query_dsl.full_text.common_terms")
@class_serializer("FieldNameQueryJsonConverter`1")
class CommonTermsQuery {
	query: string;
	cutoff_frequency: double;
	low_freq_operator: Operator;
	high_freq_operator: Operator;
	minimum_should_match: MinimumShouldMatch;
	analyzer: string;
}
@namespace("query_dsl.term_level.terms")
@class_serializer("TermsQueryJsonConverter")
class TermsQuery {
	terms: any[];
	terms_lookup: FieldLookup;
}
@namespace("query_dsl.term_level.range")
@class_serializer("RangeQueryJsonConverter")
class RangeQuery {
}
@namespace("query_dsl.term_level.regexp")
@class_serializer("FieldNameQueryJsonConverter`1")
class RegexpQuery {
	value: string;
	flags: string;
	max_determinized_states: integer;
}
@namespace("query_dsl.span.first")
class SpanFirstQuery {
	match: SpanQuery;
	end: integer;
}
@namespace("query_dsl.span.near")
class SpanNearQuery {
	clauses: SpanQuery[];
	slop: integer;
	in_order: boolean;
}
@namespace("query_dsl.span.or")
class SpanOrQuery {
	clauses: SpanQuery[];
}
@namespace("query_dsl.span.not")
class SpanNotQuery {
	include: SpanQuery;
	exclude: SpanQuery;
	pre: integer;
	post: integer;
	dist: integer;
}
@namespace("query_dsl.span.containing")
class SpanContainingQuery {
	little: SpanQuery;
	big: SpanQuery;
}
@namespace("query_dsl.span.within")
class SpanWithinQuery {
	little: SpanQuery;
	big: SpanQuery;
}
@namespace("query_dsl.span.multi_term")
class SpanMultiTermQuery {
	match: QueryContainer;
}
@namespace("query_dsl.span.field_masking")
class SpanFieldMaskingQuery {
	field: Field;
	query: SpanQuery;
}
@namespace("query_dsl.geo.bounding_box")
@class_serializer("VariableFieldNameQueryJsonConverter`2")
class GeoBoundingBoxQuery {
	bounding_box: BoundingBox;
	type: GeoExecution;
	validation_method: GeoValidationMethod;
}
@namespace("query_dsl.geo.distance")
@class_serializer("VariableFieldNameQueryJsonConverter`2")
class GeoDistanceQuery {
	location: GeoLocation;
	distance: Distance;
	distance_type: GeoDistanceType;
	validation_method: GeoValidationMethod;
}
@namespace("query_dsl.geo.polygon")
@class_serializer("VariableFieldNameQueryJsonConverter`2")
class GeoPolygonQuery {
	points: GeoLocation[];
	validation_method: GeoValidationMethod;
}
@namespace("aggregations.bucket.geo_distance")
class GeoDistanceAggregation {
	field: Field;
	origin: GeoLocation;
	unit: DistanceUnit;
	distance_type: GeoDistanceType;
	ranges: AggregationRange[];
}
@namespace("aggregations.bucket.geo_hash_grid")
class GeoHashGridAggregation {
	field: Field;
	size: integer;
	shard_size: integer;
	precision: GeoHashPrecision;
}
@namespace("aggregations.metric.geo_bounds")
class GeoBoundsAggregation {
	wrap_longitude: boolean;
}
@namespace("aggregations.bucket.histogram")
class HistogramAggregation {
	field: Field;
	script: Script;
	interval: double;
	min_doc_count: integer;
	order: HistogramOrder;
	extended_bounds: ExtendedBounds<double>;
	offset: double;
	missing: double;
}
@namespace("aggregations.bucket.global")
class GlobalAggregation {
}
@namespace("aggregations.bucket.ip_range")
class IpRangeAggregation {
	field: Field;
	ranges: IpRange[];
}
@namespace("aggregations.metric.max")
class MaxAggregation {
}
@namespace("aggregations.metric.min")
class MinAggregation {
}
@namespace("aggregations.metric.cardinality")
class CardinalityAggregation {
	precision_threshold: integer;
	rehash: boolean;
}
@namespace("aggregations.bucket.missing")
class MissingAggregation {
	field: Field;
}
@namespace("aggregations.bucket.nested")
class NestedAggregation {
	path: Field;
}
@namespace("aggregations.bucket.reverse_nested")
class ReverseNestedAggregation {
	path: Field;
}
@namespace("aggregations.bucket.range")
class RangeAggregation {
	field: Field;
	script: Script;
	ranges: AggregationRange[];
}
@namespace("aggregations.metric.stats")
class StatsAggregation {
}
@namespace("aggregations.metric.sum")
class SumAggregation {
}
@namespace("aggregations.bucket.terms")
class TermsAggregation {
	field: Field;
	script: Script;
	size: integer;
	shard_size: integer;
	min_doc_count: integer;
	execution_hint: TermsAggregationExecutionHint;
	order: TermsOrder[];
	include: TermsInclude;
	exclude: TermsExclude;
	collect_mode: TermsAggregationCollectMode;
	show_term_doc_count_error: boolean;
	missing: any;
}
@namespace("aggregations.bucket.significant_terms")
class SignificantTermsAggregation {
	field: Field;
	size: integer;
	shard_size: integer;
	min_doc_count: long;
	shard_min_doc_count: long;
	execution_hint: TermsAggregationExecutionHint;
	include: SignificantTermsIncludeExclude;
	exclude: SignificantTermsIncludeExclude;
	mutual_information: MutualInformationHeuristic;
	chi_square: ChiSquareHeuristic;
	gnd: GoogleNormalizedDistanceHeuristic;
	percentage: PercentageScoreHeuristic;
	script_heuristic: ScriptedHeuristic;
	background_filter: QueryContainer;
}
@namespace("aggregations.metric.value_count")
class ValueCountAggregation {
}
@namespace("aggregations.metric.percentile_ranks")
class PercentileRanksAggregation {
	values: double[];
	method: PercentilesMethod;
}
@namespace("aggregations.metric.top_hits")
class TopHitsAggregation {
	from: integer;
	size: integer;
	sort: Sort[];
	_source: Union<boolean, SourceFilter>;
	highlight: Highlight;
	explain: boolean;
	script_fields: Map<string, ScriptField>;
	stored_fields: Field[];
	version: boolean;
	track_scores: boolean;
}
@namespace("aggregations.bucket.children")
class ChildrenAggregation {
	type: RelationName;
}
@namespace("aggregations.metric.scripted_metric")
class ScriptedMetricAggregation {
	init_script: Script;
	map_script: Script;
	combine_script: Script;
	reduce_script: Script;
	params: Map<string, any>;
}
@namespace("aggregations.pipeline.average_bucket")
class AverageBucketAggregation {
}
@namespace("aggregations.pipeline.derivative")
class DerivativeAggregation {
}
@namespace("aggregations.pipeline.max_bucket")
class MaxBucketAggregation {
}
@namespace("aggregations.pipeline.min_bucket")
class MinBucketAggregation {
}
@namespace("aggregations.pipeline.sum_bucket")
class SumBucketAggregation {
}
@namespace("aggregations.pipeline.stats_bucket")
class StatsBucketAggregation {
}
@namespace("aggregations.pipeline.extended_stats_bucket")
class ExtendedStatsBucketAggregation {
	sigma: double;
}
@namespace("aggregations.pipeline.percentiles_bucket")
class PercentilesBucketAggregation {
	percents: double[];
}
@namespace("aggregations.pipeline.moving_average")
class MovingAverageAggregation {
	model: MovingAverageModel;
	window: integer;
	minimize: boolean;
	predict: integer;
}
@namespace("aggregations.pipeline.cumulative_sum")
class CumulativeSumAggregation {
}
@namespace("aggregations.pipeline.serial_differencing")
class SerialDifferencingAggregation {
	lag: integer;
}
@namespace("aggregations.pipeline.bucket_script")
class BucketScriptAggregation {
	script: Script;
}
@namespace("aggregations.pipeline.bucket_selector")
class BucketSelectorAggregation {
	script: Script;
}
@namespace("aggregations.bucket.sampler")
class SamplerAggregation {
	shard_size: integer;
	max_docs_per_value: integer;
	script: Script;
	execution_hint: SamplerAggregationExecutionHint;
}
@namespace("aggregations.metric.geo_centroid")
class GeoCentroidAggregation {
}
@namespace("aggregations.matrix.matrix_stats")
class MatrixStatsAggregation {
	mode: MatrixStatsMode;
}
@namespace("aggregations.bucket.adjacency_matrix")
class AdjacencyMatrixAggregation {
	filters: Map<string, QueryContainer>;
}
@namespace("indices.index_settings.update_index_settings")
@class_serializer("IndexSettingsConverter")
class UpdateIndexSettingsRequest {
	index_settings: Map<string, any>;
	index: Indices;
}
@namespace("x_pack.watcher.put_watch")
class PutWatchRequest {
	trigger: TriggerContainer;
	input: InputContainer;
	condition: ConditionContainer;
	actions: Map<string, Action>;
	metadata: Map<string, any>;
	throttle_period: string;
	transform: TransformContainer;
	id: Id;
}
@namespace("query_dsl.term_level.wildcard")
@class_serializer("FieldNameQueryJsonConverter`1")
class WildcardQuery {
	rewrite: MultiTermQueryRewrite;
}
@namespace("query_dsl.term_level.prefix")
@class_serializer("FieldNameQueryJsonConverter`1")
class PrefixQuery {
	rewrite: MultiTermQueryRewrite;
}
@namespace("search.search")
class SearchRequest {
	timeout: string;
	from: integer;
	size: integer;
	explain: boolean;
	version: boolean;
	track_scores: boolean;
	profile: boolean;
	min_score: double;
	terminate_after: long;
	indices_boost: Map<IndexName, double>;
	sort: Sort[];
	search_after: any[];
	suggest: Map<string, SuggestBucket>;
	highlight: Highlight;
	collapse: FieldCollapse;
	rescore: Rescore[];
	script_fields: Map<string, ScriptField>;
	_source: Union<boolean, SourceFilter>;
	aggs: Map<string, AggregationContainer>;
	slice: SlicedScroll;
	query: QueryContainer;
	post_filter: QueryContainer;
	index: Indices;
	type: Types;
	@request_parameter()
	stored_fields: Field[];
	@request_parameter()
	docvalue_fields: Field[];
}
@namespace("search.search_template")
class SearchTemplateRequest {
	params: Map<string, any>;
	source: string;
	inline: string;
	id: string;
	index: Indices;
	type: Types;
}
@namespace("query_dsl.span.term")
@class_serializer("FieldNameQueryJsonConverter`1")
class SpanTermQuery {
}
@namespace("modules.indices.fielddata")
class FielddataSettings {
	cache_size: string;
	cache_expire: Time;
}
@namespace("common_abstractions.union")
@class_serializer("UnionJsonConverter")
class Union<TFirst, TSecond> {
}
@namespace("cluster.cluster_allocation_explain")
class UnassignedInformation {
	reason: UnassignedInformationReason;
	at: Date;
	last_allocation_status: string;
}
@namespace("cluster.cluster_allocation_explain")
class CurrentNode {
	id: string;
	name: string;
	transport_address: string;
	weight_ranking: string;
	attributes: Map<string, string>;
}
@namespace("cluster.cluster_allocation_explain")
class AllocationDecision {
	decider: string;
	decision: AllocationExplainDecision;
	explanation: string;
}
@namespace("cluster.cluster_allocation_explain")
class NodeAllocationExplanation {
	node_id: string;
	node_name: string;
	transport_address: string;
	node_decision: Decision;
	node_attributes: Map<string, string>;
	store: AllocationStore;
	weight_ranking: integer;
	deciders: AllocationDecision[];
}
@namespace("cluster.cluster_allocation_explain")
class AllocationStore {
	found: boolean;
	in_sync: boolean;
	allocation_id: string;
	matching_sync_id: boolean;
	matching_size_in_bytes: long;
	store_exception: string;
}
@namespace("cluster.cluster_health")
class IndexHealthStats {
	status: Health;
	number_of_shards: integer;
	number_of_replicas: integer;
	active_primary_shards: integer;
	active_shards: integer;
	relocating_shards: integer;
	initializing_shards: integer;
	unassigned_shards: integer;
	shards: Map<string, ShardHealthStats>;
}
@namespace("cluster.cluster_health")
class ShardHealthStats {
	status: Health;
	primary_active: boolean;
	active_shards: integer;
	relocating_shards: integer;
	initializing_shards: integer;
	unassigned_shards: integer;
}
@namespace("cluster.cluster_pending_tasks")
class PendingTask {
	insert_order: integer;
	priority: string;
	source: string;
	time_in_queue_millis: integer;
	time_in_queue: string;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteState {
	version: integer;
	master_node: string;
	blocks: BlockState;
	nodes: Map<string, NodeState>;
	routing_table: RoutingTableState;
	routing_nodes: RoutingNodesState;
}
@namespace("cluster.cluster_state")
class BlockState {
	read_only: boolean;
}
@namespace("cluster.cluster_state")
class NodeState {
	name: string;
	transport_address: string;
	attributes: Map<string, string>;
}
@namespace("cluster.cluster_state")
class RoutingTableState {
	indices: Map<string, IndexRoutingTable>;
}
@namespace("cluster.cluster_state")
class IndexRoutingTable {
	shards: Map<string, RoutingShard[]>;
}
@namespace("cluster.cluster_state")
class RoutingShard {
	allocation_id: AllocationId;
	state: string;
	primary: boolean;
	node: string;
	relocating_node: string;
	shard: integer;
	index: string;
}
@namespace("cluster.cluster_state")
class AllocationId {
	id: string;
}
@namespace("cluster.cluster_state")
class RoutingNodesState {
	unassigned: RoutingShard[];
	nodes: Map<string, RoutingShard[]>;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteExplanation {
	command: string;
	parameters: ClusterRerouteParameters;
	decisions: ClusterRerouteDecision[];
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteParameters {
	index: string;
	shard: integer;
	from_node: string;
	to_node: string;
	node: string;
	allow_primary: boolean;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteDecision {
	decider: string;
	decision: string;
	explanation: string;
}
@namespace("cluster.cluster_state")
class MetadataState {
	templates: Map<string, TemplateMapping>;
	cluster_uuid: string;
	indices: Map<string, MetadataIndexState>;
}
@namespace("cluster.cluster_state")
class MetadataIndexState {
	state: string;
	settings: string[];
	mappings: Map<TypeName, TypeMapping>;
	aliases: string[];
}
@namespace("cluster.nodes_hot_threads")
class HotThreadInformation {
	node_name: string;
	node_id: string;
	threads: string[];
	hosts: string[];
}
@namespace("cluster")
class NodeStatistics {
	total: integer;
	successful: integer;
	failed: integer;
}
@namespace("common_abstractions.response")
class ElasticsearchVersionInfo {
	number: string;
	snapshot_build: boolean;
	lucene_version: string;
}
@namespace("cluster.task_management.list_tasks")
class TaskExecutingNode {
	name: string;
	transport_address: string;
	host: string;
	ip: string;
	tasks: Map<TaskId, TaskState>;
}
@namespace("cluster.task_management.list_tasks")
class TaskState {
	node: string;
	id: long;
	type: string;
	action: string;
	status: TaskStatus;
	description: string;
	start_time_in_millis: long;
	running_time_in_nanos: long;
	parent_task_id: TaskId;
}
@namespace("cluster.task_management.list_tasks")
class TaskStatus {
	total: long;
	updated: long;
	created: long;
	deleted: long;
	batches: long;
	version_conflicts: long;
	noops: long;
	retries: TaskRetries;
	throttled_millis: long;
	requests_per_second: long;
	throttled_until_millis: long;
}
@namespace("cluster.task_management.list_tasks")
class TaskRetries {
	bulk: integer;
	search: integer;
}
@namespace("cluster.task_management.get_task")
class TaskInfo {
	node: string;
	id: long;
	type: string;
	action: string;
	status: TaskStatus;
	description: string;
	start_time_in_millis: long;
	running_time_in_nanos: long;
	cancellable: boolean;
}
@namespace("common_options.hit")
class ShardStatistics {
	total: integer;
	successful: integer;
	failed: integer;
	failures: ShardFailure[];
}
@namespace("document.multiple")
class Retries {
	bulk: long;
	search: long;
}
@namespace("document.multiple")
class BulkIndexByScrollFailure {
	index: string;
	type: string;
	id: string;
	cause: BulkIndexFailureCause;
	status: integer;
}
@namespace("document.single.term_vectors")
class TermVector {
	field_statistics: FieldStatistics;
	terms: Map<string, TermVectorTerm>;
}
@namespace("document.single.term_vectors")
class FieldStatistics {
	doc_count: integer;
	sum_doc_freq: integer;
	sum_ttf: integer;
}
@namespace("document.single.term_vectors")
class TermVectorTerm {
	doc_freq: integer;
	term_freq: integer;
	tokens: Token[];
	ttf: integer;
}
@namespace("document.single.term_vectors")
class Token {
	end_offset: integer;
	payload: string;
	position: integer;
	start_offset: integer;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexNode {
	name: string;
	transport_address: string;
	host: string;
	ip: string;
	roles: string[];
	attributes: Map<string, string>;
	tasks: Map<TaskId, ReindexTask>;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexTask {
	node: string;
	id: long;
	type: string;
	action: string;
	status: ReindexStatus;
	description: string;
	start_time_in_millis: long;
	running_time_in_nanos: long;
	cancellable: boolean;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexStatus {
	total: long;
	updated: long;
	created: long;
	deleted: long;
	batches: long;
	version_conflicts: long;
	noops: long;
	retries: Retries;
	throttled_millis: long;
	requests_per_second: float;
	throttled_until_millis: long;
}
@namespace("search.explain")
class InstantGet<TDocument> {
	found: boolean;
	@prop_serializer("SourceConverter")
	_source: TDocument;
	fields: Map<string, LazyDocument>;
}
@namespace("indices.analyze")
class AnalyzeToken {
	token: string;
	type: string;
	start_offset: long;
	end_offset: long;
	position: long;
	position_length: long;
}
@namespace("indices.analyze")
class AnalyzeDetail {
	custom_analyzer: boolean;
	charfilters: CharFilterDetail[];
	tokenfilters: TokenDetail[];
	tokenizer: TokenDetail;
}
@namespace("indices.analyze")
class CharFilterDetail {
	name: string;
	filtered_text: string[];
}
@namespace("indices.analyze")
class TokenDetail {
	name: string;
	tokens: ExplainAnalyzeToken[];
}
@namespace("indices.analyze")
class ExplainAnalyzeToken {
	token: string;
	type: string;
	start_offset: long;
	end_offset: long;
	position: long;
	positionLength: long;
	termFrequency: long;
	keyword: boolean;
	bytes: string;
}
@namespace("indices.monitoring.indices_segments")
class IndexSegment {
	shards: Map<string, ShardsSegment>;
}
@namespace("indices.monitoring.indices_segments")
@class_serializer("Json")
class ShardsSegment {
	num_committed_segments: integer;
	num_search_segments: integer;
	routing: ShardSegmentRouting;
	segments: Map<string, Segment>;
}
@namespace("indices.monitoring.indices_segments")
class ShardSegmentRouting {
	state: string;
	primary: boolean;
	node: string;
}
@namespace("indices.monitoring.indices_segments")
class Segment {
	generation: integer;
	num_docs: long;
	deleted_docs: long;
	size: string;
	size_in_bytes: double;
	committed: boolean;
	Search: boolean;
}
@namespace("indices.monitoring.indices_shard_stores")
class IndicesShardStores {
	shards: Map<string, ShardStoreWrapper>;
}
@namespace("indices.monitoring.indices_shard_stores")
class ShardStoreWrapper {
	stores: ShardStore[];
}
@namespace("indices.monitoring.indices_shard_stores")
@class_serializer("ShardStoreJsonConverter")
class ShardStore {
	id: string;
	name: string;
	transport_address: string;
	legacy_version: long;
	allocation_id: string;
	store_exception: ShardStoreException;
	allocation: ShardStoreAllocation;
	attributes: Map<string, any>;
}
@namespace("indices.monitoring.indices_shard_stores")
class ShardStoreException {
	type: string;
	reason: string;
}
@namespace("indices.monitoring.indices_stats")
class IndicesStats {
	primaries: IndexStats;
	total: IndexStats;
}
@namespace("indices.monitoring.indices_stats")
class IndexStats {
	docs: DocStats;
	store: StoreStats;
	indexing: IndexingStats;
	get: GetStats;
	search: SearchStats;
	merges: MergesStats;
	refresh: RefreshStats;
	flush: FlushStats;
	warmer: WarmerStats;
	query_cache: QueryCacheStats;
	fielddata: FielddataStats;
	completion: CompletionStats;
	segments: SegmentsStats;
	translog: TranslogStats;
	request_cache: RequestCacheStats;
	recovery: RecoveryStats;
}
@namespace("common_options.stats")
class DocStats {
	count: long;
	deleted: long;
}
@namespace("common_options.stats")
class StoreStats {
	size: string;
	size_in_bytes: double;
}
@namespace("common_options.stats")
class IndexingStats {
	delete_current: long;
	delete_time: string;
	delete_time_in_millis: long;
	delete_total: long;
	index_current: long;
	index_time: string;
	index_time_in_millis: long;
	index_total: long;
	is_throttled: boolean;
	noop_update_total: long;
	throttle_time: string;
	throttle_time_in_millis: long;
	types: Map<string, IndexingStats>;
}
@namespace("common_options.stats")
class GetStats {
	current: long;
	exists_time: string;
	exists_time_in_millis: long;
	exists_total: long;
	missing_time: string;
	missing_time_in_millis: long;
	missing_total: long;
	time: string;
	time_in_millis: long;
	total: long;
}
@namespace("common_options.stats")
class SearchStats {
	open_contexts: long;
	fetch_current: long;
	fetch_time_in_millis: long;
	fetch_total: long;
	query_current: long;
	query_time_in_millis: long;
	query_total: long;
	scroll_current: long;
	scroll_time_in_millis: long;
	scroll_total: long;
	suggest_current: long;
	suggest_time_in_millis: long;
	suggest_total: long;
}
@namespace("common_options.stats")
class MergesStats {
	current: long;
	current_docs: long;
	current_size: string;
	current_size_in_bytes: long;
	total: long;
	total_auto_throttle: string;
	total_auto_throttle_in_bytes: long;
	total_docs: long;
	total_size: string;
	total_size_in_bytes: string;
	total_stopped_time: string;
	total__stopped_time_in_millis: long;
	total_throttled_time: string;
	total_throttled_time_in_millis: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("common_options.stats")
class RefreshStats {
	total: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("common_options.stats")
class FlushStats {
	total: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("common_options.stats")
class WarmerStats {
	current: long;
	total: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("common_options.stats")
class QueryCacheStats {
	memory_size: string;
	memory_size_in_bytes: long;
	total_count: long;
	hit_count: long;
	miss_count: long;
	cache_size: long;
	cache_count: long;
	evictions: long;
}
@namespace("common_options.stats")
class FielddataStats {
	evictions: long;
	memory_size: string;
	memory_size_in_bytes: long;
}
@namespace("common_options.stats")
class CompletionStats {
	size: string;
	size_in_bytes: long;
}
@namespace("common_options.stats")
class SegmentsStats {
	count: long;
	doc_values_memory: string;
	doc_values_memory_in_bytes: long;
	fixed_bit_set_memory: string;
	fixed_bit_set_memory_in_bytes: long;
	index_writer_max_memory: string;
	index_writer_max_memory_in_bytes: long;
	index_writer_memory: string;
	index_writer_memory_in_bytes: long;
	memory: string;
	memory_in_bytes: long;
	norms_memory: string;
	norms_memory_in_bytes: long;
	points_memory: string;
	points_memory_in_bytes: long;
	stored_fields_memory: string;
	stored_fields_memory_in_bytes: long;
	term_vectors_memory: string;
	term_vectors_memory_in_bytes: long;
	terms_memory: string;
	terms_memory_in_bytes: long;
	version_map_memory: string;
	version_map_memory_in_bytes: long;
}
@namespace("common_options.stats")
class TranslogStats {
	operations: long;
	size: string;
	size_in_bytes: long;
}
@namespace("common_options.stats")
class RequestCacheStats {
	evictions: long;
	hit_count: long;
	memory_size: string;
	memory_size_in_bytes: long;
	miss_count: long;
}
@namespace("common_options.stats")
class RecoveryStats {
	current_as_source: long;
	current_as_target: long;
	throttle_time: string;
	throttle_time_in_millis: long;
}
@namespace("indices.status_management.upgrade.upgrade_status")
class UpgradeStatus {
	size: string;
	size_in_bytes: long;
	size_to_upgrade: string;
	size_to_upgrade_in_bytes: string;
	size_to_upgrade_ancient_in_bytes: string;
}
@namespace("ingest.simulate_pipeline")
class PipelineSimulation {
	processor_results: PipelineSimulation[];
	tag: string;
	doc: DocumentSimulation;
}
@namespace("ingest.simulate_pipeline")
class DocumentSimulation {
	_index: string;
	_type: string;
	_id: string;
	_parent: string;
	_routing: string;
	_source: LazyDocument;
	_ingest: Ingest;
}
@namespace("ingest.simulate_pipeline")
class Ingest {
	timestamp: Date;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
class CompactNodeInfo {
	name: string;
}
@namespace("modules.snapshot_and_restore.restore")
class SnapshotRestore {
	snapshot: string;
	indices: IndexName[];
	shards: ShardStatistics;
}
@namespace("modules.snapshot_and_restore.snapshot")
class Snapshot {
	snapshot: string;
	indices: IndexName[];
	state: string;
	start_time: Date;
	start_time_in_millis: long;
	end_time: Date;
	end_time_in_millis: long;
	duration_in_millis: long;
	shards: ShardStatistics;
	failures: SnapshotShardFailure[];
}
@namespace("modules.snapshot_and_restore.snapshot")
class SnapshotShardFailure {
	node_id: string;
	index: string;
	shard_id: string;
	reason: string;
	status: string;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStatus {
	snapshot: string;
	repository: string;
	state: string;
	shards_stats: SnapshotShardsStats;
	stats: SnapshotStats;
	indices: Map<string, SnapshotIndexStats>;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotShardsStats {
	initializing: long;
	started: long;
	finalizing: long;
	done: long;
	failed: long;
	total: long;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStats {
	number_of_files: long;
	processed_files: long;
	total_size_in_bytes: long;
	processed_size_in_bytes: long;
	start_time_in_millis: long;
	time_in_millis: long;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotIndexStats {
	shards_stats: SnapshotShardsStats;
	stats: SnapshotStats;
	shards: Map<string, SnapshotShardsStats>;
}
@namespace("search.explain")
class ExplanationDetail {
	value: float;
	description: string;
	details: ExplanationDetail[];
}
@namespace("search.field_capabilities")
class FieldCapabilities {
	searchable: boolean;
	aggregatable: boolean;
	indices: Indices;
	non_searchable_indices: Indices;
	non_aggregatable_indices: Indices;
}
@namespace("search.search_shards")
class SearchShard {
	state: string;
	primary: boolean;
	node: string;
	relocating_node: string;
	shard: integer;
	index: string;
}
@namespace("search.search_shards")
class SearchNode {
	name: string;
	transport_address: string;
}
@namespace("search.search.profile")
class Profile {
	shards: ShardProfile[];
}
@namespace("search.search.profile")
class ShardProfile {
	id: string;
	searches: SearchProfile[];
	aggregations: AggregationProfile[];
}
@namespace("search.search.profile")
class SearchProfile {
	rewrite_time: long;
	query: QueryProfile[];
	collector: Collector[];
}
@namespace("search.search.profile")
class QueryProfile {
	type: string;
	description: string;
	time_in_nanos: long;
	breakdown: QueryBreakdown;
	children: QueryProfile[];
}
@namespace("search.search.profile")
class QueryBreakdown {
	score: long;
	next_doc: long;
	create_weight: long;
	build_scorer: long;
	advance: long;
	match: long;
}
@namespace("search.search.profile")
class Collector {
	name: string;
	reason: string;
	time_in_nanos: long;
	children: Collector[];
}
@namespace("search.search.profile")
class AggregationProfile {
	type: string;
	description: string;
	time_in_nanos: long;
	breakdown: AggregationBreakdown;
}
@namespace("search.search.profile")
class AggregationBreakdown {
	reduce: long;
	build_aggregation: long;
	build_aggregation_count: long;
	initialize: long;
	intialize_count: long;
	reduce_count: long;
	collect: long;
	collect_count: long;
}
@namespace("search.suggesters")
class Suggest<T> {
	length: integer;
	offset: integer;
	text: string;
	options: SuggestOption<T>[];
}
@namespace("search.suggesters")
class SuggestOption<TDocument> {
	text: string;
	score: double;
	freq: long;
	_index: IndexName;
	_type: TypeName;
	_id: Id;
	@prop_serializer("SourceConverter")
	_source: TDocument;
	contexts: Map<string, Context[]>;
	highlighted: string;
	collate_match: boolean;
}
@namespace("search.search.hits")
class HitsMetadata<T> {
	total: long;
	max_score: double;
	//hits: Hit<T>[];
}
@namespace("search.search.highlighting")
class HighlightHit {
	document_id: string;
	field: string;
	highlights: string[];
}
@namespace("search.explain")
class Explanation {
	value: float;
	description: string;
	details: ExplanationDetail[];
}
@namespace("search.search.hits")
class InnerHitsResult {
	hits: InnerHitsMetadata;
}
@namespace("search.search.hits")
class InnerHitsMetadata {
	total: long;
	max_score: double;
	hits: Hit<LazyDocument>[];
}
@namespace("search.search.hits")
class NestedIdentity {
	field: Field;
	offset: integer;
	_nested: NestedIdentity;
}
@namespace("search.validate")
class ValidationExplanation {
	index: string;
	valid: boolean;
	error: string;
	explanation: string;
}
@namespace("x_pack.migration.deprecation_info")
class DeprecationInfo {
	level: DeprecationWarningLevel;
	message: string;
	url: string;
	details: string;
}
@namespace("x_pack.graph.explore.response")
class GraphConnection {
	doc_count: long;
	source: long;
	target: long;
	weight: double;
}
@namespace("x_pack.graph.explore.response")
class GraphVertex {
	depth: long;
	field: string;
	term: string;
	weight: double;
}
@namespace("x_pack.info.x_pack_info")
class XPackBuildInformation {
	date: Date;
	hash: string;
}
@namespace("x_pack.info.x_pack_info")
class MinimalLicenseInformation {
	uid: string;
	type: LicenseType;
	mode: LicenseType;
	status: LicenseStatus;
	expiry_date_in_millis: long;
}
@namespace("x_pack.info.x_pack_info")
class XPackFeatures {
	watcher: XPackFeature;
	graph: XPackFeature;
	ml: XPackFeature;
	monitoring: XPackFeature;
	security: XPackFeature;
}
@namespace("x_pack.info.x_pack_info")
class XPackFeature {
	description: string;
	available: boolean;
	enabled: boolean;
	native_code_info: NativeCodeInformation;
}
@namespace("x_pack.info.x_pack_info")
class NativeCodeInformation {
	version: string;
	build_hash: string;
}
@namespace("x_pack.info.x_pack_usage")
class XPackUsage {
	available: boolean;
	enabled: boolean;
}
@namespace("x_pack.license.get_license")
class LicenseInformation {
	status: LicenseStatus;
	uid: string;
	type: LicenseType;
	issue_date: Date;
	issue_date_in_millis: long;
	expiry_date: Date;
	expiry_date_in_millis: long;
	max_nodes: long;
	issued_to: string;
	issuer: string;
}
@namespace("x_pack.license.post_license")
class LicenseAcknowledgement {
	message: string;
	license: string[];
}
@namespace("x_pack.machine_learning.job.results")
class AnomalyRecord {
	job_id: string;
	result_type: string;
	probability: double;
	record_score: double;
	initial_record_score: double;
	bucket_span: Time;
	detector_index: integer;
	is_interim: boolean;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
	function: string;
	function_description: string;
	typical: double[];
	actual: double[];
	field_name: string;
	by_field_name: string;
	by_field_value: string;
	causes: AnomalyCause[];
	influencers: Influence[];
	over_field_name: string;
	over_field_value: string;
	partition_field_name: string;
	partition_field_value: string;
}
@namespace("x_pack.machine_learning.job.results")
class AnomalyCause {
	probability: double;
	over_field_name: string;
	over_field_value: string;
	by_field_name: string;
	by_field_value: string;
	correlated_by_field_value: string;
	partition_field_name: string;
	partition_field_value: string;
	function: string;
	function_description: string;
	typical: double[];
	actual: double[];
	influencers: Influence[];
	field_name: string;
}
@namespace("x_pack.machine_learning.job.results")
class Influence {
	influencer_field_name: string;
	influencer_field_values: string[];
}
@namespace("x_pack.machine_learning.job.results")
class Bucket {
	job_id: string;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
	anomaly_score: double;
	bucket_span: Time;
	initial_anomaly_score: double;
	event_count: long;
	is_interim: boolean;
	bucket_influencers: BucketInfluencer[];
	processing_time_ms: double;
	partition_scores: PartitionScore[];
	result_type: string;
}
@namespace("x_pack.machine_learning.job.results")
class BucketInfluencer {
	job_id: string;
	result_type: string;
	influencer_field_name: string;
	influencer_field_value: string;
	initial_influencer_score: double;
	influencer_score: double;
	probability: double;
	bucket_span: long;
	is_interim: boolean;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
}
@namespace("x_pack.machine_learning.job.results")
class PartitionScore {
	partition_field_name: string;
	partition_field_value: string;
	initial_record_score: double;
	record_score: double;
	probability: double;
}
@namespace("x_pack.machine_learning.job.results")
class CategoryDefinition {
	job_id: string;
	category_id: long;
	terms: string;
	regex: string;
	max_matching_length: long;
	examples: string[];
}
@namespace("x_pack.machine_learning.datafeed")
class DatafeedStats {
	datafeed_id: string;
	state: DatafeedState;
	node: DiscoveryNode;
	assignment_explanation: string;
}
@namespace("x_pack.machine_learning.datafeed")
class DiscoveryNode {
	id: string;
	name: string;
	ephemeral_id: string;
	transport_address: string;
	attributes: Map<string, string>;
}
@namespace("x_pack.machine_learning.datafeed")
class DatafeedConfig {
	datafeed_id: string;
	aggregations: Map<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	@prop_serializer("IndicesJsonConverter")
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Map<string, ScriptField>;
	scroll_size: integer;
	@prop_serializer("TypesJsonConverter")
	types: Types;
}
@namespace("aggregations.bucket.histogram")
@class_serializer("KeyValueJsonConverter`2")
class HistogramOrder {
	key: string;
	order: SortOrder;
	count_ascending: HistogramOrder;
	count_descending: HistogramOrder;
	key_ascending: HistogramOrder;
	key_descending: HistogramOrder;
}
@namespace("aggregations.bucket.histogram")
class ExtendedBounds<T> {
	min: T;
	max: T;
}
@namespace("common_options.geo")
@class_serializer("DistanceJsonConverter")
class Distance {
	precision: double;
	unit: DistanceUnit;
}
@namespace("aggregations.bucket.terms")
@class_serializer("KeyValueJsonConverter`2")
class TermsOrder {
	key: string;
	order: SortOrder;
	count_ascending: TermsOrder;
	count_descending: TermsOrder;
	term_ascending: TermsOrder;
	term_descending: TermsOrder;
	key_ascending: TermsOrder;
	key_descending: TermsOrder;
}
@namespace("aggregations.bucket.terms")
@class_serializer("TermsIncludeJsonConverter")
class TermsInclude {
	pattern: string;
	values: string[];
	partition: long;
	num_partitions: long;
}
@namespace("aggregations.bucket.terms")
@class_serializer("TermsExcludeJsonConverter")
class TermsExclude {
	pattern: string;
	values: string[];
}
@namespace("common")
@class_serializer("SignificantTermsIncludeExcludeJsonConverter")
class SignificantTermsIncludeExclude {
	pattern: string;
	values: string[];
}
@namespace("x_pack.machine_learning.job.config")
class JobStats {
	assignment_explanation: string;
	job_id: string;
	data_counts: DataCounts;
	model_size_stats: ModelSizeStats;
	state: JobState;
	node: DiscoveryNode;
	open_time: Time;
}
@namespace("x_pack.machine_learning.job.process")
class DataCounts {
	job_id: string;
	processed_record_count: long;
	processed_field_count: long;
	input_bytes: long;
	input_field_count: long;
	invalid_date_count: long;
	missing_field_count: long;
	out_of_order_timestamp_count: long;
	empty_bucket_count: long;
	sparse_bucket_count: long;
	bucket_count: long;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	earliest_record_timestamp: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	latest_record_timestamp: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	latest_empty_bucket_timestamp: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	last_data_time: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	latest_sparse_bucket_timestamp: Date;
	input_record_count: long;
}
@namespace("x_pack.machine_learning.job.process")
class ModelSizeStats {
	job_id: string;
	result_type: string;
	model_bytes: long;
	total_by_field_count: long;
	total_over_field_count: long;
	total_partition_field_count: long;
	bucket_allocation_failures_count: long;
	memory_status: MemoryStatus;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	log_time: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
}
@namespace("x_pack.info.x_pack_usage")
class Job {
	job_id: string;
	job_type: string;
	description: string;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	create_time: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	finished_time: Date;
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	background_persist_interval: Time;
	data_description: DataDescription;
	model_snapshot_retention_days: long;
	model_snapshot_id: string;
	results_index_name: string;
	model_plot: ModelPlotConfig;
	renormalization_window_days: long;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.job.process")
class ModelSnapshot {
	job_id: string;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
	description: string;
	snapshot_id: string;
	snapshot_doc_count: long;
	model_size_stats: ModelSizeStats;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	latest_record_time_stamp: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	latest_result_time_stamp: Date;
	retain: boolean;
}
@namespace("x_pack.security")
class SecurityNode {
	name: string;
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
class PutRoleMappingStatus {
	created: boolean;
}
@namespace("x_pack.security.role.put_role")
class PutRoleStatus {
	created: boolean;
}
@namespace("x_pack.security.user.put_user")
class PutUserStatus {
	created: boolean;
}
@namespace("x_pack.watcher.acknowledge_watch")
class WatchStatus {
	version: integer;
	state: ActivationState;
	last_checked: Date;
	last_met_condition: Date;
	actions: Map<string, ActionStatus>;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ActivationState {
	timestamp: Date;
	active: boolean;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ActionStatus {
	ack: AcknowledgeState;
	last_execution: ExecutionState;
	last_successful_execution: ExecutionState;
	last_throttle: ThrottleState;
}
@namespace("x_pack.watcher.acknowledge_watch")
class AcknowledgeState {
	timestamp: Date;
	state: AcknowledgementState;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ExecutionState {
	timestamp: Date;
	successful: boolean;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ThrottleState {
	timestamp: Date;
	reason: string;
}
@namespace("x_pack.watcher.activate_watch")
class ActivationStatus {
	state: ActivationState;
	actions: Map<string, ActionStatus>;
}
@namespace("x_pack.watcher.execute_watch")
class WatchRecord {
	watch_id: string;
	messages: string[];
	state: ActionExecutionState;
	trigger_event: TriggerEventResult;
	condition: ConditionContainer;
	input: InputContainer;
	metadata: Map<string, any>;
	result: ExecutionResult;
}
@namespace("x_pack.watcher.execute_watch")
class TriggerEventResult {
	type: string;
	triggered_time: Date;
	manual: TriggerEventContainer;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResult {
	execution_time: Date;
	execution_duration: integer;
	input: ExecutionResultInput;
	condition: ExecutionResultCondition;
	actions: ExecutionResultAction[];
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultInput {
	type: InputType;
	status: Status;
	payload: Map<string, any>;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultCondition {
	type: ConditionType;
	status: Status;
	met: boolean;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultAction {
	id: string;
	type: ActionType;
	status: Status;
	email: EmailActionResult;
	index: IndexActionResult;
	webhook: WebhookActionResult;
	logging: LoggingActionResult;
	pagerduty: PagerDutyActionResult;
	hipchat: HipChatActionResult;
	slack: SlackActionResult;
	reason: string;
}
@namespace("x_pack.watcher.execution.email")
class EmailActionResult {
	reason: string;
	account: string;
	message: EmailResult;
}
@namespace("x_pack.watcher.execution.email")
class EmailResult {
	id: string;
	sent_date: Date;
	from: string;
	to: string[];
	cc: string[];
	bcc: string[];
	reply_to: string[];
	subject: string;
	body: EmailBody;
	priority: EmailPriority;
}
@namespace("x_pack.watcher.execution.index")
class IndexActionResult {
	id: string;
	response: IndexActionResultIndexResponse;
}
@namespace("x_pack.watcher.execution.index")
class IndexActionResultIndexResponse {
	index: IndexName;
	type: TypeName;
	version: integer;
	created: boolean;
	result: Result;
	id: string;
}
@namespace("x_pack.watcher.execution.webhook")
class WebhookActionResult {
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
}
@namespace("x_pack.watcher.execution")
class HttpInputResponseResult {
	status: integer;
	headers: Map<string, string[]>;
	body: string;
}
@namespace("x_pack.watcher.execution.logging")
class LoggingActionResult {
	logged_text: string;
}
@namespace("x_pack.watcher.execution.pager_duty")
class PagerDutyActionResult {
	sent_event: PagerDutyActionEventResult;
}
@namespace("x_pack.watcher.execution.pager_duty")
class PagerDutyActionEventResult {
	event: PagerDutyEvent;
	reason: string;
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
}
@namespace("x_pack.watcher.execution.hip_chat")
class HipChatActionResult {
	account: string;
	sent_messages: HipChatActionMessageResult[];
}
@namespace("x_pack.watcher.execution.hip_chat")
class HipChatActionMessageResult {
	status: Status;
	reason: string;
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
	room: string;
	user: string;
	message: HipChatMessage;
}
@namespace("x_pack.watcher.execution.slack")
class SlackActionResult {
	account: string;
	sent_messages: SlackActionMessageResult[];
}
@namespace("x_pack.watcher.execution.slack")
class SlackActionMessageResult {
	status: Status;
	reason: string;
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
	to: string;
	message: SlackMessage;
}
@namespace("x_pack.watcher")
class Watch {
	metadata: Map<string, any>;
	input: InputContainer;
	condition: ConditionContainer;
	trigger: TriggerContainer;
	transform: TransformContainer;
	@prop_serializer("ActionsJsonConverter")
	actions: Map<string, Action>;
	status: WatchStatus;
	throttle_period: string;
}
@namespace("x_pack.watcher.watcher_stats")
class WatcherNodeStats {
	watcher_state: WatcherState;
	watch_count: long;
	execution_thread_pool: ExecutionThreadPool;
	current_watches: WatchRecordStats[];
	queued_watches: WatchRecordQueuedStats[];
}
@namespace("x_pack.watcher.watcher_stats")
class ExecutionThreadPool {
	queue_size: long;
	max_size: long;
}
@namespace("x_pack.watcher.watcher_stats")
class WatchRecordQueuedStats {
	watch_id: string;
	watch_record_id: string;
	triggered_time: Date;
	execution_time: Date;
}
@namespace("document.multiple.reindex_on_server")
@class_serializer("ReindexRoutingJsonConverter")
class ReindexRouting {
}
@namespace("x_pack.license.get_license")
class License {
	uid: string;
	signature: string;
	type: LicenseType;
	issue_date_in_millis: long;
	expiry_date_in_millis: long;
	max_nodes: long;
	issued_to: string;
	issuer: string;
}
@namespace("x_pack.security.role_mapping.rules.role")
@class_serializer("RoleMappingRuleBaseJsonConverter")
class RoleMappingRuleBase {
}
@namespace("x_pack.watcher.execution")
@class_serializer("SimulatedActionsConverter")
class SimulatedActions {
	use_all: boolean;
	actions: string[];
	all: SimulatedActions;
}
@namespace("cluster.cluster_stats")
class ClusterIndicesStats {
	completion: CompletionStats;
	count: long;
	docs: DocStats;
	fielddata: FielddataStats;
	query_cache: QueryCacheStats;
	segments: SegmentsStats;
	shards: ClusterIndicesShardsStats;
	store: StoreStats;
}
@namespace("cluster.cluster_stats")
class ClusterIndicesShardsStats {
	total: double;
	primaries: double;
	replication: double;
	index: ClusterIndicesShardsIndexStats;
}
@namespace("cluster.cluster_stats")
class ClusterIndicesShardsIndexStats {
	shards: ClusterShardMetrics;
	primaries: ClusterShardMetrics;
	replication: ClusterShardMetrics;
}
@namespace("cluster.cluster_stats")
class ClusterShardMetrics {
	min: double;
	max: double;
	avg: double;
}
@namespace("cluster.cluster_stats")
class ClusterNodesStats {
	count: ClusterNodeCount;
	versions: string[];
	os: ClusterOperatingSystemStats;
	process: ClusterProcess;
	jvm: ClusterJvm;
	fs: ClusterFileSystem;
	plugins: PluginStats[];
}
@namespace("cluster.cluster_stats")
class ClusterNodeCount {
	total: integer;
	coordinating_only: integer;
	master: integer;
	data: integer;
	ingest: integer;
}
@namespace("cluster.cluster_stats")
class ClusterOperatingSystemStats {
	available_processors: integer;
	allocated_processors: integer;
	names: ClusterOperatingSystemName[];
}
@namespace("cluster.cluster_stats")
class ClusterOperatingSystemName {
	count: integer;
	name: string;
}
@namespace("cluster.cluster_stats")
class ClusterProcess {
	cpu: ClusterProcessCpu;
	open_file_descriptors: ClusterProcessOpenFileDescriptors;
}
@namespace("cluster.cluster_stats")
class ClusterProcessCpu {
	percent: integer;
}
@namespace("cluster.cluster_stats")
class ClusterProcessOpenFileDescriptors {
	min: long;
	max: long;
	avg: long;
}
@namespace("cluster.cluster_stats")
class ClusterJvm {
	max_uptime: string;
	max_uptime_in_millis: long;
	versions: ClusterJvmVersion[];
	mem: ClusterJvmMemory;
	threads: long;
}
@namespace("cluster.cluster_stats")
class ClusterJvmVersion {
	version: string;
	vm_name: string;
	vm_version: string;
	vm_vendor: string;
	count: integer;
}
@namespace("cluster.cluster_stats")
class ClusterJvmMemory {
	heap_used: string;
	heap_used_in_bytes: long;
	heap_max: string;
	heap_max_in_bytes: long;
}
@namespace("cluster.cluster_stats")
class ClusterFileSystem {
	total: string;
	total_in_bytes: long;
	free: string;
	free_in_bytes: long;
	available: string;
	available_in_bytes: long;
}
@namespace("common_options.stats")
class PluginStats {
	name: string;
	version: string;
	description: string;
	classname: string;
	jvm: boolean;
	isolated: boolean;
	site: boolean;
}
@namespace("cluster.nodes_info")
class NodeInfo {
	name: string;
	transport_address: string;
	host: string;
	ip: string;
	version: string;
	build_hash: string;
	roles: NodeRole[];
	settings: string[];
	os: NodeOperatingSystemInfo;
	process: NodeProcessInfo;
	jvm: NodeJvmInfo;
	thread_pool: Map<string, NodeThreadPoolInfo>;
	network: NodeInfoNetwork;
	transport: NodeInfoTransport;
	http: NodeInfoHttp;
	plugins: PluginStats[];
}
@namespace("cluster.nodes_info")
class NodeOperatingSystemInfo {
	name: string;
	arch: string;
	version: string;
	refresh_interval_in_millis: integer;
	available_processors: integer;
	cpu: NodeInfoOSCPU;
	mem: NodeInfoMemory;
	swap: NodeInfoMemory;
}
@namespace("cluster.nodes_info")
class NodeInfoOSCPU {
	vendor: string;
	model: string;
	mhz: integer;
	total_cores: integer;
	total_sockets: integer;
	cores_per_socket: integer;
	cache_size: string;
	cache_size_in_bytes: integer;
}
@namespace("cluster.nodes_info")
class NodeInfoMemory {
	total: string;
	total_in_bytes: long;
}
@namespace("cluster.nodes_info")
class NodeProcessInfo {
	refresh_interval_in_millis: long;
	id: long;
	mlockall: boolean;
}
@namespace("cluster.nodes_info")
class NodeJvmInfo {
	pid: integer;
	version: string;
	vm_name: string;
	vm_version: string;
	vm_vendor: string;
	memory_pools: string[];
	gc_collectors: string[];
	start_time_in_millis: long;
	mem: NodeInfoJVMMemory;
}
@namespace("cluster.nodes_info")
class NodeInfoJVMMemory {
	heap_init: string;
	heap_init_in_bytes: long;
	heap_max: string;
	heap_max_in_bytes: long;
	non_heap_init: string;
	non_heap_init_in_bytes: long;
	non_heap_max: string;
	non_heap_max_in_bytes: long;
	direct_max: string;
	direct_max_in_bytes: long;
}
@namespace("cluster.nodes_info")
class NodeThreadPoolInfo {
	type: string;
	min: integer;
	max: integer;
	queue_size: integer;
	keep_alive: string;
}
@namespace("cluster.nodes_info")
class NodeInfoNetwork {
	refresh_interval: integer;
	primary_interface: NodeInfoNetworkInterface;
}
@namespace("cluster.nodes_info")
class NodeInfoNetworkInterface {
	address: string;
	name: string;
	mac_address: string;
}
@namespace("cluster.nodes_info")
class NodeInfoTransport {
	bound_address: string[];
	publish_address: string;
}
@namespace("cluster.nodes_info")
class NodeInfoHttp {
	bound_address: string[];
	publish_address: string;
	max_content_length: string;
	max_content_length_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class NodeStats {
	timestamp: long;
	name: string;
	transport_address: string;
	host: string;
	ip: string | string[];
	roles: NodeRole[];
	indices: IndexStats;
	os: OperatingSystemStats;
	process: ProcessStats;
	script: ScriptStats;
	jvm: NodeJvmStats;
	thread_pool: Map<string, ThreadCountStats>;
	breakers: Map<string, BreakerStats>;
	fs: FileSystemStats;
	transport: TransportStats;
	http: HttpStats;
}
@namespace("cluster.nodes_stats")
class OperatingSystemStats {
	timestamp: long;
	mem: ExtendedMemoryStats;
	swap: MemoryStats;
	cpu: CPUStats;
}
@namespace("cluster.nodes_stats")
class ProcessStats {
	timestamp: long;
	open_file_descriptors: integer;
	cpu: CPUStats;
	mem: MemoryStats;
}
@namespace("cluster.nodes_stats")
class ScriptStats {
	cache_evictions: long;
	compilations: long;
}
@namespace("cluster.nodes_stats")
class NodeJvmStats {
	timestamp: long;
	uptime: string;
	uptime_in_millis: long;
	mem: MemoryStats;
	threads: ThreadStats;
	gc: GarbageCollectionStats;
	buffer_pools: Map<string, NodeBufferPool>;
	classes: JvmClassesStats;
}
@namespace("cluster.nodes_stats")
class ThreadCountStats {
	threads: long;
	queue: long;
	active: long;
	rejected: long;
	largest: long;
	completed: long;
}
@namespace("cluster.nodes_stats")
class BreakerStats {
	estimated_size: string;
	estimated_size_in_bytes: long;
	limit_size: string;
	limit_size_in_bytes: long;
	overhead: float;
	tripped: float;
}
@namespace("cluster.nodes_stats")
class FileSystemStats {
	timestamp: long;
	total: TotalFileSystemStats;
	data: DataPathStats[];
}
@namespace("cluster.nodes_stats")
class TransportStats {
	server_open: integer;
	rx_count: long;
	rx_size: string;
	rx_size_in_bytes: long;
	tx_count: long;
	tx_size: string;
	tx_size_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class HttpStats {
	current_open: integer;
	total_opened: long;
}
@namespace("cluster.nodes_usage")
class NodeUsageInformation {
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	timestamp: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	since: Date;
	rest_actions: Map<string, integer>;
}
@namespace("cluster.remote_info")
class RemoteInfo {
	connected: boolean;
	num_nodes_connected: long;
	max_connections_per_cluster: integer;
	initial_connect_timeout: Time;
	seeds: string[];
	http_addresses: string[];
}
@namespace("indices.alias_management.get_alias")
class IndexAliases {
	aliases: Map<string, AliasDefinition>;
}
@namespace("indices.alias_management")
class AliasDefinition {
	filter: QueryContainer;
	routing: string;
	index_routing: string;
	search_routing: string;
}
@namespace("indices.mapping_management.get_field_mapping")
class TypeFieldMappings {
	@prop_serializer("ResolvableDictionaryJsonConverter`2")
	mappings: Map<TypeName, Map<Field, FieldMapping>>;
}
@namespace("indices.mapping_management.get_mapping")
class IndexMappings {
	mappings: Map<TypeName, TypeMapping>;
	item: TypeMapping;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStatus {
	shards: ShardRecovery[];
}
@namespace("indices.monitoring.indices_recovery")
class ShardRecovery {
	id: long;
	type: string;
	stage: string;
	primary: boolean;
	start_time: Date;
	stop_time: Date;
	total_time_in_millis: long;
	source: RecoveryOrigin;
	target: RecoveryOrigin;
	index: RecoveryIndexStatus;
	translog: RecoveryTranslogStatus;
	start: RecoveryStartStatus;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryOrigin {
	id: string;
	hostname: string;
	ip: string;
	name: string;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryIndexStatus {
	total_time_in_millis: long;
	bytes: RecoveryBytes;
	files: RecoveryFiles;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryBytes {
	total: long;
	reused: long;
	recovered: long;
	percent: string;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryFiles {
	total: long;
	reused: long;
	recovered: long;
	percent: string;
	details: RecoveryFileDetails[];
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryFileDetails {
	name: string;
	length: long;
	recovered: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryTranslogStatus {
	recovered: long;
	total: long;
	percent: string;
	total_on_start: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStartStatus {
	check_index_time: long;
	total_time_in_millis: string;
}
@namespace("x_pack.graph.explore.request")
class GraphVertexInclude {
	term: string;
	boost: double;
}
@namespace("x_pack.graph.explore.request")
class SampleDiversity {
	field: Field;
	max_docs_per_value: integer;
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
class XPackRoleMapping {
	metadata: Map<string, any>;
	enabled: boolean;
	roles: string[];
	rules: RoleMappingRuleBase;
}
@namespace("x_pack.security.role.get_role")
class XPackRole {
	cluster: string[];
	run_as: string[];
	indices: IndicesPrivileges[];
	metadata: Map<string, any>;
}
@namespace("x_pack.security.user.get_user")
class XPackUser {
	username: string;
	roles: string[];
	full_name: string;
	email: string;
	metadata: Map<string, any>;
}
@namespace("analysis.analyzers")
class AnalyzerBase {
	version: string;
	type: string;
}
@namespace("analysis.char_filters")
class CharFilterBase {
	version: string;
	type: string;
}
@namespace("analysis.token_filters")
class TokenFilterBase {
	version: string;
	type: string;
}
@namespace("analysis.tokenizers")
class TokenizerBase {
	version: string;
	type: string;
}
@namespace("modules.indices")
class IndicesModuleSettings {
	qeueries_cache_size: string;
	circuit_breaker_settings: CircuitBreakerSettings;
	fielddata_settings: FielddataSettings;
	recovery_settings: IndicesRecoverySettings;
}
@namespace("analysis")
@class_serializer("UnionJsonConverter")
class StopWords extends Union<string, string[]> {
}
@namespace("indices.index_settings.index_templates.get_index_template")
class TemplateMapping {
	index_patterns: string[];
	order: integer;
	settings: Map<string, any>;
	mappings: Map<TypeName, TypeMapping>;
	aliases: Map<IndexName, Alias>;
	version: integer;
}
@namespace("search.suggesters.context_suggester")
@class_serializer("UnionJsonConverter")
class Context extends Union<string, GeoLocation> {
	category: string;
	geo: GeoLocation;
}
@namespace("x_pack.info.x_pack_usage")
class MonitoringUsage extends XPackUsage {
	enabled_exporters: Map<string, long>;
}
@namespace("x_pack.info.x_pack_usage")
class MachineLearningUsage extends XPackUsage {
	jobs: Map<string, Job>;
	datafeeds: Map<string, DataFeed>;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingUsage extends XPackUsage {
	execution: AlertingExecution;
	count: AlertingCount;
}
@namespace("x_pack.info.x_pack_usage")
class SecurityUsage extends XPackUsage {
	system_key: SecurityFeatureToggle;
	anonymous: SecurityFeatureToggle;
	ssl: SslUsage;
	ipfilter: IpFilterUsage;
	audit: AuditUsage;
	roles: Map<string, RoleUsage>;
	realms: Map<string, RealmUsage>;
}
@namespace("common_options.date_math")
@class_serializer("Json")
class DateMath {
	now: DateMathExpression;
}
@namespace("common_options.minimum_should_match")
@class_serializer("UnionJsonConverter")
class MinimumShouldMatch extends Union<integer, string> {
}
@namespace("query_dsl.multi_term_query_rewrite")
@class_serializer("MultiTermQueryRewriteConverter")
class MultiTermQueryRewrite {
	rewrite: RewriteMultiTerm;
	size: integer;
	constant_score: MultiTermQueryRewrite;
	scoring_boolean: MultiTermQueryRewrite;
	constant_score_boolean: MultiTermQueryRewrite;
}
@namespace("query_dsl.specialized.more_like_this.like")
@class_serializer("UnionJsonConverter")
class Like extends Union<string, LikeDocument> {
}
@namespace("x_pack.watcher.action.email")
class EmailBody {
	text: string;
	html: string;
}
@namespace("x_pack.watcher.action.pager_duty")
class PagerDutyEvent {
	account: string;
	description: string;
	event_type: PagerDutyEventType;
	incident_key: string;
	client: string;
	client_url: string;
	attach_payload: boolean;
	context: PagerDutyContext[];
}
@namespace("x_pack.watcher.schedule")
class ScheduleBase {
}
@namespace("x_pack.watcher.watcher_stats")
class WatchRecordStats extends WatchRecordQueuedStats {
	execution_phase: ExecutionPhase;
}
@namespace("index_modules.index_settings")
class IndexState {
	settings: Map<string, any>;
	mappings: Map<TypeName, TypeMapping>;
	aliases: Map<IndexName, Alias>;
}
@namespace("common_options.time_unit")
@class_serializer("TimeJsonConverter")
class Time {
	factor: double;
	interval: TimeUnit;
	milliseconds: double;
	minus_one: Time;
	zero: Time;
}
@namespace("analysis.token_filters")
class KeepTypesTokenFilter extends TokenFilterBase {
	types: string[];
}
@namespace("common")
class Request {
}
@namespace("common_abstractions.infer.property_name")
class PropertyName {
	name: string;
	cacheable_expression: boolean;
}
@namespace("common_abstractions.infer.index_name")
class IndexName {
	cluster: string;
	name: string;
}
@namespace("common_abstractions.infer.type_name")
class TypeName {
	name: string;
}
@namespace("common_abstractions.infer.join_field_routing")
@class_serializer("RoutingJsonConverter")
class Routing {
}
@namespace("common_abstractions.infer.task_id")
class TaskId {
	node_id: string;
	task_number: long;
	fully_qualified_id: string;
}
@namespace("common_options.failures")
class BulkError extends Error {
	index: string;
	shard: integer;
}
@namespace("document.multiple")
class BulkIndexFailureCause extends Error {
	index_unique_id: string;
	shard: integer;
	index: string;
}
@namespace("common_abstractions.infer.field")
class Field {
	name: string;
	boost: double;
	cachable_expression: boolean;
}
@namespace("common_abstractions.infer.indices")
@class_serializer("UnionJsonConverter")
class Indices extends Union<AllIndicesMarker, ManyIndices> {
	all: Indices;
	all_indices: Indices;
}
@namespace("common_abstractions.infer.id")
@class_serializer("IdJsonConverter")
class Id {
}
@namespace("query_dsl.geo")
class GeoLocation {
	lat: double;
	lon: double;
}
@namespace("common_options.date_math")
@class_serializer("Json")
class DateMathExpression extends DateMath {
}
@namespace("common_abstractions.infer.types")
@class_serializer("UnionJsonConverter")
class Types extends Union<AllTypesMarker, ManyTypes> {
	all: AllTypesMarker;
	all_types: AllTypesMarker;
}
@namespace("common_abstractions.infer.relation_name")
class RelationName {
	name: string;
}
@namespace("x_pack.watcher.execution")
class HttpInputRequestResult extends HttpInputRequest {
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class SmoothingModelContainer {
}
@namespace("analysis.analyzers")
class CustomAnalyzer extends AnalyzerBase {
	tokenizer: string;
	filter: string[];
	char_filter: string[];
	position_offset_gap: integer;
}
@namespace("analysis.analyzers")
class FingerprintAnalyzer extends AnalyzerBase {
	separator: string;
	max_output_size: integer;
	preserve_original: boolean;
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
	stopwords_path: string;
}
@namespace("analysis.analyzers")
class KeywordAnalyzer extends AnalyzerBase {
}
@namespace("analysis.analyzers")
class LanguageAnalyzer extends AnalyzerBase {
	type: string;
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
	stem_exclusion: string[];
	language: Language;
	stopwords_path: string;
}
@namespace("analysis.analyzers")
class PatternAnalyzer extends AnalyzerBase {
	lowercase: boolean;
	pattern: string;
	flags: string;
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
}
@namespace("analysis.analyzers")
class SimpleAnalyzer extends AnalyzerBase {
}
@namespace("analysis.analyzers")
class SnowballAnalyzer extends AnalyzerBase {
	language: SnowballLanguage;
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
}
@namespace("analysis.analyzers")
class StandardAnalyzer extends AnalyzerBase {
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
	max_token_length: integer;
}
@namespace("analysis.analyzers")
class StopAnalyzer extends AnalyzerBase {
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
	stopwords_path: string;
}
@namespace("analysis.analyzers")
class WhitespaceAnalyzer extends AnalyzerBase {
}
@namespace("analysis.char_filters")
class HtmlStripCharFilter extends CharFilterBase {
}
@namespace("analysis.char_filters")
class MappingCharFilter extends CharFilterBase {
	mappings: string[];
	mappings_path: string;
}
@namespace("analysis.char_filters")
class PatternReplaceCharFilter extends CharFilterBase {
	pattern: string;
	replacement: string;
}
@namespace("analysis.plugins.icu")
class IcuCollationTokenFilter extends TokenFilterBase {
	language: string;
	country: string;
	variant: string;
	strength: IcuCollationStrength;
	decomposition: IcuCollationDecomposition;
	alternate: IcuCollationAlternate;
	caseLevel: boolean;
	caseFirst: IcuCollationCaseFirst;
	numeric: boolean;
	variableTop: string;
	hiraganaQuaternaryMode: boolean;
}
@namespace("analysis.plugins.icu")
class IcuFoldingTokenFilter extends TokenFilterBase {
	unicodeSetFilter: string;
}
@namespace("analysis.plugins.icu")
class IcuNormalizationCharFilter extends CharFilterBase {
	name: IcuNormalizationType;
	mode: IcuNormalizationMode;
}
@namespace("analysis.plugins.icu")
class IcuNormalizationTokenFilter extends TokenFilterBase {
	name: IcuNormalizationType;
}
@namespace("analysis.plugins.icu")
class IcuTokenizer extends TokenizerBase {
	rule_files: string;
}
@namespace("analysis.plugins.icu")
class IcuTransformTokenFilter extends TokenFilterBase {
	dir: IcuTransformDirection;
	id: string;
}
@namespace("analysis.plugins.kuromoji")
class KuromojiAnalyzer extends AnalyzerBase {
	mode: KuromojiTokenizationMode;
	user_dictionary: string;
}
@namespace("analysis.plugins.kuromoji")
class KuromojiIterationMarkCharFilter extends CharFilterBase {
	normalize_kanji: boolean;
	normalize_kana: boolean;
}
@namespace("analysis.plugins.kuromoji")
class KuromojiPartOfSpeechTokenFilter extends TokenFilterBase {
	stoptags: string[];
}
@namespace("analysis.plugins.kuromoji")
class KuromojiReadingFormTokenFilter extends TokenFilterBase {
	use_romaji: boolean;
}
@namespace("analysis.plugins.kuromoji")
class KuromojiStemmerTokenFilter extends TokenFilterBase {
	minimum_length: integer;
}
@namespace("analysis.plugins.kuromoji")
class KuromojiTokenizer extends TokenizerBase {
	mode: KuromojiTokenizationMode;
	discard_punctuation: boolean;
	user_dictionary: string;
	nbest_examples: string;
	nbest_cost: integer;
}
@namespace("analysis.plugins.phonetic")
class PhoneticTokenFilter extends TokenFilterBase {
	encoder: PhoneticEncoder;
	replace: boolean;
}
@namespace("analysis.token_filters")
class AsciiFoldingTokenFilter extends TokenFilterBase {
	preserve_original: boolean;
}
@namespace("analysis.token_filters")
class CommonGramsTokenFilter extends TokenFilterBase {
	common_words: string | string[];
	common_words_path: string;
	ignore_case: boolean;
	query_mode: boolean;
}
@namespace("analysis.token_filters.compound_word")
class CompoundWordTokenFilterBase extends TokenFilterBase {
	word_list: string[];
	word_list_path: string;
	min_word_size: integer;
	min_subword_size: integer;
	max_subword_size: integer;
	only_longest_match: boolean;
	hyphenation_patterns_path: string;
}
@namespace("analysis.token_filters.delimited_payload")
class DelimitedPayloadTokenFilter extends TokenFilterBase {
	delimiter: string;
	encoding: DelimitedPayloadEncoding;
}
@namespace("analysis.token_filters.edge_n_gram")
class EdgeNGramTokenFilter extends TokenFilterBase {
	min_gram: integer;
	max_gram: integer;
	side: EdgeNGramSide;
}
@namespace("analysis.token_filters")
class ElisionTokenFilter extends TokenFilterBase {
	articles: string[];
}
@namespace("analysis.token_filters")
class FingerprintTokenFilter extends TokenFilterBase {
	separator: string;
	max_output_size: integer;
}
@namespace("analysis.token_filters")
class HunspellTokenFilter extends TokenFilterBase {
	locale: string;
	dictionary: string;
	dedup: boolean;
	longest_only: boolean;
}
@namespace("analysis.token_filters")
class KeepWordsTokenFilter extends TokenFilterBase {
	keep_words: string[];
	keep_words_path: string;
	keep_words_case: boolean;
}
@namespace("analysis.token_filters")
class KeywordMarkerTokenFilter extends TokenFilterBase {
	keywords: string[];
	keywords_path: string;
	ignore_case: boolean;
}
@namespace("analysis.token_filters")
class KStemTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class LengthTokenFilter extends TokenFilterBase {
	min: integer;
	max: integer;
}
@namespace("analysis.token_filters")
class LimitTokenCountTokenFilter extends TokenFilterBase {
	max_token_count: integer;
	consume_all_tokens: boolean;
}
@namespace("analysis.token_filters")
class LowercaseTokenFilter extends TokenFilterBase {
	language: string;
}
@namespace("analysis.token_filters")
class NGramTokenFilter extends TokenFilterBase {
	min_gram: integer;
	max_gram: integer;
}
@namespace("analysis.token_filters")
class PatternCaptureTokenFilter extends TokenFilterBase {
	patterns: string[];
	preserve_original: boolean;
}
@namespace("analysis.token_filters")
class PatternReplaceTokenFilter extends TokenFilterBase {
	pattern: string;
	replacement: string;
}
@namespace("analysis.token_filters")
class PorterStemTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class ReverseTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters.shingle")
class ShingleTokenFilter extends TokenFilterBase {
	min_shingle_size: integer;
	max_shingle_size: integer;
	output_unigrams: boolean;
	output_unigrams_if_no_shingles: boolean;
	token_separator: string;
	filler_token: string;
}
@namespace("analysis.token_filters")
class SnowballTokenFilter extends TokenFilterBase {
	language: SnowballLanguage;
}
@namespace("analysis.token_filters")
class StandardTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class StemmerOverrideTokenFilter extends TokenFilterBase {
	rules: string[];
	rules_path: string;
}
@namespace("analysis.token_filters")
class StemmerTokenFilter extends TokenFilterBase {
	language: string;
}
@namespace("analysis.token_filters.stop")
class StopTokenFilter extends TokenFilterBase {
	@prop_serializer("StopWordsJsonConverter")
	stopwords: StopWords;
	ignore_case: boolean;
	stopwords_path: string;
	remove_trailing: boolean;
}
@namespace("analysis.token_filters.synonym")
class SynonymGraphTokenFilter extends TokenFilterBase {
	synonyms_path: string;
	format: SynonymFormat;
	synonyms: string[];
	ignore_case: boolean;
	expand: boolean;
	tokenizer: string;
}
@namespace("analysis.token_filters.synonym")
class SynonymTokenFilter extends TokenFilterBase {
	synonyms_path: string;
	format: SynonymFormat;
	synonyms: string[];
	ignore_case: boolean;
	expand: boolean;
	tokenizer: string;
}
@namespace("analysis.token_filters")
class TrimTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class TruncateTokenFilter extends TokenFilterBase {
	length: integer;
}
@namespace("analysis.token_filters")
class UniqueTokenFilter extends TokenFilterBase {
	only_on_same_position: boolean;
}
@namespace("analysis.token_filters")
class UppercaseTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters.word_delimiter_graph")
class WordDelimiterGraphTokenFilter extends TokenFilterBase {
	generate_word_parts: boolean;
	generate_number_parts: boolean;
	catenate_words: boolean;
	catenate_numbers: boolean;
	catenate_all: boolean;
	split_on_case_change: boolean;
	preserve_original: boolean;
	split_on_numerics: boolean;
	stem_english_possessive: boolean;
	protected_words: string[];
	protected_words_path : string;
	type_table: string[];
	type_table_path: string;
}
@namespace("analysis.token_filters.word_delimiter")
class WordDelimiterTokenFilter extends TokenFilterBase {
	generate_word_parts: boolean;
	generate_number_parts: boolean;
	catenate_words: boolean;
	catenate_numbers: boolean;
	catenate_all: boolean;
	split_on_case_change: boolean;
	preserve_original: boolean;
	split_on_numerics: boolean;
	stem_english_possessive: boolean;
	protected_words: string[];
	protected_words_path : string;
	type_table: string[];
	type_table_path: string;
}
@namespace("analysis.tokenizers")
class KeywordTokenizer extends TokenizerBase {
	buffer_size: integer;
}
@namespace("analysis.tokenizers")
class LetterTokenizer extends TokenizerBase {
}
@namespace("analysis.tokenizers")
class LowercaseTokenizer extends TokenizerBase {
}
@namespace("analysis.tokenizers.n_gram")
class EdgeNGramTokenizer extends TokenizerBase {
	min_gram: integer;
	max_gram: integer;
	token_chars: TokenChar[];
}
@namespace("analysis.tokenizers.n_gram")
class NGramTokenizer extends TokenizerBase {
	min_gram: integer;
	max_gram: integer;
	token_chars: TokenChar[];
}
@namespace("analysis.tokenizers")
class PathHierarchyTokenizer extends TokenizerBase {
	delimiter: string;
	replacement: string;
	buffer_size: integer;
	reverse: boolean;
	skip: integer;
}
@namespace("analysis.tokenizers")
class PatternTokenizer extends TokenizerBase {
	pattern: string;
	flags: string;
	group: integer;
}
@namespace("analysis.tokenizers")
class StandardTokenizer extends TokenizerBase {
	max_token_length: integer;
}
@namespace("analysis.tokenizers")
class UaxEmailUrlTokenizer extends TokenizerBase {
	max_token_length: integer;
}
@namespace("analysis.tokenizers")
class WhitespaceTokenizer extends TokenizerBase {
}
@namespace("common_abstractions.request")
class PlainRequestBase<TParameters> extends Request {
	request_configuration: RequestConfiguration;
	pretty: boolean;
	human: boolean;
	error_trace: boolean;
	filter_path: string[];
}
@namespace("mapping.types")
class PropertyBase {
	name: PropertyName;
	local_metadata: Map<string, any>;
}
@namespace("x_pack.watcher.schedule")
@class_serializer("CronExpressionJsonConverter")
class CronExpression extends ScheduleBase {
}
@namespace("cat")
class CatResponse<TCatRecord> extends Response {
	records: TCatRecord[];
}
@namespace("cluster.cluster_allocation_explain")
class ClusterAllocationExplainResponse extends Response {
	index: string;
	shard: integer;
	primary: boolean;
	current_state: string;
	unassigned_info: UnassignedInformation;
	can_allocate: Decision;
	allocate_explanation: string;
	configured_delay: string;
	configured_delay_in_mills: long;
	current_node: CurrentNode;
	can_remain_on_current_node: Decision;
	can_remain_decisions: AllocationDecision[];
	can_rebalance_cluster: Decision;
	can_rebalance_to_other_node: Decision;
	can_rebalance_cluster_decisions: AllocationDecision[];
	rebalance_explanation: string;
	node_allocation_decisions: NodeAllocationExplanation[];
	can_move_to_other_node: Decision;
	move_explanation: string;
	allocation_delay: string;
	allocation_delay_in_millis: long;
	remaining_delay: string;
	remaining_delay_in_millis: long;
}
@namespace("cluster.cluster_health")
class ClusterHealthResponse extends Response {
	cluster_name: string;
	status: Health;
	timed_out: boolean;
	number_of_nodes: integer;
	number_of_data_nodes: integer;
	active_primary_shards: integer;
	active_shards: integer;
	relocating_shards: integer;
	initializing_shards: integer;
	unassigned_shards: integer;
	number_of_pending_tasks: integer;
	@prop_serializer("ResolvableDictionaryJsonConverter`2")
	indices: Map<IndexName, IndexHealthStats>;
}
@namespace("cluster.cluster_pending_tasks")
class ClusterPendingTasksResponse extends Response {
	tasks: PendingTask[];
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteResponse extends Response {
	state: ClusterRerouteState;
	explanations: ClusterRerouteExplanation[];
}
@namespace("cluster.cluster_settings.cluster_get_settings")
class ClusterGetSettingsResponse extends Response {
	persistent: Map<string, any>;
	transient: Map<string, any>;
}
@namespace("cluster.cluster_settings.cluster_put_settings")
class ClusterPutSettingsResponse extends Response {
	acknowledged: boolean;
	persistent: Map<string, any>;
	transient: Map<string, any>;
}
@namespace("cluster.cluster_state")
class ClusterStateResponse extends Response {
	cluster_name: string;
	master_node: string;
	state_uuid: string;
	version: long;
	nodes: Map<string, NodeState>;
	metadata: MetadataState;
	routing_table: RoutingTableState;
	routing_nodes: RoutingNodesState;
	blocks: BlockState;
}
@namespace("cluster.nodes_hot_threads")
class NodesHotThreadsResponse extends Response {
	hot_threads: HotThreadInformation[];
}
@namespace("cluster")
class NodesResponseBase extends Response {
	node_statistics: NodeStatistics;
}
@namespace("cluster.ping")
class PingResponse extends Response {
}
@namespace("cluster.root_node_info")
class RootNodeInfoResponse extends Response {
	name: string;
	tagline: string;
	version: ElasticsearchVersionInfo;
}
@namespace("cluster.task_management.cancel_tasks")
class CancelTasksResponse extends Response {
	is_valid: boolean;
	nodes: Map<string, TaskExecutingNode>;
	node_failures: ErrorCause[];
}
@namespace("cluster.task_management.get_task")
class GetTaskResponse extends Response {
	completed: boolean;
	task: TaskInfo;
}
@namespace("cluster.task_management.list_tasks")
class ListTasksResponse extends Response {
	is_valid: boolean;
	nodes: Map<string, TaskExecutingNode>;
	node_failures: ErrorCause[];
}
@namespace("common_abstractions.response")
class AcknowledgedResponseBase extends Response {
	acknowledged: boolean;
}
@namespace("common_abstractions.response")
class DictionaryResponseBase<TKey, TValue> extends Response {
}
@namespace("common_abstractions.response")
class IndicesResponseBase extends Response {
	acknowledged: boolean;
	_shards: ShardStatistics;
}
@namespace("common_abstractions.response")
class ShardsOperationResponseBase extends Response {
	_shards: ShardStatistics;
}
@namespace("document.multiple.bulk")
class BulkResponse extends Response {
	is_valid: boolean;
	took: long;
	errors: boolean;
	items: BulkResponseItem[];
	items_with_errors: BulkResponseItem[];
}
@namespace("document.multiple.delete_by_query")
class DeleteByQueryResponse extends Response {
	is_valid: boolean;
	took: long;
	task: TaskId;
	timed_out: boolean;
	slice_id: integer;
	deleted: long;
	batches: long;
	version_conflicts: long;
	noops: long;
	retries: Retries;
	throttled_millis: long;
	requests_per_second: float;
	throttled_until_millis: long;
	total: long;
	failures: BulkIndexByScrollFailure[];
}
@namespace("document.multiple.multi_get.response")
class MultiGetResponse extends Response {
	is_valid: boolean;
	hits: MultiGetHit<any>[];
}
@namespace("document.multiple.multi_term_vectors")
class MultiTermVectorsResponse extends Response {
	@prop_serializer("ReadOnlyCollectionJsonConverter`2")
	docs: TermVectors[];
}
@namespace("document.multiple.reindex_on_server")
class ReindexOnServerResponse extends Response {
	is_valid: boolean;
	took: Time;
	task: TaskId;
	timed_out: boolean;
	total: long;
	created: long;
	updated: long;
	batches: long;
	version_conflicts: long;
	noops: long;
	retries: Retries;
	failures: BulkIndexByScrollFailure[];
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexRethrottleResponse extends Response {
	nodes: Map<string, ReindexNode>;
}
@namespace("document.multiple.update_by_query")
class UpdateByQueryResponse extends Response {
	is_valid: boolean;
	took: long;
	task: TaskId;
	timed_out: boolean;
	total: long;
	updated: long;
	batches: long;
	version_conflicts: long;
	noops: long;
	retries: Retries;
	failures: BulkIndexByScrollFailure[];
	requests_per_second: float;
}
@namespace("document.single.create")
class CreateResponse extends Response {
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	result: Result;
	_shards: ShardStatistics;
	_seq_no: long;
	_primary_term: long;
}
@namespace("document.single.delete")
class DeleteResponse extends Response {
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	result: Result;
	_shards: ShardStatistics;
	_seq_no: long;
	_primary_term: long;
}
@namespace("document.single.get")
class GetResponse<TDocument> extends Response {
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	found: boolean;
	@prop_serializer("SourceConverter")
	_source: TDocument;
	fields: Map<string, LazyDocument>;
	_parent: string;
	_routing: string;
}
@namespace("document.single.index")
class IndexResponse extends Response {
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	result: Result;
	_shards: ShardStatistics;
	_seq_no: long;
	_primary_term: long;
}
@namespace("document.single.source")
class SourceResponse<T> extends Response {
	body: T;
}
@namespace("document.single.update")
class UpdateResponse<TDocument> extends Response {
	_shards: ShardStatistics;
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	get: InstantGet<TDocument>;
	result: Result;
}
@namespace("indices.alias_management.delete_alias")
class DeleteAliasResponse extends Response {
}
@namespace("indices.alias_management.put_alias")
class PutAliasResponse extends Response {
}
@namespace("indices.analyze")
class AnalyzeResponse extends Response {
	tokens: AnalyzeToken[];
	detail: AnalyzeDetail;
}
@namespace("indices.index_management.indices_exists")
class ExistsResponse extends Response {
	exists: boolean;
}
@namespace("indices.monitoring.indices_segments")
class SegmentsResponse extends Response {
	_shards: ShardStatistics;
	indices: Map<string, IndexSegment>;
}
@namespace("indices.monitoring.indices_shard_stores")
class IndicesShardStoresResponse extends Response {
	indices: Map<string, IndicesShardStores>;
}
@namespace("indices.monitoring.indices_stats")
class IndicesStatsResponse extends Response {
	_shards: ShardStatistics;
	_all: IndicesStats;
	indices: Map<string, IndicesStats>;
}
@namespace("indices.status_management.upgrade")
class UpgradeResponse extends Response {
	_shards: ShardStatistics;
}
@namespace("indices.status_management.upgrade.upgrade_status")
@class_serializer("UpgradeStatusResponseJsonConverter")
class UpgradeStatusResponse extends Response {
	upgrades: Map<string, UpgradeStatus>;
	size_in_bytes: long;
	size_to_upgrade_in_bytes: string;
	size_to_upgrade_ancient_in_bytes: string;
}
@namespace("ingest.processor")
class GrokProcessorPatternsResponse extends Response {
	patterns: Map<string, string>;
}
@namespace("ingest.simulate_pipeline")
class SimulatePipelineResponse extends Response {
	docs: PipelineSimulation[];
}
@namespace("modules.scripting.get_script")
class GetScriptResponse extends Response {
	script: StoredScript;
}
@namespace("modules.snapshot_and_restore.repositories.get_repository")
@class_serializer("GetRepositoryResponseJsonConverter")
class GetRepositoryResponse extends Response {
	repositories: Map<string, SnapshotRepository>;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
class VerifyRepositoryResponse extends Response {
	nodes: Map<string, CompactNodeInfo>;
}
@namespace("modules.snapshot_and_restore.restore")
class RestoreResponse extends Response {
	snapshot: SnapshotRestore;
}
@namespace("modules.snapshot_and_restore.snapshot.get_sapshot")
class GetSnapshotResponse extends Response {
	snapshots: Snapshot[];
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStatusResponse extends Response {
	snapshots: SnapshotStatus[];
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot")
class SnapshotResponse extends Response {
	accepted: boolean;
	snapshot: Snapshot;
}
@namespace("search.count")
class CountResponse extends Response {
	count: long;
	_shards: ShardStatistics;
}
@namespace("search.explain")
class ExplainResponse<TDocument> extends Response {
	matched: boolean;
	explanation: ExplanationDetail;
	get: InstantGet<TDocument>;
}
@namespace("search.field_capabilities")
class FieldCapabilitiesResponse extends Response {
	shards: ShardStatistics;
	fields: Map<Field, Map<string, FieldCapabilities>>;
}
@namespace("search.multi_search")
class MultiSearchResponse extends Response {
	is_valid: boolean;
	total_responses: integer;
	all_responses: Response[];
}
@namespace("search.scroll.clear_scroll")
class ClearScrollResponse extends Response {
}
@namespace("search.search_shards")
class SearchShardsResponse extends Response {
	shards: SearchShard[][];
	nodes: Map<string, SearchNode>;
}
@namespace("search.search_template.render_search_template")
class RenderSearchTemplateResponse extends Response {
	template_output: LazyDocument;
}
@namespace("search.search")
class SearchResponse<T> extends Response {
	_shards: ShardStatistics;
	aggregations: Map<string, Aggregate>;
	aggs: Map<string, Aggregate>;
	profile: Profile;
	suggest: Map<string, Suggest<T>[]>;
	took: long;
	timed_out: boolean;
	terminated_early: boolean;
	_scroll_id: string;
	hits: HitsMetadata<T>;
	num_reduce_phases: long;
	total: long;
	max_score: double;
	documents: T[];
	//hits: Hit<T>[];
	fields: Map<string, LazyDocument>;
}
@namespace("search.validate")
class ValidateQueryResponse extends Response {
	valid: boolean;
	_shards: ShardStatistics;
	explanations: ValidationExplanation[];
}
@namespace("x_pack.migration.deprecation_info")
class DeprecationInfoResponse extends Response {
	cluster_settings: DeprecationInfo[];
	node_settings: DeprecationInfo[];
	index_settings: Map<string, DeprecationInfo[]>;
}
@namespace("x_pack.graph.explore")
class GraphExploreResponse extends Response {
	took: long;
	timed_out: boolean;
	connections: GraphConnection[];
	vertices: GraphVertex[];
	failures: ShardFailure[];
}
@namespace("x_pack.info.x_pack_info")
class XPackInfoResponse extends Response {
	build: XPackBuildInformation;
	license: MinimalLicenseInformation;
	features: XPackFeatures;
	tagline: string;
}
@namespace("x_pack.info.x_pack_usage")
class XPackUsageResponse extends Response {
	graph: XPackUsage;
	monitoring: MonitoringUsage;
	ml: MachineLearningUsage;
	watcher: AlertingUsage;
	security: SecurityUsage;
}
@namespace("x_pack.license.delete_license")
class DeleteLicenseResponse extends Response {
}
@namespace("x_pack.license.get_license")
class GetLicenseResponse extends Response {
	is_valid: boolean;
	license: LicenseInformation;
}
@namespace("x_pack.license.post_license")
class PostLicenseResponse extends Response {
	acknowledged: boolean;
	license_status: LicenseStatus;
	acknowledge: LicenseAcknowledgement;
}
@namespace("x_pack.machine_learning.close_job")
class CloseJobResponse extends Response {
	closed: boolean;
}
@namespace("x_pack.machine_learning.delete_expired_data")
class DeleteExpiredDataResponse extends Response {
	deleted: boolean;
}
@namespace("x_pack.machine_learning.flush_job")
class FlushJobResponse extends Response {
	flushed: boolean;
}
@namespace("x_pack.machine_learning.get_anomaly_records")
class GetAnomalyRecordsResponse extends Response {
	count: long;
	records: AnomalyRecord[];
}
@namespace("x_pack.machine_learning.get_buckets")
class GetBucketsResponse extends Response {
	count: long;
	buckets: Bucket[];
}
@namespace("x_pack.machine_learning.get_categories")
class GetCategoriesResponse extends Response {
	count: long;
	categories: CategoryDefinition[];
}
@namespace("x_pack.machine_learning.get_datafeed_stats")
class GetDatafeedStatsResponse extends Response {
	count: long;
	datafeeds: DatafeedStats[];
}
@namespace("x_pack.machine_learning.get_datafeeds")
class GetDatafeedsResponse extends Response {
	count: long;
	datafeeds: DatafeedConfig[];
}
@namespace("x_pack.machine_learning.get_influencers")
class GetInfluencersResponse extends Response {
	count: long;
	influencers: BucketInfluencer[];
}
@namespace("x_pack.machine_learning.get_job_stats")
class GetJobStatsResponse extends Response {
	count: long;
	jobs: JobStats[];
}
@namespace("x_pack.machine_learning.get_jobs")
class GetJobsResponse extends Response {
	count: long;
	jobs: Job[];
}
@namespace("x_pack.machine_learning.get_model_snapshots")
class GetModelSnapshotsResponse extends Response {
	count: long;
	model_snapshots: ModelSnapshot[];
}
@namespace("x_pack.machine_learning.open_job")
class OpenJobResponse extends Response {
	opened: boolean;
}
@namespace("x_pack.machine_learning.post_job_data")
class PostJobDataResponse extends Response {
	job_id: string;
	processed_record_count: long;
	processed_field_count: long;
	input_bytes: long;
	input_field_count: long;
	invalid_date_count: long;
	missing_field_count: long;
	out_of_order_timestamp_count: long;
	empty_bucket_count: long;
	sparse_bucket_count: long;
	bucket_count: long;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	last_data_time: Date;
	input_record_count: long;
}
@namespace("x_pack.machine_learning.preview_datafeed")
class PreviewDatafeedResponse<T> extends Response {
	data: T[];
}
@namespace("x_pack.machine_learning.put_datafeed")
class PutDatafeedResponse extends Response {
	datafeed_id: string;
	aggregations: Map<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	@prop_serializer("IndicesJsonConverter")
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Map<string, ScriptField>;
	scroll_size: integer;
	@prop_serializer("TypesJsonConverter")
	types: Types;
}
@namespace("x_pack.machine_learning.put_job")
class PutJobResponse extends Response {
	job_id: string;
	job_type: string;
	description: string;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	create_time: Date;
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	background_persist_interval: Time;
	data_description: DataDescription;
	model_snapshot_retention_days: long;
	model_snapshot_id: string;
	results_index_name: string;
	model_plot: ModelPlotConfig;
	renormalization_window_days: long;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.start_datafeed")
class StartDatafeedResponse extends Response {
	started: boolean;
}
@namespace("x_pack.machine_learning.stop_datafeed")
class StopDatafeedResponse extends Response {
	stopped: boolean;
}
@namespace("x_pack.machine_learning.update_data_feed")
class UpdateDatafeedResponse extends Response {
	datafeed_id: string;
	aggregations: Map<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	@prop_serializer("IndicesJsonConverter")
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Map<string, ScriptField>;
	scroll_size: integer;
	@prop_serializer("TypesJsonConverter")
	types: Types;
}
@namespace("x_pack.machine_learning.update_job")
class UpdateJobResponse extends Response {
}
@namespace("x_pack.security.authenticate")
class AuthenticateResponse extends Response {
	username: string;
	roles: string[];
	full_name: string;
	email: string;
	metadata: Map<string, any>;
}
@namespace("x_pack.security.clear_cached_realms")
class ClearCachedRealmsResponse extends Response {
	cluster_name: string;
	nodes: Map<string, SecurityNode>;
}
@namespace("x_pack.security.role_mapping.delete_role_mapping")
class DeleteRoleMappingResponse extends Response {
	found: boolean;
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
class PutRoleMappingResponse extends Response {
	role_mapping: PutRoleMappingStatus;
	created: boolean;
}
@namespace("x_pack.security.role.clear_cached_roles")
class ClearCachedRolesResponse extends Response {
	cluster_name: string;
	nodes: Map<string, SecurityNode>;
}
@namespace("x_pack.security.role.delete_role")
class DeleteRoleResponse extends Response {
	found: boolean;
}
@namespace("x_pack.security.role.put_role")
class PutRoleResponse extends Response {
	role: PutRoleStatus;
}
@namespace("x_pack.security.user.change_password")
class ChangePasswordResponse extends Response {
}
@namespace("x_pack.security.user.delete_user")
class DeleteUserResponse extends Response {
	found: boolean;
}
@namespace("x_pack.security.user.disable_user")
class DisableUserResponse extends Response {
}
@namespace("x_pack.security.user.enable_user")
class EnableUserResponse extends Response {
}
@namespace("x_pack.security.user.get_user_access_token")
class GetUserAccessTokenResponse extends Response {
	access_token: string;
	type: string;
	expires_in: long;
	scope: string;
}
@namespace("x_pack.security.user.invalidate_user_access_token")
class InvalidateUserAccessTokenResponse extends Response {
	created: boolean;
}
@namespace("x_pack.security.user.put_user")
class PutUserResponse extends Response {
	user: PutUserStatus;
}
@namespace("x_pack.watcher.acknowledge_watch")
class AcknowledgeWatchResponse extends Response {
	status: WatchStatus;
}
@namespace("x_pack.watcher.activate_watch")
class ActivateWatchResponse extends Response {
	status: ActivationStatus;
}
@namespace("x_pack.watcher.deactivate_watch")
class DeactivateWatchResponse extends Response {
	status: ActivationStatus;
}
@namespace("x_pack.watcher.delete_watch")
class DeleteWatchResponse extends Response {
	_id: string;
	_version: integer;
	found: boolean;
}
@namespace("x_pack.watcher.execute_watch")
class ExecuteWatchResponse extends Response {
	_id: string;
	watch_record: WatchRecord;
}
@namespace("x_pack.watcher.get_watch")
class GetWatchResponse extends Response {
	found: boolean;
	_id: string;
	status: WatchStatus;
	watch: Watch;
}
@namespace("x_pack.watcher.schedule")
@class_serializer("IntervalJsonConverter")
class Interval extends ScheduleBase {
	factor: long;
	unit: IntervalUnit;
}
@namespace("x_pack.watcher.put_watch")
class PutWatchResponse extends Response {
	_id: string;
	_version: integer;
	created: boolean;
}
@namespace("x_pack.watcher.watcher_stats")
class WatcherStatsResponse extends Response {
	stats: WatcherNodeStats[];
	manually_stopped: boolean;
	cluster_name: string;
}
@namespace("analysis.token_filters.compound_word")
class DictionaryDecompounderTokenFilter extends CompoundWordTokenFilterBase {
}
@namespace("analysis.token_filters.compound_word")
class HyphenationDecompounderTokenFilter extends CompoundWordTokenFilterBase {
}
@namespace("cat.cat_aliases")
class CatAliasesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_allocation")
class CatAllocationRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_count")
class CatCountRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_fielddata")
class CatFielddataRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_health")
class CatHealthRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	include_timestamp: boolean;
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_help")
class CatHelpRequest extends Request {
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
}
@namespace("cat.cat_indices")
class CatIndicesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	health: Health;
	@request_parameter()
	help: boolean;
	@request_parameter()
	pri: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_master")
class CatMasterRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_node_attributes")
class CatNodeAttributesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_nodes")
class CatNodesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	full_id: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_pending_tasks")
class CatPendingTasksRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_plugins")
class CatPluginsRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_recovery")
class CatRecoveryRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_repositories")
class CatRepositoriesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_segments")
class CatSegmentsRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_shards")
class CatShardsRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_snapshots")
class CatSnapshotsRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_tasks")
class CatTasksRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	node_id: string[];
	@request_parameter()
	actions: string[];
	@request_parameter()
	detailed: boolean;
	@request_parameter()
	parent_node: string;
	@request_parameter()
	parent_task: long;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_templates")
class CatTemplatesRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_thread_pool")
class CatThreadPoolRequest extends Request {
	@request_parameter()
	format: string;
	@request_parameter()
	size: Size;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cluster.cluster_allocation_explain")
class ClusterAllocationExplainRequest extends Request {
	index: IndexName;
	shard: integer;
	primary: boolean;
	@request_parameter()
	include_yes_decisions: boolean;
	@request_parameter()
	include_disk_info: boolean;
}
@namespace("cluster.cluster_health")
class ClusterHealthRequest extends Request {
	@request_parameter()
	level: Level;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	wait_for_nodes: string;
	@request_parameter()
	wait_for_events: WaitForEvents;
	@request_parameter()
	wait_for_no_relocating_shards: boolean;
	@request_parameter()
	wait_for_status: WaitForStatus;
}
@namespace("cluster.cluster_pending_tasks")
class ClusterPendingTasksRequest extends Request {
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteRequest extends Request {
	commands: ClusterRerouteCommand[];
	@request_parameter()
	dry_run: boolean;
	@request_parameter()
	explain: boolean;
	@request_parameter()
	retry_failed: boolean;
	@request_parameter()
	metric: string[];
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.cluster_settings.cluster_get_settings")
class ClusterGetSettingsRequest extends Request {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	include_defaults: boolean;
}
@namespace("cluster.cluster_settings.cluster_put_settings")
class ClusterPutSettingsRequest extends Request {
	persistent: Map<string, any>;
	transient: Map<string, any>;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.cluster_state")
class ClusterStateRequest extends Request {
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("cluster.cluster_stats")
class ClusterStatsRequest extends Request {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_hot_threads")
class NodesHotThreadsRequest extends Request {
	@request_parameter()
	interval: Time;
	@request_parameter()
	snapshots: long;
	@request_parameter()
	threads: long;
	@request_parameter()
	ignore_idle_threads: boolean;
	@request_parameter()
	thread_type: ThreadType;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_info")
class NodesInfoRequest extends Request {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_stats")
class NodesStatsRequest extends Request {
	@request_parameter()
	completion_fields: Field[];
	@request_parameter()
	fielddata_fields: Field[];
	@request_parameter()
	fields: Field[];
	@request_parameter()
	groups: boolean;
	@request_parameter()
	level: Level;
	@request_parameter()
	types: string[];
	@request_parameter()
	timeout: Time;
	@request_parameter()
	include_segment_file_sizes: boolean;
}
@namespace("cluster.nodes_usage")
class NodesUsageRequest extends Request {
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.ping")
class PingRequest extends Request {
}
@namespace("cluster.remote_info")
class RemoteInfoRequest extends Request {
}
@namespace("cluster.root_node_info")
class RootNodeInfoRequest extends Request {
}
@namespace("cluster.task_management.cancel_tasks")
class CancelTasksRequest extends Request {
	@request_parameter()
	nodes: string[];
	@request_parameter()
	actions: string[];
	@request_parameter()
	parent_node: string;
	@request_parameter()
	parent_task_id: string;
}
@namespace("cluster.task_management.get_task")
class GetTaskRequest extends Request {
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("cluster.task_management.list_tasks")
class ListTasksRequest extends Request {
	@request_parameter()
	nodes: string[];
	@request_parameter()
	actions: string[];
	@request_parameter()
	detailed: boolean;
	@request_parameter()
	parent_node: string;
	@request_parameter()
	parent_task_id: string;
	@request_parameter()
	wait_for_completion: boolean;
	@request_parameter()
	group_by: GroupBy;
}
@namespace("document.multiple.bulk")
@class_serializer("BulkRequestJsonConverter")
class BulkRequest extends Request {
	operations: BulkOperation[];
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	pipeline: string;
}
@namespace("document.multiple.delete_by_query")
class DeleteByQueryRequest extends Request {
	query: QueryContainer;
	slice: SlicedScroll;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	from: long;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	conflicts: Conflicts;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	search_timeout: Time;
	@request_parameter()
	size: long;
	@request_parameter()
	sort: string[];
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	terminate_after: long;
	@request_parameter()
	stats: string[];
	@request_parameter()
	version: boolean;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	scroll_size: long;
	@request_parameter()
	wait_for_completion: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	slices: long;
}
@namespace("document.multiple.multi_get.request")
@class_serializer("MultiGetRequestJsonConverter")
class MultiGetRequest extends Request {
	@request_parameter()
	stored_fields: Field[];
	docs: MultiGetOperation[];
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
}
@namespace("document.multiple.multi_term_vectors")
class MultiTermVectorsRequest extends Request {
	docs: MultiTermVectorOperation[];
	@request_parameter()
	term_statistics: boolean;
	@request_parameter()
	field_statistics: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	offsets: boolean;
	@request_parameter()
	positions: boolean;
	@request_parameter()
	payloads: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	parent: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.multiple.reindex_on_server")
class ReindexOnServerRequest extends Request {
	source: ReindexSource;
	dest: ReindexDestination;
	script: Script;
	size: long;
	conflicts: Conflicts;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	wait_for_completion: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	slices: long;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexRethrottleRequest extends Request {
	@request_parameter()
	requests_per_second: long;
}
@namespace("document.multiple.update_by_query")
class UpdateByQueryRequest extends Request {
	query: QueryContainer;
	script: Script;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	from: long;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	conflicts: Conflicts;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	pipeline: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	search_timeout: Time;
	@request_parameter()
	size: long;
	@request_parameter()
	sort: string[];
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	terminate_after: long;
	@request_parameter()
	stats: string[];
	@request_parameter()
	version: boolean;
	@request_parameter()
	version_type: boolean;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	scroll_size: long;
	@request_parameter()
	wait_for_completion: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	slices: long;
}
@namespace("document.single.delete")
class DeleteRequest extends Request {
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	parent: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.exists")
class DocumentExistsRequest extends Request {
	@request_parameter()
	stored_fields: Field[];
	@request_parameter()
	parent: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.get")
class GetRequest extends Request {
	@request_parameter()
	stored_fields: Field[];
	@request_parameter()
	parent: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.source_exists")
class SourceExistsRequest extends Request {
	@request_parameter()
	parent: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.source")
class SourceRequest extends Request {
	@request_parameter()
	parent: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.term_vectors")
class TermVectorsRequest<TDocument> extends Request {
	@prop_serializer("SourceConverter")
	doc: TDocument;
	per_field_analyzer: Map<Field, string>;
	filter: TermVectorFilter;
	@request_parameter()
	term_statistics: boolean;
	@request_parameter()
	field_statistics: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	offsets: boolean;
	@request_parameter()
	positions: boolean;
	@request_parameter()
	payloads: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	parent: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.term_vectors")
class TermVectorsResponse extends Response {
	_index: string;
	_type: string;
	_id: string;
	_version: long;
	found: boolean;
	took: long;
	@prop_serializer("ResolvableDictionaryJsonConverter`2")
	term_vectors: Map<Field, TermVector>;
}
@namespace("document.single.update")
class UpdateRequest<TDocument, TPartialDocument> extends Request {
	script: Script;
	@prop_serializer("SourceConverter")
	upsert: TDocument;
	doc_as_upsert: boolean;
	@prop_serializer("SourceConverter")
	doc: TPartialDocument;
	detect_noop: boolean;
	scripted_upsert: boolean;
	_source: Union<boolean, SourceFilter>;
	fields: Field[];
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	lang: string;
	@request_parameter()
	parent: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	retry_on_conflict: long;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("indices.alias_management.alias_exists")
class AliasExistsRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	local: boolean;
}
@namespace("indices.alias_management.alias")
class BulkAliasRequest extends Request {
	actions: AliasAction[];
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.alias_management.delete_alias")
class DeleteAliasRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.alias_management.get_alias")
class GetAliasRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	local: boolean;
}
@namespace("indices.alias_management.put_alias")
class PutAliasRequest extends Request {
	routing: Routing;
	filter: QueryContainer;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.analyze")
class AnalyzeRequest extends Request {
	tokenizer: Union<string, Tokenizer>;
	analyzer: string;
	explain: boolean;
	attributes: string[];
	char_filter: Union<string, CharFilter>[];
	filter: Union<string, TokenFilter>[];
	normalizer: string;
	field: Field;
	text: string[];
	@request_parameter()
	prefer_local: boolean;
	@request_parameter()
	format: Format;
}
@namespace("indices.index_management.delete_index")
class DeleteIndexRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.index_management.get_index")
class GetIndexRequest extends Request {
	@request_parameter()
	local: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	include_defaults: boolean;
}
@namespace("indices.index_management.indices_exists")
class IndexExistsRequest extends Request {
	@request_parameter()
	local: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	include_defaults: boolean;
}
@namespace("indices.index_management.open_close_index.close_index")
class CloseIndexRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.index_management.open_close_index.open_index")
class OpenIndexRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.index_management.shrink_index")
class ShrinkIndexRequest extends Request {
	settings: Map<string, any>;
	aliases: Map<IndexName, Alias>;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.types_exists")
class TypeExistsRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	local: boolean;
}
@namespace("indices.index_settings.get_index_settings")
class GetIndexSettingsRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	include_defaults: boolean;
}
@namespace("indices.index_settings.index_templates.delete_index_template")
class DeleteIndexTemplateRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.index_settings.index_templates.get_index_template")
class GetIndexTemplateRequest extends Request {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	local: boolean;
}
@namespace("indices.index_settings.index_templates.index_template_exists")
class IndexTemplateExistsRequest extends Request {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	local: boolean;
}
@namespace("indices.mapping_management.get_field_mapping")
class GetFieldMappingRequest extends Request {
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	local: boolean;
}
@namespace("indices.mapping_management.get_mapping")
class GetMappingRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	local: boolean;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStatusRequest extends Request {
	@request_parameter()
	detailed: boolean;
	@request_parameter()
	active_only: boolean;
}
@namespace("indices.monitoring.indices_segments")
class SegmentsRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	operation_threading: string;
	@request_parameter()
	verbose: boolean;
}
@namespace("indices.monitoring.indices_shard_stores")
class IndicesShardStoresRequest extends Request {
	types: TypeName[];
	@request_parameter()
	status: string[];
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	operation_threading: string;
}
@namespace("indices.monitoring.indices_stats")
class IndicesStatsRequest extends Request {
	types: TypeName[];
	@request_parameter()
	completion_fields: Field[];
	@request_parameter()
	fielddata_fields: Field[];
	@request_parameter()
	fields: Field[];
	@request_parameter()
	groups: string[];
	@request_parameter()
	level: Level;
	@request_parameter()
	include_segment_file_sizes: boolean;
}
@namespace("indices.status_management.clear_cache")
class ClearCacheRequest extends Request {
	@request_parameter()
	fielddata: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	query: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	recycler: boolean;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	request: boolean;
}
@namespace("indices.status_management.flush")
class FlushRequest extends Request {
	@request_parameter()
	force: boolean;
	@request_parameter()
	wait_if_ongoing: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.status_management.force_merge")
class ForceMergeRequest extends Request {
	@request_parameter()
	flush: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	max_num_segments: long;
	@request_parameter()
	only_expunge_deletes: boolean;
	@request_parameter()
	operation_threading: string;
	@request_parameter()
	wait_for_merge: boolean;
}
@namespace("indices.status_management.refresh")
class RefreshRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.status_management.synced_flush")
class SyncedFlushRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("indices.status_management.upgrade")
class UpgradeRequest extends Request {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	wait_for_completion: boolean;
	@request_parameter()
	only_ancient_segments: boolean;
}
@namespace("indices.status_management.upgrade.upgrade_status")
class UpgradeStatusRequest extends Request {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("ingest.delete_pipeline")
class DeletePipelineRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("ingest.get_pipeline")
class GetPipelineRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("ingest.processor")
class GrokProcessorPatternsRequest extends Request {
}
@namespace("ingest.simulate_pipeline")
class SimulatePipelineRequest extends Request {
	pipeline: Pipeline;
	docs: SimulatePipelineDocument[];
	@request_parameter()
	verbose: boolean;
}
@namespace("mapping.types")
class CorePropertyBase extends PropertyBase {
	copy_to: Field[];
	fields: Map<PropertyName, Property>;
	similarity: Union<SimilarityOption, string>;
	store: boolean;
}
@namespace("mapping.types.core.join")
class JoinProperty extends PropertyBase {
	relations: Map<RelationName, RelationName[]>;
}
@namespace("mapping.types.core.percolator")
class PercolatorProperty extends PropertyBase {
}
@namespace("modules.scripting.delete_script")
class DeleteScriptRequest extends Request {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.scripting.get_script")
class GetScriptRequest extends Request {
}
@namespace("modules.scripting.put_script")
class PutScriptRequest extends Request {
	script: StoredScript;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.create_repository")
@class_serializer("CreateRepositoryJsonConverter")
class CreateRepositoryRequest extends Request {
	repository: SnapshotRepository;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	verify: boolean;
}
@namespace("modules.snapshot_and_restore.repositories.delete_repository")
class DeleteRepositoryRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.get_repository")
class GetRepositoryRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	local: boolean;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
class VerifyRepositoryRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.restore")
class RestoreRequest extends Request {
	indices: Indices;
	ignore_unavailable: boolean;
	include_global_state: boolean;
	rename_pattern: string;
	rename_replacement: string;
	index_settings: UpdateIndexSettingsRequest;
	ignore_index_settings: string[];
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("modules.snapshot_and_restore.snapshot.delete_snapshot")
class DeleteSnapshotRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.snapshot_and_restore.snapshot.get_sapshot")
class GetSnapshotRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	verbose: boolean;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStatusRequest extends Request {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot")
class SnapshotRequest extends Request {
	@prop_serializer("IndicesMultiSyntaxJsonConverter")
	indices: Indices;
	ignore_unavailable: boolean;
	include_global_state: boolean;
	partial: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("search.count")
class CountRequest extends Request {
	query: QueryContainer;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	min_score: double;
	@request_parameter()
	preference: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	terminate_after: long;
}
@namespace("search.explain")
class ExplainRequest<TDocument> extends Request {
	@request_parameter()
	stored_fields: Field[];
	query: QueryContainer;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	parent: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_exclude: Field[];
	@request_parameter()
	source_include: Field[];
}
@namespace("search.field_capabilities")
class FieldCapabilitiesRequest extends Request {
	@request_parameter()
	fields: Field[];
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("search.multi_search_template")
@class_serializer("MultiSearchTemplateJsonConverter")
class MultiSearchTemplateRequest extends Request {
	operations: Map<string, SearchTemplateRequest>;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	typed_keys: boolean;
	@request_parameter()
	max_concurrent_searches: long;
}
@namespace("search.multi_search")
@class_serializer("MultiSearchJsonConverter")
class MultiSearchRequest extends Request {
	operations: Map<string, SearchRequest>;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	max_concurrent_searches: long;
	@request_parameter()
	typed_keys: boolean;
	@request_parameter()
	pre_filter_shard_size: long;
}
@namespace("search.scroll.clear_scroll")
class ClearScrollRequest extends Request {
	scroll_id: string[];
}
@namespace("search.search_shards")
class SearchShardsRequest extends Request {
	@request_parameter()
	preference: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	local: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
}
@namespace("search.search_template.render_search_template")
class RenderSearchTemplateRequest extends Request {
	source: string;
	inline: string;
	file: string;
	params: Map<string, any>;
}
@namespace("search.validate")
class ValidateQueryRequest extends Request {
	query: QueryContainer;
	@request_parameter()
	explain: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	operation_threading: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	rewrite: boolean;
	@request_parameter()
	all_shards: boolean;
}
@namespace("x_pack.migration.deprecation_info")
class DeprecationInfoRequest extends Request {
}
@namespace("x_pack.info.x_pack_info")
class XPackInfoRequest extends Request {
	@request_parameter()
	categories: string[];
}
@namespace("x_pack.info.x_pack_usage")
class XPackUsageRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.license.delete_license")
class DeleteLicenseRequest extends Request {
}
@namespace("x_pack.license.get_license")
class GetLicenseRequest extends Request {
	@request_parameter()
	local: boolean;
}
@namespace("x_pack.license.post_license")
class PostLicenseRequest extends Request {
	license: License;
	@request_parameter()
	acknowledge: boolean;
}
@namespace("x_pack.machine_learning.close_job")
class CloseJobRequest extends Request {
	@request_parameter()
	force: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("x_pack.machine_learning.delete_datafeed")
class DeleteDatafeedRequest extends Request {
	@request_parameter()
	force: boolean;
}
@namespace("x_pack.machine_learning.delete_expired_data")
class DeleteExpiredDataRequest extends Request {
}
@namespace("x_pack.machine_learning.delete_job")
class DeleteJobRequest extends Request {
	@request_parameter()
	force: boolean;
}
@namespace("x_pack.machine_learning.delete_model_snapshot")
class DeleteModelSnapshotRequest extends Request {
}
@namespace("x_pack.machine_learning.flush_job")
class FlushJobRequest extends Request {
	@prop_serializer("IsoDateTimeConverter")
	advance_time: Date;
	calc_interim: boolean;
	@prop_serializer("IsoDateTimeConverter")
	end: Date;
	@prop_serializer("IsoDateTimeConverter")
	start: Date;
	@request_parameter()
	skip_time: string;
}
@namespace("x_pack.machine_learning.get_anomaly_records")
class GetAnomalyRecordsRequest extends Request {
	desc: boolean;
	exclude_interim: boolean;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	end: Date;
	page: Page;
	record_score: double;
	sort: Field;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	start: Date;
}
@namespace("x_pack.machine_learning.get_buckets")
class GetBucketsRequest extends Request {
	anomaly_score: double;
	desc: boolean;
	@prop_serializer("IsoDateTimeConverter")
	end: Date;
	exclude_interim: boolean;
	expand: boolean;
	page: Page;
	sort: Field;
	@prop_serializer("IsoDateTimeConverter")
	start: Date;
	@prop_serializer("IsoDateTimeConverter")
	timestamp: Date;
}
@namespace("x_pack.machine_learning.get_categories")
class GetCategoriesRequest extends Request {
	page: Page;
}
@namespace("x_pack.machine_learning.get_datafeed_stats")
class GetDatafeedStatsRequest extends Request {
}
@namespace("x_pack.machine_learning.get_datafeeds")
class GetDatafeedsRequest extends Request {
}
@namespace("x_pack.machine_learning.get_influencers")
class GetInfluencersRequest extends Request {
	descending: boolean;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	end: Date;
	exclude_interim: boolean;
	influencer_score: double;
	page: Page;
	sort: Field;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	start: Date;
}
@namespace("x_pack.machine_learning.get_job_stats")
class GetJobStatsRequest extends Request {
}
@namespace("x_pack.machine_learning.get_jobs")
class GetJobsRequest extends Request {
}
@namespace("x_pack.machine_learning.get_model_snapshots")
class GetModelSnapshotsRequest extends Request {
	desc: boolean;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	end: Date;
	page: Page;
	sort: Field;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	start: Date;
}
@namespace("x_pack.machine_learning.open_job")
class OpenJobRequest extends Request {
	timeout: Time;
}
@namespace("x_pack.machine_learning.post_job_data")
@class_serializer("PostJobDataConverter")
class PostJobDataRequest extends Request {
	data: any[];
	@request_parameter()
	reset_start: Date;
	@request_parameter()
	reset_end: Date;
}
@namespace("x_pack.machine_learning.preview_datafeed")
class PreviewDatafeedRequest extends Request {
}
@namespace("x_pack.machine_learning.put_datafeed")
class PutDatafeedRequest extends Request {
	aggregations: Map<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	@prop_serializer("IndicesJsonConverter")
	indices: Indices;
	job_id: Id;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Map<string, ScriptField>;
	scroll_size: integer;
	@prop_serializer("TypesJsonConverter")
	types: Types;
}
@namespace("x_pack.machine_learning.put_job")
class PutJobRequest extends Request {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	data_description: DataDescription;
	description: string;
	model_plot: ModelPlotConfig;
	model_snapshot_retention_days: long;
	results_index_name: IndexName;
}
@namespace("x_pack.machine_learning.revert_model_snapshot")
class RevertModelSnapshotRequest extends Request {
	delete_intervening_results: boolean;
}
@namespace("x_pack.machine_learning.start_datafeed")
class StartDatafeedRequest extends Request {
	timeout: Time;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	start: Date;
	@prop_serializer("EpochMillisecondsDateTimeJsonConverter")
	end: Date;
}
@namespace("x_pack.machine_learning.stop_datafeed")
class StopDatafeedRequest extends Request {
	timeout: Time;
	force: boolean;
}
@namespace("x_pack.machine_learning.update_data_feed")
class UpdateDatafeedRequest extends Request {
	aggregations: Map<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	@prop_serializer("IndicesJsonConverter")
	indices: Indices;
	job_id: Id;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Map<string, ScriptField>;
	scroll_size: integer;
	@prop_serializer("TypesJsonConverter")
	types: Types;
}
@namespace("x_pack.machine_learning.update_job")
class UpdateJobRequest extends Request {
	analysis_limits: AnalysisMemoryLimit;
	background_persist_interval: Time;
	custom_settings: Map<string, any>;
	description: string;
	model_plot_config: ModelPlotConfigEnabled;
	model_snapshot_retention_days: long;
	renormalization_window_days: long;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.update_model_snapshot")
class UpdateModelSnapshotRequest extends Request {
	description: string;
	retain: boolean;
}
@namespace("x_pack.machine_learning.validate_detector")
@class_serializer("ValidateDetectorRequestConverter")
class ValidateDetectorRequest extends Request {
	detector: Detector;
}
@namespace("x_pack.machine_learning.validate_job")
class ValidateJobRequest extends Request {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	data_description: DataDescription;
	description: string;
	model_plot: ModelPlotConfig;
	model_snapshot_retention_days: long;
	results_index_name: IndexName;
}
@namespace("x_pack.security.authenticate")
class AuthenticateRequest extends Request {
}
@namespace("x_pack.security.clear_cached_realms")
class ClearCachedRealmsRequest extends Request {
	@request_parameter()
	usernames: string[];
}
@namespace("x_pack.security.role_mapping.delete_role_mapping")
class DeleteRoleMappingRequest extends Request {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
class GetRoleMappingRequest extends Request {
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
class PutRoleMappingRequest extends Request {
	run_as: string[];
	metadata: Map<string, any>;
	enabled: boolean;
	roles: string[];
	rules: RoleMappingRuleBase;
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.role.clear_cached_roles")
class ClearCachedRolesRequest extends Request {
}
@namespace("x_pack.security.role.delete_role")
class DeleteRoleRequest extends Request {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.role.get_role")
class GetRoleRequest extends Request {
}
@namespace("x_pack.security.role.put_role")
class PutRoleRequest extends Request {
	cluster: string[];
	run_as: string[];
	indices: IndicesPrivileges[];
	metadata: Map<string, any>;
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.change_password")
class ChangePasswordRequest extends Request {
	password: string;
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.delete_user")
class DeleteUserRequest extends Request {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.disable_user")
class DisableUserRequest extends Request {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.enable_user")
class EnableUserRequest extends Request {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.get_user_access_token")
class GetUserAccessTokenRequest extends Request {
	grant_type: AccessTokenGrantType;
	scope: string;
}
@namespace("x_pack.security.user.get_user")
class GetUserRequest extends Request {
}
@namespace("x_pack.security.user.invalidate_user_access_token")
class InvalidateUserAccessTokenRequest extends Request {
}
@namespace("x_pack.security.user.put_user")
class PutUserRequest extends Request {
	password: string;
	roles: string[];
	full_name: string;
	email: string;
	metadata: Map<string, any>;
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.watcher.acknowledge_watch")
class AcknowledgeWatchRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.watcher.activate_watch")
class ActivateWatchRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.watcher.deactivate_watch")
class DeactivateWatchRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.watcher.delete_watch")
class DeleteWatchRequest extends Request {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.watcher.execute_watch")
class ExecuteWatchRequest extends Request {
	trigger_data: ScheduleTriggerEvent;
	ignore_condition: boolean;
	record_execution: boolean;
	alternative_input: Map<string, any>;
	action_modes: Map<string, ActionExecutionMode>;
	simulated_actions: SimulatedActions;
	watch: PutWatchRequest;
	@request_parameter()
	debug: boolean;
}
@namespace("x_pack.watcher.get_watch")
class GetWatchRequest extends Request {
}
@namespace("x_pack.watcher.restart_watcher")
class RestartWatcherRequest extends Request {
}
@namespace("x_pack.watcher.start_watcher")
class StartWatcherRequest extends Request {
}
@namespace("x_pack.watcher.stop_watcher")
class StopWatcherRequest extends Request {
}
@namespace("x_pack.watcher.watcher_stats")
class WatcherStatsRequest extends Request {
	@request_parameter()
	emit_stacktraces: boolean;
}
@namespace("cluster.cluster_stats")
class ClusterStatsResponse extends Response {
	cluster_name: string;
	timestamp: long;
	status: ClusterStatus;
	indices: ClusterIndicesStats;
	nodes: ClusterNodesStats;
}
@namespace("cluster.nodes_info")
class NodesInfoResponse extends Response {
	cluster_name: string;
	nodes: Map<string, NodeInfo>;
}
@namespace("cluster.nodes_stats")
class NodesStatsResponse extends Response {
	cluster_name: string;
	nodes: Map<string, NodeStats>;
}
@namespace("cluster.nodes_usage")
class NodesUsageResponse extends Response {
	cluster_name: string;
	nodes: Map<string, NodeUsageInformation>;
}
@namespace("cluster.remote_info")
class RemoteInfoResponse extends DictionaryResponseBase<string, RemoteInfo> {
	remotes: Map<string, RemoteInfo>;
}
@namespace("document.single.create")
class CreateRequest<TDocument> extends Request {
	document: TDocument;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	parent: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
	@request_parameter()
	pipeline: string;
}
@namespace("document.single.index")
class IndexRequest<TDocument> extends Request {
	document: TDocument;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	op_type: OpType;
	@request_parameter()
	parent: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
	@request_parameter()
	pipeline: string;
}
@namespace("indices.alias_management.alias")
class BulkAliasResponse extends AcknowledgedResponseBase {
}
@namespace("indices.alias_management.get_alias")
class GetAliasResponse extends DictionaryResponseBase<IndexName, IndexAliases> {
	indices: Map<IndexName, IndexAliases>;
	is_valid: boolean;
}
@namespace("indices.index_management.create_index")
class CreateIndexRequest extends Request {
	settings: Map<string, any>;
	mappings: Map<TypeName, TypeMapping>;
	aliases: Map<IndexName, Alias>;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	update_all_types: boolean;
}
@namespace("indices.index_management.create_index")
class CreateIndexResponse extends AcknowledgedResponseBase {
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.delete_index")
class DeleteIndexResponse extends IndicesResponseBase {
}
@namespace("indices.index_management.get_index")
class GetIndexResponse extends DictionaryResponseBase<IndexName, IndexState> {
	indices: Map<IndexName, IndexState>;
}
@namespace("indices.index_management.open_close_index.close_index")
class CloseIndexResponse extends AcknowledgedResponseBase {
}
@namespace("indices.index_management.open_close_index.open_index")
class OpenIndexResponse extends AcknowledgedResponseBase {
}
@namespace("indices.index_management.rollover_index")
class RolloverIndexRequest extends Request {
	conditions: RolloverConditions;
	settings: Map<string, any>;
	aliases: Map<IndexName, Alias>;
	mappings: Map<TypeName, TypeMapping>;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	dry_run: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.rollover_index")
class RolloverIndexResponse extends AcknowledgedResponseBase {
	dry_run: boolean;
	new_index: string;
	old_index: string;
	rolled_over: boolean;
	conditions: Map<string, boolean>;
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.shrink_index")
class ShrinkIndexResponse extends AcknowledgedResponseBase {
	shards_acknowledged: boolean;
}
@namespace("indices.index_settings.get_index_settings")
class GetIndexSettingsResponse extends DictionaryResponseBase<IndexName, IndexState> {
	indices: Map<IndexName, IndexState>;
}
@namespace("indices.index_settings.index_templates.delete_index_template")
class DeleteIndexTemplateResponse extends AcknowledgedResponseBase {
}
@namespace("indices.index_settings.index_templates.get_index_template")
class GetIndexTemplateResponse extends DictionaryResponseBase<string, TemplateMapping> {
	template_mappings: Map<string, TemplateMapping>;
}
@namespace("indices.index_settings.index_templates.put_index_template")
class PutIndexTemplateRequest extends Request {
	index_patterns: string[];
	order: integer;
	version: integer;
	settings: Map<string, any>;
	mappings: Map<TypeName, TypeMapping>;
	aliases: Map<IndexName, Alias>;
	@request_parameter()
	create: boolean;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	flat_settings: boolean;
}
@namespace("indices.index_settings.index_templates.put_index_template")
class PutIndexTemplateResponse extends AcknowledgedResponseBase {
}
@namespace("indices.index_settings.update_index_settings")
class UpdateIndexSettingsResponse extends AcknowledgedResponseBase {
}
@namespace("indices.mapping_management.get_field_mapping")
class GetFieldMappingResponse extends DictionaryResponseBase<IndexName, TypeFieldMappings> {
	indices: Map<IndexName, TypeFieldMappings>;
	is_valid: boolean;
}
@namespace("indices.mapping_management.get_mapping")
class GetMappingResponse extends DictionaryResponseBase<IndexName, IndexMappings> {
	indices: Map<IndexName, IndexMappings>;
	mappings: Map<IndexName, IndexMappings>;
	mapping: TypeMapping;
}
@namespace("indices.mapping_management.put_mapping")
class PutMappingRequest extends Request {
	all_field: AllField;
	date_detection: boolean;
	dynamic_date_formats: string[];
	dynamic_templates: Map<string, DynamicTemplate>;
	dynamic: Union<boolean, DynamicMapping>;
	field_names_field: FieldNamesField;
	index_field: IndexField;
	meta: Map<string, any>;
	numeric_detection: boolean;
	properties: Map<PropertyName, Property>;
	routing_field: RoutingField;
	size_field: SizeField;
	source_field: SourceField;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	update_all_types: boolean;
}
@namespace("indices.mapping_management.put_mapping")
class PutMappingResponse extends IndicesResponseBase {
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStatusResponse extends DictionaryResponseBase<IndexName, RecoveryStatus> {
	indices: Map<IndexName, RecoveryStatus>;
}
@namespace("indices.status_management.clear_cache")
class ClearCacheResponse extends ShardsOperationResponseBase {
}
@namespace("indices.status_management.flush")
class FlushResponse extends ShardsOperationResponseBase {
}
@namespace("indices.status_management.force_merge")
class ForceMergeResponse extends ShardsOperationResponseBase {
}
@namespace("indices.status_management.refresh")
class RefreshResponse extends ShardsOperationResponseBase {
}
@namespace("indices.status_management.synced_flush")
class SyncedFlushResponse extends ShardsOperationResponseBase {
}
@namespace("ingest.delete_pipeline")
class DeletePipelineResponse extends AcknowledgedResponseBase {
}
@namespace("ingest.get_pipeline")
class GetPipelineResponse extends DictionaryResponseBase<string, Pipeline> {
	pipelines: Map<string, Pipeline>;
}
@namespace("ingest.put_pipeline")
class PutPipelineRequest extends Request {
	description: string;
	processors: Processor[];
	on_failure: Processor[];
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("ingest.put_pipeline")
class PutPipelineResponse extends AcknowledgedResponseBase {
}
@namespace("modules.scripting.delete_script")
class DeleteScriptResponse extends AcknowledgedResponseBase {
}
@namespace("modules.scripting.put_script")
class PutScriptResponse extends AcknowledgedResponseBase {
}
@namespace("modules.snapshot_and_restore.repositories.create_repository")
class CreateRepositoryResponse extends AcknowledgedResponseBase {
}
@namespace("modules.snapshot_and_restore.repositories.delete_repository")
class DeleteRepositoryResponse extends AcknowledgedResponseBase {
}
@namespace("modules.snapshot_and_restore.snapshot.delete_snapshot")
class DeleteSnapshotResponse extends AcknowledgedResponseBase {
}
@namespace("search.scroll.scroll")
class ScrollRequest extends Request {
	scroll: Time;
	scroll_id: string;
}
@namespace("x_pack.graph.explore")
class GraphExploreRequest extends Request {
	query: QueryContainer;
	vertices: GraphVertexDefinition[];
	connections: Hop;
	controls: GraphExploreControls;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
}
@namespace("x_pack.machine_learning.delete_datafeed")
class DeleteDatafeedResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.machine_learning.delete_job")
class DeleteJobResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.machine_learning.delete_model_snapshot")
class DeleteModelSnapshotResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.machine_learning.revert_model_snapshot")
class RevertModelSnapshotResponse extends AcknowledgedResponseBase {
	model: ModelSnapshot;
}
@namespace("x_pack.machine_learning.update_model_snapshot")
class UpdateModelSnapshotResponse extends AcknowledgedResponseBase {
	model: ModelSnapshot;
}
@namespace("x_pack.machine_learning.validate_detector")
class ValidateDetectorResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.machine_learning.validate_job")
class ValidateJobResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
class GetRoleMappingResponse extends DictionaryResponseBase<string, XPackRoleMapping> {
	role_mappings: Map<string, XPackRoleMapping>;
}
@namespace("x_pack.security.role.get_role")
class GetRoleResponse extends DictionaryResponseBase<string, XPackRole> {
	roles: Map<string, XPackRole>;
}
@namespace("x_pack.security.user.get_user")
class GetUserResponse extends DictionaryResponseBase<string, XPackUser> {
	users: Map<string, XPackUser>;
}
@namespace("x_pack.watcher.restart_watcher")
class RestartWatcherResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.watcher.start_watcher")
class StartWatcherResponse extends AcknowledgedResponseBase {
}
@namespace("x_pack.watcher.stop_watcher")
class StopWatcherResponse extends AcknowledgedResponseBase {
}
@namespace("mapping.types.complex.object")
class ObjectProperty extends CorePropertyBase {
	dynamic: Union<boolean, DynamicMapping>;
	enabled: boolean;
	properties: Map<PropertyName, Property>;
}
@namespace("mapping.types.core.text")
class TextProperty extends CorePropertyBase {
	boost: double;
	eager_global_ordinals: boolean;
	fielddata: boolean;
	fielddata_frequency_filter: FielddataFrequencyFilter;
	index: boolean;
	index_options: IndexOptions;
	norms: boolean;
	position_increment_gap: integer;
	analyzer: string;
	search_analyzer: string;
	search_quote_analyzer: string;
	term_vector: TermVectorOption;
}
@namespace("mapping.types")
class DocValuesPropertyBase extends CorePropertyBase {
	doc_values: boolean;
}
@namespace("mapping.types.core.binary")
class BinaryProperty extends DocValuesPropertyBase {
}
@namespace("mapping.types.core.boolean")
class BooleanProperty extends DocValuesPropertyBase {
	index: boolean;
	boost: double;
	null_value: boolean;
	fielddata: NumericFielddata;
}
@namespace("mapping.types.core.date")
class DateProperty extends DocValuesPropertyBase {
	index: boolean;
	boost: double;
	null_value: Date;
	precision_step: integer;
	ignore_malformed: boolean;
	format: string;
	fielddata: NumericFielddata;
}
@namespace("mapping.types.core.keyword")
class KeywordProperty extends DocValuesPropertyBase {
	boost: double;
	eager_global_ordinals: boolean;
	ignore_above: integer;
	index: boolean;
	index_options: IndexOptions;
	norms: boolean;
	null_value: string;
	normalizer: string;
}
@namespace("mapping.types.core.number")
class NumberProperty extends DocValuesPropertyBase {
	index: boolean;
	boost: double;
	null_value: double;
	ignore_malformed: boolean;
	coerce: boolean;
	fielddata: NumericFielddata;
	scaling_factor: double;
}
@namespace("mapping.types.core.range")
class RangePropertyBase extends DocValuesPropertyBase {
	coerce: boolean;
	boost: double;
	index: boolean;
}
@namespace("mapping.types.geo.geo_point")
class GeoPointProperty extends DocValuesPropertyBase {
	ignore_malformed: boolean;
}
@namespace("mapping.types.geo.geo_shape")
class GeoShapeProperty extends DocValuesPropertyBase {
	tree: GeoTree;
	precision: Distance;
	orientation: GeoOrientation;
	tree_levels: integer;
	strategy: GeoStrategy;
	distance_error_pct: double;
	points_only: boolean;
}
@namespace("mapping.types.specialized.completion")
class CompletionProperty extends DocValuesPropertyBase {
	search_analyzer: string;
	analyzer: string;
	preserve_separators: boolean;
	preserve_position_increments: boolean;
	max_input_length: integer;
	contexts: SuggestContext[];
}
@namespace("mapping.types.specialized.generic")
class GenericProperty extends DocValuesPropertyBase {
	term_vector: TermVectorOption;
	boost: double;
	search_analyzer: string;
	ignore_above: integer;
	position_increment_gap: integer;
	fielddata: StringFielddata;
	index: FieldIndexOption;
	null_value: string;
	norms: boolean;
	index_options: IndexOptions;
	analyzer: string;
	type: string;
}
@namespace("mapping.types.specialized.ip")
class IpProperty extends DocValuesPropertyBase {
	boost: double;
	index: boolean;
	null_value: string;
}
@namespace("mapping.types.specialized.murmur3_hash")
class Murmur3HashProperty extends DocValuesPropertyBase {
}
@namespace("mapping.types.specialized.token_count")
class TokenCountProperty extends DocValuesPropertyBase {
	analyzer: string;
	index: boolean;
	boost: double;
	null_value: double;
}
@namespace("mapping.types.complex.nested")
class NestedProperty extends ObjectProperty {
	include_in_parent: boolean;
	include_in_root: boolean;
}
@namespace("mapping.types.core.range.date_range")
class DateRangeProperty extends RangePropertyBase {
	format: string;
}
@namespace("mapping.types.core.range.double_range")
class DoubleRangeProperty extends RangePropertyBase {
}
@namespace("mapping.types.core.range.float_range")
class FloatRangeProperty extends RangePropertyBase {
}
@namespace("mapping.types.core.range.integer_range")
class IntegerRangeProperty extends RangePropertyBase {
}
@namespace("mapping.types.core.range.long_range")
class LongRangeProperty extends RangePropertyBase {
}
/** namespace:common **/
/** namespace:common **/
enum HttpMethod {
	GET = 0,
	POST = 1,
	PUT = 2,
	DELETE = 3,
	HEAD = 4
}
/** namespace:common **/
enum Health {
	green = 0,
	yellow = 1,
	red = 2
}
/** namespace:common **/
enum VersionType {
	internal = 0,
	external = 1,
	external_gte = 2,
	force = 3
}
/** namespace:common **/
enum ExpandWildcards {
	open = 0,
	closed = 1,
	none = 2,
	all = 3
}
/** namespace:common **/
enum SuggestMode {
	missing = 0,
	popular = 1,
	always = 2
}
/** namespace:common **/
enum SearchType {
	query_then_fetch = 0,
	dfs_query_then_fetch = 1
}
/** namespace:common **/
enum Bytes {
	b = 0,
	k = 1,
	kb = 2,
	m = 3,
	mb = 4,
	g = 5,
	gb = 6,
	t = 7,
	tb = 8,
	p = 9,
	pb = 10
}
/** namespace:common **/
enum Size {
	Raw = 0,
	k = 1,
	m = 2,
	g = 3,
	t = 4,
	p = 5
}
/** namespace:common **/
enum Level {
	cluster = 0,
	indices = 1,
	shards = 2
}
/** namespace:common **/
enum WaitForEvents {
	immediate = 0,
	urgent = 1,
	high = 2,
	normal = 3,
	low = 4,
	languid = 5
}
/** namespace:common **/
enum WaitForStatus {
	green = 0,
	yellow = 1,
	red = 2
}
/** namespace:common **/
enum ThreadType {
	cpu = 0,
	wait = 1,
	block = 2
}
/** namespace:common **/
enum GroupBy {
	nodes = 0,
	parents = 1
}
/** namespace:common **/
enum Refresh {
	true = 0,
	false = 1,
	wait_for = 2
}
/** namespace:common **/
enum DefaultOperator {
	AND = 0,
	OR = 1
}
/** namespace:common **/
enum Conflicts {
	abort = 0,
	proceed = 1
}
/** namespace:common **/
enum OpType {
	index = 0,
	create = 1
}
/** namespace:common **/
enum Format {
	detailed = 0,
	text = 1
}
@namespace("common")
class RequestConfiguration {
	request_timeout: TimeSpan;
	ping_timeout: TimeSpan;
	content_type: string;
	accept: string;
	max_retries: integer;
	force_node: Uri;
	disable_sniff: boolean;
	disable_ping: boolean;
	disable_direct_streaming: boolean;
	allowed_status_codes: integer[];
	basic_authentication_credentials: BasicAuthenticationCredentials;
	enable_http_pipelining: boolean;
	run_as: string;
	client_certificates: any;
	throw_exceptions: boolean;
}
@namespace("common")
class ServerError {
	error: Error;
	status: integer;
}
@namespace("common")
class ShardFailure {
	reason: ErrorCause;
	shard: integer;
	index: string;
	node: string;
	status: string;
}
@namespace("common")
class BasicAuthenticationCredentials {
	username: string;
	password: string;
}
@namespace("common")
class ErrorCauseMetadata {
	licensed_expired_feature: string;
	index: string;
	index_u_u_i_d: string;
	resource_type: string;
	resource_id: string[];
	shard: integer;
	failed_shards: ShardFailure[];
	line: integer;
	column: integer;
	bytes_wanted: long;
	bytes_limit: long;
	phase: string;
	grouped: boolean;
	script_stack: string[];
	script: string;
	language: string;
}
@namespace("common")
class Map<TKey, TValue> {
	key: TKey;
	value: TValue;
}
@namespace("common_abstractions.infer.indices")
class AllIndicesMarker {
}
@namespace("common_abstractions.infer.indices")
class ManyIndices {
	indices: IndexName[];
}
@namespace("x_pack.info.x_pack_usage")
class JobStatistics {
	total: double;
	min: double;
	max: double;
	avg: double;
}
@namespace("x_pack.info.x_pack_usage")
class DataFeed {
	count: long;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingExecution {
	actions: Map<string, ExecutionAction>;
}
@namespace("x_pack.info.x_pack_usage")
class ExecutionAction {
	total: long;
	total_in_ms: long;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingCount {
	total: long;
	active: long;
}
@namespace("x_pack.info.x_pack_usage")
class SecurityFeatureToggle {
	enabled: boolean;
}
@namespace("x_pack.info.x_pack_usage")
class SslUsage {
	http: SecurityFeatureToggle;
	transport: SecurityFeatureToggle;
}
@namespace("x_pack.info.x_pack_usage")
class IpFilterUsage {
	http: boolean;
	transport: boolean;
}
@namespace("x_pack.info.x_pack_usage")
class RoleUsage {
	dls: boolean;
	fls: boolean;
	size: long;
}
@namespace("x_pack.info.x_pack_usage")
class AuditUsage extends SecurityFeatureToggle {
	outputs: string[];
}
@namespace("x_pack.info.x_pack_usage")
class RealmUsage extends XPackUsage {
	name: string[];
	size: long[];
	order: long[];
}
@namespace("common_abstractions.infer.types")
class AllTypesMarker {
}
@namespace("common_abstractions.infer.types")
class ManyTypes {
	types: TypeName[];
}
@namespace("cluster.nodes_stats")
class MemoryStats {
	total: string;
	total_in_bytes: long;
	free: string;
	free_in_bytes: long;
	used: string;
	used_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class CPUStats {
	load_average: LoadAverageStats;
	percent: float;
}
@namespace("cluster.nodes_stats")
class ExtendedMemoryStats extends MemoryStats {
	free_percent: integer;
	used_percent: integer;
}
@namespace("cluster.nodes_stats")
class LoadAverageStats {
	'1m': float;
	'5m': float;
	'15m': float;
}
@namespace("cluster.nodes_stats")
class ThreadStats {
	count: long;
	peak_count: long;
}
@namespace("cluster.nodes_stats")
class GarbageCollectionStats {
	collectors: Map<string, GarbageCollectionGenerationStats>;
}
@namespace("cluster.nodes_stats")
class GarbageCollectionGenerationStats {
	collection_count: long;
	collection_time: string;
	collection_time_in_millis: long;
}
@namespace("cluster.nodes_stats")
class NodeBufferPool {
	count: long;
	used: string;
	used_in_bytes: long;
	total_capacity: string;
	total_capacity_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class JvmClassesStats {
	current_loaded_count: long;
	total_loaded_count: long;
	total_unloaded_count: long;
}
@namespace("cluster.nodes_stats")
class JVMPool {
	used: string;
	used_in_bytes: long;
	max: string;
	max_in_bytes: long;
	peak_used: string;
	peak_used_in_bytes: long;
	peak_max: string;
	peak_max_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class TotalFileSystemStats {
	available: string;
	available_in_bytes: long;
	free: string;
	free_in_bytes: long;
	total: string;
	total_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class DataPathStats {
	path: string;
	mount: string;
	type: string;
	total: string;
	total_in_bytes: long;
	free: string;
	free_in_bytes: long;
	available: string;
	available_in_bytes: long;
	disk_reads: long;
	disk_writes: long;
	disk_read_size: string;
	disk_read_size_in_bytes: long;
	disk_write_size: string;
	disk_write_size_in_bytes: long;
	disk_queue: string;
}
