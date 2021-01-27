namespace RapidApiTests.Model
{
    public class BlogPost
    {
        public string id { get; set; } //Only using lower case on this id field because of the json-server npm package we test against
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
