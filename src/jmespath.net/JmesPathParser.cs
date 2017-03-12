// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 0.1.0.0
// Machine:  DESKTOP-UQ0H65F
// DateTime: 12/03/2017 15:05:31
// Input file <C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y - 12/03/2017 14:39:00>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using StarodubOleg.GPPG.Runtime;

namespace DevLab.JmesPath
{
internal enum TokenType {error=2,EOF=3,T_AND=4,T_OR=5,T_NOT=6,
    T_COLON=7,T_COMMA=8,T_DOT=9,T_PIPE=10,T_EQ=11,T_GT=12,
    T_GE=13,T_LT=14,T_LE=15,T_NE=16,T_FILTER=17,T_FLATTEN=18,
    T_HASHWILDCARD=19,T_LISTWILDCARD=20,T_NUMBER=21,T_LSTRING=22,T_QSTRING=23,T_RSTRING=24,
    T_USTRING=25,T_LBRACE=26,T_RBRACE=27,T_LBRACKET=28,T_RBRACKET=29,T_LPAREN=30,
    T_RPAREN=31};

internal partial struct ValueType
#line 7 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
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
  private static Rule[] rules = new Rule[73];
  private static State[] states = new State[105];
  private static string[] nonTerms = new string[] {
      "expression", "$accept", "expression_impl", "sub_expression", "index_expression", 
      "comparator_expression", "or_expression", "identifier", "and_expression", 
      "not_expression", "paren_expression", "hash_wildcard", "multi_select_list", 
      "multi_select_hash", "literal", "pipe_expression", "function_expression", 
      "raw_string", "sub_expression_impl", "bracket_specifier", "unquoted_string", 
      "arguments", "function_arguments", "slice_expression", "identifier_impl", 
      "quoted_string", "keyval_expressions", "keyval_expression", "expressions", 
      };

