namespace WeAreFootball.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using WeAreFootball.Web.ViewModels.ContacktForm;

    public interface IContactsService
    {
        Task Send(ContactFormInputViewModel contactFormEntryViewModel);
    }
}
