using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services {

    public class JuiceService: ICommonService<JuiceDto, JuiceInsertDto, JuiceUpdateDto> {

        private IRepository<Juice> _juiceRepository;
        private IMapper _mapper;

        public List<string>? Errors { get; }

        public JuiceService (IRepository<Juice> juiceRepository,
            IMapper mapper) 
        {
            _juiceRepository = juiceRepository;
            _mapper = mapper;
            this.Errors = new List<string>();
        }

        public async Task<IEnumerable<JuiceDto>> get () {
            var juices = await _juiceRepository.get();

            /*
            await _context.Juices.Select(j => new JuiceDto() {
                Id = j.Id,
                Name = j.Name,
                Milliliter = j.Milliliter,
                BrandId = j.BrandId
            }).ToListAsync();
            */

            /*return juices.Select(j => new JuiceDto() { 
                Id = j.Id,
                Name = j.Name,
                Milliliter = j.Milliliter,
                BrandId = j.BrandId
            });*/

            return juices.Select(j => _mapper.Map<JuiceDto>(j));
        }

        public async Task<JuiceDto?> getById (int id) {
            var juice = await _juiceRepository.getById(id);

            if (juice == null) {
                return null;
            }

            /*var juiceDto = new JuiceDto() {
                Id = juice.Id,
                Name = juice.Name,
                Milliliter = juice.Milliliter,
                BrandId = juice.BrandId
            };*/

            var juiceDto = _mapper.Map<JuiceDto>(juice);

            return juiceDto;
        }

        public async Task<JuiceDto> add (JuiceInsertDto juiceInsertDto) {
            /*var juice = new Juice() { 
                Name = juiceInsertDto.Name,
                Milliliter = juiceInsertDto.Milliliter,
                BrandId = juiceInsertDto.BrandId
            };*/

            var juice = _mapper.Map<Juice>(juiceInsertDto);

            await _juiceRepository.add(juice);
            await _juiceRepository.save();

            /*var juiceDto = new JuiceDto() { 
                Id = juice.Id,
                Name = juice.Name,
                Milliliter = juice.Milliliter,
                BrandId = juice.BrandId
            };*/

            var juiceDto = _mapper.Map<JuiceDto>(juice);

            return juiceDto;
        }

        public async Task<JuiceDto?> update (int id, JuiceUpdateDto juiceUpdateDto) {
            var juice = await _juiceRepository.getById(id);

            if (juice == null){
                return null;
            }

            // Here the program works correctly
            /*juice.Name = juiceUpdateDto.Name;
            juice.Milliliter = juiceUpdateDto.Milliliter;
            juice.BrandId = juiceUpdateDto.BrandId;*/

            // The program crash if you don't send the id in the body
            // Here is required assigning id to juiceUpdateDto.id to
            // avoid the problem
            juiceUpdateDto.Id = id;
            // The problem is here, because the body doesn't send an ID
            // in juiceUpdateDto, so juiceUpdateDto.Id is 0 and the
            // progama assigns juiceUpdateDto.Id to juice.Id because
            // the attribute names are the same, so the program makes
            // the change. In the case of Hector de Leon, it does not
            // affect him; because of his attributes, he has
            // beerUpdateDto.Id and beer.BeerId, his code is different
            // from min. If the attribute name Id was different like
            // juice.JuiceId it would work
            juice = _mapper.Map<JuiceUpdateDto, Juice>(juiceUpdateDto, juice);

            _juiceRepository.update(juice);
            await _juiceRepository.save();

            /*var juiceDto = new JuiceDto() { 
                Id = juice.Id,
                Name = juice.Name,
                Milliliter = juice.Milliliter,
                BrandId = juice.BrandId,
            };*/

            var juiceDto = _mapper.Map<JuiceDto>(juice);

            return juiceDto;
        }

        public async Task<JuiceDto?> delete (int id) {
            var juice = await _juiceRepository.getById(id);

            if (juice == null) {
                return null;
            }

            /*var juiceDto = new JuiceDto() { 
                Id = juice.Id,
                Name = juice.Name,
                Milliliter = juice.Milliliter,
                BrandId = juice.BrandId
            };*/

            var juiceDto = _mapper.Map<JuiceDto>(juice);

            // _context.Remove(juice);
            _juiceRepository.delete(juice);
            await _juiceRepository.save();

            return juiceDto;
        }

        public bool validate (JuiceInsertDto juiceInsertDto) {
            if (_juiceRepository.search(j => j.Name == juiceInsertDto.Name).Count() > 0) {
                this.Errors?.Add("No puede existir un jugo con un nombre ya existente");
                return false;
            }

            return true;
        }

        public bool validate (JuiceUpdateDto juiceUpdateDto) {
            if (_juiceRepository.search(j => j.Name == juiceUpdateDto.Name 
            && juiceUpdateDto.Id != j.Id).Count() > 0) 
            {
                this.Errors?.Add("No puede existir un jugo con un nombre ya existente");
                return false;
            }

            return true;
        }

    }

}
