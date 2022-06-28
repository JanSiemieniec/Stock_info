using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAPBD.Server.Models
{
    public class StockTab
    {
        [Key]
        [Required]
        public int IdStock { get; set; }
        [Required]
        [MaxLength(120)]
        public string Ticker { get; set; }
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Homepage { get; set; }
        [Required]
        [MaxLength(100)]
        public string Industry { get; set; }
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        public string Logo { get; set; }
        public ICollection<WatchList> Users { get; set; }


    }
}
