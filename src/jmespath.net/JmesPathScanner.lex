%namespace DevLab.JmesPath
%scannertype JmesPathScanner
%visibility public
%tokentype	TokenType

%option stack, minimize, parser, verbose, persistbuffer, embedbuffers 


EOL             (\r\n?|\n)

T_COLON         :
T_COMMA         ,
T_DOT           \.
T_STAR			\*
T_NUMBER		\-?[0-9]+
T_LSTRING		`[^`]*((\\`)*[^`]*)*`
T_QSTRING		\"[^\"\\\b\f\n\r\t]*((\\[\"\\/bfnrt]|\\u[0-9a-fA-F]{4})+[^\"\\\b\f\n\r\t]*)*\"
T_RSTRING		'(\\?[^'\\])*((\\['\\])+(\\?[^'\\])*)*'
T_USTRING		[A-Za-z_][0-9A-Za-z_]*
T_LBRACE		\{
T_RBRACE		\}
T_LBRACKET		\[
T_RBRACKET		\]


%{
%}

%%

{T_COLON}		{ yylval.Token = Token.Create(TokenType.T_COLON, yytext); return (int)TokenType.T_COLON; }
{T_COMMA}		{ yylval.Token = Token.Create(TokenType.T_COMMA, yytext); return (int)TokenType.T_COMMA; }
{T_DOT}			{ yylval.Token = Token.Create(TokenType.T_DOT, yytext); return (int)TokenType.T_DOT; }
{T_STAR}		{ yylval.Token = Token.Create(TokenType.T_STAR, yytext); return (int)TokenType.T_STAR; }
{T_NUMBER}		{ yylval.Token = Token.Create(TokenType.T_NUMBER, yytext); return (int)TokenType.T_NUMBER; }
{T_LSTRING}		{ yylval.Token = Token.Create(TokenType.T_LSTRING, yytext); return (int)TokenType.T_LSTRING; }
{T_QSTRING}		{ yylval.Token = Token.Create(TokenType.T_QSTRING, yytext); return (int)TokenType.T_QSTRING; }
{T_RSTRING}		{ yylval.Token = Token.Create(TokenType.T_RSTRING, yytext); return (int)TokenType.T_RSTRING; }
{T_USTRING}		{ yylval.Token = Token.Create(TokenType.T_USTRING, yytext); return (int)TokenType.T_USTRING; }
{T_LBRACE}		{ yylval.Token = Token.Create(TokenType.T_LBRACE, yytext); return (int)TokenType.T_LBRACE; }
{T_RBRACE}		{ yylval.Token = Token.Create(TokenType.T_RBRACE, yytext); return (int)TokenType.T_RBRACE; }
{T_LBRACKET}	{ yylval.Token = Token.Create(TokenType.T_LBRACKET, yytext); return (int)TokenType.T_LBRACKET; }
{T_RBRACKET}	{ yylval.Token = Token.Create(TokenType.T_RBRACKET, yytext); return (int)TokenType.T_RBRACKET; }

%%