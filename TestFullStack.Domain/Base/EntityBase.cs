using System;
using System.ComponentModel.DataAnnotations;

namespace TestFullStack.Domain.Base
{
    /// <summary>
    /// EntityBase contains base methods and properties of any entity
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }


        public EntityBase Clone()
        {
            return (EntityBase)MemberwiseClone();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool Equals(EntityBase other)
        {
            if (other == null) return false;

            // Se o Id for igual ao valor default de long (0), 
            // estamos comparando duas entidades não persistidas.
            // Nesse caso devolvemos a verificação de referencia em memória
            if (other.Id.Equals(default(long)) && Id.Equals(default(long)))
                return ReferenceEquals(this, other);

            // Verifica se as entidades são do mesmo tipo e tem o mesmo ID. Nesse caso são iguais
            return (GetType() == other.GetType() && Id.Equals(other.Id));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EntityBase);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