  static JmesPathParser() {
    states[0] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,1,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[1] = new State(new int[]{3,2,9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61},new int[]{-20,21});
    states[2] = new State(-1);
    states[3] = new State(new int[]{23,10,25,12,26,13,28,104,19,92},new int[]{-8,4,-14,5,-13,6,-12,7,-25,8,-26,9,-21,11});
    states[4] = new State(-19);
    states[5] = new State(-20);
    states[6] = new State(-21);
    states[7] = new State(-22);
    states[8] = new State(-42);
    states[9] = new State(-43);
    states[10] = new State(-69);
    states[11] = new State(-44);
    states[12] = new State(-70);
    states[13] = new State(new int[]{23,10,25,12},new int[]{-27,14,-28,103,-8,18,-25,8,-26,9,-21,11});
    states[14] = new State(new int[]{27,15,8,16});
    states[15] = new State(-49);
    states[16] = new State(new int[]{23,10,25,12},new int[]{-28,17,-8,18,-25,8,-26,9,-21,11});
    states[17] = new State(-51);
    states[18] = new State(new int[]{7,19});
    states[19] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,20,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[20] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61,27,-52,8,-52},new int[]{-20,21});
    states[21] = new State(-23);
    states[22] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,23,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[23] = new State(new int[]{9,3,11,-35,13,24,12,26,15,28,14,30,16,32,5,-35,4,-35,10,-35,28,40,20,57,17,58,18,61,3,-35,27,-35,8,-35,29,-35,31,-35},new int[]{-20,21});
    states[24] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,25,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[25] = new State(new int[]{9,3,11,-36,13,-36,12,-36,15,28,14,30,16,32,5,-36,4,-36,10,-36,28,40,20,57,17,58,18,61,3,-36,27,-36,8,-36,29,-36,31,-36},new int[]{-20,21});
    states[26] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,27,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[27] = new State(new int[]{9,3,11,-37,13,24,12,-37,15,28,14,30,16,32,5,-37,4,-37,10,-37,28,40,20,57,17,58,18,61,3,-37,27,-37,8,-37,29,-37,31,-37},new int[]{-20,21});
    states[28] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,29,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[29] = new State(new int[]{9,3,11,-38,13,-38,12,-38,15,-38,14,-38,16,32,5,-38,4,-38,10,-38,28,40,20,57,17,58,18,61,3,-38,27,-38,8,-38,29,-38,31,-38},new int[]{-20,21});
    states[30] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,31,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[31] = new State(new int[]{9,3,11,-39,13,-39,12,-39,15,28,14,-39,16,32,5,-39,4,-39,10,-39,28,40,20,57,17,58,18,61,3,-39,27,-39,8,-39,29,-39,31,-39},new int[]{-20,21});
    states[32] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,33,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[33] = new State(new int[]{9,3,11,-40,13,-40,12,-40,15,-40,14,-40,16,-40,5,-40,4,-40,10,-40,28,40,20,57,17,58,18,61,3,-40,27,-40,8,-40,29,-40,31,-40},new int[]{-20,21});
    states[34] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,35,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[35] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,-41,4,36,10,-41,28,40,20,57,17,58,18,61,3,-41,27,-41,8,-41,29,-41,31,-41},new int[]{-20,21});
    states[36] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,37,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[37] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,-45,4,-45,10,-45,28,40,20,57,17,58,18,61,3,-45,27,-45,8,-45,29,-45,31,-45},new int[]{-20,21});
    states[38] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,39,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[39] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,-56,28,40,20,57,17,58,18,61,3,-56,27,-56,8,-56,29,-56,31,-56},new int[]{-20,21});
    states[40] = new State(new int[]{21,41,7,51},new int[]{-24,49});
    states[41] = new State(new int[]{29,42,7,43});
    states[42] = new State(-30);
    states[43] = new State(new int[]{7,44,21,46,29,-58});
    states[44] = new State(new int[]{21,45,29,-59});
    states[45] = new State(-63);
    states[46] = new State(new int[]{7,47,29,-60});
    states[47] = new State(new int[]{21,48,29,-61});
    states[48] = new State(-62);
    states[49] = new State(new int[]{29,50});
    states[50] = new State(-32);
    states[51] = new State(new int[]{21,52,7,55,29,-57});
    states[52] = new State(new int[]{7,53,29,-64});
    states[53] = new State(new int[]{21,54,29,-65});
    states[54] = new State(-66);
    states[55] = new State(new int[]{21,56,29,-68});
    states[56] = new State(-67);
    states[57] = new State(-31);
    states[58] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,59,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[59] = new State(new int[]{29,60,9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61},new int[]{-20,21});
    states[60] = new State(-33);
    states[61] = new State(-34);
    states[62] = new State(-2);
    states[63] = new State(-3);
    states[64] = new State(-18);
    states[65] = new State(-4);
    states[66] = new State(-24);
    states[67] = new State(new int[]{21,41,7,51,28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-24,49,-29,68,-1,102,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[68] = new State(new int[]{29,69,8,70});
    states[69] = new State(-53);
    states[70] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,71,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[71] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61,29,-55,8,-55},new int[]{-20,21});
    states[72] = new State(-5);
    states[73] = new State(-6);
    states[74] = new State(-7);
    states[75] = new State(new int[]{30,77,3,-44,9,-44,11,-44,13,-44,12,-44,15,-44,14,-44,16,-44,5,-44,4,-44,10,-44,28,-44,20,-44,17,-44,18,-44,27,-44,8,-44,29,-44,31,-44},new int[]{-22,76});
    states[76] = new State(-25);
    states[77] = new State(new int[]{31,78,28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-23,79,-1,101,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[78] = new State(-26);
    states[79] = new State(new int[]{31,80,8,81});
    states[80] = new State(-27);
    states[81] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,82,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[82] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61,31,-29,8,-29},new int[]{-20,21});
    states[83] = new State(-8);
    states[84] = new State(-9);
    states[85] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,86,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[86] = new State(new int[]{9,3,11,-46,13,-46,12,-46,15,-46,14,-46,16,-46,5,-46,4,-46,10,-46,28,40,20,57,17,58,18,61,3,-46,27,-46,8,-46,29,-46,31,-46},new int[]{-20,21});
    states[87] = new State(-10);
    states[88] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-1,89,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});
    states[89] = new State(new int[]{31,90,9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61},new int[]{-20,21});
    states[90] = new State(-47);
    states[91] = new State(-11);
    states[92] = new State(-48);
    states[93] = new State(-12);
    states[94] = new State(-13);
    states[95] = new State(-14);
    states[96] = new State(-71);
    states[97] = new State(-15);
    states[98] = new State(-16);
    states[99] = new State(-17);
    states[100] = new State(-72);
    states[101] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61,31,-28,8,-28},new int[]{-20,21});
    states[102] = new State(new int[]{9,3,11,22,13,24,12,26,15,28,14,30,16,32,5,34,4,36,10,38,28,40,20,57,17,58,18,61,29,-54,8,-54},new int[]{-20,21});
    states[103] = new State(-50);
    states[104] = new State(new int[]{28,67,20,57,17,58,18,61,23,10,25,12,6,85,30,88,19,92,26,13,22,96,24,100},new int[]{-29,68,-1,102,-3,62,-4,63,-19,64,-5,65,-20,66,-6,72,-7,73,-8,74,-25,8,-26,9,-21,75,-9,83,-10,84,-11,87,-12,91,-13,93,-14,94,-15,95,-16,97,-17,98,-18,99});

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
    rules[11] = new Rule(-3, new int[]{-12});
    rules[12] = new Rule(-3, new int[]{-13});
    rules[13] = new Rule(-3, new int[]{-14});
    rules[14] = new Rule(-3, new int[]{-15});
    rules[15] = new Rule(-3, new int[]{-16});
    rules[16] = new Rule(-3, new int[]{-17});
    rules[17] = new Rule(-3, new int[]{-18});
    rules[18] = new Rule(-4, new int[]{-19});
    rules[19] = new Rule(-19, new int[]{-1,9,-8});
    rules[20] = new Rule(-19, new int[]{-1,9,-14});
    rules[21] = new Rule(-19, new int[]{-1,9,-13});
    rules[22] = new Rule(-19, new int[]{-1,9,-12});
    rules[23] = new Rule(-5, new int[]{-1,-20});
    rules[24] = new Rule(-5, new int[]{-20});
    rules[25] = new Rule(-17, new int[]{-21,-22});
    rules[26] = new Rule(-22, new int[]{30,31});
    rules[27] = new Rule(-22, new int[]{30,-23,31});
    rules[28] = new Rule(-23, new int[]{-1});
    rules[29] = new Rule(-23, new int[]{-23,8,-1});
    rules[30] = new Rule(-20, new int[]{28,21,29});
    rules[31] = new Rule(-20, new int[]{20});
    rules[32] = new Rule(-20, new int[]{28,-24,29});
    rules[33] = new Rule(-20, new int[]{17,-1,29});
    rules[34] = new Rule(-20, new int[]{18});
    rules[35] = new Rule(-6, new int[]{-1,11,-1});
    rules[36] = new Rule(-6, new int[]{-1,13,-1});
    rules[37] = new Rule(-6, new int[]{-1,12,-1});
    rules[38] = new Rule(-6, new int[]{-1,15,-1});
    rules[39] = new Rule(-6, new int[]{-1,14,-1});
    rules[40] = new Rule(-6, new int[]{-1,16,-1});
    rules[41] = new Rule(-7, new int[]{-1,5,-1});
    rules[42] = new Rule(-8, new int[]{-25});
    rules[43] = new Rule(-25, new int[]{-26});
    rules[44] = new Rule(-25, new int[]{-21});
    rules[45] = new Rule(-9, new int[]{-1,4,-1});
    rules[46] = new Rule(-10, new int[]{6,-1});
    rules[47] = new Rule(-11, new int[]{30,-1,31});
    rules[48] = new Rule(-12, new int[]{19});
    rules[49] = new Rule(-14, new int[]{26,-27,27});
    rules[50] = new Rule(-27, new int[]{-28});
    rules[51] = new Rule(-27, new int[]{-27,8,-28});
    rules[52] = new Rule(-28, new int[]{-8,7,-1});
    rules[53] = new Rule(-13, new int[]{28,-29,29});
    rules[54] = new Rule(-29, new int[]{-1});
    rules[55] = new Rule(-29, new int[]{-29,8,-1});
    rules[56] = new Rule(-16, new int[]{-1,10,-1});
    rules[57] = new Rule(-24, new int[]{7});
    rules[58] = new Rule(-24, new int[]{21,7});
    rules[59] = new Rule(-24, new int[]{21,7,7});
    rules[60] = new Rule(-24, new int[]{21,7,21});
    rules[61] = new Rule(-24, new int[]{21,7,21,7});
    rules[62] = new Rule(-24, new int[]{21,7,21,7,21});
    rules[63] = new Rule(-24, new int[]{21,7,7,21});
    rules[64] = new Rule(-24, new int[]{7,21});
    rules[65] = new Rule(-24, new int[]{7,21,7});
    rules[66] = new Rule(-24, new int[]{7,21,7,21});
    rules[67] = new Rule(-24, new int[]{7,7,21});
    rules[68] = new Rule(-24, new int[]{7,7});
    rules[69] = new Rule(-26, new int[]{23});
    rules[70] = new Rule(-21, new int[]{25});
    rules[71] = new Rule(-15, new int[]{22});
    rules[72] = new Rule(-18, new int[]{24});
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
#line 70 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnExpression();
					}
