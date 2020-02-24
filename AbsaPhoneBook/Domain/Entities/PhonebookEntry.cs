using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleToAttribute("PhonebookAPIFixture")]

namespace Domain.Entities
{
    [Serializable]
    public class PhonebookEntry : Entity
    {
        internal PhonebookEntry()
        {
        }

        public PhonebookEntry(Phonebook phonebook)
        {
            PhonebookId = phonebook.Id;
        }
        public void SetPhonebookId(Guid bookId)
        {
            PhonebookId = bookId;
        }
        public Guid PhonebookId { get; internal set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string PhoneNumber { get; set; }
        public virtual Phonebook Phonebook { get; set; }
    }
}
