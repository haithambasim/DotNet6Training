using Microsoft.AspNetCore.Mvc;
using Training.Data.Dtos;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("app/service/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<ArticleDto>> GetAll()
        {
            return await _articleService.GetAll();
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<ArticleDto> GetById(long id)
        {
            return await _articleService.GetById(id);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ArticleDto> Create([FromBody] ArticleCreateDto article)
        {
            return await _articleService.Create(article);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task Update(long id, [FromBody] ArticleUpdateDto article)
        {
            await _articleService.Update(id, article);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task Delete(long id)
        {
            await _articleService.Delete(id);
        }
    }
}