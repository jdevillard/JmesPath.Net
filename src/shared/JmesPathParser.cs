// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 0.1.0.0
// Machine:  DESKTOP-UQ0H65F
// DateTime: 04/03/2017 17:19:12
// Input file <C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y - 04/03/2017 17:11:24>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
internal enum TokenType {error=2,EOF=3,T_COLON=4,T_COMMA=5,T_DOT=6,
    T_STAR=7,T_NUMBER=8,T_LBRACE=9,T_RBRACE=10,T_LBRACKET=11,T_RBRACKET=12,
    T_LSTRING=13,T_QSTRING=14,T_RSTRING=15,T_USTRING=16};

internal partial struct ValueType
#line 7 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
        { 
       		public Token Token; 
       	}
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "0.1.0.0")]
internal abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "0.1.0.0")]
internal class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "0.1.0.0")]
internal partial class JmesPathParser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[39];
  private static State[] states = new State[57];
  private static string[] nonTerms = new string[] {
      "expression", "$accept", "expression_impl", "sub_expression", "index_expression", 
      "hash_wildcard", "identifier", "multi_select_list", "multi_select_hash", 
      "literal", "raw_string", "sub_expression_impl", "bracket_specifier", "slice_expression", 
      "keyval_expressions", "keyval_expression", "expressions", "identifier_impl", 
      "quoted_string", "unquoted_string", };

  static JmesPathParser() {
    states[0] = new State(new int[]{11,39,7,46,14,10,16,12,9,13,13,51,15,53},new int[]{-1,1,-3,34,-4,35,-12,36,-5,37,-13,38,-6,45,-7,47,-18,8,-19,9,-20,11,-8,48,-9,49,-10,50,-11,52});
    states[1] = new State(new int[]{3,2,6,3,11,22},new int[]{-13,21});
    states[2] = new State(-1);
    states[3] = new State(new int[]{14,10,16,12,9,13,11,56,7,46},new int[]{-7,4,-9,5,-8,6,-6,7,-18,8,-19,9,-20,11});
    states[4] = new State(-12);
    states[5] = new State(-13);
    states[6] = new State(-14);
    states[7] = new State(-15);
    states[8] = new State(-31);
    states[9] = new State(-32);
    states[10] = new State(-35);
    states[11] = new State(-33);
    states[12] = new State(-36);
    states[13] = new State(new int[]{14,10,16,12},new int[]{-15,14,-16,55,-7,18,-18,8,-19,9,-20,11});
    states[14] = new State(new int[]{10,15,5,16});
    states[15] = new State(-22);
    states[16] = new State(new int[]{14,10,16,12},new int[]{-16,17,-7,18,-18,8,-19,9,-20,11});
    states[17] = new State(-24);
    states[18] = new State(new int[]{4,19});
    states[19] = new State(new int[]{11,39,7,46,14,10,16,12,9,13,13,51,15,53},new int[]{-1,20,-3,34,-4,35,-12,36,-5,37,-13,38,-6,45,-7,47,-18,8,-19,9,-20,11,-8,48,-9,49,-10,50,-11,52});
    states[20] = new State(new int[]{6,3,11,22,10,-25,5,-25},new int[]{-13,21});
    states[21] = new State(-16);
    states[22] = new State(new int[]{8,23,7,29,12,33},new int[]{-14,31});
    states[23] = new State(new int[]{12,24,4,25});
    states[24] = new State(-18);
    states[25] = new State(new int[]{8,26});
    states[26] = new State(new int[]{4,27,12,-29});
    states[27] = new State(new int[]{8,28});
    states[28] = new State(-30);
    states[29] = new State(new int[]{12,30});
    states[30] = new State(-19);
    states[31] = new State(new int[]{12,32});
    states[32] = new State(-20);
    states[33] = new State(-21);
    states[34] = new State(-2);
    states[35] = new State(-3);
    states[36] = new State(-11);
    states[37] = new State(-4);
    states[38] = new State(-17);
    states[39] = new State(new int[]{8,23,7,40,12,33,11,39,14,10,16,12,9,13,13,51,15,53},new int[]{-14,31,-17,41,-1,54,-3,34,-4,35,-12,36,-5,37,-13,38,-6,45,-7,47,-18,8,-19,9,-20,11,-8,48,-9,49,-10,50,-11,52});
    states[40] = new State(new int[]{12,30,6,-34,11,-34,5,-34});
    states[41] = new State(new int[]{12,42,5,43});
    states[42] = new State(-26);
    states[43] = new State(new int[]{11,39,7,46,14,10,16,12,9,13,13,51,15,53},new int[]{-1,44,-3,34,-4,35,-12,36,-5,37,-13,38,-6,45,-7,47,-18,8,-19,9,-20,11,-8,48,-9,49,-10,50,-11,52});
    states[44] = new State(new int[]{6,3,11,22,12,-28,5,-28},new int[]{-13,21});
    states[45] = new State(-5);
    states[46] = new State(-34);
    states[47] = new State(-6);
    states[48] = new State(-7);
    states[49] = new State(-8);
    states[50] = new State(-9);
    states[51] = new State(-37);
    states[52] = new State(-10);
    states[53] = new State(-38);
    states[54] = new State(new int[]{6,3,11,22,12,-27,5,-27},new int[]{-13,21});
    states[55] = new State(-23);
    states[56] = new State(new int[]{11,39,7,46,14,10,16,12,9,13,13,51,15,53},new int[]{-17,41,-1,54,-3,34,-4,35,-12,36,-5,37,-13,38,-6,45,-7,47,-18,8,-19,9,-20,11,-8,48,-9,49,-10,50,-11,52});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-3, new int[]{-4});
    rules[4] = new Rule(-3, new int[]{-5});
    rules[5] = new Rule(-3, new int[]{-6});
    rules[6] = new Rule(-3, new int[]{-7});
    rules[7] = new Rule(-3, new int[]{-8});
    rules[8] = new Rule(-3, new int[]{-9});
    rules[9] = new Rule(-3, new int[]{-10});
    rules[10] = new Rule(-3, new int[]{-11});
    rules[11] = new Rule(-4, new int[]{-12});
    rules[12] = new Rule(-12, new int[]{-1,6,-7});
    rules[13] = new Rule(-12, new int[]{-1,6,-9});
    rules[14] = new Rule(-12, new int[]{-1,6,-8});
    rules[15] = new Rule(-12, new int[]{-1,6,-6});
    rules[16] = new Rule(-5, new int[]{-1,-13});
    rules[17] = new Rule(-5, new int[]{-13});
    rules[18] = new Rule(-13, new int[]{11,8,12});
    rules[19] = new Rule(-13, new int[]{11,7,12});
    rules[20] = new Rule(-13, new int[]{11,-14,12});
    rules[21] = new Rule(-13, new int[]{11,12});
    rules[22] = new Rule(-9, new int[]{9,-15,10});
    rules[23] = new Rule(-15, new int[]{-16});
    rules[24] = new Rule(-15, new int[]{-15,5,-16});
    rules[25] = new Rule(-16, new int[]{-7,4,-1});
    rules[26] = new Rule(-8, new int[]{11,-17,12});
    rules[27] = new Rule(-17, new int[]{-1});
    rules[28] = new Rule(-17, new int[]{-17,5,-1});
    rules[29] = new Rule(-14, new int[]{8,4,8});
    rules[30] = new Rule(-14, new int[]{8,4,8,4,8});
    rules[31] = new Rule(-7, new int[]{-18});
    rules[32] = new Rule(-18, new int[]{-19});
    rules[33] = new Rule(-18, new int[]{-20});
    rules[34] = new Rule(-6, new int[]{7});
    rules[35] = new Rule(-19, new int[]{14});
    rules[36] = new Rule(-20, new int[]{16});
    rules[37] = new Rule(-10, new int[]{13});
    rules[38] = new Rule(-11, new int[]{15});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)TokenType.error, (int)TokenType.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // expression -> expression_impl
