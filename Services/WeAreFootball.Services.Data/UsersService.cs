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

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            return this.usersRepository.All().OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public int GetCount()
        {
            return this.usersRepository.All().Count();
        }

        public IEnumerable<T> GetMostActiveUsers<T>()
        {
            return this.usersRepository.All()
                .OrderBy(x => x.Comments.Count())
                .Take(5)
                .To<T>().ToList();
        }

        public int GetRegisteredUsersToday(DateTime today)
        {
            var countUsers = this.usersRepository.All()
                .Where(x => x.CreatedOn.Day == today.Day)
                .ToList();

            return countUsers.Count();
        }

        public string GetUserImage(string userName)
        {
            return this.usersRepository.All().Where(x => x.UserName == userName)
                .Select(u => u.Image.RemoteImageUrl != null ? u.Image.RemoteImageUrl
                : "/images/users/" + u.Image.Id + "." + u.Image.Extension).FirstOrDefault();
        }

        public async Task UploadImageToUser(InputModel input, string userId, string imagePath)
        {
            var user = this.usersRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            Directory.CreateDirectory($"{imagePath}/users/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            user.Image = new Image()
            {
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/users/{user.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
