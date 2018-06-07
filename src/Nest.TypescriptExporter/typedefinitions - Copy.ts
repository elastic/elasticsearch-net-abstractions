interface short {}
interface byte {}
interface integer {}
interface long {}
interface float {}
interface double {}

/**namespace:DefaultLanguageConstruct */
interface map<t_key, t_value> {
}
/**namespace:Aggregations.Bucket.DateHistogram */
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
/**namespace:Analysis.Languages */
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
/**namespace:Analysis.Languages */
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
/**namespace:Analysis.Plugins.Icu.Collation */
enum IcuCollationStrength {
	primary = 0,
	secondary = 1,
	tertiary = 2,
	quaternary = 3,
	identical = 4
}
/**namespace:Analysis.Plugins.Icu.Collation */
enum IcuCollationDecomposition {
	no = 0,
	identical = 1
}
/**namespace:Analysis.Plugins.Icu.Collation */
enum IcuCollationAlternate {
	shifted = 0,
	non-ignorable = 1
}
/**namespace:Analysis.Plugins.Icu.Collation */
enum IcuCollationCaseFirst {
	lower = 0,
	upper = 1
}
/**namespace:Analysis.Plugins.Icu.Normalization */
enum IcuNormalizationType {
	nfc = 0,
	nfkc = 1,
	nfkc_cf = 2
}
/**namespace:Analysis.Plugins.Icu.Normalization */
enum IcuNormalizationMode {
	decompose = 0,
	compose = 1
}
/**namespace:Analysis.Plugins.Icu.Transform */
enum IcuTransformDirection {
	forward = 0,
	reverse = 1
}
/**namespace:Analysis.Plugins.Kuromoji */
enum KuromojiTokenizationMode {
	normal = 0,
	search = 1,
	extended = 2
}
/**namespace:Analysis.Plugins.Phonetic */
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
/**namespace:Analysis.TokenFilters.DelimitedPayload */
enum DelimitedPayloadEncoding {
	int = 0,
	float = 1,
	identity = 2
}
/**namespace:Analysis.TokenFilters.EdgeNGram */
enum EdgeNGramSide {
	front = 0,
	back = 1
}
/**namespace:Analysis.TokenFilters.Synonym */
enum SynonymFormat {
	solr = 0,
	wordnet = 1
}
/**namespace:Analysis.Tokenizers.NGram */
enum TokenChar {
	letter = 0,
	digit = 1,
	whitespace = 2,
	punctuation = 3,
	symbol = 4
}
/**namespace:CommonOptions.TimeUnit */
enum TimeUnit {
	nanos = 0,
	micros = 1,
	ms = 2,
	s = 3,
	m = 4,
	h = 5,
	d = 6
}
/**namespace:Cluster.ClusterAllocationExplain */
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
/**namespace:Cluster.ClusterAllocationExplain */
enum AllocationExplainDecision {
	NO = 0,
	YES = 1,
	THROTTLE = 2,
	ALWAYS = 3
}
/**namespace:Cluster.ClusterAllocationExplain */
enum Decision {
	yes = 0,
	no = 1
}
/**namespace:Cluster */
enum ClusterStatus {
	green = 0,
	yellow = 1,
	red = 2
}
/**namespace:Cluster.NodesInfo */
enum NodeRole {
	master = 0,
	data = 1,
	client = 2,
	ingest = 3
}
/**namespace:Search.Search.Sort */
enum SortOrder {
	asc = 0,
	desc = 1
}
/**namespace:Search.Search.Sort */
enum SortMode {
	min = 0,
	max = 1,
	sum = 2,
	avg = 3
}
/**namespace:Document */
enum Result {
	Error = 0,
	created = 1,
	updated = 2,
	deleted = 3,
	not_found = 4,
	noop = 5
}
/**namespace:QueryDsl.MultiTermQueryRewrite */
enum RewriteMultiTerm {
	constant_score = 0,
	scoring_boolean = 1,
	constant_score_boolean = 2,
	top_terms_N = 3,
	top_terms_boost_N = 4,
	top_terms_blended_freqs_N = 5
}
/**namespace:QueryDsl.FullText.MultiMatch */
enum TextQueryType {
	best_fields = 0,
	most_fields = 1,
	cross_fields = 2,
	phrase = 3,
	phrase_prefix = 4
}
/**namespace:QueryDsl */
enum Operator {
	and = 0,
	or = 1
}
/**namespace:QueryDsl.FullText.MultiMatch */
enum ZeroTermsQuery {
	all = 0,
	none = 1
}
/**namespace:CommonOptions.Geo */
enum GeoShapeRelation {
	intersects = 0,
	disjoint = 1,
	within = 2,
	contains = 3
}
/**namespace:Search.Search.Highlighting */
enum HighlighterOrder {
	score = 0
}
/**namespace:Search.Search.Highlighting */
enum HighlighterTagsSchema {
	styled = 0
}
/**namespace:Search.Search.Highlighting */
enum BoundaryScanner {
	chars = 0,
	sentence = 1,
	word = 2
}
/**namespace:Search.Search.Highlighting */
enum HighlighterFragmenter {
	simple = 0,
	span = 1
}
/**namespace:Search.Search.Highlighting */
enum HighlighterEncoder {
	default = 0,
	html = 1
}
/**namespace:QueryDsl.Joining.HasChild */
enum ChildScoreMode {
	none = 0,
	avg = 1,
	sum = 2,
	max = 3,
	min = 4
}
/**namespace:QueryDsl.FullText.SimpleQueryString */
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
/**namespace:QueryDsl.Joining.Nested */
enum NestedScoreMode {
	avg = 0,
	sum = 1,
	min = 2,
	max = 3,
	none = 4
}
/**namespace:QueryDsl.Compound.FunctionScore.Functions */
enum FunctionScoreMode {
	multiply = 0,
	sum = 1,
	avg = 2,
	first = 3,
	max = 4,
	min = 5
}
/**namespace:QueryDsl.Compound.FunctionScore.Functions */
enum FunctionBoostMode {
	multiply = 0,
	replace = 1,
	sum = 2,
	avg = 3,
	max = 4,
	min = 5
}
/**namespace:QueryDsl.Geo.BoundingBox */
enum GeoExecution {
	memory = 0,
	indexed = 1
}
/**namespace:QueryDsl.Geo */
enum GeoValidationMethod {
	coerce = 0,
	ignore_malformed = 1,
	strict = 2
}
/**namespace:CommonOptions.Geo */
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
/**namespace:CommonOptions.Geo */
enum GeoDistanceType {
	arc = 0,
	plane = 1
}
/**namespace:Indices.Monitoring.IndicesShardStores */
enum ShardStoreAllocation {
	primary = 0,
	replica = 1,
	unused = 2
}
/**namespace:Modules.Indices.Fielddata.Numeric */
enum NumericFielddataFormat {
	array = 0,
	disabled = 1
}
/**namespace:Mapping.Types.Core.Text */
enum IndexOptions {
	docs = 0,
	freqs = 1,
	positions = 2,
	offsets = 3
}
/**namespace:Mapping */
enum TermVectorOption {
	no = 0,
	yes = 1,
	with_offsets = 2,
	with_positions = 3,
	with_positions_offsets = 4,
	with_positions_offsets_payloads = 5
}
/**namespace:Mapping.Types.Geo.GeoShape */
enum GeoTree {
	geohash = 0,
	quadtree = 1
}
/**namespace:Mapping.Types.Geo.GeoShape */
enum GeoOrientation {
	cw = 0,
	ccw = 1
}
/**namespace:Mapping.Types.Geo.GeoShape */
enum GeoStrategy {
	recursive = 0,
	term = 1
}
/**namespace:Modules.Indices.Fielddata.String */
enum StringFielddataFormat {
	paged_bytes = 0,
	disabled = 1
}
/**namespace:DefaultLanguageConstruct */
enum FieldIndexOption {
	analyzed = 0,
	not_analyzed = 1,
	no = 2
}
/**namespace:Search.Suggesters.TermSuggester */
enum SuggestSort {
	score = 0,
	frequency = 1
}
/**namespace:Search.Suggesters.TermSuggester */
enum StringDistance {
	internal = 0,
	damerau_levenshtein = 1,
	levenstein = 2,
	jarowinkler = 3,
	ngram = 4
}
/**namespace:Search.Search.Rescoring */
enum ScoreMode {
	avg = 0,
	max = 1,
	min = 2,
	multiply = 3,
	total = 4
}
/**namespace:Aggregations.Bucket.GeoHashGrid */
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
/**namespace:Aggregations.Bucket.Terms */
enum TermsAggregationExecutionHint {
	map = 0,
	global_ordinals = 1,
	global_ordinals_hash = 2,
	global_ordinals_low_cardinality = 3
}
/**namespace:Aggregations.Bucket.Terms */
enum TermsAggregationCollectMode {
	depth_first = 0,
	breadth_first = 1
}
/**namespace:Aggregations.Bucket.Sampler */
enum SamplerAggregationExecutionHint {
	map = 0,
	global_ordinals = 1,
	bytes_hash = 2
}
/**namespace:Aggregations.Matrix.MatrixStats */
enum MatrixStatsMode {
	avg = 0,
	min = 1,
	max = 2,
	sum = 3,
	median = 4
}
/**namespace:XPack.Migration.DeprecationInfo */
enum DeprecationWarningLevel {
	none = 0,
	info = 1,
	warning = 2,
	critical = 3
}
/**namespace:XPack.License.GetLicense */
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
/**namespace:XPack.License.GetLicense */
enum LicenseStatus {
	active = 0,
	valid = 1,
	invalid = 2,
	expired = 3
}
/**namespace:XPack.MachineLearning.Datafeed */
enum DatafeedState {
	started = 0,
	stopped = 1,
	starting = 2,
	stopping = 3
}
/**namespace:XPack.MachineLearning.Datafeed */
enum ChunkingMode {
	auto = 0,
	manual = 1,
	off = 2
}
/**namespace:XPack.MachineLearning.Job.Config */
enum MemoryStatus {
	ok = 0,
	soft_limit = 1,
	hard_limit = 2
}
/**namespace:XPack.MachineLearning.Job.Config */
enum JobState {
	closing = 0,
	closed = 1,
	opened = 2,
	failed = 3,
	opening = 4
}
/**namespace:XPack.MachineLearning.PutJob */
enum ExcludeFrequent {
	all = 0,
	none = 1,
	by = 2,
	over = 3
}
/**namespace:XPack.Security.User.GetUserAccessToken */
enum AccessTokenGrantType {
	password = 0
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
enum AcknowledgementState {
	awaits_successful_execution = 0,
	ackable = 1,
	acked = 2
}
/**namespace:XPack.Watcher.Execution */
enum ActionExecutionMode {
	simulate = 0,
	force_simulate = 1,
	execute = 2,
	force_execute = 3,
	skip = 4
}
/**namespace:XPack.Watcher.Action */
enum ActionType {
	email = 0,
	webhook = 1,
	index = 2,
	logging = 3,
	hipchat = 4,
	slack = 5,
	pagerduty = 6
}
/**namespace:XPack.Watcher.Input */
enum InputType {
	http = 0,
	search = 1,
	simple = 2
}
/**namespace:XPack.Watcher.Execution */
enum Status {
	success = 0,
	failure = 1,
	simulated = 2,
	throttled = 3
}
/**namespace:XPack.Watcher.Condition */
enum ConditionType {
	always = 0,
	never = 1,
	script = 2,
	compare = 3,
	array_compare = 4
}
/**namespace:XPack.Watcher.Action.Email */
enum EmailPriority {
	lowest = 0,
	low = 1,
	normal = 2,
	high = 3,
	highest = 4
}
/**namespace:XPack.Watcher.Input */
enum ConnectionScheme {
	http = 0,
	https = 1
}
/**namespace:XPack.Watcher.Input */
enum HttpInputMethod {
	head = 0,
	get = 1,
	post = 2,
	put = 3,
	delete = 4
}
/**namespace:XPack.Watcher.Action.PagerDuty */
enum PagerDutyContextType {
	link = 0,
	image = 1
}
/**namespace:XPack.Watcher.Action.PagerDuty */
enum PagerDutyEventType {
	trigger = 0,
	resolve = 1,
	acknowledge = 2
}
/**namespace:XPack.Watcher.Action.HipChat */
enum HipChatMessageFormat {
	html = 0,
	text = 1
}
/**namespace:XPack.Watcher.Action.HipChat */
enum HipChatMessageColor {
	gray = 0,
	green = 1,
	purple = 2,
	red = 3,
	yellow = 4
}
/**namespace:XPack.Watcher.ExecuteWatch */
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
/**namespace:XPack.Watcher.Input */
enum ResponseContentType {
	json = 0,
	yaml = 1,
	text = 2
}
/**namespace:XPack.Watcher.Condition */
enum Quantifier {
	some = 0,
	all = 1
}
/**namespace:XPack.Watcher.Schedule */
enum Day {
	sunday = 0,
	monday = 1,
	tuesday = 2,
	wednesday = 3,
	thursday = 4,
	friday = 5,
	saturday = 6
}
/**namespace:XPack.Watcher.Schedule */
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
/**namespace:XPack.Watcher.Schedule */
enum IntervalUnit {
	s = 0,
	m = 1,
	h = 2,
	d = 3,
	w = 4
}
/**namespace:XPack.Watcher.WatcherStats */
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
/**namespace:XPack.Watcher.WatcherStats */
enum WatcherState {
	stopped = 0,
	starting = 1,
	started = 2,
	stopping = 3
}
/**namespace:Cluster.ClusterReroute.Commands */
interface cluster_reroute_command {
	Name: string;
}
/**namespace:Mapping */
interface type_mapping {
	DynamicDateFormats: string[];
	DateDetection: boolean;
	NumericDetection: boolean;
	SourceField: source_field;
	AllField: all_field;
	RoutingField: routing_field;
	IndexField: index_field;
	SizeField: size_field;
	FieldNamesField: field_names_field;
	Meta: map<string, any>[];
	DynamicTemplates: map<string, dynamic_template>[];
	Dynamic: union<boolean, DynamicMapping>;
	Properties: map<property_name, property>[];
}
/**namespace:Mapping.MetaFields.Source */
interface source_field {
	Enabled: boolean;
	Compress: boolean;
	CompressThreshold: string;
	Includes: string[];
	Excludes: string[];
}
/**namespace:Mapping.MetaFields.All */
interface all_field {
	Enabled: boolean;
	Store: boolean;
	StoreTermVectors: boolean;
	StoreTermVectorOffsets: boolean;
	StoreTermVectorPositions: boolean;
	StoreTermVectorPayloads: boolean;
	OmitNorms: boolean;
	Analyzer: string;
	SearchAnalyzer: string;
	Similarity: string;
}
/**namespace:Mapping.MetaFields.Routing */
interface routing_field {
	Required: boolean;
}
/**namespace:Mapping.MetaFields.Index */
interface index_field {
	Enabled: boolean;
}
/**namespace:Mapping.MetaFields.Size */
interface size_field {
	Enabled: boolean;
}
/**namespace:Mapping.MetaFields.FieldNames */
interface field_names_field {
	Enabled: boolean;
}
/**namespace:Mapping.DynamicTemplate */
interface dynamic_template {
	Match: string;
	Unmatch: string;
	MatchMappingType: string;
	PathMatch: string;
	PathUnmatch: string;
	Mapping: property;
}
/**namespace:Mapping.Types */
interface property {
	Name: property_name;
	Type: string;
	LocalMetadata: map<string, any>[];
}
/**namespace:Indices.AliasManagement */
interface alias {
	Filter: query_container;
	Routing: routing;
	IndexRouting: routing;
	SearchRouting: routing;
}
/**namespace:Document.Multiple.Bulk.BulkOperation */
interface bulk_operation {
	Operation: string;
	Index: index_name;
	Type: type_name;
	Id: id;
	Version: long;
	VersionType: VersionType;
	Routing: routing;
	Parent: id;
	RetriesOnConflict: integer;
}
/**namespace:Document.Multiple.Bulk.BulkResponseItem */
interface bulk_response_item {
	Operation: string;
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Status: integer;
	Error: bulk_error;
	Shards: shard_statistics;
	SequenceNumber: long;
	PrimaryTerm: long;
	IsValid: boolean;
}
/**namespace:Search.Scroll.Scroll */
interface sliced_scroll {
	Id: integer;
	Max: integer;
	Field: field;
}
/**namespace:Document.Multiple.MultiGet.Request */
interface multi_get_operation {
	Index: index_name;
	Type: type_name;
	Id: id;
	StoredFields: field[];
	Routing: string;
	Source: union<boolean, source_filter>;
	Version: long;
	VersionType: VersionType;
	CanBeFlattened: boolean;
}
/**namespace:Search.Search.SourceFiltering */
interface source_filter {
	Includes: field[];
	Excludes: field[];
}
/**namespace:Document.Multiple.MultiGet.Response */
interface multi_get_hit<t_document> {
	Source: t_document;
	Index: string;
	Found: boolean;
	Type: string;
	Version: long;
	Id: string;
	Parent: string;
	Routing: string;
	Error: error;
}
/**namespace:Document.Multiple.MultiTermVectors */
interface multi_term_vector_operation {
	Index: index_name;
	Type: type_name;
	Id: id;
	Document: any;
	StoredFields: field[];
	Offsets: boolean;
	Payloads: boolean;
	Positions: boolean;
	TermStatistics: boolean;
	FieldStatistics: boolean;
	Filter: term_vector_filter;
	Version: long;
	VersionType: VersionType;
	Routing: routing;
}
/**namespace:Document.Single.TermVectors */
interface term_vector_filter {
	MaximumNumberOfTerms: integer;
	MinimumTermFrequency: integer;
	MaximumTermFrequency: integer;
	MinimumDocumentFrequency: integer;
	MaximumDocumentFrequency: integer;
	MinimumWordLength: integer;
	MaximumWordLength: integer;
}
/**namespace:Document.Single.TermVectors */
interface term_vectors {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Found: boolean;
	Took: long;
	TermVectors: map<field, term_vector>[];
}
/**namespace:Document.Multiple.ReindexOnServer */
interface reindex_source {
	Query: query_container;
	Sort: sort[];
	Index: indices;
	Type: types;
	Size: integer;
	Remote: remote_source;
}
/**namespace:Search.Search.Sort */
interface sort {
	SortKey: field;
	Missing: any;
	Order: SortOrder;
	Mode: SortMode;
	NestedFilter: query_container;
	NestedPath: field;
}
/**namespace:Document.Multiple.ReindexOnServer */
interface remote_source {
	Host: uri;
	Username: string;
	Password: string;
}
/**namespace:Document.Multiple.ReindexOnServer */
interface reindex_destination {
	Index: index_name;
	Type: type_name;
	Routing: reindex_routing;
	OpType: OpType;
	VersionType: VersionType;
}
/**namespace:CommonOptions.Scripting */
interface script {
	Params: map<string, any>[];
	Lang: string;
}
/**namespace:Indices.AliasManagement.Alias.Actions */
interface alias_action {
}
/**namespace:QueryDsl.Abstractions.Container */
interface query_container {
	IsConditionless: boolean;
	IsStrict: boolean;
	IsVerbatim: boolean;
	IsWritable: boolean;
	RawQuery: raw_query;
	Bool: bool_query;
	MatchAll: match_all_query;
	MatchNone: match_none_query;
	Term: term_query;
	Wildcard: wildcard_query;
	Prefix: prefix_query;
	Boosting: boosting_query;
	Ids: ids_query;
	ConstantScore: constant_score_query;
	DisMax: dis_max_query;
	MultiMatch: multi_match_query;
	Match: match_query;
	MatchPhrase: match_phrase_query;
	MatchPhrasePrefix: match_phrase_prefix_query;
	Fuzzy: fuzzy_query;
	GeoShape: geo_shape_query;
	CommonTerms: common_terms_query;
	Terms: terms_query;
	Range: range_query;
	Regexp: regexp_query;
	HasChild: has_child_query;
	HasParent: has_parent_query;
	ParentId: parent_id_query;
	SpanTerm: span_term_query;
	SimpleQueryString: simple_query_string_query;
	QueryString: query_string_query;
	MoreLikeThis: more_like_this_query;
	SpanFirst: span_first_query;
	SpanOr: span_or_query;
	SpanNear: span_near_query;
	SpanNot: span_not_query;
	SpanContaining: span_containing_query;
	SpanWithin: span_within_query;
	SpanMultiTerm: span_multi_term_query;
	SpanFieldMasking: span_field_masking_query;
	Nested: nested_query;
	FunctionScore: function_score_query;
	GeoBoundingBox: geo_bounding_box_query;
	GeoDistance: geo_distance_query;
	GeoPolygon: geo_polygon_query;
	Script: script_query;
	Exists: exists_query;
	Type: type_query;
	Percolate: percolate_query;
}
/**namespace:QueryDsl.NestSpecific */
interface raw_query {
	Raw: string;
}
/**namespace:QueryDsl.Compound.Bool */
interface bool_query {
	Must: query_container[];
	MustNot: query_container[];
	Should: query_container[];
	Filter: query_container[];
	MinimumShouldMatch: minimum_should_match;
	Locked: boolean;
}
/**namespace:QueryDsl */
interface match_all_query {
	NormField: string;
}
/**namespace:QueryDsl */
interface match_none_query {
}
/**namespace:QueryDsl.TermLevel.Term */
interface term_query {
	Value: any;
}
/**namespace:QueryDsl.TermLevel.Wildcard */
interface wildcard_query {
	Rewrite: multi_term_query_rewrite;
}
/**namespace:QueryDsl.TermLevel.Prefix */
interface prefix_query {
	Rewrite: multi_term_query_rewrite;
}
/**namespace:QueryDsl.Compound.Boosting */
interface boosting_query {
	PositiveQuery: query_container;
	NegativeQuery: query_container;
	NegativeBoost: double;
}
/**namespace:QueryDsl.TermLevel.Ids */
interface ids_query {
	Types: types;
	Values: id[];
}
/**namespace:QueryDsl.Compound.ConstantScore */
interface constant_score_query {
	Filter: query_container;
}
/**namespace:QueryDsl.Compound.Dismax */
interface dis_max_query {
	TieBreaker: double;
	Queries: query_container[];
}
/**namespace:QueryDsl.FullText.MultiMatch */
interface multi_match_query {
	Type: TextQueryType;
	Query: string;
	Analyzer: string;
	FuzzyRewrite: multi_term_query_rewrite;
	Fuzziness: fuzziness;
	CutoffFrequency: double;
	PrefixLength: integer;
	MaxExpansions: integer;
	Slop: integer;
	Lenient: boolean;
	UseDisMax: boolean;
	TieBreaker: double;
	MinimumShouldMatch: minimum_should_match;
	Operator: Operator;
	Fields: field[];
	ZeroTermsQuery: ZeroTermsQuery;
}
/**namespace:QueryDsl.FullText.Match */
interface match_query {
	Query: string;
	Analyzer: string;
	FuzzyRewrite: multi_term_query_rewrite;
	Fuzziness: fuzziness;
	FuzzyTranspositions: boolean;
	CutoffFrequency: double;
	PrefixLength: integer;
	MaxExpansions: integer;
	Lenient: boolean;
	MinimumShouldMatch: minimum_should_match;
	Operator: Operator;
	ZeroTermsQuery: ZeroTermsQuery;
}
/**namespace:CommonOptions.Fuzziness */
interface fuzziness {
	Auto: boolean;
	EditDistance: integer;
	Ratio: double;
}
/**namespace:QueryDsl.FullText.MatchPhrase */
interface match_phrase_query {
	Query: string;
	Analyzer: string;
	Slop: integer;
}
/**namespace:QueryDsl.FullText.MatchPhrasePrefix */
interface match_phrase_prefix_query {
	Query: string;
	Analyzer: string;
	MaxExpansions: integer;
	Slop: integer;
}
/**namespace:QueryDsl.TermLevel.Fuzzy */
interface fuzzy_query {
	PrefixLength: integer;
	Rewrite: multi_term_query_rewrite;
	MaxExpansions: integer;
	Transpositions: boolean;
}
/**namespace:QueryDsl.Geo.Shape */
interface geo_shape_query {
	Relation: GeoShapeRelation;
}
/**namespace:QueryDsl.FullText.CommonTerms */
interface common_terms_query {
	Query: string;
	CutoffFrequency: double;
	LowFrequencyOperator: Operator;
	HighFrequencyOperator: Operator;
	MinimumShouldMatch: minimum_should_match;
	Analyzer: string;
}
/**namespace:QueryDsl.TermLevel.Terms */
interface terms_query {
	Terms: any[];
	TermsLookup: field_lookup;
}
/**namespace:QueryDsl.Abstractions.FieldLookup */
interface field_lookup {
	Index: index_name;
	Type: type_name;
	Id: id;
	Path: field;
	Routing: routing;
}
/**namespace:QueryDsl.TermLevel.Range */
interface range_query {
}
/**namespace:QueryDsl.TermLevel.Regexp */
interface regexp_query {
	Value: string;
	Flags: string;
	MaximumDeterminizedStates: integer;
}
/**namespace:QueryDsl.Joining.HasChild */
interface has_child_query {
	Type: type_name;
	ScoreMode: ChildScoreMode;
	MinChildren: integer;
	MaxChildren: integer;
	Query: query_container;
	InnerHits: inner_hits;
	IgnoreUnmapped: boolean;
}
/**namespace:Search.Search.InnerHits */
interface inner_hits {
	Name: string;
	From: integer;
	Size: integer;
	Sort: sort[];
	Highlight: highlight;
	Explain: boolean;
	Source: union<boolean, source_filter>;
	Version: boolean;
	ScriptFields: map<string, script_field>[];
	DocValueFields: field[];
}
/**namespace:Search.Search.Highlighting */
interface highlight {
	PreTags: string[];
	PostTags: string[];
	FragmentSize: integer;
	NoMatchSize: integer;
	NumberOfFragments: integer;
	FragmentOffset: integer;
	BoundaryMaxScan: integer;
	Encoder: HighlighterEncoder;
	Order: HighlighterOrder;
	TagsSchema: HighlighterTagsSchema;
	Fields: map<field, highlight_field>[];
	RequireFieldMatch: boolean;
	BoundaryChars: string;
	MaxFragmentLength: integer;
	BoundaryScanner: BoundaryScanner;
	BoundaryScannerLocale: string;
	Fragmenter: HighlighterFragmenter;
}
/**namespace:Search.Search.Highlighting */
interface highlight_field {
	Field: field;
	PreTags: string[];
	PostTags: string[];
	FragmentSize: integer;
	NoMatchSize: integer;
	NumberOfFragments: integer;
	FragmentOffset: integer;
	BoundaryMaxScan: integer;
	Order: HighlighterOrder;
	TagsSchema: HighlighterTagsSchema;
	RequireFieldMatch: boolean;
	BoundaryChars: string;
	MaxFragmentLength: integer;
	BoundaryScanner: BoundaryScanner;
	BoundaryScannerLocale: string;
	Fragmenter: HighlighterFragmenter;
	Type: union<HighlighterType, string>;
	ForceSource: boolean;
	MatchedFields: field[];
	HighlightQuery: query_container;
	PhraseLimit: integer;
}
/**namespace:CommonOptions.Scripting */
interface script_field {
	Script: script;
}
/**namespace:QueryDsl.Joining.HasParent */
interface has_parent_query {
	ParentType: type_name;
	Score: boolean;
	Query: query_container;
	InnerHits: inner_hits;
	IgnoreUnmapped: boolean;
}
/**namespace:QueryDsl.Joining.ParentId */
interface parent_id_query {
	Type: relation_name;
	Id: id;
	IgnoreUnmapped: boolean;
}
/**namespace:QueryDsl.Span.Term */
interface span_term_query {
}
/**namespace:QueryDsl.FullText.SimpleQueryString */
interface simple_query_string_query {
	Fields: field[];
	Query: string;
	Analyzer: string;
	DefaultOperator: Operator;
	Flags: SimpleQueryStringFlags;
	Lenient: boolean;
	AnalyzeWildcard: boolean;
	MinimumShouldMatch: minimum_should_match;
	QuoteFieldSuffix: string;
}
/**namespace:QueryDsl.FullText.QueryString */
interface query_string_query {
	Type: TextQueryType;
	Query: string;
	DefaultField: field;
	DefaultOperator: Operator;
	Analyzer: string;
	QuoteAnalyzer: string;
	AllowLeadingWildcard: boolean;
	FuzzyMaxExpansions: integer;
	Fuzziness: fuzziness;
	FuzzyPrefixLength: integer;
	PhraseSlop: double;
	AnalyzeWildcard: boolean;
	MaximumDeterminizedStates: integer;
	MinimumShouldMatch: minimum_should_match;
	Lenient: boolean;
	Fields: field[];
	TieBreaker: double;
	Rewrite: multi_term_query_rewrite;
	FuzzyRewrite: multi_term_query_rewrite;
	QuoteFieldSuffix: string;
	Escape: boolean;
}
/**namespace:QueryDsl.Specialized.MoreLikeThis */
interface more_like_this_query {
	Fields: field[];
	Like: like[];
	Unlike: like[];
	MaxQueryTerms: integer;
	MinTermFrequency: integer;
	MinDocumentFrequency: integer;
	MaxDocumentFrequency: integer;
	MinWordLength: integer;
	MaxWordLength: integer;
	StopWords: stop_words;
	Analyzer: string;
	MinimumShouldMatch: minimum_should_match;
	BoostTerms: double;
	Include: boolean;
	PerFieldAnalyzer: map<field, string>[];
	Version: long;
	VersionType: VersionType;
	Routing: routing;
}
/**namespace:QueryDsl.Specialized.MoreLikeThis.Like */
interface like_document {
	Index: index_name;
	Type: type_name;
	Id: id;
	Fields: field[];
	Routing: routing;
	Document: any;
	PerFieldAnalyzer: map<field, string>[];
}
/**namespace:QueryDsl.Span.First */
interface span_first_query {
	Match: span_query;
	End: integer;
}
/**namespace:QueryDsl.Span */
interface span_query {
	SpanTerm: span_term_query;
	SpanFirst: span_first_query;
	SpanNear: span_near_query;
	SpanOr: span_or_query;
	SpanNot: span_not_query;
	SpanContaining: span_containing_query;
	SpanWithin: span_within_query;
	SpanMultiTerm: span_multi_term_query;
	SpanFieldMasking: span_field_masking_query;
}
/**namespace:QueryDsl.Span.Near */
interface span_near_query {
	Clauses: span_query[];
	Slop: integer;
	InOrder: boolean;
}
/**namespace:QueryDsl.Span.Or */
interface span_or_query {
	Clauses: span_query[];
}
/**namespace:QueryDsl.Span.Not */
interface span_not_query {
	Include: span_query;
	Exclude: span_query;
	Pre: integer;
	Post: integer;
	Dist: integer;
}
/**namespace:QueryDsl.Span.Containing */
interface span_containing_query {
	Little: span_query;
	Big: span_query;
}
/**namespace:QueryDsl.Span.Within */
interface span_within_query {
	Little: span_query;
	Big: span_query;
}
/**namespace:QueryDsl.Span.MultiTerm */
interface span_multi_term_query {
	Match: query_container;
}
/**namespace:QueryDsl.Span.FieldMasking */
interface span_field_masking_query {
	Field: field;
	Query: span_query;
}
/**namespace:QueryDsl.Joining.Nested */
interface nested_query {
	ScoreMode: NestedScoreMode;
	Query: query_container;
	Path: field;
	InnerHits: inner_hits;
	IgnoreUnmapped: boolean;
}
/**namespace:QueryDsl.Compound.FunctionScore */
interface function_score_query {
	Query: query_container;
	Functions: score_function[];
	MaxBoost: double;
	ScoreMode: FunctionScoreMode;
	BoostMode: FunctionBoostMode;
	MinScore: double;
}
/**namespace:QueryDsl.Compound.FunctionScore.Functions */
interface score_function {
	Filter: query_container;
	Weight: double;
}
/**namespace:QueryDsl.Geo.BoundingBox */
interface geo_bounding_box_query {
	BoundingBox: bounding_box;
	Type: GeoExecution;
	ValidationMethod: GeoValidationMethod;
}
/**namespace:QueryDsl.Geo.BoundingBox */
interface bounding_box {
	TopLeft: geo_location;
	BottomRight: geo_location;
}
/**namespace:QueryDsl.Geo.Distance */
interface geo_distance_query {
	Location: geo_location;
	Distance: distance;
	DistanceType: GeoDistanceType;
	ValidationMethod: GeoValidationMethod;
}
/**namespace:QueryDsl.Geo.Polygon */
interface geo_polygon_query {
	Points: geo_location[];
	ValidationMethod: GeoValidationMethod;
}
/**namespace:QueryDsl.Specialized.Script */
interface script_query {
	Source: string;
	Inline: string;
	Id: id;
	Params: map<string, any>[];
	Lang: string;
}
/**namespace:QueryDsl.TermLevel.Exists */
interface exists_query {
	Field: field;
}
/**namespace:QueryDsl.TermLevel.Type */
interface type_query {
	Value: type_name;
}
/**namespace:QueryDsl.Specialized.Percolate */
interface percolate_query {
	Field: field;
	DocumentType: type_name;
	Document: any;
	Id: id;
	Index: index_name;
	Type: type_name;
	Routing: routing;
	Preference: string;
	Version: long;
}
/**namespace:Analysis.Tokenizers */
interface tokenizer {
	Version: string;
	Type: string;
}
/**namespace:Analysis.CharFilters */
interface char_filter {
	Version: string;
	Type: string;
}
/**namespace:Analysis.TokenFilters */
interface token_filter {
	Version: string;
	Type: string;
}
/**namespace:Indices.IndexManagement.RolloverIndex */
interface rollover_conditions {
	MaxAge: time;
	MaxDocs: long;
}
/**namespace:Mapping.MetaFields */
interface field_mapping {
}
/**namespace:Ingest */
interface pipeline {
	Description: string;
	Processors: processor[];
	OnFailure: processor[];
}
/**namespace:Ingest */
interface processor {
	Name: string;
	OnFailure: processor[];
}
/**namespace:Ingest.SimulatePipeline */
interface simulate_pipeline_document {
	Index: index_name;
	Type: type_name;
	Id: id;
	Source: any;
}
/**namespace:CommonAbstractions.LazyDocument */
interface lazy_document {
}
/**namespace:Modules.Indices.Fielddata.Numeric */
interface numeric_fielddata {
	Format: NumericFielddataFormat;
}
/**namespace:Modules.Indices.Fielddata */
interface fielddata_frequency_filter {
	Min: double;
	Max: double;
	MinSegmentSize: integer;
}
/**namespace:Search.Suggesters.ContextSuggester */
interface suggest_context {
	Name: string;
	Type: string;
	Path: field;
}
/**namespace:Modules.Indices.Fielddata.String */
interface string_fielddata {
	Format: StringFielddataFormat;
}
/**namespace:Modules.Indices.CircuitBreaker */
interface circuit_breaker_settings {
	TotalLimit: string;
	FielddataLimit: string;
	FielddataOverhead: float;
	RequestLimit: string;
	RequestOverhead: float;
}
/**namespace:Modules.Indices.Recovery */
interface indices_recovery_settings {
	ConcurrentStreams: integer;
	ConcurrentSmallFileStreams: integer;
	FileChunkSize: string;
	TranslogOperations: integer;
	TranslogSize: string;
	Compress: boolean;
	MaxBytesPerSecond: string;
}
/**namespace:Modules.Scripting */
interface stored_script {
	Lang: string;
	Source: string;
}
/**namespace:Modules.SnapshotAndRestore.Repositories */
interface snapshot_repository {
	Type: string;
}
/**namespace:Indices.IndexSettings.UpdateIndexSettings */
interface update_index_settings_request {
	IndexSettings: map<string, any>[];
	Index: indices;
}
/**namespace:Search.SearchTemplate */
interface search_template_request {
	Params: map<string, any>[];
	Source: string;
	Inline: string;
	Id: string;
	Index: indices;
	Type: types;
}
/**namespace:Search.Search */
interface search_request {
	Timeout: string;
	From: integer;
	Size: integer;
	Explain: boolean;
	Version: boolean;
	TrackScores: boolean;
	Profile: boolean;
	MinScore: double;
	TerminateAfter: long;
	IndicesBoost: map<index_name, double>[];
	Sort: sort[];
	SearchAfter: any[];
	Suggest: map<string, suggest_bucket>[];
	Highlight: highlight;
	Collapse: field_collapse;
	Rescore: rescore[];
	ScriptFields: map<string, script_field>[];
	Source: union<boolean, source_filter>;
	Aggregations: map<string, aggregation_container>[];
	Slice: sliced_scroll;
	Query: query_container;
	PostFilter: query_container;
	Index: indices;
	Type: types;
	StoredFields: field[];
	DocValueFields: field[];
}
/**namespace:Search.Suggesters */
interface suggest_bucket {
	Text: string;
	Prefix: string;
	Regex: string;
	Term: term_suggester;
	Phrase: phrase_suggester;
	Completion: completion_suggester;
}
/**namespace:Search.Suggesters.TermSuggester */
interface term_suggester {
	Text: string;
	ShardSize: integer;
	PrefixLength: integer;
	SuggestMode: SuggestMode;
	MinWordLength: integer;
	MaxEdits: integer;
	MaxInspections: integer;
	MinDocFrequency: double;
	MaxTermFrequency: double;
	Sort: SuggestSort;
	LowercaseTerms: boolean;
	StringDistance: StringDistance;
}
/**namespace:Search.Suggesters.PhraseSuggester */
interface phrase_suggester {
	Text: string;
	ShardSize: integer;
	GramSize: integer;
	RealWordErrorLikelihood: double;
	Confidence: double;
	MaxErrors: double;
	Separator: string;
	DirectGenerator: direct_generator[];
	Highlight: phrase_suggest_highlight;
	Collate: phrase_suggest_collate;
	Smoothing: smoothing_model_container;
}
/**namespace:Search.Suggesters.PhraseSuggester */
interface direct_generator {
	Field: field;
	Size: integer;
	PrefixLength: integer;
	SuggestMode: SuggestMode;
	MinWordLength: integer;
	MaxEdits: integer;
	MaxInspections: double;
	MinDocFrequency: double;
	MaxTermFrequency: double;
	PreFilter: string;
	PostFilter: string;
}
/**namespace:Search.Suggesters.PhraseSuggester */
interface phrase_suggest_highlight {
	PreTag: string;
	PostTag: string;
}
/**namespace:Search.Suggesters.PhraseSuggester */
interface phrase_suggest_collate {
	Query: phrase_suggest_collate_query;
	Prune: boolean;
	Params: map<string, any>[];
}
/**namespace:Search.Suggesters.PhraseSuggester */
interface phrase_suggest_collate_query {
	Source: string;
	Id: id;
}
/**namespace:Search.Suggesters.CompletionSuggester */
interface completion_suggester {
	Prefix: string;
	Regex: string;
	Fuzzy: fuzzy_suggester;
	Contexts: map<string, suggest_context_query[]>[];
}
/**namespace:Search.Suggesters.CompletionSuggester */
interface fuzzy_suggester {
	Transpositions: boolean;
	MinLength: integer;
	PrefixLength: integer;
	Fuzziness: fuzziness;
	UnicodeAware: boolean;
}
/**namespace:Search.Suggesters.ContextSuggester */
interface suggest_context_query {
	Context: context;
	Boost: double;
	Prefix: boolean;
	Precision: union<distance, integer>;
	Neighbours: union<distance[], integer[]>;
}
/**namespace:Search.Search.Collapsing */
interface field_collapse {
	Field: field;
	InnerHits: inner_hits;
	MaxConcurrentGroupSearches: integer;
}
/**namespace:Search.Search.Rescoring */
interface rescore {
	WindowSize: integer;
	Query: rescore_query;
}
/**namespace:Search.Search.Rescoring */
interface rescore_query {
	Query: query_container;
	QueryWeight: double;
	RescoreQueryWeight: double;
	ScoreMode: ScoreMode;
}
/**namespace:Aggregations */
interface aggregation_container {
	Meta: map<string, any>[];
	Average: average_aggregation;
	DateHistogram: date_histogram_aggregation;
	Percentiles: percentiles_aggregation;
	DateRange: date_range_aggregation;
	ExtendedStats: extended_stats_aggregation;
	Filter: filter_aggregation;
	Filters: filters_aggregation;
	GeoDistance: geo_distance_aggregation;
	GeoHash: geo_hash_grid_aggregation;
	GeoBounds: geo_bounds_aggregation;
	Histogram: histogram_aggregation;
	Global: global_aggregation;
	IpRange: ip_range_aggregation;
	Max: max_aggregation;
	Min: min_aggregation;
	Cardinality: cardinality_aggregation;
	Missing: missing_aggregation;
	Nested: nested_aggregation;
	ReverseNested: reverse_nested_aggregation;
	Range: range_aggregation;
	Stats: stats_aggregation;
	Sum: sum_aggregation;
	Terms: terms_aggregation;
	SignificantTerms: significant_terms_aggregation;
	ValueCount: value_count_aggregation;
	PercentileRanks: percentile_ranks_aggregation;
	TopHits: top_hits_aggregation;
	Children: children_aggregation;
	ScriptedMetric: scripted_metric_aggregation;
	AverageBucket: average_bucket_aggregation;
	Derivative: derivative_aggregation;
	MaxBucket: max_bucket_aggregation;
	MinBucket: min_bucket_aggregation;
	SumBucket: sum_bucket_aggregation;
	StatsBucket: stats_bucket_aggregation;
	ExtendedStatsBucket: extended_stats_bucket_aggregation;
	PercentilesBucket: percentiles_bucket_aggregation;
	MovingAverage: moving_average_aggregation;
	CumulativeSum: cumulative_sum_aggregation;
	SerialDifferencing: serial_differencing_aggregation;
	BucketScript: bucket_script_aggregation;
	BucketSelector: bucket_selector_aggregation;
	Sampler: sampler_aggregation;
	GeoCentroid: geo_centroid_aggregation;
	MatrixStats: matrix_stats_aggregation;
	AdjacencyMatrix: adjacency_matrix_aggregation;
	Aggregations: map<string, aggregation_container>[];
}
/**namespace:Aggregations.Metric.Average */
interface average_aggregation {
}
/**namespace:Aggregations.Bucket.DateHistogram */
interface date_histogram_aggregation {
	Field: field;
	Script: script;
	Params: map<string, any>[];
	Interval: union<DateInterval, time>;
	Format: string;
	MinimumDocumentCount: integer;
	TimeZone: string;
	Offset: string;
	Order: histogram_order;
	ExtendedBounds: extended_bounds<date_math>;
	Missing: Date;
}
/**namespace:Aggregations.Metric.Percentiles */
interface percentiles_aggregation {
	Percents: double[];
	Method: percentiles_method;
}
/**namespace:Aggregations.Metric.Percentiles.Methods */
interface percentiles_method {
}
/**namespace:Aggregations.Bucket.DateRange */
interface date_range_aggregation {
	Field: field;
	Format: string;
	Ranges: date_range_expression[];
	TimeZone: string;
}
/**namespace:Aggregations.Bucket.DateRange */
interface date_range_expression {
	From: date_math;
	To: date_math;
	Key: string;
}
/**namespace:Aggregations.Metric.ExtendedStats */
interface extended_stats_aggregation {
	Sigma: double;
}
/**namespace:Aggregations.Bucket.Filter */
interface filter_aggregation {
	Filter: query_container;
}
/**namespace:Aggregations.Bucket.Filters */
interface filters_aggregation {
	Filters: union<map<string, query_container>[], query_container[]>;
	OtherBucket: boolean;
	OtherBucketKey: string;
}
/**namespace:Aggregations.Bucket.GeoDistance */
interface geo_distance_aggregation {
	Field: field;
	Origin: geo_location;
	Unit: DistanceUnit;
	DistanceType: GeoDistanceType;
	Ranges: aggregation_range[];
}
/**namespace:CommonOptions.Range */
interface aggregation_range {
	From: double;
	To: double;
	Key: string;
}
/**namespace:Aggregations.Bucket.GeoHashGrid */
interface geo_hash_grid_aggregation {
	Field: field;
	Size: integer;
	ShardSize: integer;
	Precision: GeoHashPrecision;
}
/**namespace:Aggregations.Metric.GeoBounds */
interface geo_bounds_aggregation {
	WrapLongitude: boolean;
}
/**namespace:Aggregations.Bucket.Histogram */
interface histogram_aggregation {
	Field: field;
	Script: script;
	Interval: double;
	MinimumDocumentCount: integer;
	Order: histogram_order;
	ExtendedBounds: extended_bounds<double>;
	Offset: double;
	Missing: double;
}
/**namespace:Aggregations.Bucket.Global */
interface global_aggregation {
}
/**namespace:Aggregations.Bucket.IpRange */
interface ip_range_aggregation {
	Field: field;
	Ranges: ip_range[];
}
/**namespace:DefaultLanguageConstruct */
interface ip_range {
	From: string;
	To: string;
	Mask: string;
}
/**namespace:Aggregations.Metric.Max */
interface max_aggregation {
}
/**namespace:Aggregations.Metric.Min */
interface min_aggregation {
}
/**namespace:Aggregations.Metric.Cardinality */
interface cardinality_aggregation {
	PrecisionThreshold: integer;
	Rehash: boolean;
}
/**namespace:Aggregations.Bucket.Missing */
interface missing_aggregation {
	Field: field;
}
/**namespace:Aggregations.Bucket.Nested */
interface nested_aggregation {
	Path: field;
}
/**namespace:Aggregations.Bucket.ReverseNested */
interface reverse_nested_aggregation {
	Path: field;
}
/**namespace:Aggregations.Bucket.Range */
interface range_aggregation {
	Field: field;
	Script: script;
	Ranges: aggregation_range[];
}
/**namespace:Aggregations.Metric.Stats */
interface stats_aggregation {
}
/**namespace:Aggregations.Metric.Sum */
interface sum_aggregation {
}
/**namespace:Aggregations.Bucket.Terms */
interface terms_aggregation {
	Field: field;
	Script: script;
	Size: integer;
	ShardSize: integer;
	MinimumDocumentCount: integer;
	ExecutionHint: TermsAggregationExecutionHint;
	Order: terms_order[];
	Include: terms_include;
	Exclude: terms_exclude;
	CollectMode: TermsAggregationCollectMode;
	ShowTermDocCountError: boolean;
	Missing: any;
}
/**namespace:Aggregations.Bucket.SignificantTerms */
interface significant_terms_aggregation {
	Field: field;
	Size: integer;
	ShardSize: integer;
	MinimumDocumentCount: long;
	ShardMinimumDocumentCount: long;
	ExecutionHint: TermsAggregationExecutionHint;
	Include: significant_terms_include_exclude;
	Exclude: significant_terms_include_exclude;
	MutualInformation: mutual_information_heuristic;
	ChiSquare: chi_square_heuristic;
	GoogleNormalizedDistance: google_normalized_distance_heuristic;
	PercentageScore: percentage_score_heuristic;
	Script: scripted_heuristic;
	BackgroundFilter: query_container;
}
/**namespace:Aggregations.Bucket.SignificantTerms.Heuristics */
interface mutual_information_heuristic {
	IncludeNegatives: boolean;
	BackgroundIsSuperSet: boolean;
}
/**namespace:Aggregations.Bucket.SignificantTerms.Heuristics */
interface chi_square_heuristic {
	IncludeNegatives: boolean;
	BackgroundIsSuperSet: boolean;
}
/**namespace:Aggregations.Bucket.SignificantTerms.Heuristics */
interface google_normalized_distance_heuristic {
	BackgroundIsSuperSet: boolean;
}
/**namespace:Aggregations.Bucket.SignificantTerms.Heuristics */
interface percentage_score_heuristic {
}
/**namespace:Aggregations.Bucket.SignificantTerms.Heuristics */
interface scripted_heuristic {
	Script: script;
}
/**namespace:Aggregations.Metric.ValueCount */
interface value_count_aggregation {
}
/**namespace:Aggregations.Metric.PercentileRanks */
interface percentile_ranks_aggregation {
	Values: double[];
	Method: percentiles_method;
}
/**namespace:Aggregations.Metric.TopHits */
interface top_hits_aggregation {
	From: integer;
	Size: integer;
	Sort: sort[];
	Source: union<boolean, source_filter>;
	Highlight: highlight;
	Explain: boolean;
	ScriptFields: map<string, script_field>[];
	StoredFields: field[];
	Version: boolean;
	TrackScores: boolean;
}
/**namespace:Aggregations.Bucket.Children */
interface children_aggregation {
	Type: relation_name;
}
/**namespace:Aggregations.Metric.ScriptedMetric */
interface scripted_metric_aggregation {
	InitScript: script;
	MapScript: script;
	CombineScript: script;
	ReduceScript: script;
	Params: map<string, any>[];
}
/**namespace:Aggregations.Pipeline.AverageBucket */
interface average_bucket_aggregation {
}
/**namespace:Aggregations.Pipeline.Derivative */
interface derivative_aggregation {
}
/**namespace:Aggregations.Pipeline.MaxBucket */
interface max_bucket_aggregation {
}
/**namespace:Aggregations.Pipeline.MinBucket */
interface min_bucket_aggregation {
}
/**namespace:Aggregations.Pipeline.SumBucket */
interface sum_bucket_aggregation {
}
/**namespace:Aggregations.Pipeline.StatsBucket */
interface stats_bucket_aggregation {
}
/**namespace:Aggregations.Pipeline.ExtendedStatsBucket */
interface extended_stats_bucket_aggregation {
	Sigma: double;
}
/**namespace:Aggregations.Pipeline.PercentilesBucket */
interface percentiles_bucket_aggregation {
	Percents: double[];
}
/**namespace:Aggregations.Pipeline.MovingAverage */
interface moving_average_aggregation {
	Model: moving_average_model;
	Window: integer;
	Minimize: boolean;
	Predict: integer;
}
/**namespace:Aggregations.Pipeline.MovingAverage.Models */
interface moving_average_model {
	Name: string;
}
/**namespace:Aggregations.Pipeline.CumulativeSum */
interface cumulative_sum_aggregation {
}
/**namespace:Aggregations.Pipeline.SerialDifferencing */
interface serial_differencing_aggregation {
	Lag: integer;
}
/**namespace:Aggregations.Pipeline.BucketScript */
interface bucket_script_aggregation {
	Script: script;
}
/**namespace:Aggregations.Pipeline.BucketSelector */
interface bucket_selector_aggregation {
	Script: script;
}
/**namespace:Aggregations.Bucket.Sampler */
interface sampler_aggregation {
	ShardSize: integer;
	MaxDocsPerValue: integer;
	Script: script;
	ExecutionHint: SamplerAggregationExecutionHint;
}
/**namespace:Aggregations.Metric.GeoCentroid */
interface geo_centroid_aggregation {
}
/**namespace:Aggregations.Matrix.MatrixStats */
interface matrix_stats_aggregation {
	Mode: MatrixStatsMode;
}
/**namespace:Aggregations.Bucket.AdjacencyMatrix */
interface adjacency_matrix_aggregation {
	Filters: map<string, query_container>[];
}
/**namespace:CommonAbstractions.Response */
interface response {
	ServerError: server_error;
}
/**namespace:Aggregations */
interface aggregate {
	Meta: map<string, any>[];
}
/**namespace:Search.Search.Hits */
interface hit<t_document> {
	Score: double;
	Fields: map<string, lazy_document>[];
	Sorts: any[];
	Highlights: map<string, highlight_hit>[];
	Explanation: explanation;
	MatchedQueries: string[];
	InnerHits: map<string, inner_hits_result>[];
}
/**namespace:XPack.Graph.Explore.Request */
interface graph_vertex_definition {
	Field: field;
	Size: integer;
	MinimumDocumentCount: long;
	ShardMinimumDocumentCount: long;
	Exclude: string[];
	Include: graph_vertex_include[];
}
/**namespace:XPack.Graph.Explore.Request */
interface hop {
	Query: query_container;
	Vertices: graph_vertex_definition[];
	Connections: hop;
}
/**namespace:XPack.Graph.Explore.Request */
interface graph_explore_controls {
	UseSignificance: boolean;
	SampleSize: integer;
	Timeout: time;
	SampleDiversity: sample_diversity;
}
/**namespace:XPack.MachineLearning.Job */
interface page {
	From: integer;
	Size: integer;
}
/**namespace:XPack.MachineLearning.Datafeed */
interface chunking_config {
	Mode: ChunkingMode;
	TimeSpan: time;
}
/**namespace:XPack.MachineLearning.Job.Config */
interface analysis_config {
	BucketSpan: time;
	CategorizationFieldName: field;
	CategorizationFilters: string[];
	Detectors: detector[];
	Influencers: field[];
	Latency: time;
	MultivariateByFields: boolean;
	SummaryCountFieldName: field;
}
/**namespace:XPack.MachineLearning.Job.Detectors */
interface detector {
	DetectorDescription: string;
	ExcludeFrequent: ExcludeFrequent;
	Function: string;
	UseNull: boolean;
	DetectorIndex: integer;
}
/**namespace:XPack.MachineLearning.Job.Config */
interface analysis_limits {
	CategorizationExamplesLimit: long;
	ModelMemoryLimit: string;
}
/**namespace:XPack.MachineLearning.Job.Config */
interface data_description {
	Format: string;
	TimeField: field;
	TimeFormat: string;
}
/**namespace:XPack.MachineLearning.Job.Config */
interface model_plot_config {
	Terms: field[];
}
/**namespace:XPack.MachineLearning.Job.Config */
interface analysis_memory_limit {
	ModelMemoryLimit: string;
}
/**namespace:XPack.MachineLearning.Job.Config */
interface model_plot_config_enabled {
	Enabled: boolean;
}
/**namespace:XPack.Security.Role.PutRole */
interface indices_privileges {
	Names: indices;
	Privileges: string[];
	FieldSecurity: field_security;
	Query: query_container;
}
/**namespace:XPack.Security.Role */
interface field_security {
	Grant: field[];
	Except: field[];
}
/**namespace:XPack.Watcher.Schedule */
interface schedule_trigger_event {
	TriggeredTime: union<Date, string>;
	ScheduledTime: union<Date, string>;
}
/**namespace:XPack.Watcher.PutWatch */
interface put_watch_request {
	Trigger: trigger_container;
	Input: input_container;
	Condition: condition_container;
	Actions: map<string, action>[];
	Metadata: map<string, any>[];
	ThrottlePeriod: string;
	Transform: transform_container;
	Id: id;
}
/**namespace:XPack.Watcher.Action */
interface action {
	Name: string;
	ActionType: ActionType;
	Transform: transform_container;
	ThrottlePeriod: time;
}
/**namespace:XPack.Watcher.Trigger */
interface trigger_event_container {
	Schedule: schedule_trigger_event;
}
/**namespace:XPack.Watcher.Input */
interface http_input_authentication {
	Basic: http_input_basic_authentication;
}
/**namespace:XPack.Watcher.Input */
interface http_input_basic_authentication {
	Username: string;
	Password: string;
}
/**namespace:XPack.Watcher.Input */
interface http_input_proxy {
	Host: string;
	Port: integer;
}
/**namespace:XPack.Watcher.Action.PagerDuty */
interface pager_duty_context {
	Type: PagerDutyContextType;
	Href: string;
	Src: string;
}
/**namespace:XPack.Watcher.Action.HipChat */
interface hip_chat_message {
	Body: string;
	Format: HipChatMessageFormat;
	Color: HipChatMessageColor;
	Notify: boolean;
	From: string;
	Room: string[];
	User: string[];
}
/**namespace:XPack.Watcher.Action.Slack */
interface slack_message {
	From: string;
	To: string[];
	Icon: string;
	Text: string;
	Attachments: slack_attachment[];
	DynamicAttachments: slack_dynamic_attachment;
}
/**namespace:XPack.Watcher.Action.Slack */
interface slack_attachment {
	Fallback: string;
	Color: string;
	Pretext: string;
	AuthorName: string;
	AuthorLink: string;
	AuthorIcon: string;
	Title: string;
	TitleLink: string;
	Text: string;
	Fields: slack_attachment_field[];
	ImageUrl: string;
	ThumbUrl: string;
	Footer: string;
	FooterIcon: string;
	Ts: Date;
}
/**namespace:XPack.Watcher.Action.Slack */
interface slack_attachment_field {
	Title: string;
	Value: string;
	Short: boolean;
}
/**namespace:XPack.Watcher.Action.Slack */
interface slack_dynamic_attachment {
	ListPath: string;
	AttachmentTemplate: slack_attachment;
}
/**namespace:XPack.Watcher.Input */
interface input_container {
	Http: http_input;
	Search: search_input;
	Simple: simple_input;
	Chain: chain_input;
}
/**namespace:XPack.Watcher.Input */
interface http_input {
	Extract: string[];
	Request: http_input_request;
	ResponseContentType: ResponseContentType;
}
/**namespace:XPack.Watcher.Input */
interface http_input_request {
	Scheme: ConnectionScheme;
	Port: integer;
	Host: string;
	Path: string;
	Method: HttpInputMethod;
	Headers: map<string, string>[];
	Params: map<string, string>[];
	Url: string;
	Authentication: http_input_authentication;
	Proxy: http_input_proxy;
	ConnectionTimeout: time;
	ReadTimeout: time;
	Body: string;
}
/**namespace:XPack.Watcher.Input */
interface search_input {
	Extract: string[];
	Request: search_input_request;
	Timeout: time;
}
/**namespace:XPack.Watcher.Input */
interface search_input_request {
	Indices: index_name[];
	Types: type_name[];
	SearchType: SearchType;
	IndicesOptions: indices_options;
	Body: search_request;
	Template: search_template_request;
}
/**namespace:XPack.Watcher.Input */
interface indices_options {
	ExpandWildcards: ExpandWildcards;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
}
/**namespace:XPack.Watcher.Input */
interface simple_input {
	Payload: map<string, any>[];
}
/**namespace:XPack.Watcher.Input */
interface chain_input {
	Inputs: map<string, input_container>[];
}
/**namespace:XPack.Watcher.Condition */
interface condition_container {
	Always: always_condition;
	Never: never_condition;
	Compare: compare_condition;
	ArrayCompare: array_compare_condition;
	Script: script_condition;
}
/**namespace:XPack.Watcher.Condition */
interface always_condition {
}
/**namespace:XPack.Watcher.Condition */
interface never_condition {
}
/**namespace:XPack.Watcher.Condition */
interface compare_condition {
	Path: string;
	Comparison: string;
	Value: any;
}
/**namespace:XPack.Watcher.Condition */
interface array_compare_condition {
	ArrayPath: string;
	Path: string;
	Comparison: string;
	Value: any;
	Quantifier: Quantifier;
}
/**namespace:XPack.Watcher.Condition */
interface script_condition {
	Lang: string;
	Params: map<string, any>[];
}
/**namespace:XPack.Watcher.Trigger */
interface trigger_container {
	Schedule: schedule_container;
}
/**namespace:XPack.Watcher.Schedule */
interface schedule_container {
	Hourly: hourly_schedule;
	Daily: daily_schedule;
	Weekly: time_of_week[];
	Monthly: time_of_month[];
	Yearly: time_of_year[];
	Cron: cron_expression;
	Interval: interval;
}
/**namespace:XPack.Watcher.Schedule */
interface hourly_schedule {
	Minute: integer[];
}
/**namespace:XPack.Watcher.Schedule */
interface daily_schedule {
	At: union<string[], time_of_day>;
}
/**namespace:XPack.Watcher.Schedule */
interface time_of_day {
	Hour: integer[];
	Minute: integer[];
}
/**namespace:XPack.Watcher.Schedule */
interface time_of_week {
	On: Day[];
	At: string[];
}
/**namespace:XPack.Watcher.Schedule */
interface time_of_month {
	On: integer[];
	At: string[];
}
/**namespace:XPack.Watcher.Schedule */
interface time_of_year {
	In: Month[];
	On: integer[];
	At: string[];
}
/**namespace:XPack.Watcher.Transform */
interface transform_container {
	Search: search_transform;
	Script: script_transform;
	Chain: chain_transform;
}
/**namespace:XPack.Watcher.Transform */
interface search_transform {
	Request: search_input_request;
	Timeout: time;
}
/**namespace:XPack.Watcher.Transform */
interface script_transform {
	Params: map<string, any>[];
	Lang: string;
}
/**namespace:XPack.Watcher.Transform */
interface chain_transform {
	Transforms: transform_container[];
}
/**namespace:Analysis.Analyzers */
interface analyzer_base {
	Version: string;
	Type: string;
}
/**namespace:Analysis.Analyzers */
interface custom_analyzer extends analyzer_base {
	Tokenizer: string;
	Filter: string[];
	CharFilter: string[];
	PositionOffsetGap: integer;
}
/**namespace:Analysis.Analyzers */
interface fingerprint_analyzer extends analyzer_base {
	Separator: string;
	MaxOutputSize: integer;
	PreserveOriginal: boolean;
	StopWords: stop_words;
	StopWordsPath: string;
}
/**namespace:Analysis */
interface stop_words extends union<string, string[]> {
}
/**namespace:CommonAbstractions.Union */
interface union<t_first, t_second> {
}
/**namespace:Analysis.Analyzers */
interface keyword_analyzer extends analyzer_base {
}
/**namespace:Analysis.Analyzers */
interface language_analyzer extends analyzer_base {
	Type: string;
	StopWords: stop_words;
	StemExclusionList: string[];
	Language: Language;
	StopwordsPath: string;
}
/**namespace:Analysis.Analyzers */
interface pattern_analyzer extends analyzer_base {
	Lowercase: boolean;
	Pattern: string;
	Flags: string;
	StopWords: stop_words;
}
/**namespace:Analysis.Analyzers */
interface simple_analyzer extends analyzer_base {
}
/**namespace:Analysis.Analyzers */
interface snowball_analyzer extends analyzer_base {
	Language: SnowballLanguage;
	StopWords: stop_words;
}
/**namespace:Analysis.Analyzers */
interface standard_analyzer extends analyzer_base {
	StopWords: stop_words;
	MaxTokenLength: integer;
}
/**namespace:Analysis.Analyzers */
interface stop_analyzer extends analyzer_base {
	StopWords: stop_words;
	StopwordsPath: string;
}
/**namespace:Analysis.Analyzers */
interface whitespace_analyzer extends analyzer_base {
}
/**namespace:Analysis.CharFilters */
interface char_filter_base {
	Version: string;
	Type: string;
}
/**namespace:Analysis.CharFilters */
interface html_strip_char_filter extends char_filter_base {
}
/**namespace:Analysis.CharFilters */
interface mapping_char_filter extends char_filter_base {
	Mappings: string[];
	MappingsPath: string;
}
/**namespace:Analysis.CharFilters */
interface pattern_replace_char_filter extends char_filter_base {
	Pattern: string;
	Replacement: string;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_collation_token_filter extends token_filter_base {
	Language: string;
	Country: string;
	Variant: string;
	Strength: IcuCollationStrength;
	Decomposition: IcuCollationDecomposition;
	Alternate: IcuCollationAlternate;
	CaseLevel: boolean;
	CaseFirst: IcuCollationCaseFirst;
	Numeric: boolean;
	VariableTop: string;
	HiraganaQuaternaryMode: boolean;
}
/**namespace:Analysis.TokenFilters */
interface token_filter_base {
	Version: string;
	Type: string;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_folding_token_filter extends token_filter_base {
	UnicodeSetFilter: string;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_normalization_char_filter extends char_filter_base {
	Name: IcuNormalizationType;
	Mode: IcuNormalizationMode;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_normalization_token_filter extends token_filter_base {
	Name: IcuNormalizationType;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_tokenizer extends tokenizer_base {
	RuleFiles: string;
}
/**namespace:Analysis.Tokenizers */
interface tokenizer_base {
	Version: string;
	Type: string;
}
/**namespace:Analysis.Plugins.Icu */
interface icu_transform_token_filter extends token_filter_base {
	Direction: IcuTransformDirection;
	Id: string;
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_analyzer extends analyzer_base {
	Mode: KuromojiTokenizationMode;
	UserDictionary: string;
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_iteration_mark_char_filter extends char_filter_base {
	NormalizeKanji: boolean;
	NormalizeKana: boolean;
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_part_of_speech_token_filter extends token_filter_base {
	StopTags: string[];
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_reading_form_token_filter extends token_filter_base {
	UseRomaji: boolean;
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_stemmer_token_filter extends token_filter_base {
	MinimumLength: integer;
}
/**namespace:Analysis.Plugins.Kuromoji */
interface kuromoji_tokenizer extends tokenizer_base {
	Mode: KuromojiTokenizationMode;
	DiscardPunctuation: boolean;
	UserDictionary: string;
	NBestExamples: string;
	NBestCost: integer;
}
/**namespace:Analysis.Plugins.Phonetic */
interface phonetic_token_filter extends token_filter_base {
	Encoder: PhoneticEncoder;
	Replace: boolean;
}
/**namespace:Analysis.TokenFilters */
interface ascii_folding_token_filter extends token_filter_base {
	PreserveOriginal: boolean;
}
/**namespace:Analysis.TokenFilters */
interface common_grams_token_filter extends token_filter_base {
	CommonWords: string[];
	CommonWordsPath: string;
	IgnoreCase: boolean;
	QueryMode: boolean;
}
/**namespace:Analysis.TokenFilters.CompoundWord */
interface compound_word_token_filter_base extends token_filter_base {
	WordList: string[];
	WordListPath: string;
	MinWordSize: integer;
	MinSubwordSize: integer;
	MaxSubwordSize: integer;
	OnlyLongestMatch: boolean;
	HyphenationPatternsPath: string;
}
/**namespace:Analysis.TokenFilters.CompoundWord */
interface dictionary_decompounder_token_filter extends compound_word_token_filter_base {
}
/**namespace:Analysis.TokenFilters.CompoundWord */
interface hyphenation_decompounder_token_filter extends compound_word_token_filter_base {
}
/**namespace:Analysis.TokenFilters.DelimitedPayload */
interface delimited_payload_token_filter extends token_filter_base {
	Delimiter: string;
	Encoding: DelimitedPayloadEncoding;
}
/**namespace:Analysis.TokenFilters.EdgeNGram */
interface edge_n_gram_token_filter extends token_filter_base {
	MinGram: integer;
	MaxGram: integer;
	Side: EdgeNGramSide;
}
/**namespace:Analysis.TokenFilters */
interface elision_token_filter extends token_filter_base {
	Articles: string[];
}
/**namespace:Analysis.TokenFilters */
interface fingerprint_token_filter extends token_filter_base {
	Separator: string;
	MaxOutputSize: integer;
}
/**namespace:Analysis.TokenFilters */
interface hunspell_token_filter extends token_filter_base {
	Locale: string;
	Dictionary: string;
	Dedup: boolean;
	LongestOnly: boolean;
}
/**namespace:Analysis.TokenFilters */
interface keep_types_token_filter extends token_filter_base {
	Types: string[];
}
/**namespace:Analysis.TokenFilters */
interface keep_words_token_filter extends token_filter_base {
	KeepWords: string[];
	KeepWordsPath: string;
	KeepWordsCase: boolean;
}
/**namespace:Analysis.TokenFilters */
interface keyword_marker_token_filter extends token_filter_base {
	Keywords: string[];
	KeywordsPath: string;
	IgnoreCase: boolean;
}
/**namespace:Analysis.TokenFilters */
interface k_stem_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters */
interface length_token_filter extends token_filter_base {
	Min: integer;
	Max: integer;
}
/**namespace:Analysis.TokenFilters */
interface limit_token_count_token_filter extends token_filter_base {
	MaxTokenCount: integer;
	ConsumeAllTokens: boolean;
}
/**namespace:Analysis.TokenFilters */
interface lowercase_token_filter extends token_filter_base {
	Language: string;
}
/**namespace:Analysis.TokenFilters */
interface n_gram_token_filter extends token_filter_base {
	MinGram: integer;
	MaxGram: integer;
}
/**namespace:Analysis.TokenFilters */
interface pattern_capture_token_filter extends token_filter_base {
	Patterns: string[];
	PreserveOriginal: boolean;
}
/**namespace:Analysis.TokenFilters */
interface pattern_replace_token_filter extends token_filter_base {
	Pattern: string;
	Replacement: string;
}
/**namespace:Analysis.TokenFilters */
interface porter_stem_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters */
interface reverse_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters.Shingle */
interface shingle_token_filter extends token_filter_base {
	MinShingleSize: integer;
	MaxShingleSize: integer;
	OutputUnigrams: boolean;
	OutputUnigramsIfNoShingles: boolean;
	TokenSeparator: string;
	FillerToken: string;
}
/**namespace:Analysis.TokenFilters */
interface snowball_token_filter extends token_filter_base {
	Language: SnowballLanguage;
}
/**namespace:Analysis.TokenFilters */
interface standard_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters */
interface stemmer_override_token_filter extends token_filter_base {
	Rules: string[];
	RulesPath: string;
}
/**namespace:Analysis.TokenFilters */
interface stemmer_token_filter extends token_filter_base {
	Language: string;
}
/**namespace:Analysis.TokenFilters.Stop */
interface stop_token_filter extends token_filter_base {
	StopWords: stop_words;
	IgnoreCase: boolean;
	StopWordsPath: string;
	RemoveTrailing: boolean;
}
/**namespace:Analysis.TokenFilters.Synonym */
interface synonym_graph_token_filter extends token_filter_base {
	SynonymsPath: string;
	Format: SynonymFormat;
	Synonyms: string[];
	IgnoreCase: boolean;
	Expand: boolean;
	Tokenizer: string;
}
/**namespace:Analysis.TokenFilters.Synonym */
interface synonym_token_filter extends token_filter_base {
	SynonymsPath: string;
	Format: SynonymFormat;
	Synonyms: string[];
	IgnoreCase: boolean;
	Expand: boolean;
	Tokenizer: string;
}
/**namespace:Analysis.TokenFilters */
interface trim_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters */
interface truncate_token_filter extends token_filter_base {
	Length: integer;
}
/**namespace:Analysis.TokenFilters */
interface unique_token_filter extends token_filter_base {
	OnlyOnSamePosition: boolean;
}
/**namespace:Analysis.TokenFilters */
interface uppercase_token_filter extends token_filter_base {
}
/**namespace:Analysis.TokenFilters.WordDelimiterGraph */
interface word_delimiter_graph_token_filter extends token_filter_base {
	GenerateWordParts: boolean;
	GenerateNumberParts: boolean;
	CatenateWords: boolean;
	CatenateNumbers: boolean;
	CatenateAll: boolean;
	SplitOnCaseChange: boolean;
	PreserveOriginal: boolean;
	SplitOnNumerics: boolean;
	StemEnglishPossessive: boolean;
	ProtectedWords: string[];
	ProtectedWordsPath: string;
	TypeTable: string[];
	TypeTablePath: string;
}
/**namespace:Analysis.TokenFilters.WordDelimiter */
interface word_delimiter_token_filter extends token_filter_base {
	GenerateWordParts: boolean;
	GenerateNumberParts: boolean;
	CatenateWords: boolean;
	CatenateNumbers: boolean;
	CatenateAll: boolean;
	SplitOnCaseChange: boolean;
	PreserveOriginal: boolean;
	SplitOnNumerics: boolean;
	StemEnglishPossessive: boolean;
	ProtectedWords: string[];
	ProtectedWordsPath: string;
	TypeTable: string[];
	TypeTablePath: string;
}
/**namespace:Analysis.Tokenizers */
interface keyword_tokenizer extends tokenizer_base {
	BufferSize: integer;
}
/**namespace:Analysis.Tokenizers */
interface letter_tokenizer extends tokenizer_base {
}
/**namespace:Analysis.Tokenizers */
interface lowercase_tokenizer extends tokenizer_base {
}
/**namespace:Analysis.Tokenizers.NGram */
interface edge_n_gram_tokenizer extends tokenizer_base {
	MinGram: integer;
	MaxGram: integer;
	TokenChars: TokenChar[];
}
/**namespace:Analysis.Tokenizers.NGram */
interface n_gram_tokenizer extends tokenizer_base {
	MinGram: integer;
	MaxGram: integer;
	TokenChars: TokenChar[];
}
/**namespace:Analysis.Tokenizers */
interface path_hierarchy_tokenizer extends tokenizer_base {
	Delimiter: string;
	Replacement: string;
	BufferSize: integer;
	Reverse: boolean;
	Skip: integer;
}
/**namespace:Analysis.Tokenizers */
interface pattern_tokenizer extends tokenizer_base {
	Pattern: string;
	Flags: string;
	Group: integer;
}
/**namespace:Analysis.Tokenizers */
interface standard_tokenizer extends tokenizer_base {
	MaxTokenLength: integer;
}
/**namespace:Analysis.Tokenizers */
interface uax_email_url_tokenizer extends tokenizer_base {
	MaxTokenLength: integer;
}
/**namespace:Analysis.Tokenizers */
interface whitespace_tokenizer extends tokenizer_base {
}
/**namespace:Cat.CatAliases */
interface cat_aliases_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:CommonAbstractions.Request */
interface plain_request_base<t_parameters> extends request {
	RequestConfiguration: request_configuration;
	Pretty: boolean;
	Human: boolean;
	ErrorTrace: boolean;
	FilterPath: string[];
}
/**namespace:DefaultLanguageConstruct */
interface request {
}
/**namespace:CommonOptions.TimeUnit */
interface time {
	Factor: double;
	Interval: TimeUnit;
	Milliseconds: double;
	MinusOne: time;
	Zero: time;
}
/**namespace:Cat.CatAllocation */
interface cat_allocation_request extends request {
	Format: string;
	Bytes: Bytes;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatCount */
interface cat_count_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatFielddata */
interface cat_fielddata_request extends request {
	Format: string;
	Bytes: Bytes;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatHealth */
interface cat_health_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	IncludeTimestamp: boolean;
	Verbose: boolean;
}
/**namespace:Cat.CatHelp */
interface cat_help_request extends request {
	Help: boolean;
	SortByColumns: string[];
}
/**namespace:Cat.CatIndices */
interface cat_indices_request extends request {
	Format: string;
	Bytes: Bytes;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Health: Health;
	Help: boolean;
	Pri: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatMaster */
interface cat_master_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatNodeAttributes */
interface cat_node_attributes_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatNodes */
interface cat_nodes_request extends request {
	Format: string;
	FullId: boolean;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatPendingTasks */
interface cat_pending_tasks_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatPlugins */
interface cat_plugins_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatRecovery */
interface cat_recovery_request extends request {
	Format: string;
	Bytes: Bytes;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatRepositories */
interface cat_repositories_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat */
interface cat_response<t_cat_record> extends response {
	Records: t_cat_record[];
}
/**namespace:Cat.CatSegments */
interface cat_segments_request extends request {
	Format: string;
	Bytes: Bytes;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatShards */
interface cat_shards_request extends request {
	Format: string;
	Bytes: Bytes;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatSnapshots */
interface cat_snapshots_request extends request {
	Format: string;
	IgnoreUnavailable: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatTasks */
interface cat_tasks_request extends request {
	Format: string;
	NodeId: string[];
	Actions: string[];
	Detailed: boolean;
	ParentNode: string;
	ParentTask: long;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatTemplates */
interface cat_templates_request extends request {
	Format: string;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cat.CatThreadPool */
interface cat_thread_pool_request extends request {
	Format: string;
	Size: Size;
	Local: boolean;
	MasterTimeout: time;
	Headers: string[];
	Help: boolean;
	SortByColumns: string[];
	Verbose: boolean;
}
/**namespace:Cluster.ClusterAllocationExplain */
interface cluster_allocation_explain_request extends request {
	Index: index_name;
	Shard: integer;
	Primary: boolean;
	IncludeYesDecisions: boolean;
	IncludeDiskInfo: boolean;
}
/**namespace:CommonAbstractions.Infer.IndexName */
interface index_name {
	Cluster: string;
	Name: string;
}
/**namespace:Cluster.ClusterAllocationExplain */
interface cluster_allocation_explain_response extends response {
	Index: string;
	Shard: integer;
	Primary: boolean;
	CurrentState: string;
	UnassignedInformation: unassigned_information;
	CanAllocate: Decision;
	AllocateExplanation: string;
	ConfiguredDelay: string;
	ConfiguredDelayInMilliseconds: long;
	CurrentNode: current_node;
	CanRemainOnCurrentNode: Decision;
	CanRemainDecisions: allocation_decision[];
	CanRebalanceCluster: Decision;
	CanRebalanceToOtherNode: Decision;
	CanRebalanceClusterDecisions: allocation_decision[];
	RebalanceExplanation: string;
	NodeAllocationDecisions: node_allocation_explanation[];
	CanMoveToOtherNode: Decision;
	MoveExplanation: string;
	AllocationDelay: string;
	AllocationDelayInMilliseconds: long;
	RemainingDelay: string;
	RemainingDelayInMilliseconds: long;
}
/**namespace:Cluster.ClusterAllocationExplain */
interface unassigned_information {
	Reason: UnassignedInformationReason;
	At: Date;
	LastAllocationStatus: string;
}
/**namespace:Cluster.ClusterAllocationExplain */
interface current_node {
	Id: string;
	Name: string;
	TransportAddress: string;
	WeightRanking: string;
	NodeAttributes: map<string, string>[];
}
/**namespace:Cluster.ClusterAllocationExplain */
interface allocation_decision {
	Decider: string;
	Decision: AllocationExplainDecision;
	Explanation: string;
}
/**namespace:Cluster.ClusterAllocationExplain */
interface node_allocation_explanation {
	NodeId: string;
	NodeName: string;
	TransportAddress: string;
	NodeDecision: Decision;
	NodeAttributes: map<string, string>[];
	Store: allocation_store;
	WeightRanking: integer;
	Deciders: allocation_decision[];
}
/**namespace:Cluster.ClusterAllocationExplain */
interface allocation_store {
	Found: boolean;
	InSync: boolean;
	AllocationId: string;
	MatchingSyncId: boolean;
	MatchingSizeInBytes: long;
	StoreException: string;
}
/**namespace:Cluster.ClusterHealth */
interface cluster_health_request extends request {
	Level: Level;
	Local: boolean;
	MasterTimeout: time;
	Timeout: time;
	WaitForActiveShards: string;
	WaitForNodes: string;
	WaitForEvents: WaitForEvents;
	WaitForNoRelocatingShards: boolean;
	WaitForStatus: WaitForStatus;
}
/**namespace:Cluster.ClusterHealth */
interface cluster_health_response extends response {
	ClusterName: string;
	Status: Health;
	TimedOut: boolean;
	NumberOfNodes: integer;
	NumberOfDataNodes: integer;
	ActivePrimaryShards: integer;
	ActiveShards: integer;
	RelocatingShards: integer;
	InitializingShards: integer;
	UnassignedShards: integer;
	NumberOfPendingTasks: integer;
	Indices: map<index_name, index_health_stats>[];
}
/**namespace:Cluster.ClusterHealth */
interface index_health_stats {
	Status: Health;
	NumberOfShards: integer;
	NumberOfReplicas: integer;
	ActivePrimaryShards: integer;
	ActiveShards: integer;
	RelocatingShards: integer;
	InitializingShards: integer;
	UnassignedShards: integer;
	Shards: map<string, shard_health_stats>[];
}
/**namespace:Cluster.ClusterHealth */
interface shard_health_stats {
	Status: Health;
	PrimaryActive: boolean;
	ActiveShards: integer;
	RelocatingShards: integer;
	InitializingShards: integer;
	UnassignedShards: integer;
}
/**namespace:Cluster.ClusterPendingTasks */
interface cluster_pending_tasks_request extends request {
	Local: boolean;
	MasterTimeout: time;
}
/**namespace:Cluster.ClusterPendingTasks */
interface cluster_pending_tasks_response extends response {
	Tasks: pending_task[];
}
/**namespace:Cluster.ClusterPendingTasks */
interface pending_task {
	InsertOrder: integer;
	Priority: string;
	Source: string;
	TimeInQueueMilliseconds: integer;
	TimeInQueue: string;
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_request extends request {
	Commands: cluster_reroute_command[];
	DryRun: boolean;
	Explain: boolean;
	RetryFailed: boolean;
	Metric: string[];
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_response extends response {
	State: cluster_reroute_state;
	Explanations: cluster_reroute_explanation[];
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_state {
	Version: integer;
	MasterNode: string;
	Blocks: block_state;
	Nodes: map<string, node_state>[];
	RoutingTable: routing_table_state;
	RoutingNodes: routing_nodes_state;
}
/**namespace:Cluster.ClusterState */
interface block_state {
	ReadOnly: boolean;
}
/**namespace:Cluster.ClusterState */
interface node_state {
	Name: string;
	TransportAddress: string;
	Attributes: map<string, string>[];
}
/**namespace:Cluster.ClusterState */
interface routing_table_state {
	Indices: map<string, index_routing_table>[];
}
/**namespace:Cluster.ClusterState */
interface index_routing_table {
	Shards: map<string, routing_shard[]>[];
}
/**namespace:Cluster.ClusterState */
interface routing_shard {
	AllocationId: allocation_id;
	State: string;
	Primary: boolean;
	Node: string;
	RelocatingNode: string;
	Shard: integer;
	Index: string;
}
/**namespace:Cluster.ClusterState */
interface allocation_id {
	Id: string;
}
/**namespace:Cluster.ClusterState */
interface routing_nodes_state {
	Unassigned: routing_shard[];
	Nodes: map<string, routing_shard[]>[];
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_explanation {
	Command: string;
	Parameters: cluster_reroute_parameters;
	Decisions: cluster_reroute_decision[];
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_parameters {
	Index: string;
	Shard: integer;
	FromNode: string;
	ToNode: string;
	Node: string;
	AllowPrimary: boolean;
}
/**namespace:Cluster.ClusterReroute */
interface cluster_reroute_decision {
	Decider: string;
	Decision: string;
	Explanation: string;
}
/**namespace:Cluster.ClusterSettings.ClusterGetSettings */
interface cluster_get_settings_request extends request {
	FlatSettings: boolean;
	MasterTimeout: time;
	Timeout: time;
	IncludeDefaults: boolean;
}
/**namespace:Cluster.ClusterSettings.ClusterGetSettings */
interface cluster_get_settings_response extends response {
	Persistent: map<string, any>[];
	Transient: map<string, any>[];
}
/**namespace:Cluster.ClusterSettings.ClusterPutSettings */
interface cluster_put_settings_request extends request {
	Persistent: map<string, any>[];
	Transient: map<string, any>[];
	FlatSettings: boolean;
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Cluster.ClusterSettings.ClusterPutSettings */
interface cluster_put_settings_response extends response {
	Acknowledged: boolean;
	Persistent: map<string, any>[];
	Transient: map<string, any>[];
}
/**namespace:Cluster.ClusterState */
interface cluster_state_request extends request {
	Local: boolean;
	MasterTimeout: time;
	FlatSettings: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Cluster.ClusterState */
interface cluster_state_response extends response {
	ClusterName: string;
	MasterNode: string;
	StateUUID: string;
	Version: long;
	Nodes: map<string, node_state>[];
	Metadata: metadata_state;
	RoutingTable: routing_table_state;
	RoutingNodes: routing_nodes_state;
	Blocks: block_state;
}
/**namespace:Cluster.ClusterState */
interface metadata_state {
	Templates: map<string, template_mapping>[];
	ClusterUUID: string;
	Indices: map<string, metadata_index_state>[];
}
/**namespace:Indices.IndexSettings.IndexTemplates.GetIndexTemplate */
interface template_mapping {
	IndexPatterns: string[];
	Order: integer;
	Settings: map<string, any>[];
	Mappings: map<type_name, type_mapping>[];
	Aliases: map<index_name, alias>[];
	Version: integer;
}
/**namespace:CommonAbstractions.Infer.TypeName */
interface type_name {
	Name: string;
}
/**namespace:CommonAbstractions.Infer.PropertyName */
interface property_name {
	Name: string;
	CacheableExpression: boolean;
}
/**namespace:CommonAbstractions.Infer.JoinFieldRouting */
interface routing {
}
/**namespace:Cluster.ClusterState */
interface metadata_index_state {
	State: string;
	Settings: string[];
	Mappings: map<type_name, type_mapping>[];
	Aliases: string[];
}
/**namespace:Cluster.ClusterStats */
interface cluster_stats_request extends request {
	FlatSettings: boolean;
	Timeout: time;
}
/**namespace:Cluster.ClusterStats */
interface cluster_stats_response extends response {
	ClusterName: string;
	Timestamp: long;
	Status: ClusterStatus;
	Indices: cluster_indices_stats;
	Nodes: cluster_nodes_stats;
}
/**namespace:Cluster */
interface nodes_response_base extends response {
	NodeStatistics: node_statistics;
}
/**namespace:Cluster */
interface node_statistics {
	Total: integer;
	Successful: integer;
	Failed: integer;
}
/**namespace:Cluster.ClusterStats */
interface cluster_indices_stats {
	Completion: completion_stats;
	Count: long;
	Documents: doc_stats;
	Fielddata: fielddata_stats;
	QueryCache: query_cache_stats;
	Segments: segments_stats;
	Shards: cluster_indices_shards_stats;
	Store: store_stats;
}
/**namespace:CommonOptions.Stats */
interface completion_stats {
	Size: string;
	SizeInBytes: long;
}
/**namespace:CommonOptions.Stats */
interface doc_stats {
	Count: long;
	Deleted: long;
}
/**namespace:CommonOptions.Stats */
interface fielddata_stats {
	Evictions: long;
	MemorySize: string;
	MemorySizeInBytes: long;
}
/**namespace:CommonOptions.Stats */
interface query_cache_stats {
	MemorySize: string;
	MemorySizeInBytes: long;
	TotalCount: long;
	HitCount: long;
	MissCount: long;
	CacheSize: long;
	CacheCount: long;
	Evictions: long;
}
/**namespace:CommonOptions.Stats */
interface segments_stats {
	Count: long;
	DocValuesMemory: string;
	DocValuesMemoryInBytes: long;
	FixedBitSetMemory: string;
	FixedBitSetMemoryInBytes: long;
	IndexWriterMaxMemory: string;
	IndexWriterMaxMemoryInBytes: long;
	IndexWriterMemory: string;
	IndexWriterMemoryInBytes: long;
	Memory: string;
	MemoryInBytes: long;
	NormsMemory: string;
	NormsMemoryInBytes: long;
	PointsMemory: string;
	PointsMemoryInBytes: long;
	StoredFieldsMemory: string;
	StoredFieldsMemoryInBytes: long;
	TermVectorsMemory: string;
	TermVectorsMemoryInBytes: long;
	TermsMemory: string;
	TermsMemoryInBytes: long;
	VersionMapMemory: string;
	VersionMapMemoryInBytes: long;
}
/**namespace:Cluster.ClusterStats */
interface cluster_indices_shards_stats {
	Total: double;
	Primaries: double;
	Replication: double;
	Index: cluster_indices_shards_index_stats;
}
/**namespace:Cluster.ClusterStats */
interface cluster_indices_shards_index_stats {
	Shards: cluster_shard_metrics;
	Primaries: cluster_shard_metrics;
	Replication: cluster_shard_metrics;
}
/**namespace:Cluster.ClusterStats */
interface cluster_shard_metrics {
	Min: double;
	Max: double;
	Avg: double;
}
/**namespace:CommonOptions.Stats */
interface store_stats {
	Size: string;
	SizeInBytes: double;
}
/**namespace:Cluster.ClusterStats */
interface cluster_nodes_stats {
	Count: cluster_node_count;
	Versions: string[];
	OperatingSystem: cluster_operating_system_stats;
	Process: cluster_process;
	Jvm: cluster_jvm;
	FileSystem: cluster_file_system;
	Plugins: plugin_stats[];
}
/**namespace:Cluster.ClusterStats */
interface cluster_node_count {
	Total: integer;
	CoordinatingOnly: integer;
	Master: integer;
	Data: integer;
	Ingest: integer;
}
/**namespace:Cluster.ClusterStats */
interface cluster_operating_system_stats {
	AvailableProcessors: integer;
	AllocatedProcessors: integer;
	Names: cluster_operating_system_name[];
}
/**namespace:Cluster.ClusterStats */
interface cluster_operating_system_name {
	Count: integer;
	Name: string;
}
/**namespace:Cluster.ClusterStats */
interface cluster_process {
	Cpu: cluster_process_cpu;
	OpenFileDescriptors: cluster_process_open_file_descriptors;
}
/**namespace:Cluster.ClusterStats */
interface cluster_process_cpu {
	Percent: integer;
}
/**namespace:Cluster.ClusterStats */
interface cluster_process_open_file_descriptors {
	Min: long;
	Max: long;
	Avg: long;
}
/**namespace:Cluster.ClusterStats */
interface cluster_jvm {
	MaxUptime: string;
	MaxUptimeInMilliseconds: long;
	Versions: cluster_jvm_version[];
	Memory: cluster_jvm_memory;
	Threads: long;
}
/**namespace:Cluster.ClusterStats */
interface cluster_jvm_version {
	Version: string;
	VmName: string;
	VmVersion: string;
	VmVendor: string;
	Count: integer;
}
/**namespace:Cluster.ClusterStats */
interface cluster_jvm_memory {
	HeapUsed: string;
	HeapUsedInBytes: long;
	HeapMax: string;
	HeapMaxInBytes: long;
}
/**namespace:Cluster.ClusterStats */
interface cluster_file_system {
	Total: string;
	TotalInBytes: long;
	Free: string;
	FreeInBytes: long;
	Available: string;
	AvailableInBytes: long;
}
/**namespace:CommonOptions.Stats */
interface plugin_stats {
	Name: string;
	Version: string;
	Description: string;
	ClassName: string;
	Jvm: boolean;
	Isolated: boolean;
	Site: boolean;
}
/**namespace:Cluster.NodesHotThreads */
interface nodes_hot_threads_request extends request {
	Interval: time;
	Snapshots: long;
	Threads: long;
	IgnoreIdleThreads: boolean;
	ThreadType: ThreadType;
	Timeout: time;
}
/**namespace:Cluster.NodesHotThreads */
interface nodes_hot_threads_response extends response {
	HotThreads: hot_thread_information[];
}
/**namespace:Cluster.NodesHotThreads */
interface hot_thread_information {
	NodeName: string;
	NodeId: string;
	Threads: string[];
	Hosts: string[];
}
/**namespace:Cluster.NodesInfo */
interface nodes_info_request extends request {
	FlatSettings: boolean;
	Timeout: time;
}
/**namespace:Cluster.NodesInfo */
interface nodes_info_response extends response {
	ClusterName: string;
	Nodes: map<string, node_info>[];
}
/**namespace:Cluster.NodesInfo */
interface node_info {
	Name: string;
	TransportAddress: string;
	Host: string;
	Ip: string;
	Version: string;
	BuildHash: string;
	Roles: NodeRole[];
	Settings: string[];
	OperatingSystem: node_operating_system_info;
	Process: node_process_info;
	Jvm: node_jvm_info;
	ThreadPool: map<string, node_thread_pool_info>[];
	Network: node_info_network;
	Transport: node_info_transport;
	Http: node_info_http;
	Plugins: plugin_stats[];
}
/**namespace:Cluster.NodesInfo */
interface node_operating_system_info {
	Name: string;
	Architecture: string;
	Version: string;
	RefreshInterval: integer;
	AvailableProcessors: integer;
	Cpu: node_info_o_s_c_p_u;
	Mem: node_info_memory;
	Swap: node_info_memory;
}
/**namespace:Cluster.NodesInfo */
interface node_info_o_s_c_p_u {
	Vendor: string;
	Model: string;
	Mhz: integer;
	TotalCores: integer;
	TotalSockets: integer;
	CoresPerSocket: integer;
	CacheSize: string;
	CacheSizeInBytes: integer;
}
/**namespace:Cluster.NodesInfo */
interface node_info_memory {
	Total: string;
	TotalInBytes: long;
}
/**namespace:Cluster.NodesInfo */
interface node_process_info {
	RefreshIntervalInMilliseconds: long;
	Id: long;
	MlockAll: boolean;
}
/**namespace:Cluster.NodesInfo */
interface node_jvm_info {
	PID: integer;
	Version: string;
	VMName: string;
	VMVersion: string;
	VMVendor: string;
	MemoryPools: string[];
	GCCollectors: string[];
	StartTime: long;
	Memory: node_info_j_v_m_memory;
}
/**namespace:Cluster.NodesInfo */
interface node_info_j_v_m_memory {
	HeapInit: string;
	HeapInitInBytes: long;
	HeapMax: string;
	HeapMaxInBytes: long;
	NonHeapInit: string;
	NonHeapInitInBytes: long;
	NonHeapMax: string;
	NonHeapMaxInBytes: long;
	DirectMax: string;
	DirectMaxInBytes: long;
}
/**namespace:Cluster.NodesInfo */
interface node_thread_pool_info {
	Type: string;
	Min: integer;
	Max: integer;
	QueueSize: integer;
	KeepAlive: string;
}
/**namespace:Cluster.NodesInfo */
interface node_info_network {
	RefreshInterval: integer;
	PrimaryInterface: node_info_network_interface;
}
/**namespace:Cluster.NodesInfo */
interface node_info_network_interface {
	Address: string;
	Name: string;
	MacAddress: string;
}
/**namespace:Cluster.NodesInfo */
interface node_info_transport {
	BoundAddress: string[];
	PublishAddress: string;
}
/**namespace:Cluster.NodesInfo */
interface node_info_http {
	BoundAddress: string[];
	PublishAddress: string;
	MaxContentLength: string;
	MaxContentLengthInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface nodes_stats_request extends request {
	CompletionFields: field[];
	FielddataFields: field[];
	Fields: field[];
	Groups: boolean;
	Level: Level;
	Types: string[];
	Timeout: time;
	IncludeSegmentFileSizes: boolean;
}
/**namespace:CommonAbstractions.Infer.Field */
interface field {
	Name: string;
	Boost: double;
	CachableExpression: boolean;
}
/**namespace:Cluster.NodesStats */
interface nodes_stats_response extends response {
	ClusterName: string;
	Nodes: map<string, node_stats>[];
}
/**namespace:Cluster.NodesStats */
interface node_stats {
	Timestamp: long;
	Name: string;
	TransportAddress: string;
	Host: string;
	Ip: string[];
	Roles: NodeRole[];
	Indices: index_stats;
	OperatingSystem: operating_system_stats;
	Process: process_stats;
	Script: script_stats;
	Jvm: node_jvm_stats;
	ThreadPool: map<string, thread_count_stats>[];
	Breakers: map<string, breaker_stats>[];
	FileSystem: file_system_stats;
	Transport: transport_stats;
	Http: http_stats;
}
/**namespace:Indices.Monitoring.IndicesStats */
interface index_stats {
	Documents: doc_stats;
	Store: store_stats;
	Indexing: indexing_stats;
	Get: get_stats;
	Search: search_stats;
	Merges: merges_stats;
	Refresh: refresh_stats;
	Flush: flush_stats;
	Warmer: warmer_stats;
	QueryCache: query_cache_stats;
	Fielddata: fielddata_stats;
	Completion: completion_stats;
	Segments: segments_stats;
	Translog: translog_stats;
	RequestCache: request_cache_stats;
	Recovery: recovery_stats;
}
/**namespace:CommonOptions.Stats */
interface indexing_stats {
	DeleteCurrent: long;
	DeleteTime: string;
	DeleteTimeInMilliseconds: long;
	DeleteTotal: long;
	Current: long;
	Time: string;
	TimeInMilliseconds: long;
	Total: long;
	IsThrottled: boolean;
	NoopUpdateTotal: long;
	ThrottleTime: string;
	ThrottleTimeInMilliseconds: long;
	Types: map<string, indexing_stats>[];
}
/**namespace:CommonOptions.Stats */
interface get_stats {
	Current: long;
	ExistsTime: string;
	ExistsTimeInMilliseconds: long;
	ExistsTotal: long;
	MissingTime: string;
	MissingTimeInMilliseconds: long;
	MissingTotal: long;
	Time: string;
	TimeInMilliseconds: long;
	Total: long;
}
/**namespace:CommonOptions.Stats */
interface search_stats {
	OpenContexts: long;
	FetchCurrent: long;
	FetchTimeInMilliseconds: long;
	FetchTotal: long;
	QueryCurrent: long;
	QueryTimeInMilliseconds: long;
	QueryTotal: long;
	ScrollCurrent: long;
	ScrollTimeInMilliseconds: long;
	ScrollTotal: long;
	SuggestCurrent: long;
	SuggestTimeInMilliseconds: long;
	SuggestTotal: long;
}
/**namespace:CommonOptions.Stats */
interface merges_stats {
	Current: long;
	CurrentDocuments: long;
	CurrentSize: string;
	CurrentSizeInBytes: long;
	Total: long;
	TotalAutoThrottle: string;
	TotalAutoThrottleInBytes: long;
	TotalDocuments: long;
	TotalSize: string;
	TotalSizeInBytes: string;
	TotalStoppedTime: string;
	TotalStoppedTimeInMilliseconds: long;
	TotalThrottledTime: string;
	TotalThrottledTimeInMilliseconds: long;
	TotalTime: string;
	TotalTimeInMilliseconds: long;
}
/**namespace:CommonOptions.Stats */
interface refresh_stats {
	Total: long;
	TotalTime: string;
	TotalTimeInMilliseconds: long;
}
/**namespace:CommonOptions.Stats */
interface flush_stats {
	Total: long;
	TotalTime: string;
	TotalTimeInMilliseconds: long;
}
/**namespace:CommonOptions.Stats */
interface warmer_stats {
	Current: long;
	Total: long;
	TotalTime: string;
	TotalTimeInMilliseconds: long;
}
/**namespace:CommonOptions.Stats */
interface translog_stats {
	Operations: long;
	Size: string;
	SizeInBytes: long;
}
/**namespace:CommonOptions.Stats */
interface request_cache_stats {
	Evictions: long;
	HitCount: long;
	MemorySize: string;
	MemorySizeInBytes: long;
	MissCount: long;
}
/**namespace:CommonOptions.Stats */
interface recovery_stats {
	CurrentAsSource: long;
	CurrentAsTarget: long;
	ThrottleTime: string;
	ThrottleTimeInMilliseconds: long;
}
/**namespace:Cluster.NodesStats */
interface operating_system_stats {
	Timestamp: long;
	Memory: extended_memory_stats;
	Swap: memory_stats;
	Cpu: c_p_u_stats;
}
/**namespace:Cluster.NodesStats */
interface process_stats {
	Timestamp: long;
	OpenFileDescriptors: integer;
	CPU: c_p_u_stats;
	Memory: memory_stats;
}
/**namespace:Cluster.NodesStats */
interface script_stats {
	CacheEvictions: long;
	Compilations: long;
}
/**namespace:Cluster.NodesStats */
interface node_jvm_stats {
	Timestamp: long;
	Uptime: string;
	UptimeInMilliseconds: long;
	Memory: memory_stats;
	Threads: thread_stats;
	GarbageCollection: garbage_collection_stats;
	BufferPools: map<string, node_buffer_pool>[];
	Classes: jvm_classes_stats;
}
/**namespace:Cluster.NodesStats */
interface thread_count_stats {
	Threads: long;
	Queue: long;
	Active: long;
	Rejected: long;
	Largest: long;
	Completed: long;
}
/**namespace:Cluster.NodesStats */
interface breaker_stats {
	EstimatedSize: string;
	EstimatedSizeInBytes: long;
	LimitSize: string;
	LimitSizeInBytes: long;
	Overhead: float;
	Tripped: float;
}
/**namespace:Cluster.NodesStats */
interface file_system_stats {
	Timestamp: long;
	Total: total_file_system_stats;
	Data: data_path_stats[];
}
/**namespace:Cluster.NodesStats */
interface transport_stats {
	ServerOpen: integer;
	RXCount: long;
	RXSize: string;
	RXSizeInBytes: long;
	TXCount: long;
	TXSize: string;
	TXSizeInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface http_stats {
	CurrentOpen: integer;
	TotalOpened: long;
}
/**namespace:Cluster.NodesUsage */
interface nodes_usage_request extends request {
	Timeout: time;
}
/**namespace:Cluster.NodesUsage */
interface nodes_usage_response extends response {
	ClusterName: string;
	Nodes: map<string, node_usage_information>[];
}
/**namespace:Cluster.NodesUsage */
interface node_usage_information {
	Timestamp: Date;
	Since: Date;
	RestActions: map<string, integer>[];
}
/**namespace:Cluster.Ping */
interface ping_request extends request {
}
/**namespace:Cluster.Ping */
interface ping_response extends response {
}
/**namespace:Cluster.RemoteInfo */
interface remote_info_request extends request {
}
/**namespace:Cluster.RemoteInfo */
interface remote_info_response extends response {
	Remotes: map<string, remote_info>[];
}
/**namespace:CommonAbstractions.Response */
interface dictionary_response_base<t_key, t_value> extends response {
}
/**namespace:Cluster.RemoteInfo */
interface remote_info {
	Connected: boolean;
	NumNodesConnected: long;
	MaxConnectionsPerCluster: integer;
	InitialConnectTimeout: time;
	Seeds: string[];
	HttpAddresses: string[];
}
/**namespace:Cluster.RootNodeInfo */
interface root_node_info_request extends request {
}
/**namespace:Cluster.RootNodeInfo */
interface root_node_info_response extends response {
	Name: string;
	Tagline: string;
	Version: elasticsearch_version_info;
}
/**namespace:CommonAbstractions.Response */
interface elasticsearch_version_info {
	Number: string;
	IsSnapShotBuild: boolean;
	LuceneVersion: string;
}
/**namespace:Cluster.TaskManagement.CancelTasks */
interface cancel_tasks_request extends request {
	Nodes: string[];
	Actions: string[];
	ParentNode: string;
	ParentTaskId: string;
}
/**namespace:Cluster.TaskManagement.CancelTasks */
interface cancel_tasks_response extends response {
	IsValid: boolean;
	Nodes: map<string, task_executing_node>[];
	NodeFailures: error_cause[];
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface task_executing_node {
	Name: string;
	TransportAddress: string;
	Host: string;
	Ip: string;
	Tasks: map<task_id, task_state>[];
}
/**namespace:CommonAbstractions.Infer.TaskId */
interface task_id {
	NodeId: string;
	TaskNumber: long;
	FullyQualifiedId: string;
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface task_state {
	Node: string;
	Id: long;
	Type: string;
	Action: string;
	Status: task_status;
	Description: string;
	StartTimeInMilliseconds: long;
	RunningTimeInNanoSeconds: long;
	ParentTaskId: task_id;
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface task_status {
	Total: long;
	Updated: long;
	Created: long;
	Deleted: long;
	Batches: long;
	VersionConflicts: long;
	Noops: long;
	Retries: task_retries;
	ThrottledMilliseconds: long;
	RequestsPerSecond: long;
	ThrottledUntilMilliseconds: long;
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface task_retries {
	Bulk: integer;
	Search: integer;
}
/**namespace:Cluster.TaskManagement.GetTask */
interface get_task_request extends request {
	WaitForCompletion: boolean;
}
/**namespace:Cluster.TaskManagement.GetTask */
interface get_task_response extends response {
	Completed: boolean;
	Task: task_info;
}
/**namespace:Cluster.TaskManagement.GetTask */
interface task_info {
	Node: string;
	Id: long;
	Type: string;
	Action: string;
	Status: task_status;
	Description: string;
	StartTimeInMilliseconds: long;
	RunningTimeInNanoseconds: long;
	Cancellable: boolean;
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface list_tasks_request extends request {
	Nodes: string[];
	Actions: string[];
	Detailed: boolean;
	ParentNode: string;
	ParentTaskId: string;
	WaitForCompletion: boolean;
	GroupBy: GroupBy;
}
/**namespace:Cluster.TaskManagement.ListTasks */
interface list_tasks_response extends response {
	IsValid: boolean;
	Nodes: map<string, task_executing_node>[];
	NodeFailures: error_cause[];
}
/**namespace:CommonAbstractions.Response */
interface acknowledged_response_base extends response {
	Acknowledged: boolean;
}
/**namespace:CommonAbstractions.Response */
interface indices_response_base extends response {
	Acknowledged: boolean;
	ShardsHit: shard_statistics;
}
/**namespace:CommonOptions.Hit */
interface shard_statistics {
	Total: integer;
	Successful: integer;
	Failed: integer;
	Failures: shard_failure[];
}
/**namespace:CommonAbstractions.Response */
interface shards_operation_response_base extends response {
	Shards: shard_statistics;
}
/**namespace:Document.Multiple.Bulk */
interface bulk_request extends request {
	Operations: bulk_operation[];
	WaitForActiveShards: string;
	Refresh: Refresh;
	Routing: routing;
	Timeout: time;
	Fields: field[];
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	Pipeline: string;
}
/**namespace:CommonAbstractions.Infer.Id */
interface id {
}
/**namespace:Document.Multiple.Bulk */
interface bulk_response extends response {
	IsValid: boolean;
	Took: long;
	Errors: boolean;
	Items: bulk_response_item[];
	ItemsWithErrors: bulk_response_item[];
}
/**namespace:CommonOptions.Failures */
interface bulk_error extends error {
	Index: string;
	Shard: integer;
}
/**namespace:Document.Multiple.DeleteByQuery */
interface delete_by_query_request extends request {
	Query: query_container;
	Slice: sliced_scroll;
	Analyzer: string;
	AnalyzeWildcard: boolean;
	DefaultOperator: DefaultOperator;
	Df: string;
	From: long;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	Conflicts: Conflicts;
	ExpandWildcards: ExpandWildcards;
	Lenient: boolean;
	Preference: string;
	QueryOnQueryString: string;
	Routing: routing;
	Scroll: time;
	SearchType: SearchType;
	SearchTimeout: time;
	Size: long;
	Sort: string[];
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	TerminateAfter: long;
	Stats: string[];
	Version: boolean;
	RequestCache: boolean;
	Refresh: boolean;
	Timeout: time;
	WaitForActiveShards: string;
	ScrollSize: long;
	WaitForCompletion: boolean;
	RequestsPerSecond: long;
	Slices: long;
}
/**namespace:Document.Multiple.DeleteByQuery */
interface delete_by_query_response extends response {
	IsValid: boolean;
	Took: long;
	Task: task_id;
	TimedOut: boolean;
	SliceId: integer;
	Deleted: long;
	Batches: long;
	VersionConflicts: long;
	Noops: long;
	Retries: retries;
	ThrottledMilliseconds: long;
	RequestsPerSecond: float;
	ThrottledUntilMilliseconds: long;
	Total: long;
	Failures: bulk_index_by_scroll_failure[];
}
/**namespace:Document.Multiple */
interface retries {
	Bulk: long;
	Search: long;
}
/**namespace:Document.Multiple */
interface bulk_index_by_scroll_failure {
	Index: string;
	Type: string;
	Id: string;
	Cause: bulk_index_failure_cause;
	Status: integer;
}
/**namespace:Document.Multiple */
interface bulk_index_failure_cause extends error {
	IndexUniqueId: string;
	Shard: integer;
	Index: string;
}
/**namespace:Document.Multiple.MultiGet.Request */
interface multi_get_request extends request {
	StoredFields: field[];
	Documents: multi_get_operation[];
	Preference: string;
	Realtime: boolean;
	Refresh: boolean;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
}
/**namespace:Document.Multiple.MultiGet.Response */
interface multi_get_response extends response {
	IsValid: boolean;
	Hits: multi_get_hit<any>[];
}
/**namespace:Document.Multiple.MultiTermVectors */
interface multi_term_vectors_request extends request {
	Documents: multi_term_vector_operation[];
	TermStatistics: boolean;
	FieldStatistics: boolean;
	Fields: field[];
	Offsets: boolean;
	Positions: boolean;
	Payloads: boolean;
	Preference: string;
	Routing: routing;
	Parent: string;
	Realtime: boolean;
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Multiple.MultiTermVectors */
interface multi_term_vectors_response extends response {
	Documents: term_vectors[];
}
/**namespace:Document.Single.TermVectors */
interface term_vector {
	FieldStatistics: field_statistics;
	Terms: map<string, term_vector_term>[];
}
/**namespace:Document.Single.TermVectors */
interface field_statistics {
	DocumentCount: integer;
	SumOfDocumentFrequencies: integer;
	SumOfTotalTermFrequencies: integer;
}
/**namespace:Document.Single.TermVectors */
interface term_vector_term {
	DocumentFrequency: integer;
	TermFrequency: integer;
	Tokens: token[];
	TotalTermFrequency: integer;
}
/**namespace:Document.Single.TermVectors */
interface token {
	EndOffset: integer;
	Payload: string;
	Position: integer;
	StartOffset: integer;
}
/**namespace:Document.Multiple.ReindexOnServer */
interface reindex_on_server_request extends request {
	Source: reindex_source;
	Destination: reindex_destination;
	Script: script;
	Size: long;
	Conflicts: Conflicts;
	Refresh: boolean;
	Timeout: time;
	WaitForActiveShards: string;
	WaitForCompletion: boolean;
	RequestsPerSecond: long;
	Slices: long;
}
/**namespace:CommonAbstractions.Infer.Indices */
interface indices extends union<all_indices_marker, many_indices> {
	All: indices;
	AllIndices: indices;
}
/**namespace:CommonAbstractions.Infer.Types */
interface types extends union<all_types_marker, many_types> {
	All: all_types_marker;
	AllTypes: all_types_marker;
}
/**namespace:Document.Multiple.ReindexOnServer */
interface reindex_routing {
}
/**namespace:Document.Multiple.ReindexOnServer */
interface reindex_on_server_response extends response {
	IsValid: boolean;
	Took: time;
	Task: task_id;
	TimedOut: boolean;
	Total: long;
	Created: long;
	Updated: long;
	Batches: long;
	VersionConflicts: long;
	Noops: long;
	Retries: retries;
	Failures: bulk_index_by_scroll_failure[];
}
/**namespace:Document.Multiple.ReindexRethrottle */
interface reindex_rethrottle_request extends request {
	RequestsPerSecond: long;
}
/**namespace:Document.Multiple.ReindexRethrottle */
interface reindex_rethrottle_response extends response {
	Nodes: map<string, reindex_node>[];
}
/**namespace:Document.Multiple.ReindexRethrottle */
interface reindex_node {
	Name: string;
	TransportAddress: string;
	Host: string;
	Ip: string;
	Roles: string[];
	Attributes: map<string, string>[];
	Tasks: map<task_id, reindex_task>[];
}
/**namespace:Document.Multiple.ReindexRethrottle */
interface reindex_task {
	Node: string;
	Id: long;
	Type: string;
	Action: string;
	Status: reindex_status;
	Description: string;
	StartTimeInMilliseconds: long;
	RunningTimeInNanoseconds: long;
	Cancellable: boolean;
}
/**namespace:Document.Multiple.ReindexRethrottle */
interface reindex_status {
	Total: long;
	Updated: long;
	Created: long;
	Deleted: long;
	Batches: long;
	VersionConflicts: long;
	Noops: long;
	Retries: retries;
	ThrottledInMilliseconds: long;
	RequestsPerSecond: float;
	ThrottledUntilInMilliseconds: long;
}
/**namespace:Document.Multiple.UpdateByQuery */
interface update_by_query_request extends request {
	Query: query_container;
	Script: script;
	Analyzer: string;
	AnalyzeWildcard: boolean;
	DefaultOperator: DefaultOperator;
	Df: string;
	From: long;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	Conflicts: Conflicts;
	ExpandWildcards: ExpandWildcards;
	Lenient: boolean;
	Pipeline: string;
	Preference: string;
	QueryOnQueryString: string;
	Routing: routing;
	Scroll: time;
	SearchType: SearchType;
	SearchTimeout: time;
	Size: long;
	Sort: string[];
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	TerminateAfter: long;
	Stats: string[];
	Version: boolean;
	VersionType: boolean;
	RequestCache: boolean;
	Refresh: boolean;
	Timeout: time;
	WaitForActiveShards: string;
	ScrollSize: long;
	WaitForCompletion: boolean;
	RequestsPerSecond: long;
	Slices: long;
}
/**namespace:Document.Multiple.UpdateByQuery */
interface update_by_query_response extends response {
	IsValid: boolean;
	Took: long;
	Task: task_id;
	TimedOut: boolean;
	Total: long;
	Updated: long;
	Batches: long;
	VersionConflicts: long;
	Noops: long;
	Retries: retries;
	Failures: bulk_index_by_scroll_failure[];
	RequestsPerSecond: float;
}
/**namespace:Document.Single.Create */
interface create_request<t_document> extends request {
	Document: t_document;
	WaitForActiveShards: string;
	Parent: string;
	Refresh: Refresh;
	Routing: routing;
	Timeout: time;
	Version: long;
	VersionType: VersionType;
	Pipeline: string;
}
/**namespace:Document.Single.Create */
interface create_response extends response {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Result: Result;
	Shards: shard_statistics;
	SequenceNumber: long;
	PrimaryTerm: long;
}
/**namespace:Document.Single.Delete */
interface delete_request extends request {
	WaitForActiveShards: string;
	Parent: string;
	Refresh: Refresh;
	Routing: routing;
	Timeout: time;
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Delete */
interface delete_response extends response {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Result: Result;
	Shards: shard_statistics;
	SequenceNumber: long;
	PrimaryTerm: long;
}
/**namespace:Document.Single.Exists */
interface document_exists_request extends request {
	StoredFields: field[];
	Parent: string;
	Preference: string;
	Realtime: boolean;
	Refresh: boolean;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Get */
interface get_request extends request {
	StoredFields: field[];
	Parent: string;
	Preference: string;
	Realtime: boolean;
	Refresh: boolean;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Get */
interface get_response<t_document> extends response {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Found: boolean;
	Source: t_document;
	Fields: map<string, lazy_document>[];
	Parent: string;
	Routing: string;
}
/**namespace:Document.Single.Index */
interface index_request<t_document> extends request {
	Document: t_document;
	WaitForActiveShards: string;
	OpType: OpType;
	Parent: string;
	Refresh: Refresh;
	Routing: routing;
	Timeout: time;
	Version: long;
	VersionType: VersionType;
	Pipeline: string;
}
/**namespace:Document.Single.Index */
interface index_response extends response {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Result: Result;
	Shards: shard_statistics;
	SequenceNumber: long;
	PrimaryTerm: long;
}
/**namespace:Document.Single.SourceExists */
interface source_exists_request extends request {
	Parent: string;
	Preference: string;
	Realtime: boolean;
	Refresh: boolean;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Source */
interface source_request extends request {
	Parent: string;
	Preference: string;
	Realtime: boolean;
	Refresh: boolean;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Source */
interface source_response<t> extends response {
	Body: t;
}
/**namespace:Document.Single.TermVectors */
interface term_vectors_request<t_document> extends request {
	Document: t_document;
	PerFieldAnalyzer: map<field, string>[];
	Filter: term_vector_filter;
	TermStatistics: boolean;
	FieldStatistics: boolean;
	Fields: field[];
	Offsets: boolean;
	Positions: boolean;
	Payloads: boolean;
	Preference: string;
	Routing: routing;
	Parent: string;
	Realtime: boolean;
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.TermVectors */
interface term_vectors_response extends response {
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Found: boolean;
	Took: long;
	TermVectors: map<field, term_vector>[];
}
/**namespace:Document.Single.Update */
interface update_request<t_document, t_partial_document> extends request {
	Script: script;
	Upsert: t_document;
	DocAsUpsert: boolean;
	Doc: t_partial_document;
	DetectNoop: boolean;
	ScriptedUpsert: boolean;
	Source: union<boolean, source_filter>;
	Fields: field[];
	WaitForActiveShards: string;
	SourceEnabled: boolean;
	Lang: string;
	Parent: string;
	Refresh: Refresh;
	RetryOnConflict: long;
	Routing: routing;
	Timeout: time;
	Version: long;
	VersionType: VersionType;
}
/**namespace:Document.Single.Update */
interface update_response<t_document> extends response {
	ShardsHit: shard_statistics;
	Index: string;
	Type: string;
	Id: string;
	Version: long;
	Get: instant_get<t_document>;
	Result: Result;
}
/**namespace:Search.Explain */
interface instant_get<t_document> {
	Found: boolean;
	Source: t_document;
	Fields: map<string, lazy_document>[];
}
/**namespace:Indices.AliasManagement.AliasExists */
interface alias_exists_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Local: boolean;
}
/**namespace:Indices.AliasManagement.Alias */
interface bulk_alias_request extends request {
	Actions: alias_action[];
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Indices.AliasManagement.Alias */
interface bulk_alias_response extends response {
}
/**namespace:Indices.AliasManagement.DeleteAlias */
interface delete_alias_request extends request {
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Indices.AliasManagement.DeleteAlias */
interface delete_alias_response extends response {
}
/**namespace:Indices.AliasManagement.GetAlias */
interface get_alias_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Local: boolean;
}
/**namespace:Indices.AliasManagement.GetAlias */
interface get_alias_response extends response {
	Indices: map<index_name, index_aliases>[];
	IsValid: boolean;
}
/**namespace:Indices.AliasManagement.GetAlias */
interface index_aliases {
	Aliases: map<string, alias_definition>[];
}
/**namespace:Indices.AliasManagement */
interface alias_definition {
	Filter: query_container;
	Routing: string;
	IndexRouting: string;
	SearchRouting: string;
}
/**namespace:CommonOptions.MinimumShouldMatch */
interface minimum_should_match extends union<integer, string> {
}
/**namespace:QueryDsl.MultiTermQueryRewrite */
interface multi_term_query_rewrite {
	Rewrite: RewriteMultiTerm;
	Size: integer;
	ConstantScore: multi_term_query_rewrite;
	ScoringBoolean: multi_term_query_rewrite;
	ConstantScoreBoolean: multi_term_query_rewrite;
}
/**namespace:CommonAbstractions.Infer.RelationName */
interface relation_name {
	Name: string;
}
/**namespace:QueryDsl.Specialized.MoreLikeThis.Like */
interface like extends union<string, like_document> {
}
/**namespace:QueryDsl.Geo */
interface geo_location {
	Latitude: double;
	Longitude: double;
}
/**namespace:CommonOptions.Geo */
interface distance {
	Precision: double;
	Unit: DistanceUnit;
}
/**namespace:Indices.AliasManagement.PutAlias */
interface put_alias_request extends request {
	Routing: routing;
	Filter: query_container;
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Indices.AliasManagement.PutAlias */
interface put_alias_response extends response {
}
/**namespace:Indices.Analyze */
interface analyze_request extends request {
	Tokenizer: union<string, tokenizer>;
	Analyzer: string;
	Explain: boolean;
	Attributes: string[];
	CharFilter: union<string, char_filter>[];
	Filter: union<string, token_filter>[];
	Normalizer: string;
	Field: field;
	Text: string[];
	PreferLocal: boolean;
	Format: Format;
}
/**namespace:Indices.Analyze */
interface analyze_response extends response {
	Tokens: analyze_token[];
	Detail: analyze_detail;
}
/**namespace:Indices.Analyze */
interface analyze_token {
	Token: string;
	Type: string;
	StartOffset: long;
	EndOffset: long;
	Position: long;
	PositionLength: long;
}
/**namespace:Indices.Analyze */
interface analyze_detail {
	CustomAnalyzer: boolean;
	CharFilters: char_filter_detail[];
	Filters: token_detail[];
	Tokenizer: token_detail;
}
/**namespace:Indices.Analyze */
interface char_filter_detail {
	Name: string;
	FilteredText: string[];
}
/**namespace:Indices.Analyze */
interface token_detail {
	Name: string;
	Tokens: explain_analyze_token[];
}
/**namespace:Indices.Analyze */
interface explain_analyze_token {
	Token: string;
	Type: string;
	StartOffset: long;
	EndOffset: long;
	Position: long;
	PositionLength: long;
	TermFrequency: long;
	Keyword: boolean;
	Bytes: string;
}
/**namespace:Indices.IndexManagement.CreateIndex */
interface create_index_request extends request {
	Settings: map<string, any>[];
	Mappings: map<type_name, type_mapping>[];
	Aliases: map<index_name, alias>[];
	WaitForActiveShards: string;
	Timeout: time;
	MasterTimeout: time;
	UpdateAllTypes: boolean;
}
/**namespace:Indices.IndexManagement.CreateIndex */
interface create_index_response extends response {
	ShardsAcknowledged: boolean;
}
/**namespace:Indices.IndexManagement.DeleteIndex */
interface delete_index_request extends request {
	Timeout: time;
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.IndexManagement.DeleteIndex */
interface delete_index_response extends response {
}
/**namespace:Indices.IndexManagement.GetIndex */
interface get_index_request extends request {
	Local: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	FlatSettings: boolean;
	IncludeDefaults: boolean;
}
/**namespace:Indices.IndexManagement.GetIndex */
interface get_index_response extends response {
	Indices: map<index_name, index_state>[];
}
/**namespace:IndexModules.IndexSettings */
interface index_state {
	Settings: map<string, any>[];
	Mappings: map<type_name, type_mapping>[];
	Aliases: map<index_name, alias>[];
}
/**namespace:Indices.IndexManagement.IndicesExists */
interface index_exists_request extends request {
	Local: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	FlatSettings: boolean;
	IncludeDefaults: boolean;
}
/**namespace:Indices.IndexManagement.IndicesExists */
interface exists_response extends response {
	Exists: boolean;
}
/**namespace:Indices.IndexManagement.OpenCloseIndex.CloseIndex */
interface close_index_request extends request {
	Timeout: time;
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.IndexManagement.OpenCloseIndex.CloseIndex */
interface close_index_response extends response {
}
/**namespace:Indices.IndexManagement.OpenCloseIndex.OpenIndex */
interface open_index_request extends request {
	Timeout: time;
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.IndexManagement.OpenCloseIndex.OpenIndex */
interface open_index_response extends response {
}
/**namespace:Indices.IndexManagement.RolloverIndex */
interface rollover_index_request extends request {
	Conditions: rollover_conditions;
	Settings: map<string, any>[];
	Aliases: map<index_name, alias>[];
	Mappings: map<type_name, type_mapping>[];
	Timeout: time;
	DryRun: boolean;
	MasterTimeout: time;
	WaitForActiveShards: string;
}
/**namespace:Indices.IndexManagement.RolloverIndex */
interface rollover_index_response extends response {
	DryRun: boolean;
	NewIndex: string;
	OldIndex: string;
	RolledOver: boolean;
	Conditions: map<string, boolean>[];
	ShardsAcknowledged: boolean;
}
/**namespace:Indices.IndexManagement.ShrinkIndex */
interface shrink_index_request extends request {
	Settings: map<string, any>[];
	Aliases: map<index_name, alias>[];
	Timeout: time;
	MasterTimeout: time;
	WaitForActiveShards: string;
}
/**namespace:Indices.IndexManagement.ShrinkIndex */
interface shrink_index_response extends response {
	ShardsAcknowledged: boolean;
}
/**namespace:Indices.IndexManagement.TypesExists */
interface type_exists_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Local: boolean;
}
/**namespace:Indices.IndexSettings.GetIndexSettings */
interface get_index_settings_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	FlatSettings: boolean;
	Local: boolean;
	IncludeDefaults: boolean;
}
/**namespace:Indices.IndexSettings.GetIndexSettings */
interface get_index_settings_response extends response {
	Indices: map<index_name, index_state>[];
}
/**namespace:Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate */
interface delete_index_template_request extends request {
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate */
interface delete_index_template_response extends response {
}
/**namespace:Indices.IndexSettings.IndexTemplates.GetIndexTemplate */
interface get_index_template_request extends request {
	FlatSettings: boolean;
	MasterTimeout: time;
	Local: boolean;
}
/**namespace:Indices.IndexSettings.IndexTemplates.GetIndexTemplate */
interface get_index_template_response extends response {
	TemplateMappings: map<string, template_mapping>[];
}
/**namespace:Indices.IndexSettings.IndexTemplates.IndexTemplateExists */
interface index_template_exists_request extends request {
	FlatSettings: boolean;
	MasterTimeout: time;
	Local: boolean;
}
/**namespace:Indices.IndexSettings.IndexTemplates.PutIndexTemplate */
interface put_index_template_request extends request {
	IndexPatterns: string[];
	Order: integer;
	Version: integer;
	Settings: map<string, any>[];
	Mappings: map<type_name, type_mapping>[];
	Aliases: map<index_name, alias>[];
	Create: boolean;
	Timeout: time;
	MasterTimeout: time;
	FlatSettings: boolean;
}
/**namespace:Indices.IndexSettings.IndexTemplates.PutIndexTemplate */
interface put_index_template_response extends response {
}
/**namespace:Indices.IndexSettings.UpdateIndexSettings */
interface update_index_settings_response extends response {
}
/**namespace:Indices.MappingManagement.GetFieldMapping */
interface get_field_mapping_request extends request {
	IncludeDefaults: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Local: boolean;
}
/**namespace:Indices.MappingManagement.GetFieldMapping */
interface get_field_mapping_response extends response {
	Indices: map<index_name, type_field_mappings>[];
	IsValid: boolean;
}
/**namespace:Indices.MappingManagement.GetFieldMapping */
interface type_field_mappings {
	Mappings: map<type_name, map<field, field_mapping>[]>[];
}
/**namespace:Indices.MappingManagement.GetMapping */
interface get_mapping_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Local: boolean;
}
/**namespace:Indices.MappingManagement.GetMapping */
interface get_mapping_response extends response {
	Indices: map<index_name, index_mappings>[];
	Mappings: map<index_name, index_mappings>[];
	Mapping: type_mapping;
}
/**namespace:Indices.MappingManagement.GetMapping */
interface index_mappings {
	Mappings: map<type_name, type_mapping>[];
	Item: type_mapping;
}
/**namespace:Indices.MappingManagement.PutMapping */
interface put_mapping_request extends request {
	AllField: all_field;
	DateDetection: boolean;
	DynamicDateFormats: string[];
	DynamicTemplates: map<string, dynamic_template>[];
	Dynamic: union<boolean, DynamicMapping>;
	FieldNamesField: field_names_field;
	IndexField: index_field;
	Meta: map<string, any>[];
	NumericDetection: boolean;
	Properties: map<property_name, property>[];
	RoutingField: routing_field;
	SizeField: size_field;
	SourceField: source_field;
	Timeout: time;
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	UpdateAllTypes: boolean;
}
/**namespace:Indices.MappingManagement.PutMapping */
interface put_mapping_response extends response {
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_status_request extends request {
	Detailed: boolean;
	ActiveOnly: boolean;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_status_response extends response {
	Indices: map<index_name, recovery_status>[];
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_status {
	Shards: shard_recovery[];
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface shard_recovery {
	Id: long;
	Type: string;
	Stage: string;
	Primary: boolean;
	StartTime: Date;
	StopTime: Date;
	TotalTimeInMilliseconds: long;
	Source: recovery_origin;
	Target: recovery_origin;
	Index: recovery_index_status;
	Translog: recovery_translog_status;
	Start: recovery_start_status;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_origin {
	Id: string;
	HostName: string;
	Ip: string;
	Name: string;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_index_status {
	TotalTimeInMilliseconds: long;
	Bytes: recovery_bytes;
	Files: recovery_files;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_bytes {
	Total: long;
	Reused: long;
	Recovered: long;
	Percent: string;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_files {
	Total: long;
	Reused: long;
	Recovered: long;
	Percent: string;
	Details: recovery_file_details[];
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_file_details {
	Name: string;
	Length: long;
	Recovered: long;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_translog_status {
	Recovered: long;
	Total: long;
	Percent: string;
	TotalOnStart: long;
	TotalTime: string;
	TotalTimeInMilliseconds: long;
}
/**namespace:Indices.Monitoring.IndicesRecovery */
interface recovery_start_status {
	CheckIndexTime: long;
	TotalTimeInMilliseconds: string;
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface segments_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	OperationThreading: string;
	Verbose: boolean;
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface segments_response extends response {
	Shards: shard_statistics;
	Indices: map<string, index_segment>[];
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface index_segment {
	Shards: map<string, shards_segment>[];
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface shards_segment {
	CommittedSegments: integer;
	SearchSegments: integer;
	Routing: shard_segment_routing;
	Segments: map<string, segment>[];
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface shard_segment_routing {
	State: string;
	Primary: boolean;
	Node: string;
}
/**namespace:Indices.Monitoring.IndicesSegments */
interface segment {
	Generation: integer;
	TotalDocuments: long;
	DeletedDocuments: long;
	Size: string;
	SizeInBytes: double;
	Committed: boolean;
	Search: boolean;
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface indices_shard_stores_request extends request {
	Types: type_name[];
	Status: string[];
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	OperationThreading: string;
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface indices_shard_stores_response extends response {
	Indices: map<string, indices_shard_stores>[];
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface indices_shard_stores {
	Shards: map<string, shard_store_wrapper>[];
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface shard_store_wrapper {
	Stores: shard_store[];
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface shard_store {
	Id: string;
	Name: string;
	TransportAddress: string;
	LegacyVersion: long;
	AllocationId: string;
	StoreException: shard_store_exception;
	Allocation: ShardStoreAllocation;
	Attributes: map<string, any>[];
}
/**namespace:Indices.Monitoring.IndicesShardStores */
interface shard_store_exception {
	Type: string;
	Reason: string;
}
/**namespace:Indices.Monitoring.IndicesStats */
interface indices_stats_request extends request {
	Types: type_name[];
	CompletionFields: field[];
	FielddataFields: field[];
	Fields: field[];
	Groups: string[];
	Level: Level;
	IncludeSegmentFileSizes: boolean;
}
/**namespace:Indices.Monitoring.IndicesStats */
interface indices_stats_response extends response {
	Shards: shard_statistics;
	Stats: indices_stats;
	Indices: map<string, indices_stats>[];
}
/**namespace:Indices.Monitoring.IndicesStats */
interface indices_stats {
	Primaries: index_stats;
	Total: index_stats;
}
/**namespace:Indices.StatusManagement.ClearCache */
interface clear_cache_request extends request {
	Fielddata: boolean;
	Fields: field[];
	Query: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	Recycler: boolean;
	RequestCache: boolean;
	Request: boolean;
}
/**namespace:Indices.StatusManagement.ClearCache */
interface clear_cache_response extends response {
}
/**namespace:Indices.StatusManagement.Flush */
interface flush_request extends request {
	Force: boolean;
	WaitIfOngoing: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.StatusManagement.Flush */
interface flush_response extends response {
}
/**namespace:Indices.StatusManagement.ForceMerge */
interface force_merge_request extends request {
	Flush: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	MaxNumSegments: long;
	OnlyExpungeDeletes: boolean;
	OperationThreading: string;
	WaitForMerge: boolean;
}
/**namespace:Indices.StatusManagement.ForceMerge */
interface force_merge_response extends response {
}
/**namespace:Indices.StatusManagement.Refresh */
interface refresh_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.StatusManagement.Refresh */
interface refresh_response extends response {
}
/**namespace:Indices.StatusManagement.SyncedFlush */
interface synced_flush_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.StatusManagement.SyncedFlush */
interface synced_flush_response extends response {
}
/**namespace:Indices.StatusManagement.Upgrade */
interface upgrade_request extends request {
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	IgnoreUnavailable: boolean;
	WaitForCompletion: boolean;
	OnlyAncientSegments: boolean;
}
/**namespace:Indices.StatusManagement.Upgrade */
interface upgrade_response extends response {
	Shards: shard_statistics;
}
/**namespace:Indices.StatusManagement.Upgrade.UpgradeStatus */
interface upgrade_status_request extends request {
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Indices.StatusManagement.Upgrade.UpgradeStatus */
interface upgrade_status_response extends response {
	Upgrades: map<string, upgrade_status>[];
	SizeInBytes: long;
	SizeToUpgradeInBytes: string;
	SizeToUpgradeAncientInBytes: string;
}
/**namespace:Indices.StatusManagement.Upgrade.UpgradeStatus */
interface upgrade_status {
	Size: string;
	SizeInBytes: long;
	SizeToUpgrade: string;
	SizeToUpgradeInBytes: string;
	SizeToUpgradeAncientInBytes: string;
}
/**namespace:Ingest.DeletePipeline */
interface delete_pipeline_request extends request {
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Ingest.DeletePipeline */
interface delete_pipeline_response extends response {
}
/**namespace:Ingest.GetPipeline */
interface get_pipeline_request extends request {
	MasterTimeout: time;
}
/**namespace:Ingest.GetPipeline */
interface get_pipeline_response extends response {
	Pipelines: map<string, pipeline>[];
}
/**namespace:Ingest.Processor */
interface grok_processor_patterns_request extends request {
}
/**namespace:Ingest.Processor */
interface grok_processor_patterns_response extends response {
	Patterns: map<string, string>[];
}
/**namespace:Ingest.PutPipeline */
interface put_pipeline_request extends request {
	Description: string;
	Processors: processor[];
	OnFailure: processor[];
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Ingest.PutPipeline */
interface put_pipeline_response extends response {
}
/**namespace:Ingest.SimulatePipeline */
interface simulate_pipeline_request extends request {
	Pipeline: pipeline;
	Documents: simulate_pipeline_document[];
	Verbose: boolean;
}
/**namespace:Ingest.SimulatePipeline */
interface simulate_pipeline_response extends response {
	Documents: pipeline_simulation[];
}
/**namespace:Ingest.SimulatePipeline */
interface pipeline_simulation {
	ProcessorResults: pipeline_simulation[];
	Tag: string;
	Document: document_simulation;
}
/**namespace:Ingest.SimulatePipeline */
interface document_simulation {
	Index: string;
	Type: string;
	Id: string;
	Parent: string;
	Routing: string;
	Source: lazy_document;
	Ingest: ingest;
}
/**namespace:Ingest.SimulatePipeline */
interface ingest {
	Timestamp: Date;
}
/**namespace:Mapping.Types.Complex.Nested */
interface nested_property extends object_property {
	IncludeInParent: boolean;
	IncludeInRoot: boolean;
}
/**namespace:Mapping.Types.Complex.Object */
interface object_property extends core_property_base {
	Dynamic: union<boolean, DynamicMapping>;
	Enabled: boolean;
	Properties: map<property_name, property>[];
}
/**namespace:Mapping.Types */
interface core_property_base extends property_base {
	CopyTo: field[];
	Fields: map<property_name, property>[];
	Similarity: union<SimilarityOption, string>;
	Store: boolean;
}
/**namespace:Mapping.Types */
interface property_base {
	Name: property_name;
	LocalMetadata: map<string, any>[];
}
/**namespace:Mapping.Types.Core.Binary */
interface binary_property extends doc_values_property_base {
}
/**namespace:Mapping.Types */
interface doc_values_property_base extends core_property_base {
	DocValues: boolean;
}
/**namespace:Mapping.Types.Core.Boolean */
interface boolean_property extends doc_values_property_base {
	Index: boolean;
	Boost: double;
	NullValue: boolean;
	Fielddata: numeric_fielddata;
}
/**namespace:Mapping.Types.Core.Date */
interface date_property extends doc_values_property_base {
	Index: boolean;
	Boost: double;
	NullValue: Date;
	PrecisionStep: integer;
	IgnoreMalformed: boolean;
	Format: string;
	Fielddata: numeric_fielddata;
}
/**namespace:Mapping.Types.Core.Join */
interface join_property extends property_base {
	Relations: map<relation_name, relation_name[]>[];
}
/**namespace:Mapping.Types.Core.Keyword */
interface keyword_property extends doc_values_property_base {
	Boost: double;
	EagerGlobalOrdinals: boolean;
	IgnoreAbove: integer;
	Index: boolean;
	IndexOptions: IndexOptions;
	Norms: boolean;
	NullValue: string;
	Normalizer: string;
}
/**namespace:Mapping.Types.Core.Number */
interface number_property extends doc_values_property_base {
	Index: boolean;
	Boost: double;
	NullValue: double;
	IgnoreMalformed: boolean;
	Coerce: boolean;
	Fielddata: numeric_fielddata;
	ScalingFactor: double;
}
/**namespace:Mapping.Types.Core.Percolator */
interface percolator_property extends property_base {
}
/**namespace:Mapping.Types.Core.Range.DateRange */
interface date_range_property extends range_property_base {
	Format: string;
}
/**namespace:Mapping.Types.Core.Range */
interface range_property_base extends doc_values_property_base {
	Coerce: boolean;
	Boost: double;
	Index: boolean;
}
/**namespace:Mapping.Types.Core.Range.DoubleRange */
interface double_range_property extends range_property_base {
}
/**namespace:Mapping.Types.Core.Range.FloatRange */
interface float_range_property extends range_property_base {
}
/**namespace:Mapping.Types.Core.Range.IntegerRange */
interface integer_range_property extends range_property_base {
}
/**namespace:Mapping.Types.Core.Range.LongRange */
interface long_range_property extends range_property_base {
}
/**namespace:Mapping.Types.Core.Text */
interface text_property extends core_property_base {
	Boost: double;
	EagerGlobalOrdinals: boolean;
	Fielddata: boolean;
	FielddataFrequencyFilter: fielddata_frequency_filter;
	Index: boolean;
	IndexOptions: IndexOptions;
	Norms: boolean;
	PositionIncrementGap: integer;
	Analyzer: string;
	SearchAnalyzer: string;
	SearchQuoteAnalyzer: string;
	TermVector: TermVectorOption;
}
/**namespace:Mapping.Types.Geo.GeoPoint */
interface geo_point_property extends doc_values_property_base {
	IgnoreMalformed: boolean;
}
/**namespace:Mapping.Types.Geo.GeoShape */
interface geo_shape_property extends doc_values_property_base {
	Tree: GeoTree;
	Precision: distance;
	Orientation: GeoOrientation;
	TreeLevels: integer;
	Strategy: GeoStrategy;
	DistanceErrorPercentage: double;
	PointsOnly: boolean;
}
/**namespace:Mapping.Types.Specialized.Completion */
interface completion_property extends doc_values_property_base {
	SearchAnalyzer: string;
	Analyzer: string;
	PreserveSeparators: boolean;
	PreservePositionIncrements: boolean;
	MaxInputLength: integer;
	Contexts: suggest_context[];
}
/**namespace:Mapping.Types.Specialized.Generic */
interface generic_property extends doc_values_property_base {
	TermVector: TermVectorOption;
	Boost: double;
	SearchAnalyzer: string;
	IgnoreAbove: integer;
	PositionIncrementGap: integer;
	Fielddata: string_fielddata;
	Index: FieldIndexOption;
	NullValue: string;
	Norms: boolean;
	IndexOptions: IndexOptions;
	Analyzer: string;
	Type: string;
}
/**namespace:Mapping.Types.Specialized.Ip */
interface ip_property extends doc_values_property_base {
	Boost: double;
	Index: boolean;
	NullValue: string;
}
/**namespace:Mapping.Types.Specialized.Murmur3Hash */
interface murmur3_hash_property extends doc_values_property_base {
}
/**namespace:Mapping.Types.Specialized.TokenCount */
interface token_count_property extends doc_values_property_base {
	Analyzer: string;
	Index: boolean;
	Boost: double;
	NullValue: double;
}
/**namespace:Modules.Indices */
interface indices_module_settings {
	QeueriesCacheSize: string;
	CircuitBreakerSettings: circuit_breaker_settings;
	FielddataSettings: fielddata_settings;
	RecoverySettings: indices_recovery_settings;
}
/**namespace:Modules.Indices.Fielddata */
interface fielddata_settings {
	CacheSize: string;
	CacheExpire: time;
}
/**namespace:Modules.Scripting.DeleteScript */
interface delete_script_request extends request {
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Modules.Scripting.DeleteScript */
interface delete_script_response extends response {
}
/**namespace:Modules.Scripting.GetScript */
interface get_script_request extends request {
}
/**namespace:Modules.Scripting.GetScript */
interface get_script_response extends response {
	Script: stored_script;
}
/**namespace:Modules.Scripting.PutScript */
interface put_script_request extends request {
	Script: stored_script;
	Timeout: time;
	MasterTimeout: time;
}
/**namespace:Modules.Scripting.PutScript */
interface put_script_response extends response {
}
/**namespace:Modules.SnapshotAndRestore.Repositories.CreateRepository */
interface create_repository_request extends request {
	Repository: snapshot_repository;
	MasterTimeout: time;
	Timeout: time;
	Verify: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Repositories.CreateRepository */
interface create_repository_response extends response {
}
/**namespace:Modules.SnapshotAndRestore.Repositories.DeleteRepository */
interface delete_repository_request extends request {
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Modules.SnapshotAndRestore.Repositories.DeleteRepository */
interface delete_repository_response extends response {
}
/**namespace:Modules.SnapshotAndRestore.Repositories.GetRepository */
interface get_repository_request extends request {
	MasterTimeout: time;
	Local: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Repositories.GetRepository */
interface get_repository_response extends response {
	Repositories: map<string, snapshot_repository>[];
}
/**namespace:Modules.SnapshotAndRestore.Repositories.VerifyRepository */
interface verify_repository_request extends request {
	MasterTimeout: time;
	Timeout: time;
}
/**namespace:Modules.SnapshotAndRestore.Repositories.VerifyRepository */
interface verify_repository_response extends response {
	Nodes: map<string, compact_node_info>[];
}
/**namespace:Modules.SnapshotAndRestore.Repositories.VerifyRepository */
interface compact_node_info {
	Name: string;
}
/**namespace:Modules.SnapshotAndRestore.Restore */
interface restore_request extends request {
	Indices: indices;
	IgnoreUnavailable: boolean;
	IncludeGlobalState: boolean;
	RenamePattern: string;
	RenameReplacement: string;
	IndexSettings: update_index_settings_request;
	IgnoreIndexSettings: string[];
	MasterTimeout: time;
	WaitForCompletion: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Restore */
interface restore_response extends response {
	Snapshot: snapshot_restore;
}
/**namespace:Modules.SnapshotAndRestore.Restore */
interface snapshot_restore {
	Name: string;
	Indices: index_name[];
	Shards: shard_statistics;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot */
interface delete_snapshot_request extends request {
	MasterTimeout: time;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot */
interface delete_snapshot_response extends response {
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.GetSapshot */
interface get_snapshot_request extends request {
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
	Verbose: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.GetSapshot */
interface get_snapshot_response extends response {
	Snapshots: snapshot[];
}
/**namespace:Modules.SnapshotAndRestore.Snapshot */
interface snapshot {
	Name: string;
	Indices: index_name[];
	State: string;
	StartTime: Date;
	StartTimeInMilliseconds: long;
	EndTime: Date;
	EndTimeInMilliseconds: long;
	DurationInMilliseconds: long;
	Shards: shard_statistics;
	Failures: snapshot_shard_failure[];
}
/**namespace:Modules.SnapshotAndRestore.Snapshot */
interface snapshot_shard_failure {
	NodeId: string;
	Index: string;
	ShardId: string;
	Reason: string;
	Status: string;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_status_request extends request {
	MasterTimeout: time;
	IgnoreUnavailable: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_status_response extends response {
	Snapshots: snapshot_status[];
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_status {
	Snapshot: string;
	Repository: string;
	State: string;
	ShardsStats: snapshot_shards_stats;
	Stats: snapshot_stats;
	Indices: map<string, snapshot_index_stats>[];
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_shards_stats {
	Initializing: long;
	Started: long;
	Finalizing: long;
	Done: long;
	Failed: long;
	Total: long;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_stats {
	NumberOfFiles: long;
	ProcessedFiles: long;
	TotalSizeInBytes: long;
	ProcessedSizeInBytes: long;
	StartTimeInMilliseconds: long;
	TimeInMilliseconds: long;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.SnapshotStatus */
interface snapshot_index_stats {
	ShardsStats: snapshot_shards_stats;
	Stats: snapshot_stats;
	Shards: map<string, snapshot_shards_stats>[];
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.Snapshot */
interface snapshot_request extends request {
	Indices: indices;
	IgnoreUnavailable: boolean;
	IncludeGlobalState: boolean;
	Partial: boolean;
	MasterTimeout: time;
	WaitForCompletion: boolean;
}
/**namespace:Modules.SnapshotAndRestore.Snapshot.Snapshot */
interface snapshot_response extends response {
	Accepted: boolean;
	Snapshot: snapshot;
}
/**namespace:Search.Count */
interface count_request extends request {
	Query: query_container;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	MinScore: double;
	Preference: string;
	Routing: routing;
	QueryOnQueryString: string;
	Analyzer: string;
	AnalyzeWildcard: boolean;
	DefaultOperator: DefaultOperator;
	Df: string;
	Lenient: boolean;
	TerminateAfter: long;
}
/**namespace:Search.Count */
interface count_response extends response {
	Count: long;
	Shards: shard_statistics;
}
/**namespace:Search.Explain */
interface explain_request<t_document> extends request {
	StoredFields: field[];
	Query: query_container;
	AnalyzeWildcard: boolean;
	Analyzer: string;
	DefaultOperator: DefaultOperator;
	Df: string;
	Lenient: boolean;
	Parent: string;
	Preference: string;
	QueryOnQueryString: string;
	Routing: routing;
	SourceEnabled: boolean;
	SourceExclude: field[];
	SourceInclude: field[];
}
/**namespace:Search.Explain */
interface explain_response<t_document> extends response {
	Matched: boolean;
	Explanation: explanation_detail;
	Get: instant_get<t_document>;
}
/**namespace:Search.Explain */
interface explanation_detail {
	Value: float;
	Description: string;
	Details: explanation_detail[];
}
/**namespace:Search.FieldCapabilities */
interface field_capabilities_request extends request {
	Fields: field[];
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Search.FieldCapabilities */
interface field_capabilities_response extends response {
	Shards: shard_statistics;
	Fields: map<field, map<string, field_capabilities>[]>[];
}
/**namespace:Search.FieldCapabilities */
interface field_capabilities {
	Searchable: boolean;
	Aggregatable: boolean;
	Indices: indices;
	NonSearchableIndices: indices;
	NonAggregatableIndices: indices;
}
/**namespace:Search.MultiSearchTemplate */
interface multi_search_template_request extends request {
	Operations: map<string, search_template_request>[];
	SearchType: SearchType;
	TypedKeys: boolean;
	MaxConcurrentSearches: long;
}
/**namespace:Search.MultiSearch */
interface multi_search_request extends request {
	Operations: map<string, search_request>[];
	SearchType: SearchType;
	MaxConcurrentSearches: long;
	TypedKeys: boolean;
	PreFilterShardSize: long;
}
/**namespace:Search.Suggesters.PhraseSuggester.SmoothingModel */
interface smoothing_model_container {
}
/**namespace:Search.Suggesters.ContextSuggester */
interface context extends union<string, geo_location> {
	Category: string;
	Geo: geo_location;
}
/**namespace:Aggregations.Bucket.Histogram */
interface histogram_order {
	Key: string;
	Order: SortOrder;
	CountAscending: histogram_order;
	CountDescending: histogram_order;
	KeyAscending: histogram_order;
	KeyDescending: histogram_order;
}
/**namespace:Aggregations.Bucket.Histogram */
interface extended_bounds<t> {
	Minimum: t;
	Maximum: t;
}
/**namespace:CommonOptions.DateMath */
interface date_math {
	Now: date_math_expression;
}
/**namespace:CommonOptions.DateMath */
interface date_math_expression extends date_math {
}
/**namespace:Aggregations.Bucket.Terms */
interface terms_order {
	Key: string;
	Order: SortOrder;
	CountAscending: terms_order;
	CountDescending: terms_order;
	TermAscending: terms_order;
	TermDescending: terms_order;
	KeyAscending: terms_order;
	KeyDescending: terms_order;
}
/**namespace:Aggregations.Bucket.Terms */
interface terms_include {
	Pattern: string;
	Values: string[];
	Partition: long;
	NumberOfPartitions: long;
}
/**namespace:Aggregations.Bucket.Terms */
interface terms_exclude {
	Pattern: string;
	Values: string[];
}
/**namespace:DefaultLanguageConstruct */
interface significant_terms_include_exclude {
	Pattern: string;
	Values: string[];
}
/**namespace:Search.MultiSearch */
interface multi_search_response extends response {
	IsValid: boolean;
	TotalResponses: integer;
	AllResponses: response[];
}
/**namespace:Search.Scroll.ClearScroll */
interface clear_scroll_request extends request {
	ScrollIds: string[];
}
/**namespace:Search.Scroll.ClearScroll */
interface clear_scroll_response extends response {
}
/**namespace:Search.Scroll.Scroll */
interface scroll_request extends request {
	Scroll: time;
	ScrollId: string;
}
/**namespace:Search.Search.Hits */
interface inner_hits_result {
	Hits: inner_hits_metadata;
}
/**namespace:Search.Search.Hits */
interface inner_hits_metadata {
	Total: long;
	MaxScore: double;
	Hits: hit<lazy_document>[];
}
/**namespace:Search.Search.Hits */
interface nested_identity {
	Field: field;
	Offset: integer;
	Nested: nested_identity;
}
/**namespace:Search.Search.Highlighting */
interface highlight_hit {
	DocumentId: string;
	Field: string;
	Highlights: string[];
}
/**namespace:Search.Explain */
interface explanation {
	Value: float;
	Description: string;
	Details: explanation_detail[];
}
/**namespace:Search.SearchShards */
interface search_shards_request extends request {
	Preference: string;
	Routing: routing;
	Local: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
}
/**namespace:Search.SearchShards */
interface search_shards_response extends response {
	Shards: search_shard[][];
	Nodes: map<string, search_node>[];
}
/**namespace:Search.SearchShards */
interface search_shard {
	State: string;
	Primary: boolean;
	Node: string;
	RelocatingNode: string;
	Shard: integer;
	Index: string;
}
/**namespace:Search.SearchShards */
interface search_node {
	Name: string;
	TransportAddress: string;
}
/**namespace:Search.SearchTemplate.RenderSearchTemplate */
interface render_search_template_request extends request {
	Source: string;
	Inline: string;
	File: string;
	Params: map<string, any>[];
}
/**namespace:Search.SearchTemplate.RenderSearchTemplate */
interface render_search_template_response extends response {
	TemplateOutput: lazy_document;
}
/**namespace:Search.Search */
interface search_response<t> extends response {
	Shards: shard_statistics;
	Aggregations: map<string, aggregate>[];
	Aggs: map<string, aggregate>[];
	Profile: profile;
	Suggest: map<string, suggest<t>[]>[];
	Took: long;
	TimedOut: boolean;
	TerminatedEarly: boolean;
	ScrollId: string;
	HitsMetadata: hits_metadata<t>;
	NumberOfReducePhases: long;
	Total: long;
	MaxScore: double;
	Documents: t[];
	Hits: hit<t>[];
	Fields: map<string, lazy_document>[][];
}
/**namespace:Search.Search.Profile */
interface profile {
	Shards: shard_profile[];
}
/**namespace:Search.Search.Profile */
interface shard_profile {
	Id: string;
	Searches: search_profile[];
	Aggregations: aggregation_profile[];
}
/**namespace:Search.Search.Profile */
interface search_profile {
	RewriteTime: long;
	Query: query_profile[];
	Collector: collector[];
}
/**namespace:Search.Search.Profile */
interface query_profile {
	Type: string;
	Description: string;
	TimeInNanoseconds: long;
	Breakdown: query_breakdown;
	Children: query_profile[];
}
/**namespace:Search.Search.Profile */
interface query_breakdown {
	Score: long;
	NextDoc: long;
	CreateWeight: long;
	BuildScorer: long;
	Advance: long;
	Match: long;
}
/**namespace:Search.Search.Profile */
interface collector {
	Name: string;
	Reason: string;
	TimeInNanoseconds: long;
	Children: collector[];
}
/**namespace:Search.Search.Profile */
interface aggregation_profile {
	Type: string;
	Description: string;
	TimeInNanoseconds: long;
	Breakdown: aggregation_breakdown;
}
/**namespace:Search.Search.Profile */
interface aggregation_breakdown {
	Reduce: long;
	BuildAggregation: long;
	BuildAggregationCount: long;
	Initialize: long;
	InitializeCount: long;
	ReduceCount: long;
	Collect: long;
	CollectCount: long;
}
/**namespace:Search.Suggesters */
interface suggest<t> {
	Length: integer;
	Offset: integer;
	Text: string;
	Options: suggest_option<t>[];
}
/**namespace:Search.Suggesters */
interface suggest_option<t_document> {
	Text: string;
	Score: double;
	Frequency: long;
	Index: index_name;
	Type: type_name;
	Id: id;
	Source: t_document;
	Contexts: map<string, context[]>[];
	Highlighted: string;
	CollateMatch: boolean;
}
/**namespace:Search.Search.Hits */
interface hits_metadata<t> {
	Total: long;
	MaxScore: double;
	Hits: hit<t>[];
}
/**namespace:Search.Validate */
interface validate_query_request extends request {
	Query: query_container;
	Explain: boolean;
	IgnoreUnavailable: boolean;
	AllowNoIndices: boolean;
	ExpandWildcards: ExpandWildcards;
	OperationThreading: string;
	QueryOnQueryString: string;
	Analyzer: string;
	AnalyzeWildcard: boolean;
	DefaultOperator: DefaultOperator;
	Df: string;
	Lenient: boolean;
	Rewrite: boolean;
	AllShards: boolean;
}
/**namespace:Search.Validate */
interface validate_query_response extends response {
	Valid: boolean;
	Shards: shard_statistics;
	Explanations: validation_explanation[];
}
/**namespace:Search.Validate */
interface validation_explanation {
	Index: string;
	Valid: boolean;
	Error: string;
	Explanation: string;
}
/**namespace:XPack.Migration.DeprecationInfo */
interface deprecation_info_request extends request {
}
/**namespace:XPack.Migration.DeprecationInfo */
interface deprecation_info_response extends response {
	ClusterSettings: deprecation_info[];
	NodeSettings: deprecation_info[];
	IndexSettings: map<string, deprecation_info[]>[];
}
/**namespace:XPack.Migration.DeprecationInfo */
interface deprecation_info {
	Level: DeprecationWarningLevel;
	Message: string;
	Url: string;
	Details: string;
}
/**namespace:XPack.Graph.Explore */
interface graph_explore_request extends request {
	Query: query_container;
	Vertices: graph_vertex_definition[];
	Connections: hop;
	Controls: graph_explore_controls;
	Routing: routing;
	Timeout: time;
}
/**namespace:XPack.Graph.Explore.Request */
interface graph_vertex_include {
	Term: string;
	Boost: double;
}
/**namespace:XPack.Graph.Explore.Request */
interface sample_diversity {
	Field: field;
	MaxDocumentsPerValue: integer;
}
/**namespace:XPack.Graph.Explore */
interface graph_explore_response extends response {
	Took: long;
	TimedOut: boolean;
	Connections: graph_connection[];
	Vertices: graph_vertex[];
	Failures: shard_failure[];
}
/**namespace:XPack.Graph.Explore.Response */
interface graph_connection {
	DocumentCount: long;
	Source: long;
	Target: long;
	Weight: double;
}
/**namespace:XPack.Graph.Explore.Response */
interface graph_vertex {
	Depth: long;
	Field: string;
	Term: string;
	Weight: double;
}
/**namespace:XPack.Info.XPackInfo */
interface x_pack_info_request extends request {
	Categories: string[];
}
/**namespace:XPack.Info.XPackInfo */
interface x_pack_info_response extends response {
	Build: x_pack_build_information;
	License: minimal_license_information;
	Features: x_pack_features;
	Tagline: string;
}
/**namespace:XPack.Info.XPackInfo */
interface x_pack_build_information {
	Date: Date;
	Hash: string;
}
/**namespace:XPack.Info.XPackInfo */
interface minimal_license_information {
	UID: string;
	Type: LicenseType;
	Mode: LicenseType;
	Status: LicenseStatus;
	ExpiryDateInMilliseconds: long;
}
/**namespace:XPack.Info.XPackInfo */
interface x_pack_features {
	Watcher: x_pack_feature;
	Graph: x_pack_feature;
	MachineLearning: x_pack_feature;
	Monitoring: x_pack_feature;
	Security: x_pack_feature;
}
/**namespace:XPack.Info.XPackInfo */
interface x_pack_feature {
	Description: string;
	Available: boolean;
	Enabled: boolean;
	NativeCodeInformation: native_code_information;
}
/**namespace:XPack.Info.XPackInfo */
interface native_code_information {
	Version: string;
	BuildHash: string;
}
/**namespace:XPack.Info.XPackUsage */
interface x_pack_usage_request extends request {
	MasterTimeout: time;
}
/**namespace:XPack.Info.XPackUsage */
interface x_pack_usage_response extends response {
	Graph: x_pack_usage;
	Monitoring: monitoring_usage;
	MachineLearning: machine_learning_usage;
	Alerting: alerting_usage;
	Security: security_usage;
}
/**namespace:XPack.Info.XPackUsage */
interface x_pack_usage {
	Available: boolean;
	Enabled: boolean;
}
/**namespace:XPack.Info.XPackUsage */
interface monitoring_usage extends x_pack_usage {
	EnabledExporters: map<string, long>[];
}
/**namespace:XPack.Info.XPackUsage */
interface machine_learning_usage extends x_pack_usage {
	Jobs: map<string, job>[];
	Datafeeds: map<string, data_feed>[];
}
/**namespace:XPack.Info.XPackUsage */
interface alerting_usage extends x_pack_usage {
	Execution: alerting_execution;
	Count: alerting_count;
}
/**namespace:XPack.Info.XPackUsage */
interface security_usage extends x_pack_usage {
	SystemKey: security_feature_toggle;
	Anonymous: security_feature_toggle;
	Ssl: ssl_usage;
	IpFilter: ip_filter_usage;
	Audit: audit_usage;
	Roles: map<string, role_usage>[];
	Realms: map<string, realm_usage>[];
}
/**namespace:XPack.License.DeleteLicense */
interface delete_license_request extends request {
}
/**namespace:XPack.License.DeleteLicense */
interface delete_license_response extends response {
}
/**namespace:XPack.License.GetLicense */
interface get_license_request extends request {
	Local: boolean;
}
/**namespace:XPack.License.GetLicense */
interface get_license_response extends response {
	IsValid: boolean;
	License: license_information;
}
/**namespace:XPack.License.GetLicense */
interface license_information {
	Status: LicenseStatus;
	UID: string;
	Type: LicenseType;
	IssueDate: Date;
	IssueDateInMilliseconds: long;
	ExpiryDate: Date;
	ExpiryDateInMilliseconds: long;
	MaxNodes: long;
	IssuedTo: string;
	Issuer: string;
}
/**namespace:XPack.License.PostLicense */
interface post_license_request extends request {
	License: license;
	Acknowledge: boolean;
}
/**namespace:XPack.License.GetLicense */
interface license {
	UID: string;
	Signature: string;
	Type: LicenseType;
	IssueDateInMilliseconds: long;
	ExpiryDateInMilliseconds: long;
	MaxNodes: long;
	IssuedTo: string;
	Issuer: string;
}
/**namespace:XPack.License.PostLicense */
interface post_license_response extends response {
	Acknowledged: boolean;
	LicenseStatus: LicenseStatus;
	Acknowledge: license_acknowledgement;
}
/**namespace:XPack.License.PostLicense */
interface license_acknowledgement {
	Message: string;
	License: string[];
}
/**namespace:XPack.MachineLearning.CloseJob */
interface close_job_request extends request {
	Force: boolean;
	Timeout: time;
}
/**namespace:XPack.MachineLearning.CloseJob */
interface close_job_response extends response {
	Closed: boolean;
}
/**namespace:XPack.MachineLearning.DeleteDatafeed */
interface delete_datafeed_request extends request {
	Force: boolean;
}
/**namespace:XPack.MachineLearning.DeleteDatafeed */
interface delete_datafeed_response extends response {
}
/**namespace:XPack.MachineLearning.DeleteExpiredData */
interface delete_expired_data_request extends request {
}
/**namespace:XPack.MachineLearning.DeleteExpiredData */
interface delete_expired_data_response extends response {
	Deleted: boolean;
}
/**namespace:XPack.MachineLearning.DeleteJob */
interface delete_job_request extends request {
	Force: boolean;
}
/**namespace:XPack.MachineLearning.DeleteJob */
interface delete_job_response extends response {
}
/**namespace:XPack.MachineLearning.DeleteModelSnapshot */
interface delete_model_snapshot_request extends request {
}
/**namespace:XPack.MachineLearning.DeleteModelSnapshot */
interface delete_model_snapshot_response extends response {
}
/**namespace:XPack.MachineLearning.FlushJob */
interface flush_job_request extends request {
	AdvanceTime: Date;
	CalculateInterim: boolean;
	End: Date;
	Start: Date;
	SkipTime: string;
}
/**namespace:XPack.MachineLearning.FlushJob */
interface flush_job_response extends response {
	Flushed: boolean;
}
/**namespace:XPack.MachineLearning.GetAnomalyRecords */
interface get_anomaly_records_request extends request {
	Descending: boolean;
	ExcludeInterim: boolean;
	End: Date;
	Page: page;
	RecordScore: double;
	Sort: field;
	Start: Date;
}
/**namespace:XPack.MachineLearning.GetAnomalyRecords */
interface get_anomaly_records_response extends response {
	Count: long;
	Records: anomaly_record[];
}
/**namespace:XPack.MachineLearning.Job.Results */
interface anomaly_record {
	JobId: string;
	ResultType: string;
	Probability: double;
	RecordScore: double;
	InitialRecordScore: double;
	BucketSpan: time;
	DetectorIndex: integer;
	IsInterim: boolean;
	Timestamp: Date;
	Function: string;
	FunctionDescription: string;
	Typical: double[];
	Actual: double[];
	FieldName: string;
	ByFieldName: string;
	ByFieldValue: string;
	Causes: anomaly_cause[];
	Influencers: influence[];
	OverFieldName: string;
	OverFieldValue: string;
	PartitionFieldName: string;
	PartitionFieldValue: string;
}
/**namespace:XPack.MachineLearning.Job.Results */
interface anomaly_cause {
	Probability: double;
	OverFieldName: string;
	OverFieldValue: string;
	ByFieldName: string;
	ByFieldValue: string;
	CorrelatedByFieldValue: string;
	PartitionFieldName: string;
	PartitionFieldValue: string;
	Function: string;
	FunctionDescription: string;
	Typical: double[];
	Actual: double[];
	Influencers: influence[];
	FieldName: string;
}
/**namespace:XPack.MachineLearning.Job.Results */
interface influence {
	InfluencerFieldName: string;
	InfluencerFieldValues: string[];
}
/**namespace:XPack.MachineLearning.GetBuckets */
interface get_buckets_request extends request {
	AnomalyScore: double;
	Descending: boolean;
	End: Date;
	ExcludeInterim: boolean;
	Expand: boolean;
	Page: page;
	Sort: field;
	Start: Date;
	Timestamp: Date;
}
/**namespace:XPack.MachineLearning.GetBuckets */
interface get_buckets_response extends response {
	Count: long;
	Buckets: bucket[];
}
/**namespace:XPack.MachineLearning.Job.Results */
interface bucket {
	JobId: string;
	Timestamp: Date;
	AnomalyScore: double;
	BucketSpan: time;
	InitialAnomalyScore: double;
	EventCount: long;
	IsInterim: boolean;
	BucketInfluencers: bucket_influencer[];
	ProcessingTimeMilliseconds: double;
	PartitionScores: partition_score[];
	ResultType: string;
}
/**namespace:XPack.MachineLearning.Job.Results */
interface bucket_influencer {
	JobId: string;
	ResultType: string;
	InfluencerFieldName: string;
	InfluencerFieldValue: string;
	InitialInfluencerScore: double;
	InfluencerScore: double;
	Probability: double;
	BucketSpan: long;
	IsInterim: boolean;
	Timestamp: Date;
}
/**namespace:XPack.MachineLearning.Job.Results */
interface partition_score {
	PartitionFieldName: string;
	PartitionFieldValue: string;
	InitialRecordScore: double;
	RecordScore: double;
	Probability: double;
}
/**namespace:XPack.MachineLearning.GetCategories */
interface get_categories_request extends request {
	Page: page;
}
/**namespace:XPack.MachineLearning.GetCategories */
interface get_categories_response extends response {
	Count: long;
	Categories: category_definition[];
}
/**namespace:XPack.MachineLearning.Job.Results */
interface category_definition {
	JobId: string;
	CategoryId: long;
	Terms: string;
	Regex: string;
	MaxMatchingLength: long;
	Examples: string[];
}
/**namespace:XPack.MachineLearning.GetDatafeedStats */
interface get_datafeed_stats_request extends request {
}
/**namespace:XPack.MachineLearning.GetDatafeedStats */
interface get_datafeed_stats_response extends response {
	Count: long;
	Datafeeds: datafeed_stats[];
}
/**namespace:XPack.MachineLearning.Datafeed */
interface datafeed_stats {
	DatafeedId: string;
	State: DatafeedState;
	Node: discovery_node;
	AssignmentExplanation: string;
}
/**namespace:XPack.MachineLearning.Datafeed */
interface discovery_node {
	Id: string;
	Name: string;
	EphemeralId: string;
	TransportAddress: string;
	Attributes: map<string, string>[];
}
/**namespace:XPack.MachineLearning.GetDatafeeds */
interface get_datafeeds_request extends request {
}
/**namespace:XPack.MachineLearning.GetDatafeeds */
interface get_datafeeds_response extends response {
	Count: long;
	Datafeeds: datafeed_config[];
}
/**namespace:XPack.MachineLearning.Datafeed */
interface datafeed_config {
	DatafeedId: string;
	Aggregations: map<string, aggregation_container>[];
	ChunkingConfig: chunking_config;
	Frequency: time;
	Indices: indices;
	JobId: string;
	Query: query_container;
	QueryDelay: time;
	ScriptFields: map<string, script_field>[];
	ScrollSize: integer;
	Types: types;
}
/**namespace:XPack.MachineLearning.GetInfluencers */
interface get_influencers_request extends request {
	Descending: boolean;
	End: Date;
	ExcludeInterim: boolean;
	InfluencerScore: double;
	Page: page;
	Sort: field;
	Start: Date;
}
/**namespace:XPack.MachineLearning.GetInfluencers */
interface get_influencers_response extends response {
	Count: long;
	Influencers: bucket_influencer[];
}
/**namespace:XPack.MachineLearning.GetJobStats */
interface get_job_stats_request extends request {
}
/**namespace:XPack.MachineLearning.GetJobStats */
interface get_job_stats_response extends response {
	Count: long;
	Jobs: job_stats[];
}
/**namespace:XPack.MachineLearning.Job.Config */
interface job_stats {
	AssignmentExplanation: string;
	JobId: string;
	DataCounts: data_counts;
	ModelSizeStats: model_size_stats;
	State: JobState;
	Node: discovery_node;
	OpenTime: time;
}
/**namespace:XPack.MachineLearning.Job.Process */
interface data_counts {
	JobId: string;
	ProcessedRecordCount: long;
	ProcessedFieldCount: long;
	InputBytes: long;
	InputFieldCount: long;
	InvalidDateCount: long;
	MissingFieldCount: long;
	OutOfOrderTimestampCount: long;
	EmptyBucketCount: long;
	SparseBucketCount: long;
	BucketCount: long;
	EarliestRecordTimestamp: Date;
	LatestRecordTimestamp: Date;
	LatestEmptyBucketTimestamp: Date;
	LastDataTime: Date;
	LatestSparseBucketTimestamp: Date;
	InputRecordCount: long;
}
/**namespace:XPack.MachineLearning.Job.Process */
interface model_size_stats {
	JobId: string;
	ResultType: string;
	ModelBytes: long;
	TotalByFieldCount: long;
	TotalOverFieldCount: long;
	TotalPartitionFieldCount: long;
	BucketAllocationFailuresCount: long;
	MemoryStatus: MemoryStatus;
	LogTime: Date;
	Timestamp: Date;
}
/**namespace:XPack.MachineLearning.GetJobs */
interface get_jobs_request extends request {
}
/**namespace:XPack.MachineLearning.GetJobs */
interface get_jobs_response extends response {
	Count: long;
	Jobs: job[];
}
/**namespace:XPack.Info.XPackUsage */
interface job {
	JobId: string;
	JobType: string;
	Description: string;
	CreateTime: Date;
	FinishedTime: Date;
	AnalysisConfig: analysis_config;
	AnalysisLimits: analysis_limits;
	BackgroundPersistInterval: time;
	DataDescription: data_description;
	ModelSnapshotRetentionDays: long;
	ModelSnapshotId: string;
	ResultsIndexName: string;
	ModelPlotConfig: model_plot_config;
	RenormalizationWindowDays: long;
	ResultsRetentionDays: long;
}
/**namespace:XPack.MachineLearning.GetModelSnapshots */
interface get_model_snapshots_request extends request {
	Descending: boolean;
	End: Date;
	Page: page;
	Sort: field;
	Start: Date;
}
/**namespace:XPack.MachineLearning.GetModelSnapshots */
interface get_model_snapshots_response extends response {
	Count: long;
	ModelSnapshots: model_snapshot[];
}
/**namespace:XPack.MachineLearning.Job.Process */
interface model_snapshot {
	JobId: string;
	Timestamp: Date;
	Description: string;
	SnapshotId: string;
	SnapshotDocCount: long;
	ModelSizeStats: model_size_stats;
	LatestRecordTimeStamp: Date;
	LatestResultTimeStamp: Date;
	Retain: boolean;
}
/**namespace:XPack.MachineLearning.OpenJob */
interface open_job_request extends request {
	Timeout: time;
}
/**namespace:XPack.MachineLearning.OpenJob */
interface open_job_response extends response {
	Opened: boolean;
}
/**namespace:XPack.MachineLearning.PostJobData */
interface post_job_data_request extends request {
	Data: any[];
	ResetStart: Date;
	ResetEnd: Date;
}
/**namespace:XPack.MachineLearning.PostJobData */
interface post_job_data_response extends response {
	JobId: string;
	ProcessedRecordCount: long;
	ProcessedFieldCount: long;
	InputBytes: long;
	InputFieldCount: long;
	InvalidDateCount: long;
	MissingFieldCount: long;
	OutOfOrderTimestampCount: long;
	EmptyBucketCount: long;
	SparseBucketCount: long;
	BucketCount: long;
	LastDataTime: Date;
	InputRecordCount: long;
}
/**namespace:XPack.MachineLearning.PreviewDatafeed */
interface preview_datafeed_request extends request {
}
/**namespace:XPack.MachineLearning.PreviewDatafeed */
interface preview_datafeed_response<t> extends response {
	Data: t[];
}
/**namespace:XPack.MachineLearning.PutDatafeed */
interface put_datafeed_request extends request {
	Aggregations: map<string, aggregation_container>[];
	ChunkingConfig: chunking_config;
	Frequency: time;
	Indices: indices;
	JobId: id;
	Query: query_container;
	QueryDelay: time;
	ScriptFields: map<string, script_field>[];
	ScrollSize: integer;
	Types: types;
}
/**namespace:XPack.MachineLearning.PutDatafeed */
interface put_datafeed_response extends response {
	DatafeedId: string;
	Aggregations: map<string, aggregation_container>[];
	ChunkingConfig: chunking_config;
	Frequency: time;
	Indices: indices;
	JobId: string;
	Query: query_container;
	QueryDelay: time;
	ScriptFields: map<string, script_field>[];
	ScrollSize: integer;
	Types: types;
}
/**namespace:XPack.MachineLearning.PutJob */
interface put_job_request extends request {
	AnalysisConfig: analysis_config;
	AnalysisLimits: analysis_limits;
	DataDescription: data_description;
	Description: string;
	ModelPlotConfig: model_plot_config;
	ModelSnapshotRetentionDays: long;
	ResultsIndexName: index_name;
}
/**namespace:XPack.MachineLearning.PutJob */
interface put_job_response extends response {
	JobId: string;
	JobType: string;
	Description: string;
	CreateTime: Date;
	AnalysisConfig: analysis_config;
	AnalysisLimits: analysis_limits;
	BackgroundPersistInterval: time;
	DataDescription: data_description;
	ModelSnapshotRetentionDays: long;
	ModelSnapshotId: string;
	ResultsIndexName: string;
	ModelPlotConfig: model_plot_config;
	RenormalizationWindowDays: long;
	ResultsRetentionDays: long;
}
/**namespace:XPack.MachineLearning.RevertModelSnapshot */
interface revert_model_snapshot_request extends request {
	DeleteInterveningResults: boolean;
}
/**namespace:XPack.MachineLearning.RevertModelSnapshot */
interface revert_model_snapshot_response extends response {
	Model: model_snapshot;
}
/**namespace:XPack.MachineLearning.StartDatafeed */
interface start_datafeed_request extends request {
	Timeout: time;
	Start: Date;
	End: Date;
}
/**namespace:XPack.MachineLearning.StartDatafeed */
interface start_datafeed_response extends response {
	Started: boolean;
}
/**namespace:XPack.MachineLearning.StopDatafeed */
interface stop_datafeed_request extends request {
	Timeout: time;
	Force: boolean;
}
/**namespace:XPack.MachineLearning.StopDatafeed */
interface stop_datafeed_response extends response {
	Stopped: boolean;
}
/**namespace:XPack.MachineLearning.UpdateDataFeed */
interface update_datafeed_request extends request {
	Aggregations: map<string, aggregation_container>[];
	ChunkingConfig: chunking_config;
	Frequency: time;
	Indices: indices;
	JobId: id;
	Query: query_container;
	QueryDelay: time;
	ScriptFields: map<string, script_field>[];
	ScrollSize: integer;
	Types: types;
}
/**namespace:XPack.MachineLearning.UpdateDataFeed */
interface update_datafeed_response extends response {
	DatafeedId: string;
	Aggregations: map<string, aggregation_container>[];
	ChunkingConfig: chunking_config;
	Frequency: time;
	Indices: indices;
	JobId: string;
	Query: query_container;
	QueryDelay: time;
	ScriptFields: map<string, script_field>[];
	ScrollSize: integer;
	Types: types;
}
/**namespace:XPack.MachineLearning.UpdateJob */
interface update_job_request extends request {
	AnalysisLimits: analysis_memory_limit;
	BackgroundPersistInterval: time;
	CustomSettings: map<string, any>[];
	Description: string;
	ModelPlotConfig: model_plot_config_enabled;
	ModelSnapshotRetentionDays: long;
	RenormalizationWindowDays: long;
	ResultsRetentionDays: long;
}
/**namespace:XPack.MachineLearning.UpdateJob */
interface update_job_response extends response {
}
/**namespace:XPack.MachineLearning.UpdateModelSnapshot */
interface update_model_snapshot_request extends request {
	Description: string;
	Retain: boolean;
}
/**namespace:XPack.MachineLearning.UpdateModelSnapshot */
interface update_model_snapshot_response extends response {
	Model: model_snapshot;
}
/**namespace:XPack.MachineLearning.ValidateDetector */
interface validate_detector_request extends request {
	Detector: detector;
}
/**namespace:XPack.MachineLearning.ValidateDetector */
interface validate_detector_response extends response {
}
/**namespace:XPack.MachineLearning.ValidateJob */
interface validate_job_request extends request {
	AnalysisConfig: analysis_config;
	AnalysisLimits: analysis_limits;
	DataDescription: data_description;
	Description: string;
	ModelPlotConfig: model_plot_config;
	ModelSnapshotRetentionDays: long;
	ResultsIndexName: index_name;
}
/**namespace:XPack.MachineLearning.ValidateJob */
interface validate_job_response extends response {
}
/**namespace:XPack.Security.Authenticate */
interface authenticate_request extends request {
}
/**namespace:XPack.Security.Authenticate */
interface authenticate_response extends response {
	Username: string;
	Roles: string[];
	FullName: string;
	Email: string;
	Metadata: map<string, any>[];
}
/**namespace:XPack.Security.ClearCachedRealms */
interface clear_cached_realms_request extends request {
	Usernames: string[];
}
/**namespace:XPack.Security.ClearCachedRealms */
interface clear_cached_realms_response extends response {
	ClusterName: string;
	Nodes: map<string, security_node>[];
}
/**namespace:XPack.Security */
interface security_node {
	Name: string;
}
/**namespace:XPack.Security.RoleMapping.DeleteRoleMapping */
interface delete_role_mapping_request extends request {
	Refresh: Refresh;
}
/**namespace:XPack.Security.RoleMapping.DeleteRoleMapping */
interface delete_role_mapping_response extends response {
	Found: boolean;
}
/**namespace:XPack.Security.RoleMapping.GetRoleMapping */
interface get_role_mapping_request extends request {
}
/**namespace:XPack.Security.RoleMapping.GetRoleMapping */
interface get_role_mapping_response extends response {
	RoleMappings: map<string, x_pack_role_mapping>[];
}
/**namespace:XPack.Security.RoleMapping.GetRoleMapping */
interface x_pack_role_mapping {
	Metadata: map<string, any>[];
	Enabled: boolean;
	Roles: string[];
	Rules: role_mapping_rule_base;
}
/**namespace:XPack.Security.RoleMapping.Rules.Role */
interface role_mapping_rule_base {
}
/**namespace:XPack.Security.RoleMapping.PutRoleMapping */
interface put_role_mapping_request extends request {
	RunAs: string[];
	Metadata: map<string, any>[];
	Enabled: boolean;
	Roles: string[];
	Rules: role_mapping_rule_base;
	Refresh: Refresh;
}
/**namespace:XPack.Security.RoleMapping.PutRoleMapping */
interface put_role_mapping_response extends response {
	RoleMapping: put_role_mapping_status;
	Created: boolean;
}
/**namespace:XPack.Security.RoleMapping.PutRoleMapping */
interface put_role_mapping_status {
	Created: boolean;
}
/**namespace:XPack.Security.Role.ClearCachedRoles */
interface clear_cached_roles_request extends request {
}
/**namespace:XPack.Security.Role.ClearCachedRoles */
interface clear_cached_roles_response extends response {
	ClusterName: string;
	Nodes: map<string, security_node>[];
}
/**namespace:XPack.Security.Role.DeleteRole */
interface delete_role_request extends request {
	Refresh: Refresh;
}
/**namespace:XPack.Security.Role.DeleteRole */
interface delete_role_response extends response {
	Found: boolean;
}
/**namespace:XPack.Security.Role.GetRole */
interface get_role_request extends request {
}
/**namespace:XPack.Security.Role.GetRole */
interface get_role_response extends response {
	Roles: map<string, x_pack_role>[];
}
/**namespace:XPack.Security.Role.GetRole */
interface x_pack_role {
	Cluster: string[];
	RunAs: string[];
	Indices: indices_privileges[];
	Metadata: map<string, any>[];
}
/**namespace:XPack.Security.Role.PutRole */
interface put_role_request extends request {
	Cluster: string[];
	RunAs: string[];
	Indices: indices_privileges[];
	Metadata: map<string, any>[];
	Refresh: Refresh;
}
/**namespace:XPack.Security.Role.PutRole */
interface put_role_response extends response {
	Role: put_role_status;
}
/**namespace:XPack.Security.Role.PutRole */
interface put_role_status {
	Created: boolean;
}
/**namespace:XPack.Security.User.ChangePassword */
interface change_password_request extends request {
	Password: string;
	Refresh: Refresh;
}
/**namespace:XPack.Security.User.ChangePassword */
interface change_password_response extends response {
}
/**namespace:XPack.Security.User.DeleteUser */
interface delete_user_request extends request {
	Refresh: Refresh;
}
/**namespace:XPack.Security.User.DeleteUser */
interface delete_user_response extends response {
	Found: boolean;
}
/**namespace:XPack.Security.User.DisableUser */
interface disable_user_request extends request {
	Refresh: Refresh;
}
/**namespace:XPack.Security.User.DisableUser */
interface disable_user_response extends response {
}
/**namespace:XPack.Security.User.EnableUser */
interface enable_user_request extends request {
	Refresh: Refresh;
}
/**namespace:XPack.Security.User.EnableUser */
interface enable_user_response extends response {
}
/**namespace:XPack.Security.User.GetUserAccessToken */
interface get_user_access_token_request extends request {
	GrantType: AccessTokenGrantType;
	Scope: string;
}
/**namespace:XPack.Security.User.GetUserAccessToken */
interface get_user_access_token_response extends response {
	AccessToken: string;
	Type: string;
	ExpiresIn: long;
	Scope: string;
}
/**namespace:XPack.Security.User.GetUser */
interface get_user_request extends request {
}
/**namespace:XPack.Security.User.GetUser */
interface get_user_response extends response {
	Users: map<string, x_pack_user>[];
}
/**namespace:XPack.Security.User.GetUser */
interface x_pack_user {
	Username: string;
	Roles: string[];
	FullName: string;
	Email: string;
	Metadata: map<string, any>[];
}
/**namespace:XPack.Security.User.InvalidateUserAccessToken */
interface invalidate_user_access_token_request extends request {
}
/**namespace:XPack.Security.User.InvalidateUserAccessToken */
interface invalidate_user_access_token_response extends response {
	Created: boolean;
}
/**namespace:XPack.Security.User.PutUser */
interface put_user_request extends request {
	Password: string;
	Roles: string[];
	FullName: string;
	Email: string;
	Metadata: map<string, any>[];
	Refresh: Refresh;
}
/**namespace:XPack.Security.User.PutUser */
interface put_user_response extends response {
	User: put_user_status;
}
/**namespace:XPack.Security.User.PutUser */
interface put_user_status {
	Created: boolean;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface acknowledge_watch_request extends request {
	MasterTimeout: time;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface acknowledge_watch_response extends response {
	Status: watch_status;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface watch_status {
	Version: integer;
	State: activation_state;
	LastChecked: Date;
	LastMetCondition: Date;
	Actions: map<string, action_status>[];
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface activation_state {
	Timestamp: Date;
	Active: boolean;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface action_status {
	Acknowledgement: acknowledge_state;
	LastExecution: execution_state;
	LastSuccessfulExecution: execution_state;
	LastThrottle: throttle_state;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface acknowledge_state {
	Timestamp: Date;
	State: AcknowledgementState;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface execution_state {
	Timestamp: Date;
	Successful: boolean;
}
/**namespace:XPack.Watcher.AcknowledgeWatch */
interface throttle_state {
	Timestamp: Date;
	Reason: string;
}
/**namespace:XPack.Watcher.ActivateWatch */
interface activate_watch_request extends request {
	MasterTimeout: time;
}
/**namespace:XPack.Watcher.ActivateWatch */
interface activate_watch_response extends response {
	Status: activation_status;
}
/**namespace:XPack.Watcher.ActivateWatch */
interface activation_status {
	State: activation_state;
	Actions: map<string, action_status>[];
}
/**namespace:XPack.Watcher.DeactivateWatch */
interface deactivate_watch_request extends request {
	MasterTimeout: time;
}
/**namespace:XPack.Watcher.DeactivateWatch */
interface deactivate_watch_response extends response {
	Status: activation_status;
}
/**namespace:XPack.Watcher.DeleteWatch */
interface delete_watch_request extends request {
	MasterTimeout: time;
}
/**namespace:XPack.Watcher.DeleteWatch */
interface delete_watch_response extends response {
	Id: string;
	Version: integer;
	Found: boolean;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execute_watch_request extends request {
	TriggerData: schedule_trigger_event;
	IgnoreCondition: boolean;
	RecordExecution: boolean;
	AlternativeInput: map<string, any>[];
	ActionModes: map<string, ActionExecutionMode>[];
	SimulatedActions: simulated_actions;
	Watch: put_watch_request;
	Debug: boolean;
}
/**namespace:XPack.Watcher.Execution */
interface simulated_actions {
	UseAll: boolean;
	Actions: string[];
	All: simulated_actions;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execute_watch_response extends response {
	Id: string;
	WatchRecord: watch_record;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface watch_record {
	WatchId: string;
	Messages: string[];
	State: ActionExecutionState;
	TriggerEvent: trigger_event_result;
	Condition: condition_container;
	Input: input_container;
	Metadata: map<string, any>[];
	Result: execution_result;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface trigger_event_result {
	Type: string;
	TriggeredTime: Date;
	Manual: trigger_event_container;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execution_result {
	ExecutionTime: Date;
	ExecutionDuration: integer;
	Input: execution_result_input;
	Condition: execution_result_condition;
	Actions: execution_result_action[];
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execution_result_input {
	Type: InputType;
	Status: Status;
	Payload: map<string, any>[];
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execution_result_condition {
	Type: ConditionType;
	Status: Status;
	Met: boolean;
}
/**namespace:XPack.Watcher.ExecuteWatch */
interface execution_result_action {
	Id: string;
	Type: ActionType;
	Status: Status;
	Email: email_action_result;
	Index: index_action_result;
	Webhook: webhook_action_result;
	Logging: logging_action_result;
	PagerDuty: pager_duty_action_result;
	HipChat: hip_chat_action_result;
	Slack: slack_action_result;
	Reason: string;
}
/**namespace:XPack.Watcher.Execution.Email */
interface email_action_result {
	Reason: string;
	Account: string;
	Message: email_result;
}
/**namespace:XPack.Watcher.Execution.Email */
interface email_result {
	Id: string;
	SentDate: Date;
	From: string;
	To: string[];
	Cc: string[];
	Bcc: string[];
	ReplyTo: string[];
	Subject: string;
	Body: email_body;
	Priority: EmailPriority;
}
/**namespace:XPack.Watcher.Action.Email */
interface email_body {
	Text: string;
	Html: string;
}
/**namespace:XPack.Watcher.Execution.Index */
interface index_action_result {
	Id: string;
	Response: index_action_result_index_response;
}
/**namespace:XPack.Watcher.Execution.Index */
interface index_action_result_index_response {
	Index: index_name;
	Type: type_name;
	Version: integer;
	Created: boolean;
	Result: Result;
	Id: string;
}
/**namespace:XPack.Watcher.Execution.Webhook */
interface webhook_action_result {
	Request: http_input_request_result;
	Response: http_input_response_result;
}
/**namespace:XPack.Watcher.Execution */
interface http_input_request_result extends http_input_request {
}
/**namespace:XPack.Watcher.Execution */
interface http_input_response_result {
	StatusCode: integer;
	Headers: map<string, string[]>[];
	Body: string;
}
/**namespace:XPack.Watcher.Execution.Logging */
interface logging_action_result {
	LoggedText: string;
}
/**namespace:XPack.Watcher.Execution.PagerDuty */
interface pager_duty_action_result {
	SentEvent: pager_duty_action_event_result;
}
/**namespace:XPack.Watcher.Execution.PagerDuty */
interface pager_duty_action_event_result {
	Event: pager_duty_event;
	Reason: string;
	Request: http_input_request_result;
	Response: http_input_response_result;
}
/**namespace:XPack.Watcher.Action.PagerDuty */
interface pager_duty_event {
	Account: string;
	Description: string;
	EventType: PagerDutyEventType;
	IncidentKey: string;
	Client: string;
	ClientUrl: string;
	AttachPayload: boolean;
	Context: pager_duty_context[];
}
/**namespace:XPack.Watcher.Execution.HipChat */
interface hip_chat_action_result {
	Account: string;
	SentMessages: hip_chat_action_message_result[];
}
/**namespace:XPack.Watcher.Execution.HipChat */
interface hip_chat_action_message_result {
	Status: Status;
	Reason: string;
	Request: http_input_request_result;
	Response: http_input_response_result;
	Room: string;
	User: string;
	Message: hip_chat_message;
}
/**namespace:XPack.Watcher.Execution.Slack */
interface slack_action_result {
	Account: string;
	SentMessages: slack_action_message_result[];
}
/**namespace:XPack.Watcher.Execution.Slack */
interface slack_action_message_result {
	Status: Status;
	Reason: string;
	Request: http_input_request_result;
	Response: http_input_response_result;
	To: string;
	Message: slack_message;
}
/**namespace:XPack.Watcher.GetWatch */
interface get_watch_request extends request {
}
/**namespace:XPack.Watcher.GetWatch */
interface get_watch_response extends response {
	Found: boolean;
	Id: string;
	Status: watch_status;
	Watch: watch;
}
/**namespace:XPack.Watcher */
interface watch {
	Meta: map<string, any>[];
	Input: input_container;
	Condition: condition_container;
	Trigger: trigger_container;
	Transform: transform_container;
	Actions: map<string, action>[];
	Status: watch_status;
	ThrottlePeriod: string;
}
/**namespace:XPack.Watcher.Schedule */
interface cron_expression extends schedule_base {
}
/**namespace:XPack.Watcher.Schedule */
interface schedule_base {
}
/**namespace:XPack.Watcher.Schedule */
interface interval extends schedule_base {
	Factor: long;
	Unit: IntervalUnit;
}
/**namespace:XPack.Watcher.PutWatch */
interface put_watch_response extends response {
	Id: string;
	Version: integer;
	Created: boolean;
}
/**namespace:XPack.Watcher.RestartWatcher */
interface restart_watcher_request extends request {
}
/**namespace:XPack.Watcher.RestartWatcher */
interface restart_watcher_response extends response {
}
/**namespace:XPack.Watcher.StartWatcher */
interface start_watcher_request extends request {
}
/**namespace:XPack.Watcher.StartWatcher */
interface start_watcher_response extends response {
}
/**namespace:XPack.Watcher.StopWatcher */
interface stop_watcher_request extends request {
}
/**namespace:XPack.Watcher.StopWatcher */
interface stop_watcher_response extends response {
}
/**namespace:XPack.Watcher.WatcherStats */
interface watcher_stats_request extends request {
	EmitStacktraces: boolean;
}
/**namespace:XPack.Watcher.WatcherStats */
interface watcher_stats_response extends response {
	Stats: watcher_node_stats[];
	ManuallyStopped: boolean;
	ClusterName: string;
}
/**namespace:XPack.Watcher.WatcherStats */
interface watcher_node_stats {
	WatcherState: WatcherState;
	WatchCount: long;
	ExecutionThreadPool: execution_thread_pool;
	CurrentWatches: watch_record_stats[];
	QueuedWatches: watch_record_queued_stats[];
}
/**namespace:XPack.Watcher.WatcherStats */
interface execution_thread_pool {
	QueueSize: long;
	MaxSize: long;
}
/**namespace:XPack.Watcher.WatcherStats */
interface watch_record_stats extends watch_record_queued_stats {
	ExecutionPhase: ExecutionPhase;
}
/**namespace:XPack.Watcher.WatcherStats */
interface watch_record_queued_stats {
	WatchId: string;
	WatchRecordId: string;
	TriggeredTime: Date;
	ExecutionTime: Date;
}
/**namespace:DefaultLanguageConstruct */
/**namespace:DefaultLanguageConstruct */
enum HttpMethod {
	GET = 0,
	POST = 1,
	PUT = 2,
	DELETE = 3,
	HEAD = 4
}
/**namespace:DefaultLanguageConstruct */
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
/**namespace:DefaultLanguageConstruct */
enum Health {
	green = 0,
	yellow = 1,
	red = 2
}
/**namespace:DefaultLanguageConstruct */
enum Size {
	Raw = 0,
	k = 1,
	m = 2,
	g = 3,
	t = 4,
	p = 5
}
/**namespace:DefaultLanguageConstruct */
enum Level {
	cluster = 0,
	indices = 1,
	shards = 2
}
/**namespace:DefaultLanguageConstruct */
enum WaitForEvents {
	immediate = 0,
	urgent = 1,
	high = 2,
	normal = 3,
	low = 4,
	languid = 5
}
/**namespace:DefaultLanguageConstruct */
enum WaitForStatus {
	green = 0,
	yellow = 1,
	red = 2
}
/**namespace:DefaultLanguageConstruct */
enum ExpandWildcards {
	open = 0,
	closed = 1,
	none = 2,
	all = 3
}
/**namespace:DefaultLanguageConstruct */
enum ThreadType {
	cpu = 0,
	wait = 1,
	block = 2
}
/**namespace:DefaultLanguageConstruct */
enum GroupBy {
	nodes = 0,
	parents = 1
}
/**namespace:DefaultLanguageConstruct */
enum Refresh {
	true = 0,
	false = 1,
	wait_for = 2
}
/**namespace:DefaultLanguageConstruct */
enum VersionType {
	internal = 0,
	external = 1,
	external_gte = 2,
	force = 3
}
/**namespace:DefaultLanguageConstruct */
enum DefaultOperator {
	AND = 0,
	OR = 1
}
/**namespace:DefaultLanguageConstruct */
enum Conflicts {
	abort = 0,
	proceed = 1
}
/**namespace:DefaultLanguageConstruct */
enum SearchType {
	query_then_fetch = 0,
	dfs_query_then_fetch = 1
}
/**namespace:DefaultLanguageConstruct */
enum OpType {
	index = 0,
	create = 1
}
/**namespace:DefaultLanguageConstruct */
enum Format {
	detailed = 0,
	text = 1
}
/**namespace:DefaultLanguageConstruct */
enum SuggestMode {
	missing = 0,
	popular = 1,
	always = 2
}
/**namespace:DefaultLanguageConstruct */
interface request_configuration {
	RequestTimeout: time_span;
	PingTimeout: time_span;
	ContentType: string;
	Accept: string;
	MaxRetries: integer;
	ForceNode: uri;
	DisableSniff: boolean;
	DisablePing: boolean;
	DisableDirectStreaming: boolean;
	AllowedStatusCodes: integer[];
	BasicAuthenticationCredentials: basic_authentication_credentials;
	EnableHttpPipelining: boolean;
	RunAs: string;
	ClientCertificates: any;
	ThrowExceptions: boolean;
}
/**namespace:DefaultLanguageConstruct */
interface basic_authentication_credentials {
	Username: string;
	Password: string;
}
/**namespace:DefaultLanguageConstruct */
interface server_error {
	Error: error;
	Status: integer;
}
/**namespace:DefaultLanguageConstruct */
interface error extends error_cause {
	RootCause: error_cause[];
	Headers: map<string, string>[];
}
/**namespace:DefaultLanguageConstruct */
interface error_cause {
	Reason: string;
	Type: string;
	CausedBy: error_cause;
	StackTrace: string;
	Metadata: error_cause_metadata;
}
/**namespace:DefaultLanguageConstruct */
interface shard_failure {
	Reason: error_cause;
	Shard: integer;
	Index: string;
	Node: string;
	Status: string;
}
/**namespace:DefaultLanguageConstruct */
interface map<t_key, t_value> {
	Key: t_key;
	Value: t_value;
}
/**namespace:DefaultLanguageConstruct */
interface error_cause_metadata {
	LicensedExpiredFeature: string;
	Index: string;
	IndexUUID: string;
	ResourceType: string;
	ResourceId: string[];
	Shard: integer;
	FailedShards: shard_failure[];
	Line: integer;
	Column: integer;
	BytesWanted: long;
	BytesLimit: long;
	Phase: string;
	Grouped: boolean;
	ScriptStack: string[];
	Script: string;
	Language: string;
}
/**namespace:Cluster.NodesStats */
interface extended_memory_stats extends memory_stats {
	FreePercent: integer;
	UsedPercent: integer;
}
/**namespace:Cluster.NodesStats */
interface memory_stats {
	Total: string;
	TotalInBytes: long;
	Free: string;
	FreeInBytes: long;
	Used: string;
	UsedInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface c_p_u_stats {
	LoadAverage: load_average_stats;
	Percent: float;
}
/**namespace:Cluster.NodesStats */
interface load_average_stats {
	OneMinute: float;
	FiveMinute: float;
	FifteenMinute: float;
}
/**namespace:Cluster.NodesStats */
interface thread_stats {
	Count: long;
	PeakCount: long;
}
/**namespace:Cluster.NodesStats */
interface garbage_collection_stats {
	Collectors: map<string, garbage_collection_generation_stats>[];
}
/**namespace:Cluster.NodesStats */
interface garbage_collection_generation_stats {
	CollectionCount: long;
	CollectionTime: string;
	CollectionTimeInMilliseconds: long;
}
/**namespace:Cluster.NodesStats */
interface node_buffer_pool {
	Count: long;
	Used: string;
	UsedInBytes: long;
	TotalCapacity: string;
	TotalCapacityInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface jvm_classes_stats {
	CurrentLoadedCount: long;
	TotalLoadedCount: long;
	TotalUnloadedCount: long;
}
/**namespace:Cluster.NodesStats */
interface j_v_m_pool {
	Used: string;
	UsedInBytes: long;
	Max: string;
	MaxInBytes: long;
	PeakUsed: string;
	PeakUsedInBytes: long;
	PeakMax: string;
	PeakMaxInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface total_file_system_stats {
	Available: string;
	AvailableInBytes: long;
	Free: string;
	FreeInBytes: long;
	Total: string;
	TotalInBytes: long;
}
/**namespace:Cluster.NodesStats */
interface data_path_stats {
	Path: string;
	Mount: string;
	Type: string;
	Total: string;
	TotalInBytes: long;
	Free: string;
	FreeInBytes: long;
	Available: string;
	AvailableInBytes: long;
	DiskReads: long;
	DiskWrites: long;
	DiskReadSize: string;
	DiskReadSizeInBytes: long;
	DiskWriteSize: string;
	DiskWriteSizeInBytes: long;
	DiskQueue: string;
}
/**namespace:CommonAbstractions.Infer.Indices */
interface all_indices_marker {
}
/**namespace:CommonAbstractions.Infer.Indices */
interface many_indices {
	Indices: index_name[];
}
/**namespace:CommonAbstractions.Infer.Types */
interface all_types_marker {
}
/**namespace:CommonAbstractions.Infer.Types */
interface many_types {
	Types: type_name[];
}
/**namespace:XPack.Info.XPackUsage */
interface job_statistics {
	Total: double;
	Minimum: double;
	Maximum: double;
	Average: double;
}
/**namespace:XPack.Info.XPackUsage */
interface data_feed {
	Count: long;
}
/**namespace:XPack.Info.XPackUsage */
interface alerting_execution {
	Actions: map<string, execution_action>[];
}
/**namespace:XPack.Info.XPackUsage */
interface execution_action {
	Total: long;
	TotalInMilliseconds: long;
}
/**namespace:XPack.Info.XPackUsage */
interface alerting_count {
	Total: long;
	Active: long;
}
/**namespace:XPack.Info.XPackUsage */
interface security_feature_toggle {
	Enabled: boolean;
}
/**namespace:XPack.Info.XPackUsage */
interface ssl_usage {
	Http: security_feature_toggle;
	Transport: security_feature_toggle;
}
/**namespace:XPack.Info.XPackUsage */
interface ip_filter_usage {
	Http: boolean;
	Transport: boolean;
}
/**namespace:XPack.Info.XPackUsage */
interface audit_usage extends security_feature_toggle {
	Outputs: string[];
}
/**namespace:XPack.Info.XPackUsage */
interface role_usage {
	Dls: boolean;
	Fls: boolean;
	Size: long;
}
/**namespace:XPack.Info.XPackUsage */
interface realm_usage extends x_pack_usage {
	Name: string[];
	Size: long[];
	Order: long[];
}
