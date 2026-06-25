using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AvaloniaApplication26.Models;

namespace AvaloniaApplication26.ViewModels
{

    public class UserCourseViewModel : ViewModelBase
    {
        private readonly UserCourseService _userCourseService;
        public ObservableCollection<Course> UserCourses { get; } = new();

        private User _currentUser;

       
        private Course _selectedUserCourse;
        public Course SelectedUserCourse
        {
            get => _selectedUserCourse;
            set
            {
                _selectedUserCourse = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EnrollCommand { get; }
        public RelayCommand UnenrollCommand { get; }

        public UserCourseViewModel(UserCourseService userCourseService)
        {
            _userCourseService = userCourseService;
            EnrollCommand = new RelayCommand(Enroll, () => _currentUser != null && SelectedUserCourse != null);
            UnenrollCommand = new RelayCommand(Unenroll, () => _currentUser != null && SelectedUserCourse != null);
        }

        public void LoadCoursesForUser(User user)
        {
            _currentUser = user;
            UserCourses.Clear();
            if (user == null) return;
            foreach (var c in _userCourseService.GetCoursesForUser(user.Id))
                UserCourses.Add(c);
        }

        public void SetCurrentCourse(Course course)
        {
            SelectedUserCourse = course;
        }

        public void AddUserToCourse(int userId, int courseId)
        {
            _userCourseService.AddUserToCourse(userId, courseId);
        }

        public void RemoveUserFromCourse(int userId, int courseId)
        {
            _userCourseService.RemoveUserFromCourse(userId, courseId);
        }

        public void DeleteAllCoursesForUser(int userId)
        {
            _userCourseService.DeleteAllCoursesForUser(userId);
        }

        public void DeleteAllUsersForCourse(int courseId)
        {
            _userCourseService.DeleteAllUsersForCourse(courseId);
        }

        private void Enroll()
        {
            if (_currentUser == null || SelectedUserCourse == null) return;
            _userCourseService.AddUserToCourse(_currentUser.Id, SelectedUserCourse.Id);
            LoadCoursesForUser(_currentUser);
        }

        private void Unenroll()
        {
            if (_currentUser == null || SelectedUserCourse == null) return;
            _userCourseService.RemoveUserFromCourse(_currentUser.Id, SelectedUserCourse.Id);
            LoadCoursesForUser(_currentUser);
        }
    }
}