using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exercise6.Data;
using Exercise6.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6.ViewModels;

public partial class StudentAssignmentsViewModel : ViewModelBase
{
    private readonly Exercise6Context _context;

    public StudentAssignmentsViewModel(Exercise6Context context)
    {
        _context = context;
        Students = _context.Students
            .LoadAsync()
            .ContinueWith(t => _context.Students.Local.ToObservableCollection());
        Courses = _context.Courses
            .LoadAsync()
            .ContinueWith(t => _context.Courses.Local.ToObservableCollection());
        StudentAssignments = _context.StudentAssignments
            .LoadAsync()
            .ContinueWith(t => _context.StudentAssignments.Local.ToObservableCollection());
    }

    [RelayCommand]
    private async Task AddStudentAssignmentAsync()
    {
        if (NewStudentAssignment.Validate())
        {
            if (StudentAssignments.Result.Any(sa => sa.Student == NewStudentAssignment.Student && sa.Course == NewStudentAssignment.Course))
            {
                await MessageBoxManager.GetMessageBoxStandard("Błąd", "Student jest już przypisany do przedmiotu.", icon: MsBox.Avalonia.Enums.Icon.Error).ShowAsync();
            }
            else
            {
                _context.Add(NewStudentAssignment);
                await _context.SaveChangesAsync();
                NewStudentAssignment = new StudentAssignment();
            }            
        }
    }

    [RelayCommand]
    private async Task DeleteStudentAssignmentAsync(StudentAssignment studentAssignment)
    {
        _context.Remove(studentAssignment);
        await _context.SaveChangesAsync();
    }

    [ObservableProperty]
    private StudentAssignment newStudentAssignment = new StudentAssignment();

    private TaskNotifier<ObservableCollection<Student>> students;
    public Task<ObservableCollection<Student>> Students
    {
        get => students;
        set => SetPropertyAndNotifyOnCompletion(ref students, value);
    }

    private TaskNotifier<ObservableCollection<Course>> courses;
    public Task<ObservableCollection<Course>> Courses
    {
        get => courses;
        set => SetPropertyAndNotifyOnCompletion(ref courses, value);
    }

    private TaskNotifier<ObservableCollection<StudentAssignment>> studentAssignments;
    public Task<ObservableCollection<StudentAssignment>> StudentAssignments
    {
        get => studentAssignments;
        set => SetPropertyAndNotifyOnCompletion(ref studentAssignments, value);
    }
}
