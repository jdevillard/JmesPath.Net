%namespace DevLab.JmesPath
%scannertype JmesPathScanner
%visibility internal
%tokentype	TokenType
%using StarodubOleg.GPPG.Runtime;

%option stack, minimize, parser, verbose, persistbuffer, embedbuffers 

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
T_STAR			\*
T_CURRENT		@

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

T_WHITESPACE	[ \b\f\n\r\t]+
E_UNRECOGNIZED	.

%{
%}

%%

{T_AND}				{ return MakeToken(TokenType.T_AND); }
{T_OR}				{ return MakeToken(TokenType.T_OR); }
{T_NOT}				{ return MakeToken(TokenType.T_NOT); }

{T_COLON}			{ return MakeToken(TokenType.T_COLON); }
{T_COMMA}			{ return MakeToken(TokenType.T_COMMA); }
{T_DOT}				{ return MakeToken(TokenType.T_DOT); }
{T_PIPE}			{ return MakeToken(TokenType.T_PIPE); }

{T_EQ}				{ return MakeToken(TokenType.T_EQ); }
{T_GT}				{ return MakeToken(TokenType.T_GT); }
{T_GE}				{ return MakeToken(TokenType.T_GE); }
{T_LT}				{ return MakeToken(TokenType.T_LT); }
{T_LE}				{ return MakeToken(TokenType.T_LE); }
{T_NE}				{ return MakeToken(TokenType.T_NE); }

{T_FILTER}			{ return MakeToken(TokenType.T_FILTER); }
{T_FLATTEN}			{ return MakeToken(TokenType.T_FLATTEN); }
{T_STAR}			{ return MakeToken(TokenType.T_STAR); }
{T_CURRENT}			{ return MakeToken(TokenType.T_CURRENT); }

{T_NUMBER}			{ return MakeToken(TokenType.T_NUMBER); }
{T_LSTRING}			{ return MakeToken(TokenType.T_LSTRING); }
{T_QSTRING}			{ return MakeToken(TokenType.T_QSTRING); }
{T_RSTRING}			{ return MakeToken(TokenType.T_RSTRING); }
{T_USTRING}			{ return MakeToken(TokenType.T_USTRING); }

{T_LBRACE}			{ return MakeToken(TokenType.T_LBRACE); }
{T_RBRACE}			{ return MakeToken(TokenType.T_RBRACE); }
{T_LBRACKET}		{ return MakeToken(TokenType.T_LBRACKET); }
{T_RBRACKET}		{ return MakeToken(TokenType.T_RBRACKET); }
{T_LPAREN}			{ return MakeToken(TokenType.T_LPAREN); }
{T_RPAREN}			{ return MakeToken(TokenType.T_RPAREN); }

{T_WHITESPACE}   	{ }
{E_UNRECOGNIZED}	{ yyerror(yytext); }

%{

	yylloc = new LexLocation(tokLin,tokCol,tokELin,tokECol);

%}

%%