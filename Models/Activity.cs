using SQLite;
namespace Projet2023.Models;

public class Activity
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id {get; set;}
    [Column("activityname")]
    public string ActivityName {get; set;}
    [Column("ECTS")]
    public string ECTS {get; set;}
    [Column("code")]
    public string Code{get; set;}
    [Column("teacher")]
    public string TeacherName{get; set;}

     [Column("teacher_id")]
    public int Teacher_Id { get; set; }

    [Ignore]
    public Teacher Teacher { get; set; }
    
}

