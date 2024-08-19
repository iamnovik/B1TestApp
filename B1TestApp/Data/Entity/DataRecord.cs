using System;

namespace B1TestApp.Data.Entity
{
    public class DataRecord
    {
        public long Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string LatinText { get; set; } = null!;
        public string CyrillicText { get; set; } = null!;
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
    }
}