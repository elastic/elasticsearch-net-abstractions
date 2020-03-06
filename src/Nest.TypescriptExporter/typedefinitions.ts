
function class_serializer(ns: string) {return function (ns: any){}}
function rest_spec_name(ns: string) {return function (ns: any){}}
function prop_serializer(ns: string) {return function (ns: any, x:any){}}
function request_parameter() {return function (ns: any, x:any){}}
function namespace(ns: string) {return function (ns: any){}}

interface Uri {}
interface Date {}
interface TimeSpan {}
interface SourceDocument {}
@namespace("common")
class ErrorCause {
	additional_properties: Dictionary<string, any>;
	bytes_limit: long;
	bytes_wanted: long;
	caused_by: ErrorCause;
	column: integer;
	failed_shards: ShardFailure[];
	grouped: boolean;
	index: string;
	index_u_u_i_d: string;
	language: string;
	licensed_expired_feature: string;
	line: integer;
	phase: string;
	reason: string;
	resource_id: string[];
	resource_type: string;
	script: string;
	script_stack: string[];
	shard: integer;
	stack_trace: string;
	type: string;
}
@namespace("common")
class MainError extends ErrorCause {
	headers: Dictionary<string, string>;
	root_cause: ErrorCause[];
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
/** namespace:aggregations.bucket.auto_date_histogram **/
enum MinimumInterval {
	second = 0,
	minute = 1,
	hour = 2,
	day = 3,
	month = 4,
	year = 5
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
/** namespace:aggregations.bucket.geo_tile_grid **/
enum GeoTilePrecision {
	Precision0 = 0,
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
	Precision12 = 12,
	Precision13 = 13,
	Precision14 = 14,
	Precision15 = 15,
	Precision16 = 16,
	Precision17 = 17,
	Precision18 = 18,
	Precision19 = 19,
	Precision20 = 20,
	Precision21 = 21,
	Precision22 = 22,
	Precision23 = 23,
	Precision24 = 24,
	Precision25 = 25,
	Precision26 = 26,
	Precision27 = 27,
	Precision28 = 28,
	Precision29 = 29
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
/** namespace:aggregations.metric.weighted_average **/
enum ValueType {
	string = 0,
	long = 1,
	double = 2,
	number = 3,
	date = 4,
	date_nanos = 5,
	ip = 6,
	numeric = 7,
	geo_point = 8,
	boolean = 9
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
/** namespace:analysis.tokenizers **/
enum NoriDecompoundMode {
	discard = 0,
	none = 1,
	mixed = 2
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
enum IcuNormalizationType {
	nfc = 0,
	nfkc = 1,
	nfkc_cf = 2
}
/** namespace:analysis.plugins.icu.normalization **/
enum IcuNormalizationMode {
	decompose = 0,
	compose = 1
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
	double_metaphone = 1,
	soundex = 2,
	refined_soundex = 3,
	caverphone1 = 4,
	caverphone2 = 5,
	cologne = 6,
	nysiis = 7,
	koelnerphonetik = 8,
	haasephonetik = 9,
	beider_morse = 10,
	daitch_mokotoff = 11
}
/** namespace:analysis.plugins.phonetic **/
enum PhoneticLanguage {
	any = 0,
	comomon = 1,
	cyrillic = 2,
	english = 3,
	french = 4,
	german = 5,
	hebrew = 6,
	hungarian = 7,
	polish = 8,
	romanian = 9,
	russian = 10,
	spanish = 11
}
/** namespace:analysis.plugins.phonetic **/
enum PhoneticNameType {
	generic = 0,
	ashkenazi = 1,
	sephardic = 2
}
/** namespace:analysis.plugins.phonetic **/
enum PhoneticRuleType {
	approx = 0,
	exact = 1
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
/** namespace:analysis.token_filters **/
enum KeepTypesMode {
	include = 0,
	exclude = 1
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
	no = 1,
	worse_balance = 2,
	throttled = 3,
	awaiting_info = 4,
	allocation_delayed = 5,
	no_valid_shard_copy = 6,
	no_attempt = 7
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
	REALLOCATED_REPLICA = 11,
	PRIMARY_FAILED = 12,
	FORCED_EMPTY_PRIMARY = 13,
	MANUAL_ALLOCATION = 14
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
	ingest = 3,
	ml = 4
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
/** namespace:common_options.shape **/
enum ShapeRelation {
	intersects = 0,
	disjoint = 1,
	within = 2
}
/** namespace:query_dsl **/
enum Operator {
	and = 0,
	or = 1
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
/** namespace:query_dsl.multi_term_query_rewrite **/
enum RewriteMultiTerm {
	constant_score = 0,
	scoring_boolean = 1,
	constant_score_boolean = 2,
	top_terms_N = 3,
	top_terms_boost_N = 4,
	top_terms_blended_freqs_N = 5
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
/** namespace:search.search.highlighting **/
enum BoundaryScanner {
	chars = 0,
	sentence = 1,
	word = 2
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
enum HighlighterEncoder {
	default = 0,
	html = 1
}
/** namespace:search.search.sort **/
enum SortMode {
	min = 0,
	max = 1,
	sum = 2,
	avg = 3,
	median = 4
}
/** namespace:search.search.sort **/
enum NumericType {
	long = 0,
	double = 1,
	date = 2,
	date_nanos = 3
}
/** namespace:search.search.sort **/
enum SortOrder {
	asc = 0,
	desc = 1
}
/** namespace:query_dsl.joining.has_child **/
enum ChildScoreMode {
	none = 0,
	avg = 1,
	sum = 2,
	max = 3,
	min = 4
}
/** namespace:query_dsl.full_text.multi_match **/
enum ZeroTermsQuery {
	all = 0,
	none = 1
}
/** namespace:query_dsl.full_text.multi_match **/
enum TextQueryType {
	best_fields = 0,
	most_fields = 1,
	cross_fields = 2,
	phrase = 3,
	phrase_prefix = 4,
	bool_prefix = 5
}
/** namespace:query_dsl.joining.nested **/
enum NestedScoreMode {
	avg = 0,
	sum = 1,
	min = 2,
	max = 3,
	none = 4
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
/** namespace:mapping.dynamic_template **/
enum MatchType {
	simple = 0,
	regex = 1
}
/** namespace:indices.monitoring.indices_shard_stores **/
enum ShardStoreAllocation {
	primary = 0,
	replica = 1,
	unused = 2
}
/** namespace:indices.monitoring.indices_stats **/
enum ShardRoutingState {
	UNASSIGNED = 0,
	INITIALIZING = 1,
	STARTED = 2,
	RELOCATING = 3
}
/** namespace:ingest.processors **/
enum ShapeType {
	geo_shape = 0,
	shape = 1
}
/** namespace:ingest.processors **/
enum ConvertProcessorType {
	integer = 0,
	long = 1,
	float = 2,
	double = 3,
	string = 4,
	boolean = 5,
	auto = 6
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
/** namespace:mapping **/
enum TermVectorOption {
	no = 0,
	yes = 1,
	with_offsets = 2,
	with_positions = 3,
	with_positions_offsets = 4,
	with_positions_offsets_payloads = 5
}
/** namespace:mapping.types.core.text **/
enum IndexOptions {
	docs = 0,
	freqs = 1,
	positions = 2,
	offsets = 3
}
/** namespace:modules.indices.fielddata.numeric **/
enum NumericFielddataFormat {
	array = 0,
	disabled = 1
}
/** namespace:modules.indices.fielddata **/
enum FielddataLoading {
	eager = 0,
	eager_global_ordinals = 1
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
	date_range = 4,
	ip_range = 5
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
	search_as_you_type = 7,
	date = 8,
	date_nanos = 9,
	boolean = 10,
	completion = 11,
	nested = 12,
	object = 13,
	murmur3 = 14,
	token_count = 15,
	percolator = 16,
	integer = 17,
	long = 18,
	short = 19,
	byte = 20,
	float = 21,
	half_float = 22,
	scaled_float = 23,
	double = 24,
	integer_range = 25,
	float_range = 26,
	long_range = 27,
	double_range = 28,
	date_range = 29,
	ip_range = 30,
	alias = 31,
	join = 32,
	rank_feature = 33,
	rank_features = 34,
	flattened = 35,
	shape = 36
}
/** namespace:mapping.types.geo.geo_shape **/
enum GeoOrientation {
	ClockWise = 0,
	CounterClockWise = 1
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
/** namespace:modules.indices.fielddata.string **/
enum StringFielddataFormat {
	paged_bytes = 0,
	disabled = 1
}
/** namespace:mapping.types.specialized.shape **/
enum ShapeOrientation {
	ClockWise = 0,
	CounterClockWise = 1
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
/** namespace:modules.indices.fielddata.geo_point **/
enum GeoPointFielddataFormat {
	array = 0,
	doc_values = 1,
	compressed = 2,
	disabled = 3
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
/** namespace:common **/
enum GeoShapeFormat {
	GeoJson = 0,
	WellKnownText = 1
}
/** namespace:query_dsl.geo.w_k_t **/
enum CharacterType {
	Whitespace = 0,
	Alpha = 1,
	Comment = 2
}
/** namespace:query_dsl.geo.w_k_t **/
enum TokenType {
	None = 0,
	Word = 1,
	LParen = 2,
	RParen = 3,
	Comma = 4
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
/** namespace:x_pack.machine_learning.job.detectors **/
enum RuleAction {
	skip_result = 0,
	skip_model_update = 1
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum AppliesTo {
	actual = 0,
	typical = 1,
	diff_from_typical = 2,
	time = 3
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum ConditionOperator {
	gt = 0,
	gte = 1,
	lt = 2,
	lte = 3
}
/** namespace:x_pack.machine_learning.job.detectors **/
enum RuleFilterType {
	include = 0,
	exclude = 1
}
/** namespace:x_pack.machine_learning.put_job **/
enum ExcludeFrequent {
	all = 0,
	none = 1,
	by = 2,
	over = 3
}
/** namespace:search.search.rescoring **/
enum ScoreMode {
	avg = 0,
	max = 1,
	min = 2,
	multiply = 3,
	total = 4
}
/** namespace:search.suggesters.term_suggester **/
enum SuggestSort {
	score = 0,
	frequency = 1
}
/** namespace:search.suggesters.term_suggester **/
enum StringDistance {
	internal = 0,
	damerau_levenshtein = 1,
	levenshtein = 2,
	jaro_winkler = 3,
	ngram = 4
}
/** namespace:x_pack.roll_up.rollup_configuration **/
enum RollupMetric {
	min = 0,
	max = 1,
	sum = 2,
	avg = 3,
	value_count = 4
}
/** namespace:x_pack.security.user.get_user_access_token **/
enum AccessTokenGrantType {
	password = 0
}
/** namespace:x_pack.watcher.execution **/
enum ActionExecutionMode {
	simulate = 0,
	force_simulate = 1,
	execute = 2,
	force_execute = 3,
	skip = 4
}
/** namespace:x_pack.watcher.condition **/
enum Quantifier {
	some = 0,
	all = 1
}
/** namespace:x_pack.watcher.action **/
enum ActionType {
	email = 0,
	webhook = 1,
	index = 2,
	logging = 3,
	slack = 4,
	pagerduty = 5
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
enum ConnectionScheme {
	http = 0,
	https = 1
}
/** namespace:x_pack.watcher.input **/
enum ResponseContentType {
	json = 0,
	yaml = 1,
	text = 2
}
/** namespace:x_pack.watcher.acknowledge_watch **/
enum AcknowledgementState {
	awaits_successful_execution = 0,
	ackable = 1,
	acked = 2
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
/** namespace:search.search.highlighting **/
enum HighlighterType {
	plain = 0,
	fvh = 1,
	unified = 2
}
/** namespace:search.search.hits **/
enum TotalHitsRelation {
	eq = 0,
	gte = 1
}
/** namespace:search.search.sort **/
enum SortSpecialField {
	_score = 0,
	_doc = 1
}
/** namespace:x_pack.cross_cluster_replication.follow.follow_info **/
enum FollowerIndexStatus {
	active = 0,
	paused = 1
}
/** namespace:x_pack.ilm.get_status **/
enum LifecycleOperationMode {
	RUNNING = 0,
	STOPPING = 1,
	STOPPED = 2
}
/** namespace:x_pack.license.get_license **/
enum LicenseStatus {
	active = 0,
	valid = 1,
	invalid = 2,
	expired = 3
}
/** namespace:x_pack.machine_learning.datafeed **/
enum DatafeedState {
	started = 0,
	stopped = 1,
	starting = 2,
	stopping = 3
}
/** namespace:x_pack.machine_learning.job.config **/
enum MemoryStatus {
	ok = 0,
	soft_limit = 1,
	hard_limit = 2
}
/** namespace:x_pack.machine_learning.job.config **/
enum JobState {
	closing = 0,
	closed = 1,
	opened = 2,
	failed = 3,
	opening = 4
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
/** namespace:x_pack.migration.deprecation_info **/
enum DeprecationWarningLevel {
	none = 0,
	info = 1,
	warning = 2,
	critical = 3
}
/** namespace:x_pack.roll_up.get_rollup_job **/
enum IndexingJobState {
	started = 0,
	indexing = 1,
	stopping = 2,
	stopped = 3,
	aborting = 4
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
enum ConditionType {
	always = 0,
	never = 1,
	script = 2,
	compare = 3,
	array_compare = 4
}
/** namespace:x_pack.watcher.execution **/
enum Status {
	success = 0,
	failure = 1,
	simulated = 2,
	throttled = 3
}
/** namespace:x_pack.watcher.input **/
enum InputType {
	http = 0,
	search = 1,
	simple = 2
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
/** namespace:x_pack.watcher.watcher_stats **/
enum WatcherState {
	stopped = 0,
	starting = 1,
	started = 2,
	stopping = 3
}
@namespace("document.multiple.bulk.bulk_response_item")
class BulkResponseItemBase {
	error: MainError;
	_id: string;
	_index: string;
	operation: string;
	_primary_term: long;
	result: string;
	_seq_no: long;
	_shards: ShardStatistics;
	status: integer;
	_type: string;
	_version: long;
	is_valid: boolean;
}
@namespace("x_pack.security.role_mapping.rules.role")
class RoleMappingRuleBase {
}
@namespace("analysis.analyzers")
class AnalyzerBase implements IAnalyzer {
	type: string;
	version: string;
}
@namespace("analysis.char_filters")
class CharFilterBase implements ICharFilter {
	type: string;
	version: string;
}
@namespace("analysis.token_filters")
class TokenFilterBase implements ITokenFilter {
	type: string;
	version: string;
}
@namespace("analysis.tokenizers")
class TokenizerBase implements ITokenizer {
	type: string;
	version: string;
}
@namespace("x_pack.watcher.schedule")
class ScheduleBase {
}
@namespace("common_abstractions.request")
class RequestBase {
}
@namespace("common_abstractions.response")
class ResponseBase implements IResponse {
	debug_information: string;
	is_valid: boolean;
	server_error: ServerError;
}
@namespace("analysis.token_filters.compound_word")
class CompoundWordTokenFilterBase extends TokenFilterBase {
	hyphenation_patterns_path: string;
	max_subword_size: integer;
	min_subword_size: integer;
	min_word_size: integer;
	only_longest_match: boolean;
	word_list: string[];
	word_list_path: string;
}
@namespace("")
class PlainRequestBase<TParameters> extends RequestBase {
	error_trace: boolean;
	filter_path: string[];
	human: boolean;
	pretty: boolean;
	source_query_string: string;
}
@namespace("cluster")
class NodesResponseBase extends ResponseBase implements IResponse {
	_nodes: NodeStatistics;
}
@namespace("common_abstractions.response")
class AcknowledgedResponseBase extends ResponseBase implements IResponse {
	acknowledged: boolean;
	is_valid: boolean;
}
@namespace("common_abstractions.response")
class ShardsOperationResponseBase extends ResponseBase implements IResponse {
	_shards: ShardStatistics;
}
@namespace("document.single")
class WriteResponseBase extends ResponseBase implements IResponse {
	_id: string;
	_index: string;
	_primary_term: long;
	result: Result;
	_seq_no: long;
	_shards: ShardStatistics;
	_type: string;
	_version: long;
}
@namespace("mapping.types")
class PropertyBase implements IProperty {
	local_metadata: Dictionary<string, any>;
	name: PropertyName;
	type: string;
}
@namespace("common_abstractions.response")
class DynamicResponseBase extends ResponseBase {
}
@namespace("common_abstractions.response")
class DictionaryResponseBase<TKey, TValue> extends ResponseBase {
}
@namespace("common_abstractions.response")
class IndicesResponseBase extends AcknowledgedResponseBase implements IResponse {
	_shards: ShardStatistics;
}
@namespace("mapping.types")
class CorePropertyBase extends PropertyBase {
	copy_to: Field[];
	fields: Dictionary<PropertyName, IProperty>;
	similarity: string;
	store: boolean;
}
@namespace("mapping.types")
class DocValuesPropertyBase extends CorePropertyBase {
	doc_values: boolean;
}
@namespace("mapping.types.core.range")
class RangePropertyBase extends DocValuesPropertyBase {
	boost: double;
	coerce: boolean;
	index: boolean;
}
/** namespace:analysis.analyzers **/
interface IAnalyzer {
	type: string;
	version: string;
}
/** namespace:analysis.char_filters **/
interface ICharFilter {
	type: string;
	version: string;
}
/** namespace:analysis.token_filters **/
interface ITokenFilter {
	type: string;
	version: string;
}
/** namespace:analysis.tokenizers **/
interface ITokenizer {
	type: string;
	version: string;
}
@namespace("common_options.scripting")
class Script {
	lang: string;
	params: Dictionary<string, any>;
}
/** namespace:cat **/
interface ICatRecord {
}
@namespace("cluster.cluster_reroute.commands")
class ClusterRerouteCommand {
	name: string;
}
@namespace("document.multiple.bulk.bulk_operation")
class BulkOperation {
	_id: Id;
	_index: IndexName;
	operation: string;
	retry_on_conflict: integer;
	routing: Routing;
	version: long;
	version_type: VersionType;
}
@namespace("query_dsl.abstractions.container")
class QueryContainer {
	bool: BoolQuery;
	boosting: BoostingQuery;
	common: CommonTermsQuery;
	constant_score: ConstantScoreQuery;
	dis_max: DisMaxQuery;
	exists: ExistsQuery;
	function_score: FunctionScoreQuery;
	fuzzy: FuzzyQuery;
	geo_bounding_box: GeoBoundingBoxQuery;
	geo_distance: GeoDistanceQuery;
	geo_polygon: GeoPolygonQuery;
	geo_shape: GeoShapeQuery;
	shape: ShapeQuery;
	has_child: HasChildQuery;
	has_parent: HasParentQuery;
	ids: IdsQuery;
	intervals: IntervalsQuery;
	is_conditionless: boolean;
	is_strict: boolean;
	is_verbatim: boolean;
	is_writable: boolean;
	match: MatchQuery;
	match_all: MatchAllQuery;
	match_none: MatchNoneQuery;
	match_phrase: MatchPhraseQuery;
	match_phrase_prefix: MatchPhrasePrefixQuery;
	more_like_this: MoreLikeThisQuery;
	multi_match: MultiMatchQuery;
	nested: NestedQuery;
	parent_id: ParentIdQuery;
	percolate: PercolateQuery;
	prefix: PrefixQuery;
	query_string: QueryStringQuery;
	range: RangeQuery;
	raw_query: RawQuery;
	regexp: RegexpQuery;
	script: ScriptQuery;
	script_score: ScriptScoreQuery;
	simple_query_string: SimpleQueryStringQuery;
	span_containing: SpanContainingQuery;
	field_masking_span: SpanFieldMaskingQuery;
	span_first: SpanFirstQuery;
	span_multi: SpanMultiTermQuery;
	span_near: SpanNearQuery;
	span_not: SpanNotQuery;
	span_or: SpanOrQuery;
	span_term: SpanTermQuery;
	span_within: SpanWithinQuery;
	term: TermQuery;
	terms: TermsQuery;
	terms_set: TermsSetQuery;
	wildcard: WildcardQuery;
	rank_feature: RankFeatureQuery;
	distance_feature: DistanceFeatureQuery;
	pinned: PinnedQuery;
}
@namespace("query_dsl.abstractions.query")
class Query {
	boost: double;
	conditionless: boolean;
	is_strict: boolean;
	is_verbatim: boolean;
	is_writable: boolean;
	_name: string;
}
@namespace("query_dsl.compound.function_score.functions")
class ScoreFunction {
	filter: QueryContainer;
	weight: double;
}
@namespace("query_dsl.geo.bounding_box")
class BoundingBox {
	bottom_right: GeoLocation;
	top_left: GeoLocation;
	wkt: string;
}
@namespace("query_dsl.abstractions.field_lookup")
class FieldLookup {
	id: Id;
	index: IndexName;
	path: Field;
	routing: Routing;
}
@namespace("query_dsl.geo.shape")
class GeoShape {
	type: string;
}
@namespace("search.search.inner_hits")
class InnerHits {
	collapse: FieldCollapse;
	docvalue_fields: Field[];
	explain: boolean;
	from: integer;
	highlight: Highlight;
	ignore_unmapped: boolean;
	name: string;
	script_fields: Dictionary<string, ScriptField>;
	size: integer;
	sort: Sort[];
	_source: Union<boolean, SourceFilter>;
	version: boolean;
}
@namespace("search.search.collapsing")
class FieldCollapse {
	field: Field;
	inner_hits: InnerHits;
	max_concurrent_group_searches: integer;
}
@namespace("search.search.highlighting")
class Highlight {
	boundary_chars: string;
	boundary_max_scan: integer;
	boundary_scanner: BoundaryScanner;
	boundary_scanner_locale: string;
	encoder: HighlighterEncoder;
	fields: Dictionary<Field, HighlightField>;
	fragmenter: HighlighterFragmenter;
	fragment_offset: integer;
	fragment_size: integer;
	max_fragment_length: integer;
	no_match_size: integer;
	number_of_fragments: integer;
	order: HighlighterOrder;
	post_tags: string[];
	pre_tags: string[];
	require_field_match: boolean;
	tags_schema: HighlighterTagsSchema;
}
@namespace("search.search.highlighting")
class HighlightField {
	boundary_chars: string;
	boundary_max_scan: integer;
	boundary_scanner: BoundaryScanner;
	boundary_scanner_locale: string;
	field: Field;
	force_source: boolean;
	fragmenter: HighlighterFragmenter;
	fragment_offset: integer;
	fragment_size: integer;
	highlight_query: QueryContainer;
	matched_fields: Field[];
	max_fragment_length: integer;
	no_match_size: integer;
	number_of_fragments: integer;
	order: HighlighterOrder;
	phrase_limit: integer;
	post_tags: string[];
	pre_tags: string[];
	require_field_match: boolean;
	tags_schema: HighlighterTagsSchema;
	type: Union<HighlighterType, string>;
}
@namespace("common_options.scripting")
class ScriptField {
	script: Script;
}
@namespace("search.search.sort")
class Sort {
	missing: any;
	mode: SortMode;
	numeric_type: NumericType;
	nested: NestedSort;
	order: SortOrder;
	sort_key: Field;
}
@namespace("search.search.sort")
class NestedSort {
	filter: QueryContainer;
	nested: NestedSort;
	path: Field;
}
@namespace("search.search.source_filtering")
class SourceFilter {
	excludes: Field[];
	includes: Field[];
}
@namespace("query_dsl.full_text.intervals")
class IntervalsContainer {
	all_of: IntervalsAllOf;
	any_of: IntervalsAnyOf;
	match: IntervalsMatch;
	prefix: IntervalsPrefix;
	wildcard: IntervalsWildcard;
}
@namespace("common_abstractions.fluent")
class Descriptor {
}
@namespace("query_dsl.full_text.intervals")
class Intervals {
	filter: IntervalsFilter;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsFilter {
	after: IntervalsContainer;
	before: IntervalsContainer;
	contained_by: IntervalsContainer;
	containing: IntervalsContainer;
	not_contained_by: IntervalsContainer;
	not_containing: IntervalsContainer;
	not_overlapping: IntervalsContainer;
	overlapping: IntervalsContainer;
	script: Script;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsNoFilter {
}
@namespace("common_options.fuzziness")
class Fuzziness {
	auto: boolean;
	low: integer;
	high: integer;
	edit_distance: integer;
	ratio: double;
}
@namespace("query_dsl.specialized.more_like_this.like")
class LikeDocument {
	doc: any;
	fields: Field[];
	_id: Id;
	_index: IndexName;
	per_field_analyzer: Dictionary<Field, string>;
	routing: Routing;
}
@namespace("query_dsl.specialized.rank_feature")
class RankFeatureFunction {
}
@namespace("common_options.date_math")
class DateMath extends String {}
@namespace("search.scroll.scroll")
class SlicedScroll {
	field: Field;
	id: integer;
	max: integer;
}
@namespace("document.multiple.multi_get.request")
class MultiGetOperation {
	can_be_flattened: boolean;
	_id: Id;
	_index: IndexName;
	routing: string;
	_source: Union<boolean, SourceFilter>;
	stored_fields: Field[];
	version: long;
	version_type: VersionType;
}
@namespace("document.multiple.multi_get.response")
class MultiGetHit<TDocument> {
	error: MainError;
	found: boolean;
	id: string;
	index: string;
	routing: string;
	source: TDocument;
	type: string;
	version: long;
	sequence_number: long;
	primary_term: long;
}
@namespace("document.multiple.multi_term_vectors")
class MultiTermVectorOperation {
	doc: any;
	field_statistics: boolean;
	filter: TermVectorFilter;
	_id: Id;
	_index: IndexName;
	offsets: boolean;
	payloads: boolean;
	positions: boolean;
	routing: Routing;
	fields: Field[];
	term_statistics: boolean;
	version: long;
	version_type: VersionType;
}
@namespace("document.single.term_vectors")
class TermVectorFilter {
	max_doc_freq: integer;
	max_num_terms: integer;
	max_term_freq: integer;
	max_word_length: integer;
	min_doc_freq: integer;
	min_term_freq: integer;
	min_word_length: integer;
}
@namespace("document.single.term_vectors")
class TermVectors {
	found: boolean;
	id: string;
	index: string;
	term_vectors: Dictionary<Field, TermVector>;
	took: long;
	version: long;
}
@namespace("document.multiple.reindex_on_server")
class ReindexDestination {
	index: IndexName;
	op_type: OpType;
	routing: ReindexRouting;
	version_type: VersionType;
}
@namespace("document.multiple.reindex_on_server")
class ReindexSource {
	index: Indices;
	query: QueryContainer;
	remote: RemoteSource;
	size: integer;
	slice: SlicedScroll;
	sort: Sort[];
	_source: Field[];
}
@namespace("document.multiple.reindex_on_server")
class RemoteSource {
	host: Uri;
	password: string;
	username: string;
}
@namespace("common_abstractions.lazy_document")
class LazyDocument {
}
@namespace("search.explain")
class InlineGet<TDocument> {
	fields: Dictionary<string, LazyDocument>;
	found: boolean;
	_source: TDocument;
}
@namespace("indices.alias_management.alias.actions")
class AliasAction {
}
@namespace("indices.alias_management")
class Alias {
	filter: QueryContainer;
	index_routing: Routing;
	is_write_index: boolean;
	routing: Routing;
	search_routing: Routing;
}
@namespace("mapping")
class TypeMapping {
	all_field: AllField;
	date_detection: boolean;
	dynamic: Union<boolean, DynamicMapping>;
	dynamic_date_formats: string[];
	dynamic_templates: Dictionary<string, DynamicTemplate>;
	_field_names: FieldNamesField;
	index_field: IndexField;
	_meta: Dictionary<string, any>;
	numeric_detection: boolean;
	properties: Dictionary<PropertyName, IProperty>;
	_routing: RoutingField;
	_size: SizeField;
	_source: SourceField;
}
@namespace("mapping.meta_fields")
class FieldMapping {
}
@namespace("mapping.dynamic_template")
class DynamicTemplate {
	mapping: IProperty;
	match: string;
	match_mapping_type: string;
	match_pattern: MatchType;
	path_match: string;
	path_unmatch: string;
	unmatch: string;
}
@namespace("index_modules.index_settings")
class IndexState {
	aliases: Dictionary<IndexName, Alias>;
	mappings: TypeMapping;
	settings: Dictionary<string, any>;
}
@namespace("indices.index_management.rollover_index")
class RolloverConditions {
	max_age: Time;
	max_docs: long;
	max_size: string;
}
@namespace("indices.index_settings.index_templates.get_index_template")
class TemplateMapping {
	aliases: Dictionary<IndexName, Alias>;
	index_patterns: string[];
	mappings: TypeMapping;
	order: integer;
	settings: Dictionary<string, any>;
	version: integer;
}
@namespace("ingest")
class Pipeline {
	description: string;
	on_failure: Processor[];
	processors: Processor[];
}
@namespace("ingest")
class Processor {
	name: string;
	on_failure: Processor[];
	if: string;
	tag: string;
	ignore_failure: boolean;
}
@namespace("ingest.simulate_pipeline")
class SimulatePipelineDocument {
	_id: Id;
	_index: IndexName;
	_source: any;
}
@namespace("mapping.types")
class PropertyWithClrOrigin {
}
@namespace("modules.indices.fielddata")
class Fielddata {
	filter: FielddataFilter;
	loading: FielddataLoading;
}
@namespace("modules.indices.fielddata")
class FielddataFilter {
	frequency: FielddataFrequencyFilter;
	regex: FielddataRegexFilter;
}
@namespace("modules.indices.fielddata")
class FielddataFrequencyFilter {
	max: double;
	min: double;
	min_segment_size: integer;
}
@namespace("modules.indices.fielddata")
class FielddataRegexFilter {
	pattern: string;
}
@namespace("mapping.types.core.text")
class TextIndexPrefixes {
	max_chars: integer;
	min_chars: integer;
}
@namespace("mapping.types.specialized.completion")
class SuggestContext {
	name: string;
	path: Field;
	type: string;
}
@namespace("modules.indices.circuit_breaker")
class CircuitBreakerSettings {
	fielddata_limit: string;
	fielddata_overhead: float;
	request_limit: string;
	request_overhead: float;
	total_limit: string;
}
@namespace("modules.indices.recovery")
class IndicesRecoverySettings {
	compress: boolean;
	concurrent_small_file_streams: integer;
	concurrent_streams: integer;
	file_chunk_size: string;
	max_bytes_per_second: string;
	translog_operations: integer;
	translog_size: string;
}
@namespace("modules.indices")
class IndicesModuleSettings {
	circuit_breaker_settings: CircuitBreakerSettings;
	fielddata_settings: FielddataSettings;
	qeueries_cache_size: string;
	recovery_settings: IndicesRecoverySettings;
}
@namespace("modules.scripting.execute_painless_script")
class PainlessContextSetup {
	document: any;
	index: IndexName;
	query: QueryContainer;
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
@namespace("x_pack.cross_cluster_replication.auto_follow.get_auto_follow_pattern")
class AutoFollowPattern {
	follow_index_pattern: string;
	leader_index_patterns: string[];
	max_outstanding_read_requests: long;
	max_outstanding_write_requests: integer;
	read_poll_timeout: Time;
	max_read_request_operation_count: integer;
	max_read_request_size: string;
	max_retry_delay: Time;
	max_write_buffer_count: integer;
	max_write_buffer_size: string;
	max_write_request_operation_count: integer;
	max_write_request_size: string;
	remote_cluster: string;
}
@namespace("x_pack.graph.explore.request")
class Hop {
	connections: Hop;
	query: QueryContainer;
	vertices: GraphVertexDefinition[];
}
@namespace("x_pack.graph.explore.request")
class GraphVertexDefinition {
	exclude: string[];
	field: Field;
	include: GraphVertexInclude[];
	min_doc_count: long;
	shard_min_doc_count: long;
	size: integer;
}
@namespace("x_pack.graph.explore.request")
class GraphExploreControls {
	sample_diversity: SampleDiversity;
	sample_size: integer;
	timeout: Time;
	use_significance: boolean;
}
@namespace("x_pack.ilm.move_to_step")
class StepKey {
	action: string;
	name: string;
	phase: string;
}
@namespace("x_pack.ilm")
class Policy {
	phases: Phases;
}
@namespace("x_pack.ilm")
class Phases {
	cold: Phase;
	delete: Phase;
	hot: Phase;
	warm: Phase;
}
@namespace("x_pack.ilm")
class Phase {
	actions: Dictionary<string, LifecycleAction>;
	min_age: Time;
}
@namespace("x_pack.ilm.actions")
class LifecycleAction {
}
@namespace("x_pack.machine_learning.job")
class Page {
	from: integer;
	size: integer;
}
@namespace("aggregations")
class AggregationContainer {
	adjacency_matrix: AdjacencyMatrixAggregation;
	aggs: Dictionary<string, AggregationContainer>;
	avg: AverageAggregation;
	avg_bucket: AverageBucketAggregation;
	bucket_script: BucketScriptAggregation;
	bucket_selector: BucketSelectorAggregation;
	bucket_sort: BucketSortAggregation;
	cardinality: CardinalityAggregation;
	children: ChildrenAggregation;
	composite: CompositeAggregation;
	cumulative_sum: CumulativeSumAggregation;
	cumulative_cardinality: CumulativeCardinalityAggregation;
	date_histogram: DateHistogramAggregation;
	auto_date_histogram: AutoDateHistogramAggregation;
	date_range: DateRangeAggregation;
	derivative: DerivativeAggregation;
	extended_stats: ExtendedStatsAggregation;
	extended_stats_bucket: ExtendedStatsBucketAggregation;
	filter: FilterAggregation;
	filters: FiltersAggregation;
	geo_bounds: GeoBoundsAggregation;
	geo_centroid: GeoCentroidAggregation;
	geo_distance: GeoDistanceAggregation;
	geohash_grid: GeoHashGridAggregation;
	geotile_grid: GeoTileGridAggregation;
	global: GlobalAggregation;
	histogram: HistogramAggregation;
	ip_range: IpRangeAggregation;
	matrix_stats: MatrixStatsAggregation;
	max: MaxAggregation;
	max_bucket: MaxBucketAggregation;
	meta: Dictionary<string, any>;
	min: MinAggregation;
	min_bucket: MinBucketAggregation;
	missing: MissingAggregation;
	moving_avg: MovingAverageAggregation;
	moving_fn: MovingFunctionAggregation;
	nested: NestedAggregation;
	parent: ParentAggregation;
	percentile_ranks: PercentileRanksAggregation;
	percentiles: PercentilesAggregation;
	percentiles_bucket: PercentilesBucketAggregation;
	range: RangeAggregation;
	rare_terms: RareTermsAggregation;
	reverse_nested: ReverseNestedAggregation;
	sampler: SamplerAggregation;
	scripted_metric: ScriptedMetricAggregation;
	serial_diff: SerialDifferencingAggregation;
	significant_terms: SignificantTermsAggregation;
	significant_text: SignificantTextAggregation;
	stats: StatsAggregation;
	stats_bucket: StatsBucketAggregation;
	sum: SumAggregation;
	sum_bucket: SumBucketAggregation;
	terms: TermsAggregation;
	top_hits: TopHitsAggregation;
	value_count: ValueCountAggregation;
	weighted_avg: WeightedAverageAggregation;
	median_absolute_deviation: MedianAbsoluteDeviationAggregation;
}
@namespace("aggregations")
class Aggregation {
	meta: Dictionary<string, any>;
	name: string;
}
@namespace("aggregations.pipeline")
class BucketsPath {
}
@namespace("aggregations.bucket.composite")
class CompositeAggregationSource {
	field: Field;
	missing_bucket: boolean;
	name: string;
	order: SortOrder;
	source_type: string;
}
@namespace("aggregations.bucket.date_range")
class DateRangeExpression {
	from: DateMath;
	key: string;
	to: DateMath;
}
@namespace("common_options.range")
class AggregationRange {
	from: double;
	key: string;
	to: double;
}
@namespace("aggregations.bucket.ip_range")
class IpRangeAggregationRange {
	from: string;
	mask: string;
	to: string;
}
@namespace("aggregations.pipeline.moving_average.models")
class MovingAverageModel {
	name: string;
}
@namespace("aggregations.metric.percentiles.methods")
class PercentilesMethod {
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class ChiSquareHeuristic {
	background_is_superset: boolean;
	include_negatives: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class GoogleNormalizedDistanceHeuristic {
	background_is_superset: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class MutualInformationHeuristic {
	background_is_superset: boolean;
	include_negatives: boolean;
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class PercentageScoreHeuristic {
}
@namespace("aggregations.bucket.significant_terms.heuristics")
class ScriptedHeuristic {
	script: Script;
}
@namespace("aggregations.metric.weighted_average")
class WeightedAverageValue {
	field: Field;
	missing: double;
	script: Script;
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
	custom_rules: DetectionRule[];
	detector_description: string;
	detector_index: integer;
	exclude_frequent: ExcludeFrequent;
	function: string;
	use_null: boolean;
}
@namespace("x_pack.machine_learning.job.detectors")
class DetectionRule {
	actions: RuleAction[];
	conditions: RuleCondition[];
	scope: Dictionary<Field, FilterRef>;
}
@namespace("x_pack.machine_learning.job.detectors")
class RuleCondition {
	applies_to: AppliesTo;
	operator: ConditionOperator;
	value: double;
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
@namespace("x_pack.machine_learning.job.config")
class ModelPlotConfigEnabled {
	enabled: boolean;
}
@namespace("x_pack.machine_learning.job.config")
class AnalysisMemoryLimit {
	model_memory_limit: string;
}
@namespace("search.search.rescoring")
class Rescore {
	query: RescoreQuery;
	window_size: integer;
}
@namespace("search.search.rescoring")
class RescoreQuery {
	rescore_query: QueryContainer;
	query_weight: double;
	rescore_query_weight: double;
	score_mode: ScoreMode;
}
@namespace("search.suggesters")
class SuggestBucket {
	completion: CompletionSuggester;
	phrase: PhraseSuggester;
	prefix: string;
	regex: string;
	term: TermSuggester;
	text: string;
}
@namespace("search.suggesters.context_suggester")
class SuggestContextQuery {
	boost: double;
	context: Context;
	neighbours: Union<Distance[], integer[]>;
	precision: Union<Distance, integer>;
	prefix: boolean;
}
@namespace("search.suggesters.completion_suggester")
class SuggestFuzziness {
	fuzziness: Fuzziness;
	min_length: integer;
	prefix_length: integer;
	transpositions: boolean;
	unicode_aware: boolean;
}
@namespace("search.suggesters")
class Suggester {
	analyzer: string;
	field: Field;
	size: integer;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestCollate {
	params: Dictionary<string, any>;
	prune: boolean;
	query: PhraseSuggestCollateQuery;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestCollateQuery {
	id: Id;
	source: string;
}
@namespace("search.suggesters.phrase_suggester")
class DirectGenerator {
	field: Field;
	max_edits: integer;
	max_inspections: float;
	max_term_freq: float;
	min_doc_freq: float;
	min_word_length: integer;
	post_filter: string;
	pre_filter: string;
	prefix_length: integer;
	size: integer;
	suggest_mode: SuggestMode;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggestHighlight {
	post_tag: string;
	pre_tag: string;
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class SmoothingModelContainer {
	laplace: LaplaceSmoothingModel;
	linear_interpolation: LinearInterpolationSmoothingModel;
	stupid_backoff: StupidBackoffSmoothingModel;
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class SmoothingModel {
}
@namespace("search")
class TypedSearchRequest {
}
@namespace("x_pack.roll_up.rollup_configuration")
class RollupGroupings {
	date_histogram: DateHistogramRollupGrouping;
	histogram: HistogramRollupGrouping;
	terms: TermsRollupGrouping;
}
@namespace("x_pack.roll_up.rollup_configuration")
class DateHistogramRollupGrouping {
	delay: Time;
	field: Field;
	format: string;
	interval: Time;
	time_zone: string;
}
@namespace("x_pack.roll_up.rollup_configuration")
class HistogramRollupGrouping {
	fields: Field[];
	interval: long;
}
@namespace("x_pack.roll_up.rollup_configuration")
class TermsRollupGrouping {
	fields: Field[];
}
@namespace("x_pack.roll_up.rollup_configuration")
class RollupFieldMetric {
	field: Field;
	metrics: RollupMetric[];
}
@namespace("x_pack.security.api_key.create_api_key")
class ApiKeyRole {
	cluster: string[];
	index: ApiKeyPrivileges[];
}
@namespace("x_pack.security.api_key.create_api_key")
class ApiKeyPrivileges {
	names: string[];
	privileges: string[];
}
@namespace("x_pack.security.privileges.has_privileges")
class ApplicationPrivilegesCheck {
	application: string;
	privileges: string[];
	resources: string[];
}
@namespace("x_pack.security.privileges.has_privileges")
class IndexPrivilegesCheck {
	names: string[];
	privileges: string[];
}
@namespace("x_pack.security.privileges.put_privileges")
class PrivilegesActions {
	actions: string[];
	metadata: Dictionary<string, any>;
}
@namespace("x_pack.security.role.put_role")
class ApplicationPrivileges {
	application: string;
	privileges: string[];
	resources: string[];
}
@namespace("x_pack.security.role.put_role")
class IndicesPrivileges {
	field_security: FieldSecurity;
	names: Indices;
	privileges: string[];
	query: QueryContainer;
}
@namespace("x_pack.security.role")
class FieldSecurity {
	except: Field[];
	grant: Field[];
}
@namespace("x_pack.slm")
class SnapshotLifecycleConfig {
	ignore_unavailable: boolean;
	include_global_state: boolean;
	indices: Indices;
}
@namespace("x_pack.watcher.schedule")
class Schedule {
}
@namespace("x_pack.sql")
class SqlRequest {
	fetch_size: integer;
	filter: QueryContainer;
	query: string;
	time_zone: string;
}
@namespace("x_pack.watcher.trigger")
class TriggerEvent {
}
@namespace("x_pack.watcher")
class Watch {
	actions: Dictionary<string, Action>;
	condition: ConditionContainer;
	input: InputContainer;
	metadata: Dictionary<string, any>;
	status: WatchStatus;
	throttle_period: string;
	transform: TransformContainer;
	trigger: TriggerContainer;
}
@namespace("x_pack.watcher.action")
class Action {
	action_type: ActionType;
	name: string;
	throttle_period: Time;
	foreach: string;
	max_iterations: integer;
	transform: TransformContainer;
	condition: ConditionContainer;
}
@namespace("x_pack.watcher.transform")
class TransformContainer {
	chain: ChainTransform;
	script: ScriptTransform;
	search: SearchTransform;
}
@namespace("x_pack.watcher.transform")
class Transform {
}
@namespace("x_pack.watcher.input")
class SearchInputRequest {
	body: SearchRequest;
	indices: IndexName[];
	indices_options: IndicesOptions;
	search_type: SearchType;
	template: SearchTemplateRequest;
}
@namespace("x_pack.watcher.input")
class IndicesOptions {
	allow_no_indices: boolean;
	expand_wildcards: ExpandWildcards;
	ignore_unavailable: boolean;
}
@namespace("x_pack.watcher.condition")
class ConditionContainer {
	always: AlwaysCondition;
	array_compare: ArrayCompareCondition;
	compare: CompareCondition;
	never: NeverCondition;
	script: ScriptCondition;
}
@namespace("x_pack.watcher.condition")
class Condition {
}
@namespace("x_pack.watcher.condition")
class ArrayCompareCondition {
	array_path: string;
	comparison: string;
	path: string;
	quantifier: Quantifier;
	value: any;
}
@namespace("x_pack.watcher.input")
class InputContainer {
	chain: ChainInput;
	http: HttpInput;
	search: SearchInput;
	simple: SimpleInput;
}
@namespace("x_pack.watcher.input")
class Input {
}
@namespace("x_pack.watcher.input")
class HttpInputRequest {
	auth: HttpInputAuthentication;
	body: string;
	connection_timeout: Time;
	headers: Dictionary<string, string>;
	host: string;
	method: HttpInputMethod;
	params: Dictionary<string, string>;
	path: string;
	port: integer;
	proxy: HttpInputProxy;
	read_timeout: Time;
	scheme: ConnectionScheme;
	url: string;
}
@namespace("x_pack.watcher.input")
class HttpInputAuthentication {
	basic: HttpInputBasicAuthentication;
}
@namespace("x_pack.watcher.input")
class HttpInputBasicAuthentication {
	password: string;
	username: string;
}
@namespace("x_pack.watcher.input")
class HttpInputProxy {
	host: string;
	port: integer;
}
@namespace("x_pack.watcher.trigger")
class TriggerContainer {
	schedule: ScheduleContainer;
}
@namespace("x_pack.watcher.schedule")
class ScheduleContainer {
	cron: CronExpression;
	daily: DailySchedule;
	hourly: HourlySchedule;
	interval: Interval;
	monthly: TimeOfMonth[];
	weekly: TimeOfWeek[];
	yearly: TimeOfYear[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfDay {
	hour: integer[];
	minute: integer[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfMonth {
	at: string[];
	on: integer[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfWeek {
	at: string[];
	on: Day[];
}
@namespace("x_pack.watcher.schedule")
class TimeOfYear {
	at: string[];
	int: Month[];
	on: integer[];
}
@namespace("aggregations")
class Aggregate {
	meta: Dictionary<string, any>;
}
@namespace("search.search.hits")
class HitMetadata<TDocument> {
	_id: string;
	_index: string;
	_primary_term: long;
	_routing: string;
	_seq_no: long;
	_source: TDocument;
	_type: string;
	_version: long;
}
@namespace("search.search.hits")
class HitsMetadata<T> {
	hits: Hit<T>[];
	max_score: double;
	total: TotalHits;
}
@namespace("search.suggesters")
class SuggestDictionary<T> {
	item: Suggest<T>[];
	keys: string[];
	values: Suggest<T>[][];
}
@namespace("search.suggesters")
class Suggest<T> {
	length: integer;
	offset: integer;
	options: SuggestOption<T>[];
	text: string;
}
@namespace("search.suggesters")
class SuggestOption<TDocument> {
	collate_match: boolean;
	contexts: Dictionary<string, Context[]>;
	_score: double;
	fields: Dictionary<string, LazyDocument>;
	freq: long;
	highlighted: string;
	_id: string;
	_index: IndexName;
	score: double;
	_source: TDocument;
	score: double;
	text: string;
}
@namespace("x_pack.slm")
class SnapshotLifecyclePolicy {
	config: SnapshotLifecycleConfig;
	name: string;
	repository: string;
	schedule: CronExpression;
}
@namespace("x_pack.watcher.action.email")
class EmailBody {
	html: string;
	text: string;
}
@namespace("x_pack.watcher.action.pager_duty")
class PagerDutyContext {
	href: string;
	src: string;
	type: PagerDutyContextType;
}
@namespace("x_pack.watcher.action.pager_duty")
class PagerDutyEvent {
	account: string;
	attach_payload: boolean;
	client: string;
	client_url: string;
	context: PagerDutyContext[];
	description: string;
	event_type: PagerDutyEventType;
	incident_key: string;
}
@namespace("x_pack.watcher.action.slack")
class SlackMessage {
	attachments: SlackAttachment[];
	dynamic_attachments: SlackDynamicAttachment;
	from: string;
	icon: string;
	text: string;
	to: string[];
}
@namespace("x_pack.watcher.action.slack")
class SlackAttachment {
	author_icon: string;
	author_link: string;
	author_name: string;
	color: string;
	fallback: string;
	fields: SlackAttachmentField[];
	footer: string;
	footer_icon: string;
	image_url: string;
	pretext: string;
	text: string;
	thumb_url: string;
	title: string;
	title_link: string;
	ts: Date;
}
@namespace("x_pack.watcher.action.slack")
class SlackAttachmentField {
	short: boolean;
	title: string;
	value: string;
}
@namespace("x_pack.watcher.action.slack")
class SlackDynamicAttachment {
	attachment_template: SlackAttachment;
	list_path: string;
}
@namespace("x_pack.watcher.trigger")
class TriggerEventContainer {
	schedule: ScheduleTriggerEvent;
}
/** namespace:common_abstractions.response **/
interface IResponse {
	server_error: ServerError;
}
@namespace("query_dsl.compound.bool")
class BoolQuery {
	filter: QueryContainer[];
	locked: boolean;
	minimum_should_match: MinimumShouldMatch;
	must: QueryContainer[];
	must_not: QueryContainer[];
	should: QueryContainer[];
}
@namespace("query_dsl.compound.boosting")
class BoostingQuery {
	negative_boost: double;
	negative: QueryContainer;
	positive: QueryContainer;
}
@namespace("query_dsl.abstractions.field_name")
class FieldNameQuery {
	field: Field;
}
@namespace("query_dsl.compound.constant_score")
class ConstantScoreQuery {
	filter: QueryContainer;
}
@namespace("query_dsl.compound.dismax")
class DisMaxQuery {
	queries: QueryContainer[];
	tie_breaker: double;
}
@namespace("query_dsl.term_level.exists")
class ExistsQuery {
	field: Field;
}
@namespace("query_dsl.compound.function_score")
class FunctionScoreQuery {
	boost_mode: FunctionBoostMode;
	functions: ScoreFunction[];
	max_boost: double;
	min_score: double;
	query: QueryContainer;
	score_mode: FunctionScoreMode;
}
@namespace("query_dsl.joining.has_child")
class HasChildQuery {
	ignore_unmapped: boolean;
	inner_hits: InnerHits;
	max_children: integer;
	min_children: integer;
	query: QueryContainer;
	score_mode: ChildScoreMode;
	type: RelationName;
}
@namespace("query_dsl.joining.has_parent")
class HasParentQuery {
	ignore_unmapped: boolean;
	inner_hits: InnerHits;
	parent_type: RelationName;
	query: QueryContainer;
	score: boolean;
}
@namespace("query_dsl.term_level.ids")
class IdsQuery {
	values: Id[];
}
@namespace("query_dsl.full_text.intervals")
class IntervalsAllOf {
	intervals: IntervalsContainer[];
	max_gaps: integer;
	ordered: boolean;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsAnyOf {
	intervals: IntervalsContainer[];
}
@namespace("query_dsl.full_text.intervals")
class IntervalsMatch {
	analyzer: string;
	max_gaps: integer;
	ordered: boolean;
	query: string;
	use_field: Field;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsPrefix {
	analyzer: string;
	prefix: string;
	use_field: Field;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsWildcard {
	analyzer: string;
	pattern: string;
	use_field: Field;
}
@namespace("query_dsl")
class MatchAllQuery {
	norm_field: string;
}
@namespace("query_dsl")
class MatchNoneQuery {
}
@namespace("query_dsl.specialized.more_like_this")
class MoreLikeThisQuery {
	analyzer: string;
	boost_terms: double;
	fields: Field[];
	include: boolean;
	like: Like[];
	max_doc_freq: integer;
	max_query_terms: integer;
	max_word_length: integer;
	min_doc_freq: integer;
	minimum_should_match: MinimumShouldMatch;
	min_term_freq: integer;
	min_word_length: integer;
	per_field_analyzer: Dictionary<Field, string>;
	routing: Routing;
	stop_words: StopWords;
	unlike: Like[];
	version: long;
	version_type: VersionType;
}
@namespace("query_dsl.full_text.multi_match")
class MultiMatchQuery {
	analyzer: string;
	auto_generate_synonyms_phrase_query: boolean;
	cutoff_frequency: double;
	fields: Field[];
	fuzziness: Fuzziness;
	fuzzy_rewrite: MultiTermQueryRewrite;
	fuzzy_transpositions: boolean;
	lenient: boolean;
	max_expansions: integer;
	minimum_should_match: MinimumShouldMatch;
	operator: Operator;
	prefix_length: integer;
	query: string;
	slop: integer;
	tie_breaker: double;
	type: TextQueryType;
	use_dis_max: boolean;
	zero_terms_query: ZeroTermsQuery;
}
@namespace("query_dsl.joining.nested")
class NestedQuery {
	ignore_unmapped: boolean;
	inner_hits: InnerHits;
	path: Field;
	query: QueryContainer;
	score_mode: NestedScoreMode;
}
@namespace("query_dsl.joining.parent_id")
class ParentIdQuery {
	id: Id;
	ignore_unmapped: boolean;
	type: RelationName;
}
@namespace("query_dsl.specialized.percolate")
class PercolateQuery {
	document: any;
	documents: any[];
	field: Field;
	id: Id;
	index: IndexName;
	preference: string;
	routing: Routing;
	version: long;
}
@namespace("query_dsl.full_text.query_string")
class QueryStringQuery {
	allow_leading_wildcard: boolean;
	analyzer: string;
	analyze_wildcard: boolean;
	auto_generate_synonyms_phrase_query: boolean;
	default_field: Field;
	default_operator: Operator;
	enable_position_increments: boolean;
	escape: boolean;
	fields: Field[];
	fuzziness: Fuzziness;
	fuzzy_max_expansions: integer;
	fuzzy_prefix_length: integer;
	fuzzy_rewrite: MultiTermQueryRewrite;
	fuzzy_transpositions: boolean;
	lenient: boolean;
	max_determinized_states: integer;
	minimum_should_match: MinimumShouldMatch;
	phrase_slop: double;
	query: string;
	quote_analyzer: string;
	quote_field_suffix: string;
	rewrite: MultiTermQueryRewrite;
	tie_breaker: double;
	time_zone: string;
	type: TextQueryType;
}
@namespace("query_dsl.nest_specific")
class RawQuery {
	raw: string;
}
@namespace("query_dsl.specialized.script")
class ScriptQuery {
	script: Script;
}
@namespace("query_dsl.specialized.script_score")
class ScriptScoreQuery {
	query: QueryContainer;
	script: Script;
}
@namespace("query_dsl.full_text.simple_query_string")
class SimpleQueryStringQuery {
	analyzer: string;
	analyze_wildcard: boolean;
	auto_generate_synonyms_phrase_query: boolean;
	default_operator: Operator;
	fields: Field[];
	flags: SimpleQueryStringFlags;
	fuzzy_max_expansions: integer;
	fuzzy_prefix_length: integer;
	fuzzy_transpositions: boolean;
	lenient: boolean;
	minimum_should_match: MinimumShouldMatch;
	query: string;
	quote_field_suffix: string;
}
@namespace("query_dsl.span")
class SpanQuery {
	span_containing: SpanContainingQuery;
	field_masking_span: SpanFieldMaskingQuery;
	span_first: SpanFirstQuery;
	span_gap: SpanGapQuery;
	span_multi: SpanMultiTermQuery;
	span_near: SpanNearQuery;
	span_not: SpanNotQuery;
	span_or: SpanOrQuery;
	span_term: SpanTermQuery;
	span_within: SpanWithinQuery;
}
@namespace("query_dsl.span")
class SpanSubQuery {
}
@namespace("query_dsl.specialized.pinned")
class PinnedQuery {
	ids: Id[];
	organic: QueryContainer;
}
@namespace("mapping.meta_fields.all")
class AllField {
	analyzer: string;
	enabled: boolean;
	omit_norms: boolean;
	search_analyzer: string;
	similarity: string;
	store: boolean;
	store_term_vector_offsets: boolean;
	store_term_vector_payloads: boolean;
	store_term_vector_positions: boolean;
	store_term_vectors: boolean;
}
/** namespace:mapping.types **/
interface IProperty {
	local_metadata: Dictionary<string, any>;
	name: PropertyName;
	type: string;
}
@namespace("mapping.meta_fields.field_names")
class FieldNamesField {
	enabled: boolean;
}
@namespace("mapping.meta_fields.index")
class IndexField {
	enabled: boolean;
}
@namespace("mapping.meta_fields.routing")
class RoutingField {
	required: boolean;
}
@namespace("mapping.meta_fields.size")
class SizeField {
	enabled: boolean;
}
@namespace("mapping.meta_fields.source")
class SourceField {
	compress: boolean;
	compress_threshold: string;
	enabled: boolean;
	excludes: string[];
	includes: string[];
}
@namespace("modules.indices.fielddata.numeric")
class NumericFielddata {
	format: NumericFielddataFormat;
}
@namespace("modules.indices.fielddata.string")
class StringFielddata {
	format: StringFielddataFormat;
}
@namespace("common_options.scripting")
class InlineScript {
	source: string;
}
@namespace("aggregations.bucket")
class BucketAggregation {
	aggregations: Dictionary<string, AggregationContainer>;
}
@namespace("aggregations.metric")
class MetricAggregation {
	field: Field;
	missing: double;
	script: Script;
}
@namespace("aggregations.pipeline")
class PipelineAggregation {
	buckets_path: BucketsPath;
	format: string;
	gap_policy: GapPolicy;
}
@namespace("aggregations.pipeline.bucket_sort")
class BucketSortAggregation {
	from: integer;
	gap_policy: GapPolicy;
	size: integer;
	sort: Sort[];
}
@namespace("aggregations.matrix")
class MatrixAggregation {
	fields: Field[];
	missing: Dictionary<Field, double>;
}
@namespace("aggregations.metric.weighted_average")
class WeightedAverageAggregation {
	format: string;
	value: WeightedAverageValue;
	value_type: ValueType;
	weight: WeightedAverageValue;
}
@namespace("x_pack.machine_learning.job.config")
class ModelPlotConfig {
	terms: Field[];
}
@namespace("search.suggesters.completion_suggester")
class CompletionSuggester {
	contexts: Dictionary<string, SuggestContextQuery[]>;
	fuzzy: SuggestFuzziness;
	prefix: string;
	regex: string;
	skip_duplicates: boolean;
}
@namespace("search.suggesters.phrase_suggester")
class PhraseSuggester {
	collate: PhraseSuggestCollate;
	confidence: double;
	direct_generator: DirectGenerator[];
	force_unigrams: boolean;
	gram_size: integer;
	highlight: PhraseSuggestHighlight;
	max_errors: double;
	real_word_error_likelihood: double;
	separator: string;
	shard_size: integer;
	smoothing: SmoothingModelContainer;
	text: string;
	token_limit: integer;
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class LaplaceSmoothingModel {
	alpha: double;
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class LinearInterpolationSmoothingModel {
	bigram_lambda: double;
	trigram_lambda: double;
	unigram_lambda: double;
}
@namespace("search.suggesters.phrase_suggester.smoothing_model")
class StupidBackoffSmoothingModel {
	discount: double;
}
@namespace("search.suggesters.term_suggester")
class TermSuggester {
	lowercase_terms: boolean;
	max_edits: integer;
	max_inspections: integer;
	max_term_freq: float;
	min_doc_freq: float;
	min_word_length: integer;
	prefix_length: integer;
	shard_size: integer;
	sort: SuggestSort;
	string_distance: StringDistance;
	suggest_mode: SuggestMode;
	text: string;
}
@namespace("x_pack.watcher.schedule")
class ScheduleTriggerEvent {
	scheduled_time: Union<Date, string>;
	triggered_time: Union<Date, string>;
}
@namespace("x_pack.watcher.transform")
class ChainTransform {
	transforms: TransformContainer[];
}
@namespace("x_pack.watcher.transform")
class ScriptTransform {
	lang: string;
	params: Dictionary<string, any>;
}
@namespace("x_pack.watcher.transform")
class SearchTransform {
	request: SearchInputRequest;
	timeout: Time;
}
@namespace("x_pack.watcher.condition")
class AlwaysCondition {
}
@namespace("x_pack.watcher.condition")
class CompareCondition {
	comparison: string;
	path: string;
	value: any;
}
@namespace("x_pack.watcher.condition")
class NeverCondition {
}
@namespace("x_pack.watcher.condition")
class ScriptCondition {
	lang: string;
	params: Dictionary<string, any>;
}
@namespace("x_pack.watcher.input")
class ChainInput {
	inputs: Dictionary<string, InputContainer>;
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
@namespace("x_pack.watcher.input")
class SimpleInput {
	payload: Dictionary<string, any>;
}
@namespace("x_pack.watcher.schedule")
class DailySchedule {
	at: Union<string[], TimeOfDay>;
}
@namespace("x_pack.watcher.schedule")
class HourlySchedule {
	minute: integer[];
}
@namespace("search.search.hits")
class Hit<TDocument> {
	_explanation: Explanation;
	fields: Dictionary<string, LazyDocument>;
	highlight: Dictionary<string, string[]>;
	inner_hits: Dictionary<string, InnerHitsResult>;
	matched_queries: string[];
	_nested: NestedIdentity;
	_score: double;
	sort: any[];
}
@namespace("query_dsl.full_text.common_terms")
class CommonTermsQuery {
	analyzer: string;
	cutoff_frequency: double;
	high_freq_operator: Operator;
	low_freq_operator: Operator;
	minimum_should_match: MinimumShouldMatch;
	query: string;
}
@namespace("query_dsl.term_level.fuzzy")
class FuzzyQuery {
	max_expansions: integer;
	prefix_length: integer;
	rewrite: MultiTermQueryRewrite;
	transpositions: boolean;
}
@namespace("query_dsl.geo.bounding_box")
class GeoBoundingBoxQuery {
	bounding_box: BoundingBox;
	type: GeoExecution;
	validation_method: GeoValidationMethod;
}
@namespace("query_dsl.geo.distance")
class GeoDistanceQuery {
	distance: Distance;
	distance_type: GeoDistanceType;
	location: GeoLocation;
	validation_method: GeoValidationMethod;
}
@namespace("query_dsl.geo.polygon")
class GeoPolygonQuery {
	points: GeoLocation[];
	validation_method: GeoValidationMethod;
}
@namespace("query_dsl.geo.shape")
class GeoShapeQuery {
	ignore_unmapped: boolean;
	indexed_shape: FieldLookup;
	relation: GeoShapeRelation;
	shape: GeoShape;
}
@namespace("query_dsl.specialized.shape")
class ShapeQuery {
	ignore_unmapped: boolean;
	indexed_shape: FieldLookup;
	relation: ShapeRelation;
	shape: GeoShape;
}
@namespace("query_dsl.full_text.match")
class MatchQuery {
	analyzer: string;
	auto_generate_synonyms_phrase_query: boolean;
	cutoff_frequency: double;
	fuzziness: Fuzziness;
	fuzzy_rewrite: MultiTermQueryRewrite;
	fuzzy_transpositions: boolean;
	lenient: boolean;
	max_expansions: integer;
	minimum_should_match: MinimumShouldMatch;
	operator: Operator;
	prefix_length: integer;
	query: string;
	zero_terms_query: ZeroTermsQuery;
}
@namespace("query_dsl.full_text.match_phrase")
class MatchPhraseQuery {
	analyzer: string;
	query: string;
	slop: integer;
}
@namespace("query_dsl.full_text.match_phrase_prefix")
class MatchPhrasePrefixQuery {
	analyzer: string;
	max_expansions: integer;
	query: string;
	slop: integer;
	zero_terms_query: ZeroTermsQuery;
}
@namespace("query_dsl.term_level.term")
class TermQuery {
	value: any;
}
@namespace("query_dsl.term_level.range")
class RangeQuery {
}
@namespace("query_dsl.term_level.regexp")
class RegexpQuery {
	flags: string;
	max_determinized_states: integer;
	value: string;
}
@namespace("query_dsl.span.containing")
class SpanContainingQuery {
	big: SpanQuery;
	little: SpanQuery;
}
@namespace("query_dsl.span.field_masking")
class SpanFieldMaskingQuery {
	field: Field;
	query: SpanQuery;
}
@namespace("query_dsl.span.first")
class SpanFirstQuery {
	end: integer;
	match: SpanQuery;
}
@namespace("query_dsl.span.gap")
class SpanGapQuery {
	field: Field;
	width: integer;
}
@namespace("query_dsl.span.multi_term")
class SpanMultiTermQuery {
	match: QueryContainer;
}
@namespace("query_dsl.span.near")
class SpanNearQuery {
	clauses: SpanQuery[];
	in_order: boolean;
	slop: integer;
}
@namespace("query_dsl.span.not")
class SpanNotQuery {
	dist: integer;
	exclude: SpanQuery;
	include: SpanQuery;
	post: integer;
	pre: integer;
}
@namespace("query_dsl.span.or")
class SpanOrQuery {
	clauses: SpanQuery[];
}
@namespace("query_dsl.span.within")
class SpanWithinQuery {
	big: SpanQuery;
	little: SpanQuery;
}
@namespace("query_dsl.term_level.terms")
class TermsQuery {
	terms: any[];
	terms_lookup: FieldLookup;
}
@namespace("query_dsl.term_level.terms_set")
class TermsSetQuery {
	minimum_should_match_field: Field;
	minimum_should_match_script: Script;
	terms: any[];
}
@namespace("query_dsl.specialized.rank_feature")
class RankFeatureQuery {
	function: RankFeatureFunction;
}
@namespace("query_dsl.specialized.distance_feature")
class DistanceFeatureQuery {
	origin: Union<GeoCoordinate, DateMath>;
	pivot: Union<Distance, Time>;
}
@namespace("aggregations.bucket.adjacency_matrix")
class AdjacencyMatrixAggregation {
	filters: Dictionary<string, QueryContainer>;
}
@namespace("aggregations.metric.average")
class AverageAggregation {
}
@namespace("aggregations.pipeline.average_bucket")
class AverageBucketAggregation {
}
@namespace("aggregations.pipeline.bucket_script")
class BucketScriptAggregation {
	script: Script;
}
@namespace("aggregations.pipeline.bucket_selector")
class BucketSelectorAggregation {
	script: Script;
}
@namespace("aggregations.metric.cardinality")
class CardinalityAggregation {
	precision_threshold: integer;
	rehash: boolean;
}
@namespace("aggregations.bucket.children")
class ChildrenAggregation {
	type: RelationName;
}
@namespace("aggregations.bucket.composite")
class CompositeAggregation {
	after: Dictionary<string, any>;
	size: integer;
	sources: CompositeAggregationSource[];
}
@namespace("aggregations.pipeline.cumulative_sum")
class CumulativeSumAggregation {
}
@namespace("aggregations.pipeline.cumulative_cardinality")
class CumulativeCardinalityAggregation {
}
@namespace("aggregations.bucket.date_histogram")
class DateHistogramAggregation {
	extended_bounds: ExtendedBounds<DateMath>;
	field: Field;
	format: string;
	interval: Union<DateInterval, Time>;
	calendar_interval: Union<DateInterval, Time>;
	fixed_interval: Union<DateInterval, Time>;
	min_doc_count: integer;
	missing: Date;
	offset: string;
	order: HistogramOrder;
	params: Dictionary<string, any>;
	script: Script;
	time_zone: string;
}
@namespace("aggregations.bucket.auto_date_histogram")
class AutoDateHistogramAggregation {
	field: Field;
	buckets: integer;
	format: string;
	missing: Date;
	offset: string;
	params: Dictionary<string, any>;
	script: Script;
	time_zone: string;
	minimum_interval: MinimumInterval;
}
@namespace("aggregations.bucket.date_range")
class DateRangeAggregation {
	field: Field;
	format: string;
	missing: any;
	ranges: DateRangeExpression[];
	time_zone: string;
}
@namespace("aggregations.pipeline.derivative")
class DerivativeAggregation {
}
@namespace("aggregations.metric.extended_stats")
class ExtendedStatsAggregation {
	sigma: double;
}
@namespace("aggregations.pipeline.extended_stats_bucket")
class ExtendedStatsBucketAggregation {
	sigma: double;
}
@namespace("aggregations.bucket.filter")
class FilterAggregation {
	filter: QueryContainer;
}
@namespace("aggregations.bucket.filters")
class FiltersAggregation {
	filters: Union<Dictionary<string, QueryContainer>, QueryContainer[]>;
	other_bucket: boolean;
	other_bucket_key: string;
}
@namespace("aggregations.metric.geo_bounds")
class GeoBoundsAggregation {
	wrap_longitude: boolean;
}
@namespace("aggregations.metric.geo_centroid")
class GeoCentroidAggregation {
}
@namespace("aggregations.bucket.geo_distance")
class GeoDistanceAggregation {
	distance_type: GeoDistanceType;
	field: Field;
	origin: GeoLocation;
	ranges: AggregationRange[];
	unit: DistanceUnit;
}
@namespace("aggregations.bucket.geo_hash_grid")
class GeoHashGridAggregation {
	field: Field;
	precision: GeoHashPrecision;
	shard_size: integer;
	size: integer;
}
@namespace("aggregations.bucket.geo_tile_grid")
class GeoTileGridAggregation {
	field: Field;
	precision: GeoTilePrecision;
	shard_size: integer;
	size: integer;
}
@namespace("aggregations.bucket.global")
class GlobalAggregation {
}
@namespace("aggregations.bucket.histogram")
class HistogramAggregation {
	extended_bounds: ExtendedBounds<double>;
	field: Field;
	interval: double;
	min_doc_count: integer;
	missing: double;
	offset: double;
	order: HistogramOrder;
	script: Script;
}
@namespace("aggregations.bucket.ip_range")
class IpRangeAggregation {
	field: Field;
	ranges: IpRangeAggregationRange[];
}
@namespace("aggregations.matrix.matrix_stats")
class MatrixStatsAggregation {
	mode: MatrixStatsMode;
}
@namespace("aggregations.metric.max")
class MaxAggregation {
}
@namespace("aggregations.pipeline.max_bucket")
class MaxBucketAggregation {
}
@namespace("aggregations.metric.min")
class MinAggregation {
}
@namespace("aggregations.pipeline.min_bucket")
class MinBucketAggregation {
}
@namespace("aggregations.bucket.missing")
class MissingAggregation {
	field: Field;
}
@namespace("aggregations.pipeline.moving_average")
class MovingAverageAggregation {
	minimize: boolean;
	model: MovingAverageModel;
	predict: integer;
	window: integer;
}
@namespace("aggregations.pipeline.moving_function")
class MovingFunctionAggregation {
	script: string;
	window: integer;
	shift: integer;
}
@namespace("aggregations.bucket.nested")
class NestedAggregation {
	path: Field;
}
@namespace("aggregations.bucket.parent")
class ParentAggregation {
	type: RelationName;
}
@namespace("aggregations.metric.percentile_ranks")
class PercentileRanksAggregation {
	method: PercentilesMethod;
	values: double[];
	keyed: boolean;
}
@namespace("aggregations.metric.percentiles")
class PercentilesAggregation {
	method: PercentilesMethod;
	percents: double[];
	keyed: boolean;
}
@namespace("aggregations.pipeline.percentiles_bucket")
class PercentilesBucketAggregation {
	percents: double[];
}
@namespace("aggregations.bucket.range")
class RangeAggregation {
	field: Field;
	ranges: AggregationRange[];
	script: Script;
}
@namespace("aggregations.bucket.rare_terms")
class RareTermsAggregation {
	exclude: TermsExclude;
	field: Field;
	include: TermsInclude;
	max_doc_count: long;
	missing: any;
	precision: double;
}
@namespace("aggregations.bucket.reverse_nested")
class ReverseNestedAggregation {
	path: Field;
}
@namespace("aggregations.bucket.sampler")
class SamplerAggregation {
	execution_hint: SamplerAggregationExecutionHint;
	max_docs_per_value: integer;
	script: Script;
	shard_size: integer;
}
@namespace("aggregations.metric.scripted_metric")
class ScriptedMetricAggregation {
	combine_script: Script;
	init_script: Script;
	map_script: Script;
	params: Dictionary<string, any>;
	reduce_script: Script;
}
@namespace("aggregations.pipeline.serial_differencing")
class SerialDifferencingAggregation {
	lag: integer;
}
@namespace("aggregations.bucket.significant_terms")
class SignificantTermsAggregation {
	background_filter: QueryContainer;
	chi_square: ChiSquareHeuristic;
	exclude: IncludeExclude;
	execution_hint: TermsAggregationExecutionHint;
	field: Field;
	gnd: GoogleNormalizedDistanceHeuristic;
	include: IncludeExclude;
	min_doc_count: long;
	mutual_information: MutualInformationHeuristic;
	percentage: PercentageScoreHeuristic;
	script_heuristic: ScriptedHeuristic;
	shard_min_doc_count: long;
	shard_size: integer;
	size: integer;
}
@namespace("aggregations.bucket.significant_text")
class SignificantTextAggregation {
	background_filter: QueryContainer;
	chi_square: ChiSquareHeuristic;
	exclude: IncludeExclude;
	execution_hint: TermsAggregationExecutionHint;
	field: Field;
	filter_duplicate_text: boolean;
	gnd: GoogleNormalizedDistanceHeuristic;
	include: IncludeExclude;
	min_doc_count: long;
	mutual_information: MutualInformationHeuristic;
	percentage: PercentageScoreHeuristic;
	script_heuristic: ScriptedHeuristic;
	shard_min_doc_count: long;
	shard_size: integer;
	size: integer;
	source_fields: Field[];
}
@namespace("aggregations.metric.stats")
class StatsAggregation {
}
@namespace("aggregations.pipeline.stats_bucket")
class StatsBucketAggregation {
}
@namespace("aggregations.metric.sum")
class SumAggregation {
}
@namespace("aggregations.pipeline.sum_bucket")
class SumBucketAggregation {
}
@namespace("aggregations.bucket.terms")
class TermsAggregation {
	collect_mode: TermsAggregationCollectMode;
	exclude: TermsExclude;
	execution_hint: TermsAggregationExecutionHint;
	field: Field;
	include: TermsInclude;
	min_doc_count: integer;
	missing: any;
	order: TermsOrder[];
	script: Script;
	shard_size: integer;
	show_term_doc_count_error: boolean;
	size: integer;
}
@namespace("aggregations.metric.top_hits")
class TopHitsAggregation {
	docvalue_fields: Field[];
	explain: boolean;
	from: integer;
	highlight: Highlight;
	script_fields: Dictionary<string, ScriptField>;
	size: integer;
	sort: Sort[];
	_source: Union<boolean, SourceFilter>;
	stored_fields: Field[];
	track_scores: boolean;
	version: boolean;
}
@namespace("aggregations.metric.value_count")
class ValueCountAggregation {
}
@namespace("aggregations.metric.median_absolute_deviation")
class MedianAbsoluteDeviationAggregation {
	compression: double;
}
@namespace("query_dsl.full_text.intervals")
class IntervalsQuery {
}
@namespace("query_dsl.term_level.prefix")
class PrefixQuery {
	rewrite: MultiTermQueryRewrite;
}
@namespace("query_dsl.term_level.wildcard")
class WildcardQuery {
	rewrite: MultiTermQueryRewrite;
}
@namespace("query_dsl.span.term")
class SpanTermQuery {
}
@namespace("common_abstractions.union")
class Union<TFirst, TSecond> {
}
@namespace("cluster.cluster_allocation_explain")
class AllocationDecision {
	decider: string;
	decision: AllocationExplainDecision;
	explanation: string;
}
@namespace("cluster.cluster_allocation_explain")
class CurrentNode {
	id: string;
	name: string;
	attributes: Dictionary<string, string>;
	transport_address: string;
	weight_ranking: integer;
}
@namespace("cluster.cluster_allocation_explain")
class NodeAllocationExplanation {
	deciders: AllocationDecision[];
	node_attributes: Dictionary<string, string>;
	node_decision: Decision;
	node_id: string;
	node_name: string;
	store: AllocationStore;
	transport_address: string;
	weight_ranking: integer;
}
@namespace("cluster.cluster_allocation_explain")
class AllocationStore {
	allocation_id: string;
	found: boolean;
	in_sync: boolean;
	matching_size_in_bytes: long;
	matching_sync_id: boolean;
	store_exception: string;
}
@namespace("cluster.cluster_allocation_explain")
class UnassignedInformation {
	at: Date;
	last_allocation_status: string;
	reason: UnassignedInformationReason;
}
@namespace("cluster.cluster_health")
class IndexHealthStats {
	active_primary_shards: integer;
	active_shards: integer;
	initializing_shards: integer;
	number_of_replicas: integer;
	number_of_shards: integer;
	relocating_shards: integer;
	shards: Dictionary<string, ShardHealthStats>;
	status: Health;
	unassigned_shards: integer;
}
@namespace("cluster.cluster_health")
class ShardHealthStats {
	active_shards: integer;
	initializing_shards: integer;
	primary_active: boolean;
	relocating_shards: integer;
	status: Health;
	unassigned_shards: integer;
}
@namespace("cluster.cluster_pending_tasks")
class PendingTask {
	insert_order: integer;
	priority: string;
	source: string;
	time_in_queue: string;
	time_in_queue_millis: integer;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteExplanation {
	command: string;
	decisions: ClusterRerouteDecision[];
	parameters: ClusterRerouteParameters;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteDecision {
	decider: string;
	decision: string;
	explanation: string;
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteParameters {
	allow_primary: boolean;
	from_node: string;
	index: string;
	node: string;
	shard: integer;
	to_node: string;
}
@namespace("cluster")
class NodeStatistics {
	failed: integer;
	successful: integer;
	total: integer;
	failures: ErrorCause[];
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
@namespace("common_options.stats")
class CompletionStats {
	size_in_bytes: long;
}
@namespace("common_options.stats")
class DocStats {
	count: long;
	deleted: long;
}
@namespace("common_options.stats")
class FielddataStats {
	evictions: long;
	memory_size_in_bytes: long;
}
@namespace("common_options.stats")
class QueryCacheStats {
	cache_count: long;
	cache_size: long;
	evictions: long;
	hit_count: long;
	memory_size_in_bytes: long;
	miss_count: long;
	total_count: long;
}
@namespace("common_options.stats")
class SegmentsStats {
	count: long;
	doc_values_memory_in_bytes: long;
	fixed_bit_set_memory_in_bytes: long;
	index_writer_max_memory_in_bytes: long;
	index_writer_memory_in_bytes: long;
	max_unsafe_auto_id_timestamp: long;
	memory_in_bytes: long;
	norms_memory_in_bytes: long;
	points_memory_in_bytes: long;
	stored_fields_memory_in_bytes: long;
	terms_memory_in_bytes: long;
	term_vectors_memory_in_bytes: long;
	version_map_memory_in_bytes: long;
	file_sizes: Dictionary<string, ShardFileSizeInfo>;
}
@namespace("indices.monitoring.indices_stats")
class ShardFileSizeInfo {
	description: string;
	size_in_bytes: long;
}
@namespace("cluster.cluster_stats")
class ClusterIndicesShardsStats {
	index: ClusterIndicesShardsIndexStats;
	primaries: double;
	replication: double;
	total: double;
}
@namespace("cluster.cluster_stats")
class ClusterIndicesShardsIndexStats {
	primaries: ClusterShardMetrics;
	replication: ClusterShardMetrics;
	shards: ClusterShardMetrics;
}
@namespace("cluster.cluster_stats")
class ClusterShardMetrics {
	avg: double;
	max: double;
	min: double;
}
@namespace("common_options.stats")
class StoreStats {
	size: string;
	size_in_bytes: double;
}
@namespace("cluster.cluster_stats")
class ClusterNodesStats {
	count: ClusterNodeCount;
	discovery_types: Dictionary<string, integer>;
	fs: ClusterFileSystem;
	jvm: ClusterJvm;
	network_types: ClusterNetworkTypes;
	os: ClusterOperatingSystemStats;
	packaging_types: NodePackagingType[];
	plugins: PluginStats[];
	process: ClusterProcess;
	versions: string[];
}
@namespace("cluster.cluster_stats")
class ClusterNodeCount {
	coordinating_only: integer;
	data: integer;
	ingest: integer;
	master: integer;
	total: integer;
	voting_only: integer;
}
@namespace("cluster.cluster_stats")
class ClusterFileSystem {
	available_in_bytes: long;
	free_in_bytes: long;
	total_in_bytes: long;
}
@namespace("cluster.cluster_stats")
class ClusterJvm {
	max_uptime_in_millis: long;
	mem: ClusterJvmMemory;
	threads: long;
	versions: ClusterJvmVersion[];
}
@namespace("cluster.cluster_stats")
class ClusterJvmMemory {
	heap_max_in_bytes: long;
	heap_used_in_bytes: long;
}
@namespace("cluster.cluster_stats")
class ClusterJvmVersion {
	bundled_jdk: boolean;
	count: integer;
	using_bundled_jdk: boolean;
	version: string;
	vm_name: string;
	vm_vendor: string;
	vm_version: string;
}
@namespace("cluster.cluster_stats")
class ClusterNetworkTypes {
	http_types: Dictionary<string, integer>;
	transport_types: Dictionary<string, integer>;
}
@namespace("cluster.cluster_stats")
class ClusterOperatingSystemStats {
	allocated_processors: integer;
	available_processors: integer;
	mem: OperatingSystemMemoryInfo;
	names: ClusterOperatingSystemName[];
	pretty_names: ClusterOperatingSystemPrettyNane[];
}
@namespace("cluster.cluster_stats")
class OperatingSystemMemoryInfo {
	free_in_bytes: long;
	free_percent: integer;
	total_in_bytes: long;
	used_in_bytes: long;
	used_percent: integer;
}
@namespace("cluster.cluster_stats")
class ClusterOperatingSystemName {
	count: integer;
	name: string;
}
@namespace("cluster.nodes_info")
class ClusterOperatingSystemPrettyNane {
	count: integer;
	pretty_name: string;
}
@namespace("cluster.cluster_stats")
class NodePackagingType {
	count: integer;
	flavor: string;
	type: string;
}
@namespace("common_options.stats")
class PluginStats {
	classname: string;
	description: string;
	elasticsearch_version: string;
	extended_plugins: string[];
	name: string;
	has_native_controller: boolean;
	java_version: string;
	version: string;
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
	avg: long;
	max: long;
	min: long;
}
@namespace("cluster.nodes_hot_threads")
class HotThreadInformation {
	hosts: string[];
	node_id: string;
	node_name: string;
	threads: string[];
}
@namespace("cluster.nodes_info")
class NodeInfo {
	build_hash: string;
	host: string;
	http: NodeInfoHttp;
	ip: string;
	jvm: NodeJvmInfo;
	name: string;
	network: NodeInfoNetwork;
	os: NodeOperatingSystemInfo;
	plugins: PluginStats[];
	process: NodeProcessInfo;
	roles: NodeRole[];
	settings: string[];
	thread_pool: Dictionary<string, NodeThreadPoolInfo>;
	transport: NodeInfoTransport;
	transport_address: string;
	version: string;
}
@namespace("cluster.nodes_info")
class NodeInfoHttp {
	bound_address: string[];
	max_content_length: string;
	max_content_length_in_bytes: long;
	publish_address: string;
}
@namespace("cluster.nodes_info")
class NodeJvmInfo {
	gc_collectors: string[];
	mem: NodeInfoJvmMemory;
	memory_pools: string[];
	pid: integer;
	start_time_in_millis: long;
	version: string;
	vm_name: string;
	vm_vendor: string;
	vm_version: string;
}
@namespace("cluster.nodes_info")
class NodeInfoJvmMemory {
	direct_max: string;
	direct_max_in_bytes: long;
	heap_init: string;
	heap_init_in_bytes: long;
	heap_max: string;
	heap_max_in_bytes: long;
	non_heap_init: string;
	non_heap_init_in_bytes: long;
	non_heap_max: string;
	non_heap_max_in_bytes: long;
}
@namespace("cluster.nodes_info")
class NodeInfoNetwork {
	primary_interface: NodeInfoNetworkInterface;
	refresh_interval: integer;
}
@namespace("cluster.nodes_info")
class NodeInfoNetworkInterface {
	address: string;
	mac_address: string;
	name: string;
}
@namespace("cluster.nodes_info")
class NodeOperatingSystemInfo {
	arch: string;
	available_processors: integer;
	cpu: NodeInfoOSCPU;
	mem: NodeInfoMemory;
	name: string;
	pretty_name: string;
	refresh_interval_in_millis: integer;
	swap: NodeInfoMemory;
	version: string;
}
@namespace("cluster.nodes_info")
class NodeInfoOSCPU {
	cache_size: string;
	cache_size_in_bytes: integer;
	cores_per_socket: integer;
	mhz: integer;
	model: string;
	total_cores: integer;
	total_sockets: integer;
	vendor: string;
}
@namespace("cluster.nodes_info")
class NodeInfoMemory {
	total: string;
	total_in_bytes: long;
}
@namespace("cluster.nodes_info")
class NodeProcessInfo {
	id: long;
	mlockall: boolean;
	refresh_interval_in_millis: long;
}
@namespace("cluster.nodes_info")
class NodeThreadPoolInfo {
	keep_alive: string;
	max: integer;
	core: integer;
	size: integer;
	queue_size: integer;
	type: string;
}
@namespace("cluster.nodes_info")
class NodeInfoTransport {
	bound_address: string[];
	publish_address: string;
}
@namespace("cluster.nodes_stats")
class NodeStats {
	adaptive_selection: Dictionary<string, AdaptiveSelectionStats>;
	breakers: Dictionary<string, BreakerStats>;
	fs: FileSystemStats;
	host: string;
	http: HttpStats;
	indices: IndexStats;
	ingest: NodeIngestStats;
	ip: string[];
	jvm: NodeJvmStats;
	name: string;
	os: OperatingSystemStats;
	process: ProcessStats;
	roles: NodeRole[];
	script: ScriptStats;
	thread_pool: Dictionary<string, ThreadCountStats>;
	timestamp: long;
	transport: TransportStats;
	transport_address: string;
}
@namespace("cluster.nodes_stats")
class AdaptiveSelectionStats {
	avg_queue_size: long;
	avg_response_time: long;
	avg_response_time_ns: long;
	avg_service_time: string;
	avg_service_time_ns: long;
	outgoing_searches: long;
	rank: string;
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
	data: DataPathStats[];
	timestamp: long;
	total: TotalFileSystemStats;
}
@namespace("cluster.nodes_stats")
class HttpStats {
	current_open: integer;
	total_opened: long;
}
@namespace("indices.monitoring.indices_stats")
class IndexStats {
	completion: CompletionStats;
	docs: DocStats;
	fielddata: FielddataStats;
	flush: FlushStats;
	get: GetStats;
	indexing: IndexingStats;
	merges: MergesStats;
	query_cache: QueryCacheStats;
	recovery: RecoveryStats;
	refresh: RefreshStats;
	request_cache: RequestCacheStats;
	search: SearchStats;
	segments: SegmentsStats;
	store: StoreStats;
	translog: TranslogStats;
	warmer: WarmerStats;
}
@namespace("common_options.stats")
class FlushStats {
	periodic: long;
	total: long;
	total_time: string;
	total_time_in_millis: long;
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
class IndexingStats {
	index_current: long;
	delete_current: long;
	delete_time: string;
	delete_time_in_millis: long;
	delete_total: long;
	is_throttled: boolean;
	noop_update_total: long;
	throttle_time: string;
	throttle_time_in_millis: long;
	index_time: string;
	index_time_in_millis: long;
	index_total: long;
	types: Dictionary<string, IndexingStats>;
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
	total_size_in_bytes: long;
	total_stopped_time: string;
	total__stopped_time_in_millis: long;
	total_throttled_time: string;
	total_throttled_time_in_millis: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("common_options.stats")
class RecoveryStats {
	current_as_source: long;
	current_as_target: long;
	throttle_time: string;
	throttle_time_in_millis: long;
}
@namespace("common_options.stats")
class RefreshStats {
	total: long;
	total_time: string;
	total_time_in_millis: long;
	external_total: long;
	external_total_time_in_millis: long;
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
class SearchStats {
	fetch_current: long;
	fetch_time_in_millis: long;
	fetch_total: long;
	open_contexts: long;
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
class TranslogStats {
	earliest_last_modified_age: long;
	operations: long;
	size: string;
	size_in_bytes: long;
	uncommitted_operations: integer;
	uncommitted_size: string;
	uncommitted_size_in_bytes: long;
}
@namespace("common_options.stats")
class WarmerStats {
	current: long;
	total: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("cluster.nodes_stats.statistics")
class NodeIngestStats {
	pipelines: Dictionary<string, IngestStats>;
	total: IngestStats;
}
@namespace("cluster.nodes_stats.statistics")
class IngestStats {
	count: long;
	current: long;
	failed: long;
	time_in_millis: long;
	processors: KeyedProcessorStats[];
}
@namespace("cluster.nodes_stats.statistics")
class KeyedProcessorStats {
	type: string;
	statistics: ProcessStats;
}
@namespace("cluster.nodes_stats")
class ProcessStats {
	cpu: CPUStats;
	mem: MemoryStats;
	open_file_descriptors: integer;
	timestamp: long;
}
@namespace("cluster.nodes_stats")
class NodeJvmStats {
	buffer_pools: Dictionary<string, NodeBufferPool>;
	classes: JvmClassesStats;
	gc: GarbageCollectionStats;
	mem: MemoryStats;
	threads: ThreadStats;
	timestamp: long;
	uptime: string;
	uptime_in_millis: long;
}
@namespace("cluster.nodes_stats")
class OperatingSystemStats {
	cpu: CPUStats;
	mem: ExtendedMemoryStats;
	swap: MemoryStats;
	timestamp: long;
}
@namespace("cluster.nodes_stats")
class ScriptStats {
	cache_evictions: long;
	compilations: long;
}
@namespace("cluster.nodes_stats")
class ThreadCountStats {
	active: long;
	completed: long;
	largest: long;
	queue: long;
	rejected: long;
	threads: long;
}
@namespace("cluster.nodes_stats")
class TransportStats {
	rx_count: long;
	rx_size: string;
	rx_size_in_bytes: long;
	server_open: integer;
	tx_count: long;
	tx_size: string;
	tx_size_in_bytes: long;
}
@namespace("cluster.nodes_usage")
class NodeUsageInformation {
	rest_actions: Dictionary<string, integer>;
	since: Date;
	timestamp: Date;
}
@namespace("cluster.remote_info")
class RemoteInfo {
	connected: boolean;
	skip_unavailable: boolean;
	initial_connect_timeout: Time;
	max_connections_per_cluster: integer;
	num_nodes_connected: long;
	seeds: string[];
}
@namespace("common_abstractions.response")
class ElasticsearchVersionInfo {
	lucene_version: string;
	number: string;
	build_flavor: string;
	build_type: string;
	build_hash: string;
	build_date: Date;
	build_snapshot: boolean;
	minimum_wire_compatibility_version: string;
	minimum_index_compatibility_version: string;
}
@namespace("cluster.task_management.list_tasks")
class TaskExecutingNode {
	attributes: Dictionary<string, string>;
	host: string;
	ip: string;
	name: string;
	roles: string[];
	tasks: Dictionary<TaskId, TaskState>;
	transport_address: string;
}
@namespace("cluster.task_management.list_tasks")
class TaskState {
	action: string;
	cancellable: boolean;
	description: string;
	headers: Dictionary<string, string>;
	id: long;
	node: string;
	parent_task_id: TaskId;
	running_time_in_nanos: long;
	start_time_in_millis: long;
	status: TaskStatus;
	type: string;
}
@namespace("cluster.task_management.list_tasks")
class TaskStatus {
	batches: long;
	created: long;
	deleted: long;
	noops: long;
	requests_per_second: float;
	retries: TaskRetries;
	throttled_millis: long;
	throttled_until_millis: long;
	total: long;
	updated: long;
	version_conflicts: long;
}
@namespace("cluster.task_management.list_tasks")
class TaskRetries {
	bulk: integer;
	search: integer;
}
@namespace("cluster.task_management.get_task")
class TaskInfo {
	action: string;
	cancellable: boolean;
	children: TaskInfo[];
	description: string;
	headers: Dictionary<string, string>;
	id: long;
	node: string;
	running_time_in_nanos: long;
	start_time_in_millis: long;
	status: TaskStatus;
	type: string;
}
@namespace("common_options.hit")
class ShardStatistics {
	failed: integer;
	failures: ShardFailure[];
	successful: integer;
	total: integer;
}
@namespace("common_options.geo")
class Distance {
	precision: double;
	unit: DistanceUnit;
}
@namespace("document.multiple")
class BulkIndexByScrollFailure {
	cause: MainError;
	id: string;
	index: string;
	status: integer;
	type: string;
}
@namespace("document.multiple")
class Retries {
	bulk: long;
	search: long;
}
@namespace("document.single.term_vectors")
class TermVector {
	field_statistics: FieldStatistics;
	terms: Dictionary<string, TermVectorTerm>;
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
	score: double;
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
@namespace("document.multiple.reindex_on_server")
class ReindexRouting {
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexNode {
	attributes: Dictionary<string, string>;
	host: string;
	ip: string;
	name: string;
	roles: string[];
	tasks: Dictionary<TaskId, ReindexTask>;
	transport_address: string;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexTask {
	action: string;
	cancellable: boolean;
	description: string;
	id: long;
	node: string;
	running_time_in_nanos: long;
	start_time_in_millis: long;
	status: ReindexStatus;
	type: string;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexStatus {
	batches: long;
	created: long;
	deleted: long;
	noops: long;
	requests_per_second: float;
	retries: Retries;
	throttled_millis: long;
	throttled_until_millis: long;
	total: long;
	updated: long;
	version_conflicts: long;
}
@namespace("indices.alias_management.get_alias")
class IndexAliases {
	aliases: Dictionary<string, AliasDefinition>;
}
@namespace("indices.alias_management")
class AliasDefinition {
	filter: QueryContainer;
	index_routing: string;
	is_write_index: boolean;
	routing: string;
	search_routing: string;
}
@namespace("indices.analyze")
class AnalyzeDetail {
	charfilters: CharFilterDetail[];
	custom_analyzer: boolean;
	tokenfilters: TokenDetail[];
	tokenizer: TokenDetail;
}
@namespace("indices.analyze")
class CharFilterDetail {
	filtered_text: string[];
	name: string;
}
@namespace("indices.analyze")
class TokenDetail {
	name: string;
	tokens: ExplainAnalyzeToken[];
}
@namespace("indices.analyze")
class ExplainAnalyzeToken {
	bytes: string;
	end_offset: long;
	keyword: boolean;
	position: long;
	positionLength: long;
	start_offset: long;
	termFrequency: long;
	token: string;
	type: string;
}
@namespace("indices.analyze")
class AnalyzeToken {
	end_offset: long;
	position: long;
	position_length: long;
	start_offset: long;
	token: string;
	type: string;
}
@namespace("indices.index_management.open_close_index.close_index")
class CloseIndexResult {
	closed: boolean;
	shards: Dictionary<string, CloseShardResult>;
}
@namespace("indices.index_management.open_close_index.close_index")
class CloseShardResult {
	failures: ShardFailure[];
}
@namespace("indices.mapping_management.get_field_mapping")
class TypeFieldMappings {
	mappings: Dictionary<Field, FieldMapping>;
}
@namespace("indices.mapping_management.get_mapping")
class IndexMappings {
	item: TypeMapping;
	mappings: TypeMapping;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStatus {
	shards: ShardRecovery[];
}
@namespace("indices.monitoring.indices_recovery")
class ShardRecovery {
	id: long;
	index: RecoveryIndexStatus;
	primary: boolean;
	source: RecoveryOrigin;
	stage: string;
	start: RecoveryStartStatus;
	start_time_in_millis: Date;
	stop_time_in_millis: Date;
	target: RecoveryOrigin;
	total_time_in_millis: long;
	translog: RecoveryTranslogStatus;
	type: string;
	verify_index: RecoveryVerifyIndex;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryIndexStatus {
	bytes: RecoveryBytes;
	files: RecoveryFiles;
	size: RecoveryBytes;
	source_throttle_time_in_millis: long;
	target_throttle_time_in_millis: long;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryBytes {
	percent: string;
	recovered: long;
	reused: long;
	total: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryFiles {
	details: RecoveryFileDetails[];
	percent: string;
	recovered: long;
	reused: long;
	total: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryFileDetails {
	length: long;
	name: string;
	recovered: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryOrigin {
	hostname: string;
	id: string;
	ip: string;
	name: string;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStartStatus {
	check_index_time: long;
	total_time_in_millis: string;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryTranslogStatus {
	percent: string;
	recovered: long;
	total: long;
	total_on_start: long;
	total_time: string;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryVerifyIndex {
	check_index_time_in_millis: long;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_segments")
class IndexSegment {
	shards: Dictionary<string, ShardsSegment>;
}
@namespace("indices.monitoring.indices_segments")
class ShardsSegment {
	num_committed_segments: integer;
	routing: ShardSegmentRouting;
	num_search_segments: integer;
	segments: Dictionary<string, Segment>;
}
@namespace("indices.monitoring.indices_segments")
class ShardSegmentRouting {
	node: string;
	primary: boolean;
	state: string;
}
@namespace("indices.monitoring.indices_segments")
class Segment {
	attributes: Dictionary<string, string>;
	committed: boolean;
	compound: boolean;
	deleted_docs: long;
	generation: integer;
	memory_in_bytes: double;
	search: boolean;
	size_in_bytes: double;
	num_docs: long;
	version: string;
}
@namespace("indices.monitoring.indices_shard_stores")
class IndicesShardStores {
	shards: Dictionary<string, ShardStoreWrapper>;
}
@namespace("indices.monitoring.indices_shard_stores")
class ShardStoreWrapper {
	stores: ShardStore[];
}
@namespace("indices.monitoring.indices_shard_stores")
class ShardStore {
	allocation: ShardStoreAllocation;
	allocation_id: string;
	attributes: Dictionary<string, any>;
	id: string;
	legacy_version: long;
	name: string;
	store_exception: ShardStoreException;
	transport_address: string;
}
@namespace("indices.monitoring.indices_shard_stores")
class ShardStoreException {
	reason: string;
	type: string;
}
@namespace("indices.monitoring.indices_stats")
class IndicesStats {
	primaries: IndexStats;
	shards: Dictionary<string, ShardStats[]>;
	total: IndexStats;
	uuid: string;
}
@namespace("indices.monitoring.indices_stats")
class ShardStats {
	commit: ShardCommit;
	completion: ShardCompletion;
	docs: ShardDocs;
	fielddata: ShardFielddata;
	flush: ShardFlush;
	get: ShardGet;
	indexing: ShardIndexing;
	merges: ShardMerges;
	shard_path: ShardPath;
	query_cache: ShardQueryCache;
	recovery: ShardStatsRecovery;
	refresh: ShardRefresh;
	request_cache: ShardRequestCache;
	routing: ShardRouting;
	search: ShardSearch;
	segments: ShardSegments;
	seq_no: ShardSequenceNumber;
	store: ShardStatsStore;
	translog: ShardTransactionLog;
	warmer: ShardWarmer;
}
@namespace("indices.monitoring.indices_stats")
class ShardCommit {
	generation: integer;
	id: string;
	num_docs: long;
	user_data: Dictionary<string, string>;
}
@namespace("indices.monitoring.indices_stats")
class ShardCompletion {
	size_in_bytes: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardDocs {
	count: long;
	deleted: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardFielddata {
	evictions: long;
	memory_size_in_bytes: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardFlush {
	total: long;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardGet {
	current: long;
	exists_time_in_millis: long;
	exists_total: long;
	missing_time_in_millis: long;
	missing_total: long;
	time_in_millis: long;
	total: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardIndexing {
	delete_current: long;
	delete_time_in_millis: long;
	delete_total: long;
	index_current: long;
	index_failed: long;
	index_time_in_millis: long;
	index_total: long;
	is_throttled: boolean;
	noop_update_total: long;
	throttle_time_in_millis: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardMerges {
	current: long;
	current_docs: long;
	current_size_in_bytes: long;
	total: long;
	total_auto_throttle_in_bytes: long;
	total_docs: long;
	total_size_in_bytes: long;
	total_stopped_time_in_millis: long;
	total_throttled_time_in_millis: long;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardPath {
	data_path: string;
	is_custom_data_path: boolean;
	state_path: string;
}
@namespace("indices.monitoring.indices_stats")
class ShardQueryCache {
	cache_count: long;
	cache_size: long;
	evictions: long;
	hit_count: long;
	memory_size_in_bytes: long;
	miss_count: long;
	total_count: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardStatsRecovery {
	current_as_source: long;
	current_as_target: long;
	throttle_time_in_millis: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardRefresh {
	listeners: long;
	total: long;
	total_time_in_millis: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardRequestCache {
	evictions: long;
	hit_count: long;
	memory_size_in_bytes: long;
	miss_count: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardRouting {
	node: string;
	primary: boolean;
	relocating_node: string;
	state: ShardRoutingState;
}
@namespace("indices.monitoring.indices_stats")
class ShardSearch {
	fetch_current: long;
	fetch_time_in_millis: long;
	fetch_total: long;
	open_contexts: long;
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
@namespace("indices.monitoring.indices_stats")
class ShardSegments {
	count: long;
	doc_values_memory_in_bytes: long;
	file_sizes: Dictionary<string, ShardFileSizeInfo>;
	fixed_bit_set_memory_in_bytes: long;
	index_writer_memory_in_bytes: long;
	max_unsafe_auto_id_timestamp: long;
	memory_in_bytes: long;
	norms_memory_in_bytes: long;
	points_memory_in_bytes: long;
	stored_fields_memory_in_bytes: long;
	terms_memory_in_bytes: long;
	term_vectors_memory_in_bytes: long;
	version_map_memory_in_bytes: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardSequenceNumber {
	global_checkpoint: long;
	local_checkpoint: long;
	max_seq_no: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardStatsStore {
	size_in_bytes: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardTransactionLog {
	operations: long;
	size_in_bytes: long;
	uncommitted_operations: long;
	uncommitted_size_in_bytes: long;
}
@namespace("indices.monitoring.indices_stats")
class ShardWarmer {
	current: long;
	total: long;
	total_time_in_millis: long;
}
@namespace("ingest.simulate_pipeline")
class PipelineSimulation {
	doc: DocumentSimulation;
	processor_results: PipelineSimulation[];
	tag: string;
}
@namespace("ingest.simulate_pipeline")
class DocumentSimulation {
	_id: string;
	_index: string;
	_ingest: Ingest;
	_parent: string;
	_routing: string;
	_source: LazyDocument;
	_type: string;
}
@namespace("ingest.simulate_pipeline")
class Ingest {
	timestamp: Date;
}
@namespace("modules.indices.fielddata")
class FielddataSettings {
	cache_expire: Time;
	cache_size: string;
}
@namespace("modules.snapshot_and_restore.repositories.cleanup_repository")
class CleanupRepositoryResults {
	deleted_bytes: long;
	deleted_blobs: long;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
class CompactNodeInfo {
	name: string;
}
@namespace("modules.snapshot_and_restore.restore")
class SnapshotRestore {
	indices: IndexName[];
	snapshot: string;
	shards: ShardStatistics;
}
@namespace("modules.snapshot_and_restore.snapshot")
class Snapshot {
	duration_in_millis: long;
	end_time: Date;
	end_time_in_millis: long;
	failures: SnapshotShardFailure[];
	indices: IndexName[];
	snapshot: string;
	shards: ShardStatistics;
	start_time: Date;
	start_time_in_millis: long;
	state: string;
	metadata: Dictionary<string, any>;
}
@namespace("modules.snapshot_and_restore.snapshot")
class SnapshotShardFailure {
	index: string;
	node_id: string;
	reason: string;
	shard_id: string;
	status: string;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStatus {
	include_global_state: boolean;
	indices: Dictionary<string, SnapshotIndexStats>;
	repository: string;
	shards_stats: SnapshotShardsStats;
	snapshot: string;
	state: string;
	stats: SnapshotStats;
	uuid: string;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotIndexStats {
	shards: Dictionary<string, SnapshotShardsStats>;
	shards_stats: SnapshotShardsStats;
	stats: SnapshotStats;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotShardsStats {
	done: long;
	failed: long;
	finalizing: long;
	initializing: long;
	started: long;
	total: long;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStats {
	incremental: FileCountSnapshotStats;
	total: FileCountSnapshotStats;
	start_time_in_millis: long;
	time_in_millis: long;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class FileCountSnapshotStats {
	file_count: integer;
	size_in_bytes: long;
}
@namespace("x_pack.graph.explore.request")
class GraphVertexInclude {
	boost: double;
	term: string;
}
@namespace("x_pack.graph.explore.request")
class SampleDiversity {
	field: Field;
	max_docs_per_value: integer;
}
@namespace("x_pack.license.get_license")
class License {
	expiry_date_in_millis: long;
	issue_date_in_millis: long;
	issued_to: string;
	issuer: string;
	max_nodes: long;
	signature: string;
	type: LicenseType;
	uid: string;
}
@namespace("x_pack.machine_learning.post_calendar_events")
class ScheduledEvent {
	calendar_id: Id;
	description: string;
	start_time: Date;
	end_time: Date;
	event_id: Id;
}
@namespace("aggregations.bucket.histogram")
class ExtendedBounds<T> {
	max: T;
	min: T;
}
@namespace("aggregations.bucket.terms")
class TermsExclude {
	pattern: string;
	values: string[];
}
@namespace("aggregations.bucket.terms")
class TermsInclude {
	num_partitions: long;
	partition: long;
	pattern: string;
	values: string[];
}
@namespace("aggregations.bucket.significant_terms")
class IncludeExclude {
	pattern: string;
	values: string[];
}
@namespace("x_pack.machine_learning.job.detectors")
class FilterRef {
	filter_id: Id;
	filter_type: RuleFilterType;
}
@namespace("x_pack.watcher.execution")
class SimulatedActions {
	actions: string[];
	all: SimulatedActions;
	use_all: boolean;
}
@namespace("x_pack.watcher.acknowledge_watch")
class WatchStatus {
	actions: Dictionary<string, ActionStatus>;
	last_checked: Date;
	last_met_condition: Date;
	state: ActivationState;
	version: integer;
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
	state: AcknowledgementState;
	timestamp: Date;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ExecutionState {
	successful: boolean;
	timestamp: Date;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ThrottleState {
	reason: string;
	timestamp: Date;
}
@namespace("x_pack.watcher.acknowledge_watch")
class ActivationState {
	active: boolean;
	timestamp: Date;
}
@namespace("search.explain")
class ExplanationDetail {
	description: string;
	details: ExplanationDetail[];
	value: float;
}
@namespace("search.field_capabilities")
class FieldCapabilities {
	aggregatable: boolean;
	indices: Indices;
	non_aggregatable_indices: Indices;
	non_searchable_indices: Indices;
	searchable: boolean;
}
@namespace("search.search_shards")
class SearchNode {
	name: string;
	transport_address: string;
}
@namespace("search.search_shards")
class SearchShard {
	index: string;
	node: string;
	primary: boolean;
	relocating_node: string;
	shard: integer;
	state: string;
}
@namespace("common_options.hit")
class ClusterStatistics {
	skipped: integer;
	successful: integer;
	total: integer;
}
@namespace("search.explain")
class Explanation {
	description: string;
	details: ExplanationDetail[];
	value: float;
}
@namespace("search.search.hits")
class InnerHitsResult {
	hits: InnerHitsMetadata;
}
@namespace("search.search.hits")
class InnerHitsMetadata {
	hits: Hit<LazyDocument>[];
	max_score: double;
	total: TotalHits;
}
@namespace("search.search.hits")
class NestedIdentity {
	field: Field;
	_nested: NestedIdentity;
	offset: integer;
}
@namespace("search.search.hits")
class TotalHits {
	relation: TotalHitsRelation;
	value: long;
}
@namespace("search.search.profile")
class Profile {
	shards: ShardProfile[];
}
@namespace("search.search.profile")
class ShardProfile {
	aggregations: AggregationProfile[];
	id: string;
	searches: SearchProfile[];
}
@namespace("search.search.profile")
class AggregationProfile {
	breakdown: AggregationBreakdown;
	description: string;
	time_in_nanos: long;
	type: string;
}
@namespace("search.search.profile")
class AggregationBreakdown {
	build_aggregation: long;
	build_aggregation_count: long;
	collect: long;
	collect_count: long;
	initialize: long;
	intialize_count: long;
	reduce: long;
	reduce_count: long;
}
@namespace("search.search.profile")
class SearchProfile {
	collector: Collector[];
	query: QueryProfile[];
	rewrite_time: long;
}
@namespace("search.search.profile")
class Collector {
	children: Collector[];
	name: string;
	reason: string;
	time_in_nanos: long;
}
@namespace("search.search.profile")
class QueryProfile {
	breakdown: QueryBreakdown;
	children: QueryProfile[];
	description: string;
	time_in_nanos: long;
	type: string;
}
@namespace("search.search.profile")
class QueryBreakdown {
	advance: long;
	build_scorer: long;
	create_weight: long;
	match: long;
	next_doc: long;
	score: long;
}
@namespace("search.validate")
class ValidationExplanation {
	error: string;
	explanation: string;
	index: string;
	valid: boolean;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_index_stats")
class FollowIndexStats {
	index: string;
	shards: FollowIndexShardStats[];
}
@namespace("x_pack.cross_cluster_replication.follow.follow_index_stats")
class FollowIndexShardStats {
	bytes_read: long;
	failed_read_requests: long;
	failed_write_requests: long;
	follower_global_checkpoint: long;
	follower_index: string;
	follower_mapping_version: long;
	follower_max_seq_no: long;
	follower_settings_version: long;
	follower_aliases_version: long;
	last_requested_seq_no: long;
	leader_global_checkpoint: long;
	leader_index: string;
	leader_max_seq_no: long;
	operations_read: long;
	operations_written: long;
	outstanding_read_requests: integer;
	outstanding_write_requests: integer;
	remote_cluster: string;
	shard_id: integer;
	successful_read_requests: long;
	successful_write_requests: long;
	total_read_remote_exec_time_millis: long;
	total_read_time_millis: long;
	total_write_time_millis: long;
	write_buffer_operation_count: long;
	write_buffer_size_in_bytes: long;
	read_exceptions: FollowIndexReadException[];
	time_since_last_read_millis: long;
	fatal_exception: ErrorCause;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_index_stats")
class FollowIndexReadException {
	from_seq_no: long;
	retries: integer;
	exception: ErrorCause;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_info")
class FollowerInfo {
	follower_index: string;
	remote_cluster: string;
	leader_index: string;
	status: FollowerIndexStatus;
	parameters: FollowConfig;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_info")
class FollowConfig {
	max_read_request_operation_count: integer;
	max_read_request_size: string;
	max_outstanding_read_requests: integer;
	max_write_request_operation_count: integer;
	max_write_request_size: string;
	max_outstanding_write_requests: integer;
	max_write_buffer_count: integer;
	max_write_buffer_size: string;
	max_retry_delay: Time;
	read_poll_timeout: Time;
}
@namespace("x_pack.cross_cluster_replication.stats")
class CcrAutoFollowStats {
	number_of_failed_follow_indices: long;
	number_of_failed_remote_cluster_state_requests: long;
	number_of_successful_follow_indices: long;
	recent_auto_follow_errors: ErrorCause[];
	auto_followed_clusters: AutoFollowedCluster[];
}
@namespace("x_pack.cross_cluster_replication.stats")
class AutoFollowedCluster {
	cluster_name: string;
	time_since_last_check_millis: Date;
	last_seen_metadata_version: long;
}
@namespace("x_pack.cross_cluster_replication.stats")
class CcrFollowStats {
	indices: FollowIndexStats[];
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
@namespace("x_pack.ilm.explain_lifecycle")
class LifecycleExplain {
	action: string;
	action_time_millis: Date;
	failed_step: string;
	index: IndexName;
	lifecycle_date_millis: Date;
	managed: boolean;
	phase: string;
	phase_time_millis: Date;
	policy: string;
	step: string;
	step_info: Dictionary<string, any>;
	step_time_millis: Date;
	age: Time;
}
@namespace("x_pack.ilm.get_lifecycle")
class LifecyclePolicy {
	modified_date: Date;
	policy: Policy;
	version: integer;
}
@namespace("x_pack.info.x_pack_info")
class XPackBuildInformation {
	date: Date;
	hash: string;
}
@namespace("x_pack.info.x_pack_info")
class XPackFeatures {
	ccr: XPackFeature;
	data_frame: XPackFeature;
	flattened: XPackFeature;
	data_science: XPackFeature;
	graph: XPackFeature;
	ilm: XPackFeature;
	logstash: XPackFeature;
	ml: XPackFeature;
	monitoring: XPackFeature;
	rollup: XPackFeature;
	security: XPackFeature;
	sql: XPackFeature;
	vectors: XPackFeature;
	watcher: XPackFeature;
}
@namespace("x_pack.info.x_pack_info")
class XPackFeature {
	available: boolean;
	description: string;
	enabled: boolean;
	native_code_info: NativeCodeInformation;
}
@namespace("x_pack.info.x_pack_info")
class NativeCodeInformation {
	build_hash: string;
	version: string;
}
@namespace("x_pack.info.x_pack_info")
class MinimalLicenseInformation {
	expiry_date_in_millis: long;
	mode: LicenseType;
	status: LicenseStatus;
	type: LicenseType;
	uid: string;
}
@namespace("x_pack.info.x_pack_usage")
class XPackUsage {
	available: boolean;
	enabled: boolean;
}
@namespace("x_pack.info.x_pack_usage")
class QueryUsage {
	total: integer;
	paging: integer;
	failed: integer;
	count: integer;
}
@namespace("x_pack.info.x_pack_usage")
class IlmUsage {
	policy_count: integer;
	policy_stats: IlmPolicyStatistics[];
}
@namespace("x_pack.license.get_license")
class LicenseInformation {
	expiry_date: Date;
	expiry_date_in_millis: long;
	issue_date: Date;
	issue_date_in_millis: long;
	issued_to: string;
	issuer: string;
	max_nodes: long;
	status: LicenseStatus;
	type: LicenseType;
	uid: string;
}
@namespace("x_pack.license.post_license")
class LicenseAcknowledgement {
	license: string[];
	message: string;
}
@namespace("x_pack.machine_learning.job.results")
class AnomalyRecord {
	actual: double[];
	bucket_span: Time;
	by_field_name: string;
	by_field_value: string;
	causes: AnomalyCause[];
	detector_index: integer;
	field_name: string;
	function: string;
	function_description: string;
	influencers: Influence[];
	initial_record_score: double;
	is_interim: boolean;
	job_id: string;
	over_field_name: string;
	over_field_value: string;
	partition_field_name: string;
	partition_field_value: string;
	probability: double;
	record_score: double;
	result_type: string;
	timestamp: Date;
	typical: double[];
}
@namespace("x_pack.machine_learning.job.results")
class AnomalyCause {
	actual: double[];
	by_field_name: string;
	by_field_value: string;
	correlated_by_field_value: string;
	field_name: string;
	function: string;
	function_description: string;
	influencers: Influence[];
	over_field_name: string;
	over_field_value: string;
	partition_field_name: string;
	partition_field_value: string;
	probability: double;
	typical: double[];
}
@namespace("x_pack.machine_learning.job.results")
class Influence {
	influencer_field_name: string;
	influencer_field_values: string[];
}
@namespace("x_pack.machine_learning.job.results")
class Bucket {
	anomaly_score: double;
	bucket_influencers: BucketInfluencer[];
	bucket_span: Time;
	event_count: long;
	initial_anomaly_score: double;
	is_interim: boolean;
	job_id: string;
	partition_scores: PartitionScore[];
	processing_time_ms: double;
	result_type: string;
	timestamp: Date;
}
@namespace("x_pack.machine_learning.job.results")
class BucketInfluencer {
	bucket_span: long;
	influencer_field_name: string;
	influencer_field_value: string;
	influencer_score: double;
	initial_influencer_score: double;
	is_interim: boolean;
	job_id: string;
	probability: double;
	result_type: string;
	timestamp: Date;
}
@namespace("x_pack.machine_learning.job.results")
class PartitionScore {
	initial_record_score: double;
	partition_field_name: string;
	partition_field_value: string;
	probability: double;
	record_score: double;
}
@namespace("x_pack.machine_learning.get_calendars")
class Calendar {
	calendar_id: string;
	job_ids: string[];
	description: string;
}
@namespace("x_pack.machine_learning.job.results")
class CategoryDefinition {
	category_id: long;
	examples: string[];
	job_id: string;
	max_matching_length: long;
	regex: string;
	terms: string;
}
@namespace("x_pack.machine_learning.datafeed")
class DatafeedStats {
	assignment_explanation: string;
	datafeed_id: string;
	node: DiscoveryNode;
	state: DatafeedState;
	timing_stats: DatafeedTimingStats;
}
@namespace("x_pack.machine_learning.datafeed")
class DiscoveryNode {
	attributes: Dictionary<string, string>;
	ephemeral_id: string;
	id: string;
	name: string;
	transport_address: string;
}
@namespace("x_pack.machine_learning.datafeed")
class DatafeedTimingStats {
	bucket_count: long;
	exponential_average_search_time_per_hour_ms: double;
	job_id: string;
	search_count: long;
	total_search_time_ms: double;
}
@namespace("x_pack.machine_learning.datafeed")
class DatafeedConfig {
	aggregations: Dictionary<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	datafeed_id: string;
	frequency: Time;
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Dictionary<string, ScriptField>;
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.get_filters")
class Filter {
	description: string;
	filter_id: string;
	items: string[];
}
@namespace("x_pack.machine_learning.job.config")
class JobStats {
	assignment_explanation: string;
	data_counts: DataCounts;
	forecasts_stats: JobForecastStatistics;
	job_id: string;
	model_size_stats: ModelSizeStats;
	node: DiscoveryNode;
	open_time: Time;
	state: JobState;
	timing_stats: TimingStats;
}
@namespace("x_pack.machine_learning.job.process")
class DataCounts {
	bucket_count: long;
	earliest_record_timestamp: Date;
	empty_bucket_count: long;
	input_bytes: long;
	input_field_count: long;
	input_record_count: long;
	invalid_date_count: long;
	job_id: string;
	last_data_time: Date;
	latest_empty_bucket_timestamp: Date;
	latest_record_timestamp: Date;
	latest_sparse_bucket_timestamp: Date;
	missing_field_count: long;
	out_of_order_timestamp_count: long;
	processed_field_count: long;
	processed_record_count: long;
	sparse_bucket_count: long;
}
@namespace("x_pack.machine_learning.job.config")
class JobForecastStatistics {
	memory_bytes: JobStatistics;
	processing_time_ms: JobStatistics;
	records: JobStatistics;
	status: Dictionary<string, long>;
	total: long;
}
@namespace("x_pack.machine_learning.job.process")
class ModelSizeStats {
	bucket_allocation_failures_count: long;
	job_id: string;
	log_time: Date;
	memory_status: MemoryStatus;
	model_bytes: long;
	result_type: string;
	timestamp: Date;
	total_by_field_count: long;
	total_over_field_count: long;
	total_partition_field_count: long;
}
@namespace("x_pack.machine_learning.job.config")
class TimingStats {
	job_id: string;
	bucket_count: long;
	minimum_bucket_processing_time_ms: double;
	maximum_bucket_processing_time_ms: double;
	average_bucket_processing_time_ms: double;
	exponential_average_bucket_processing_time_ms: double;
	exponential_average_bucket_processing_time_per_hour_ms: double;
}
@namespace("x_pack.info.x_pack_usage")
class Job {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	background_persist_interval: Time;
	create_time: Date;
	data_description: DataDescription;
	description: string;
	finished_time: Date;
	job_id: string;
	job_type: string;
	model_plot: ModelPlotConfig;
	model_snapshot_id: string;
	model_snapshot_retention_days: long;
	renormalization_window_days: long;
	results_index_name: string;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.job.process")
class ModelSnapshot {
	description: string;
	job_id: string;
	latest_record_time_stamp: Date;
	latest_result_time_stamp: Date;
	model_size_stats: ModelSizeStats;
	retain: boolean;
	snapshot_doc_count: long;
	snapshot_id: string;
	timestamp: Date;
}
@namespace("x_pack.machine_learning.job.results")
class OverallBucket {
	bucket_span: long;
	is_interim: boolean;
	jobs: OverallBucketJobInfo[];
	overall_score: double;
	result_type: string;
	timestamp: Date;
}
@namespace("x_pack.machine_learning.job.results")
class OverallBucketJobInfo {
	job_id: string;
	max_anomaly_score: double;
}
@namespace("x_pack.machine_learning.machine_learning_info")
class Defaults {
	anomaly_detectors: AnomalyDetectors;
	datafeeds: Datafeeds;
}
@namespace("x_pack.machine_learning.machine_learning_info")
class AnomalyDetectors {
	model_memory_limit: string;
	categorization_examples_limit: integer;
	model_snapshot_retention_days: integer;
}
@namespace("x_pack.machine_learning.machine_learning_info")
class Datafeeds {
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.machine_learning_info")
class Limits {
	max_model_memory_limit: string;
}
@namespace("x_pack.migration.deprecation_info")
class DeprecationInfo {
	details: string;
	level: DeprecationWarningLevel;
	message: string;
	url: string;
}
@namespace("x_pack.roll_up.get_rollup_capabilities")
class RollupCapabilities {
	rollup_jobs: RollupCapabilitiesJob[];
}
@namespace("x_pack.roll_up.get_rollup_capabilities")
class RollupCapabilitiesJob {
	fields: Dictionary<Field, Dictionary<string, any>>;
	index_pattern: string;
	job_id: string;
	rollup_index: string;
}
@namespace("x_pack.roll_up.get_rollup_index_capabilities")
class RollupIndexCapabilities {
	rollup_jobs: RollupIndexCapabilitiesJob[];
}
@namespace("x_pack.roll_up.get_rollup_index_capabilities")
class RollupIndexCapabilitiesJob {
	fields: Dictionary<Field, Dictionary<string, string>>;
	index_pattern: string;
	job_id: string;
	rollup_index: string;
}
@namespace("x_pack.roll_up.get_rollup_job")
class RollupJobInformation {
	config: RollupJobConfiguration;
	stats: RollupJobStats;
	status: RollupJobStatus;
}
@namespace("x_pack.roll_up.get_rollup_job")
class RollupJobConfiguration {
	cron: string;
	groups: RollupGroupings;
	id: string;
	index_pattern: string;
	metrics: RollupFieldMetric[];
	page_size: long;
	rollup_index: IndexName;
	timeout: Time;
}
@namespace("x_pack.roll_up.get_rollup_job")
class RollupJobStats {
	documents_processed: long;
	pages_processed: long;
	rollups_indexed: long;
	trigger_count: long;
	search_failures: long;
	index_failures: long;
	index_time_in_ms: long;
	index_total: long;
	search_time_in_ms: long;
	search_total: long;
}
@namespace("x_pack.roll_up.get_rollup_job")
class RollupJobStatus {
	current_position: Dictionary<string, any>;
	job_state: IndexingJobState;
	upgraded_doc_id: boolean;
}
@namespace("x_pack.security.api_key.get_api_key")
class ApiKeys {
	creation: Date;
	expiration: Date;
	id: string;
	invalidated: boolean;
	name: string;
	realm: string;
	username: string;
}
@namespace("x_pack.security.authenticate")
class RealmInfo {
	name: string;
	type: string;
}
@namespace("x_pack.security")
class SecurityNode {
	name: string;
}
@namespace("x_pack.security.privileges.delete_privileges")
class FoundUserPrivilege {
	found: boolean;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class ApplicationResourcePrivileges {
	application: string;
	privileges: string[];
	resources: string[];
}
@namespace("x_pack.security.privileges.get_user_privileges")
class GlobalPrivileges {
	application: ApplicationGlobalUserPrivileges;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class ApplicationGlobalUserPrivileges {
	manage: ManageUserPrivileges;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class ManageUserPrivileges {
	applications: string[];
}
@namespace("x_pack.security.privileges.get_user_privileges")
class UserIndicesPrivileges {
	field_security: FieldSecuritySettings;
	names: string[];
	privileges: string[];
	query: QueryUserPrivileges;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class FieldSecuritySettings {
	except: string[];
	grant: string[];
}
@namespace("x_pack.security.privileges.get_user_privileges")
class QueryUserPrivileges {
	term: TermUserPrivileges;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class TermUserPrivileges {
	apps: boolean;
}
@namespace("x_pack.security.privileges.has_privileges")
class ResourcePrivileges {
	privileges: Dictionary<string, boolean>;
	resource: string;
}
@namespace("x_pack.security.privileges.put_privileges")
class PutPrivilegesStatus {
	created: boolean;
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
class XPackRoleMapping {
	enabled: boolean;
	metadata: Dictionary<string, any>;
	roles: string[];
	rules: RoleMappingRuleBase;
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
class PutRoleMappingStatus {
	created: boolean;
}
@namespace("x_pack.security.role.get_role")
class XPackRole {
	cluster: string[];
	indices: IndicesPrivileges[];
	metadata: Dictionary<string, any>;
	run_as: string[];
}
@namespace("x_pack.security.role.put_role")
class PutRoleStatus {
	created: boolean;
}
@namespace("x_pack.security.user.get_user")
class XPackUser {
	email: string;
	full_name: string;
	metadata: Dictionary<string, any>;
	roles: string[];
	username: string;
}
@namespace("x_pack.slm")
class SnapshotLifecyclePolicyMetadata {
	modified_date_millis: Date;
	next_execution_millis: Date;
	policy: SnapshotLifecyclePolicy;
	version: integer;
	in_progress: SnapshotLifecycleInProgress;
	last_success: SnapshotLifecycleInvocationRecord;
	last_failure: SnapshotLifecycleInvocationRecord;
}
@namespace("x_pack.slm")
class SnapshotLifecycleInProgress {
	name: string;
	uuid: string;
	state: string;
	start_time_millis: Date;
}
@namespace("x_pack.slm")
class SnapshotLifecycleInvocationRecord {
	snapshot_name: string;
	time: Date;
}
@namespace("x_pack.sql.query_sql")
class SqlColumn {
	name: string;
	type: string;
}
@namespace("x_pack.ssl.get_certificates")
class ClusterCertificateInformation {
	path: string;
	alias: string;
	format: string;
	subject_dn: string;
	serial_number: string;
	has_private_key: boolean;
	expiry: Date;
}
@namespace("x_pack.watcher.activate_watch")
class ActivationStatus {
	actions: Dictionary<string, ActionStatus>;
	state: ActivationState;
}
@namespace("x_pack.watcher.execute_watch")
class WatchRecord {
	condition: ConditionContainer;
	input: InputContainer;
	messages: string[];
	metadata: Dictionary<string, any>;
	result: ExecutionResult;
	state: ActionExecutionState;
	trigger_event: TriggerEventResult;
	user: string;
	node: string;
	watch_id: string;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResult {
	actions: ExecutionResultAction[];
	condition: ExecutionResultCondition;
	execution_duration: integer;
	execution_time: Date;
	input: ExecutionResultInput;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultAction {
	email: EmailActionResult;
	id: string;
	index: IndexActionResult;
	logging: LoggingActionResult;
	pagerduty: PagerDutyActionResult;
	reason: string;
	slack: SlackActionResult;
	status: Status;
	type: ActionType;
	webhook: WebhookActionResult;
}
@namespace("x_pack.watcher.execution.email")
class EmailActionResult {
	account: string;
	message: EmailResult;
	reason: string;
}
@namespace("x_pack.watcher.execution.email")
class EmailResult {
	bcc: string[];
	body: EmailBody;
	cc: string[];
	from: string;
	id: string;
	priority: EmailPriority;
	reply_to: string[];
	sent_date: Date;
	subject: string;
	to: string[];
}
@namespace("x_pack.watcher.execution.index")
class IndexActionResult {
	id: string;
	response: IndexActionResultIndexResponse;
}
@namespace("x_pack.watcher.execution.index")
class IndexActionResultIndexResponse {
	created: boolean;
	id: string;
	index: IndexName;
	result: Result;
	version: integer;
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
@namespace("x_pack.watcher.execution")
class HttpInputResponseResult {
	body: string;
	headers: Dictionary<string, string[]>;
	status: integer;
}
@namespace("x_pack.watcher.execution.slack")
class SlackActionResult {
	account: string;
	sent_messages: SlackActionMessageResult[];
}
@namespace("x_pack.watcher.execution.slack")
class SlackActionMessageResult {
	message: SlackMessage;
	reason: string;
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
	status: Status;
	to: string;
}
@namespace("x_pack.watcher.execution.webhook")
class WebhookActionResult {
	request: HttpInputRequestResult;
	response: HttpInputResponseResult;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultCondition {
	met: boolean;
	status: Status;
	type: ConditionType;
}
@namespace("x_pack.watcher.execute_watch")
class ExecutionResultInput {
	payload: Dictionary<string, any>;
	status: Status;
	type: InputType;
}
@namespace("x_pack.watcher.execute_watch")
class TriggerEventResult {
	manual: TriggerEventContainer;
	triggered_time: Date;
	type: string;
}
@namespace("x_pack.watcher.watcher_stats")
class WatcherNodeStats {
	current_watches: WatchRecordStats[];
	execution_thread_pool: ExecutionThreadPool;
	queued_watches: WatchRecordQueuedStats[];
	watch_count: long;
	watcher_state: WatcherState;
}
@namespace("x_pack.watcher.watcher_stats")
class WatchRecordQueuedStats {
	execution_time: Date;
	triggered_time: Date;
	watch_id: string;
	watch_record_id: string;
}
@namespace("x_pack.watcher.watcher_stats")
class ExecutionThreadPool {
	max_size: long;
	queue_size: long;
}
@namespace("analysis")
class StopWords extends Union<string, string[]> {
}
@namespace("cat.cat_aliases")
class CatAliasesRecord implements ICatRecord {
	alias: string;
	filter: string;
	index: string;
	indexRouting: string;
	searchRouting: string;
}
@namespace("cat.cat_aliases")
class CatAliasesResponse extends ResponseBase {
	records: CatAliasesRecord[];
}
@namespace("cat.cat_allocation")
class CatAllocationRecord implements ICatRecord {
	diskAvail: string;
	diskRatio: string;
	diskUsed: string;
	ip: string;
	node: string;
	shards: string;
}
@namespace("cat.cat_allocation")
class CatAllocationResponse extends ResponseBase {
	records: CatAllocationRecord[];
}
@namespace("cat.cat_count")
class CatCountRecord implements ICatRecord {
	count: string;
	epoch: string;
	timestamp: string;
}
@namespace("cat.cat_count")
class CatCountResponse extends ResponseBase {
	records: CatCountRecord[];
}
@namespace("cat.cat_fielddata")
class CatFielddataRecord implements ICatRecord {
	field: string;
	host: string;
	id: string;
	ip: string;
	node: string;
	size: string;
}
@namespace("cat.cat_fielddata")
class CatFielddataResponse extends ResponseBase {
	records: CatFielddataRecord[];
}
@namespace("cat.cat_health")
class CatHealthRecord implements ICatRecord {
	cluster: string;
	epoch: string;
	init: string;
	'node.data': string;
	'node.total': string;
	pending_tasks: string;
	pri: string;
	relo: string;
	shards: string;
	status: string;
	timestamp: string;
	unassign: string;
}
@namespace("cat.cat_health")
class CatHealthResponse extends ResponseBase {
	records: CatHealthRecord[];
}
@namespace("cat.cat_help")
class CatHelpRecord implements ICatRecord {
	endpoint: string;
}
@namespace("cat.cat_help")
class CatHelpResponse extends ResponseBase {
	records: CatHelpRecord[];
}
@namespace("cat.cat_indices")
class CatIndicesRecord implements ICatRecord {
	'docs.count': string;
	'docs.deleted': string;
	health: string;
	index: string;
	uuid: string;
	pri: string;
	'pri.store.size': string;
	rep: string;
	status: string;
	'store.size': string;
	tm: string;
}
@namespace("cat.cat_indices")
class CatIndicesResponse extends ResponseBase {
	records: CatIndicesRecord[];
}
@namespace("cat.cat_master")
class CatMasterRecord implements ICatRecord {
	id: string;
	ip: string;
	node: string;
}
@namespace("cat.cat_master")
class CatMasterResponse extends ResponseBase {
	records: CatMasterRecord[];
}
@namespace("cat.cat_node_attributes")
class CatNodeAttributesRecord implements ICatRecord {
	attr: string;
	host: string;
	id: string;
	ip: string;
	node: string;
	port: long;
	pid: long;
	value: string;
}
@namespace("cat.cat_node_attributes")
class CatNodeAttributesResponse extends ResponseBase {
	records: CatNodeAttributesRecord[];
}
@namespace("cat.cat_nodes")
class CatNodesRecord implements ICatRecord {
	build: string;
	completion_size: string;
	cpu: string;
	disk_available: string;
	fielddata_evictions: string;
	fielddata_memory: string;
	file_descriptor_current: integer;
	file_descriptor_max: integer;
	file_descriptor_percent: integer;
	filter_cache_evictions: string;
	filter_cache_memory: string;
	flush_total: string;
	flush_total_time: string;
	get_current: string;
	get_exists_time: string;
	get_exists_total: string;
	get_missing_time: string;
	get_missing_total: string;
	get_time: string;
	get_total: string;
	heap_current: string;
	heap_max: string;
	heap_percent: string;
	id_cache_memory: string;
	indexing_delete_current: string;
	indexing_delete_time: string;
	indexing_delete_total: string;
	indexing_index_current: string;
	indexing_index_time: string;
	indexing_index_total: string;
	ip: string;
	jdk: string;
	load_15m: string;
	load_5m: string;
	load_1m: string;
	master: string;
	merges_current: string;
	merges_current_docs: string;
	merges_current_size: string;
	merges_total: string;
	merges_total_docs: string;
	merges_total_time: string;
	name: string;
	node_id: string;
	node_role: string;
	percolate_current: string;
	percolate_memory: string;
	percolate_queries: string;
	percolate_time: string;
	percolate_total: string;
	pid: string;
	port: string;
	ram_current: string;
	ram_max: string;
	ram_percent: string;
	refresh_time: string;
	refresh_total: string;
	search_fetch_current: string;
	search_fetch_time: string;
	search_fetch_total: string;
	search_open_contexts: string;
	search_query_current: string;
	search_query_time: string;
	search_query_total: string;
	segments_count: string;
	segments_index_writer_max_memory: string;
	segments_index_writer_memory: string;
	segments_memory: string;
	segments_version_map_memory: string;
	uptime: string;
	version: string;
}
@namespace("cat.cat_nodes")
class CatNodesResponse extends ResponseBase {
	records: CatNodesRecord[];
}
@namespace("cat.cat_pending_tasks")
class CatPendingTasksRecord implements ICatRecord {
	insertOrder: integer;
	priority: string;
	source: string;
	timeInQueue: string;
}
@namespace("cat.cat_pending_tasks")
class CatPendingTasksResponse extends ResponseBase {
	records: CatPendingTasksRecord[];
}
@namespace("cat.cat_plugins")
class CatPluginsRecord implements ICatRecord {
	component: string;
	description: string;
	id: string;
	isolation: string;
	name: string;
	type: string;
	url: string;
	version: string;
}
@namespace("cat.cat_plugins")
class CatPluginsResponse extends ResponseBase {
	records: CatPluginsRecord[];
}
@namespace("cat.cat_recovery")
class CatRecoveryRecord implements ICatRecord {
	bytes: string;
	bytes_percent: string;
	bytes_recovered: string;
	bytes_total: string;
	files: string;
	files_percent: string;
	files_recovered: string;
	files_total: string;
	index: string;
	repository: string;
	shard: string;
	snapshot: string;
	source_host: string;
	source_node: string;
	stage: string;
	target_host: string;
	target_node: string;
	time: string;
	translog_ops: long;
	translog_ops_percent: string;
	translog_ops_recovered: long;
	type: string;
}
@namespace("cat.cat_recovery")
class CatRecoveryResponse extends ResponseBase {
	records: CatRecoveryRecord[];
}
@namespace("cat.cat_repositories")
class CatRepositoriesRecord implements ICatRecord {
	id: string;
	type: string;
}
@namespace("cat.cat_repositories")
class CatRepositoriesResponse extends ResponseBase {
	records: CatRepositoriesRecord[];
}
@namespace("cat.cat_segments")
class CatSegmentsRecord implements ICatRecord {
	committed: string;
	compound: string;
	'docs.count': string;
	'docs.deleted': string;
	generation: string;
	id: string;
	index: string;
	ip: string;
	prirep: string;
	searchable: string;
	segment: string;
	shard: string;
	size: string;
	'size.memory': string;
	version: string;
}
@namespace("cat.cat_segments")
class CatSegmentsResponse extends ResponseBase {
	records: CatSegmentsRecord[];
}
@namespace("cat.cat_shards")
class CatShardsRecord implements ICatRecord {
	'completion.size': string;
	docs: string;
	'fielddata.evictions': string;
	'fielddata.memory_size': string;
	'filter_cache.memory_size': string;
	'flush.total': string;
	'flush.total_time': string;
	'get.current': string;
	'get.exists_time': string;
	'get.exists_total': string;
	'get.missing_time': string;
	'get.missing_total': string;
	'get.time': string;
	'get.total': string;
	id: string;
	'id_cache.memory_size': string;
	index: string;
	'indexing.delete_current': string;
	'indexing.delete_time': string;
	'indexing.delete_total': string;
	'indexing.index_current': string;
	'indexing.index_time': string;
	'indexing.index_total': string;
	ip: string;
	'merges.current': string;
	'merges.current_docs': string;
	'merges.current_size': string;
	'merges.total_docs': string;
	'merges.total_size': string;
	'merges.total_time': string;
	node: string;
	'percolate.current': string;
	'percolate.memory_size': string;
	'percolate.queries': string;
	'percolate.time': string;
	'percolate.total': string;
	prirep: string;
	'refresh.time': string;
	'refresh.total': string;
	'search.fetch_current': string;
	'search.fetch_time': string;
	'search.fetch_total': string;
	'search.open_contexts': string;
	'search.query_current': string;
	'search.query_time': string;
	'search.query_total': string;
	'segments.count': string;
	'segments.fixed_bitset_memory': string;
	'segments.index_writer_max_memory': string;
	'segments.index_writer_memory': string;
	'segments.memory': string;
	'segments.version_map_memory': string;
	shard: string;
	state: string;
	store: string;
	'warmer.current': string;
	'warmer.total': string;
	'warmer.total_time': string;
}
@namespace("cat.cat_shards")
class CatShardsResponse extends ResponseBase {
	records: CatShardsRecord[];
}
@namespace("cat.cat_snapshots")
class CatSnapshotsRecord implements ICatRecord {
	duration: Time;
	end_epoch: long;
	end_time: string;
	failed_shards: long;
	id: string;
	indices: long;
	start_epoch: long;
	start_time: string;
	status: string;
	successful_shards: long;
	total_shards: long;
}
@namespace("cat.cat_snapshots")
class CatSnapshotsResponse extends ResponseBase {
	records: CatSnapshotsRecord[];
}
@namespace("cat.cat_tasks")
class CatTasksRecord implements ICatRecord {
	action: string;
	ip: string;
	node: string;
	parent_task_id: string;
	running_time: string;
	start_time: string;
	task_id: string;
	timestamp: string;
	type: string;
}
@namespace("cat.cat_tasks")
class CatTasksResponse extends ResponseBase {
	records: CatTasksRecord[];
}
@namespace("cat.cat_templates")
class CatTemplatesRecord implements ICatRecord {
	index_patterns: string;
	name: string;
	order: long;
	version: long;
}
@namespace("cat.cat_templates")
class CatTemplatesResponse extends ResponseBase {
	records: CatTemplatesRecord[];
}
@namespace("cat.cat_thread_pool")
class CatThreadPoolRecord implements ICatRecord {
	active: integer;
	completed: long;
	core: integer;
	ephemeral_node_id: string;
	host: string;
	ip: string;
	keep_alive: Time;
	largest: integer;
	max: integer;
	name: string;
	node_id: string;
	node_name: string;
	pool_size: integer;
	port: integer;
	pid: integer;
	queue: integer;
	queue_size: integer;
	rejected: long;
	size: integer;
	type: string;
}
@namespace("cat.cat_thread_pool")
class CatThreadPoolResponse extends ResponseBase {
	records: CatThreadPoolRecord[];
}
@namespace("common_options.minimum_should_match")
class MinimumShouldMatch extends Union<integer, string> {
}
@namespace("query_dsl.multi_term_query_rewrite")
class MultiTermQueryRewrite {
	constant_score: MultiTermQueryRewrite;
	constant_score_boolean: MultiTermQueryRewrite;
	rewrite: RewriteMultiTerm;
	scoring_boolean: MultiTermQueryRewrite;
	size: integer;
}
@namespace("query_dsl.specialized.more_like_this.like")
class Like extends Union<string, LikeDocument> {
}
@namespace("aggregations.bucket.histogram")
class HistogramOrder {
	count_ascending: HistogramOrder;
	count_descending: HistogramOrder;
	key: string;
	key_ascending: HistogramOrder;
	key_descending: HistogramOrder;
	order: SortOrder;
}
@namespace("aggregations.bucket.terms")
class TermsOrder {
	count_ascending: TermsOrder;
	count_descending: TermsOrder;
	key: string;
	key_ascending: TermsOrder;
	key_descending: TermsOrder;
	order: SortOrder;
}
@namespace("search.suggesters.context_suggester")
class Context extends Union<string, GeoLocation> {
	category: string;
	geo: GeoLocation;
}
@namespace("x_pack.info.x_pack_usage")
class SqlUsage extends XPackUsage {
	features: Dictionary<string, integer>;
	queries: Dictionary<string, QueryUsage>;
}
@namespace("x_pack.info.x_pack_usage")
class CcrUsage extends XPackUsage {
	auto_follow_patterns_count: integer;
	follower_indices_count: integer;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingUsage extends XPackUsage {
	count: AlertingCount;
	execution: AlertingExecution;
	watch: AlertingInput;
}
@namespace("x_pack.info.x_pack_usage")
class MachineLearningUsage extends XPackUsage {
	node_count: integer;
	datafeeds: Dictionary<string, DataFeed>;
	jobs: Dictionary<string, Job>;
}
@namespace("x_pack.info.x_pack_usage")
class MonitoringUsage extends XPackUsage {
	collection_enabled: boolean;
	enabled_exporters: Dictionary<string, long>;
}
@namespace("x_pack.info.x_pack_usage")
class SecurityUsage extends XPackUsage {
	anonymous: SecurityFeatureToggle;
	audit: AuditUsage;
	ipfilter: IpFilterUsage;
	realms: Dictionary<string, RealmUsage>;
	role_mapping: Dictionary<string, RoleMappingUsage>;
	roles: Dictionary<string, RoleUsage>;
	ssl: SslUsage;
	system_key: SecurityFeatureToggle;
}
@namespace("x_pack.info.x_pack_usage")
class VectorUsage extends XPackUsage {
	dense_vector_fields_count: integer;
	sparse_vector_fields_count: integer;
	dense_vector_dims_avg_count: integer;
}
@namespace("x_pack.watcher.watcher_stats")
class WatchRecordStats extends WatchRecordQueuedStats {
	execution_phase: ExecutionPhase;
}
@namespace("common_abstractions.infer.name")
class Names extends String {}
@namespace("common_abstractions.infer.node_id")
class NodeIds extends String {}
@namespace("common_abstractions.infer.indices")
class Indices extends String {}
@namespace("common_abstractions.infer.index_name")
class IndexName extends String {}
@namespace("common_abstractions.infer.field")
class Field extends String {}
@namespace("common_abstractions.infer.name")
class Name extends String {}
@namespace("common_abstractions.infer.metrics")
class Metrics extends String {}
@namespace("common_abstractions.infer.metrics")
class IndexMetrics extends String {}
@namespace("common_abstractions.infer.task_id")
class TaskId extends String {}
@namespace("common_abstractions.infer.id")
class Id extends String {}
@namespace("common_abstractions.infer.join_field_routing")
class Routing extends String {}
@namespace("query_dsl.geo")
class GeoLocation {
	lat: double;
	lon: double;
}
@namespace("common_abstractions.infer.relation_name")
class RelationName extends String {}
@namespace("common_options.date_math")
class DateMathExpression extends String {}
@namespace("common_options.date_math")
class DateMathTime {
	factor: integer;
	interval: DateMathTimeUnit;
}
@namespace("common_abstractions.infer.property_name")
class PropertyName extends String {}
@namespace("common_abstractions.infer.id")
class Ids {
}
@namespace("common_abstractions.infer.timestamp")
class Timestamp {
}
@namespace("common_abstractions.infer.long_id")
class LongId {
}
@namespace("x_pack.sql.query_sql")
class SqlValue extends LazyDocument {
}
@namespace("x_pack.watcher.execution")
class HttpInputRequestResult extends HttpInputRequest {
}
@namespace("analysis.analyzers")
class CustomAnalyzer extends AnalyzerBase {
	char_filter: string[];
	filter: string[];
	position_offset_gap: integer;
	tokenizer: string;
}
@namespace("analysis.analyzers")
class FingerprintAnalyzer extends AnalyzerBase {
	max_output_size: integer;
	preserve_original: boolean;
	separator: string;
	stopwords: StopWords;
	stopwords_path: string;
}
@namespace("analysis.analyzers")
class KeywordAnalyzer extends AnalyzerBase {
}
@namespace("analysis.analyzers")
class LanguageAnalyzer extends AnalyzerBase {
	language: Language;
	stem_exclusion: string[];
	stopwords: StopWords;
	stopwords_path: string;
	type: string;
}
@namespace("analysis.analyzers")
class NoriAnalyzer extends AnalyzerBase {
	decompound_mode: NoriDecompoundMode;
	stoptags: string[];
	user_dictionary: string;
}
@namespace("analysis.analyzers")
class PatternAnalyzer extends AnalyzerBase {
	flags: string;
	lowercase: boolean;
	pattern: string;
	stopwords: StopWords;
}
@namespace("analysis.analyzers")
class SimpleAnalyzer extends AnalyzerBase {
}
@namespace("analysis.analyzers")
class SnowballAnalyzer extends AnalyzerBase {
	language: SnowballLanguage;
	stopwords: StopWords;
}
@namespace("analysis.analyzers")
class StandardAnalyzer extends AnalyzerBase {
	max_token_length: integer;
	stopwords: StopWords;
}
@namespace("analysis.analyzers")
class StopAnalyzer extends AnalyzerBase {
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
	flags: string;
	pattern: string;
	replacement: string;
}
@namespace("analysis.plugins.icu")
class IcuAnalyzer extends AnalyzerBase {
	method: IcuNormalizationType;
	mode: IcuNormalizationMode;
}
@namespace("analysis.plugins.icu")
class IcuCollationTokenFilter extends TokenFilterBase {
	alternate: IcuCollationAlternate;
	caseFirst: IcuCollationCaseFirst;
	caseLevel: boolean;
	country: string;
	decomposition: IcuCollationDecomposition;
	hiraganaQuaternaryMode: boolean;
	language: string;
	numeric: boolean;
	strength: IcuCollationStrength;
	variableTop: string;
	variant: string;
}
@namespace("analysis.plugins.icu")
class IcuFoldingTokenFilter extends TokenFilterBase {
	unicode_set_filter: string;
}
@namespace("analysis.plugins.icu")
class IcuNormalizationCharFilter extends CharFilterBase {
	mode: IcuNormalizationMode;
	name: IcuNormalizationType;
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
	normalize_kana: boolean;
	normalize_kanji: boolean;
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
	discard_punctuation: boolean;
	mode: KuromojiTokenizationMode;
	nbest_cost: integer;
	nbest_examples: string;
	user_dictionary: string;
	user_dictionary_rules: string[];
}
@namespace("analysis.plugins.phonetic")
class PhoneticTokenFilter extends TokenFilterBase {
	encoder: PhoneticEncoder;
	languageset: PhoneticLanguage[];
	max_code_len: integer;
	name_type: PhoneticNameType;
	replace: boolean;
	rule_type: PhoneticRuleType;
}
@namespace("analysis.token_filters")
class AsciiFoldingTokenFilter extends TokenFilterBase {
	preserve_original: boolean;
}
@namespace("analysis.token_filters")
class CommonGramsTokenFilter extends TokenFilterBase {
	common_words: string[];
	common_words_path: string;
	ignore_case: boolean;
	query_mode: boolean;
}
@namespace("analysis.token_filters")
class ConditionTokenFilter extends TokenFilterBase {
	script: Script;
	filter: string[];
}
@namespace("analysis.token_filters.delimited_payload")
class DelimitedPayloadTokenFilter extends TokenFilterBase {
	delimiter: string;
	encoding: DelimitedPayloadEncoding;
}
@namespace("analysis.token_filters.edge_n_gram")
class EdgeNGramTokenFilter extends TokenFilterBase {
	max_gram: integer;
	min_gram: integer;
	side: EdgeNGramSide;
}
@namespace("analysis.token_filters")
class ElisionTokenFilter extends TokenFilterBase {
	articles: string[];
	articles_case: boolean;
}
@namespace("analysis.token_filters")
class FingerprintTokenFilter extends TokenFilterBase {
	max_output_size: integer;
	separator: string;
}
@namespace("analysis.token_filters")
class HunspellTokenFilter extends TokenFilterBase {
	dedup: boolean;
	dictionary: string;
	locale: string;
	longest_only: boolean;
}
@namespace("analysis.token_filters")
class KeepTypesTokenFilter extends TokenFilterBase {
	mode: KeepTypesMode;
	types: string[];
}
@namespace("analysis.token_filters")
class KeepWordsTokenFilter extends TokenFilterBase {
	keep_words: string[];
	keep_words_case: boolean;
	keep_words_path: string;
}
@namespace("analysis.token_filters")
class KeywordMarkerTokenFilter extends TokenFilterBase {
	ignore_case: boolean;
	keywords: string[];
	keywords_path: string;
	keywords_pattern: string;
}
@namespace("analysis.token_filters")
class KStemTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class LengthTokenFilter extends TokenFilterBase {
	max: integer;
	min: integer;
}
@namespace("analysis.token_filters")
class LimitTokenCountTokenFilter extends TokenFilterBase {
	consume_all_tokens: boolean;
	max_token_count: integer;
}
@namespace("analysis.token_filters")
class LowercaseTokenFilter extends TokenFilterBase {
	language: string;
}
@namespace("analysis.token_filters")
class MultiplexerTokenFilter extends TokenFilterBase {
	filters: string[];
	preserve_original: boolean;
}
@namespace("analysis.token_filters")
class NGramTokenFilter extends TokenFilterBase {
	max_gram: integer;
	min_gram: integer;
}
@namespace("analysis.token_filters")
class NoriPartOfSpeechTokenFilter extends TokenFilterBase {
	stoptags: string[];
}
@namespace("analysis.token_filters")
class PatternCaptureTokenFilter extends TokenFilterBase {
	patterns: string[];
	preserve_original: boolean;
}
@namespace("analysis.token_filters")
class PatternReplaceTokenFilter extends TokenFilterBase {
	flags: string;
	pattern: string;
	replacement: string;
}
@namespace("analysis.token_filters")
class PorterStemTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class PredicateTokenFilter extends TokenFilterBase {
	script: Script;
}
@namespace("analysis.token_filters")
class RemoveDuplicatesTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters")
class ReverseTokenFilter extends TokenFilterBase {
}
@namespace("analysis.token_filters.shingle")
class ShingleTokenFilter extends TokenFilterBase {
	filler_token: string;
	max_shingle_size: integer;
	min_shingle_size: integer;
	output_unigrams: boolean;
	output_unigrams_if_no_shingles: boolean;
	token_separator: string;
}
@namespace("analysis.token_filters")
class SnowballTokenFilter extends TokenFilterBase {
	language: SnowballLanguage;
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
	ignore_case: boolean;
	remove_trailing: boolean;
	stopwords: StopWords;
	stopwords_path: string;
}
@namespace("analysis.token_filters.synonym")
class SynonymGraphTokenFilter extends TokenFilterBase {
	expand: boolean;
	format: SynonymFormat;
	lenient: boolean;
	synonyms: string[];
	synonyms_path: string;
	tokenizer: string;
}
@namespace("analysis.token_filters.synonym")
class SynonymTokenFilter extends TokenFilterBase {
	expand: boolean;
	format: SynonymFormat;
	lenient: boolean;
	synonyms: string[];
	synonyms_path: string;
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
	adjust_offsets: boolean;
	catenate_all: boolean;
	catenate_numbers: boolean;
	catenate_words: boolean;
	generate_number_parts: boolean;
	generate_word_parts: boolean;
	preserve_original: boolean;
	protected_words: string[];
	protected_words_path : string;
	split_on_case_change: boolean;
	split_on_numerics: boolean;
	stem_english_possessive: boolean;
	type_table: string[];
	type_table_path: string;
}
@namespace("analysis.token_filters.word_delimiter")
class WordDelimiterTokenFilter extends TokenFilterBase {
	catenate_all: boolean;
	catenate_numbers: boolean;
	catenate_words: boolean;
	generate_number_parts: boolean;
	generate_word_parts: boolean;
	preserve_original: boolean;
	protected_words: string[];
	protected_words_path : string;
	split_on_case_change: boolean;
	split_on_numerics: boolean;
	stem_english_possessive: boolean;
	type_table: string[];
	type_table_path: string;
}
@namespace("analysis.tokenizers")
class CharGroupTokenizer extends TokenizerBase {
	tokenize_on_chars: string[];
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
	max_gram: integer;
	min_gram: integer;
	token_chars: TokenChar[];
}
@namespace("analysis.tokenizers.n_gram")
class NGramTokenizer extends TokenizerBase {
	max_gram: integer;
	min_gram: integer;
	token_chars: TokenChar[];
}
@namespace("analysis.tokenizers")
class NoriTokenizer extends TokenizerBase {
	decompound_mode: NoriDecompoundMode;
	user_dictionary: string;
	user_dictionary_rules: string[];
}
@namespace("analysis.tokenizers")
class PathHierarchyTokenizer extends TokenizerBase {
	buffer_size: integer;
	delimiter: string;
	replacement: string;
	reverse: boolean;
	skip: integer;
}
@namespace("analysis.tokenizers")
class PatternTokenizer extends TokenizerBase {
	flags: string;
	group: integer;
	pattern: string;
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
	max_token_length: integer;
}
@namespace("common_options.time_unit")
class Time {
	factor: double;
	interval: TimeUnit;
	milliseconds: double;
	minus_one: Time;
	zero: Time;
}
@namespace("cat")
class CatResponse<TCatRecord> extends ResponseBase implements IResponse {
	records: TCatRecord[];
}
@namespace("cluster.cluster_allocation_explain")
class ClusterAllocationExplainResponse extends ResponseBase implements IResponse {
	allocate_explanation: string;
	allocation_delay: string;
	allocation_delay_in_millis: long;
	can_allocate: Decision;
	can_move_to_other_node: Decision;
	can_rebalance_cluster: Decision;
	can_rebalance_cluster_decisions: AllocationDecision[];
	can_rebalance_to_other_node: Decision;
	can_remain_decisions: AllocationDecision[];
	can_remain_on_current_node: Decision;
	configured_delay: string;
	configured_delay_in_mills: long;
	current_node: CurrentNode;
	current_state: string;
	index: string;
	move_explanation: string;
	node_allocation_decisions: NodeAllocationExplanation[];
	primary: boolean;
	rebalance_explanation: string;
	remaining_delay: string;
	remaining_delay_in_millis: long;
	shard: integer;
	unassigned_info: UnassignedInformation;
}
@namespace("cluster.cluster_health")
class ClusterHealthResponse extends ResponseBase implements IResponse {
	active_primary_shards: integer;
	active_shards: integer;
	active_shards_percent_as_number: double;
	cluster_name: string;
	delayed_unassigned_shards: integer;
	indices: Dictionary<IndexName, IndexHealthStats>;
	initializing_shards: integer;
	number_of_data_nodes: integer;
	number_of_in_flight_fetch: integer;
	number_of_nodes: integer;
	number_of_pending_tasks: integer;
	relocating_shards: integer;
	status: Health;
	task_max_waiting_in_queue_millis: long;
	timed_out: boolean;
	unassigned_shards: integer;
}
@namespace("cluster.cluster_pending_tasks")
class ClusterPendingTasksResponse extends ResponseBase implements IResponse {
	tasks: PendingTask[];
}
@namespace("cluster.cluster_reroute")
class ClusterRerouteResponse extends ResponseBase implements IResponse {
	explanations: ClusterRerouteExplanation[];
	state: string[];
}
@namespace("cluster.cluster_settings.cluster_get_settings")
class ClusterGetSettingsResponse extends ResponseBase implements IResponse {
	persistent: Dictionary<string, any>;
	transient: Dictionary<string, any>;
}
@namespace("cluster.cluster_settings.cluster_put_settings")
class ClusterPutSettingsResponse extends ResponseBase implements IResponse {
	acknowledged: boolean;
	persistent: Dictionary<string, any>;
	transient: Dictionary<string, any>;
}
@namespace("cluster.nodes_hot_threads")
class NodesHotThreadsResponse extends ResponseBase implements IResponse {
	hot_threads: HotThreadInformation[];
}
@namespace("cluster.ping")
class PingResponse extends ResponseBase implements IResponse {
}
@namespace("cluster.root_node_info")
class RootNodeInfoResponse extends ResponseBase implements IResponse {
	name: string;
	cluster_name: string;
	cluster_uuid: string;
	version: ElasticsearchVersionInfo;
	tagline: string;
}
@namespace("cluster.task_management.cancel_tasks")
class CancelTasksResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	node_failures: ErrorCause[];
	nodes: Dictionary<string, TaskExecutingNode>;
}
@namespace("cluster.task_management.get_task")
class GetTaskResponse extends ResponseBase implements IResponse {
	completed: boolean;
	task: TaskInfo;
}
@namespace("cluster.task_management.list_tasks")
class ListTasksResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	node_failures: ErrorCause[];
	nodes: Dictionary<string, TaskExecutingNode>;
}
@namespace("document.multiple.bulk")
class BulkResponse extends ResponseBase implements IResponse {
	errors: boolean;
	is_valid: boolean;
	items: BulkResponseItemBase[];
	items_with_errors: BulkResponseItemBase[];
	took: long;
}
@namespace("query_dsl.geo")
class GeoCoordinate extends GeoLocation {
	z: double;
}
@namespace("document.multiple.delete_by_query")
class DeleteByQueryResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	batches: long;
	deleted: long;
	failures: BulkIndexByScrollFailure[];
	noops: long;
	requests_per_second: float;
	retries: Retries;
	slice_id: integer;
	task: TaskId;
	throttled_millis: long;
	throttled_until_millis: long;
	timed_out: boolean;
	took: long;
	total: long;
	version_conflicts: long;
}
@namespace("document.multiple.multi_get.response")
class MultiGetResponse extends ResponseBase implements IResponse {
	hits: MultiGetHit<any>[];
	is_valid: boolean;
}
@namespace("document.multiple.multi_term_vectors")
class MultiTermVectorsResponse extends ResponseBase implements IResponse {
	docs: TermVectors[];
}
@namespace("document.multiple.reindex_on_server")
class ReindexOnServerResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	batches: long;
	created: long;
	failures: BulkIndexByScrollFailure[];
	noops: long;
	retries: Retries;
	slice_id: integer;
	task: TaskId;
	timed_out: boolean;
	took: Time;
	total: long;
	updated: long;
	version_conflicts: long;
}
@namespace("document.multiple.reindex_rethrottle")
class ReindexRethrottleResponse extends ResponseBase implements IResponse {
	nodes: Dictionary<string, ReindexNode>;
}
@namespace("document.multiple.update_by_query")
class UpdateByQueryResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	batches: long;
	failures: BulkIndexByScrollFailure[];
	noops: long;
	requests_per_second: float;
	retries: Retries;
	task: TaskId;
	timed_out: boolean;
	took: long;
	total: long;
	updated: long;
	version_conflicts: long;
}
@namespace("document.single.term_vectors")
class TermVectorsResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	found: boolean;
	_id: string;
	_index: string;
	term_vectors: Dictionary<Field, TermVector>;
	took: long;
	_type: string;
	_version: long;
}
@namespace("indices.alias_management.delete_alias")
class DeleteAliasResponse extends ResponseBase implements IResponse {
}
@namespace("indices.alias_management.put_alias")
class PutAliasResponse extends ResponseBase implements IResponse {
}
@namespace("indices.analyze")
class AnalyzeResponse extends ResponseBase implements IResponse {
	detail: AnalyzeDetail;
	tokens: AnalyzeToken[];
}
@namespace("indices.index_management.indices_exists")
class ExistsResponse extends ResponseBase implements IResponse {
	exists: boolean;
}
@namespace("indices.monitoring.indices_segments")
class SegmentsResponse extends ResponseBase implements IResponse {
	indices: Dictionary<string, IndexSegment>;
	_shards: ShardStatistics;
}
@namespace("indices.monitoring.indices_shard_stores")
class IndicesShardStoresResponse extends ResponseBase implements IResponse {
	indices: Dictionary<string, IndicesShardStores>;
}
@namespace("indices.monitoring.indices_stats")
class IndicesStatsResponse extends ResponseBase implements IResponse {
	indices: Dictionary<string, IndicesStats>;
	_shards: ShardStatistics;
	_all: IndicesStats;
}
@namespace("ingest.processor")
class GrokProcessorPatternsResponse extends ResponseBase implements IResponse {
	patterns: Dictionary<string, string>;
}
@namespace("ingest.simulate_pipeline")
class SimulatePipelineResponse extends ResponseBase implements IResponse {
	docs: PipelineSimulation[];
}
@namespace("modules.scripting.get_script")
class GetScriptResponse extends ResponseBase implements IResponse {
	script: StoredScript;
}
@namespace("modules.snapshot_and_restore.repositories.cleanup_repository")
class CleanupRepositoryResponse extends ResponseBase implements IResponse {
	results: CleanupRepositoryResults;
}
@namespace("modules.snapshot_and_restore.repositories.get_repository")
class GetRepositoryResponse extends ResponseBase implements IResponse {
	repositories: Dictionary<string, SnapshotRepository>;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
class VerifyRepositoryResponse extends ResponseBase implements IResponse {
	nodes: Dictionary<string, CompactNodeInfo>;
}
@namespace("modules.snapshot_and_restore.restore")
class RestoreResponse extends ResponseBase implements IResponse {
	snapshot: SnapshotRestore;
}
@namespace("modules.snapshot_and_restore.snapshot.get_snapshot")
class GetSnapshotResponse extends ResponseBase implements IResponse {
	snapshots: Snapshot[];
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
class SnapshotStatusResponse extends ResponseBase implements IResponse {
	snapshots: SnapshotStatus[];
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot")
class SnapshotResponse extends ResponseBase implements IResponse {
	accepted: boolean;
	snapshot: Snapshot;
}
@namespace("x_pack.watcher.schedule")
class CronExpression extends ScheduleBase {
}
@namespace("search.count")
class CountResponse extends ResponseBase implements IResponse {
	count: long;
	_shards: ShardStatistics;
}
@namespace("search.field_capabilities")
class FieldCapabilitiesResponse extends ResponseBase implements IResponse {
	fields: Dictionary<Field, Dictionary<string, FieldCapabilities>>;
}
@namespace("search.multi_search")
class MultiSearchResponse extends ResponseBase implements IResponse {
	took: long;
	all_responses: IResponse[];
	is_valid: boolean;
	total_responses: integer;
}
@namespace("search.scroll.clear_scroll")
class ClearScrollResponse extends ResponseBase implements IResponse {
}
@namespace("search.search_shards")
class SearchShardsResponse extends ResponseBase implements IResponse {
	nodes: Dictionary<string, SearchNode>;
	shards: SearchShard[][];
}
@namespace("search.search_template.render_search_template")
class RenderSearchTemplateResponse extends ResponseBase implements IResponse {
	template_output: LazyDocument;
}
@namespace("search.validate")
class ValidateQueryResponse extends ResponseBase implements IResponse {
	explanations: ValidationExplanation[];
	_shards: ShardStatistics;
	valid: boolean;
}
@namespace("x_pack.cross_cluster_replication.auto_follow.get_auto_follow_pattern")
class GetAutoFollowPatternResponse extends ResponseBase implements IResponse {
	patterns: Dictionary<string, AutoFollowPattern>;
}
@namespace("x_pack.cross_cluster_replication.follow.create_follow_index")
class CreateFollowIndexResponse extends ResponseBase implements IResponse {
	follow_index_created: boolean;
	follow_index_shards_acked: boolean;
	index_following_started: boolean;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_index_stats")
class FollowIndexStatsResponse extends ResponseBase implements IResponse {
	indices: FollowIndexStats[];
}
@namespace("x_pack.cross_cluster_replication.follow.follow_info")
class FollowInfoResponse extends ResponseBase implements IResponse {
	follower_indices: FollowerInfo[];
}
@namespace("x_pack.cross_cluster_replication.follow.forget_follower_index")
class ForgetFollowerIndexResponse extends ResponseBase implements IResponse {
	_shards: ShardStatistics;
}
@namespace("x_pack.cross_cluster_replication.stats")
class CcrStatsResponse extends ResponseBase implements IResponse {
	auto_follow_stats: CcrAutoFollowStats;
	follow_stats: CcrFollowStats;
}
@namespace("x_pack.graph.explore")
class GraphExploreResponse extends ResponseBase implements IResponse {
	connections: GraphConnection[];
	failures: ShardFailure[];
	timed_out: boolean;
	took: long;
	vertices: GraphVertex[];
}
@namespace("x_pack.ilm.explain_lifecycle")
class ExplainLifecycleResponse extends ResponseBase implements IResponse {
	indices: Dictionary<string, LifecycleExplain>;
}
@namespace("x_pack.ilm.get_status")
class GetIlmStatusResponse extends ResponseBase implements IResponse {
	operation_mode: LifecycleOperationMode;
}
@namespace("x_pack.ilm.remove_policy")
class RemovePolicyResponse extends ResponseBase implements IResponse {
	failed_indexes: string[];
	has_failures: boolean;
}
@namespace("x_pack.info.x_pack_info")
class XPackInfoResponse extends ResponseBase implements IResponse {
	build: XPackBuildInformation;
	features: XPackFeatures;
	license: MinimalLicenseInformation;
	tagline: string;
}
@namespace("x_pack.info.x_pack_usage")
class XPackUsageResponse extends ResponseBase implements IResponse {
	sql: SqlUsage;
	rollup: XPackUsage;
	data_frame: XPackUsage;
	flattened: XPackUsage;
	data_science: XPackUsage;
	ilm: IlmUsage;
	ccr: CcrUsage;
	watcher: AlertingUsage;
	graph: XPackUsage;
	logstash: XPackUsage;
	ml: MachineLearningUsage;
	monitoring: MonitoringUsage;
	security: SecurityUsage;
	vectors: VectorUsage;
	voting_only: XPackUsage;
}
@namespace("x_pack.license.delete_license")
class DeleteLicenseResponse extends ResponseBase implements IResponse {
}
@namespace("x_pack.license.get_basic_license_status")
class GetBasicLicenseStatusResponse extends ResponseBase implements IResponse {
	eligible_to_start_basic: boolean;
}
@namespace("x_pack.license.get_license")
class GetLicenseResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
	license: LicenseInformation;
}
@namespace("x_pack.license.get_trial_license_status")
class GetTrialLicenseStatusResponse extends ResponseBase implements IResponse {
	eligible_to_start_trial: boolean;
}
@namespace("x_pack.license.post_license")
class PostLicenseResponse extends ResponseBase implements IResponse {
	acknowledge: LicenseAcknowledgement;
	acknowledged: boolean;
	license_status: LicenseStatus;
}
@namespace("x_pack.machine_learning.close_job")
class CloseJobResponse extends ResponseBase implements IResponse {
	closed: boolean;
}
@namespace("x_pack.machine_learning.delete_calendar_job")
class DeleteCalendarJobResponse extends ResponseBase implements IResponse {
	calendar_id: string;
	description: string;
	job_ids: Id[];
}
@namespace("x_pack.machine_learning.delete_expired_data")
class DeleteExpiredDataResponse extends ResponseBase implements IResponse {
	deleted: boolean;
}
@namespace("x_pack.machine_learning.flush_job")
class FlushJobResponse extends ResponseBase implements IResponse {
	flushed: boolean;
}
@namespace("x_pack.machine_learning.get_anomaly_records")
class GetAnomalyRecordsResponse extends ResponseBase implements IResponse {
	count: long;
	records: AnomalyRecord[];
}
@namespace("x_pack.machine_learning.get_buckets")
class GetBucketsResponse extends ResponseBase implements IResponse {
	buckets: Bucket[];
	count: long;
}
@namespace("x_pack.machine_learning.get_calendar_events")
class GetCalendarEventsResponse extends ResponseBase implements IResponse {
	count: integer;
	events: ScheduledEvent[];
}
@namespace("x_pack.machine_learning.get_calendars")
class GetCalendarsResponse extends ResponseBase implements IResponse {
	count: long;
	calendars: Calendar[];
}
@namespace("x_pack.machine_learning.get_categories")
class GetCategoriesResponse extends ResponseBase implements IResponse {
	categories: CategoryDefinition[];
	count: long;
}
@namespace("x_pack.machine_learning.get_datafeed_stats")
class GetDatafeedStatsResponse extends ResponseBase implements IResponse {
	count: long;
	datafeeds: DatafeedStats[];
}
@namespace("x_pack.machine_learning.get_datafeeds")
class GetDatafeedsResponse extends ResponseBase implements IResponse {
	count: long;
	datafeeds: DatafeedConfig[];
}
@namespace("x_pack.machine_learning.get_filters")
class GetFiltersResponse extends ResponseBase implements IResponse {
	count: long;
	filters: Filter[];
}
@namespace("x_pack.machine_learning.get_influencers")
class GetInfluencersResponse extends ResponseBase implements IResponse {
	count: long;
	influencers: BucketInfluencer[];
}
@namespace("x_pack.machine_learning.get_job_stats")
class GetJobStatsResponse extends ResponseBase implements IResponse {
	count: long;
	jobs: JobStats[];
}
@namespace("x_pack.machine_learning.get_jobs")
class GetJobsResponse extends ResponseBase implements IResponse {
	count: long;
	jobs: Job[];
}
@namespace("x_pack.machine_learning.get_model_snapshots")
class GetModelSnapshotsResponse extends ResponseBase implements IResponse {
	count: long;
	model_snapshots: ModelSnapshot[];
}
@namespace("x_pack.machine_learning.get_overall_buckets")
class GetOverallBucketsResponse extends ResponseBase implements IResponse {
	count: long;
	overall_buckets: OverallBucket[];
}
@namespace("x_pack.machine_learning.machine_learning_info")
class MachineLearningInfoResponse extends ResponseBase implements IResponse {
	defaults: Defaults;
	limits: Limits;
	upgrade_mode: boolean;
}
@namespace("x_pack.machine_learning.open_job")
class OpenJobResponse extends ResponseBase implements IResponse {
	opened: boolean;
}
@namespace("x_pack.machine_learning.post_calendar_events")
class PostCalendarEventsResponse extends ResponseBase implements IResponse {
	events: ScheduledEvent[];
}
@namespace("x_pack.machine_learning.post_job_data")
class PostJobDataResponse extends ResponseBase implements IResponse {
	bucket_count: long;
	earliest_record_timestamp: Date;
	empty_bucket_count: long;
	input_bytes: long;
	input_field_count: long;
	input_record_count: long;
	invalid_date_count: long;
	job_id: string;
	last_data_time: Date;
	latest_record_timestamp: Date;
	missing_field_count: long;
	out_of_order_timestamp_count: long;
	processed_field_count: long;
	processed_record_count: long;
	sparse_bucket_count: long;
}
@namespace("x_pack.machine_learning.put_calendar_job")
class PutCalendarJobResponse extends ResponseBase implements IResponse {
	calendar_id: string;
	description: string;
	job_ids: string[];
}
@namespace("x_pack.machine_learning.put_calendar")
class PutCalendarResponse extends ResponseBase implements IResponse {
	calendar_id: string;
	description: string;
	job_ids: string[];
}
@namespace("x_pack.machine_learning.put_datafeed")
class PutDatafeedResponse extends ResponseBase implements IResponse {
	aggregations: Dictionary<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	datafeed_id: string;
	frequency: Time;
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Dictionary<string, ScriptField>;
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.put_filter")
class PutFilterResponse extends ResponseBase implements IResponse {
	description: string;
	filter_id: string;
	items: string[];
}
@namespace("x_pack.machine_learning.put_job")
class PutJobResponse extends ResponseBase implements IResponse {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	background_persist_interval: Time;
	create_time: Date;
	data_description: DataDescription;
	description: string;
	job_id: string;
	job_type: string;
	model_plot: ModelPlotConfig;
	model_snapshot_id: string;
	model_snapshot_retention_days: long;
	renormalization_window_days: long;
	results_index_name: string;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.revert_model_snapshot")
class RevertModelSnapshotResponse extends ResponseBase implements IResponse {
	model: ModelSnapshot;
}
@namespace("x_pack.machine_learning.start_datafeed")
class StartDatafeedResponse extends ResponseBase implements IResponse {
	started: boolean;
}
@namespace("x_pack.machine_learning.stop_datafeed")
class StopDatafeedResponse extends ResponseBase implements IResponse {
	stopped: boolean;
}
@namespace("x_pack.machine_learning.update_data_feed")
class UpdateDatafeedResponse extends ResponseBase implements IResponse {
	aggregations: Dictionary<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	datafeed_id: string;
	frequency: Time;
	indices: Indices;
	job_id: string;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Dictionary<string, ScriptField>;
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.update_filter")
class UpdateFilterResponse extends ResponseBase implements IResponse {
	description: string;
	filter_id: string;
	items: string[];
}
@namespace("x_pack.machine_learning.update_job")
class UpdateJobResponse extends ResponseBase implements IResponse {
}
@namespace("x_pack.migration.deprecation_info")
class DeprecationInfoResponse extends ResponseBase implements IResponse {
	cluster_settings: DeprecationInfo[];
	index_settings: Dictionary<string, DeprecationInfo[]>;
	node_settings: DeprecationInfo[];
}
@namespace("x_pack.roll_up.get_rollup_job")
class GetRollupJobResponse extends ResponseBase implements IResponse {
	jobs: RollupJobInformation[];
}
@namespace("x_pack.roll_up.start_rollup_job")
class StartRollupJobResponse extends ResponseBase implements IResponse {
	started: boolean;
}
@namespace("x_pack.roll_up.stop_rollup_job")
class StopRollupJobResponse extends ResponseBase implements IResponse {
	stopped: boolean;
}
@namespace("x_pack.security.api_key.create_api_key")
class CreateApiKeyResponse extends ResponseBase implements IResponse {
	id: string;
	name: string;
	expiration: Date;
	api_key: string;
}
@namespace("x_pack.security.api_key.get_api_key")
class GetApiKeyResponse extends ResponseBase implements IResponse {
	api_keys: ApiKeys[];
}
@namespace("x_pack.security.api_key.invalidate_api_key")
class InvalidateApiKeyResponse extends ResponseBase implements IResponse {
	error_count: integer;
	error_details: ErrorCause[];
	invalidated_api_keys: string[];
	previously_invalidated_api_keys: string[];
}
@namespace("x_pack.security.authenticate")
class AuthenticateResponse extends ResponseBase implements IResponse {
	email: string;
	full_name: string;
	metadata: Dictionary<string, any>;
	roles: string[];
	username: string;
	authentication_realm: RealmInfo;
	lookup_realm: RealmInfo;
}
@namespace("x_pack.security.clear_cached_realms")
class ClearCachedRealmsResponse extends ResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, SecurityNode>;
}
@namespace("x_pack.security.privileges.get_user_privileges")
class GetUserPrivilegesResponse extends ResponseBase implements IResponse {
	applications: ApplicationResourcePrivileges[];
	cluster: string[];
	global: GlobalPrivileges[];
	indices: UserIndicesPrivileges[];
	run_as: string[];
}
@namespace("x_pack.security.privileges.has_privileges")
class HasPrivilegesResponse extends ResponseBase implements IResponse {
	application: Dictionary<string, ResourcePrivileges[]>;
	cluster: Dictionary<string, boolean>;
	has_all_requested: boolean;
	index: ResourcePrivileges[];
	username: string;
}
@namespace("x_pack.security.role_mapping.delete_role_mapping")
class DeleteRoleMappingResponse extends ResponseBase implements IResponse {
	found: boolean;
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
class PutRoleMappingResponse extends ResponseBase implements IResponse {
	created: boolean;
	role_mapping: PutRoleMappingStatus;
}
@namespace("x_pack.security.role.clear_cached_roles")
class ClearCachedRolesResponse extends ResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, SecurityNode>;
}
@namespace("x_pack.security.role.delete_role")
class DeleteRoleResponse extends ResponseBase implements IResponse {
	found: boolean;
}
@namespace("x_pack.security.role.put_role")
class PutRoleResponse extends ResponseBase implements IResponse {
	role: PutRoleStatus;
}
@namespace("x_pack.security.user.change_password")
class ChangePasswordResponse extends ResponseBase implements IResponse {
}
@namespace("x_pack.security.user.delete_user")
class DeleteUserResponse extends ResponseBase implements IResponse {
	found: boolean;
}
@namespace("x_pack.security.user.disable_user")
class DisableUserResponse extends ResponseBase implements IResponse {
}
@namespace("x_pack.security.user.enable_user")
class EnableUserResponse extends ResponseBase implements IResponse {
}
@namespace("x_pack.security.user.get_user_access_token")
class GetUserAccessTokenResponse extends ResponseBase implements IResponse {
	access_token: string;
	expires_in: long;
	scope: string;
	type: string;
}
@namespace("x_pack.security.user.invalidate_user_access_token")
class InvalidateUserAccessTokenResponse extends ResponseBase implements IResponse {
	invalidated_tokens: long;
	previously_invalidated_tokens: long;
	error_count: long;
	error_details: ErrorCause[];
}
@namespace("x_pack.security.user.put_user")
class PutUserResponse extends ResponseBase implements IResponse {
	created: boolean;
}
@namespace("x_pack.slm.execute_lifecycle")
class ExecuteSnapshotLifecycleResponse extends ResponseBase implements IResponse {
	snapshot_name: string;
}
@namespace("x_pack.sql.clear_sql_cursor")
class ClearSqlCursorResponse extends ResponseBase implements IResponse {
	succeeded: boolean;
}
@namespace("x_pack.sql.query_sql")
class QuerySqlResponse extends ResponseBase implements IResponse {
	columns: SqlColumn[];
	cursor: string;
	rows: SqlValue[][];
	values: SqlValue[][];
}
@namespace("x_pack.sql.translate_sql")
class TranslateSqlResponse extends ResponseBase implements IResponse {
	result: SearchRequest;
}
@namespace("x_pack.ssl.get_certificates")
class GetCertificatesResponse extends ResponseBase implements IResponse {
	certificates: ClusterCertificateInformation[];
}
@namespace("x_pack.watcher.acknowledge_watch")
class AcknowledgeWatchResponse extends ResponseBase implements IResponse {
	status: WatchStatus;
}
@namespace("x_pack.watcher.activate_watch")
class ActivateWatchResponse extends ResponseBase implements IResponse {
	status: ActivationStatus;
}
@namespace("x_pack.watcher.deactivate_watch")
class DeactivateWatchResponse extends ResponseBase implements IResponse {
	status: ActivationStatus;
}
@namespace("x_pack.watcher.delete_watch")
class DeleteWatchResponse extends ResponseBase implements IResponse {
	found: boolean;
	_id: string;
	_version: integer;
}
@namespace("x_pack.watcher.execute_watch")
class ExecuteWatchResponse extends ResponseBase implements IResponse {
	_id: string;
	watch_record: WatchRecord;
}
@namespace("x_pack.watcher.get_watch")
class GetWatchResponse extends ResponseBase implements IResponse {
	found: boolean;
	_id: string;
	status: WatchStatus;
	watch: Watch;
}
@namespace("x_pack.watcher.put_watch")
class PutWatchResponse extends ResponseBase implements IResponse {
	created: boolean;
	_id: string;
	_version: integer;
	_seq_no: long;
	_primary_term: long;
}
@namespace("x_pack.watcher.watcher_stats")
class WatcherStatsResponse extends ResponseBase implements IResponse {
	cluster_name: string;
	manually_stopped: boolean;
	stats: WatcherNodeStats[];
}
@namespace("cluster.cluster_stats")
class ClusterStatsResponse extends NodesResponseBase implements IResponse {
	cluster_name: string;
	cluster_uuid: string;
	indices: ClusterIndicesStats;
	nodes: ClusterNodesStats;
	status: ClusterStatus;
	timestamp: long;
}
@namespace("cluster.nodes_info")
class NodesInfoResponse extends NodesResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, NodeInfo>;
}
@namespace("cluster.nodes_stats")
class NodesStatsResponse extends NodesResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, NodeStats>;
}
@namespace("cluster.nodes_usage")
class NodesUsageResponse extends NodesResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, NodeUsageInformation>;
}
@namespace("cluster.reload_secure_settings")
class ReloadSecureSettingsResponse extends NodesResponseBase implements IResponse {
	cluster_name: string;
	nodes: Dictionary<string, NodeStats>;
}
@namespace("document.single.create")
class CreateResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
}
@namespace("document.single.delete")
class DeleteResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
}
@namespace("document.single.get")
class GetResponse<TDocument> extends ResponseBase {
	fields: Dictionary<string, LazyDocument>;
	found: boolean;
	_id: string;
	_index: string;
	_primary_term: long;
	_routing: string;
	_seq_no: long;
	_source: TDocument;
	_type: string;
	_version: long;
}
@namespace("document.single.index")
class IndexResponse extends ResponseBase implements IResponse {
	is_valid: boolean;
}
@namespace("document.single.source")
class SourceResponse<TDocument> extends ResponseBase {
	body: TDocument;
}
@namespace("indices.alias_management.alias")
class BulkAliasResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("indices.index_management.clone_index")
class CloneIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
	index: string;
}
@namespace("indices.index_management.create_index")
class CreateIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
	index: string;
}
@namespace("indices.index_management.freeze_index")
class FreezeIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.open_close_index.close_index")
class CloseIndexResponse extends AcknowledgedResponseBase implements IResponse {
	indices: Dictionary<string, CloseIndexResult>;
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.open_close_index.open_index")
class OpenIndexResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("indices.index_management.rollover_index")
class RolloverIndexResponse extends AcknowledgedResponseBase implements IResponse {
	conditions: Dictionary<string, boolean>;
	dry_run: boolean;
	new_index: string;
	old_index: string;
	rolled_over: boolean;
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.shrink_index")
class ShrinkIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.split_index")
class SplitIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
}
@namespace("indices.index_management.unfreeze_index")
class UnfreezeIndexResponse extends AcknowledgedResponseBase implements IResponse {
	shards_acknowledged: boolean;
}
@namespace("indices.index_settings.index_templates.delete_index_template")
class DeleteIndexTemplateResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("indices.index_settings.index_templates.put_index_template")
class PutIndexTemplateResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("indices.index_settings.update_index_settings")
class UpdateIndexSettingsResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("indices.status_management.clear_cache")
class ClearCacheResponse extends ShardsOperationResponseBase implements IResponse {
}
@namespace("indices.status_management.flush")
class FlushResponse extends ShardsOperationResponseBase implements IResponse {
}
@namespace("indices.status_management.force_merge")
class ForceMergeResponse extends ShardsOperationResponseBase implements IResponse {
}
@namespace("indices.status_management.refresh")
class RefreshResponse extends ShardsOperationResponseBase implements IResponse {
}
@namespace("indices.status_management.synced_flush")
class SyncedFlushResponse extends ShardsOperationResponseBase implements IResponse {
}
@namespace("ingest.delete_pipeline")
class DeletePipelineResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("ingest.put_pipeline")
class PutPipelineResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("modules.scripting.delete_script")
class DeleteScriptResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("modules.scripting.execute_painless_script")
class ExecutePainlessScriptResponse<TResult> extends ResponseBase {
	result: TResult;
}
@namespace("modules.scripting.put_script")
class PutScriptResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("modules.snapshot_and_restore.repositories.create_repository")
class CreateRepositoryResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("modules.snapshot_and_restore.repositories.delete_repository")
class DeleteRepositoryResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("modules.snapshot_and_restore.snapshot.delete_snapshot")
class DeleteSnapshotResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.watcher.schedule")
class Interval extends ScheduleBase {
	factor: long;
	unit: IntervalUnit;
}
@namespace("search.explain")
class ExplainResponse<TDocument> extends ResponseBase {
	explanation: ExplanationDetail;
	get: InlineGet<TDocument>;
	matched: boolean;
}
@namespace("search.search")
class SearchResponse<TDocument> extends ResponseBase {
	aggregations: Dictionary<string, Aggregate>;
	_clusters: ClusterStatistics;
	documents: TDocument[];
	fields: Dictionary<string, LazyDocument>;
	hits: Hit<TDocument>[];
	hits: HitsMetadata<TDocument>;
	max_score: double;
	num_reduce_phases: long;
	profile: Profile;
	_scroll_id: string;
	_shards: ShardStatistics;
	suggest: SuggestDictionary<TDocument>;
	terminated_early: boolean;
	timed_out: boolean;
	took: long;
	total: long;
}
@namespace("x_pack.cross_cluster_replication.auto_follow.create_auto_follow_pattern")
class CreateAutoFollowPatternResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.cross_cluster_replication.auto_follow.delete_auto_follow_pattern")
class DeleteAutoFollowPatternResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.cross_cluster_replication.follow.pause_follow_index")
class PauseFollowIndexResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.cross_cluster_replication.follow.resume_follow_index")
class ResumeFollowIndexResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.cross_cluster_replication.follow.unfollow_index")
class UnfollowIndexResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.delete_lifecycle")
class DeleteLifecycleResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.move_to_step")
class MoveToStepResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.put_lifecycle")
class PutLifecycleResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.retry")
class RetryIlmResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.start")
class StartIlmResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.ilm.stop")
class StopIlmResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.license.start_basic_license")
class StartBasicLicenseResponse extends AcknowledgedResponseBase implements IResponse {
	acknowledge: Dictionary<string, string[]>;
	basic_was_started: boolean;
	error_message: string;
}
@namespace("x_pack.license.start_trial_license")
class StartTrialLicenseResponse extends AcknowledgedResponseBase implements IResponse {
	error_message: string;
	trial_was_started: boolean;
}
@namespace("x_pack.machine_learning.delete_calendar_event")
class DeleteCalendarEventResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_calendar")
class DeleteCalendarResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_datafeed")
class DeleteDatafeedResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_filter")
class DeleteFilterResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_forecast")
class DeleteForecastResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_job")
class DeleteJobResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.delete_model_snapshot")
class DeleteModelSnapshotResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.forecast_job")
class ForecastJobResponse extends AcknowledgedResponseBase implements IResponse {
	forecast_id: string;
}
@namespace("x_pack.machine_learning.preview_datafeed")
class PreviewDatafeedResponse<TDocument> extends ResponseBase {
	data: TDocument[];
}
@namespace("x_pack.machine_learning.update_model_snapshot")
class UpdateModelSnapshotResponse extends AcknowledgedResponseBase implements IResponse {
	model: ModelSnapshot;
}
@namespace("x_pack.machine_learning.validate_detector")
class ValidateDetectorResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.machine_learning.validate_job")
class ValidateJobResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.roll_up.create_rollup_job")
class CreateRollupJobResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.roll_up.delete_rollup_job")
class DeleteRollupJobResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.slm.delete_lifecycle")
class DeleteSnapshotLifecycleResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.slm.put_lifecycle")
class PutSnapshotLifecycleResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.watcher.restart_watcher")
class RestartWatcherResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.watcher.start_watcher")
class StartWatcherResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("x_pack.watcher.stop_watcher")
class StopWatcherResponse extends AcknowledgedResponseBase implements IResponse {
}
@namespace("analysis.token_filters.compound_word")
class DictionaryDecompounderTokenFilter extends CompoundWordTokenFilterBase {
}
@namespace("analysis.token_filters.compound_word")
class HyphenationDecompounderTokenFilter extends CompoundWordTokenFilterBase {
}
@namespace("cat.cat_aliases")
@rest_spec_name("cat.aliases")
class CatAliasesRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_allocation")
@rest_spec_name("cat.allocation")
class CatAllocationRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_count")
@rest_spec_name("cat.count")
class CatCountRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_fielddata")
@rest_spec_name("cat.fielddata")
class CatFielddataRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_health")
@rest_spec_name("cat.health")
class CatHealthRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	include_timestamp: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_help")
@rest_spec_name("cat.help")
class CatHelpRequest extends RequestBase {
	@request_parameter()
	help: boolean;
	@request_parameter()
	sort_by_columns: string[];
}
@namespace("cat.cat_indices")
@rest_spec_name("cat.indices")
class CatIndicesRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	health: Health;
	@request_parameter()
	help: boolean;
	@request_parameter()
	include_unloaded_segments: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	pri: boolean;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_master")
@rest_spec_name("cat.master")
class CatMasterRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_node_attributes")
@rest_spec_name("cat.nodeattrs")
class CatNodeAttributesRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_nodes")
@rest_spec_name("cat.nodes")
class CatNodesRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	full_id: boolean;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_pending_tasks")
@rest_spec_name("cat.pending_tasks")
class CatPendingTasksRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_plugins")
@rest_spec_name("cat.plugins")
class CatPluginsRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_recovery")
@rest_spec_name("cat.recovery")
class CatRecoveryRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_repositories")
@rest_spec_name("cat.repositories")
class CatRepositoriesRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_segments")
@rest_spec_name("cat.segments")
class CatSegmentsRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
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
@rest_spec_name("cat.shards")
class CatShardsRequest extends RequestBase {
	@request_parameter()
	bytes: Bytes;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_snapshots")
@rest_spec_name("cat.snapshots")
class CatSnapshotsRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_tasks")
@rest_spec_name("cat.tasks")
class CatTasksRequest extends RequestBase {
	@request_parameter()
	actions: string[];
	@request_parameter()
	detailed: boolean;
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	node_id: string[];
	@request_parameter()
	parent_task: long;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_templates")
@rest_spec_name("cat.templates")
class CatTemplatesRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cat.cat_thread_pool")
@rest_spec_name("cat.thread_pool")
class CatThreadPoolRequest extends RequestBase {
	@request_parameter()
	format: string;
	@request_parameter()
	headers: string[];
	@request_parameter()
	help: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	size: Size;
	@request_parameter()
	sort_by_columns: string[];
	@request_parameter()
	verbose: boolean;
}
@namespace("cluster.cluster_allocation_explain")
@rest_spec_name("cluster.allocation_explain")
class ClusterAllocationExplainRequest extends RequestBase {
	index: IndexName;
	primary: boolean;
	shard: integer;
	@request_parameter()
	include_disk_info: boolean;
	@request_parameter()
	include_yes_decisions: boolean;
}
@namespace("cluster.cluster_health")
@rest_spec_name("cluster.health")
class ClusterHealthRequest extends RequestBase {
	@request_parameter()
	expand_wildcards: ExpandWildcards;
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
	wait_for_events: WaitForEvents;
	@request_parameter()
	wait_for_no_initializing_shards: boolean;
	@request_parameter()
	wait_for_no_relocating_shards: boolean;
	@request_parameter()
	wait_for_nodes: string;
	@request_parameter()
	wait_for_status: WaitForStatus;
}
@namespace("cluster.cluster_pending_tasks")
@rest_spec_name("cluster.pending_tasks")
class ClusterPendingTasksRequest extends RequestBase {
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("cluster.cluster_reroute")
@rest_spec_name("cluster.reroute")
class ClusterRerouteRequest extends RequestBase {
	commands: ClusterRerouteCommand[];
	@request_parameter()
	dry_run: boolean;
	@request_parameter()
	explain: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	metric: string[];
	@request_parameter()
	retry_failed: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.cluster_settings.cluster_get_settings")
@rest_spec_name("cluster.get_settings")
class ClusterGetSettingsRequest extends RequestBase {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.cluster_settings.cluster_put_settings")
@rest_spec_name("cluster.put_settings")
class ClusterPutSettingsRequest extends RequestBase {
	persistent: Dictionary<string, any>;
	transient: Dictionary<string, any>;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.cluster_state")
@rest_spec_name("cluster.state")
class ClusterStateRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_metadata_version: long;
	@request_parameter()
	wait_for_timeout: Time;
}
@namespace("cluster.cluster_state")
class ClusterStateResponse extends ResponseBase {
	state: string[];
	cluster_name: string;
	cluster_uuid: string;
	master_node: string;
	state_uuid: string;
	version: long;
}
@namespace("cluster.cluster_stats")
@rest_spec_name("cluster.stats")
class ClusterStatsRequest extends RequestBase {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_hot_threads")
@rest_spec_name("nodes.hot_threads")
class NodesHotThreadsRequest extends RequestBase {
	@request_parameter()
	ignore_idle_threads: boolean;
	@request_parameter()
	interval: Time;
	@request_parameter()
	snapshots: long;
	@request_parameter()
	thread_type: ThreadType;
	@request_parameter()
	threads: long;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_info")
@rest_spec_name("nodes.info")
class NodesInfoRequest extends RequestBase {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.nodes_stats")
@rest_spec_name("nodes.stats")
class NodesStatsRequest extends RequestBase {
	@request_parameter()
	completion_fields: Field[];
	@request_parameter()
	fielddata_fields: Field[];
	@request_parameter()
	fields: Field[];
	@request_parameter()
	groups: boolean;
	@request_parameter()
	include_segment_file_sizes: boolean;
	@request_parameter()
	level: Level;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	types: string[];
}
@namespace("cluster.nodes_usage")
@rest_spec_name("nodes.usage")
class NodesUsageRequest extends RequestBase {
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.ping")
@rest_spec_name("ping")
class PingRequest extends RequestBase {
}
@namespace("cluster.reload_secure_settings")
@rest_spec_name("nodes.reload_secure_settings")
class ReloadSecureSettingsRequest extends RequestBase {
	@request_parameter()
	timeout: Time;
}
@namespace("cluster.remote_info")
@rest_spec_name("cluster.remote_info")
class RemoteInfoRequest extends RequestBase {
}
@namespace("cluster.remote_info")
class RemoteInfoResponse extends DictionaryResponseBase<string, RemoteInfo> {
	remotes: Dictionary<string, RemoteInfo>;
}
@namespace("cluster.root_node_info")
@rest_spec_name("info")
class RootNodeInfoRequest extends RequestBase {
}
@namespace("cluster.task_management.cancel_tasks")
@rest_spec_name("tasks.cancel")
class CancelTasksRequest extends RequestBase {
	@request_parameter()
	actions: string[];
	@request_parameter()
	nodes: string[];
	@request_parameter()
	parent_task_id: string;
}
@namespace("cluster.task_management.get_task")
@rest_spec_name("tasks.get")
class GetTaskRequest extends RequestBase {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("cluster.task_management.list_tasks")
@rest_spec_name("tasks.list")
class ListTasksRequest extends RequestBase {
	@request_parameter()
	actions: string[];
	@request_parameter()
	detailed: boolean;
	@request_parameter()
	group_by: GroupBy;
	@request_parameter()
	nodes: string[];
	@request_parameter()
	parent_task_id: string;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("document.multiple.bulk")
@rest_spec_name("bulk")
class BulkRequest extends RequestBase {
	operations: BulkOperation[];
	@request_parameter()
	pipeline: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	timeout: Time;
	@request_parameter()
	type_query_string: string;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("document.multiple.delete_by_query_rethrottle")
@rest_spec_name("delete_by_query_rethrottle")
class DeleteByQueryRethrottleRequest extends RequestBase {
	@request_parameter()
	requests_per_second: long;
}
@namespace("document.multiple.delete_by_query")
@rest_spec_name("delete_by_query")
class DeleteByQueryRequest extends RequestBase {
	query: QueryContainer;
	slice: SlicedScroll;
	max_docs: long;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	conflicts: Conflicts;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	from: long;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	scroll_size: long;
	@request_parameter()
	search_timeout: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	size: long;
	@request_parameter()
	slices: long;
	@request_parameter()
	sort: string[];
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	stats: string[];
	@request_parameter()
	terminate_after: long;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: boolean;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("document.multiple.multi_get.request")
@rest_spec_name("mget")
class MultiGetRequest extends RequestBase {
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
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
}
@namespace("document.multiple.multi_term_vectors")
@rest_spec_name("mtermvectors")
class MultiTermVectorsRequest extends RequestBase {
	docs: MultiTermVectorOperation[];
	ids: Id[];
	@request_parameter()
	field_statistics: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	offsets: boolean;
	@request_parameter()
	payloads: boolean;
	@request_parameter()
	positions: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	term_statistics: boolean;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.multiple.reindex_on_server")
@rest_spec_name("reindex")
class ReindexOnServerRequest extends RequestBase {
	conflicts: Conflicts;
	dest: ReindexDestination;
	script: Script;
	size: long;
	max_docs: long;
	source: ReindexSource;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	slices: long;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("document.multiple.reindex_rethrottle")
@rest_spec_name("reindex_rethrottle")
class ReindexRethrottleRequest extends RequestBase {
	@request_parameter()
	requests_per_second: long;
}
@namespace("document.multiple.update_by_query_rethrottle")
@rest_spec_name("update_by_query_rethrottle")
class UpdateByQueryRethrottleRequest extends RequestBase {
	@request_parameter()
	requests_per_second: long;
}
@namespace("document.multiple.update_by_query")
@rest_spec_name("update_by_query")
class UpdateByQueryRequest extends RequestBase {
	slice: SlicedScroll;
	query: QueryContainer;
	script: Script;
	max_docs: long;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	conflicts: Conflicts;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	from: long;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	pipeline: string;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	refresh: boolean;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	requests_per_second: long;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	scroll_size: long;
	@request_parameter()
	search_timeout: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	size: long;
	@request_parameter()
	slices: long;
	@request_parameter()
	sort: string[];
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	stats: string[];
	@request_parameter()
	terminate_after: long;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	version: boolean;
	@request_parameter()
	version_type: boolean;
	@request_parameter()
	wait_for_active_shards: string;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("document.single.delete")
@rest_spec_name("delete")
class DeleteRequest extends RequestBase {
	@request_parameter()
	if_primary_term: long;
	@request_parameter()
	if_sequence_number: long;
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
	wait_for_active_shards: string;
}
@namespace("document.single.exists")
@rest_spec_name("exists")
class DocumentExistsRequest extends RequestBase {
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
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	stored_fields: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.get")
@rest_spec_name("get")
class GetRequest extends RequestBase {
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
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	stored_fields: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.source_exists")
@rest_spec_name("exists_source")
class SourceExistsRequest extends RequestBase {
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
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.source")
@rest_spec_name("get_source")
class SourceRequest extends RequestBase {
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
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.term_vectors")
@rest_spec_name("termvectors")
class TermVectorsRequest<TDocument> extends RequestBase {
	doc: TDocument;
	filter: TermVectorFilter;
	per_field_analyzer: Dictionary<Field, string>;
	@request_parameter()
	field_statistics: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	offsets: boolean;
	@request_parameter()
	payloads: boolean;
	@request_parameter()
	positions: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	realtime: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	term_statistics: boolean;
	@request_parameter()
	version: long;
	@request_parameter()
	version_type: VersionType;
}
@namespace("document.single.update")
@rest_spec_name("update")
class UpdateRequest<TDocument, TPartialDocument> extends RequestBase {
	detect_noop: boolean;
	doc: TPartialDocument;
	doc_as_upsert: boolean;
	script: Script;
	scripted_upsert: boolean;
	_source: Union<boolean, SourceFilter>;
	upsert: TDocument;
	@request_parameter()
	if_primary_term: long;
	@request_parameter()
	if_sequence_number: long;
	@request_parameter()
	lang: string;
	@request_parameter()
	refresh: Refresh;
	@request_parameter()
	retry_on_conflict: long;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("document.single.update")
class UpdateResponse<TDocument> extends ResponseBase {
	is_valid: boolean;
	get: InlineGet<TDocument>;
}
@namespace("indices.alias_management.alias_exists")
@rest_spec_name("indices.exists_alias")
class AliasExistsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	local: boolean;
}
@namespace("indices.alias_management.alias")
@rest_spec_name("indices.update_aliases")
class BulkAliasRequest extends RequestBase {
	actions: AliasAction[];
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.alias_management.delete_alias")
@rest_spec_name("indices.delete_alias")
class DeleteAliasRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.alias_management.get_alias")
@rest_spec_name("indices.get_alias")
class GetAliasRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	local: boolean;
}
@namespace("indices.alias_management.get_alias")
class GetAliasResponse extends DictionaryResponseBase<IndexName, IndexAliases> {
	indices: Dictionary<IndexName, IndexAliases>;
	is_valid: boolean;
}
@namespace("indices.alias_management.put_alias")
@rest_spec_name("indices.put_alias")
class PutAliasRequest extends RequestBase {
	filter: QueryContainer;
	index_routing: Routing;
	is_write_index: boolean;
	routing: Routing;
	search_routing: Routing;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.analyze")
@rest_spec_name("indices.analyze")
class AnalyzeRequest extends RequestBase {
	analyzer: string;
	attributes: string[];
	char_filter: Union<string, ICharFilter>[];
	explain: boolean;
	field: Field;
	filter: Union<string, ITokenFilter>[];
	normalizer: string;
	text: string[];
	tokenizer: Union<string, ITokenizer>;
}
@namespace("indices.index_management.clone_index")
@rest_spec_name("indices.clone")
class CloneIndexRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	settings: Dictionary<string, any>;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.delete_index")
@rest_spec_name("indices.delete")
class DeleteIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.index_management.delete_index")
class DeleteIndexResponse extends IndicesResponseBase implements IResponse {
}
@namespace("indices.index_management.freeze_index")
@rest_spec_name("indices.freeze")
class FreezeIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.get_index")
@rest_spec_name("indices.get")
class GetIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.index_management.get_index")
class GetIndexResponse extends DictionaryResponseBase<IndexName, IndexState> {
	indices: Dictionary<IndexName, IndexState>;
}
@namespace("indices.index_management.indices_exists")
@rest_spec_name("indices.exists")
class IndexExistsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	local: boolean;
}
@namespace("indices.index_management.open_close_index.close_index")
@rest_spec_name("indices.close")
class CloseIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.open_close_index.open_index")
@rest_spec_name("indices.open")
class OpenIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.shrink_index")
@rest_spec_name("indices.shrink")
class ShrinkIndexRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	settings: Dictionary<string, any>;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.split_index")
@rest_spec_name("indices.split")
class SplitIndexRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	settings: Dictionary<string, any>;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.types_exists")
@rest_spec_name("indices.exists_type")
class TypeExistsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	local: boolean;
}
@namespace("indices.index_management.unfreeze_index")
@rest_spec_name("indices.unfreeze")
class UnfreezeIndexRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_settings.get_index_settings")
@rest_spec_name("indices.get_settings")
class GetIndexSettingsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.index_settings.get_index_settings")
class GetIndexSettingsResponse extends DictionaryResponseBase<IndexName, IndexState> {
	indices: Dictionary<IndexName, IndexState>;
}
@namespace("indices.index_settings.index_templates.delete_index_template")
@rest_spec_name("indices.delete_template")
class DeleteIndexTemplateRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.index_settings.index_templates.get_index_template")
@rest_spec_name("indices.get_template")
class GetIndexTemplateRequest extends RequestBase {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.index_settings.index_templates.get_index_template")
class GetIndexTemplateResponse extends DictionaryResponseBase<string, TemplateMapping> {
	template_mappings: Dictionary<string, TemplateMapping>;
}
@namespace("indices.index_settings.index_templates.index_template_exists")
@rest_spec_name("indices.exists_template")
class IndexTemplateExistsRequest extends RequestBase {
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.index_settings.update_index_settings")
@rest_spec_name("indices.put_settings")
class UpdateIndexSettingsRequest extends RequestBase {
	index_settings: Dictionary<string, any>;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	preserve_existing: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.mapping_management.get_field_mapping")
@rest_spec_name("indices.get_field_mapping")
class GetFieldMappingRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_defaults: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	local: boolean;
}
@namespace("indices.mapping_management.get_field_mapping")
class GetFieldMappingResponse extends DictionaryResponseBase<IndexName, TypeFieldMappings> {
	indices: Dictionary<IndexName, TypeFieldMappings>;
	is_valid: boolean;
}
@namespace("indices.mapping_management.get_mapping")
@rest_spec_name("indices.get_mapping")
class GetMappingRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("indices.mapping_management.get_mapping")
class GetMappingResponse extends DictionaryResponseBase<IndexName, IndexMappings> {
	indices: Dictionary<IndexName, IndexMappings>;
}
@namespace("indices.mapping_management.put_mapping")
class PutMappingResponse extends IndicesResponseBase implements IResponse {
}
@namespace("indices.monitoring.indices_recovery")
@rest_spec_name("indices.recovery")
class RecoveryStatusRequest extends RequestBase {
	@request_parameter()
	active_only: boolean;
	@request_parameter()
	detailed: boolean;
}
@namespace("indices.monitoring.indices_recovery")
class RecoveryStatusResponse extends DictionaryResponseBase<IndexName, RecoveryStatus> {
	indices: Dictionary<IndexName, RecoveryStatus>;
}
@namespace("indices.monitoring.indices_segments")
@rest_spec_name("indices.segments")
class SegmentsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	verbose: boolean;
}
@namespace("indices.monitoring.indices_shard_stores")
@rest_spec_name("indices.shard_stores")
class IndicesShardStoresRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	status: string[];
}
@namespace("indices.monitoring.indices_stats")
@rest_spec_name("indices.stats")
class IndicesStatsRequest extends RequestBase {
	@request_parameter()
	completion_fields: Field[];
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	fielddata_fields: Field[];
	@request_parameter()
	fields: Field[];
	@request_parameter()
	forbid_closed_indices: boolean;
	@request_parameter()
	groups: string[];
	@request_parameter()
	include_segment_file_sizes: boolean;
	@request_parameter()
	include_unloaded_segments: boolean;
	@request_parameter()
	level: Level;
}
@namespace("indices.status_management.clear_cache")
@rest_spec_name("indices.clear_cache")
class ClearCacheRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	fielddata: boolean;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	query: boolean;
	@request_parameter()
	request: boolean;
}
@namespace("indices.status_management.flush")
@rest_spec_name("indices.flush")
class FlushRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	force: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	wait_if_ongoing: boolean;
}
@namespace("indices.status_management.force_merge")
@rest_spec_name("indices.forcemerge")
class ForceMergeRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	flush: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	max_num_segments: long;
	@request_parameter()
	only_expunge_deletes: boolean;
}
@namespace("indices.status_management.refresh")
@rest_spec_name("indices.refresh")
class RefreshRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
}
@namespace("indices.status_management.synced_flush")
@rest_spec_name("indices.flush_synced")
class SyncedFlushRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
}
@namespace("ingest.delete_pipeline")
@rest_spec_name("ingest.delete_pipeline")
class DeletePipelineRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("ingest.get_pipeline")
@rest_spec_name("ingest.get_pipeline")
class GetPipelineRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
}
@namespace("ingest.get_pipeline")
class GetPipelineResponse extends DictionaryResponseBase<string, Pipeline> {
	pipelines: Dictionary<string, Pipeline>;
}
@namespace("ingest.processor")
@rest_spec_name("ingest.processor_grok")
class GrokProcessorPatternsRequest extends RequestBase {
}
@namespace("ingest.simulate_pipeline")
@rest_spec_name("ingest.simulate")
class SimulatePipelineRequest extends RequestBase {
	docs: SimulatePipelineDocument[];
	pipeline: Pipeline;
	@request_parameter()
	verbose: boolean;
}
@namespace("mapping.types.complex.flattened")
class FlattenedProperty extends PropertyBase {
	boost: double;
	depth_limit: integer;
	doc_values: boolean;
	eager_global_ordinals: boolean;
	ignore_above: integer;
	index: boolean;
	index_options: IndexOptions;
	null_value: string;
	similarity: string;
	split_queries_on_whitespace: boolean;
}
@namespace("mapping.types.core.join")
class JoinProperty extends PropertyBase {
	relations: Dictionary<RelationName, RelationName[]>;
}
@namespace("mapping.types.core.percolator")
class PercolatorProperty extends PropertyBase {
}
@namespace("mapping.types.core.rank_features")
class RankFeaturesProperty extends PropertyBase {
}
@namespace("mapping.types.core.rank_feature")
class RankFeatureProperty extends PropertyBase {
	positive_score_impact: boolean;
}
@namespace("mapping.types.specialized.field_alias")
class FieldAliasProperty extends PropertyBase {
	path: Field;
}
@namespace("modules.scripting.delete_script")
@rest_spec_name("delete_script")
class DeleteScriptRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.scripting.execute_painless_script")
@rest_spec_name("scripts_painless_execute")
class ExecutePainlessScriptRequest extends RequestBase {
	context: string;
	context_setup: PainlessContextSetup;
	script: InlineScript;
}
@namespace("modules.scripting.get_script")
@rest_spec_name("get_script")
class GetScriptRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.scripting.put_script")
@rest_spec_name("put_script")
class PutScriptRequest extends RequestBase {
	script: StoredScript;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.cleanup_repository")
@rest_spec_name("snapshot.cleanup_repository")
class CleanupRepositoryRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.create_repository")
@rest_spec_name("snapshot.create_repository")
class CreateRepositoryRequest extends RequestBase {
	repository: SnapshotRepository;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	verify: boolean;
}
@namespace("modules.snapshot_and_restore.repositories.delete_repository")
@rest_spec_name("snapshot.delete_repository")
class DeleteRepositoryRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.get_repository")
@rest_spec_name("snapshot.get_repository")
class GetRepositoryRequest extends RequestBase {
	@request_parameter()
	local: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.snapshot_and_restore.repositories.verify_repository")
@rest_spec_name("snapshot.verify_repository")
class VerifyRepositoryRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("modules.snapshot_and_restore.restore")
@rest_spec_name("snapshot.restore")
class RestoreRequest extends RequestBase {
	ignore_index_settings: string[];
	ignore_unavailable: boolean;
	include_aliases: boolean;
	include_global_state: boolean;
	index_settings: UpdateIndexSettingsRequest;
	indices: Indices;
	partial: boolean;
	rename_pattern: string;
	rename_replacement: string;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("modules.snapshot_and_restore.snapshot.delete_snapshot")
@rest_spec_name("snapshot.delete")
class DeleteSnapshotRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.snapshot_and_restore.snapshot.get_snapshot")
@rest_spec_name("snapshot.get")
class GetSnapshotRequest extends RequestBase {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	verbose: boolean;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot_status")
@rest_spec_name("snapshot.status")
class SnapshotStatusRequest extends RequestBase {
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	master_timeout: Time;
}
@namespace("modules.snapshot_and_restore.snapshot.snapshot")
@rest_spec_name("snapshot.create")
class SnapshotRequest extends RequestBase {
	ignore_unavailable: boolean;
	include_global_state: boolean;
	indices: Indices;
	partial: boolean;
	metadata: Dictionary<string, any>;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("x_pack.cross_cluster_replication.auto_follow.delete_auto_follow_pattern")
@rest_spec_name("ccr.delete_auto_follow_pattern")
class DeleteAutoFollowPatternRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.create_follow_index")
@rest_spec_name("ccr.follow")
class CreateFollowIndexRequest extends RequestBase {
	@request_parameter()
	wait_for_active_shards: string;
	remote_cluster: string;
	leader_index: IndexName;
	max_read_request_operation_count: long;
	max_outstanding_read_requests: long;
	max_read_request_size: string;
	max_write_request_operation_count: long;
	max_write_request_size: string;
	max_outstanding_write_requests: long;
	max_write_buffer_count: long;
	max_write_buffer_size: string;
	max_retry_delay: Time;
	read_poll_timeout: Time;
}
@namespace("x_pack.cross_cluster_replication.follow.follow_info")
@rest_spec_name("ccr.follow_info")
class FollowInfoRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.follow_index_stats")
@rest_spec_name("ccr.follow_stats")
class FollowIndexStatsRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.forget_follower_index")
@rest_spec_name("ccr.forget_follower")
class ForgetFollowerIndexRequest extends RequestBase {
	follower_cluster: string;
	follower_index: IndexName;
	follower_index_uuid: string;
	leader_remote_cluster: string;
}
@namespace("x_pack.cross_cluster_replication.auto_follow.get_auto_follow_pattern")
@rest_spec_name("ccr.get_auto_follow_pattern")
class GetAutoFollowPatternRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.pause_follow_index")
@rest_spec_name("ccr.pause_follow")
class PauseFollowIndexRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.resume_follow_index")
@rest_spec_name("ccr.resume_follow")
class ResumeFollowIndexRequest extends RequestBase {
	max_read_request_operation_count: long;
	max_outstanding_read_requests: long;
	max_read_request_size: string;
	max_write_request_operation_count: long;
	max_write_request_size: string;
	max_outstanding_write_requests: long;
	max_write_buffer_count: long;
	max_write_buffer_size: string;
	max_retry_delay: Time;
	read_poll_timeout: Time;
}
@namespace("x_pack.cross_cluster_replication.stats")
@rest_spec_name("ccr.stats")
class CcrStatsRequest extends RequestBase {
}
@namespace("x_pack.cross_cluster_replication.follow.unfollow_index")
@rest_spec_name("ccr.unfollow")
class UnfollowIndexRequest extends RequestBase {
}
@namespace("x_pack.ilm.delete_lifecycle")
@rest_spec_name("ilm.delete_lifecycle")
class DeleteLifecycleRequest extends RequestBase {
}
@namespace("x_pack.ilm.explain_lifecycle")
@rest_spec_name("ilm.explain_lifecycle")
class ExplainLifecycleRequest extends RequestBase {
	@request_parameter()
	only_errors: boolean;
	@request_parameter()
	only_managed: boolean;
}
@namespace("x_pack.ilm.get_lifecycle")
@rest_spec_name("ilm.get_lifecycle")
class GetLifecycleRequest extends RequestBase {
}
@namespace("x_pack.ilm.get_status")
@rest_spec_name("ilm.get_status")
class GetIlmStatusRequest extends RequestBase {
}
@namespace("x_pack.ilm.move_to_step")
@rest_spec_name("ilm.move_to_step")
class MoveToStepRequest extends RequestBase {
	current_step: StepKey;
	next_step: StepKey;
}
@namespace("x_pack.ilm.put_lifecycle")
@rest_spec_name("ilm.put_lifecycle")
class PutLifecycleRequest extends RequestBase {
	policy: Policy;
}
@namespace("x_pack.ilm.remove_policy")
@rest_spec_name("ilm.remove_policy")
class RemovePolicyRequest extends RequestBase {
}
@namespace("x_pack.ilm.retry")
@rest_spec_name("ilm.retry")
class RetryIlmRequest extends RequestBase {
}
@namespace("x_pack.ilm.start")
@rest_spec_name("ilm.start")
class StartIlmRequest extends RequestBase {
}
@namespace("x_pack.ilm.stop")
@rest_spec_name("ilm.stop")
class StopIlmRequest extends RequestBase {
}
@namespace("search.validate")
@rest_spec_name("indices.validate_query")
class ValidateQueryRequest extends RequestBase {
	@request_parameter()
	all_shards: boolean;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	explain: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	rewrite: boolean;
	query: QueryContainer;
}
@namespace("x_pack.license.delete_license")
@rest_spec_name("license.delete")
class DeleteLicenseRequest extends RequestBase {
}
@namespace("x_pack.license.get_license")
@rest_spec_name("license.get")
class GetLicenseRequest extends RequestBase {
	@request_parameter()
	local: boolean;
}
@namespace("x_pack.license.get_basic_license_status")
@rest_spec_name("license.get_basic_status")
class GetBasicLicenseStatusRequest extends RequestBase {
}
@namespace("x_pack.license.get_trial_license_status")
@rest_spec_name("license.get_trial_status")
class GetTrialLicenseStatusRequest extends RequestBase {
}
@namespace("x_pack.license.post_license")
@rest_spec_name("license.post")
class PostLicenseRequest extends RequestBase {
	@request_parameter()
	acknowledge: boolean;
	license: License;
}
@namespace("x_pack.license.start_basic_license")
@rest_spec_name("license.post_start_basic")
class StartBasicLicenseRequest extends RequestBase {
	@request_parameter()
	acknowledge: boolean;
}
@namespace("x_pack.license.start_trial_license")
@rest_spec_name("license.post_start_trial")
class StartTrialLicenseRequest extends RequestBase {
	@request_parameter()
	acknowledge: boolean;
	@request_parameter()
	type_query_string: string;
}
@namespace("x_pack.machine_learning.close_job")
@rest_spec_name("ml.close_job")
class CloseJobRequest extends RequestBase {
	@request_parameter()
	allow_no_jobs: boolean;
	@request_parameter()
	force: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("x_pack.machine_learning.delete_calendar")
@rest_spec_name("ml.delete_calendar")
class DeleteCalendarRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.delete_calendar_event")
@rest_spec_name("ml.delete_calendar_event")
class DeleteCalendarEventRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.delete_calendar_job")
@rest_spec_name("ml.delete_calendar_job")
class DeleteCalendarJobRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.delete_datafeed")
@rest_spec_name("ml.delete_datafeed")
class DeleteDatafeedRequest extends RequestBase {
	@request_parameter()
	force: boolean;
}
@namespace("x_pack.machine_learning.delete_expired_data")
@rest_spec_name("ml.delete_expired_data")
class DeleteExpiredDataRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.delete_filter")
@rest_spec_name("ml.delete_filter")
class DeleteFilterRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.delete_forecast")
@rest_spec_name("ml.delete_forecast")
class DeleteForecastRequest extends RequestBase {
	@request_parameter()
	allow_no_forecasts: boolean;
	@request_parameter()
	timeout: Time;
}
@namespace("x_pack.machine_learning.delete_job")
@rest_spec_name("ml.delete_job")
class DeleteJobRequest extends RequestBase {
	@request_parameter()
	force: boolean;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("x_pack.machine_learning.delete_model_snapshot")
@rest_spec_name("ml.delete_model_snapshot")
class DeleteModelSnapshotRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.flush_job")
@rest_spec_name("ml.flush_job")
class FlushJobRequest extends RequestBase {
	@request_parameter()
	skip_time: string;
	advance_time: Date;
	calc_interim: boolean;
	end: Date;
	start: Date;
}
@namespace("x_pack.machine_learning.forecast_job")
@rest_spec_name("ml.forecast")
class ForecastJobRequest extends RequestBase {
	duration: Time;
	expires_in: Time;
}
@namespace("x_pack.machine_learning.get_buckets")
@rest_spec_name("ml.get_buckets")
class GetBucketsRequest extends RequestBase {
	anomaly_score: double;
	desc: boolean;
	end: Date;
	exclude_interim: boolean;
	expand: boolean;
	page: Page;
	sort: Field;
	start: Date;
}
@namespace("x_pack.machine_learning.get_calendar_events")
@rest_spec_name("ml.get_calendar_events")
class GetCalendarEventsRequest extends RequestBase {
	@request_parameter()
	end: Date;
	@request_parameter()
	job_id: string;
	@request_parameter()
	start: string;
	from: integer;
	size: integer;
}
@namespace("x_pack.machine_learning.get_calendars")
@rest_spec_name("ml.get_calendars")
class GetCalendarsRequest extends RequestBase {
	page: Page;
}
@namespace("x_pack.machine_learning.get_categories")
@rest_spec_name("ml.get_categories")
class GetCategoriesRequest extends RequestBase {
	page: Page;
}
@namespace("x_pack.machine_learning.get_datafeed_stats")
@rest_spec_name("ml.get_datafeed_stats")
class GetDatafeedStatsRequest extends RequestBase {
	@request_parameter()
	allow_no_datafeeds: boolean;
}
@namespace("x_pack.machine_learning.get_datafeeds")
@rest_spec_name("ml.get_datafeeds")
class GetDatafeedsRequest extends RequestBase {
	@request_parameter()
	allow_no_datafeeds: boolean;
}
@namespace("x_pack.machine_learning.get_filters")
@rest_spec_name("ml.get_filters")
class GetFiltersRequest extends RequestBase {
	@request_parameter()
	from: integer;
	@request_parameter()
	size: integer;
}
@namespace("x_pack.machine_learning.get_influencers")
@rest_spec_name("ml.get_influencers")
class GetInfluencersRequest extends RequestBase {
	descending: boolean;
	end: Date;
	exclude_interim: boolean;
	influencer_score: double;
	page: Page;
	sort: Field;
	start: Date;
}
@namespace("x_pack.machine_learning.get_job_stats")
@rest_spec_name("ml.get_job_stats")
class GetJobStatsRequest extends RequestBase {
	@request_parameter()
	allow_no_jobs: boolean;
}
@namespace("x_pack.machine_learning.get_jobs")
@rest_spec_name("ml.get_jobs")
class GetJobsRequest extends RequestBase {
	@request_parameter()
	allow_no_jobs: boolean;
}
@namespace("x_pack.machine_learning.get_model_snapshots")
@rest_spec_name("ml.get_model_snapshots")
class GetModelSnapshotsRequest extends RequestBase {
	desc: boolean;
	end: Date;
	page: Page;
	sort: Field;
	start: Date;
}
@namespace("x_pack.machine_learning.get_overall_buckets")
@rest_spec_name("ml.get_overall_buckets")
class GetOverallBucketsRequest extends RequestBase {
	allow_no_jobs: boolean;
	bucket_span: Time;
	end: Date;
	exclude_interim: boolean;
	overall_score: double;
	start: Date;
	top_n: integer;
}
@namespace("x_pack.machine_learning.get_anomaly_records")
@rest_spec_name("ml.get_records")
class GetAnomalyRecordsRequest extends RequestBase {
	desc: boolean;
	end: Date;
	exclude_interim: boolean;
	page: Page;
	record_score: double;
	sort: Field;
	start: Date;
}
@namespace("x_pack.machine_learning.machine_learning_info")
@rest_spec_name("ml.info")
class MachineLearningInfoRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.open_job")
@rest_spec_name("ml.open_job")
class OpenJobRequest extends RequestBase {
	timeout: Time;
}
@namespace("x_pack.machine_learning.post_calendar_events")
@rest_spec_name("ml.post_calendar_events")
class PostCalendarEventsRequest extends RequestBase {
	events: ScheduledEvent[];
}
@namespace("x_pack.machine_learning.post_job_data")
@rest_spec_name("ml.post_data")
class PostJobDataRequest extends RequestBase {
	@request_parameter()
	reset_end: Date;
	@request_parameter()
	reset_start: Date;
	data: any[];
}
@namespace("x_pack.machine_learning.preview_datafeed")
@rest_spec_name("ml.preview_datafeed")
class PreviewDatafeedRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.put_calendar")
@rest_spec_name("ml.put_calendar")
class PutCalendarRequest extends RequestBase {
	description: string;
}
@namespace("x_pack.machine_learning.put_calendar_job")
@rest_spec_name("ml.put_calendar_job")
class PutCalendarJobRequest extends RequestBase {
}
@namespace("x_pack.machine_learning.put_datafeed")
@rest_spec_name("ml.put_datafeed")
class PutDatafeedRequest extends RequestBase {
	aggregations: Dictionary<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	indices: Indices;
	job_id: Id;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Dictionary<string, ScriptField>;
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.put_filter")
@rest_spec_name("ml.put_filter")
class PutFilterRequest extends RequestBase {
	description: string;
	items: string[];
}
@namespace("x_pack.machine_learning.put_job")
@rest_spec_name("ml.put_job")
class PutJobRequest extends RequestBase {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	data_description: DataDescription;
	description: string;
	model_plot: ModelPlotConfig;
	model_snapshot_retention_days: long;
	results_index_name: IndexName;
}
@namespace("x_pack.machine_learning.revert_model_snapshot")
@rest_spec_name("ml.revert_model_snapshot")
class RevertModelSnapshotRequest extends RequestBase {
	delete_intervening_results: boolean;
}
@namespace("x_pack.machine_learning.start_datafeed")
@rest_spec_name("ml.start_datafeed")
class StartDatafeedRequest extends RequestBase {
	end: Date;
	start: Date;
	timeout: Time;
}
@namespace("x_pack.machine_learning.stop_datafeed")
@rest_spec_name("ml.stop_datafeed")
class StopDatafeedRequest extends RequestBase {
	@request_parameter()
	allow_no_datafeeds: boolean;
	force: boolean;
	timeout: Time;
}
@namespace("x_pack.machine_learning.update_data_feed")
@rest_spec_name("ml.update_datafeed")
class UpdateDatafeedRequest extends RequestBase {
	aggregations: Dictionary<string, AggregationContainer>;
	chunking_config: ChunkingConfig;
	frequency: Time;
	indices: Indices;
	job_id: Id;
	query: QueryContainer;
	query_delay: Time;
	script_fields: Dictionary<string, ScriptField>;
	scroll_size: integer;
}
@namespace("x_pack.machine_learning.update_filter")
@rest_spec_name("ml.update_filter")
class UpdateFilterRequest extends RequestBase {
	add_items: string[];
	description: string;
	remove_items: string[];
}
@namespace("x_pack.machine_learning.update_job")
@rest_spec_name("ml.update_job")
class UpdateJobRequest extends RequestBase {
	analysis_limits: AnalysisMemoryLimit;
	background_persist_interval: Time;
	custom_settings: Dictionary<string, any>;
	description: string;
	model_plot_config: ModelPlotConfigEnabled;
	model_snapshot_retention_days: long;
	renormalization_window_days: long;
	results_retention_days: long;
}
@namespace("x_pack.machine_learning.update_model_snapshot")
@rest_spec_name("ml.update_model_snapshot")
class UpdateModelSnapshotRequest extends RequestBase {
	description: string;
	retain: boolean;
}
@namespace("x_pack.machine_learning.validate_job")
@rest_spec_name("ml.validate")
class ValidateJobRequest extends RequestBase {
	analysis_config: AnalysisConfig;
	analysis_limits: AnalysisLimits;
	data_description: DataDescription;
	description: string;
	model_plot: ModelPlotConfig;
	model_snapshot_retention_days: long;
	results_index_name: IndexName;
}
@namespace("x_pack.machine_learning.validate_detector")
@rest_spec_name("ml.validate_detector")
class ValidateDetectorRequest extends RequestBase {
	detector: Detector;
}
@namespace("x_pack.migration.deprecation_info")
@rest_spec_name("migration.deprecations")
class DeprecationInfoRequest extends RequestBase {
}
@namespace("search.scroll.clear_scroll")
@rest_spec_name("clear_scroll")
class ClearScrollRequest extends RequestBase {
	scroll_id: string[];
}
@namespace("search.count")
@rest_spec_name("count")
class CountRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_throttled: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	min_score: double;
	@request_parameter()
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	terminate_after: long;
	query: QueryContainer;
}
@namespace("search.explain")
@rest_spec_name("explain")
class ExplainRequest extends RequestBase {
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
	preference: string;
	@request_parameter()
	query_on_query_string: string;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	source_enabled: boolean;
	@request_parameter()
	source_excludes: Field[];
	@request_parameter()
	source_includes: Field[];
	query: QueryContainer;
	@request_parameter()
	stored_fields: Field[];
}
@namespace("search.field_capabilities")
@rest_spec_name("field_caps")
class FieldCapabilitiesRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	fields: Field[];
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_unmapped: boolean;
}
@namespace("search.multi_search")
@rest_spec_name("msearch")
class MultiSearchRequest extends RequestBase {
	@request_parameter()
	ccs_minimize_roundtrips: boolean;
	@request_parameter()
	max_concurrent_searches: long;
	@request_parameter()
	max_concurrent_shard_requests: long;
	@request_parameter()
	pre_filter_shard_size: long;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	total_hits_as_integer: boolean;
	@request_parameter()
	typed_keys: boolean;
	operations: Dictionary<string, SearchRequest>;
}
@namespace("search.multi_search_template")
@rest_spec_name("msearch_template")
class MultiSearchTemplateRequest extends RequestBase {
	@request_parameter()
	ccs_minimize_roundtrips: boolean;
	@request_parameter()
	max_concurrent_searches: long;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	total_hits_as_integer: boolean;
	@request_parameter()
	typed_keys: boolean;
	operations: Dictionary<string, SearchTemplateRequest>;
}
@namespace("search.search_template.render_search_template")
@rest_spec_name("render_search_template")
class RenderSearchTemplateRequest extends RequestBase {
	file: string;
	params: Dictionary<string, any>;
	source: string;
}
@namespace("search.search_shards")
@rest_spec_name("search_shards")
class SearchShardsRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	local: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	routing: Routing;
}
@namespace("x_pack.roll_up.delete_rollup_job")
@rest_spec_name("rollup.delete_job")
class DeleteRollupJobRequest extends RequestBase {
}
@namespace("x_pack.roll_up.get_rollup_job")
@rest_spec_name("rollup.get_jobs")
class GetRollupJobRequest extends RequestBase {
}
@namespace("x_pack.roll_up.get_rollup_capabilities")
@rest_spec_name("rollup.get_rollup_caps")
class GetRollupCapabilitiesRequest extends RequestBase {
}
@namespace("x_pack.roll_up.get_rollup_index_capabilities")
@rest_spec_name("rollup.get_rollup_index_caps")
class GetRollupIndexCapabilitiesRequest extends RequestBase {
}
@namespace("x_pack.roll_up.create_rollup_job")
@rest_spec_name("rollup.put_job")
class CreateRollupJobRequest extends RequestBase {
	cron: string;
	groups: RollupGroupings;
	index_pattern: string;
	metrics: RollupFieldMetric[];
	page_size: long;
	rollup_index: IndexName;
}
@namespace("x_pack.roll_up.rollup_search")
@rest_spec_name("rollup.rollup_search")
class RollupSearchRequest extends RequestBase {
	@request_parameter()
	total_hits_as_integer: boolean;
	@request_parameter()
	typed_keys: boolean;
	aggs: Dictionary<string, AggregationContainer>;
	query: QueryContainer;
	size: integer;
}
@namespace("x_pack.roll_up.start_rollup_job")
@rest_spec_name("rollup.start_job")
class StartRollupJobRequest extends RequestBase {
}
@namespace("x_pack.roll_up.stop_rollup_job")
@rest_spec_name("rollup.stop_job")
class StopRollupJobRequest extends RequestBase {
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_completion: boolean;
}
@namespace("x_pack.security.authenticate")
@rest_spec_name("security.authenticate")
class AuthenticateRequest extends RequestBase {
}
@namespace("x_pack.security.user.change_password")
@rest_spec_name("security.change_password")
class ChangePasswordRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	password: string;
}
@namespace("x_pack.security.clear_cached_realms")
@rest_spec_name("security.clear_cached_realms")
class ClearCachedRealmsRequest extends RequestBase {
	@request_parameter()
	usernames: string[];
}
@namespace("x_pack.security.role.clear_cached_roles")
@rest_spec_name("security.clear_cached_roles")
class ClearCachedRolesRequest extends RequestBase {
}
@namespace("x_pack.security.api_key.create_api_key")
@rest_spec_name("security.create_api_key")
class CreateApiKeyRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	expiration: Time;
	name: string;
	role_descriptors: Dictionary<string, ApiKeyRole>;
}
@namespace("x_pack.security.privileges.delete_privileges")
@rest_spec_name("security.delete_privileges")
class DeletePrivilegesRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.role.delete_role")
@rest_spec_name("security.delete_role")
class DeleteRoleRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.role_mapping.delete_role_mapping")
@rest_spec_name("security.delete_role_mapping")
class DeleteRoleMappingRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.delete_user")
@rest_spec_name("security.delete_user")
class DeleteUserRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.disable_user")
@rest_spec_name("security.disable_user")
class DisableUserRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.user.enable_user")
@rest_spec_name("security.enable_user")
class EnableUserRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
}
@namespace("x_pack.security.api_key.get_api_key")
@rest_spec_name("security.get_api_key")
class GetApiKeyRequest extends RequestBase {
	@request_parameter()
	id: string;
	@request_parameter()
	name: string;
	@request_parameter()
	realm_name: string;
	@request_parameter()
	username: string;
}
@namespace("x_pack.security.privileges.get_privileges")
@rest_spec_name("security.get_privileges")
class GetPrivilegesRequest extends RequestBase {
}
@namespace("x_pack.security.role.get_role")
@rest_spec_name("security.get_role")
class GetRoleRequest extends RequestBase {
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
@rest_spec_name("security.get_role_mapping")
class GetRoleMappingRequest extends RequestBase {
}
@namespace("x_pack.security.user.get_user_access_token")
@rest_spec_name("security.get_token")
class GetUserAccessTokenRequest extends RequestBase {
	grant_type: AccessTokenGrantType;
	scope: string;
}
@namespace("x_pack.security.user.get_user")
@rest_spec_name("security.get_user")
class GetUserRequest extends RequestBase {
}
@namespace("x_pack.security.privileges.get_user_privileges")
@rest_spec_name("security.get_user_privileges")
class GetUserPrivilegesRequest extends RequestBase {
}
@namespace("x_pack.security.privileges.has_privileges")
@rest_spec_name("security.has_privileges")
class HasPrivilegesRequest extends RequestBase {
	application: ApplicationPrivilegesCheck[];
	cluster: string[];
	index: IndexPrivilegesCheck[];
}
@namespace("x_pack.security.api_key.invalidate_api_key")
@rest_spec_name("security.invalidate_api_key")
class InvalidateApiKeyRequest extends RequestBase {
	id: string;
	name: string;
	realm_name: string;
	username: string;
}
@namespace("x_pack.security.user.invalidate_user_access_token")
@rest_spec_name("security.invalidate_token")
class InvalidateUserAccessTokenRequest extends RequestBase {
}
@namespace("x_pack.security.role.put_role")
@rest_spec_name("security.put_role")
class PutRoleRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	applications: ApplicationPrivileges[];
	cluster: string[];
	global: Dictionary<string, any>;
	indices: IndicesPrivileges[];
	metadata: Dictionary<string, any>;
	run_as: string[];
}
@namespace("x_pack.security.role_mapping.put_role_mapping")
@rest_spec_name("security.put_role_mapping")
class PutRoleMappingRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	enabled: boolean;
	metadata: Dictionary<string, any>;
	roles: string[];
	rules: RoleMappingRuleBase;
	run_as: string[];
}
@namespace("x_pack.security.user.put_user")
@rest_spec_name("security.put_user")
class PutUserRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	email: string;
	full_name: string;
	metadata: Dictionary<string, any>;
	password: string;
	password_hash: string;
	roles: string[];
}
@namespace("x_pack.ssl.get_certificates")
@rest_spec_name("ssl.certificates")
class GetCertificatesRequest extends RequestBase {
}
@namespace("x_pack.slm.delete_lifecycle")
@rest_spec_name("slm.delete_lifecycle")
class DeleteSnapshotLifecycleRequest extends RequestBase {
}
@namespace("x_pack.slm.execute_lifecycle")
@rest_spec_name("slm.execute_lifecycle")
class ExecuteSnapshotLifecycleRequest extends RequestBase {
}
@namespace("x_pack.slm.get_lifecycle")
@rest_spec_name("slm.get_lifecycle")
class GetSnapshotLifecycleRequest extends RequestBase {
}
@namespace("x_pack.slm.put_lifecycle")
@rest_spec_name("slm.put_lifecycle")
class PutSnapshotLifecycleRequest extends RequestBase {
	config: SnapshotLifecycleConfig;
	name: string;
	repository: string;
	schedule: CronExpression;
}
@namespace("x_pack.sql.clear_sql_cursor")
@rest_spec_name("sql.clear_cursor")
class ClearSqlCursorRequest extends RequestBase {
	cursor: string;
}
@namespace("x_pack.watcher.acknowledge_watch")
@rest_spec_name("watcher.ack_watch")
class AcknowledgeWatchRequest extends RequestBase {
}
@namespace("x_pack.watcher.activate_watch")
@rest_spec_name("watcher.activate_watch")
class ActivateWatchRequest extends RequestBase {
}
@namespace("x_pack.watcher.deactivate_watch")
@rest_spec_name("watcher.deactivate_watch")
class DeactivateWatchRequest extends RequestBase {
}
@namespace("x_pack.watcher.delete_watch")
@rest_spec_name("watcher.delete_watch")
class DeleteWatchRequest extends RequestBase {
}
@namespace("x_pack.watcher.execute_watch")
@rest_spec_name("watcher.execute_watch")
class ExecuteWatchRequest extends RequestBase {
	@request_parameter()
	debug: boolean;
	action_modes: Dictionary<string, ActionExecutionMode>;
	alternative_input: Dictionary<string, any>;
	ignore_condition: boolean;
	record_execution: boolean;
	simulated_actions: SimulatedActions;
	trigger_data: ScheduleTriggerEvent;
	watch: Watch;
}
@namespace("x_pack.watcher.get_watch")
@rest_spec_name("watcher.get_watch")
class GetWatchRequest extends RequestBase {
}
@namespace("x_pack.watcher.put_watch")
@rest_spec_name("watcher.put_watch")
class PutWatchRequest extends RequestBase {
	@request_parameter()
	active: boolean;
	@request_parameter()
	if_primary_term: long;
	@request_parameter()
	if_sequence_number: long;
	@request_parameter()
	version: long;
	actions: Dictionary<string, Action>;
	condition: ConditionContainer;
	input: InputContainer;
	metadata: Dictionary<string, any>;
	throttle_period: string;
	transform: TransformContainer;
	trigger: TriggerContainer;
}
@namespace("x_pack.watcher.start_watcher")
@rest_spec_name("watcher.start")
class StartWatcherRequest extends RequestBase {
}
@namespace("x_pack.watcher.watcher_stats")
@rest_spec_name("watcher.stats")
class WatcherStatsRequest extends RequestBase {
	@request_parameter()
	emit_stacktraces: boolean;
}
@namespace("x_pack.watcher.stop_watcher")
@rest_spec_name("watcher.stop")
class StopWatcherRequest extends RequestBase {
}
@namespace("x_pack.info.x_pack_info")
@rest_spec_name("xpack.info")
class XPackInfoRequest extends RequestBase {
	@request_parameter()
	categories: string[];
}
@namespace("x_pack.info.x_pack_usage")
@rest_spec_name("xpack.usage")
class XPackUsageRequest extends RequestBase {
	@request_parameter()
	master_timeout: Time;
}
@namespace("x_pack.ilm.get_lifecycle")
class GetLifecycleResponse extends DictionaryResponseBase<string, LifecyclePolicy> {
	policies: Dictionary<string, LifecyclePolicy>;
}
@namespace("x_pack.roll_up.get_rollup_capabilities")
class GetRollupCapabilitiesResponse extends DictionaryResponseBase<IndexName, RollupCapabilities> {
	indices: Dictionary<IndexName, RollupCapabilities>;
}
@namespace("x_pack.roll_up.get_rollup_index_capabilities")
class GetRollupIndexCapabilitiesResponse extends DictionaryResponseBase<IndexName, RollupIndexCapabilities> {
	indices: Dictionary<IndexName, RollupIndexCapabilities>;
}
@namespace("x_pack.roll_up.rollup_search")
class RollupSearchResponse<TDocument> extends ResponseBase {
}
@namespace("x_pack.security.privileges.delete_privileges")
class DeletePrivilegesResponse extends DictionaryResponseBase<string, Dictionary<string, FoundUserPrivilege>> {
	applications: Dictionary<string, Dictionary<string, FoundUserPrivilege>>;
}
@namespace("x_pack.security.privileges.get_privileges")
class GetPrivilegesResponse extends DictionaryResponseBase<string, Dictionary<string, PrivilegesActions>> {
	applications: Dictionary<string, Dictionary<string, PrivilegesActions>>;
}
@namespace("x_pack.security.privileges.put_privileges")
class PutPrivilegesResponse extends DictionaryResponseBase<string, Dictionary<string, PutPrivilegesStatus>> {
	applications: Dictionary<string, Dictionary<string, PutPrivilegesStatus>>;
}
@namespace("x_pack.security.role_mapping.get_role_mapping")
class GetRoleMappingResponse extends DictionaryResponseBase<string, XPackRoleMapping> {
	role_mappings: Dictionary<string, XPackRoleMapping>;
}
@namespace("x_pack.security.role.get_role")
class GetRoleResponse extends DictionaryResponseBase<string, XPackRole> {
	roles: Dictionary<string, XPackRole>;
}
@namespace("x_pack.security.user.get_user")
class GetUserResponse extends DictionaryResponseBase<string, XPackUser> {
	users: Dictionary<string, XPackUser>;
}
@namespace("x_pack.slm.get_lifecycle")
class GetSnapshotLifecycleResponse extends DictionaryResponseBase<string, SnapshotLifecyclePolicyMetadata> {
	policies: Dictionary<string, SnapshotLifecyclePolicyMetadata>;
}
@namespace("indices.index_management.create_index")
@rest_spec_name("indices.create")
class CreateIndexRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	mappings: TypeMapping;
	settings: Dictionary<string, any>;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_management.rollover_index")
@rest_spec_name("indices.rollover")
class RolloverIndexRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	conditions: RolloverConditions;
	mappings: TypeMapping;
	settings: Dictionary<string, any>;
	@request_parameter()
	dry_run: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
	@request_parameter()
	wait_for_active_shards: string;
}
@namespace("indices.index_settings.index_templates.put_index_template")
@rest_spec_name("indices.put_template")
class PutIndexTemplateRequest extends RequestBase {
	aliases: Dictionary<IndexName, Alias>;
	index_patterns: string[];
	mappings: TypeMapping;
	order: integer;
	settings: Dictionary<string, any>;
	version: integer;
	@request_parameter()
	create: boolean;
	@request_parameter()
	flat_settings: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("indices.mapping_management.put_mapping")
@rest_spec_name("indices.put_mapping")
class PutMappingRequest extends RequestBase {
	all_field: AllField;
	date_detection: boolean;
	dynamic: Union<boolean, DynamicMapping>;
	dynamic_date_formats: string[];
	dynamic_templates: Dictionary<string, DynamicTemplate>;
	field_names_field: FieldNamesField;
	index_field: IndexField;
	meta: Dictionary<string, any>;
	numeric_detection: boolean;
	properties: Dictionary<PropertyName, IProperty>;
	routing_field: RoutingField;
	size_field: SizeField;
	source_field: SourceField;
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	include_type_name: boolean;
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("ingest.put_pipeline")
@rest_spec_name("ingest.put_pipeline")
class PutPipelineRequest extends RequestBase {
	description: string;
	on_failure: Processor[];
	processors: Processor[];
	@request_parameter()
	master_timeout: Time;
	@request_parameter()
	timeout: Time;
}
@namespace("x_pack.cross_cluster_replication.auto_follow.create_auto_follow_pattern")
@rest_spec_name("ccr.put_auto_follow_pattern")
class CreateAutoFollowPatternRequest extends RequestBase {
	remote_cluster: string;
	leader_index_patterns: string[];
	follow_index_pattern: string;
	max_read_request_operation_count: integer;
	max_outstanding_read_requests: long;
	max_read_request_size: string;
	max_write_request_operation_count: integer;
	max_write_request_size: string;
	max_outstanding_write_requests: integer;
	max_write_buffer_count: integer;
	max_write_buffer_size: string;
	max_retry_delay: Time;
	max_poll_timeout: Time;
}
@namespace("x_pack.graph.explore")
@rest_spec_name("graph.explore")
class GraphExploreRequest extends RequestBase {
	@request_parameter()
	routing: Routing;
	@request_parameter()
	timeout: Time;
	connections: Hop;
	controls: GraphExploreControls;
	query: QueryContainer;
	vertices: GraphVertexDefinition[];
}
@namespace("search.scroll.scroll")
@rest_spec_name("scroll")
class ScrollRequest extends RequestBase {
	@request_parameter()
	total_hits_as_integer: boolean;
	scroll: Time;
	scroll_id: string;
}
@namespace("search.search")
@rest_spec_name("search")
class SearchRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	allow_partial_search_results: boolean;
	@request_parameter()
	analyze_wildcard: boolean;
	@request_parameter()
	analyzer: string;
	@request_parameter()
	batched_reduce_size: long;
	@request_parameter()
	ccs_minimize_roundtrips: boolean;
	@request_parameter()
	default_operator: DefaultOperator;
	@request_parameter()
	df: string;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	ignore_throttled: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	lenient: boolean;
	@request_parameter()
	max_concurrent_shard_requests: long;
	@request_parameter()
	pre_filter_shard_size: long;
	@request_parameter()
	preference: string;
	@request_parameter()
	request_cache: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	sequence_number_primary_term: boolean;
	@request_parameter()
	stats: string[];
	@request_parameter()
	suggest_field: Field;
	@request_parameter()
	suggest_mode: SuggestMode;
	@request_parameter()
	suggest_size: long;
	@request_parameter()
	suggest_text: string;
	@request_parameter()
	total_hits_as_integer: boolean;
	@request_parameter()
	typed_keys: boolean;
	aggs: Dictionary<string, AggregationContainer>;
	collapse: FieldCollapse;
	@request_parameter()
	docvalue_fields: Field[];
	explain: boolean;
	from: integer;
	highlight: Highlight;
	indices_boost: Dictionary<IndexName, double>;
	min_score: double;
	post_filter: QueryContainer;
	profile: boolean;
	query: QueryContainer;
	rescore: Rescore[];
	script_fields: Dictionary<string, ScriptField>;
	search_after: any[];
	size: integer;
	slice: SlicedScroll;
	sort: Sort[];
	_source: Union<boolean, SourceFilter>;
	@request_parameter()
	stored_fields: Field[];
	suggest: Dictionary<string, SuggestBucket>;
	terminate_after: long;
	timeout: string;
	track_scores: boolean;
	@request_parameter()
	track_total_hits: boolean;
	version: boolean;
}
@namespace("search.search_template")
@rest_spec_name("search_template")
class SearchTemplateRequest extends RequestBase {
	@request_parameter()
	allow_no_indices: boolean;
	@request_parameter()
	ccs_minimize_roundtrips: boolean;
	@request_parameter()
	expand_wildcards: ExpandWildcards;
	@request_parameter()
	explain: boolean;
	@request_parameter()
	ignore_throttled: boolean;
	@request_parameter()
	ignore_unavailable: boolean;
	@request_parameter()
	preference: string;
	@request_parameter()
	profile: boolean;
	@request_parameter()
	routing: Routing;
	@request_parameter()
	scroll: Time;
	@request_parameter()
	search_type: SearchType;
	@request_parameter()
	total_hits_as_integer: boolean;
	@request_parameter()
	typed_keys: boolean;
	id: string;
	params: Dictionary<string, any>;
	source: string;
}
@namespace("x_pack.security.privileges.put_privileges")
@rest_spec_name("security.put_privileges")
class PutPrivilegesRequest extends RequestBase {
	@request_parameter()
	refresh: Refresh;
	applications: Dictionary<string, Dictionary<string, PrivilegesActions>>;
}
@namespace("x_pack.sql.query_sql")
@rest_spec_name("sql.query")
class QuerySqlRequest extends RequestBase {
	@request_parameter()
	format: string;
	cursor: string;
	columnar: boolean;
	fetch_size: integer;
	filter: QueryContainer;
	query: string;
	time_zone: string;
}
@namespace("x_pack.sql.translate_sql")
@rest_spec_name("sql.translate")
class TranslateSqlRequest extends RequestBase {
	fetch_size: integer;
	filter: QueryContainer;
	query: string;
	time_zone: string;
}
@namespace("document.single.create")
@rest_spec_name("create")
class CreateRequest<TDocument> extends RequestBase {
	document: TDocument;
	@request_parameter()
	pipeline: string;
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
	wait_for_active_shards: string;
}
@namespace("document.single.index")
@rest_spec_name("index")
class IndexRequest<TDocument> extends RequestBase {
	document: TDocument;
	@request_parameter()
	if_primary_term: long;
	@request_parameter()
	if_sequence_number: long;
	@request_parameter()
	op_type: OpType;
	@request_parameter()
	pipeline: string;
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
	wait_for_active_shards: string;
}
@namespace("mapping.types.complex.object")
class ObjectProperty extends CorePropertyBase {
	dynamic: Union<boolean, DynamicMapping>;
	enabled: boolean;
	properties: Dictionary<PropertyName, IProperty>;
}
@namespace("mapping.types.core.search_as_you_type")
class SearchAsYouTypeProperty extends CorePropertyBase {
	analyzer: string;
	index: boolean;
	index_options: IndexOptions;
	max_shingle_size: integer;
	norms: boolean;
	search_analyzer: string;
	search_quote_analyzer: string;
	term_vector: TermVectorOption;
}
@namespace("mapping.types.core.text")
class TextProperty extends CorePropertyBase {
	analyzer: string;
	boost: double;
	eager_global_ordinals: boolean;
	fielddata: boolean;
	fielddata_frequency_filter: FielddataFrequencyFilter;
	index: boolean;
	index_options: IndexOptions;
	index_phrases: boolean;
	index_prefixes: TextIndexPrefixes;
	norms: boolean;
	position_increment_gap: integer;
	search_analyzer: string;
	search_quote_analyzer: string;
	term_vector: TermVectorOption;
}
@namespace("mapping.types.complex.nested")
class NestedProperty extends ObjectProperty {
	include_in_parent: boolean;
	include_in_root: boolean;
}
@namespace("mapping.types.core.binary")
class BinaryProperty extends DocValuesPropertyBase {
}
@namespace("mapping.types.core.boolean")
class BooleanProperty extends DocValuesPropertyBase {
	boost: double;
	fielddata: NumericFielddata;
	index: boolean;
	null_value: boolean;
}
@namespace("mapping.types.core.date_nanos")
class DateNanosProperty extends DocValuesPropertyBase {
	boost: double;
	format: string;
	ignore_malformed: boolean;
	index: boolean;
	null_value: Date;
	precision_step: integer;
}
@namespace("mapping.types.core.date")
class DateProperty extends DocValuesPropertyBase {
	boost: double;
	fielddata: NumericFielddata;
	format: string;
	ignore_malformed: boolean;
	index: boolean;
	null_value: Date;
	precision_step: integer;
}
@namespace("mapping.types.core.keyword")
class KeywordProperty extends DocValuesPropertyBase {
	boost: double;
	eager_global_ordinals: boolean;
	ignore_above: integer;
	index: boolean;
	index_options: IndexOptions;
	normalizer: string;
	norms: boolean;
	null_value: string;
	split_queries_on_whitespace: boolean;
}
@namespace("mapping.types.core.number")
class NumberProperty extends DocValuesPropertyBase {
	boost: double;
	coerce: boolean;
	fielddata: NumericFielddata;
	ignore_malformed: boolean;
	index: boolean;
	null_value: double;
	scaling_factor: double;
}
@namespace("mapping.types.geo.geo_point")
class GeoPointProperty extends DocValuesPropertyBase {
	ignore_malformed: boolean;
	ignore_z_value: boolean;
	null_value: GeoLocation;
}
@namespace("mapping.types.geo.geo_shape")
class GeoShapeProperty extends DocValuesPropertyBase {
	ignore_malformed: boolean;
	ignore_z_value: boolean;
	orientation: GeoOrientation;
	strategy: GeoStrategy;
	coerce: boolean;
}
@namespace("mapping.types.specialized.completion")
class CompletionProperty extends DocValuesPropertyBase {
	analyzer: string;
	contexts: SuggestContext[];
	max_input_length: integer;
	preserve_position_increments: boolean;
	preserve_separators: boolean;
	search_analyzer: string;
}
@namespace("mapping.types.specialized.generic")
class GenericProperty extends DocValuesPropertyBase {
	analyzer: string;
	boost: double;
	fielddata: StringFielddata;
	ignore_above: integer;
	index: boolean;
	index_options: IndexOptions;
	norms: boolean;
	null_value: string;
	position_increment_gap: integer;
	search_analyzer: string;
	term_vector: TermVectorOption;
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
@namespace("mapping.types.specialized.shape")
class ShapeProperty extends DocValuesPropertyBase {
	ignore_malformed: boolean;
	ignore_z_value: boolean;
	orientation: ShapeOrientation;
	coerce: boolean;
}
@namespace("mapping.types.specialized.token_count")
class TokenCountProperty extends DocValuesPropertyBase {
	analyzer: string;
	boost: double;
	index: boolean;
	null_value: double;
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
@namespace("mapping.types.core.range.ip_range")
class IpRangeProperty extends RangePropertyBase {
}
@namespace("mapping.types.core.range.long_range")
class LongRangeProperty extends RangePropertyBase {
}
@namespace("common")
class Dictionary<TKey, TValue> {
	key: TKey;
	value: TValue;
}
/** namespace:common **/
enum HttpMethod {
	GET = 0,
	POST = 1,
	PUT = 2,
	DELETE = 3,
	HEAD = 4
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
enum Health {
	green = 0,
	yellow = 1,
	red = 2
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
/** namespace:common **/
enum PostType {
	ByteArray = 0,
	LiteralString = 1,
	EnumerableOfString = 2,
	EnumerableOfObject = 3,
	Serializable = 4
}
/** namespace:common **/
enum PipelineFailure {
	BadAuthentication = 0,
	BadResponse = 1,
	PingFailure = 2,
	SniffFailure = 3,
	CouldNotStartSniffOnStartup = 4,
	MaxTimeoutReached = 5,
	MaxRetriesReached = 6,
	Unexpected = 7,
	BadRequest = 8,
	NoNodesAttempted = 9
}
/** namespace:common **/
enum ExpandWildcards {
	open = 0,
	closed = 1,
	none = 2,
	all = 3
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
	parents = 1,
	none = 2
}
/** namespace:common **/
enum Refresh {
	true = 0,
	false = 1,
	wait_for = 2
}
/** namespace:common **/
enum VersionType {
	internal = 0,
	external = 1,
	external_gte = 2,
	force = 3
}
/** namespace:common **/
enum Conflicts {
	abort = 0,
	proceed = 1
}
/** namespace:common **/
enum DefaultOperator {
	AND = 0,
	OR = 1
}
/** namespace:common **/
enum SearchType {
	query_then_fetch = 0,
	dfs_query_then_fetch = 1
}
/** namespace:common **/
enum OpType {
	index = 0,
	create = 1
}
/** namespace:common **/
enum SuggestMode {
	missing = 0,
	popular = 1,
	always = 2
}
@namespace("common")
class CustomResponseBuilderBase {
}
@namespace("common")
class UrlParameter {
}
@namespace("common")
class MemoryStreamFactory {
}
@namespace("common")
class ElasticsearchSerializer {
}
@namespace("common")
class ElasticsearchResponse {
}
@namespace("common")
class Connection {
}
@namespace("common")
class ConnectionPool {
	last_update: Date;
	max_retries: integer;
	nodes: Node[];
	sniffed_on_startup: boolean;
	supports_pinging: boolean;
	supports_reseeding: boolean;
	using_ssl: boolean;
}
@namespace("common")
class PostData {
	disable_direct_streaming: boolean;
	type: PostType;
	written_bytes: short[];
}
@namespace("common")
class ServerError {
	error: MainError;
	status: integer;
}
@namespace("common")
class ShardFailure {
	index: string;
	node: string;
	reason: ErrorCause;
	shard: integer;
	status: string;
}
@namespace("common")
class ElasticsearchUrlFormatter {
}
@namespace("cluster.nodes_stats")
class DataPathStats {
	available: string;
	available_in_bytes: long;
	disk_queue: string;
	disk_reads: long;
	disk_read_size: string;
	disk_read_size_in_bytes: long;
	disk_writes: long;
	disk_write_size: string;
	disk_write_size_in_bytes: long;
	free: string;
	free_in_bytes: long;
	mount: string;
	path: string;
	total: string;
	total_in_bytes: long;
	type: string;
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
class CPUStats {
	percent: integer;
	sys: string;
	sys_in_millis: long;
	total: string;
	total_in_millis: long;
	user: string;
	user_in_millis: long;
}
@namespace("cluster.nodes_stats")
class MemoryStats {
	resident: string;
	resident_in_bytes: long;
	share: string;
	share_in_bytes: long;
	total_virtual: string;
	total_virtual_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class NodeBufferPool {
	count: long;
	total_capacity: string;
	total_capacity_in_bytes: long;
	used: string;
	used_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class JvmClassesStats {
	current_loaded_count: long;
	total_loaded_count: long;
	total_unloaded_count: long;
}
@namespace("cluster.nodes_stats")
class GarbageCollectionStats {
	collectors: Dictionary<string, GarbageCollectionGenerationStats>;
}
@namespace("cluster.nodes_stats")
class GarbageCollectionGenerationStats {
	collection_count: long;
	collection_time: string;
	collection_time_in_millis: long;
}
@namespace("cluster.nodes_stats")
class ThreadStats {
	count: long;
	peak_count: long;
}
@namespace("cluster.nodes_stats")
class JvmPool {
	max: string;
	max_in_bytes: long;
	peak_max: string;
	peak_max_in_bytes: long;
	peak_used: string;
	peak_used_in_bytes: long;
	used: string;
	used_in_bytes: long;
}
@namespace("cluster.nodes_stats")
class ExtendedMemoryStats extends MemoryStats {
	free_percent: integer;
	used_percent: integer;
}
@namespace("cluster.nodes_stats")
class LoadAverageStats {
	'15m': float;
	'5m': float;
	'1m': float;
}
@namespace("x_pack.info.x_pack_usage")
class IlmPolicyStatistics {
	phases: Phases;
	indices_managed: integer;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingCount {
	active: long;
	total: long;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingExecution {
	actions: Dictionary<string, ExecutionAction>;
}
@namespace("x_pack.info.x_pack_usage")
class ExecutionAction {
	total: long;
	total_in_ms: long;
}
@namespace("x_pack.info.x_pack_usage")
class AlertingInput {
	input: Dictionary<string, AlertingCount>;
	trigger: Dictionary<string, AlertingCount>;
}
@namespace("x_pack.info.x_pack_usage")
class DataFeed {
	count: long;
}
@namespace("x_pack.info.x_pack_usage")
class JobStatistics {
	avg: double;
	max: double;
	min: double;
	total: double;
}
@namespace("x_pack.info.x_pack_usage")
class ForecastStatistics {
	forecasted_jobs: long;
	memory_bytes: JobStatistics;
	processing_time_ms: JobStatistics;
	records: JobStatistics;
	status: Dictionary<string, long>;
	total: long;
}
@namespace("x_pack.info.x_pack_usage")
class SecurityFeatureToggle {
	enabled: boolean;
}
@namespace("x_pack.info.x_pack_usage")
class IpFilterUsage {
	http: boolean;
	transport: boolean;
}
@namespace("x_pack.info.x_pack_usage")
class RoleMappingUsage {
	enabled: integer;
	size: integer;
}
@namespace("x_pack.info.x_pack_usage")
class RoleUsage {
	dls: boolean;
	fls: boolean;
	size: long;
}
@namespace("x_pack.info.x_pack_usage")
class SslUsage {
	http: SecurityFeatureToggle;
	transport: SecurityFeatureToggle;
}
@namespace("x_pack.info.x_pack_usage")
class AuditUsage extends SecurityFeatureToggle {
	outputs: string[];
}
@namespace("x_pack.info.x_pack_usage")
class RealmUsage extends XPackUsage {
	name: string[];
	order: long[];
	size: long[];
}
