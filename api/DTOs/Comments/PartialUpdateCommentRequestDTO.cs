using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comments
{
    public class PartialUpdateCommentRequestDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}