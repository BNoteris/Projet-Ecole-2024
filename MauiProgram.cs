
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SQLite;

namespace Projet2023;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<SQLiteAsyncConnection>(sp =>
		{
		string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "School.db3");
		return new SQLiteAsyncConnection(databasePath);
		});
		builder.Services.AddSingleton<LocalDBService>();	
		
		
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<Teacher_ListView>();
		builder.Services.AddTransient<Student_ListView>();
		builder.Services.AddTransient<Activity_ListView>();
		
		builder.Services.AddSingleton<ActivityFunctions>();
		
		builder.Services.AddSingleton<StudentFunctions>();
		builder.Services.AddSingleton<TeacherFunctions>();
		builder.Services.AddSingleton<EvaluationFunctions>();
		
		return builder.Build();
	}
}
