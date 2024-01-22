using Projet2023.Models;
using SQLite;
namespace Projet2023;

public class TeacherFunctions : DataFunctions<Teacher>
{
    private readonly SQLiteAsyncConnection _connection;

    public TeacherFunctions(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Teacher>> GetAllAsync()
    {
        return await _connection.Table<Teacher>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<Teacher> GetByIdAsync(int id)
    {
        return await _connection.Table<Teacher>().Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task CreateAsync(Teacher teacher)
    {
        await _connection.InsertAsync(teacher).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        await _connection.UpdateAsync(teacher).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Teacher teacher)
    {
        await _connection.DeleteAsync(teacher).ConfigureAwait(false);
    }

      public async  Task<int> GetIdByNameAsync(string lastName){
        Teacher teacher = await _connection.Table<Teacher>().Where(s => s.LastName == lastName)
            .FirstOrDefaultAsync();

       int  teacherId = teacher.Id;  
       return teacherId;
    }
}
