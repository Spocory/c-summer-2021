using System.Linq;

namespace Learning.Repository
{
    public interface IEnrollRepository
    {
        EnrollModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        EnrollModel[] GetAll(int userId);
    }

    public class EnrollModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }

    }

    public class EnrollRepository : IEnrollRepository
    {
        public EnrollModel Add(int userId, int classId)
        {
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


        public EnrollModel[] GetAll(int userId)
        {

            var items = DatabaseAccessor.Instance.UserClass
                .Where(t => t.UserId == userId)
                .Select(t => new EnrollModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId,
                })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int classId)
        {
            var items = DatabaseAccessor.Instance.UserClass
                                .Where(t => t.UserId == userId && t.ClassId == classId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.UserClass.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }

        
    }
}