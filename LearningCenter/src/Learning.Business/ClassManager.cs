using System.Linq;
using Learning.Repository;

namespace Learning.Business
{
    public interface IClassManager
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

        public ClassModel(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }


    }

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }




        public ClassModel[] Classes
        {
            get
            {
                return classRepository.Classes
                                         .Select(t => new ClassModel(t.Id, t.Name, t.Price, t.Description))
                                         .ToArray();
            }
        }

        public ClassModel Class(int classId)
        {
            var classModel = classRepository.Class(classId);
            return new ClassModel(classModel.Id, classModel.Name, classModel.Price, classModel.Description);
        }


        //public ClassModel GetClass(int classId)
        //{
        //    var classx = ClassManager.GetClass(classId);

        //    return new ClassModel(classx.Id,classx.Name,classx.Price,classx.Description)
        //    {
        //        Id = classx.Id,
        //        Name = classx.Name,
        //        Price = classx.Price,
        //        Description = classx.Description
        //    };
        //}


    }
}