namespace Uzduotis4
{
    public class UrlModel
    {
        public List[] list { get; set; }
        public string msg { get; set; }
        public string status_code { get; set; }
    }
    public class List
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}