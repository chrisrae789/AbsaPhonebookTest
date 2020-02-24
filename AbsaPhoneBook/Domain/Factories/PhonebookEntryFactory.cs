using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
    public static class PhonebookEntryFactory
    {
        public static PhonebookEntry Create(Phonebook phonebook)
        {
            var entry = new PhonebookEntry(phonebook);
            return entry;
        }
    }
}
