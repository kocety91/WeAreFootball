namespace WeAreFootball.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Services.Mapping;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IRepository<NewsTag> newsTagsRepository;

        public TagsService(
            IDeletableEntityRepository<Tag> tagsRepository,
            IRepository<NewsTag> newsTagsRepository)
        {
            this.tagsRepository = tagsRepository;
            this.newsTagsRepository = newsTagsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.tagsRepository.All().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<T> GetTagsForNews<T>(int id)
        {
            return this.newsTagsRepository.All().Where(x => x.NewsId == id).To<T>().ToList();
        }
    }
}
