using Microsoft.AspNetCore.Mvc;
using Training.Data.Dtos;
using Training.Exceptions;
using Training.Services;

namespace Training.Controllers
{
    [ApiController]
    [Route("app/service/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ArticleService articleService, ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get-pages")]
        public async Task<List<ArticleDto>> GetPages(string term, int page, int pageSize, string sortColumn, string SortOrder)
        {
            return await _articleService.GetPages(term, page, pageSize, sortColumn, SortOrder);
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
            if (id == 0)
                throw new UserFriendlyException($"Invalid Article id { id }");

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