using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PracticeWebApp.Models
{
    [Table("post")]
    public class Post
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Column("message")]
        [MaxLength(200)]
        public string Message { get; set; }
        [Column("createdtimestamputc")]
        public DateTime CreatedTimeStampUtc { get; set; }
        [Column("updatedtimestamputc")]
        public DateTime? UpdatedTimeStampUtc { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
