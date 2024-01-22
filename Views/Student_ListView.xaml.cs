using Projet2023.Models;

namespace Projet2023;

public partial class Student_ListView : ContentPage
{
	
	private readonly StudentFunctions _studentFunctions;
	private readonly EvaluationFunctions _evaluationFunctions;
	public Student_ListView(StudentFunctions studentFunctions,EvaluationFunctions evaluationFunctions)
	{
		InitializeComponent();
		BindingContext = this;
		Task.Run(async () => listStudentView.ItemsSource = await _studentFunctions.GetAllAsync());
		 _studentFunctions = studentFunctions;
		 _evaluationFunctions = evaluationFunctions;
		 
		 
	}

private int _editStudentId;
    private async void saveStudentButton_Clicked(object sender, EventArgs e){
		 if (string.IsNullOrWhiteSpace(firstNameEntryField.Text) ||
        string.IsNullOrWhiteSpace(lastNameEntryField.Text)||
        string.IsNullOrWhiteSpace(emailEntryField.Text)||
        string.IsNullOrWhiteSpace(studentIdEntryField.Text))
    {
        DisplayAlert("Error", "All fields are required", "OK");
        return;
    }
		if (_editStudentId == 0){
			await _studentFunctions.CreateAsync(new Student{
				FirstName = firstNameEntryField.Text,
				LastName = lastNameEntryField.Text,
				Email = emailEntryField.Text,
				StudentId = studentIdEntryField.Text
			});
		}
		else{
			await _studentFunctions.UpdateAsync(new Student{
				Id= _editStudentId,
				FirstName = firstNameEntryField.Text,
				LastName = lastNameEntryField.Text,
				Email = emailEntryField.Text,
				StudentId = studentIdEntryField.Text
			});
			_editStudentId = 0;
		}
		firstNameEntryField.Text = string.Empty;
		lastNameEntryField.Text = string.Empty;
		emailEntryField.Text = string.Empty;
		studentIdEntryField.Text = string.Empty;
		listStudentView.ItemsSource = await _studentFunctions.GetAllAsync();
    }
	
private async void ListStudentView_ItemTapped(object sender, ItemTappedEventArgs e){
		var student = (Student)e.Item;

		var action = await DisplayActionSheet("Action", "Cancel", null, "Edit","Details", "Delete");

		switch(action){
			case "Edit":

			
				_editStudentId = student.Id;
				firstNameEntryField.Text = student.FirstName;
				lastNameEntryField.Text = student.LastName;
				emailEntryField.Text = student.Email;
				studentIdEntryField.Text = student.StudentId;

				break;
			case "Delete":

				await _studentFunctions.DeleteAsync(student);
				listStudentView.ItemsSource = await _studentFunctions.GetAllAsync();

				break;	
			case "Details":
			var Student_Details = new Student_DetailsView(_studentFunctions,_evaluationFunctions,student);
			await Navigation.PushAsync(Student_Details);
			break;
		}
	}
	 
	
}