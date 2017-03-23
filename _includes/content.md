# JmesPath.Net
A fully compliant implementation of [JMESPATH](http://jmespath.org/specification.html) for .NetCore

# Getting started

## Using the parser

*JmesPath.Net* uses [Newtonsoft.Json](http://www.newtonsoft.com/json) to handle [JSON](http://json.org/) and comes with a simple to use parser:

```c#
using DevLab.JmesPath;

const string input = @"{ \"foo\": \"bar\" }";
const string expression = "foo";

var jmes = new JmesPath();
var result = jmes.Transform(input, expression);
```

The *JmesPath.Transform* method accepts and produces well formed JSON constructs (object, array or string, boolean, number and null values). In the example above, the *result* is a JSON string token, including the quotes.

```c#
using Newtonsoft.Json.Linq;

System.Diagnostics.Debug.Assert(result == "\"bar\"");

var token = JToken.Parse(result);
var text = token.ToString();

System.Diagnostics.Debug.Assert(text == "bar");
```

## Using functions
*JmesPath.Net* supports the specified [builtin functions](http://jmespath.org/specification.html#built-in-functions). Additionaly, the library supports registering custom functions if required.

First, create an assembly and declare a new function:
```c#
using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Utils;
using Newtonsof.Json.Linq;

public sealed class SubstringFunction : JmesPathFunction
{
    public SubstringFunction()
        : base("substring", 2)
    {
    }

    public override void Validate(params JmesPathFunctionArgument[] arguments)
    {
        base.Validate();

        if (arguments.Length != 3)
            throw new Exception("Error: invalid-arity, the substring expects three arguments");

        // using .GetTokenType() extension method
        // to recognize all "string" types
        // (string, guid, date time, timespan, uri, bytes)

        if (arguments[0].Token.GetTokenType() != "string")
            throw new Exception("Error: invalid-value, the substring function expects its first argument to be a string.");

        // using .Type to distinguish integers from floating point values

        if (arguments[1].Token.Type != JTokenType.Integer || arguments[2].Token.Type() != JTokenType.Integer)
            throw new Exception("Error: invalid-value, the substring function expects its second and third arguments to be integers.");
    }

    public override JToken Execute(params JmesPathFunctionArgument[] arguments)
    {
        var text = arguments[0].Token.Value<string>();
        var index = arguments[0].Token.Value<int>();
        var length = arguments[0].Token.Value<int>();

        var sub = text.Substring(index, length);
        
        return new JValue(sub);
    }
}
```
Then, register and use the custom function as you would normally:


```c#
using DevLab.JmesPath;

const string input = "{\"foo\": \"bar\"}";
const string expression = "foo.substring(@, 1, 2)";

var jmes = new JmesPath();
jmes.FuntionRepository
    .Register<SubstringFunction>()
    ;

var result = jmes.Transform(input, expression);

System.Diagnostics.Debug.Assert(result == "\"ba\"");
```