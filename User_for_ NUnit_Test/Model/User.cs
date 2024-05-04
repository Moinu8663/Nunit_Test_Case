using System.ComponentModel.DataAnnotations;

namespace User_for__NUnit_Test.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Mobile_No { get; set; }
        public string? Email { get; set;}
    }
}
