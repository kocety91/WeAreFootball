namespace WeAreFootball.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IContactsService
    {
        Task Send(ContactFormInputViewModel contactFormEntryViewModel);
    }
}