#line 31 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						OnExpression();
					}
#line default
        break;
      case 11: // sub_expression -> sub_expression_impl
#line 47 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						OnSubExpression();
					}
#line default
        break;
      case 16: // index_expression -> expression, bracket_specifier
#line 60 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("index expression (expression, bracket_specifier): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndexExpression();
					}
#line default
        break;
      case 18: // bracket_specifier -> T_LBRACKET, T_NUMBER, T_RBRACKET
#line 68 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (index): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndex(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 19: // bracket_specifier -> T_LBRACKET, T_STAR, T_RBRACKET
#line 73 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (list wildcard projection).");
						OnListWildcardProjection();
					}
#line default
        break;
      case 21: // bracket_specifier -> T_LBRACKET, T_RBRACKET
#line 79 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (flattening projection).");
						OnFlattenProjection();
					}
#line default
        break;
      case 22: // multi_select_hash -> T_LBRACE, keyval_expressions, T_RBRACE
#line 86 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						PopMultiSelectHash();
					}
#line default
        break;
      case 23: // keyval_expressions -> keyval_expression
#line 91 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						PushMultiSelectHash();
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 24: // keyval_expressions -> keyval_expressions, T_COMMA, keyval_expression
#line 96 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 26: // multi_select_list -> T_LBRACKET, expressions, T_RBRACKET
#line 106 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						PopMultiSelectList();
					}
#line default
        break;
      case 27: // expressions -> expression
#line 112 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						PushMultiSelectList();
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 28: // expressions -> expressions, T_COMMA, expression
#line 117 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 29: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER
#line 123 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier : [{0}:{1}].", ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
						OnSliceExpression(ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token, null);
					}
#line default
        break;
      case 30: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER, T_COLON, T_NUMBER
#line 128 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier : [{0}:{1}:{2}].", ValueStack[ValueStack.Depth-5].Token, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
						OnSliceExpression(ValueStack[ValueStack.Depth-5].Token, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 31: // identifier -> identifier_impl
#line 135 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("identifier ({0}): {1}.", ValueStack[ValueStack.Depth-1].Token.Type, ValueStack[ValueStack.Depth-1].Token);
						OnIdentifier(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 34: // hash_wildcard -> T_STAR
#line 146 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("wildcard (hash wildcard projection): {0}", ValueStack[ValueStack.Depth-1].Token);
						OnHashWildcardProjection();
					}
#line default
        break;
      case 37: // literal -> T_LSTRING
#line 159 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("literal string : {0}", ValueStack[ValueStack.Depth-1].Token);
						OnLiteralString(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 38: // raw_string -> T_RSTRING
#line 165 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("raw string : {0}", ValueStack[ValueStack.Depth-1].Token);
						OnRawString(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((TokenType)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((TokenType)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 171 "C:\Projects\jjme\src\jmespath.net/../shared/JmesPathParser.y"
 #line default
}
}
