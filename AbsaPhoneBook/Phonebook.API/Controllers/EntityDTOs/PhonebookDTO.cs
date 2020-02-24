using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Controllers.EntityDTOs
{
    public sealed class PhonebookDTO
    {
        public PhonebookDTO() { }
        public string Id { get; set; }
        public string Name { get; set; }

        //TO DO :  Use Automapper custom resolver
        public void SetID(Domain.Entities.Phonebook phonebook)
        {
            try
            {
                var bookid = Guid.Parse(Id);
                phonebook.SetId(bookid);
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
