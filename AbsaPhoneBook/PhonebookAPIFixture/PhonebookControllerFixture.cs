using Domain.Entities;
using Domain.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistance;
using Persistance.Context;
using Persistance.Interfaces;
using Phonebook.API.Controllers;
using Phonebook.API.Controllers.EntityDTOs;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAPIFixture
{
    [TestFixture]
    public class PhonebookControllerFixture
    {
        DbContextOptions<AbsaPhonebookContext> options;
        IPhonebookRepository repository;
        IPhonebookEntryRepository entryRepository;
        Domain.Entities.Phonebook phonebook;
        Domain.Entities.PhonebookEntry phonebookEntry;
        PhonebookController controller;

        private PhonebookEntry CreateEntry(Domain.Entities.Phonebook phonebook, string name, string number)
        {
            var entry = PhonebookEntryFactory.Create(phonebook);
            entry.Name = name;
            entry.PhoneNumber = number;
            return entry;
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AbsaPhonebookContext>().UseInMemoryDatabase(databaseName: "AbsaPhonebook").Options;
            var context = new AbsaPhonebookContext(options);
            context.RemoveRange(context.Phonebook);
            context.RemoveRange(context.PhonebookEntry);
            context.SaveChanges();

            phonebook = PhonebookFactory.Create();
            phonebook.Name = "Stellenbosch Directory";

            phonebook.PhonebookEntries.Add(CreateEntry(phonebook, "Dave Worthington", "021979452"));
            phonebookEntry = CreateEntry(phonebook, "Bill Lumsden", "0215554334");
            phonebook.PhonebookEntries.Add(phonebookEntry);
            repository = new PhonebookRepository(context);
            entryRepository = new PhonebookEntryRepository(context);

            repository.Add(phonebook);
            repository.Commit();

            controller = new PhonebookController(repository, entryRepository);
        }

        [Test]
        public void CanGetPhonebookByName()
        {
            var result = controller.Get("Stell");
            Assert.AreEqual(1, result.Value.Count());
            Assert.AreEqual("Stellenbosch Directory", result.Value.First().Name);
        }

        [Test]
        public async Task CanGetPhonebookEntry()
        {   
            var result = await controller.GetEntry(phonebookEntry.Id);
            Assert.IsInstanceOf<ActionResult<PhonebookEntry>>(result);

            var realResult = result.Result as OkObjectResult;
           
            Assert.IsNotNull(realResult.Value);
            var checkEntry = realResult.Value as PhonebookEntry;
            Assert.AreEqual("Bill Lumsden", checkEntry.Name);
        }

        [Test]
        public void CanGetPhonebookEntries()
        {
            var result = controller.GetEntries(phonebook.Id, string.Empty);
            Assert.AreEqual(2, result.Value.Count());
        }

        [Test]
        public void CanPostPhonebook()
        {
            controller.Post("Wellington");

            var book = repository.GetPhonebooksByName("Well").FirstOrDefault();

            Assert.IsNotNull(book);
            Assert.AreEqual("Wellington", book.Name);
            Assert.IsNotNull(book.Id);
        }
        [Test]
        public async Task CanPostEntry()
        {
            var oldCount = this.entryRepository.GetCountOfPhonebookEntries();

            await controller.PostEntry(phonebook.Id, new PhonebookEntryDTO(new PhonebookEntry() { Name = "James Pond", PhoneNumber = "0317683242" }));

            Assert.AreNotEqual(oldCount, this.entryRepository.GetCountOfPhonebookEntries());
        }
    }
}