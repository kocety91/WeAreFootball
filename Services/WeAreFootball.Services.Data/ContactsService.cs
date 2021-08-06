namespace WeAreFootball.Services.Data
{
    using System.Threading.Tasks;

    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;

    public class ContactsService : IContactsService
    {
        private readonly IRepository<ContactForm> contactFromRepository;

        public ContactsService(IRepository<ContactForm> contactFromRepository)
        {
            this.contactFromRepository = contactFromRepository;
        }

        public async Task Send(ContactFormInputViewModel contactFormEntryViewModel)
        {
            var form = new ContactFrom()
            {
                FullName = contactFormEntryViewModel.FullName,
                Email = contactFormEntryViewModel.Email,
                Content = contactFormEntryViewModel.Content,
            };

            await this.contactFromRepository.AddAsync(form);
            await this.contactFromRepository.SaveChangesAsync();
        }
    }
}
