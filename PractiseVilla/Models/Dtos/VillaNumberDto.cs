using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PractiseVilla.Models.Dtos
{
    public class VillaNumberDto
    {
        
        public int VillaNo { get; set; }
        
        public int VillaId { get; set; }
     
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
