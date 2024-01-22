
namespace Projet2023;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		InitializeDatabase();
		MainPage = new AppShell();
		
	}
	private void InitializeDatabase()
    {
        LocalDBService.InitializeDatabaseAsync().Wait(); 
    }
}
