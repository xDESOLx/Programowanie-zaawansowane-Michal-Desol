using Avalonia.Controls;
using Exercise6.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise6.Views
{
    public partial class StudentAssignmentsView : UserControl
    {
        public StudentAssignmentsView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<StudentAssignmentsViewModel>();
        }
    }
}
