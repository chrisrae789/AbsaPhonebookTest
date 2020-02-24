using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    [Serializable]
    public class Phonebook : Entity
    {
        public Phonebook()
        {
            PhonebookEntries = new HashSet<PhonebookEntry>();
        }

        protected override void DisposeEntity()
        {
            base.DisposeEntity();
            PhonebookEntries = null;
        }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<PhonebookEntry> PhonebookEntries { get; set; }

        
    }
}
