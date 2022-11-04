using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;

namespace Training.Controllers
{
    [ApiController]
    [Route("app/service/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CmsContext _cmsContext;
        private readonly IMapper _mapper;

        public CategoryController(CmsContext cmsContext, IMapper mapper)
        {
            _cmsContext = cmsContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<CategoryDto>> GetAll()
        {
            var data = await _cmsContext.Categories.ToListAsync();

            return _mapper.Map<List<Category>, List<CategoryDto>>(data);
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<CategoryDto> GetById(long id)
        {
            var data = await _cmsContext.Categories.FindAsync(id);

            return _mapper.Map<Category, CategoryDto>(data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<CategoryDto> Create([FromBody] CategoryCreateDto category)
        {
            var newCategory = new Category();

            _mapper.Map(newCategory, category);

            var Entity = (await _cmsContext.AddAsync(newCategory)).Entity;

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDto>(Entity);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task Update(long id, [FromBody] CategoryUpdateDto category)
        {
            var EntityToBeUpdated = await _cmsContext.Categories.FindAsync(id);

            if (EntityToBeUpdated != null)
            {
                _mapper.Map(category, EntityToBeUpdated);

                await _cmsContext.SaveChangesAsync();
            }
        }
    }
}