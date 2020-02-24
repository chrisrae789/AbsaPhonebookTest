using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Context;
using Persistance.Interfaces;
using Phonebook.API.Controllers.EntityDTOs;

namespace Phonebook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        readonly IPhonebookRepository phonebookRepository;
        readonly IPhonebookEntryRepository phonebookEntryRepository;

        public PhonebookController(IPhonebookRepository phonebookRepository, IPhonebookEntryRepository entryRepository)
        {
            this.phonebookRepository = phonebookRepository;
            this.phonebookEntryRepository = entryRepository;
        }

        // GET: api/Phonebook/{name}
        [HttpGet]
        public ActionResult<List<Domain.Entities.Phonebook>> Get(string name)
        {
            var phoneBooks = phonebookRepository.GetPhonebooksByName(name);
            return phoneBooks.ToList();
        }

        // GET: api/Phonebook/GUID
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Domain.Entities.Phonebook>> Get(Guid id)
        {
            var phonebook = await phonebookRepository.GetById(id);
            if (phonebook == null)
                return NotFound();
            return Ok(phonebook);
        }

        // POST: api/Phonebook
        [Route("create/{name}")]
        [HttpPost()]
        public async Task<IActionResult> Post([FromRoute] string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phonebook = PhonebookFactory.Create();
            phonebook.Name = name;
            phonebookRepository.Add(phonebook);
          
            await phonebookRepository.Commit();
            return Ok();
        }

        // PUT: api/Phonebook/GUID
        [Route("update/{id}")]
        [HttpPut()]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] PhonebookDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phonebook = await phonebookRepository.GetById(id);
            if (phonebook == null)
                return NotFound();

            var updatedBook = PhonebookFactory.Create();
            updatedBook.Name = value.Name;
            value.SetID(updatedBook);

            phonebookRepository.Update(updatedBook);
            await phonebookRepository.Commit();

            return NoContent();
        }

        // DELETE: api/ApiWithActions/GUID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                 BadRequest(ModelState);
            phonebookRepository.Delete(id);
            await phonebookRepository.Commit();

            return Ok();
        }

        ////Entry
        //// GET: api/Phonebook/{name}
        [Route("entries/byphonebook/{phonebookid}")]
        [HttpGet("{{name}}")]
        public ActionResult<IList<PhonebookEntry>> GetEntries(Guid phonebookId, string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var phoneBookEntries = phonebookEntryRepository.GetPhonebookEntriesByName(phonebookId, name);
            return phoneBookEntries.ToList();
        }

        // GET: api/Phonebook/GUID
        [Route("entries/{entryid}")]
        [HttpGet("entries/{entryid}")]
        public async Task<ActionResult<PhonebookEntry>> GetEntry(Guid entryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entry = await phonebookEntryRepository.GetById(entryId);
            if (entry == null)
                return NotFound();

            return Ok(entry);
        }

       //POST: api/Phonebook/entries
       [Route("entries/byphonebook/create/{phonebookId}")]
       [HttpPost()]
        public async Task<ActionResult> PostEntry(Guid phonebookId, [FromBody] PhonebookEntryDTO entry)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var phonebook = await phonebookRepository.GetById(phonebookId);
            if (phonebook == null)
                return NotFound();
            var phonebookEntry = PhonebookEntryFactory.Create(phonebook);
            phonebookEntry.Name = entry.Name;
            phonebookEntry.PhoneNumber = entry.PhoneNumber;
            //.NET returned empty Guid from JSON request
            //TO DO : Use Automapper 

            phonebookEntryRepository.Add(phonebookEntry);
            await phonebookEntryRepository.Commit();
            return Ok();
        }

        // PUT: api/Phonebook/entries/GUID
        [Route("entries/byphonebook/update/{phonebookId}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntry(Guid phonebookId, [FromBody] PhonebookEntryDTO entry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var phonebook = await phonebookRepository.GetById(phonebookId);
            if (phonebook == null)
                NotFound();
            var phonebookEntry = PhonebookEntryFactory.Create(phonebook);
            entry.SetIds(phonebookEntry);
            phonebookEntry.Name = entry.Name;
            phonebookEntry.PhoneNumber = entry.PhoneNumber;
            phonebookEntryRepository.Update(phonebookEntry);
            await phonebookEntryRepository.Commit();

            return Ok();
        }

        // DELETE: api/Entries/ApiWithActions/GUID
        [Route("entries/{id}")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            phonebookEntryRepository.Delete(id);
            await phonebookEntryRepository.Commit();
            
            return Ok();
        }
    }
}
