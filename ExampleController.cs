using System.Threading.Tasks;
using System.Web.Http;

public class DataController : ApiController
{
    private readonly MyService _myService;

    public DataController(MyService myService)
    {
        _myService = myService;
    }

    [HttpGet]
    public async Task<IHttpActionResult> Get()
    {
        var data = await _myService.GetDataAsync();
        return Ok(data);
    }
}
