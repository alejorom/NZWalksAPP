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
            if (ModelState.IsValid)
            {
                // Map or Convert DTO to Domain Model
                var walkDomain = mapper.Map<Walk>(addWalkRequestDto);

                // Use domain model to create the walk
                walkDomain = await walkRepository.CreateAsync(walkDomain);

                // Map Domain model back to DTO
                return Ok(mapper.Map<WalkDto>(walkDomain));
            }
            return BadRequest();
        }

        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            var walksDomain = await walkRepository.GetAllAsync();
            
            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }

        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

                if (walkDomain == null)
                {
                    return NotFound();
                }

                // Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomain));
            }
            return BadRequest();
        }

        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

    }
}