#line default
        break;
      case 18: // sub_expression -> sub_expression_impl
#line 93 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSubExpression();
					}
#line default
        break;
      case 23: // index_expression -> expression, bracket_specifier
#line 106 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("index expression (expression, bracket_specifier): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndexExpression();
					}
#line default
        break;
      case 25: // function_expression -> unquoted_string, arguments
#line 114 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PopFunction(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 26: // arguments -> T_LPAREN, T_RPAREN
#line 120 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PushFunction();
					}
#line default
        break;
      case 28: // function_arguments -> expression
#line 127 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PushFunction();
						AddFunctionArg();
					}
#line default
        break;
      case 29: // function_arguments -> function_arguments, T_COMMA, expression
#line 132 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						AddFunctionArg();
					}
#line default
        break;
      case 30: // bracket_specifier -> T_LBRACKET, T_NUMBER, T_RBRACKET
#line 138 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (index): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndex(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 31: // bracket_specifier -> T_LISTWILDCARD
#line 143 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (list wildcard projection).");
						OnListWildcardProjection();
					}
#line default
        break;
      case 33: // bracket_specifier -> T_FILTER, expression, T_RBRACKET
#line 149 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnFilterExpression();
					}
#line default
        break;
      case 34: // bracket_specifier -> T_FLATTEN
