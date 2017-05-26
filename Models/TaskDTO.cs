using System;

namespace TodoApp.Models
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public Uri Assignment { get; set; }
    }
}