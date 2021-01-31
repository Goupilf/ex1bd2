using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestORMCodeFirst.Entities
{
    [Table("COURS")]
    public class Cours
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string CodeCours { get; set; }


        [Column(TypeName = "varchar(15)")]
        public string NomCours { get; set; }


        public virtual ICollection<InscriptionCours> InscriptionCours { get; set; }

        public Cours() 
        {
            InscriptionCours = new List<InscriptionCours>();
        }
    }
}
