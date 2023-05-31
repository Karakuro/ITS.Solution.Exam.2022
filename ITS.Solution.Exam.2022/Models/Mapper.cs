using ITS.Solution.Exam._2022.DAL;

namespace ITS.Solution.Exam._2022.Models
{
    public class Mapper
    {
        public ProductModel MapEntityToModel(Product entity)
        {
            ProductModel model = new ProductModel()
            {
                Producer = entity.Producer,
                Points = entity.Nutriscore_Value,
                Grade = entity.Nutriscore_Grade,
                CategoryName = entity.Category?.Name ?? "NO CATEGORY",
                Ingredients = entity.Ingredients,
                Name = entity.Name
            };
            return model;
        }
    }
}
