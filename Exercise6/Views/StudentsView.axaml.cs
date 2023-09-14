using Avalonia.Controls;
using Exercise6.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise6.Views
{
    public partial class StudentsView : UserControl
    {
        public StudentsView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<StudentsViewModel>();         
        }
    }
}
