using System.ComponentModel.DataAnnotations;

namespace ITS.Solution.Exam._2022.DAL
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
