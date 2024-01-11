#nullable enable
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace CSlab13
{
    public class Statie
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public int MagazineId { get; set; }
        public string? Description { get; set; }
    }
}