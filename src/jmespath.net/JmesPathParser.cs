// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// Input file <JmesPathParser.y - 13/06/2020 15:54:57>

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
    T_STAR=19,T_CURRENT=20,T_ETYPE=21,T_NUMBER=22,T_LSTRING=23,T_QSTRING=24,
    T_RSTRING=25,T_USTRING=26,T_LBRACE=27,T_RBRACE=28,T_LBRACKET=29,T_RBRACKET=30,
    T_LPAREN=31,T_RPAREN=32,T_LISTWILDCARD=33};

internal partial struct ValueType
#line 7 "JmesPathParser.y"
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
  private static Rule[] rules = new Rule[79];
  private static State[] states = new State[114];
  private static string[] nonTerms = new string[] {
      "expression", "$accept", "expression_impl", "sub_expression", "index_expression", 
      "comparator_expression", "or_expression", "identifier", "and_expression", 
      "not_expression", "paren_expression", "hash_wildcard", "multi_select_list", 
      "multi_select_hash", "literal", "pipe_expression", "function_expression", 
      "raw_string", "current_node", "sub_expression_impl", "bracket_specifier", 
      "unquoted_string", "arguments", "function_arguments", "expression_type", 
      "slice_expression", "identifier_impl", "quoted_string", "keyval_expressions", 
      "keyval_expression", "expressions", };

  static JmesPathParser() {
    states[0] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,1,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[1] = new State(new int[]{3,2,9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61},new int[]{-21,20});
    states[2] = new State(-1);
    states[3] = new State(new int[]{24,11,26,76,27,89,29,113,19,86},new int[]{-8,4,-14,5,-13,6,-17,7,-12,8,-27,9,-28,10,-22,12});
    states[4] = new State(-20);
    states[5] = new State(-21);
    states[6] = new State(-22);
    states[7] = new State(-23);
    states[8] = new State(-24);
    states[9] = new State(-48);
    states[10] = new State(-49);
    states[11] = new State(-75);
    states[12] = new State(new int[]{31,14,3,-50,9,-50,11,-50,13,-50,12,-50,15,-50,14,-50,16,-50,5,-50,4,-50,10,-50,29,-50,17,-50,18,-50,32,-50,8,-50,30,-50,28,-50},new int[]{-23,13});
    states[13] = new State(-27);
    states[14] = new State(new int[]{32,15,29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104,21,109},new int[]{-24,16,-1,111,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103,-25,112});
    states[15] = new State(-28);
    states[16] = new State(new int[]{32,17,8,18});
    states[17] = new State(-29);
    states[18] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104,21,109},new int[]{-1,19,-25,108,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[19] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,32,-32,8,-32},new int[]{-21,20});
    states[20] = new State(-25);
    states[21] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,22,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[22] = new State(new int[]{9,3,11,-41,13,23,12,25,15,27,14,29,16,31,5,-41,4,-41,10,-41,29,39,17,58,18,61,3,-41,32,-41,8,-41,30,-41,28,-41},new int[]{-21,20});
    states[23] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,24,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[24] = new State(new int[]{9,3,11,-42,13,-42,12,-42,15,27,14,29,16,31,5,-42,4,-42,10,-42,29,39,17,58,18,61,3,-42,32,-42,8,-42,30,-42,28,-42},new int[]{-21,20});
    states[25] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,26,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[26] = new State(new int[]{9,3,11,-43,13,23,12,-43,15,27,14,29,16,31,5,-43,4,-43,10,-43,29,39,17,58,18,61,3,-43,32,-43,8,-43,30,-43,28,-43},new int[]{-21,20});
    states[27] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,28,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[28] = new State(new int[]{9,3,11,-44,13,-44,12,-44,15,-44,14,-44,16,31,5,-44,4,-44,10,-44,29,39,17,58,18,61,3,-44,32,-44,8,-44,30,-44,28,-44},new int[]{-21,20});
    states[29] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,30,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[30] = new State(new int[]{9,3,11,-45,13,-45,12,-45,15,27,14,-45,16,31,5,-45,4,-45,10,-45,29,39,17,58,18,61,3,-45,32,-45,8,-45,30,-45,28,-45},new int[]{-21,20});
    states[31] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,32,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[32] = new State(new int[]{9,3,11,-46,13,-46,12,-46,15,-46,14,-46,16,-46,5,-46,4,-46,10,-46,29,39,17,58,18,61,3,-46,32,-46,8,-46,30,-46,28,-46},new int[]{-21,20});
    states[33] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,34,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[34] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,-47,4,35,10,-47,29,39,17,58,18,61,3,-47,32,-47,8,-47,30,-47,28,-47},new int[]{-21,20});
    states[35] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,36,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[36] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,-51,4,-51,10,-51,29,39,17,58,18,61,3,-51,32,-51,8,-51,30,-51,28,-51},new int[]{-21,20});
    states[37] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,38,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[38] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,-62,29,39,17,58,18,61,3,-62,32,-62,8,-62,30,-62,28,-62},new int[]{-21,20});
    states[39] = new State(new int[]{22,40,19,48,7,52},new int[]{-26,50});
    states[40] = new State(new int[]{30,41,7,42});
    states[41] = new State(-36);
    states[42] = new State(new int[]{7,43,22,45,30,-64});
    states[43] = new State(new int[]{22,44,30,-65});
    states[44] = new State(-69);
    states[45] = new State(new int[]{7,46,30,-66});
    states[46] = new State(new int[]{22,47,30,-67});
    states[47] = new State(-68);
    states[48] = new State(new int[]{30,49});
    states[49] = new State(-37);
    states[50] = new State(new int[]{30,51});
    states[51] = new State(-38);
    states[52] = new State(new int[]{22,53,7,56,30,-63});
    states[53] = new State(new int[]{7,54,30,-70});
    states[54] = new State(new int[]{22,55,30,-71});
    states[55] = new State(-72);
    states[56] = new State(new int[]{22,57,30,-74});
    states[57] = new State(-73);
    states[58] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,59,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[59] = new State(new int[]{30,60,9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61},new int[]{-21,20});
    states[60] = new State(-39);
    states[61] = new State(-40);
    states[62] = new State(-2);
    states[63] = new State(-3);
    states[64] = new State(-19);
    states[65] = new State(-4);
    states[66] = new State(-26);
    states[67] = new State(new int[]{22,40,19,68,7,52,29,67,17,58,18,61,24,11,26,76,6,79,31,82,27,89,23,98,25,102,20,104},new int[]{-26,50,-31,69,-1,107,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[68] = new State(new int[]{30,49,9,-54,11,-54,13,-54,12,-54,15,-54,14,-54,16,-54,5,-54,4,-54,10,-54,29,-54,17,-54,18,-54,8,-54});
    states[69] = new State(new int[]{30,70,8,71});
    states[70] = new State(-59);
    states[71] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,72,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[72] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,30,-61,8,-61},new int[]{-21,20});
    states[73] = new State(-5);
    states[74] = new State(-6);
    states[75] = new State(-7);
    states[76] = new State(-76);
    states[77] = new State(-8);
    states[78] = new State(-9);
    states[79] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,80,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[80] = new State(new int[]{9,3,11,-52,13,-52,12,-52,15,-52,14,-52,16,-52,5,-52,4,-52,10,-52,29,39,17,58,18,61,3,-52,32,-52,8,-52,30,-52,28,-52},new int[]{-21,20});
    states[81] = new State(-10);
    states[82] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,83,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[83] = new State(new int[]{32,84,9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61},new int[]{-21,20});
    states[84] = new State(-53);
    states[85] = new State(-11);
    states[86] = new State(-54);
    states[87] = new State(-12);
    states[88] = new State(-13);
    states[89] = new State(new int[]{24,11,26,76},new int[]{-29,90,-30,106,-8,94,-27,9,-28,10,-22,105});
    states[90] = new State(new int[]{28,91,8,92});
    states[91] = new State(-55);
    states[92] = new State(new int[]{24,11,26,76},new int[]{-30,93,-8,94,-27,9,-28,10,-22,105});
    states[93] = new State(-57);
    states[94] = new State(new int[]{7,95});
    states[95] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,96,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[96] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,28,-58,8,-58},new int[]{-21,20});
    states[97] = new State(-14);
    states[98] = new State(-77);
    states[99] = new State(-15);
    states[100] = new State(-16);
    states[101] = new State(-17);
    states[102] = new State(-78);
    states[103] = new State(-18);
    states[104] = new State(-34);
    states[105] = new State(-50);
    states[106] = new State(-56);
    states[107] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,30,-60,8,-60},new int[]{-21,20});
    states[108] = new State(-33);
    states[109] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-1,110,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});
    states[110] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,32,-35,8,-35},new int[]{-21,20});
    states[111] = new State(new int[]{9,3,11,21,13,23,12,25,15,27,14,29,16,31,5,33,4,35,10,37,29,39,17,58,18,61,32,-30,8,-30},new int[]{-21,20});
    states[112] = new State(-31);
    states[113] = new State(new int[]{29,67,17,58,18,61,24,11,26,76,6,79,31,82,19,86,27,89,23,98,25,102,20,104},new int[]{-31,69,-1,107,-3,62,-4,63,-20,64,-5,65,-21,66,-6,73,-7,74,-8,75,-27,9,-28,10,-22,12,-9,77,-10,78,-11,81,-12,85,-13,87,-14,88,-15,97,-16,99,-17,100,-18,101,-19,103});

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
    rules[18] = new Rule(-3, new int[]{-19});
    rules[19] = new Rule(-4, new int[]{-20});
    rules[20] = new Rule(-20, new int[]{-1,9,-8});
    rules[21] = new Rule(-20, new int[]{-1,9,-14});
    rules[22] = new Rule(-20, new int[]{-1,9,-13});
    rules[23] = new Rule(-20, new int[]{-1,9,-17});
    rules[24] = new Rule(-20, new int[]{-1,9,-12});
    rules[25] = new Rule(-5, new int[]{-1,-21});
    rules[26] = new Rule(-5, new int[]{-21});
    rules[27] = new Rule(-17, new int[]{-22,-23});
    rules[28] = new Rule(-23, new int[]{31,32});
    rules[29] = new Rule(-23, new int[]{31,-24,32});
    rules[30] = new Rule(-24, new int[]{-1});
    rules[31] = new Rule(-24, new int[]{-25});
    rules[32] = new Rule(-24, new int[]{-24,8,-1});
    rules[33] = new Rule(-24, new int[]{-24,8,-25});
    rules[34] = new Rule(-19, new int[]{20});
    rules[35] = new Rule(-25, new int[]{21,-1});
    rules[36] = new Rule(-21, new int[]{29,22,30});
    rules[37] = new Rule(-21, new int[]{29,19,30});
    rules[38] = new Rule(-21, new int[]{29,-26,30});
    rules[39] = new Rule(-21, new int[]{17,-1,30});
    rules[40] = new Rule(-21, new int[]{18});
    rules[41] = new Rule(-6, new int[]{-1,11,-1});
    rules[42] = new Rule(-6, new int[]{-1,13,-1});
    rules[43] = new Rule(-6, new int[]{-1,12,-1});
    rules[44] = new Rule(-6, new int[]{-1,15,-1});
    rules[45] = new Rule(-6, new int[]{-1,14,-1});
    rules[46] = new Rule(-6, new int[]{-1,16,-1});
    rules[47] = new Rule(-7, new int[]{-1,5,-1});
    rules[48] = new Rule(-8, new int[]{-27});
    rules[49] = new Rule(-27, new int[]{-28});
    rules[50] = new Rule(-27, new int[]{-22});
    rules[51] = new Rule(-9, new int[]{-1,4,-1});
    rules[52] = new Rule(-10, new int[]{6,-1});
    rules[53] = new Rule(-11, new int[]{31,-1,32});
    rules[54] = new Rule(-12, new int[]{19});
    rules[55] = new Rule(-14, new int[]{27,-29,28});
    rules[56] = new Rule(-29, new int[]{-30});
    rules[57] = new Rule(-29, new int[]{-29,8,-30});
    rules[58] = new Rule(-30, new int[]{-8,7,-1});
    rules[59] = new Rule(-13, new int[]{29,-31,30});
    rules[60] = new Rule(-31, new int[]{-1});
    rules[61] = new Rule(-31, new int[]{-31,8,-1});
    rules[62] = new Rule(-16, new int[]{-1,10,-1});
    rules[63] = new Rule(-26, new int[]{7});
    rules[64] = new Rule(-26, new int[]{22,7});
    rules[65] = new Rule(-26, new int[]{22,7,7});
    rules[66] = new Rule(-26, new int[]{22,7,22});
    rules[67] = new Rule(-26, new int[]{22,7,22,7});
    rules[68] = new Rule(-26, new int[]{22,7,22,7,22});
    rules[69] = new Rule(-26, new int[]{22,7,7,22});
    rules[70] = new Rule(-26, new int[]{7,22});
    rules[71] = new Rule(-26, new int[]{7,22,7});
    rules[72] = new Rule(-26, new int[]{7,22,7,22});
    rules[73] = new Rule(-26, new int[]{7,7,22});
    rules[74] = new Rule(-26, new int[]{7,7});
    rules[75] = new Rule(-28, new int[]{24});
    rules[76] = new Rule(-22, new int[]{26});
    rules[77] = new Rule(-15, new int[]{23});
    rules[78] = new Rule(-18, new int[]{25});
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
#line 71 "JmesPathParser.y"
     {
						OnExpression();
						ResolveParsingState();
					}
