using System.Threading.Tasks;

public class PolizasService : IPolizasService
{
    private readonly RestClient _restClient;

    public PolizasService(RestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<string> ObtenerPolizaAsync(string numeroPoliza)
    {
        var url = $"https://api.ejemplo.com/polizas/{numeroPoliza}";
        var poliza = await _restClient.GetAsync<string>(url);
        return poliza;
    }

    // Ejemplo de un método POST, si necesitas implementar una operación POST
    public async Task<PolizaResponse> CrearPolizaAsync(PolizaRequest request)
    {
        var url = "https://api.ejemplo.com/polizas";
        var response = await _restClient.PostAsync<PolizaRequest, PolizaResponse>(url, request);
        return response;
    }

    // Ejemplo de un método PUT
    public async Task<PolizaResponse> ActualizarPolizaAsync(string numeroPoliza, PolizaRequest request)
    {
        var url = $"https://api.ejemplo.com/polizas/{numeroPoliza}";
        var response = await _restClient.PutAsync<PolizaRequest, PolizaResponse>(url, request);
        return response;
    }

    // Ejemplo de un método DELETE
    public async Task EliminarPolizaAsync(string numeroPoliza)
    {
        var url = $"https://api.ejemplo.com/polizas/{numeroPoliza}";
        await _restClient.DeleteAsync(url);
    }
}
