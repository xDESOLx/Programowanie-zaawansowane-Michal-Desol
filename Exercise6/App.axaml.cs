using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Exercise6.Data;
using Exercise6.Models;
using Exercise6.ViewModels;
using Exercise6.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Exercise6;

public partial class App : Application
{
    public IServiceProvider Services { get; private set; }
    public new static App Current => (App)Application.Current!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public override void RegisterServices()
    {
        var services = new ServiceCollection();
        services.AddDbContext<Exercise6Context>(options => options.UseSqlite("Data Source=Exercise6.db"));
        services.AddTransient<MainViewModel>();
        services.AddTransient<StudentsViewModel>();
        services.AddTransient<CoursesViewModel>();
        services.AddTransient<StudentAssignmentsViewModel>();
        Services = services.BuildServiceProvider();
        if (!Design.IsDesignMode)
        {
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Exercise6Context>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                    var student = new Student()
                    {
                        FirstName = "Michał",
                        LastName = "Desol",
                        IndexNo = "129908"
                    };
                    var course = new Course()
                    {
                        Title = "Programowania Zaawansowane"
                    };
                    var studentAssignment = new StudentAssignment()
                    {
                        Student = student,
                        Course = course
                    };
                    if (!context.Students.Any())
                    {
                        context.Students.Add(student);
                    }

                    if (!context.Courses.Any())
                    {
                        context.Courses.Add(course);
                    }

                    if (!context.StudentAssignments.Any())
                    {
                        context.StudentAssignments.Add(studentAssignment);
                    }

                    if (context.ChangeTracker.HasChanges())
                    {
                        context.SaveChanges();
                    }
                }
            }
        }        

        base.RegisterServices();
    }
}
