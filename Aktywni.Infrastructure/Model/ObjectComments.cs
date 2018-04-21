using System;
using System.Collections.Generic;

namespace Aktywni.Infrastructure.Model
{
    public partial class ObjectComments
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ObjectId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Objects Object { get; set; }
        public Users User { get; set; }
    }
}
