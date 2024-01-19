using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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

        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var regionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            // Use Domain Model to create Region
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }
    }
}
