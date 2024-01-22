using Projet2023.Models;
using SQLite;

namespace Projet2023;

public class EvaluationFunctions    
{
    private readonly SQLiteAsyncConnection _connection;

    public EvaluationFunctions(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Evaluation>> GetAllAsync()
    {
        var AllCotes = await _connection.Table<Cote>().ToListAsync().ConfigureAwait(false);
        var AllAppreciations = await _connection.Table<Appreciation>().ToListAsync().ConfigureAwait(false);
        var AllEval = new List<Evaluation>();
        AllEval.AddRange(AllAppreciations);
        AllEval.AddRange(AllCotes);
        return AllEval;
    }
    public async Task<List<Evaluation>> GetAllByStudentIdAsync(int studentId)
{
    var cotes = await _connection.Table<Cote>().Where(c => c.Student_Id == studentId).ToListAsync().ConfigureAwait(false);
    var appreciations = await _connection.Table<Appreciation>().Where(a => a.Student_Id == studentId).ToListAsync().ConfigureAwait(false);

    var allEvaluations = new List<Evaluation>();
    allEvaluations.AddRange(appreciations);
    allEvaluations.AddRange(cotes);

    return allEvaluations;
} 

    public async Task<Evaluation> GetByIdAsync(int id)
    {
        return await _connection.Table<Evaluation>().Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task CreateAsync(Evaluation evaluation, string lastName, string activityName)
    {
         Student student = await _connection.Table<Student>()
                                       .Where(t => t.LastName == lastName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);
        if (student == null)
    {
       return; // permet de ne pas créer de student inexistant dans la database (point d'amélioration)
    }           
        int studentId = student.Id;
        evaluation.Student_Id = studentId;
                    
        Activity activity = await _connection.Table<Activity>()
                                       .Where(t => t.ActivityName == activityName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);
        if (activity == null)
    {
        return; // permet de ne pas créer de activity inexistant dans la database (point d'amélioration)
    }           
        int activityId = activity.Id;
        evaluation.Activity_Id = activityId;
        
        await _connection.InsertAsync(evaluation);
    
    }

    public async Task UpdateAsync(Evaluation evaluation, string lastName, string activityName)
    {
       Student student = await _connection.Table<Student>()
                                       .Where(t => t.LastName == lastName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);
        if (student == null)
    {
       return;   // permet de ne pas créer de student inexistant dans la database (point d'amélioration)
    }           
        int studentId = student.Id;
        evaluation.Student_Id = studentId;

       Activity activity = await _connection.Table<Activity>()
                                       .Where(t => t.ActivityName == activityName)
                                       .FirstOrDefaultAsync()
                                       .ConfigureAwait(false);   
      if (activity == null)
    {
        return; // permet de ne pas créer de activity inexistant dans la database (point d'amélioration)
    }           
        int activityId = activity.Id;
        evaluation.Activity_Id = activityId;
        await _connection.UpdateAsync(evaluation);
    }                                                              
       
    
    public async Task DeleteAsync(Evaluation evaluation)
    {
        await _connection.DeleteAsync(evaluation).ConfigureAwait(false);
    }
}
