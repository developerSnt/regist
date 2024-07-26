using System.ComponentModel.DataAnnotations;

namespace regist
{
    public class DataInfo
    {
        [Key]
        public int Id { get; set; }
        public string url {  get; set; }
        public string source { get; set; }
        public string elementClass { get; set; }
        public string elementId { get; set; }
        public string Domion {  get; set; }
    }
}
