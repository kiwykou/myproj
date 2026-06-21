using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System.ComponentModel;

namespace AvaloniaApplication26.ViewModels
{
    public class UserViewModel
    {

        private readonly UserService _userService;
        public ObservableCollection<User> Users { get; } = new();

        public string NewUserName { get; set; }
        public string NewUserSurname { get; set; }

        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }

        public UserViewModel(UserService userService)
        {
            _userService = userService;
            LoadUsers();

            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(() => DeleteUser(SelectedUser));
        }

        public User SelectedUser { get; set; }

        public void LoadUsers()
        {
            Users.Clear();
            foreach (var u in _userService.GetAllUsers())
                Users.Add(u);
        }

        private void AddUser()
        {
            if (string.IsNullOrWhiteSpace(NewUserName) || string.IsNullOrWhiteSpace(NewUserSurname))
                return;

            var u = new User
            {
                Name = NewUserName,
                Surname = NewUserSurname,
                ClassNumber = "11А",
                City = "Москва",
                StudentPhone = "",
                MotherName = "",
                FatherName = "",
                MotherPhone = "",
                FatherPhone = ""
            };
            _userService.AddUser(u);
            LoadUsers();
            NewUserName = "";
            NewUserSurname = "";
        }

        public void DeleteUser(User user)
        {
            if (user == null) return;
            _userService.DeleteUser(user.Id);
            LoadUsers();
        }
    }
}
