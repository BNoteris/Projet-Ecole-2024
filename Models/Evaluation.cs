using Projet2023.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Projet2023.Models;

public class Evaluation
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id {get; set;}    
     [Column("note")]    
    public virtual int Note {get; set;}
    
    [ForeignKey(typeof(Activity))]
    public int Activity_Id { get; set; }
    [Column("activityName")]
    public string ActivityName { get; set; }

     [ManyToOne]
    public Activity activity { get; set; }
    [ForeignKey(typeof(Student))]
    public int Student_Id { get; set; }
    [Column("StudentName")]
    public string StudentName { get; set; }
    [ManyToOne]
    public Student student { get; set; }

   
    
}



