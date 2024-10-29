using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class JuiceController: ControllerBase {

        private IValidator<JuiceInsertDto> _juiceInsertValidator;
        private IValidator<JuiceUpdateDto> _juiceUpdateValidator;
        private ICommonService<JuiceDto, JuiceInsertDto, JuiceUpdateDto> _juiceService;

        public JuiceController (IValidator<JuiceInsertDto> juiceInsertValidator,
            IValidator<JuiceUpdateDto> juiceUpdateValidator,
            [FromKeyedServices("juiceService")] ICommonService<JuiceDto, JuiceInsertDto, JuiceUpdateDto> juiceService) 
        {
            _juiceInsertValidator = juiceInsertValidator;
            _juiceUpdateValidator = juiceUpdateValidator;
            _juiceService = juiceService;
        }

        [HttpGet]
        public async Task<IEnumerable<JuiceDto>> get () =>
            await _juiceService.get();

        [HttpGet("{id}")]
        public async Task<ActionResult<JuiceDto>> getById (int id) {
            var juiceDto = await _juiceService.getById(id);

            return juiceDto == null ? NotFound() : Ok(juiceDto);
        }

        [HttpPost]
        public async Task<ActionResult<JuiceDto>> add (JuiceInsertDto juiceInsertDto) {
            var validationResult = await _juiceInsertValidator.ValidateAsync(juiceInsertDto);

            if (!validationResult.IsValid) {
                return BadRequest(validationResult.Errors);
            }

            if (!_juiceService.validate(juiceInsertDto)) {
                return BadRequest(_juiceService.Errors);
            }

            var juiceDto = await _juiceService.add(juiceInsertDto);

            return CreatedAtAction(nameof(getById), new { id = juiceDto.Id }, juiceDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JuiceDto>> update (int id, JuiceUpdateDto juiceUpdateDto) {
            var validationResult = await _juiceUpdateValidator.ValidateAsync(juiceUpdateDto);

            if (!validationResult.IsValid) {
                return BadRequest(validationResult.Errors);
            }

            if (!_juiceService.validate(juiceUpdateDto)) {
                return BadRequest(_juiceService.Errors);
            }

            var juiceDto = await _juiceService.update(id, juiceUpdateDto);

            return juiceDto == null ? NotFound() : Ok(juiceDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<JuiceDto>> delete (int id) {
            var juiceDto = await _juiceService.delete(id);

            return juiceDto == null ? NotFound() : Ok(juiceDto);
        }

    }

}
