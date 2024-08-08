using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhotoProduct
    {
        [Key]
        public string PhotoProductId { get; set; }
        public byte[] PhotoData { get; set; }



        public string ProductId { get; set; }
        //public Product? Product { get; set; }
    }
}
