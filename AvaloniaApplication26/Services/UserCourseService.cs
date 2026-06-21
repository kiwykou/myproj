using System;
using Dapper;
using System.Collections.Generic;
using System.Text;
using AvaloniaApplication26.Models;
using System.Linq;

namespace AvaloniaApplication26.Services
{
    public class UserCourseService
    {
        private readonly DatabaseService _databaseService;
        public UserCourseService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public IEnumerable<Course>GetCoursesForUser(int  userId)
        {
            using var connection = _databaseService.GetConnection();
            var courseIds = connection.Query<int>(
                                            "SELECT CourseId FROM UserCourse WHERE UserId = $UserId",
                                            new { UserId = userId }
                );
            if (!courseIds.Any())
                return new List<Course>();
            return connection.Query<Course>(
                                   "SELECT * FROM Course WHERE Id IN $CourseIds",
                                   new { CourseIds = courseIds }
                );
        }
        public void AddUserToCourse(int  userId, int courseId)
        {
            using var connection = _databaseService.GetConnection();
            connection.Execute(
                "INSERT INTO UserCourse (UserId, CourseId) VALUES ($UserId, $CourseId)",
                new { UserId = userId, CourseId = courseId }
                );
        }
        public void RemoveUserFromCourse(int userId, int courseId)
        {
            using var connection = _databaseService.GetConnection();
            connection.Execute(
                "DELETE FROM UserCourse WHERE UserId = $UserId AND CourseId = @CourseId",
                new { UserId = userId, CourseId = courseId }
                );
        }
        public void DeleteAllCoursesForUser(int userId)
        {
            using var connection = _databaseService.GetConnection();
            connection.Execute(
                "DELETE FROM UserCourse WHERE UserId = $UserId",
                new { UserId = userId }
                );
        }
               
    }
}
