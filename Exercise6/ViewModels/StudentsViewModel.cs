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

public partial class StudentsViewModel : ViewModelBase
{
    private readonly Exercise6Context _context;

    public StudentsViewModel(Exercise6Context context)
    {
        _context = context;
        Students = _context.Students
            .LoadAsync()
            .ContinueWith(t => _context.Students.Local.ToObservableCollection());
    }

    [RelayCommand]
    private async Task AddStudentAsync()
    {
        if (NewStudent.Validate())
        {
            _context.Add(NewStudent);
            await _context.SaveChangesAsync();
            NewStudent = new Student();
        }
    }

    [RelayCommand]
    private async Task DeleteStudentAsync(Student student)
    {
        _context.Remove(student);
        await _context.SaveChangesAsync();
    }

    [RelayCommand]
    private async Task UpdateStudentAsync() => await _context.SaveChangesAsync();

    [ObservableProperty]
    private Student newStudent = new Student();

    private TaskNotifier<ObservableCollection<Student>> students;
    public Task<ObservableCollection<Student>> Students
    {
        get => students;
        set => SetPropertyAndNotifyOnCompletion(ref students, value);
    }
}
