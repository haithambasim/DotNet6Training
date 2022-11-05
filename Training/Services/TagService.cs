using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;

namespace Training.Services
{
    public class TagService
    {
        private readonly CmsContext _cmsContext;
        private readonly IMapper _mapper;
        public TagService(CmsContext cmsContext, IMapper mapper)
        {
            _cmsContext = cmsContext;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> GetAll()
        {
            var data = await _cmsContext.Tags.ToListAsync();

            return _mapper.Map<List<Tag>, List<TagDto>>(data);
        }

        public async Task<TagDto> GetById(long id)
        {
            var data = await _cmsContext.Tags.FindAsync(id);

            return _mapper.Map<Tag, TagDto>(data);
        }

        public async Task<TagDto> Create(TagCreateDto Tag)
        {
            var newTag = new Tag()
            {
                InsertDate = DateTime.Now,
            };

            _mapper.Map(Tag, newTag);

            var Entity = (await _cmsContext.AddAsync(newTag)).Entity;

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<Tag, TagDto>(Entity);
        }

        public async Task Update(long id, TagUpdateDto Tag)
        {
            var EntityToBeUpdated = await _cmsContext.Tags.FindAsync(id);

            if (EntityToBeUpdated != null)
            {
                _mapper.Map(Tag, EntityToBeUpdated);

                EntityToBeUpdated.UpdateDate = DateTime.Now;

                await _cmsContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var Entity = await _cmsContext.Tags.FindAsync(id);

            if (Entity != null)
            {
                _cmsContext.Tags.Remove(Entity);

                await _cmsContext.SaveChangesAsync();
            }
        }
    }
}
