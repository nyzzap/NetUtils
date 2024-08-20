using Microsoft.Extensions.DependencyInjection;
using System;

public static class HttpClientFactoryConfiguration
{
    public static IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddHttpClient("MyApiClient", client =>
        {
            client.BaseAddress = new Uri("https://api.example.com/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return serviceCollection.BuildServiceProvider();
    }
}
