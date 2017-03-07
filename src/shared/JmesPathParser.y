%namespace DevLab.JmesPath
%partial
%parsertype JmesPathParser
%visibility internal
%tokentype TokenType

%union 	{ 
       		public Token Token; 
       	}

%token
	T_COLON,
	T_COMMA,
	T_DOT,
	T_STAR,
	T_PIPE,
	T_NUMBER,
	T_LBRACE,
	T_RBRACE,
	T_LBRACKET,
	T_RBRACKET,
	T_LSTRING,
	T_QSTRING,
	T_RSTRING,
	T_USTRING

%left T_PIPE
%left T_DOT
%left T_LBRACKET
%left T_STAR
%left T_RBRACKET

%start expression

%%
expression			: expression_impl
					{
						OnExpression();
					}
					;

expression_impl		: sub_expression
					| index_expression
					| hash_wildcard
					| identifier
					| multi_select_list
					| multi_select_hash
					| literal
					| pipe_expression
					| raw_string
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
					| T_LBRACKET T_RBRACKET
					{
						System.Diagnostics.Debug.WriteLine("bracket_specifier (flattening projection).");
						OnFlattenProjection();
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

identifier			: identifier_impl
					{
						System.Diagnostics.Debug.WriteLine("identifier ({0}): {1}.", $1.Token.Type, $1.Token);
						OnIdentifier($1.Token);
					}
					;
					
identifier_impl		: quoted_string
					| unquoted_string
					;

hash_wildcard		: T_STAR
					{
						System.Diagnostics.Debug.WriteLine("wildcard (hash wildcard projection): {0}", $1.Token);
						OnHashWildcardProjection();
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