using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [MinLength(4), MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [MinLength(4), MaxLength(250)]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? PetId { get; set; }
        [ForeignKey("PetId")]
        public LostPet? LostPet { get; set; }
    }
}