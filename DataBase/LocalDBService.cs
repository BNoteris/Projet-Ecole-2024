using SQLite;
using Projet2023.Models;
namespace Projet2023;

public class LocalDBService    //Classe de création de la connexion vers la database et création des tables dans la database
{
    private const string Database = "School.db3";
    private readonly SQLiteAsyncConnection _connection;

    public LocalDBService(){
        _connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Database));
       
        
    } 
    public async Task InitializeTablesAsync() 
    {
        await _connection.CreateTablesAsync<Student, Activity, Teacher, Evaluation>().ConfigureAwait(false);
        await _connection.CreateTablesAsync<Evaluation, Cote, Appreciation>().ConfigureAwait(false);
        
    }

    public static async Task InitializeDatabaseAsync()
    {
        LocalDBService databaseService = new LocalDBService();
        await databaseService.InitializeTablesAsync().ConfigureAwait(false);
    }
}
