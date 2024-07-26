using System.ComponentModel.DataAnnotations;

namespace regist
{
    public class Regis
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }

        public List<string> certificate { get; set; }
        public string password { get; set; }
        public string comformpassword { get; set; }
    }
}
