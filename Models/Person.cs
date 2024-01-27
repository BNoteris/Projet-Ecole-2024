using SQLite;
namespace Projet2023.Models;
[Table("person")]
public class Person
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id {get; set;}
    [Column("firstname")]
    public string FirstName {get; set;}
    [Column("lastname")]
    public string LastName {get; set;}
    [Column("email")]
    public string Email {get; set;}
}
