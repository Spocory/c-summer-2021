using System.Linq;
using Learning.Repository;

namespace Learning.Business
{
    public interface IEnrollManager
    {
        EnrollModel Add(int userId, int classId);
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

        public EnrollModel[] GetAll(int userId)
        {
            var itemsx =
    from c in DatabaseAccessor.Instance.Class
    join uc in DatabaseAccessor.Instance.UserClass on c.ClassId equals uc.ClassId
    join u in DatabaseAccessor.Instance.User on uc.UserId equals u.UserId
    where u.UserId == userId
    select new EnrollModel
    {
        UserId = u.UserId,
        ClassId = c.ClassId,
        Name = c.ClassName,
        Price = c.ClassPrice,
        Description = c.ClassDescription
    };
            var newthings = itemsx.ToArray();

            return (newthings);
        }







        //public EnrollModel[] GetAll(int userId)
        //{
        //    var items = enrollRepository.GetAll(userId)
        //        .Select(t => new EnrollModel
        //        {
        //            UserId = t.UserId,
        //            ClassId = t.ClassId,
        //        })
        //        .ToArray();

        //    return items;
        //}


    }
}