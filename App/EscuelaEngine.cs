using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EscuelaCore.Entidades;
using EscuelaCore.Util;

namespace EscuelaCore
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();
        }

        public void PrintDictionary(Dictionary<DictionaryKey, IEnumerable<EscuelaBaseObj>> dic, bool printEval = false)
        {
            foreach (var kvp in dic)
            {
                Printer.WriteTitle($"{kvp.Key}");

                foreach (var escObj in kvp.Value)
                {
                    switch (escObj) 
                    {
                        case Evaluacion assessment:
                            if (printEval)
                                Console.WriteLine(assessment);
                            break;
                        case Escuela school:
                            Console.WriteLine(school);
                            break;
                        case Curso schoolGrade:
                            //var. and cond. below are redundant now
                            var schoolGradeTmp = schoolGrade as Curso;
                            if (schoolGradeTmp != null) 
                            {
                                int count = schoolGrade.Alumnos.Count;
                                Console.WriteLine(escObj + " - No. of Students: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(escObj.Nombre);
                            break;

                    }
                }
            }
        }

        public Dictionary<DictionaryKey, IEnumerable<EscuelaBaseObj>> GetDicionaryObjects() 
        {
            var dictionary = new Dictionary<DictionaryKey, IEnumerable<EscuelaBaseObj>>();

            dictionary.Add(DictionaryKey.School, new List<EscuelaBaseObj> { Escuela });
            dictionary.Add(DictionaryKey.Classes, Escuela.Cursos.Cast<EscuelaBaseObj>());

            var students = new List<Student>();
            var courses = new List<Asignatura>();
            var assessments = new List<Evaluacion>();

            foreach (var schoolGrade in Escuela.Cursos) 
            {
                courses.AddRange(schoolGrade.Asignaturas);
                students.AddRange(schoolGrade.Alumnos);
                foreach (var student in students) 
                {
                    assessments.AddRange(student.Evaluaciones);
                }
            }

            dictionary.Add(DictionaryKey.Students, students);
            dictionary.Add(DictionaryKey.Subjects, courses);
            dictionary.Add(DictionaryKey.Assessments, assessments);

            return dictionary;
        }

        public (IReadOnlyList<EscuelaBaseObj>, int) GetEscuelaObjects(
            bool retrieveEvaluations = true,
            bool retrieveStudents = true,
            bool retrieveSubjects = true,
            bool retrieveCourses = true)
        {
            return GetEscuelaObjects(out _, out _, out _, out _, retrieveEvaluations, retrieveStudents, retrieveSubjects, retrieveCourses);
        }

        public (IReadOnlyList<EscuelaBaseObj>, int) GetEscuelaObjects(
            out int assessmentCount,
            bool retrieveEvaluations = true,
            bool retrieveStudents = true,
            bool retrieveSubjects = true,
            bool retrieveCourses = true)
        {
            return GetEscuelaObjects(out assessmentCount, out _, out _, out _, 
                retrieveEvaluations, retrieveStudents, retrieveSubjects, retrieveCourses);
        }

        public (IReadOnlyList<EscuelaBaseObj>, int) GetEscuelaObjects(
            out int assessmentCount,
            out int studentCount,
            bool retrieveEvaluations = true,
            bool retrieveStudents = true,
            bool retrieveSubjects = true,
            bool retrieveCourses = true)
        {
            return GetEscuelaObjects(out assessmentCount, out studentCount, out _, out _, 
                retrieveEvaluations, retrieveStudents, retrieveSubjects, retrieveCourses);
        }

        public (IReadOnlyList<EscuelaBaseObj>, int) GetEscuelaObjects(
            out int assessmentCount,
            out int studentCount,
            out int subjectCount,
            bool retrieveEvaluations = true,
            bool retrieveStudents = true,
            bool retrieveSubjects = true,
            bool retrieveCourses = true)
        {
            return GetEscuelaObjects(out assessmentCount, out studentCount, out subjectCount, out _, 
                retrieveEvaluations, retrieveStudents, retrieveSubjects, retrieveCourses);
        }

        public (IReadOnlyList<EscuelaBaseObj>, int) GetEscuelaObjects(
            out int assessmentCount,
            out int studentCount,
            out int subjectCount,
            out int coursesCount,
            bool retrieveEvaluations = true,
            bool retrieveStudents = true,
            bool retrieveSubjects = true,
            bool retrieveCourses = true)
        {
            int totalCount = 0;
            assessmentCount = studentCount = subjectCount = coursesCount = 0;

            var listObj = new List<EscuelaBaseObj>();
            listObj.Add(Escuela);
            
            if(retrieveCourses) 
                listObj.AddRange(Escuela.Cursos);

            coursesCount = Escuela.Cursos.Count;

            foreach (var curso in Escuela.Cursos)
            {
                if (retrieveSubjects)
                    listObj.AddRange(curso.Asignaturas);
                subjectCount += curso.Asignaturas.Count;

                if (retrieveStudents)
                    listObj.AddRange(curso.Alumnos);

                studentCount += curso.Alumnos.Count;

                if (retrieveEvaluations) 
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listObj.AddRange(alumno.Evaluaciones);
                        assessmentCount += alumno.Evaluaciones.Count;
                    }
                }
            }

            totalCount = listObj.Count;

            return (listObj, totalCount);
        }

        #region Load Methods
        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            var evaluaciones = new List<Evaluacion>();
            foreach (var grado in Escuela.Cursos)
            {
                foreach (var materia in grado.Asignaturas)
                {
                    foreach (var alumno in grado.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            alumno.Evaluaciones.Add(
                                new Evaluacion
                                {
                                    Alumno = alumno,
                                    Asignatura = materia,
                                    Nombre = $"{materia.Nombre} Ev# {i + 1}",
                                    Nota = MathF.Round((float)rnd.NextDouble() * 5.0f, 2)
                                });
                        }

                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };
            
            Random rnd = new Random();
            foreach(var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion

        private List<Student> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Student { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }
    }
}