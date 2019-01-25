using System;
using System.Collections.Generic;

namespace MaliMali.Model
{
    public class Item
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int Rating { get; set; }
        public List<string> Images {get; set;}
        public string Image_url { get; set; }
        public long Post_Date { get; set; }
        public string Item_ID { get; set; }
        public string User_ID { get; set; }
        public long Available { get; set; }
        public string Category { get; set; }

        public Item()
        {
            Images = new List<string>();
        }
    }
}
