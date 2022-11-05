using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;

namespace Training.Services
{
    public class ArticleService
    {
        private readonly CmsContext _cmsContext;
        private readonly IMapper _mapper;
        public ArticleService(CmsContext cmsContext, IMapper mapper)
        {
            _cmsContext = cmsContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleDto>> GetAll()
        {
            var data = await _cmsContext.Articles.Include(x => x.Category).ToListAsync();

            return _mapper.Map<List<Article>, List<ArticleDto>>(data);
        }

        public async Task<ArticleDto> GetById(long id)
        {
            var data = await _cmsContext.Articles.FindAsync(id);

            return _mapper.Map<Article, ArticleDto>(data);
        }

        public async Task<ArticleDto> Create(ArticleCreateDto article)
        {
            var newArticle = new Article()
            {
                InsertDate = DateTime.Now
            };

            _mapper.Map(article, newArticle);

            var Entity = (await _cmsContext.Articles.AddAsync(newArticle)).Entity;

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<Article, ArticleDto>(Entity);
        }

        public async Task Update(long id, ArticleUpdateDto article)
        {
            var EntityToBeUpdated = await _cmsContext.Articles.FindAsync(id);

            if (EntityToBeUpdated != null)
            {
                _mapper.Map(article, EntityToBeUpdated);

                EntityToBeUpdated.UpdateDate = DateTime.Now;

                await _cmsContext.SaveChangesAsync();
            }
        }

        public async Task Delete(long id)
        {
            var Entity = await _cmsContext.Articles.FindAsync(id);

            if (Entity != null)
            {
                _cmsContext.Articles.Remove(Entity);

                await _cmsContext.SaveChangesAsync();
            }
        }
    }
}
