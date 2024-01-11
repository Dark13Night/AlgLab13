#nullable enable
using System.Collections.Generic;

namespace CSlab13
{
    public class Magazine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<StatieGroup> StatieGroup { get; set; } = new();
    }
}
