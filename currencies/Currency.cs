using System;
using System.IO;

namespace currencies
{
    public class Currency
    {
        public Currency(string name, float value, DateTime updated)
        {
            Name = name;
            Value = value;
            Updated = updated;
        }
        public string Name { get; set; }
        public float Value { get; set; }
        public DateTime Updated { get; set; }
    }
}
