using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string? FullName { get; set; }
        //public string? Photo { get; set; }
        public int? IloscOdwiedzin { get; set; }
        //public int? Kolejnosc { get; set; }


        //public List<Product>? Products { get; set; }
        //public List<Subcategory>? Subcategories { get; set; }
        //public List<Subsubcategory>? Subsubcategories { get; set; }
    }
}
