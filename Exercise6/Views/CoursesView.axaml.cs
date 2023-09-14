using Avalonia.Controls;
using Exercise6.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise6.Views
{
    public partial class CoursesView : UserControl
    {
        public CoursesView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<CoursesViewModel>();
        }
    }
}
