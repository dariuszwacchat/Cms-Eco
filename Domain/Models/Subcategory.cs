using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subcategory
    {
        [Key]
        public string SubcategoryId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int? IloscOdwiedzin { get; set; }
        //public int? Kolejnosc { get; set; } 

        public string? CategoryId { get; set; }
        //public Category? Category { get; set; }


        //public List<Product> Products { get; set; }
        //public List<Subsubcategory> Subsubcategories { get; set; }

    }
}
