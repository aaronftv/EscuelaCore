using System;
using System.Collections.Generic;

namespace EscuelaCore.Entidades
{
    public class Student : EscuelaBaseObj
    {
        public List<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();
    }
}