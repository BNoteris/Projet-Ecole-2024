namespace Projet2023;
using Projet2023.Models;

public partial class Activity_ListView : ContentPage
{
	private readonly ActivityFunctions _activityFunctions;
	private readonly TeacherFunctions _teacherFunctions;
	public Activity_ListView(ActivityFunctions activityFunctions, TeacherFunctions teacherFunctions)
	{
		InitializeComponent();
		BindingContext = this;
		Task.Run(async () => listActivityView.ItemsSource = await _activityFunctions.GetAllAsync());
		 _activityFunctions = activityFunctions;
		 _teacherFunctions = teacherFunctions;
	}

	private int _editActivityId;
    private async void saveActivityButton_Clicked(object sender, EventArgs e){
		 if (string.IsNullOrWhiteSpace(ectsEntryField.Text) ||
        string.IsNullOrWhiteSpace(activityNameEntryField.Text) ||
        string.IsNullOrWhiteSpace(codeEntryField.Text) ||
        string.IsNullOrWhiteSpace(teacherEntryField.Text))
    {
        DisplayAlert("Error", "All fields are required", "OK");
        return;
    }
		if (_editActivityId == 0){
			string lastName = teacherEntryField.Text;
			await _activityFunctions.CreateAsync(new Activity{
				ECTS = ectsEntryField.Text,
				Code = codeEntryField.Text,
				ActivityName = activityNameEntryField.Text,
				TeacherName = teacherEntryField.Text
			}, lastName);
		}
		else{
			string lastName = teacherEntryField.Text;
			await _activityFunctions.UpdateAsync(new Activity{
				Id= _editActivityId,
				ECTS = ectsEntryField.Text,
				Code = codeEntryField.Text,
				ActivityName = activityNameEntryField.Text,
				TeacherName = teacherEntryField.Text
			}, lastName);
			_editActivityId = 0;
		}
		ectsEntryField.Text = string.Empty;
		codeEntryField.Text= string.Empty;
		activityNameEntryField.Text = string.Empty;
		teacherEntryField.Text = string.Empty;
		listActivityView.ItemsSource = await _activityFunctions.GetAllAsync();
    }

private async void ListActivityView_ItemTapped(object sender, ItemTappedEventArgs e){
		var activity = (Activity)e.Item;

		var action = await DisplayActionSheet("Action", "Cancel", null, "Edit","Details", "Delete");

		switch(action){
			case "Edit":

				_editActivityId = activity.Id;
				ectsEntryField.Text = activity.ECTS;
				activityNameEntryField.Text = activity.ActivityName;
				codeEntryField.Text = activity.Code;
				teacherEntryField.Text = activity.TeacherName;

				break;
			case "Delete":

				await _activityFunctions.DeleteAsync(activity);
				listActivityView.ItemsSource = await _activityFunctions.GetAllAsync();

				break;	
			case "Details":
			var Activity_Details = new Activity_DetailsView(_activityFunctions, activity);
			await Navigation.PushAsync(Activity_Details);
			break;
		}
	}
	 
}