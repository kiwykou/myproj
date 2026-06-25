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
        private readonly UserCourseService _userCourseService;  // ← ЭТО ПОЛЕ
        public ObservableCollection<Course> UserCourses { get; } = new();

        private User _currentUser;
        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public RelayCommand EnrollCommand { get; }
        public RelayCommand UnenrollCommand { get; }

        public UserCourseViewModel(UserCourseService userCourseService)
        {
            _userCourseService = userCourseService;  // ← СОХРАНЯЕМ
            EnrollCommand = new RelayCommand(Enroll, () => _currentUser != null && SelectedCourse != null);
            UnenrollCommand = new RelayCommand(Unenroll, () => _currentUser != null && SelectedCourse != null);
        }

        public void LoadCoursesForUser(User user)
        {
            _currentUser = user;
            UserCourses.Clear();
            if (user == null) return;
            foreach (var c in _userCourseService.GetCoursesForUser(user.Id))  // ← _userCourseService
                UserCourses.Add(c);
        }

        public void SetCurrentCourse(Course course) => SelectedCourse = course;

        // Методы, которые вызывает MainWindowViewModel
        public void AddUserToCourse(int userId, int courseId)
        {
            _userCourseService.AddUserToCourse(userId, courseId);  // ← _userCourseService
        }

        public void RemoveUserFromCourse(int userId, int courseId)
        {
            _userCourseService.RemoveUserFromCourse(userId, courseId);  // ← _userCourseService
        }

        public void DeleteAllCoursesForUser(int userId)
        {
            _userCourseService.DeleteAllCoursesForUser(userId);  // ← _userCourseService
        }

        public void DeleteAllUsersForCourse(int courseId)
        {
            _userCourseService.DeleteAllUsersForCourse(courseId);  // ← _userCourseService
        }

        private void Enroll()
        {
            if (_currentUser == null || SelectedCourse == null) return;
            _userCourseService.AddUserToCourse(_currentUser.Id, SelectedCourse.Id);  // ← _userCourseService
            LoadCoursesForUser(_currentUser);
        }

        private void Unenroll()
        {
            if (_currentUser == null || SelectedCourse == null) return;
            _userCourseService.RemoveUserFromCourse(_currentUser.Id, SelectedCourse.Id);  // ← _userCourseService
            LoadCoursesForUser(_currentUser);
        }
    }
}