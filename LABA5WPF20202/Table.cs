using System.Collections.Generic;

namespace LABA5WPF20202
{
    public class Table
    {
        public List<Row> Rows { get; set; }
        public Table()
        {
            Rows = new List<Row>();
        }
    }
}