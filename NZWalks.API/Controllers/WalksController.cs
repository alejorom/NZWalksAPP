using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

            // Use domain model to create the walk
            walkDomain = await walkRepository.CreateAsync(walkDomain);

            // Map Domain model back to DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));

        }

    }
}
