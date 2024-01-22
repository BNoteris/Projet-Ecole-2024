
using Microsoft.Toolkit.Mvvm.Input;
using Projet2023.Models;
namespace Projet2023;

public partial class Teacher_ListView : ContentPage
{
	private readonly TeacherFunctions _teacherFunctions;
	public Teacher_ListView(TeacherFunctions teacherFunctions)
	{
		InitializeComponent();
		BindingContext = this;
		_teacherFunctions= teacherFunctions;
		Task.Run(async () => listTeacherView.ItemsSource = await _teacherFunctions.GetAllAsync());
	}
	private int _editTeacherId;

	private async void saveTeacherButton_Clicked(object sender, EventArgs e){
		 if (string.IsNullOrWhiteSpace(firstNameTEntryField.Text) ||
        string.IsNullOrWhiteSpace(lastNameTEntryField.Text)||
        string.IsNullOrWhiteSpace(emailTEntryField.Text)||
        string.IsNullOrWhiteSpace(teacherIdEntryField.Text)||
        string.IsNullOrWhiteSpace(salaryEntryField.Text))
    {
        DisplayAlert("Error", "All fields are required", "OK");
        return;
    }
		if (_editTeacherId == 0){
			await _teacherFunctions.CreateAsync(new Teacher{
				FirstName = firstNameTEntryField.Text,
				LastName = lastNameTEntryField.Text,
				Email = emailTEntryField.Text,
				TeacherId = teacherIdEntryField.Text,
				Salary = salaryEntryField.Text
			});
		}
		else{
			await _teacherFunctions.UpdateAsync(new Teacher{
				Id= _editTeacherId,
				FirstName = firstNameTEntryField.Text,
				LastName = lastNameTEntryField.Text,
				Email = emailTEntryField.Text,
				TeacherId = teacherIdEntryField.Text,
				Salary = salaryEntryField.Text
			});
			_editTeacherId = 0;
		}
		firstNameTEntryField.Text = string.Empty;
		lastNameTEntryField.Text = string.Empty;
		emailTEntryField.Text = string.Empty;
		teacherIdEntryField.Text = string.Empty;
		salaryEntryField.Text = string.Empty;

		listTeacherView.ItemsSource = await _teacherFunctions.GetAllAsync();
	}
	private async void ListTeacherView_ItemTapped(object sender, ItemTappedEventArgs e){
		var teacher = (Teacher)e.Item;
		var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Details", "Delete");

		switch(action){
			case "Edit":

				_editTeacherId = teacher.Id;
				firstNameTEntryField.Text = teacher.FirstName;
				lastNameTEntryField.Text = teacher.LastName;
				emailTEntryField.Text = teacher.Email;
				teacherIdEntryField.Text = teacher.TeacherId;
				salaryEntryField.Text = teacher.Salary;

				break;
			case "Delete":

				await _teacherFunctions.DeleteAsync(teacher);
				listTeacherView.ItemsSource = await _teacherFunctions.GetAllAsync();

				break;	
			case "Details":
			var Teacher_Details = new Teacher_DetailsView(_teacherFunctions,teacher);
			await Navigation.PushAsync(Teacher_Details);
			break;	
		}
	}
}