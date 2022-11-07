using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Training.Data.Dtos;
using Training.Services;
using Training.Shared;

namespace Training.Controllers
{
    [Authorize]
    [ApiController]
    [Route("app/service/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IOptions<List<AppClient>> _apiClients;

        public CategoryController(CategoryService categoryService, IOptions<List<AppClient>> apiClients)
        {
            _categoryService = categoryService;
            _apiClients = apiClients;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<CategoryDto>> GetAll()
        {
            Console.WriteLine(_apiClients.Value);
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

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task Delete(long id)
        {
            await _categoryService.Delete(id);
        }
    }
}