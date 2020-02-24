using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factories
{
    public static class PhonebookFactory 
    {
        public static Phonebook Create()
        {
            var phonebook = new Phonebook();
            return phonebook;
        }
    }
}
