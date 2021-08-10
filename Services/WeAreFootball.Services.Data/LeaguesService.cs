namespace WeAreFootball.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels.Leagues;

    public class LeaguesService : ILeaguesService
    {
        private readonly IDeletableEntityRepository<League> leaguesRepository;
        private readonly IRepository<NewsLeague> newsLeaguesRepository;

        public LeaguesService(
            IDeletableEntityRepository<League> leaguesRepository,
            IRepository<NewsLeague> newsLeaguesRepository)
        {
            this.leaguesRepository = leaguesRepository;
            this.newsLeaguesRepository = newsLeaguesRepository;
        }

        public async Task<int> CreateAsync(CreateLeagueViewModel input, string imagePath, string imagePathLogo)
        {
            var league = this.leaguesRepository.All().Where(x => x.Name == input.Name).FirstOrDefault();

            if (league == null)
            {
                league = new League()
                {
                    Name = input.Name,
                    Country = input.Country,
                };
            }
            else
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.LeagueAlreadyExists, input.Name));
            }

            Directory.CreateDirectory($"{imagePath}/leagues/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            await this.leaguesRepository.AddAsync(league);
            await this.leaguesRepository.SaveChangesAsync();

            league.Image = new Image()
            {
                LeagueId = league.Id,
                League = league,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/leagues/{league.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            Directory.CreateDirectory($"{imagePathLogo}/leagues/");
            var logoExtension = Path.GetExtension(input.Logo.FileName).TrimStart('.');

            league.Logo = new Logo()
            {
                LeagueId = league.Id,
                League = league,
                Extension = logoExtension,
            };

            var logoPhysicalPath = $"{imagePathLogo}/leagues/{league.Logo.Id}.{logoExtension}";
            using Stream logoFileStream = new FileStream(logoPhysicalPath, FileMode.Create);
            await input.Logo.CopyToAsync(logoFileStream);

            await this.leaguesRepository.SaveChangesAsync();

            return league.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var league = this.leaguesRepository.All().FirstOrDefault(x => x.Id == id);

            if (league == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.LeagueDoesntExists));
            }

            this.leaguesRepository.Delete(league);
            await this.leaguesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> All<T>()
        {
            return this.leaguesRepository.All()
                .OrderBy(x => x.Id)
                .To<T>().ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.leaguesRepository.All().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(int id)
        {
            return this.leaguesRepository.All()
                .Where(x => x.Id == id)
                .OrderByDescending(x => x.Teams.Count())
                .To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.leaguesRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.leaguesRepository.All()
                .Where(x => x.Country != "International")
               .To<T>().ToList();
        }

        public IEnumerable<T> GetleaguesForNews<T>(int id)
        {
            return this.newsLeaguesRepository.All()
                .Where(x => x.NewsId == id)
                .To<T>().ToList();
        }

        public IEnumerable<T> ShowAll<T>(int page, int itemsPerPage = 6)
        {
            return this.leaguesRepository.All()
                .OrderByDescending(x => x.Teams.Count())
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>().ToList();
        }

        public string GetLeagueName(int leagueId)
        {
            return this.leaguesRepository.All()
                .Where(x => x.Id == leagueId)
                .Select(x => x.Name)
                .FirstOrDefault();
        }
    }
}
