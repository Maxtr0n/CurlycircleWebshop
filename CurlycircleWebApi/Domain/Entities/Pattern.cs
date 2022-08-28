using Domain.Entities.Abstractions;

namespace Domain.Entities
{
    public class Pattern : EntityBase
    {
        public string Name { get; set; } = null!;

        public Pattern()
        {
        }
    }
}
