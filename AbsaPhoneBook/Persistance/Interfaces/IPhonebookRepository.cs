using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Interfaces
{
    public interface IPhonebookRepository
    {
        IEnumerable<Phonebook> GetPhonebooksByName(string name);
        Task<Phonebook> GetById(Guid id);
        Phonebook Update(Phonebook phonebook);
        Phonebook Add(Phonebook phonebook);
        void Delete(Guid id);
        int GetCountOfPhonebooks();
        int GetCountOfPhonebookEntries(Guid id);
        Task<int> Commit();
    }
}
