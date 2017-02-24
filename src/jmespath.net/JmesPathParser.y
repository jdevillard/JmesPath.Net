%namespace DevLab.JmesPath
%partial
%parsertype JmesPathParser
%visibility public
%tokentype TokenType

%union 	{ 
       		public Token Token; 
       	}

%token
	T_COLON,
	T_COMMA,
	T_DOT,
	T_NUMBER,
	T_LBRACE,
	T_RBRACE,
	T_LBRACKET,
	T_RBRACKET,
	T_QSTRING,
	T_USTRING

%start expression

%%
expression			: identifier
					{
						System.Diagnostics.Debug.WriteLine("expression (identifier): {0}.", $1.Token);
						OnExpression();
					}
					| index_expression
					{
						System.Diagnostics.Debug.WriteLine("expression (index_expression): {0}.", $1.Token);
						OnExpression();
					}
					| multi_select_hash
					{
						System.Diagnostics.Debug.WriteLine("expression (multi_select_hash): {0}", $1.Token);
						OnExpression();
					}
					| multi_select_list
					{
						System.Diagnostics.Debug.WriteLine("expression (multi_select_list): {0}", $1.Token);
						OnExpression();
					}
					| sub_expression
					{
						System.Diagnostics.Debug.WriteLine("expression (sub_expression): {0}.", $1.Token);
						OnExpression();
					}
					;

index_expression	: expression bracket_specifier
					{
						System.Diagnostics.Debug.WriteLine("index expression (bracket_specifier): {0}.", $1.Token);
						OnIndexExpression();
					}
					| bracket_specifier
					{
					}
					;

bracket_specifier	: T_LBRACKET T_NUMBER T_RBRACKET
					{
						System.Diagnostics.Debug.WriteLine("bracket_specifier : {0}.", $2.Token);
						OnBracketSpecifier($2.Token);
					}
					| T_LBRACKET T_RBRACKET
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

sub_expression		: expression T_DOT identifier
					{
						OnSubExpression();
					}
					| expression T_DOT multi_select_hash
					{
						OnSubExpression();
					}
					| expression T_DOT multi_select_list
					{
						OnSubExpression();
					}
					;

identifier			: unquoted_string
					{
						System.Diagnostics.Debug.WriteLine("identifier (quoted string): {0}.", $1.Token);
						OnIdentifier($1.Token);
					}
					| quoted_string
					{
						System.Diagnostics.Debug.WriteLine("identifier (unquoted string): {0}", $1.Token);
						OnIdentifier($1.Token);
					}
					;

unquoted_string		: T_USTRING
					{
						System.Diagnostics.Debug.WriteLine("unquoted string : {0}", $1.Token);
					}
					;

quoted_string		: T_QSTRING
					{
						System.Diagnostics.Debug.WriteLine("quoted string : {0}", $1.Token);
					}
					;

%%