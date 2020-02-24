using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Controllers.EntityDTOs
{
    public sealed class PhonebookEntryDTO
    {
        public string Id { get; set; }
        public string PhonebookId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public PhonebookEntryDTO() { }
        public PhonebookEntryDTO(PhonebookEntry entry)
        {
            Id = entry.Id.ToString();
            PhonebookId = entry.PhonebookId.ToString();
            Name = entry.Name;
            PhoneNumber = entry.PhoneNumber;
        }

        public void SetIds(PhonebookEntry phonebookEntry)
        {
            try
            {
                var id = Guid.Parse(Id);
                phonebookEntry.SetId(id);
                var bookId = Guid.Parse(PhonebookId);
                phonebookEntry.SetPhonebookId(bookId);
            }
            catch (ArgumentNullException)
            {
                //TO DO: Log exception
                throw;
            }
            catch (FormatException)
            {
                //TO DO: Log exception
                throw;
            }

        }
    }
}
