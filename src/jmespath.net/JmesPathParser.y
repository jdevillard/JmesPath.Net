%namespace DevLab.JmesPath
%partial
%parsertype JmesPathParser
%visibility internal
%tokentype TokenType

%union 	{ 
       		public Token Token; 
       	}

%token

	T_AND,
	T_OR,
	T_NOT,

	T_COLON,
	T_COMMA,
	T_DOT,
	T_PIPE,
	
	T_EQ,
	T_GT,
	T_GE,
	T_LT,
	T_LE,
	T_NE,

	T_FILTER,
	T_FLATTEN,
	T_STAR,
	T_CURRENT,
	T_ETYPE,

	T_NUMBER,
	T_LSTRING,
	T_QSTRING,
	T_RSTRING,
	T_USTRING,

	T_LBRACE,
	T_RBRACE,
	T_LBRACKET,
	T_RBRACKET,
	T_LPAREN,
	T_RPAREN

%left T_PIPE
%left T_OR
%left T_AND
%left T_NOT
%left T_EQ
%left T_GT
%left T_GE
%left T_LT
%left T_LE
%left T_NE
%left T_NOT
%left T_DOT
%left T_LBRACKET
%left T_STAR
%left T_FILTER
%left T_FLATTEN
%left T_LISTWILDCARD
%left T_RBRACKET

%start expression

%%
expression			: expression_impl
					{
						OnExpression();
						ResolveParsingState();
					}					
					;

expression_impl		: sub_expression
					| index_expression
					| comparator_expression
					| or_expression
					| identifier
					| and_expression
					| not_expression
					| paren_expression
					| hash_wildcard
					| multi_select_list
					| multi_select_hash
					| literal
					| pipe_expression
					| function_expression
					| raw_string
					| current_node
					;

sub_expression		: sub_expression_impl
					{
						OnSubExpression();
					}
					;

sub_expression_impl	: expression T_DOT identifier
					| expression T_DOT multi_select_hash
					| expression T_DOT multi_select_list
					| expression T_DOT hash_wildcard
					;


index_expression	: expression bracket_specifier
					{
						System.Diagnostics.Debug.WriteLine("index expression (expression, bracket_specifier): {0}.", $1.Token);
						OnIndexExpression();
					}
					| bracket_specifier
					;

function_expression	: unquoted_string arguments
					{
						PopFunction($1.Token);
					}
					;

arguments			: T_LPAREN T_RPAREN
					{
						PushFunction();
					}
					| T_LPAREN function_arguments T_RPAREN
					;
		
function_arguments	: expression
					{
						PushFunction();
						AddFunctionArg();
					}
					| expression_type
					{
						PushFunction();
						AddFunctionArg();
					}
					| function_arguments T_COMMA expression 
					{
						AddFunctionArg();
					}
					| function_arguments T_COMMA expression_type 
					{
						AddFunctionArg();
					}
					;

current_node		: T_CURRENT
					{
						OnCurrentNode();
					}
					;
expression_type		: T_ETYPE expression
					{
						OnExpressionType();
					}
					;
										
bracket_specifier	: T_LBRACKET T_NUMBER T_RBRACKET
					{
						System.Diagnostics.Debug.WriteLine("bracket_specifier (index): {0}.", $2.Token);
						OnIndex($2.Token);
					}
					| T_LBRACKET T_STAR T_RBRACKET
					{
						System.Diagnostics.Debug.WriteLine("bracket_specifier (list wildcard projection).");
						OnListWildcardProjection();
					}
					| T_LBRACKET slice_expression T_RBRACKET
					| T_FILTER expression T_RBRACKET
					{
						OnFilterProjection();
					}
					| T_FLATTEN
					{
						System.Diagnostics.Debug.WriteLine("bracket_specifier (flattening projection).");
						OnFlattenProjection();
					}
					;

