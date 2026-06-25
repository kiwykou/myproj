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
            }
        }

        public MainWindowViewModel()
        {
            var db = new DatabaseService();
            var userService = new UserService(db);
            var courseService = new CourseService(db);
            var userCourseService = new UserCourseService(db);

            UserViewModel = new UserViewModel(userService);
            CourseViewModel = new CourseViewModel(courseService);
            UserCourseViewModel = new UserCourseViewModel(userCourseService);
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
    }
}

