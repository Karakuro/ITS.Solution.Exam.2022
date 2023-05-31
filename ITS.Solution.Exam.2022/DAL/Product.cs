using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITS.Solution.Exam._2022.DAL
{
    public class Product
    {
        [Key]
        [StringLength(13, MinimumLength = 13)]
        public string Barcode { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Producer { get; set; }
        public string Ingredients { get; set; }
        [Column(TypeName = "nchar(1)")]
        public string Nutriscore_Grade { get; set; }
        public int Nutriscore_Value { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
