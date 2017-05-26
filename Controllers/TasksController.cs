using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private TodoAppContext _context;

        public TasksController(TodoAppContext context)
        {
            _context = context;
        }

        private Task FindById(int id)
        {
           return _context.Tasks.Include(x => x.Assignment).Single(x => x.TaskId == id);
        }

        // GET: api/tasks
        [HttpGet]
        public IActionResult GetAll()
        {
            // Project a list of DTOs for all Task objects             
            var tasks = _context.Tasks.Include(x => x.Assignment).ToList<Task>();
            return new ObjectResult(TaskToDTO(tasks));
        }

        // GET: api/tasks/3
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetById(int id)
        {
            Task task = FindById(id);
            if (task == null)
            {
                return NotFound();
            }
            return new ObjectResult(TaskToDTO(task));
        }

        // POST api/tasks
        [HttpPost]
        public IActionResult Create([FromBody] TaskDTO dto, int personId)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            
            Person person = _context.Persons.Single(x => x.PersonId == personId);
            if(person == null)
            {
                return BadRequest();
            }

            Task task = DTOToTask(dto);
            person.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtRoute("GetTask", new { controller = "Task", id = task.TaskId }, task);
        }

        // PUT api/tasks/3
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskDTO dto)
        {
            if (dto == null || dto.Id != id)
            {
                return BadRequest();
            }

            Task task = FindById(id);
            if (task == null)
            {
                return NotFound();
            }

            Task toUpdate = DTOToTask(dto);
            _context.Tasks.Update(toUpdate);
            _context.SaveChanges();
            return new NoContentResult();
        }
        
        // DELETE api/tasks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Task task = FindById(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
        
        private TaskDTO TaskToDTO(Task task)
        {
            TaskDTO dto = new TaskDTO();
            dto.Id = task.TaskId;
            dto.Title = task.Title;
            dto.DueDate = task.DueDate;
            dto.Priority = task.Priority;
            dto.Assignment = new Uri(@Url.Link("GetPerson", new { id = task.Assignment.PersonId }));
            return dto;
        }

        private List<TaskDTO> TaskToDTO(List<Task> tasks)
        {
            List<TaskDTO> dtos = new List<TaskDTO>();
            foreach(Task task in tasks)
            {
                dtos.Add(TaskToDTO(task));
            }
            return dtos;
        }
        
        private Task DTOToTask(TaskDTO dto)
        {
            Task task  = new Task();
            task.TaskId = dto.Id;
            task.Title = dto.Title;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;
            return task;
        }
    }
}