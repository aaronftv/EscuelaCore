using EscuelaCore.Util;
using System;
using System.Collections.Generic;

namespace EscuelaCore.Entidades
{
    public class Escuela : EscuelaBaseObj, IPlace
    {
        public int AnioDeCreacion { get; set; }

        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Address { get; set; }
        public TiposEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set; }

        public Escuela(string nombre, int año) => (Nombre, AnioDeCreacion) = (nombre, año);

        public Escuela(string nombre, int año, 
                       TiposEscuela tipo, 
                       string pais = "", string ciudad = "") : base()
        {
            (Nombre, AnioDeCreacion) = (nombre, año);
            Pais = pais;
            Ciudad = ciudad;
        }

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} {System.Environment.NewLine} Pais: {Pais}, Ciudad:{Ciudad}";
        }

        public void ClearPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Clear school...");
            foreach(var curso in Cursos) 
            {
                curso.ClearPlace();
            }

            Printer.WriteTitle($"School {Nombre} cleared...");

            //Printer.Beep(13000, 500, 3);
        }
    }
}
