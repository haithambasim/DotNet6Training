using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Training.Data.Dtos;
using Training.Data.Entities;
using Training.Data.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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

        public async Task<List<ArticleDto>> GetPages(string term, int page, int pageSize, string sortColumn, string SortOrder)
        {
            var data = await _cmsContext.Articles
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .Where(x => x.Title.Contains(term) || x.ShortDescription.Contains(term) || x.Content.Contains(term))
                .OrderBy($"{sortColumn ?? "id"} {SortOrder ?? "asc"}")
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return _mapper.Map<List<Article>, List<ArticleDto>>(data);
        }
        public async Task<List<ArticleDto>> GetAll()
        {
            var data = await _cmsContext.Articles
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .ToListAsync();

            return _mapper.Map<List<Article>, List<ArticleDto>>(data);
        }
        public async Task<ArticleDto> GetById(long id)
        {
            var data = await _cmsContext.Articles
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<Article, ArticleDto>(data);
        }

        public async Task<ArticleDto> Create(ArticleCreateDto article)
        {
            var Tags = await _cmsContext.Tags.Where(x => article.TagIds.Contains(x.Id)).ToListAsync();

            var newArticle = new Article()
            {
                InsertDate = DateTime.Now,
                Tags = Tags,
            };

            _mapper.Map(article, newArticle);

            var Entity = (await _cmsContext.Articles.AddAsync(newArticle)).Entity;

            await _cmsContext.SaveChangesAsync();

            return _mapper.Map<Article, ArticleDto>(Entity);
        }

        public async Task Update(long id, ArticleUpdateDto article)
        {
            var Tags = await _cmsContext.Tags.Where(x => article.TagIds.Contains(x.Id)).ToListAsync();

            var EntityToBeUpdated = await _cmsContext.Articles
                .Include(x => x.Tags)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (EntityToBeUpdated != null)
            {
                _mapper.Map(article, EntityToBeUpdated);

                EntityToBeUpdated.UpdateDate = DateTime.Now;

                EntityToBeUpdated.Tags = Tags;

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
