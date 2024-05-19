using EscuelaCore.Util;
using System;
using System.Collections.Generic;

namespace EscuelaCore.Entidades
{
    public class Curso : EscuelaBaseObj, IPlace
    {
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Student> Alumnos{ get; set; }
        public string Address { get; set; }
        
        public void ClearPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Clear course...");
            Console.WriteLine($"Course {Nombre} cleared...");
        }
    }
}