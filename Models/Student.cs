
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
}
