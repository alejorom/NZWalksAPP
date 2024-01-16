using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
    // https://localhost:port/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RegionsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            return Ok(regions);
        }

        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}
