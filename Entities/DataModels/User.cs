using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.DataModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? LastName { get; set; }

        [Column(TypeName = "char(11)")]
        public string? TelNo { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }

        [Column(TypeName = "tinyint")]
        public int Age { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Job { get; set; }

        public bool Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set;}

        [Column(TypeName = "tinyint")]
        public int StatusId { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string? Password { get; set; }
    }
}
