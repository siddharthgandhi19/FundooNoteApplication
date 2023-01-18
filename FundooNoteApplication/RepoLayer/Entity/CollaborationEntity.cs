using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepoLayer.Entity
{
    public class CollaborationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long CollabId { get; set; }
        public string Email { get; set; }


        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }

        [ForeignKey("Note")]
        public long NoteID { get; set; }
        public virtual NoteEntity Note { get; set; }
    }
}
