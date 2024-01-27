using Projet2023.Models;
using SQLite;

namespace Projet2023.Models;

public class Teacher:Person
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id {get; set;}
    [Column("salary")]
    public string Salary {get; set;}
    [Column("teacherId")]
    public string TeacherId {get; set;}
}
