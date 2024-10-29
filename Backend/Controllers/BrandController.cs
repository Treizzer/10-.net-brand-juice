using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BrandController: ControllerBase {

        private ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> _brandService;

        public BrandController ([FromKeyedServices("brandService")] ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> brandService) {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDto>> get () =>
            await _brandService.get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> getById (int id) {
            var brandDto = await _brandService.getById(id);

            return brandDto == null ? NotFound() : Ok(brandDto);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDto>> add (BrandInsertDto brandInsertDto) {
            if (!_brandService.validate(brandInsertDto)) {
                return BadRequest(_brandService.Errors);
            }

            var brandDto = await _brandService.add(brandInsertDto);

            return CreatedAtAction(nameof(getById), new { id = brandDto.Id }, brandDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BrandDto>> update (int id, BrandUpdateDto brandUpdateDto) { 
            if (!_brandService.validate(brandUpdateDto)) {
                return BadRequest(_brandService.Errors);
            }

            var brandDto = await _brandService.update(id, brandUpdateDto);

            return brandDto == null ? NotFound() : Ok(brandDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BrandDto>> delete (int id) {
            var brandDto = await _brandService.delete(id);

            return brandDto == null ? NotFound() : Ok(brandDto);
        }

    }

}
