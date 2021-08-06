﻿namespace WeAreFootball.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewsService
    {
        int GetCount();

        T GetById<T>(int id);

        int GetSearchedCount(string teamName);

        int GetTodayNewsCount(DateTime today);

        IEnumerable<T> GetMostComment<T>();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetLastFiveSimilarNews<T>(int leagueId, int newsId);

        IEnumerable<T> GetNewsByCountry<T>(int leagueId);

        IEnumerable<T> GetNewsByTeamName<T>(string teamName);

        Task Scrape();

        Task<int> CreateAsync(CreateNewsViewModel input, string userId, string imagePath);

        Task UpdateAsync(int id, EditNewsInputModel input);

        IEnumerable<T> Search<T>(string searchString, int page, int itemsPerPage = 6);

        Task DeleteAsync(int id);
    }
}
