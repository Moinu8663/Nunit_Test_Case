using User_for__NUnit_Test.Model;

namespace User_for__NUnit_Test.Repository
{
    public interface Irepo
    {
        void Add(User user);
        public List<User> GetAll();
        public User GetUserByMobileNo(string Mobile_No);
        public void UpdateUser(string Mobile_No, User user);
        public void DeleteUser(string Mobile_No);
    }
}
