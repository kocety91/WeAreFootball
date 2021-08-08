namespace WeAreFootball.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels.Events;

    public class EventsService : IEventsService
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public EventsService(
            IRepository<Event> eventsRepository,
            IDeletableEntityRepository<Team> teamsRepository)
        {
            this.eventsRepository = eventsRepository;
            this.teamsRepository = teamsRepository;
        }

        public async Task<int> CreateAsync(CreateEventViewModel input, string userId, string imagePath)
        {
            var currentEvent = new Event()
            {
                UserId = userId,
                Date = input.Date,
                Sign = input.Sign,
                Content = input.Content,
                IsDerbyOfTheWeek = input.IsDerbyOfTheWeek,
                LeagueId = input.LeagueId,
            };

            var searchedAwayTeam = this.teamsRepository.All().Where(x => x.Id == input.AwayTeamId).FirstOrDefault();

            currentEvent.EventTeams.Add(new EventTeams
            {
                TeamId = input.AwayTeamId,
                Team = searchedAwayTeam,
                EventId = currentEvent.Id,
            });

            var searchedHomeTeam = this.teamsRepository.All().Where(x => x.Id == input.HomeTeamId).FirstOrDefault();

            currentEvent.EventTeams.Add(new EventTeams
            {
                TeamId = input.HomeTeamId,
                Team = searchedHomeTeam,
                EventId = currentEvent.Id,
            });

            Directory.CreateDirectory($"{imagePath}/events/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            await this.eventsRepository.AddAsync(currentEvent);
            await this.eventsRepository.SaveChangesAsync();

            currentEvent.Image = new Image()
            {
                EventId = currentEvent.Id,
                Event = currentEvent,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/events/{currentEvent.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
            await this.eventsRepository.SaveChangesAsync();

            return currentEvent.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var currentEvent = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            if (currentEvent == null)
            {
                throw new ArgumentException(string.Format("Event doesnt exist."));
            }

            this.eventsRepository.Delete(currentEvent);
            await this.eventsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            return this.eventsRepository.All().OrderBy(x => x.Date)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetLastFour<T>()
        {
            return this.eventsRepository.All()
                .OrderByDescending(x => x.Date)
                .Take(4)
                .To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.eventsRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.eventsRepository.All().Count();
        }

        public IEnumerable<T> TodayEvents<T>()
        {
            var todayDate = DateTime.Now.Day;
            return this.eventsRepository.All()
                .Where(x => x.Date.Day == todayDate)
                .To<T>().ToList();
        }

        public T GetLatestDerbyOfTheWeek<T>()
        {
            return this.eventsRepository.All()
                .Where(x => x.IsDerbyOfTheWeek == true)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetByTeamId<T>(int teamId)
        {
            return this.eventsRepository.All()
                .Where(x => x.EventTeams.Any(t => t.TeamId == teamId))
                .Take(3).To<T>().ToList();
        }

        public IEnumerable<T> GetByLeagueId<T>(int leagueId, int page, int itemsPerPage = 6)
        {
            return this.eventsRepository.All()
                .Where(x => x.LeagueId == leagueId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetByTeamName<T>(string teamName)
        {
            return this.eventsRepository.All()
                .Where(x => x.EventTeams.Any(t => t.Team.Name.Contains(teamName)))
                .To<T>().ToList();
        }
    }
}
