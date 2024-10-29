using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repositories;

namespace Backend.Services {

    public class BrandService: ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> {

        private IRepository<Brand> _brandRepository;
        // Implements Mappers
        IMapper _mapper;

        public List<string>? Errors { get; }

        public BrandService (IRepository<Brand> brandRepository,
            IMapper mapper)     
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            this.Errors = new List<string>();
        }

        public async Task<IEnumerable<BrandDto>> get () {
            var brands = await _brandRepository.get();

            /*return brands.Select(b => new BrandDto() { 
                Id = b.Id,
                Name = b.Name
            });*/
            return brands.Select(b => _mapper.Map<BrandDto>(b));
        }

        public async Task<BrandDto?> getById (int id) {
            var brand = await _brandRepository.getById(id);

            if (brand == null) {
                return null;
            }

            /*return new BrandDto() { 
                Id = brand.Id,
                Name = brand.Name
            };*/
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> add (BrandInsertDto brandInsertDto) {
            /*var brand = new Brand() { 
                Name = brandInsertDto.Name
            };*/
            var brand = _mapper.Map<Brand>(brandInsertDto);

            await _brandRepository.add(brand);
            await _brandRepository.save();

            /*var brandDto = new BrandDto() { 
                Id = brand.Id,
                Name = brand.Name
            };*/
            var brandDto = _mapper.Map<BrandDto>(brand);

            return brandDto;
        }

        public async Task<BrandDto?> update (int id, BrandUpdateDto brandUpdateDto) {
            var brand = await _brandRepository.getById(id);

            if (brand == null) {
                return null;
            }

            // brand.Name = brandUpdateDto.Name;
            brandUpdateDto.Id = id;
            brand = _mapper.Map<BrandUpdateDto, Brand>(brandUpdateDto, brand);

            _brandRepository.update(brand);
            await _brandRepository.save();

            var brandDto = new BrandDto() { 
                Id = brand.Id,
                Name = brand.Name
            };

            return brandDto;
        }

        public async Task<BrandDto?> delete (int id) {
            var brand = await _brandRepository.getById(id);

            if (brand == null) {
                return null;
            }

            /*var brandDto = new BrandDto() { 
                Id = brand.Id,
                Name = brand.Name
            };*/
            var brandDto = _mapper.Map<BrandDto>(brand);

            _brandRepository.delete(brand);
            await _brandRepository.save();

            return brandDto;
        }

        public bool validate (BrandInsertDto brandInsertDto) {
            if (_brandRepository.search(b => b.Name == brandInsertDto.Name).Count() > 0) {
                this.Errors?.Add("No puede existir una marca con un nombre ya existente");
                return false;
            }

            return true;
        }

        public bool validate (BrandUpdateDto brandUpdateDto) {
            if (_brandRepository.search(b => b.Name == brandUpdateDto.Name
            && b.Id != brandUpdateDto.Id).Count() > 0) 
            {
                this.Errors?.Add("No puede existir una marca con un nombre ya existente");
                return false;
            }

            return true;
        }
    }

}
