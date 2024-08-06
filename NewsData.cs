using System.ComponentModel.DataAnnotations;

namespace regist
{
    public class NewsData
    {
        [Key]
        public int Id { get; set; }
        public string tital { get; set; }
        public string date { get; set; }
        public string imageurl { get; set; }
        public string description { get; set; }
    }
}
