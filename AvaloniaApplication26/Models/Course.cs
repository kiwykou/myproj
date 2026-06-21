using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaApplication26.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
