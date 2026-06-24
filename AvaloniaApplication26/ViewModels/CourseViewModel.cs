using AvaloniaApplication26.Models;
using AvaloniaApplication26.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AvaloniaApplication26.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {

        private readonly CourseService _courseService;
        public ObservableCollection<Course> Courses { get; } = new();

        private string _newCourseName;
        public string NewCourseName
        {
            get => _newCourseName;
            set
            {
                _newCourseName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCourseCommand { get; }
        public RelayCommand DeleteCourseCommand { get; }

        public Course SelectedCourse { get; set; }

        public CourseViewModel(CourseService courseService)
        {
            _courseService = courseService;
            LoadCourses();

            AddCourseCommand = new RelayCommand(AddCourse);
            DeleteCourseCommand = new RelayCommand(() => DeleteCourse(SelectedCourse));
        }

        public void LoadCourses()
        {
            Courses.Clear();
            foreach (var c in _courseService.GetAllCourses())
                Courses.Add(c);
        }

        private void AddCourse()
        {
            if (string.IsNullOrWhiteSpace(NewCourseName)) return;
            var c = new Course { CourseName = NewCourseName, Description = "", Price = 0, Duration = 0 };
            _courseService.AddCourse(c);
            LoadCourses();
            NewCourseName = "";
        }

        public void DeleteCourse(Course course)
        {
            if (course == null) return;
            _courseService.DeleteCourse(course.Id);
            LoadCourses();
        }
    }
}