#line 153 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (flattening projection).");
						OnFlattenProjection();
					}
#line default
        break;
      case 35: // comparator_expression -> expression, T_EQ, expression
#line 161 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 36: // comparator_expression -> expression, T_GE, expression
#line 165 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 37: // comparator_expression -> expression, T_GT, expression
#line 169 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 38: // comparator_expression -> expression, T_LE, expression
#line 173 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 39: // comparator_expression -> expression, T_LT, expression
#line 177 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 40: // comparator_expression -> expression, T_NE, expression
#line 181 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 41: // or_expression -> expression, T_OR, expression
#line 187 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnOrExpression();
					}
#line default
        break;
      case 42: // identifier -> identifier_impl
#line 193 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("identifier ({0}): {1}.", ValueStack[ValueStack.Depth-1].Token.Type, ValueStack[ValueStack.Depth-1].Token);
						OnIdentifier(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 45: // and_expression -> expression, T_AND, expression
#line 204 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnAndExpression();
					}
#line default
        break;
      case 46: // not_expression -> T_NOT, expression
#line 210 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnNotExpression();
					}
#line default
        break;
      case 48: // hash_wildcard -> T_HASHWILDCARD
#line 219 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("wildcard (hash wildcard projection): {0}", ValueStack[ValueStack.Depth-1].Token);
						OnHashWildcardProjection();
					}
#line default
        break;
      case 49: // multi_select_hash -> T_LBRACE, keyval_expressions, T_RBRACE
#line 226 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PopMultiSelectHash();
					}
#line default
        break;
      case 50: // keyval_expressions -> keyval_expression
#line 231 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PushMultiSelectHash();
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 51: // keyval_expressions -> keyval_expressions, T_COMMA, keyval_expression
#line 236 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 53: // multi_select_list -> T_LBRACKET, expressions, T_RBRACKET
#line 246 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PopMultiSelectList();
					}
#line default
        break;
      case 54: // expressions -> expression
#line 252 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						PushMultiSelectList();
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 55: // expressions -> expressions, T_COMMA, expression
#line 257 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 56: // pipe_expression -> expression, T_PIPE, expression
#line 263 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnPipeExpression();
					}
#line default
        break;
      case 57: // slice_expression -> T_COLON
#line 269 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, null, null);
					}
#line default
        break;
      case 58: // slice_expression -> T_NUMBER, T_COLON
#line 273 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-2].Token, null, null);
					}
#line default
        break;
      case 59: // slice_expression -> T_NUMBER, T_COLON, T_COLON
#line 277 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-3].Token, null, null);
					}
#line default
        break;
      case 60: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER
#line 281 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token, null);
					}
#line default
        break;
      case 61: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER, T_COLON
#line 285 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-4].Token, ValueStack[ValueStack.Depth-2].Token, null);
					}
#line default
        break;
      case 62: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER, T_COLON, T_NUMBER
#line 289 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-5].Token, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 63: // slice_expression -> T_NUMBER, T_COLON, T_COLON, T_NUMBER
#line 293 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-4].Token, null, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 64: // slice_expression -> T_COLON, T_NUMBER
#line 297 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-1].Token, null);
					}
#line default
        break;
      case 65: // slice_expression -> T_COLON, T_NUMBER, T_COLON
#line 301 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-2].Token, null);
					}
#line default
        break;
      case 66: // slice_expression -> T_COLON, T_NUMBER, T_COLON, T_NUMBER
#line 305 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 67: // slice_expression -> T_COLON, T_COLON, T_NUMBER
#line 309 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, null, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 68: // slice_expression -> T_COLON, T_COLON
#line 313 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						OnSliceExpression(null, null, null);
					}
#line default
        break;
      case 71: // literal -> T_LSTRING
#line 325 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("literal string : {0}", ValueStack[ValueStack.Depth-1].Token);
						OnLiteralString(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 72: // raw_string -> T_RSTRING
#line 331 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
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

#line 337 "C:\Projects\jmespath\jjme\src\jmespath.net/JmesPathParser.y"
 #line default
}
}
