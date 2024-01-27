namespace Projet2023;

using System.Collections.ObjectModel;
using Projet2023.Models;

public partial class Student_DetailsView : ContentPage
{
	private readonly StudentFunctions _studentFunctions;
    private readonly EvaluationFunctions _evaluationFunctions;
	public ObservableCollection<Student> StudentDetailsList { get; set; } = new ObservableCollection<Student>();

	
     public Student_DetailsView(StudentFunctions studentFunctions, EvaluationFunctions evaluationFunctions, Student student)
    {
        InitializeComponent();
        _studentFunctions = studentFunctions;
        _evaluationFunctions = evaluationFunctions;
		 Task.Run(async () => 
        {
            var newStudent = await _studentFunctions.GetByIdAsync(student.Id);
            StudentDetailsList.Add(newStudent);
        });
        _ = InitializeAsync(student);
		_ = BulletinAsync(student); 

        BindingContext = this;
        Task.Run(async () => listEvaluationView.ItemsSource = await _evaluationFunctions.GetAllByStudentIdAsync(student.Id));
    }

 	private int studentId =  0;
	private string studentName;
	Student student1 = new();
    private async Task InitializeAsync(Student student)				//Permet d'avoir l'id et le Nom de l'étudiant dans la database
    {
        var newStudent = await _studentFunctions.GetByIdAsync(student.Id);
		studentName = student.LastName;
        studentId = student.Id;
		student1 = student;
    }

	private async Task BulletinAsync(Student student)				
    {
        double totalNotes =0;
		double average =0;
		List<Evaluation> allEval = await _evaluationFunctions.GetAllByStudentIdAsync(student.Id);
		foreach(var evaluation in allEval){
			 totalNotes += evaluation.Note;
		}
		int numberOfEvaluations = allEval.Count;
        average = numberOfEvaluations > 0 ? totalNotes / numberOfEvaluations : 0;
		var Average = average.ToString("F1");
		 Bulletin.Text = $"Average note of the student: {Average}/20";
		
		    }

	
 
	 private void AddEntryButton_Clicked(object sender, EventArgs e)
        {
           
            coteFrame.IsVisible = !coteFrame.IsVisible;
            appreciationFrame.IsVisible = !appreciationFrame.IsVisible;
        }
    private int _editEvaluationId;
    

    private async void saveCoteButton_Clicked(object sender, EventArgs e){
		 if (string.IsNullOrWhiteSpace(activityNameEntryField.Text) ||
        string.IsNullOrWhiteSpace(coteEntryField.Text))
    {
        DisplayAlert("Error", "All fields are required", "OK");
        return;
    }
		coteFrame.IsVisible = !coteFrame.IsVisible;
        appreciationFrame.IsVisible = !appreciationFrame.IsVisible;
		string ActivityName = activityNameEntryField.Text;
		int note = Convert.ToInt32(coteEntryField.Text);
		if (note>20){
			 DisplayAlert("Error", "This note is not an acceptable value", "OK");
        return;
		}
		if (_editEvaluationId == 0){
			await _evaluationFunctions.CreateAsync(new Cote{
				note = note,
				ActivityName = ActivityName,
				StudentName = studentName
			}, studentName, ActivityName );
		}
		else{
			await _evaluationFunctions.UpdateAsync(new Cote{
				Id= _editEvaluationId,
				note = note,
				ActivityName = ActivityName,
				StudentName = studentName
			}, studentName, ActivityName );
			_editEvaluationId = 0;
		}
		coteEntryField.Text = string.Empty;
		activityNameEntryField.Text = string.Empty;
		listEvaluationView.ItemsSource = await _evaluationFunctions.GetAllByStudentIdAsync(studentId);
		_ = BulletinAsync(student1); 

    }
	private async void saveAppreciationButton_Clicked(object sender, EventArgs e){
		 if (string.IsNullOrWhiteSpace(activityNameEntryField1.Text) ||
        string.IsNullOrWhiteSpace(appreciationEntryField.Text))
    {
        DisplayAlert("Error", "All fields are required", "OK");
        return;
    }
		coteFrame.IsVisible = !coteFrame.IsVisible;
        appreciationFrame.IsVisible = !appreciationFrame.IsVisible;
		string ActivityName = activityNameEntryField1.Text;
		string appreciation = appreciationEntryField.Text;
		if (_editEvaluationId == 0){
			await _evaluationFunctions.CreateAsync(new Appreciation{
				appreciation = appreciation,
				ActivityName = ActivityName,
				StudentName = studentName
			}, studentName, ActivityName );
		}
		else{
			await _evaluationFunctions.UpdateAsync(new Appreciation{
				Id= _editEvaluationId,
				appreciation = appreciation,
				ActivityName = ActivityName,
				StudentName = studentName
			}, studentName, ActivityName );
			_editEvaluationId = 0;
		}
		appreciationEntryField.Text = string.Empty;
		activityNameEntryField1.Text = string.Empty;
		listEvaluationView.ItemsSource = await _evaluationFunctions.GetAllByStudentIdAsync(studentId);
		_ = BulletinAsync(student1); 
		 
    }

private async void ListEvaluationView_ItemTapped(object sender, ItemTappedEventArgs e){
		var evaluation = (Evaluation)e.Item;
		var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
		switch(action){
			case "Edit":
				 coteFrame.IsVisible = !coteFrame.IsVisible;
            	appreciationFrame.IsVisible = !appreciationFrame.IsVisible;
				_editEvaluationId = evaluation.Id;
				activityNameEntryField.Text = evaluation.ActivityName;
				coteEntryField.Text = Convert.ToString(evaluation.Note);
				
				

				break;
			case "Delete":

				await _evaluationFunctions.DeleteAsync(evaluation);
				listEvaluationView.ItemsSource =await _evaluationFunctions.GetAllByStudentIdAsync(studentId);
				_ = BulletinAsync(student1);

				break;	
		}
	}
}
