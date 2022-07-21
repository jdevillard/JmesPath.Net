using DevLab.JmesPath;

if (args.Length == 0)
{
    Console.Error.WriteLine("Missing required JMESPath expression.");
    Environment.Exit(42);
}

var expression = args[0];

var stdin = Console.In;
var stdout = Console.Out;

var text = stdin.ReadToEnd();

var jp = new JmesPath();
var token = jp.Transform(text, expression);

stdout.WriteLine(token);
