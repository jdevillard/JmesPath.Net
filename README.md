# JMESPath.Net

A fully compliant implementation of [JMESPath](http://jmespath.org/specification.html) for .Net Core.

[![Build status](https://ci.appveyor.com/api/projects/status/va3p48ufrj0pxl1t/branch/master?svg=true)](https://ci.appveyor.com/project/jdevillard/jmespath-net/branch/master)
 
# Getting started

## Using the parser

`JMESPath.Net` uses [Newtonsoft.Json](http://www.newtonsoft.com/json) to handle [JSON](http://json.org/)
and comes with a simple to use parser:

```c#
using DevLab.JmesPath;

const string input = @"{ \"foo\": \"bar\" }";
const string expression = "foo";

var jmes = new JmesPath();
var result = jmes.Transform(input, expression);

```

The `JmesPath.Transform` method accepts and produces well formed JSON constructs (object, array or string, boolean, number and null values).
In the example above, the `result` is a JSON string token, including the quotes.

```c#
using Newtonsoft.Json.Linq;

System.Diagnostics.Debug.Assert(result == "\"bar\"");

var token = JToken.Parse(result);
var text = token.ToString();

System.Diagnostics.Debug.Assert(text == "bar");

```
