using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IPhonebookEntryRepository
    {
        IEnumerable<PhonebookEntry> GetPhonebookEntries(Guid phonebookId);
        IEnumerable<PhonebookEntry> GetPhonebookEntriesByName(Guid phonebookId, string name);
        Task<PhonebookEntry> GetById(Guid id);
        PhonebookEntry Update(PhonebookEntry phonebookEntry);
        PhonebookEntry Add(PhonebookEntry phonebookEntry);
        void Delete(Guid id);
        int GetCountOfPhonebookEntries();
        Task<int> Commit();
    }
}
