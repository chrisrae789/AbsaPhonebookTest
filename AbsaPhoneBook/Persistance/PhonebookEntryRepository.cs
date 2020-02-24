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
    public sealed class PhonebookEntryRepository : IPhonebookEntryRepository, IDisposable
    {
        private AbsaPhonebookContext context;

        public PhonebookEntryRepository(AbsaPhonebookContext context)
        {
            this.context = context;
        }

        public PhonebookEntry Add(PhonebookEntry phonebookEntry)
        {
            context.Attach(phonebookEntry);
            context.Add(phonebookEntry);
            return phonebookEntry;
        }

        public async Task<int> Commit()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Guid id)
        {
            var phonebookEntry = context.PhonebookEntry.FirstOrDefault(a => a.Id == id);
            if (phonebookEntry != null)
            {
                context.PhonebookEntry.Remove(phonebookEntry);
            }
            
        }

        public async Task<PhonebookEntry> GetById(Guid id)
        {
            return await context.PhonebookEntry.FindAsync(id);
        }

        public int GetCountOfPhonebookEntries()
        {
            return context.PhonebookEntry.Count();
        }

        public IEnumerable<PhonebookEntry> GetPhonebookEntries(Guid phonebookId)
        {
            return context.PhonebookEntry.Where(a => a.PhonebookId == phonebookId).ToList();
        }

        public IEnumerable<PhonebookEntry> GetPhonebookEntriesByName(Guid phonebookId, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return context.PhonebookEntry.Where(a => a.PhonebookId == phonebookId).Where(a => a.Name.Contains(name)).OrderBy(b=> b.Name).ToList();
            }
            return context.PhonebookEntry.Where(a => a.PhonebookId == phonebookId).ToList();
        }

        public PhonebookEntry Update(PhonebookEntry phonebookEntry)
        {
            var entity = context.PhonebookEntry.Attach(phonebookEntry);
            entity.State = EntityState.Modified;
            return phonebookEntry;
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
    }
}
