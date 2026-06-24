using System;
using Dapper;
using System.Collections.Generic;
using System.Text;
using AvaloniaApplication26.Models;

namespace AvaloniaApplication26.Services
{

    public class UserService
    {
        private readonly DatabaseService _databaseService;

        public UserService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<User> GetAllUsers()
        {
            using var connection = _databaseService.GetConnection();
            return connection.Query<User>("SELECT * FROM User");
        }

        public void AddUser(User user)
        {
            using var connection = _databaseService.GetConnection();
            string sql = @"
                INSERT INTO User (Name, Surname, ClassNumber, City, StudentPhone, MotherName, FatherName, MotherPhone, FatherPhone)
                VALUES (@Name, @Surname, @ClassNumber, @City, @StudentPhone, @MotherName, @FatherName, @MotherPhone, @FatherPhone)";
            connection.Execute(sql, user);
        }

        public void DeleteUser(int userId)
        {
            using var connection = _databaseService.GetConnection();
            connection.Execute("DELETE FROM User WHERE Id = @Id", new { Id = userId });
        }
    }
}
