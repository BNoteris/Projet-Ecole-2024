
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Projet2023.Models;

public class Cote : Evaluation
{
    
     public int note{get; set;}  

  
    [Ignore]
     public override int Note{
        get{return base.Note= note;}
        set{base.Note= value;}
        
    }
}
