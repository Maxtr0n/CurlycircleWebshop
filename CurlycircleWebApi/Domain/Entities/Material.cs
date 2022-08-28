using Domain.Entities.Abstractions;

namespace Domain.Entities
{
    public class Material : EntityBase
    {
        public string Name { get; set; } = null!;

        public Material()
        {
        }
    }
}
