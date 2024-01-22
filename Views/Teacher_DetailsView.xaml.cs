using System.Collections.ObjectModel;
using Projet2023.Models;

namespace Projet2023;

public partial class Teacher_DetailsView : ContentPage
{
	private readonly TeacherFunctions _teacherFunctions;
    public ObservableCollection<Teacher> TeacherDetailsList { get; set; } = new ObservableCollection<Teacher>();

    public Teacher_DetailsView(TeacherFunctions teacherFunctions, Teacher teacher)
    {
        InitializeComponent();
        _teacherFunctions = teacherFunctions;

        // Assuming GetByIdAsync returns a Task<Teacher>
        Task.Run(async () => 
        {
            var newTeacher = await _teacherFunctions.GetByIdAsync(teacher.Id);
            TeacherDetailsList.Add(newTeacher);
        });

        BindingContext = this;
    }
}

