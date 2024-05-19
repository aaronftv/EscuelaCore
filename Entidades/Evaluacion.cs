using System;
using System.Diagnostics;

namespace EscuelaCore.Entidades
{
    [DebuggerDisplay("{Nota},{Alumno.Nombre},{Asignatura.Nombre}")]
    public class Evaluacion : EscuelaBaseObj
    {
        public Student Alumno { get; set; }
        public Asignatura Asignatura  { get; set; }

        public float Nota { get; set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}