#line default
        break;
      case 19: // sub_expression -> sub_expression_impl
#line 96 "JmesPathParser.y"
     {
						OnSubExpression();
					}
#line default
        break;
      case 25: // index_expression -> expression, bracket_specifier
#line 110 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("index expression (expression, bracket_specifier): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndexExpression();
					}
#line default
        break;
      case 27: // function_expression -> unquoted_string, arguments
#line 118 "JmesPathParser.y"
     {
						PopFunction(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 28: // arguments -> T_LPAREN, T_RPAREN
#line 124 "JmesPathParser.y"
     {
						PushFunction();
					}
#line default
        break;
      case 30: // function_arguments -> expression
#line 131 "JmesPathParser.y"
     {
						PushFunction();
						AddFunctionArg();
					}
#line default
        break;
      case 31: // function_arguments -> expression_type
#line 136 "JmesPathParser.y"
     {
						PushFunction();
						AddFunctionArg();
					}
#line default
        break;
      case 32: // function_arguments -> function_arguments, T_COMMA, expression
#line 141 "JmesPathParser.y"
     {
						AddFunctionArg();
					}
#line default
        break;
      case 33: // function_arguments -> function_arguments, T_COMMA, expression_type
#line 145 "JmesPathParser.y"
     {
						AddFunctionArg();
					}
#line default
        break;
      case 34: // current_node -> T_CURRENT
#line 151 "JmesPathParser.y"
     {
						OnCurrentNode();
					}
#line default
        break;
      case 35: // expression_type -> T_ETYPE, expression
#line 156 "JmesPathParser.y"
     {
						OnExpressionType();
					}
#line default
        break;
      case 36: // bracket_specifier -> T_LBRACKET, T_NUMBER, T_RBRACKET
#line 162 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (index): {0}.", ValueStack[ValueStack.Depth-2].Token);
						OnIndex(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 37: // bracket_specifier -> T_LBRACKET, T_STAR, T_RBRACKET
#line 167 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (list wildcard projection).");
						OnListWildcardProjection();
					}
#line default
        break;
      case 39: // bracket_specifier -> T_FILTER, expression, T_RBRACKET
#line 173 "JmesPathParser.y"
     {
						OnFilterProjection();
					}
#line default
        break;
      case 40: // bracket_specifier -> T_FLATTEN
#line 177 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("bracket_specifier (flattening projection).");
						OnFlattenProjection();
					}
#line default
        break;
      case 41: // comparator_expression -> expression, T_EQ, expression
#line 185 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 42: // comparator_expression -> expression, T_GE, expression
#line 189 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 43: // comparator_expression -> expression, T_GT, expression
#line 193 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 44: // comparator_expression -> expression, T_LE, expression
#line 197 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 45: // comparator_expression -> expression, T_LT, expression
#line 201 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 46: // comparator_expression -> expression, T_NE, expression
#line 205 "JmesPathParser.y"
     {
						OnComparisonExpression(ValueStack[ValueStack.Depth-2].Token);
					}
#line default
        break;
      case 47: // or_expression -> expression, T_OR, expression
#line 211 "JmesPathParser.y"
     {
						OnOrExpression();
					}
#line default
        break;
      case 48: // identifier -> identifier_impl
#line 217 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("identifier ({0}): {1}.", ValueStack[ValueStack.Depth-1].Token.Type, ValueStack[ValueStack.Depth-1].Token);
						OnIdentifier(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 51: // and_expression -> expression, T_AND, expression
#line 228 "JmesPathParser.y"
     {
						OnAndExpression();
					}
#line default
        break;
      case 52: // not_expression -> T_NOT, expression
#line 234 "JmesPathParser.y"
     {
						OnNotExpression();
					}
#line default
        break;
      case 54: // hash_wildcard -> T_STAR
#line 243 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("wildcard (hash wildcard projection): {0}", ValueStack[ValueStack.Depth-1].Token);
						OnHashWildcardProjection();
					}
#line default
        break;
      case 55: // multi_select_hash -> T_LBRACE, keyval_expressions, T_RBRACE
#line 250 "JmesPathParser.y"
     {
						PopMultiSelectHash();
					}
#line default
        break;
      case 56: // keyval_expressions -> keyval_expression
#line 255 "JmesPathParser.y"
     {
						PushMultiSelectHash();
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 57: // keyval_expressions -> keyval_expressions, T_COMMA, keyval_expression
#line 260 "JmesPathParser.y"
     {
						AddMultiSelectHashExpression();
					}
#line default
        break;
      case 59: // multi_select_list -> T_LBRACKET, expressions, T_RBRACKET
#line 270 "JmesPathParser.y"
     {
						PopMultiSelectList();
					}
#line default
        break;
      case 60: // expressions -> expression
#line 276 "JmesPathParser.y"
     {
						PushMultiSelectList();
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 61: // expressions -> expressions, T_COMMA, expression
#line 281 "JmesPathParser.y"
     {
						AddMultiSelectListExpression();
					}
#line default
        break;
      case 62: // pipe_expression -> expression, T_PIPE, expression
#line 287 "JmesPathParser.y"
     {
						OnPipeExpression();
					}
#line default
        break;
      case 63: // slice_expression -> T_COLON
#line 293 "JmesPathParser.y"
     {
						OnSliceExpression(null, null, null);
					}
#line default
        break;
      case 64: // slice_expression -> T_NUMBER, T_COLON
#line 297 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-2].Token, null, null);
					}
#line default
        break;
      case 65: // slice_expression -> T_NUMBER, T_COLON, T_COLON
#line 301 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-3].Token, null, null);
					}
#line default
        break;
      case 66: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER
#line 305 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token, null);
					}
