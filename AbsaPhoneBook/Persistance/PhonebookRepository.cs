using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public sealed class PhonebookRepository : IPhonebookRepository, IDisposable
    {
        private AbsaPhonebookContext context;

        public PhonebookRepository(AbsaPhonebookContext context)
        {
            this.context = context;
        }

        public Phonebook Add(Phonebook phonebook)
        {
            context.Attach(phonebook);
            context.Add(phonebook);
          
            return phonebook;
        }

        public async Task<int> Commit()
        {
            try
            {
               return  await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Guid id)
        {
            var phonebook = context.Phonebook.Include(a => a.PhonebookEntries).FirstOrDefault(a => a.Id == id);
            if (phonebook != null)
            {
                context.Remove<Phonebook>(phonebook);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
             
            }
        }

        public async Task<Phonebook> GetById(Guid id)
        {
            return await context.Phonebook.FindAsync(id);
        }

        public int GetCountOfPhonebooks()
        {
            return context.Phonebook.Count();
        }

        public IEnumerable<Phonebook> GetPhonebooksByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return context.Phonebook.Where(a => a.Name.Contains(name)).OrderBy(b=> b.Name).ToList();
            }
            return context.Phonebook.ToList();
        }

        public Phonebook Update(Phonebook phonebook)
        {
            var trackedPhonebook = context.Phonebook.FirstOrDefault(a => a.Id == phonebook.Id);
            trackedPhonebook.Name = phonebook.Name;
            context.Entry(trackedPhonebook).State = EntityState.Modified;

            return trackedPhonebook;
        }

        public int GetCountOfPhonebookEntries(Guid id)
        {
           return context.PhonebookEntry.Where(a => a.PhonebookId == id).Count();
        }
    }
}
