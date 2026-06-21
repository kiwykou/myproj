using System;
using Dapper;
using System.Collections.Generic;
using System.Text;
using AvaloniaApplication26.Models;

namespace AvaloniaApplication26.Services
{
    public class CourseService
    {

        private readonly DatabaseService _databaseService;


        public CourseService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        public IEnumerable<Course> GetAllCourses()
        {
            using var connection = _databaseService.GetConnection();
            return connection.Query<Course>("SELECT * FROM Course");
        }

        public void AddCourse(Course course)
        {
            using var connection = _databaseService.GetConnection();
            string sql = @"
                INSERT INTO Course (CourseName, Description, Price, Duration)
                VALUES ($CourseName, $Description, $Price, $Duration)";
            connection.Execute(sql, course);
        }

        public void DeleteCourse(int courseId)
        {
            using var connection = _databaseService.GetConnection();
            connection.Execute("DELETE FROM Course WHERE Id = @Id", new { Id = courseId });
        }
    }
}
