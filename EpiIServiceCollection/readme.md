# Episerver IServiceCollection
Demonstrates the ability to use Microsoft's dependency injection IServiceCollection with an Episerver IConfigurableModule during Episerver intialization.

NuGet Packages required:
* [EPiserver.Framework](https://nuget.episerver.com/package/?id=EPiServer.Framework&v=11.1.0) >= 11.1.0
* [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/2.1.0) >= 2.1

## Example

```cs
// assembly attribute to run before Episerver Initialization
[assembly: System.Web.PreApplicationStartMethod(typeof(ExampleClass), nameof(ExampleClass.Init))]

public class ExampleClass
{
    public static void Init()
    {
        // attach to configuration callback for Microsoft IServiceCollection
        EpiserverIServiceCollection.MapServiceCollection.ConfigureServices += (serviceCollection) =>
        {
           serviceCollection.AddTransient<IFoo, Foo>();
        };
    }
}
```