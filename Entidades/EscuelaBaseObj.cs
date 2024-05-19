using System;

namespace EscuelaCore.Entidades
{
    public abstract class EscuelaBaseObj
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public EscuelaBaseObj() 
        {
            UniqueId = Guid.NewGuid().ToString();
        }
        public override string ToString()
        {
            return $"{Nombre},{UniqueId}";
        }
    }
}
