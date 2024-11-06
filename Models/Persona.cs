using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpradoT5A.Models
{
    [Table("persona")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int IdPersona { get; set; }

        [MaxLength(45)]
        public string NombrePersona { get; set; }

        public Persona() { 
        
            IdPersona = 0;
            NombrePersona = string.Empty;
        }
    }

}
