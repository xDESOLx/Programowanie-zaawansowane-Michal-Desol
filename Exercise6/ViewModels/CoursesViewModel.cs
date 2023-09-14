using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exercise6.Data;
using Exercise6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    private readonly Exercise6Context _context;

    public CoursesViewModel(Exercise6Context context)
    {
        _context = context;
        Courses = _context.Courses
            .LoadAsync()
            .ContinueWith(t => _context.Courses.Local.ToObservableCollection());
    }

    [RelayCommand]
    private async Task AddCourseAsync()
    {
        if (NewCourse.Validate())
        {
            _context.Add(NewCourse);
            await _context.SaveChangesAsync();
            NewCourse = new Course();
        }
    }

    [RelayCommand]
    private async Task DeleteCourseAsync(Course course)
    {
        _context.Remove(course);
        await _context.SaveChangesAsync();
    }

    [RelayCommand]
    private async Task UpdateCourseAsync() => await _context.SaveChangesAsync();

    [ObservableProperty]
    private Course newCourse = new Course();

    private TaskNotifier<ObservableCollection<Course>> courses;
    public Task<ObservableCollection<Course>> Courses
    {
        get => courses;
        set => SetPropertyAndNotifyOnCompletion(ref courses, value);
    }
}
