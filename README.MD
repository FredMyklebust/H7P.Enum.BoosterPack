### What is Enum Boosterpack?

Enum Boosterpack is a set of source generators which creates fast extension methods for your enums.
* AsString() - ToString() on enum-values is slow, AsString() is more than 17x faster on .NET 5, and more than 200x faster on .NET 4.8.
* AutoDescriptor - Creates a GetDescription() extension method that returns the value from enum-values decorated with a Description-attribute, which is more than 950x times faster on .NET 5.0, and more than 1650x faster on .NET 4.8 than using reflection.

Unlike ToString() and extracting the description-value using reflection, AsString() and GetDescription() doesn't allocate.


### Limitations

The following limitations apply:
* No support for flags yet
* Supports public and internal enums only


### How do I get started?

Enum Boosterpack will automatically identify and create AsString() extension methods for your public and internal enums. Declare an enum:

```csharp
internal enum Color
{
    Red,
    Green
}   
```
Then in your application code, call AsString() instead of ToString() on your enum-value:

```csharp
var redString = Color.Red.AsString();
```

To generate GetDescription() extension methods, add a Describable-attribute to your enums. Make sure all enum-values have a Description-attribute:
```csharp
using H7P.AutoDescriptor;

[Describable]
internal enum Color
{
    [Description("Red as a rose")]
    Red,

    [Description("Green as the grass")]
    Green
}   
```
Then in your application code, call GetDescription() on the enum-value:

```csharp
var redDescription = Color.Red.GetDescription();
```


### Performance

To verify the performance claims, run the provided benchmarks in the project "H7P.Enum.BoosterPack.Benchmarks".

The benchmarks use [BenchmarkDotNet](https://benchmarkdotnet.org/ "BenchmarkDotnet's Homepage"), and require that you compile the benchmark-project in release-mode and run them from the command line.

If you have any suggestions on how to improve the performance even further, please file an issue with some example code, or even better open a pull request.


### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Enum Boosterpack](https://www.nuget.org/packages/H7P.Enum.Boosterpack/) from the package manager console:

```
PM> Install-Package H7P.Enum.Boosterpack
```

### Do you have an issue?

File an issue above.

### License, etc.

Enum Boosterpack is Copyright &copy; 2021 Fred Myklebust and other contributors under the [MIT license](LICENSE.txt).
