using System.Collections.Generic;

namespace Eudic2Readwise.Models.EudicResponse
{
    public class Datum
    {
        public string word { get; set; }
        public string exp { get; set; }
    }

    public class EudicRoot
    {
        public List<Datum> data { get; set; }
        public string message { get; set; }
    }

}
