using Projet2023.Models;
using SQLite;

namespace Projet2023;

public class ActivityFunctions 
{
    private readonly SQLiteAsyncConnection _connection;

    public ActivityFunctions(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        return await _connection.Table<Activity>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<Activity> GetByIdAsync(int id)
    {
        return await _connection.Table<Activity>().Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task CreateAsync(Activity activity, string lastName)
    {
         Teacher teacher = await _connection.Table<Teacher>()
                                       .Where(t => t.LastName == lastName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);
         if (teacher == null)
    {
       return;   // permet de ne pas créer de teacher inexistant dans la database (point d'amélioration)
    }     
        int teacherId = teacher.Id;
        activity.Teacher_Id = teacherId;            
        await _connection.InsertAsync(activity);
    
    }

    public async Task UpdateAsync(Activity activity, string lastName)
    {
       Teacher teacher = await _connection.Table<Teacher>()
                                       .Where(t => t.LastName == lastName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);     
     if (teacher == null) // permet de ne pas créer de teacher inexistant dans la database (point d'amélioration)
    {
       return;
    }     
        int teacherId = teacher.Id;
        activity.Teacher_Id = teacherId;
        await _connection.UpdateAsync(activity);
    }

    public async Task DeleteAsync(Activity activity)
    {
        await _connection.DeleteAsync(activity).ConfigureAwait(false);
    }
}
