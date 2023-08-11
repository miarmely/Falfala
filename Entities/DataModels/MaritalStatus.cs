using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class MaritalStatus
    {
        [Column(TypeName = "tinyint")]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(12)")]
        public string? StatusName { get; set; }
    }
}
