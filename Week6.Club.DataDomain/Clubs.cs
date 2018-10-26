using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.Club.DataDomain
{
    [Table("Club")]
    public class Clubs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }
        public int adminID { get; set; }
        public virtual ICollection<Members> clubMembers { get; set; }
    }
}
