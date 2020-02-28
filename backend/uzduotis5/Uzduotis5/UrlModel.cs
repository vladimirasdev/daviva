using System.Collections.Generic;
namespace Uzduotis5
{
    public class UrlModel
    {
        public List<url1> list { get; set; }
        public string msg { get; set; }
        public string status_code { get; set; }
    }
    public class url1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class UrlModel2
    {
        public List<url2> list { get; set; }
        public string msg { get; set; }
        public string status_code { get; set; }
    }

    public class url2
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public string year_start { get; set; }
        public string year_end { get; set; }
    }
}