namespace WeAreFootball.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ITagsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        IEnumerable<T> GetTagsForNews<T>(int id);
    }
}
