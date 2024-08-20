using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método GET genérico
    public async Task<Result<T>> GetAsync<T>(string requestUri)
    {
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            return await ProcessResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new Result<T>(default, null, ex.Message);
        }
    }

    // Método POST genérico
    public async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest requestData)
    {
        try
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, content);
            return await ProcessResponse<TResponse>(response);
        }
        catch (Exception ex)
        {
            return new Result<TResponse>(default, null, ex.Message);
        }
    }

    // Método PUT genérico
    public async Task<Result<TResponse>> PutAsync<TRequest, TResponse>(string requestUri, TRequest requestData)
    {
        try
        {
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestUri, content);
            return await ProcessResponse<TResponse>(response);
        }
        catch (Exception ex)
        {
            return new Result<TResponse>(default, null, ex.Message);
        }
    }

    // Método DELETE genérico
    public async Task<Result<T>> DeleteAsync<T>(string requestUri)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            return await ProcessResponse<T>(response);
        }
        catch (Exception ex)
        {
            return new Result<T>(default, null, ex.Message);
        }
    }

    // Procesa la respuesta HTTP
    private async Task<Result<T>> ProcessResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            T data = default;

            // Intenta deserializar el contenido si no está vacío
            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    data = JsonConvert.DeserializeObject<T>(content);
                }
                catch (JsonException ex)
                {
                    return new Result<T>(default, response.StatusCode, $"Error al deserializar: {ex.Message}");
                }
            }

            return new Result<T>(data, response.StatusCode, null);
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return new Result<T>(default, response.StatusCode, errorMessage);
        }
    }
}

// Clase para encapsular el resultado
public class Result<T>
{
    public T Data { get; }
    public System.Net.HttpStatusCode? StatusCode { get; }
    public string ErrorMessage { get; }

    public Result(T data, System.Net.HttpStatusCode? statusCode, string errorMessage)
    {
        Data = data;
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}

// Métodos de extensión
public static class HttpClientExtensions
{
    public static void SetJsonContent(this HttpContent content)
    {
        if (content.Headers.ContentType == null)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
