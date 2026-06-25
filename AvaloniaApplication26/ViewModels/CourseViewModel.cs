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
            set { _newCourseName = value; OnPropertyChanged(); }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public RelayCommand AddCourseCommand { get; }
        public RelayCommand DeleteCourseCommand { get; }

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

        public void AddCourse(Course course)
        {
            _courseService.AddCourse(course);
            LoadCourses();
        }

        public void DeleteCourse(int courseId)
        {
            _courseService.DeleteCourse(courseId);
            LoadCourses();
        }

        public void DeleteCourse(Course course)
        {
            if (course == null) return;
            _courseService.DeleteCourse(course.Id);
            LoadCourses();
        }

        private void AddCourse()
        {
            if (string.IsNullOrWhiteSpace(NewCourseName)) return;
            var c = new Course
            {
                CourseName = NewCourseName,
                Description = "",
                Price = 0,
                Duration = 0
            };
            _courseService.AddCourse(c);
            LoadCourses();
            NewCourseName = "";
        }
    }
}


