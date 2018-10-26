﻿using Clubs.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDomain.Classes.ClubModels
{
    [Table("Member")]
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }
        public int AssociatedClub { get; set; }
        [ForeignKey("studentMember")]
        public string StudentNumber { get; set; }
        public bool approved { get; set; }
        [ForeignKey("AssociatedClub")]
        public virtual Club myClub { get; set; }
        public virtual Student studentMember { get; set; }

    }
}