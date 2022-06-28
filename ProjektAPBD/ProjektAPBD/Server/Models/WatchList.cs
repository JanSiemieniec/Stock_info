using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAPBD.Server.Models
{
    public class WatchList
    {
        [Key]
        public int IdWatchList { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public int IdStock { get; set; }
        public DateTime Data { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("IdStock")]
        public StockTab StockTab { get; set; }
    }
}
