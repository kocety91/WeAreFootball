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
    using WeAreFootball.Web.ViewModels.Teams;

    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public TeamsService(
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.teamsRepository = teamsRepository;
            this.tagsRepository = tagsRepository;
        }

        public async Task<int> CreateAsync(CreateTeamViewModel input, string imagePath, string imagePathLogo)
        {
            var team = this.teamsRepository.All().Where(x => x.Name == input.Name).FirstOrDefault();

            if (team == null)
            {
                team = new Team()
                {
                    Name = input.Name,
                    City = input.City,
                    StadiumName = input.StadiumName,
                    LeagueId = input.LeagueId,
                };
            }
            else
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.TeamAlreadyExists, input.Name));
            }

            Directory.CreateDirectory($"{imagePath}/teams/");
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            await this.teamsRepository.AddAsync(team);
            await this.teamsRepository.SaveChangesAsync();

            team.Image = new Image()
            {
                TeamId = team.Id,
                Team = team,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/teams/{team.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            Directory.CreateDirectory($"{imagePathLogo}/teams/");
            var logoExtension = Path.GetExtension(input.Logo.FileName).TrimStart('.');

            team.Logo = new Logo()
            {
                TeamId = team.Id,
                Team = team,
                Extension = logoExtension,
            };

            var logoPhysicalPath = $"{imagePathLogo}/teams/{team.Logo.Id}.{logoExtension}";
            using Stream logoFileStream = new FileStream(logoPhysicalPath, FileMode.Create);
            await input.Logo.CopyToAsync(logoFileStream);

            await this.teamsRepository.SaveChangesAsync();

            var currnetTeamTag = new Tag()
            {
                Name = team.Name,
            };

            if (!this.tagsRepository.All().Any(x => x.Name == currnetTeamTag.Name))
            {
                await this.tagsRepository.AddAsync(currnetTeamTag);
            }

            return team.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var team = this.teamsRepository.All().FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.TeamDoesntExists));
            }

            this.teamsRepository.Delete(team);
            await this.teamsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            return this.teamsRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.teamsRepository.All().Count();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.teamsRepository.All().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<T> All<T>(int page, int itemsPerPage = 6)
        {
            return this.teamsRepository.All().OrderBy(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            return this.teamsRepository.All().OrderBy(x => x.Id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public T GetByTagName<T>(string tagName)
        {
            return this.teamsRepository.All()
                .Where(x => x.Name == tagName)
                .To<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetByLeagueId<T>(int leagueId)
        {
            return this.teamsRepository.All()
                .Where(x => x.LeagueId == leagueId)
                .To<T>().ToList();
        }
    }
}
