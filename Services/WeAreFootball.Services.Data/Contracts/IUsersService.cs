namespace WeAreFootball.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WeAreFootball.Web.ViewModels.Administration;

    public interface IUsersService
    {
        int GetCount();

        string GetUserImage(string userName);

        Task UploadImageToUser(InputModel input, string userId, string imagePath);

        int GetRegisteredUsersToday(DateTime today);

        IEnumerable<T> GetMostActiveUsers<T>();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);
    }
}
