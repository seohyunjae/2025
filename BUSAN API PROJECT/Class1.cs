using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSAN_API_PROJECT
{
    internal class Class1
    {
        public class AttractionItem
        {
            public int UC_SEQ { get; set; }
            public string MAIN_TITLE { get; set; }
            public string ADDR1 { get; set; }
            public string GUGUN_NM { get; set; }
            public string LAT { get; set; }
            public string LNG { get; set; }
            public string ITEMCNTNTS { get; set; }
            public string MAIN_IMG_NORMAL { get; set; }
        }

        public class ApiBody
        {
            public List<AttractionItem> items { get; set; }
        }

        public class ApiResponse
        {
            public ApiBody body { get; set; }
        }

        public class RootObject
        {
            public ApiResponse getAttractionKr { get; set; }
        }
    }
}
