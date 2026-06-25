using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AvaloniaApplication26.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {
        public UserViewModel UserViewModel { get; }
        public CourseViewModel CourseViewModel { get; }
        public UserCourseViewModel UserCourseViewModel { get; }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                UserViewModel.SelectedUser = value;
                UserCourseViewModel.LoadCoursesForUser(value);
                RaiseCommandsCanExecuteChanged();
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                CourseViewModel.SelectedCourse = value;
                UserCourseViewModel.SetCurrentCourse(value);
                RaiseCommandsCanExecuteChanged();
            }
        }

        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand AddCourseCommand { get; }
        public RelayCommand DeleteCourseCommand { get; }
        public RelayCommand EnrollCommand { get; }
        public RelayCommand UnenrollCommand { get; }

        public MainWindowViewModel()
        {
            var db = new DatabaseService();
            var userService = new UserService(db);
            var courseService = new CourseService(db);
            var userCourseService = new UserCourseService(db);

            UserViewModel = new UserViewModel(userService);
            CourseViewModel = new CourseViewModel(courseService);
            UserCourseViewModel = new UserCourseViewModel(userCourseService);

            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, () => SelectedUser != null);
            AddCourseCommand = new RelayCommand(AddCourse);
            DeleteCourseCommand = new RelayCommand(DeleteCourse, () => SelectedCourse != null);
            EnrollCommand = new RelayCommand(Enroll, () => SelectedUser != null && SelectedCourse != null);
            UnenrollCommand = new RelayCommand(Unenroll, () => SelectedUser != null && SelectedCourse != null);
        }

        public void RaiseCommandsCanExecuteChanged()
        {
            AddUserCommand?.RaiseCanExecuteChanged();
            DeleteUserCommand?.RaiseCanExecuteChanged();
            AddCourseCommand?.RaiseCanExecuteChanged();
            DeleteCourseCommand?.RaiseCanExecuteChanged();
            EnrollCommand?.RaiseCanExecuteChanged();
            UnenrollCommand?.RaiseCanExecuteChanged();
        }

        private void AddUser()
        {
            if (string.IsNullOrWhiteSpace(UserViewModel.NewUserName) || string.IsNullOrWhiteSpace(UserViewModel.NewUserSurname))
                return;
            var user = new User
            {
                Name = UserViewModel.NewUserName,
                Surname = UserViewModel.NewUserSurname,
                ClassNumber = "11A",
                City = "Москва",
                StudentPhone = "",
                MotherName = "",
                FatherName = "",
                MotherPhone = "",
                FatherPhone = ""
            };
            UserViewModel.AddUser(user);
            UserViewModel.NewUserName = "";
            UserViewModel.NewUserSurname = "";
        }

        private void DeleteUser()
        {
            if (SelectedUser == null) return;
            UserCourseViewModel.DeleteAllCoursesForUser(SelectedUser.Id);
            UserViewModel.DeleteUser(SelectedUser.Id);
            UserCourseViewModel.UserCourses.Clear();
        }

        private void AddCourse()
        {
            if (string.IsNullOrWhiteSpace(CourseViewModel.NewCourseName)) return;
            var course = new Course
            {
                CourseName = CourseViewModel.NewCourseName,
                Description = "",
                Price = 0,
                Duration = 0
            };
            CourseViewModel.AddCourse(course);
            CourseViewModel.NewCourseName = "";
        }

        private void DeleteCourse()
        {
            if (SelectedCourse == null) return;
            UserCourseViewModel.DeleteAllUsersForCourse(SelectedCourse.Id);
            CourseViewModel.DeleteCourse(SelectedCourse.Id);
        }

        private void Enroll()
        {
            if (SelectedUser == null || SelectedCourse == null) return;
            UserCourseViewModel.AddUserToCourse(SelectedUser.Id, SelectedCourse.Id);
            UserCourseViewModel.LoadCoursesForUser(SelectedUser);
        }

        private void Unenroll()
        {
            if (SelectedUser == null || SelectedCourse == null) return;
            UserCourseViewModel.RemoveUserFromCourse(SelectedUser.Id, SelectedCourse.Id);
            UserCourseViewModel.LoadCoursesForUser(SelectedUser);
        }
    }

    public class RelayCommand : System.Windows.Input.ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged;

        // ⬅️ ЭТОТ МЕТОД ДОБАВЛЕН
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
    

