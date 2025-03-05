using System.Collections.Generic;

namespace SWProductWebApi.Helper
{
    /// <summary>
    /// Product Query Model
    /// </summary>
    public class ProductQuery
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public List<string>? Size { get; set; }
        public List<string>? Highlight { get; set; }

    }
}
