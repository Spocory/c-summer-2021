using System.Linq;

namespace Ziggle.Repository
{
    public interface ICategoryRepository
    {
        CategoryModel[] Categories { get; }
        CategoryModel Category(int categoryId);
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryRepository : ICategoryRepository
    {
        public CategoryModel[] Categories
        {
            get
            {
                return DatabaseAccessor.Instance.Category
                                               .Select(t => new CategoryModel { Id = t.CategoryId, Name = t.CategoryName })
                                               .ToArray();
            }
        }

        public CategoryModel Category(int categoryId)
        {
            var category = DatabaseAccessor.Instance.Category
                                                   .Where(t => t.CategoryId == categoryId)
                                                   .Select(t => new CategoryModel { Id = t.CategoryId, Name = t.CategoryName })
                                                   .First();
            return category;
        }
    }
}