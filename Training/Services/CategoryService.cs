using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;

namespace Training.Services
{
    public class CategoryService
    {
        private readonly CmsContext _cmsContext;
        private readonly IMapper _mapper;
        public CategoryService(CmsContext cmsContext, IMapper mapper)
        {
            _cmsContext = cmsContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var data = await _cmsContext.Categories.ToListAsync();

            return _mapper.Map<List<Category>, List<CategoryDto>>(data);
        }

        public async Task<CategoryDto> GetById(long id)
        {
            var data = await _cmsContext.Categories.FindAsync(id);

            return _mapper.Map<Category, CategoryDto>(data);
        }

        public async Task<CategoryDto> Create(CategoryCreateDto category)
        {
            var newCategory = new Category()
            {
                InsertDate = DateTime.Now,
            };

            _mapper.Map(category, newCategory);

            var Entity = (await _cmsContext.AddAsync(newCategory)).Entity;

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDto>(Entity);
        }

        public async Task Update(long id, CategoryUpdateDto category)
        {
            var EntityToBeUpdated = await _cmsContext.Categories.FindAsync(id);

            if (EntityToBeUpdated != null)
            {
                _mapper.Map(category, EntityToBeUpdated);

                EntityToBeUpdated.UpdateDate = DateTime.Now;

                await _cmsContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var Entity = await _cmsContext.Categories.FindAsync(id);

            if (Entity != null)
            {
                _cmsContext.Categories.Remove(Entity);

                await _cmsContext.SaveChangesAsync();
            }
        }
    }
}
