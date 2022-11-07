using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.Data.Dtos;
using Training.Services;

namespace Training.Controllers
{
    [Authorize]
    [ApiController]
    [Route("app/service/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagService _TagService;

        public TagController(TagService TagService)
        {
            _TagService = TagService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<List<TagDto>> GetAll()
        {
            return await _TagService.GetAll();
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<TagDto> GetById(long id)
        {
            return await _TagService.GetById(id);
        }

        [HttpPost]
        [Route("create")]
        public async Task<TagDto> Create([FromBody] TagCreateDto Tag)
        {
            return await _TagService.Create(Tag);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task Update(long id, [FromBody] TagUpdateDto Tag)
        {
            await _TagService.Update(id, Tag);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task Delete(long id)
        {
            await _TagService.Delete(id);
        }
    }
}