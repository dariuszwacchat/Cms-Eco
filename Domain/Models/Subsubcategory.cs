using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Subsubcategory
    {
        [Key]
        public string SubsubcategoryId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int? IloscOdwiedzin { get; set; }
        //public int? Kolejnosc { get; set; }


        public string? CategoryId { get; set; }
        //public Category? Category { get; set; }

        public string? SubcategoryId { get; set; }
        //public Subcategory? Subcategory { get; set; }


        //public List<Product> Products { get; set; }

    }
}
