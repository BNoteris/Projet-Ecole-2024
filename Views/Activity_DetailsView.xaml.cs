namespace Projet2023;

using System.Collections.ObjectModel;
using Projet2023.Models;


public partial class Activity_DetailsView : ContentPage
{
private readonly ActivityFunctions _activityFunctions;
    public ObservableCollection<Activity> ActivityDetailsList { get; set; } = new ObservableCollection<Activity>();

    public Activity_DetailsView(ActivityFunctions activityFunctions, Activity activity)
    {
        InitializeComponent();
        _activityFunctions = activityFunctions;

        // Assuming GetByIdAsync returns a Task<Activity>
        Task.Run(async () => 
        {
            var newActivity = await _activityFunctions.GetByIdAsync(activity.Id);
            ActivityDetailsList.Add(newActivity);
        });

        BindingContext = this;
		
    }
}	