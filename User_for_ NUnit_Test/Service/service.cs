using User_for__NUnit_Test.Model;
using User_for__NUnit_Test.Repository;

namespace User_for__NUnit_Test.Service
{
    public class service:Iservice
    {
        private readonly Irepo repo;
        public service(Irepo repo)
        {
            this.repo=repo;
        }

        public void Add(User user)
        {
            repo.Add(user);
        }

        public void DeleteUser(string Mobile_No)
        {
            repo.DeleteUser(Mobile_No);
        }

        public List<User> GetAll()
        {
            return repo.GetAll();
        }

        public User GetUserByMobileNo(string Mobile_No)
        {
            return repo.GetUserByMobileNo(Mobile_No);
        }

        public void UpdateUser(string Mobile_No, User user)
        {
            repo.UpdateUser(Mobile_No, user);
        }
    }
}
