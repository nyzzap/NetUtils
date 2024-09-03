using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RestClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RestClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient CreateClient()
    {
        return _httpClientFactory.CreateClient();
    }

    public async Task<T> GetAsync<T>(string url)
    {
        var client = CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var client = CreateClient();
        var jsonContent = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var client = CreateClient();
        var jsonContent = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
    }

    public async Task DeleteAsync(string url)
    {
        var client = CreateClient();
        var response = await client.DeleteAsync(url);
        response.EnsureSuccessStatusCode();
    }
}
