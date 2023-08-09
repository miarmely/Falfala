using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class UserView
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? LastName { get; set; }
        public string? TelNo { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public string? Job { get; set; }
        public string? Sex { get; set; }
        public string? BirthDate { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Password { get; set; }
    }
}
