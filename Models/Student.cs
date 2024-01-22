
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;
namespace Projet2023.Models;

public class Student : Person
{
    [PrimaryKey]
    [AutoIncrement]
    [SQLite.Column("id")]
    public int Id {get; set;}
    [SQLite.Column("studentId")]
    public string StudentId {get; set;}
    [Ignore]
    public List<Evaluation> evaluations { get; private set; } = new List<Evaluation>();
    
   /* private static List<Student> Students = new List<Student>();
     public static List<Student> GetStudentList() {
        return Students;
    }
    public void AddEval(Evaluation evaluation){
        evaluations.Add(evaluation);
    }
    /*public Student(string firstName, string lastName, string email, string studentId):base(firstName,lastName,email){
        this.StudentId=studentId;
    }*/
}
