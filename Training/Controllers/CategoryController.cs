using Microsoft.AspNetCore.Mvc;
using Training.Data.Dtos;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("app/service/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<CategoryDto>> GetAll()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<CategoryDto> GetById(long id)
        {
            return await _categoryService.GetById(id);
        }

        [HttpPost]
        [Route("create")]
        public async Task<CategoryDto> Create([FromBody] CategoryCreateDto category)
        {
            return await _categoryService.Create(category);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task Update(long id, [FromBody] CategoryUpdateDto category)
        {
            await _categoryService.Update(id, category);
        }
    }
}