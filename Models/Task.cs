using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public bool IsComplete { get; set; }
        public virtual Person Assignment { get; set; }
    }
}