using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicBot.Cards
{
    public class Item
    {
        public string type { get; set; }
        public string horizontalAlignment { get; set; }
        public bool wrap { get; set; }
        public string text { get; set; }
        public bool? isSubtle { get; set; }
        public string spacing { get; set; }
    }

    public class SelectAction
    {
        public string type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }

    public class Column
    {
        public string type { get; set; }
        public string width { get; set; }
        public List<Item> items { get; set; }
        // public SelectAction selectAction { get; set; }
    }

    public class Body
    {
        public string type { get; set; }
        public List<Column> columns { get; set; }
    }

    public class RootObject
    {
        public string type { get; set; }
        public string version { get; set; }
        public List<Body> body { get; set; }
    }
}