comparator_expression
					: expression T_EQ expression
					{
						OnComparisonExpression($2.Token);
					}
					| expression T_GE expression
					{
						OnComparisonExpression($2.Token);
					}
					| expression T_GT expression
					{
						OnComparisonExpression($2.Token);
					}
					| expression T_LE expression
					{
						OnComparisonExpression($2.Token);
					}
					| expression T_LT expression
					{
						OnComparisonExpression($2.Token);
					}
					| expression T_NE expression
					{
						OnComparisonExpression($2.Token);
					}
					;

or_expression		: expression T_OR expression
					{
						OnOrExpression();
					}
					;

identifier			: identifier_impl
					{
						System.Diagnostics.Debug.WriteLine("identifier ({0}): {1}.", $1.Token.Type, $1.Token);
						OnIdentifier($1.Token);
					}
					;
					
identifier_impl		: quoted_string
					| unquoted_string
					;

and_expression		: expression T_AND expression
					{
						OnAndExpression();
					}
					;

not_expression		: T_NOT expression
					{
						OnNotExpression();
					}
					;

paren_expression	: T_LPAREN expression T_RPAREN
					;

hash_wildcard		: T_STAR
					{
						System.Diagnostics.Debug.WriteLine("wildcard (hash wildcard projection): {0}", $1.Token);
						OnHashWildcardProjection();
					}
					;

multi_select_hash	: T_LBRACE keyval_expressions T_RBRACE
					{
						PopMultiSelectHash();
					};

keyval_expressions	: keyval_expression
					{
						PushMultiSelectHash();
						AddMultiSelectHashExpression();
					}
					| keyval_expressions T_COMMA keyval_expression
					{
						AddMultiSelectHashExpression();
					}
					;

keyval_expression	: identifier T_COLON expression
					;


multi_select_list	: T_LBRACKET expressions T_RBRACKET
					{
						PopMultiSelectList();
					}
					;

expressions			: expression
					{
						PushMultiSelectList();
						AddMultiSelectListExpression();
					}
					| expressions T_COMMA expression
					{
						AddMultiSelectListExpression();
					}
					;

pipe_expression		: expression T_PIPE expression
					{
						OnPipeExpression();
					}
					;

slice_expression	: T_COLON
					{
						OnSliceExpression(null, null, null);
					}
					| T_NUMBER T_COLON
					{
						OnSliceExpression($1.Token, null, null);
					}
					| T_NUMBER T_COLON          T_COLON
					{
						OnSliceExpression($1.Token, null, null);
					}
					| T_NUMBER T_COLON T_NUMBER
					{
						OnSliceExpression($1.Token, $3.Token, null);
					}
					| T_NUMBER T_COLON T_NUMBER T_COLON
					{
						OnSliceExpression($1.Token, $3.Token, null);
					}
					| T_NUMBER T_COLON T_NUMBER T_COLON T_NUMBER
					{
						OnSliceExpression($1.Token, $3.Token, $5.Token);
					}
					| T_NUMBER T_COLON          T_COLON T_NUMBER
					{
						OnSliceExpression($1.Token, null, $4.Token);
					}
					|          T_COLON T_NUMBER
					{
						OnSliceExpression(null, $2.Token, null);
					}
					|          T_COLON T_NUMBER T_COLON
					{
						OnSliceExpression(null, $2.Token, null);
					}
					|          T_COLON T_NUMBER T_COLON T_NUMBER
					{
						OnSliceExpression(null, $2.Token, $4.Token);
					}
					|          T_COLON          T_COLON T_NUMBER
					{
						OnSliceExpression(null, null, $3.Token);
					}
					|          T_COLON          T_COLON
					{
						OnSliceExpression(null, null, null);
					}
					;

quoted_string		: T_QSTRING
					;

unquoted_string		: T_USTRING
					;

literal				: T_LSTRING
					{
						System.Diagnostics.Debug.WriteLine("literal string : {0}", $1.Token);
						OnLiteralString($1.Token);
					}
					;
raw_string			: T_RSTRING
					{
						System.Diagnostics.Debug.WriteLine("raw string : {0}", $1.Token);
						OnRawString($1.Token);
					}
					;

%%