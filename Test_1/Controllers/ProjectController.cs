using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TPSS.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/project")]
    [EnableCors]
    public class ProjectController : ControllerBase
    {

        public ProjectController() { }
    }
}
