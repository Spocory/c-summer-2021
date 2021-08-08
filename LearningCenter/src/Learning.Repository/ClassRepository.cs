using System.Linq;

namespace Learning.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel Class(int classId);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Class
                                               .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Price = t.ClassPrice, Description = t.ClassDescription })
                                               .ToArray();
            }
        }
        public ClassModel Class(int classId)
        {
            var classx = DatabaseAccessor.Instance.Class
                                                   .Where(t => t.ClassId == classId)
                                                   .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Price = t.ClassPrice, Description = t.ClassDescription })
                                                   .First();
            return classx;
        }
    }
}