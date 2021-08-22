### What is Enum Boosterpack?

Enum Boosterpack is a set of sourcegenerators which creates extension methods for your enums.
* ToFastString - ToString() on enum-values is slow, ToFastString() is about 10x faster.
* AutoDescriptor - Creates a GetDescription() extension method that returns the value from enum-values decorated with a Description-attribute, about 30x times faster than reflection.

### Limitations
As it is early days, the following limitations apply:
* C# 8 or later
* Supports public and internal enums
* No (current) support for flags, but planned

### How do I get started?

Enum Boosterpack will automatically identify and create ToFastString() extension methods for your public and internal enums:

```csharp
    internal enum Color
    {
        Red,
        Green
    }   
```
Then in your application code, call ToFastString() instead of ToString() on your enum-value:

```csharp
var redString = Color.Red.ToFastString();
```

To generate GetDescription() extension methods, add a Describable-attribute to your enums:
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

### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Enum Boosterpack](https://www.nuget.org/packages/H7P.Enum.Boosterpack/) from the package manager console:

```
PM> Install-Package H7P.Enum.Boosterpack
```

### Do you have an issue?

File an issue above.

### License, etc.

Enum Boosterpack is Copyright &copy; 2021 Fred Myklebust and other contributors under the [MIT license](LICENSE.txt).
