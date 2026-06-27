using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System.ComponentModel;

namespace AvaloniaApplication26.ViewModels
{

    public class UserViewModel : ViewModelBase
    {
        private readonly UserService _userService;
        public ObservableCollection<User> Users { get; } = new();

        
        private string _newUserName;
        public string NewUserName
        {
            get => _newUserName;
            set { _newUserName = value; OnPropertyChanged(); }
        }

        private string _newUserSurname;
        public string NewUserSurname
        {
            get => _newUserSurname;
            set { _newUserSurname = value; OnPropertyChanged(); }
        }

        private string _newClassNumber;
        public string NewClassNumber
        {
            get => _newClassNumber;
            set { _newClassNumber = value; OnPropertyChanged(); }
        }

        private string _newCity;
        public string NewCity
        {
            get => _newCity;
            set { _newCity = value; OnPropertyChanged(); }
        }

        private string _newStudentPhone;
        public string NewStudentPhone
        {
            get => _newStudentPhone;
            set { _newStudentPhone = value; OnPropertyChanged(); }
        }

        private string _newMotherName;
        public string NewMotherName
        {
            get => _newMotherName;
            set { _newMotherName = value; OnPropertyChanged(); }
        }

        private string _newFatherName;
        public string NewFatherName
        {
            get => _newFatherName;
            set { _newFatherName = value; OnPropertyChanged(); }
        }

        private string _newMotherPhone;
        public string NewMotherPhone
        {
            get => _newMotherPhone;
            set { _newMotherPhone = value; OnPropertyChanged(); }
        }

        private string _newFatherPhone;
        public string NewFatherPhone
        {
            get => _newFatherPhone;
            set { _newFatherPhone = value; OnPropertyChanged(); }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }

        public UserViewModel(UserService userService)
        {
            _userService = userService;
            LoadUsers();

            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(() => DeleteUser(SelectedUser));
        }

        public void LoadUsers()
        {
            Users.Clear();
            foreach (var u in _userService.GetAllUsers())
                Users.Add(u);
        }

        public void AddUser(User user)
        {
            _userService.AddUser(user);
            LoadUsers();
        }

        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            LoadUsers();
        }

        public void DeleteUser(User user)
        {
            if (user == null) return;
            _userService.DeleteUser(user.Id);
            LoadUsers();
        }

        private void AddUser()
        {

            if (string.IsNullOrWhiteSpace(NewUserName) || string.IsNullOrWhiteSpace(NewUserSurname))
                return;

            var u = new User
            {
                Name = NewUserName,
                Surname = NewUserSurname,
                ClassNumber = NewClassNumber,        
                City = NewCity,                     
                StudentPhone = NewStudentPhone,      
                MotherName = NewMotherName,          
                FatherName = NewFatherName,          
                MotherPhone = NewMotherPhone,        
                FatherPhone = NewFatherPhone         
            };

            _userService.AddUser(u);
            LoadUsers();

            // Очищаем все поля
            NewUserName = "";
            NewUserSurname = "";
            NewClassNumber = "";
            NewCity = "";
            NewStudentPhone = "";
            NewMotherName = "";
            NewFatherName = "";
            NewMotherPhone = "";
            NewFatherPhone = "";
        }
    }
}
             
