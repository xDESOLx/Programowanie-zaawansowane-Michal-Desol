using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6.Models;

public partial class Student : ObservableValidator
{
    [ObservableProperty]
    private int id;

    [Required(ErrorMessage = "Imię jest wymagane")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string firstName;

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string lastName;
    [Required(ErrorMessage = "Numer albumu jest wymagany")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Numer albumu musi mieć 6 znaków")]
    [ObservableProperty]
    [NotifyDataErrorInfo]
    private string indexNo;

    [ObservableProperty]
    private ObservableCollection<Course> courses;

    public override string? ToString() => $"{FirstName} {LastName}";

    public bool Validate()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
