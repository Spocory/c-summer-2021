using System.Linq;

namespace Learning.Repository
{
    public interface IEnrollRepository
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

    public class EnrollRepository : IEnrollRepository
    {
        public EnrollModel Add(int userId, int classId)
        {
            if (userId == 0)
            {
                return null;
            }
            if (classId == 0)
            {
                return null;
            }

            var item = DatabaseAccessor.Instance.UserClass.Add(
                new Learning.ClassDatabase.UserClass
                {
                    UserId = userId,
                    ClassId = classId,
            
                });

            DatabaseAccessor.Instance.SaveChanges();

            return new EnrollModel
            {
                UserId = item.Entity.UserId,
                ClassId = item.Entity.ClassId,
            };
        }


        //public EnrollModel[] GetAll(int userId)
        //{

        //    var items = DatabaseAccessor.Instance.UserClass
        //        .Where(t => t.UserId == userId)
        //        .Select(t => new EnrollModel
        //        {
        //            UserId = t.UserId,
        //            ClassId = t.ClassId,
        //        })
        //        .ToArray();
        //    return items;
        //}


        //(class)Product {Id, ProdName, ProdQty}
        //(user)Category {Id, CatName}
        //(userclass)ProductCategory{ ProdId, CatId} //association table

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







        
    }
}