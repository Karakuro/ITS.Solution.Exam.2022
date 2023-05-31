namespace ITS.Solution.Exam._2022.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Producer { get; set; }
        public string Grade { get; set; }
        public int Points { get; set; }
        public string Ingredients { get; set; }
        public List<ProductModel> BetterProducts { get; set;}
    }
}
