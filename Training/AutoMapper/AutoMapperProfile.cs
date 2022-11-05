using AutoMapper;
using Training.Data.Dtos;
using Training.Data.Entities;

namespace Training.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Category
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();

            // Article
            CreateMap<ArticleCreateDto, Article>().ReverseMap();
            CreateMap<ArticleUpdateDto, Article>();
            CreateMap<Article, ArticleDto>();

            // Tag
            CreateMap<TagCreateDto, Tag>().ReverseMap();
            CreateMap<TagUpdateDto, Tag>();
            CreateMap<Tag, TagDto>();
        }
    }
}