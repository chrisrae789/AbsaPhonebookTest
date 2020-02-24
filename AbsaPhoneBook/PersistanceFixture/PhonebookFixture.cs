using Domain.Entities;
using Domain.Factories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistance;
using Persistance.Context;
using Persistance.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PersistanceFixture
{
    [TestFixture]
    public class PersistanceTests
    {
        DbContextOptions<AbsaPhonebookContext> options;
        IPhonebookRepository repository;
        IPhonebookEntryRepository entryRepository;
        Phonebook phonebook;
                
        private PhonebookEntry CreateEntry(Phonebook phonebook, string name, string number)
        {
            var entry = PhonebookEntryFactory.Create(phonebook);
            entry.Name = name;
            entry.PhoneNumber = number;
            return entry;
        }

        [SetUp]
        public void Setup()
        {
            //InMemory DB did not catch violation of foreign key constraint due to incorrect context configuration          
            options = new DbContextOptionsBuilder<AbsaPhonebookContext>().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AbsaPhonebook;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options;
            //"Data Source=(local);Initial Catalog=AbsaPhonebook;Integrated Security=True;" - MSSQL connection string
            var context = new AbsaPhonebookContext(options);
            context.RemoveRange(context.Phonebook);
            context.RemoveRange(context.PhonebookEntry);
            context.SaveChanges();

            phonebook = PhonebookFactory.Create();
            phonebook.Name = "Stellenbosch Directory";

            phonebook.PhonebookEntries.Add(CreateEntry(phonebook, "Dave Worthington", "021979452"));
            phonebook.PhonebookEntries.Add(CreateEntry(phonebook, "Bill Lumsden", "0215554334"));
            repository = new PhonebookRepository(context);
            entryRepository = new PhonebookEntryRepository(context);

            repository.Add(phonebook);
            repository.Commit();
        }

        [Test]
        public void CanCreatePhonebookWithEntries()
        {
            using (var context = new AbsaPhonebookContext(options))
            {
                Assert.AreEqual("Stellenbosch Directory", context.Phonebook.FirstOrDefault().Name);
                Assert.AreEqual(1, context.Phonebook.Count());
                Assert.AreEqual(2, context.PhonebookEntry.Count());
            }
        }

        [Test]
        public void CanGetPhonebooks()
        {
            var list = repository.GetPhonebooksByName("Stell");
            Assert.AreEqual(1, list.Count());
        }

        [Test]
        public void CanNotGetPhonebooks()
        {
            var list = repository.GetPhonebooksByName("UCT");
            Assert.AreEqual(0, list.Count());
        }

        [Test]
        public async Task CanRemovePhonebook()
        {
            repository.Delete(phonebook.Id);
            await repository.Commit();
            using (var context = new AbsaPhonebookContext(options))
            {
                 Assert.AreEqual(null, context.Phonebook.FirstOrDefault());
                Assert.AreEqual(0, context.PhonebookEntry.Count());
            }
        }

        [Test]
        public void CanRemovePhonebookEntry()
        {
            var phonebookEntry = phonebook.PhonebookEntries.Where(a => a.PhonebookId == phonebook.Id).First();
            entryRepository.Delete(phonebookEntry.Id);
            entryRepository.Commit();
            using (var context = new AbsaPhonebookContext(options))
            {
                Assert.AreEqual(1, context.PhonebookEntry.Count());
            }
        }

    }
}