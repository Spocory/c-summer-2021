using System.Linq;
using Learning.Repository;

namespace Learning.Business
{
    public interface IEnrollManager
    {
        EnrollModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        EnrollModel[] GetAll(int userId);
    }

    public class EnrollModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class EnrollManager : IEnrollManager
    {
        private readonly IEnrollRepository enrollRepository;

        public EnrollManager(IEnrollRepository enrollRepository)
        {
            this.enrollRepository = enrollRepository;
        }

        public EnrollModel Add(int userId, int classId)
        {
            
            if (classId != 0)
            {
                var item = enrollRepository.Add(userId, classId);

                return new EnrollModel
                {
                    ClassId = item.ClassId,
                    UserId = item.UserId,
                };
            }
            return null;

        }

        public EnrollModel[] GetAll(int UserId)
        {
            var items = enrollRepository.GetAll(UserId)
                .Select(t => {
                    var classx = ClassRepository.GetClass(t.ClassId);

                    return new EnrollModel
                    {
                        Name = classx.Name,
                        Price = classx.Price,
                        Description = classx.Description
                    };
                })
                .ToArray();

            return items;
        }



        public bool Remove(int userId, int classId)
        {
            return enrollRepository.Remove(userId, classId);
        }
    }
}