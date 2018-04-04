using System.Collections.Generic;

namespace LoginApp.Cryptology.Models
{
    public class Keys
    {
        public List<string> Key16 { get; set; }
        public List<string> Key32 { get; set; }
        public List<string> Key64 { get; set; }

        public List<string> Template;
    }
}
