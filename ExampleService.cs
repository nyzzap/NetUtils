using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;

using System.Threading.Tasks;

public class MyApiService
{
    private readonly HttpClientService _httpClientService;

    public MyApiService(HttpClient httpClient)
    {
        _httpClientService = new HttpClientService(httpClient);
    }

    public async Task<MyModel> GetItemAsync(int id)
    {
        return await _httpClientService.GetAsync<MyModel>($"items/{id}");
    }

    public async Task<MyModel> CreateItemAsync(MyModel newItem)
    {
        return await _httpClientService.PostAsync<MyModel, MyModel>("items", newItem);
    }

    public async Task<MyModel> UpdateItemAsync(int id, MyModel updatedItem)
    {
        return await _httpClientService.PutAsync<MyModel, MyModel>($"items/{id}", updatedItem);
    }

    public async Task<MyModel> DeleteItemAsync(int id)
    {
        return await _httpClientService.DeleteAsync<MyModel>($"items/{id}");
    }
}


// En Global.asax.cs o el punto de entrada principal de tu aplicación ASP.NET
public class Global : HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {
        // Configurar el servicio para HttpClientFactory
        var serviceProvider = HttpClientFactoryConfiguration.ConfigureServices();
        var myService = serviceProvider.GetService<MyService>();

        // Usar el servicio como sea necesario, por ejemplo, en un controlador
    }
}