#line default
        break;
      case 67: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER, T_COLON
#line 309 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-4].Token, ValueStack[ValueStack.Depth-2].Token, null);
					}
#line default
        break;
      case 68: // slice_expression -> T_NUMBER, T_COLON, T_NUMBER, T_COLON, T_NUMBER
#line 313 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-5].Token, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 69: // slice_expression -> T_NUMBER, T_COLON, T_COLON, T_NUMBER
#line 317 "JmesPathParser.y"
     {
						OnSliceExpression(ValueStack[ValueStack.Depth-4].Token, null, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 70: // slice_expression -> T_COLON, T_NUMBER
#line 321 "JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-1].Token, null);
					}
#line default
        break;
      case 71: // slice_expression -> T_COLON, T_NUMBER, T_COLON
#line 325 "JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-2].Token, null);
					}
#line default
        break;
      case 72: // slice_expression -> T_COLON, T_NUMBER, T_COLON, T_NUMBER
#line 329 "JmesPathParser.y"
     {
						OnSliceExpression(null, ValueStack[ValueStack.Depth-3].Token, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 73: // slice_expression -> T_COLON, T_COLON, T_NUMBER
#line 333 "JmesPathParser.y"
     {
						OnSliceExpression(null, null, ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 74: // slice_expression -> T_COLON, T_COLON
#line 337 "JmesPathParser.y"
     {
						OnSliceExpression(null, null, null);
					}
#line default
        break;
      case 77: // literal -> T_LSTRING
#line 349 "JmesPathParser.y"
     {
						System.Diagnostics.Debug.WriteLine("literal string : {0}", ValueStack[ValueStack.Depth-1].Token);
						OnLiteralString(ValueStack[ValueStack.Depth-1].Token);
					}
#line default
        break;
      case 78: // raw_string -> T_RSTRING
#line 355 "JmesPathParser.y"
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

#line 361 "JmesPathParser.y"
 #line default
}
}
