using System.Collections.Generic;

namespace UseSCIM.Host.Models
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ExternalId { get; set; }

        public bool Active { get; set; }

        public string DisplayName { get; set; }

        public virtual List<Email> Emails { get; set; }
    }
}
