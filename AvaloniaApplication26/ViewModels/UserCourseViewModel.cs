using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AvaloniaApplication26.Models;

namespace AvaloniaApplication26.ViewModels
{
        public class UserCourseViewModel
        {
            private readonly UserCourseService _userCourseService;

            public ObservableCollection<Course> UserCourses { get; } = new();

            public RelayCommand EnrollCommand { get; }
            public RelayCommand UnenrollCommand { get; }

            private User _currentUser = null!;   
            private Course _currentCourse = null!; 

            public UserCourseViewModel(UserCourseService userCourseService)
            {
                _userCourseService = userCourseService;

                EnrollCommand = new RelayCommand(Enroll);
                UnenrollCommand = new RelayCommand(Unenroll);
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
                _currentCourse = course;
            }

            private void Enroll()
            {
                if (_currentUser == null || _currentCourse == null) return;
                _userCourseService.AddUserToCourse(_currentUser.Id, _currentCourse.Id);
                LoadCoursesForUser(_currentUser);
            }

            private void Unenroll()
            {
                if (_currentUser == null || _currentCourse == null) return;
                _userCourseService.RemoveUserFromCourse(_currentUser.Id, _currentCourse.Id);
                LoadCoursesForUser(_currentUser);
            }
        }
    }
