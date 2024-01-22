using Projet2023.Models;
using Microsoft.Data.Sqlite;
using SQLite;
namespace Projet2023;

public class StudentFunctions : DataFunctions<Student>
{
    private readonly SQLiteAsyncConnection _connection;

    public StudentFunctions(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        var Students = await _connection.Table<Student>().ToListAsync().ConfigureAwait(false);
        return Students;
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _connection.Table<Student>().Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task CreateAsync(Student student)
    {
        await _connection.InsertAsync(student).ConfigureAwait(false);
    }

    public async Task UpdateAsync(Student student)
    {
        await _connection.UpdateAsync(student).ConfigureAwait(false);
    }

    public async Task DeleteAsync(Student student)
    {
        await _connection.DeleteAsync(student).ConfigureAwait(false);
    }
    
    
    public async  Task<int> GetIdByNameAsync(string lastName){
       Student student = await _connection.Table<Student>().Where(s => s.LastName == lastName)
            .FirstOrDefaultAsync();

       int  studentId = student.Id;  
       return studentId;
    }
    

}
