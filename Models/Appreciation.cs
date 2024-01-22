using Projet2023.Models;
using SQLite;
namespace Projet2023.Models;
using SQLiteNetExtensions.Attributes;

public class Appreciation : Evaluation
{
    
    public string appreciation {get; set;}



    private int[] table_Note = {20, 16, 12, 8, 4};
    private string[] table_Appreciation = {"TB", "B", "c", "N", "X"};
    public string [] Table_Appreciation{
        get{return table_Appreciation;}
    }
    public void SetAppreciation(string newappreciation)
    {
        appreciation = newappreciation;
    }
    [Ignore]
    public override int Note 
    {get{          
        if (appreciation == "X"){return 20;}
        else if (appreciation == "TB"){return 16;}
        else if (appreciation == "B")
        {
            return 12;
        }
        else if (appreciation == "C")
        {
            return 8;
        }
        else if (appreciation == "N")
        {
            return 4;
        }
        else
        {
            return 0;
        }
        ;}
    set{base.Note =value;}
    }
}

