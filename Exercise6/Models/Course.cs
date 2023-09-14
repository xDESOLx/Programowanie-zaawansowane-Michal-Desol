using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6.Models;

public partial class Course : ObservableValidator
{
    [ObservableProperty]
    private int id;

    [Required(ErrorMessage = "Nazwa przedmiotu jest wymagana")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string title;

    [ObservableProperty]
    private ObservableCollection<Student> students;

    public override string? ToString() => Title;

    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
