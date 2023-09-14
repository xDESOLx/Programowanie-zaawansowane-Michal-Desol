using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6.Models;

public partial class StudentAssignment : ObservableValidator
{
    [ObservableProperty]
    private int id;

    [Required(ErrorMessage = "Student jest wymagany")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private Student student;

    [ObservableProperty]
    private int studentId;

    [Required(ErrorMessage = "Przedmiot jest wymagany")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private Course course;

    [ObservableProperty]
    private int courseId;

    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
