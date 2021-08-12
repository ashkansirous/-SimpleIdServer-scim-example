namespace UseSCIM.Host.Models
{
    public class Email
    {
        public string Id { get; set; }

        public bool Primary { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }
    }
}
