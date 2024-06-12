using System;

namespace asp.net_1.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
