using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

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
            // Obtener data de la base de datos mediante DomainModel
            var regionsDomain = dbContext.Regions.ToList();

            // Convertir DomainModel a DTO
            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add( new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain .Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            // Retornar DTO
            return Ok(regionDto);
        }

        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Obtener data de la base de datos mediante DomainModel
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            // Convertir DomainModel a DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            // Retornar DTO
            return Ok(regionDto);
        }
    }
}
