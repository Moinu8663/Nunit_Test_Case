using User_for__NUnit_Test.Model;

namespace User_for__NUnit_Test.Repository
{
    public class Repo:Irepo
    {
        private readonly UserContext context;
        public Repo(UserContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void DeleteUser(string Mobile_No)
        {
            var us = GetUserByMobileNo(Mobile_No);
            context.Users.Remove(us);
            context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetUserByMobileNo(string Mobile_No)
        {
            return context.Users.Where(o => o.Mobile_No == Mobile_No).FirstOrDefault();
        }

        public void UpdateUser(string Mobile_No, User user)
        {
            var us = GetUserByMobileNo(Mobile_No);
            us.Name = user.Name;
            us.Age = user.Age;
            us.Email = user.Email;
            context.SaveChanges();
        }
    }
}
