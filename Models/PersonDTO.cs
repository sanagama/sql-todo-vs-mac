using System;
using System.Collections.Generic;

namespace TodoApp.Models
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Uri> Tasks { get; set; }
    }
}