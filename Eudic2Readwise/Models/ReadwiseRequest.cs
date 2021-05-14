using System.Collections.Generic;

namespace Eudic2Readwise.Models.ReadwiseRequest
{
    public class Highlight
    {
        public string text { get; set; }
    }

    public class ReadwiseRoot
    {
        public List<Highlight> highlights { get; set; }
    }
}
