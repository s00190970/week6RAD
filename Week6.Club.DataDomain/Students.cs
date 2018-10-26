using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.Club.DataDomain
{
    [Table("Student")]
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentNumber { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
