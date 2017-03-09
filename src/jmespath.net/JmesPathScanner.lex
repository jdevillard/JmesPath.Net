%namespace DevLab.JmesPath
%scannertype JmesPathScanner
%visibility internal
%tokentype	TokenType

%option stack, minimize, parser, verbose, persistbuffer, embedbuffers 

EOL             (\r\n?|\n)

T_AND			&&
T_OR			\|\|
T_NOT			!

T_COLON         :
T_COMMA         ,
T_DOT           \.
T_PIPE			\|

T_EQ			==
T_GT			>
T_GE			>=
T_LT			<
T_LE			<=
T_NE			!=

T_FLATTEN		\[\]
T_FILTER		\[\?
T_HASHWILDCARD	\*
T_LISTWILDCARD	\[\*\]

T_NUMBER		\-?[0-9]+

T_LSTRING		`[^`]*((\\`)*[^`]*)*`
T_QSTRING		\"[^\"\\\b\f\n\r\t]*((\\[\"\\/bfnrt]|\\u[0-9a-fA-F]{4})+[^\"\\\b\f\n\r\t]*)*\"
T_RSTRING		'(\\?[^'\\])*((\\['\\])+(\\?[^'\\])*)*'
T_USTRING		[A-Za-z_][0-9A-Za-z_]*

T_LBRACE		\{
T_RBRACE		\}
T_LBRACKET		\[
T_RBRACKET		\]
T_LPAREN		\(
T_RPAREN		\)

%{
%}

%%

{T_AND}				{ yylval.Token = Token.Create(TokenType.T_AND, yytext); return (int)TokenType.T_AND; }
{T_OR}				{ yylval.Token = Token.Create(TokenType.T_AND, yytext); return (int)TokenType.T_OR; }
{T_NOT}				{ yylval.Token = Token.Create(TokenType.T_NOT, yytext); return (int)TokenType.T_NOT; }

{T_COLON}			{ yylval.Token = Token.Create(TokenType.T_COLON, yytext); return (int)TokenType.T_COLON; }
{T_COMMA}			{ yylval.Token = Token.Create(TokenType.T_COMMA, yytext); return (int)TokenType.T_COMMA; }
{T_DOT}				{ yylval.Token = Token.Create(TokenType.T_DOT, yytext); return (int)TokenType.T_DOT; }
{T_PIPE}			{ yylval.Token = Token.Create(TokenType.T_PIPE, yytext); return (int)TokenType.T_PIPE; }

{T_EQ}				{ yylval.Token = Token.Create(TokenType.T_EQ, yytext); return (int)TokenType.T_EQ; }
{T_GT}				{ yylval.Token = Token.Create(TokenType.T_GT, yytext); return (int)TokenType.T_GT; }
{T_GE}				{ yylval.Token = Token.Create(TokenType.T_GE, yytext); return (int)TokenType.T_GE; }
{T_LT}				{ yylval.Token = Token.Create(TokenType.T_LT, yytext); return (int)TokenType.T_LT; }
{T_LE}				{ yylval.Token = Token.Create(TokenType.T_LE, yytext); return (int)TokenType.T_LE; }
{T_NE}				{ yylval.Token = Token.Create(TokenType.T_NE, yytext); return (int)TokenType.T_NE; }

{T_FILTER}			{ yylval.Token = Token.Create(TokenType.T_FILTER, yytext); return (int)TokenType.T_FILTER; }
{T_FLATTEN}			{ yylval.Token = Token.Create(TokenType.T_FLATTEN, yytext); return (int)TokenType.T_FLATTEN; }
{T_HASHWILDCARD}	{ yylval.Token = Token.Create(TokenType.T_HASHWILDCARD, yytext); return (int)TokenType.T_HASHWILDCARD; }
{T_LISTWILDCARD}	{ yylval.Token = Token.Create(TokenType.T_LISTWILDCARD, yytext); return (int)TokenType.T_LISTWILDCARD; }

{T_NUMBER}			{ yylval.Token = Token.Create(TokenType.T_NUMBER, yytext); return (int)TokenType.T_NUMBER; }
{T_LSTRING}			{ yylval.Token = Token.Create(TokenType.T_LSTRING, yytext); return (int)TokenType.T_LSTRING; }
{T_QSTRING}			{ yylval.Token = Token.Create(TokenType.T_QSTRING, yytext); return (int)TokenType.T_QSTRING; }
{T_RSTRING}			{ yylval.Token = Token.Create(TokenType.T_RSTRING, yytext); return (int)TokenType.T_RSTRING; }
{T_USTRING}			{ yylval.Token = Token.Create(TokenType.T_USTRING, yytext); return (int)TokenType.T_USTRING; }

{T_LBRACE}			{ yylval.Token = Token.Create(TokenType.T_LBRACE, yytext); return (int)TokenType.T_LBRACE; }
{T_RBRACE}			{ yylval.Token = Token.Create(TokenType.T_RBRACE, yytext); return (int)TokenType.T_RBRACE; }
{T_LBRACKET}		{ yylval.Token = Token.Create(TokenType.T_LBRACKET, yytext); return (int)TokenType.T_LBRACKET; }
{T_RBRACKET}		{ yylval.Token = Token.Create(TokenType.T_RBRACKET, yytext); return (int)TokenType.T_RBRACKET; }
{T_LPAREN}			{ yylval.Token = Token.Create(TokenType.T_LPAREN, yytext); return (int)TokenType.T_LPAREN; }
{T_RPAREN}			{ yylval.Token = Token.Create(TokenType.T_RPAREN, yytext); return (int)TokenType.T_RPAREN; }

%%