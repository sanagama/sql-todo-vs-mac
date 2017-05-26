using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private TodoAppContext _context;

        public PersonsController(TodoAppContext context)
        {
            _context = context;
        }

        private Person FindById(int id)
        {
           return _context.Persons.Include(x => x.Tasks).Single(x => x.PersonId == id);
        }

        // GET: api/persons
        [HttpGet]
        public IActionResult GetAll()
        {
            // Project a list of DTOs for all Person objects             
            var persons = _context.Persons.Include(x => x.Tasks).ToList<Person>();
            return new ObjectResult(PersonToDTO(persons));
        }

        // GET: api/persons/3
        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult GetById(int id)
        {
            Person person = FindById(id);
            if (person == null)
            {
                return BadRequest();
            }
            return new ObjectResult(PersonToDTO(person));
        }

        // POST api/persons
        [HttpPost]
        public IActionResult Create([FromBody] PersonDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            Person person = DTOToPerson(dto);
            _context.Persons.Add(person);
            _context.SaveChanges();
            return CreatedAtRoute("GetPerson", new { controller = "Person", id = person.PersonId }, person);
        }

        // PUT api/persons/3
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PersonDTO dto)
        {
            if (dto == null || dto.Id != id)
            {
                return BadRequest();
            }

            Person person = FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            Person toUpdate = DTOToPerson(dto);
            _context.Persons.Update(toUpdate);
            _context.SaveChanges();
            return new NoContentResult();
        }
        
        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Person person = FindById(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
        
        private PersonDTO PersonToDTO(Person person)
        {
            PersonDTO dto = new PersonDTO();
            dto.Id = person.PersonId;
            dto.FirstName = person.FirstName;
            dto.LastName = person.LastName;
            
            dto.Tasks = new List<Uri>();
            foreach( Task t in person.Tasks)
            {
                dto.Tasks.Add(new Uri(@Url.Link("GetTask", new { id = t.TaskId })));
            }
            return dto;
        }

        private List<PersonDTO> PersonToDTO(List<Person> persons)
        {
            List<PersonDTO> dtos = new List<PersonDTO>();
            foreach(Person person in persons)
            {
                dtos.Add(PersonToDTO(person));
            }
            return dtos;
        }
        
        private Person DTOToPerson(PersonDTO dto)
        {
            Person person = new Person();
            person.PersonId = dto.Id;
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            return person;
        }
    }
}