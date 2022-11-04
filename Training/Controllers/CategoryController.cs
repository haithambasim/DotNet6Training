using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;

namespace Training.Controllers
{
    [ApiController]
    [Route("app/service/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CmsContext _cmsContext;
        public CategoryController(CmsContext cmsContext)
        {
            _cmsContext = cmsContext;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<Category>> GetAll()
        {
            return await _cmsContext.Categories.ToListAsync();
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<Category> GetById(long id)
        {
            return await _cmsContext.Categories.FindAsync(id);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Category> Create([FromBody] Category category)
        {
            var Entity = (await _cmsContext.AddAsync(category)).Entity;

            await _cmsContext.SaveChangesAsync();

            return Entity;
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task Update(long id, [FromBody] Category category)
        {
            var EntityToBeUpdated = await _cmsContext.Categories.FindAsync(id);

            if (EntityToBeUpdated != null)
            {
                EntityToBeUpdated.Name = category.Name;
                EntityToBeUpdated.Slug = category.Slug;

                await _cmsContext.SaveChangesAsync();
            }
        }
    }
}