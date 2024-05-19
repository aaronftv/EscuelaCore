using System;
using System.Collections.Generic;
using System.Linq;
using EscuelaCore.App;
using EscuelaCore.Entidades;
using EscuelaCore.Util;
using static System.Console;

namespace EscuelaCore
{
    /// <summary>
    /// Based on the 2 first .NET Code courses from Platzi
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += EventAction;
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Printer.Beep(100, 1000, 1);
            //AppDomain.CurrentDomain.ProcessExit -= EventAction;

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            //Printer.Beep(10000, cantidad:10);
            //ImpimirCursosEscuela(engine.Escuela);
            //ImprimirListaExamenes(engine.Escuela);

            #region Polymorfism tests
            /*
            Printer.DrawLine(21);
            Printer.DrawLine(21);
            Printer.WriteTitle("Polymorfism Tests");

            var alumnoTest = new Alumno { Nombre = "Claire Underwood" };
            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"ID: {alumnoTest.UniqueId}");
            WriteLine($"GetType(): {alumnoTest.GetType()}");

            EscuelaBaseObj obj = alumnoTest;
            Printer.WriteTitle("EscuelaObj");
            WriteLine($"Alumno: {obj.Nombre}");
            WriteLine($"ID: {obj.UniqueId}");
            WriteLine($"GetType(): {obj.GetType()}");

            var assessment = new Evaluacion() { Nombre = "Math assessment", Nota = 4.5f };
            Printer.WriteTitle("Assessment");
            WriteLine($"Name:       {assessment.Nombre}");
            WriteLine($"ID:         {assessment.UniqueId}");
            WriteLine($"Nota:       {assessment.Nota}");
            WriteLine($"GetType():  {assessment.GetType()}");

            //obj = assessment;
            Printer.WriteTitle("Assessment (EscuelaObj)");
            WriteLine($"Name:       {obj.Nombre}");
            WriteLine($"ID:         {obj.UniqueId}");
            WriteLine($"GetType():  {obj.GetType()}");

            if (obj is Alumno)
            {
                Alumno retrievedStudent = (Alumno)obj;
            }

            Alumno retrievedStudent2 = obj as Alumno;
            */
            #endregion

            /*Some more tests
             engine.Escuela.ClearPlace();

             var iPlaceList = from obj in escuelaObjList
                             where obj is IPlace
                             select (IPlace) obj; 
            */

            /*in out vars tests, function overrride
            var escuelaObjList = engine.GetEscuelaObjects(
                out int assessmentCount,
                out int studentCount,
                out int subjectCount,
                out int coursesCount);
            
            escuelaObjList = engine.GetEscuelaObjects(
                out int assessmentCount1,
                out int studentCount1,
                out int subjectCount1);

            escuelaObjList = engine.GetEscuelaObjects(
                out int assessmentCount2,
                out int studentCount2);

            escuelaObjList = engine.GetEscuelaObjects(
                out int assessmentCount3);
            */

            /*//Dictionary excercise
            Printer.WriteTitle("Dictionary tests");
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(10, "JuanGa");
            dictionary.Add(23, "Lorem Ipsum");

            foreach(var keyValPair in dictionary) 
            {
                WriteLine($"Key: {keyValPair.Key} Value: {keyValPair.Value}");
            }

            Printer.WriteTitle("Access to Dictionary");

            WriteLine(dictionary[23]);
            dictionary[0] = "Poker Face";
            WriteLine(dictionary[0]);

            Printer.WriteTitle("Another Dictionary");
            var dic = new Dictionary<string, string>();
            dic["Luna"] = "Celestial body that orbitates around earth";
            WriteLine(dic["Luna"]);
            dic.Add("Luna", "Soy Luna protagonist");
            WriteLine(dic["Luna"]);*/

            var dicTemp = engine.GetDicionaryObjects();
            //engine.PrintDictionary(dicTemp, false);

            var reporter = new Reporter(dicTemp);   

            var school = reporter.GetSchool();
            var assessments = reporter.GetAssessmentList();
            var students = reporter.GetStudentList();
            var distinctCoursesWithApprovedAssessments = reporter.GetSubjectList();
            var assessmentPerSubjectDict = reporter.GetAssessmenstPerSubjectDict();
            var GPAperStudentDict = reporter.GetStudentGPAPerSubject();
            var TopXStudentsHighestGPA = reporter.TopXStudentsHighestGPA(5, "Matemáticas");

            /* Simple console UI Phase 9
            Printer.WriteTitle("--- CONSOLE ASSESSMENT CAPTURE FORM ---");
            var newAssessment = new Evaluacion();
            string assessmentName;
            string assessmentGradeString;
            float assessmentGrade;

            try
            {
                WriteLine("Enter assessment name: ");
                Printer.PressEnter();
                assessmentName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(assessmentName))
                {
                    throw new ArgumentNullException("Assessment name text can't be null");
                }
                else
                {
                    newAssessment.Nombre = assessmentName.ToLower();
                    WriteLine("Assessment name has been entered correctly. ");
                }

                WriteLine("Enter assessment grade: ");
                Printer.PressEnter();
                assessmentGradeString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(assessmentGradeString))
                {
                    throw new ArgumentNullException("Assessment grade text can't be null");
                }
                else if (float.TryParse(assessmentGradeString, out assessmentGrade))
                {
                    if (assessmentGrade < 0 || assessmentGrade > 5.0)
                    {
                        throw new ArgumentOutOfRangeException("Assessment grade should be between 0 and 5");
                    }

                    newAssessment.Nota = assessmentGrade;
                    WriteLine("Assessment grade has been entered correctly. ");
                }
                else
                {
                    throw new ArgumentException("Assessment grade has to be a valid type: float");
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                WriteLine("Exiting program... ");
            }
            finally 
            {
                Printer.WriteTitle("FINALLY");
                WriteLine("Y'all come back! ");
            }
            */

            var optionString = string.Empty;
            var option = 0;
            var validEntry = true;

            Printer.WriteTitle("--- CONSOLE REPORT DISPLAY FORM ---");
            
            while(true) 
            {

                WriteLine("Please select the report you want to visualize, then press ENTER:");
                WriteLine("SCHOOL REPORT (1) | ASSESSMENT REPORT (2) | ASSESSMENTS PER SUBJECT (5) | EXIT (E)");
                //TODO: Add the rest of the reports
                // STUDENT LIST (3) | COURSES WITH APPROVED ASSESMENTS (4) |
                //TODO: Add option to specify max of items, ideally only when more than 5 items are retrieved (think abt a good logic for that)

                optionString = ReadLine();

                try
                {
                    if (string.IsNullOrWhiteSpace(optionString))
                    {
                        throw new ArgumentNullException("Option can't be null");
                    }
                    else if (optionString[0].ToString().ToUpper() == "E")
                    {
                        Printer.WriteTitle("CLOSING PROGRAM");
                        WriteLine("Y'all come back! ");
                        break;
                    }

                    if (!int.TryParse(optionString, out option))
                    {
                        throw new ArgumentException("Please Enter a valid number");
                    }
                    else if (option < 1 || option > 7)
                    {
                        throw new ArgumentOutOfRangeException("Please number within the range");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    validEntry = false;
                }
                finally
                {

                }

                if (validEntry)
                {
                    switch (option)
                    {
                        case 1:
                            ReportPrinter.ShowSchoolReport(school);
                            break;
                        case 2:
                            ReportPrinter.ShowAssessmentReport(assessments);
                            break;
                        case 5:
                            ReportPrinter.ShowAssessmentsPerSubject(assessmentPerSubjectDict);
                            break;
                        default:
                            WriteLine("Option not configured");
                            break;

                    }
                }
                validEntry = true;
                Printer.DrawLine(20);
            }           
        }

        private static void EventAction(object sender, EventArgs e)
        {
            Printer.WriteTitle("EXITING PROGRAM");
            //Printer.Beep(2000, 800, 3);
            //Printer.WriteTitle("DONE!");
        }

        private static void ImprimirListaExamenes(Escuela escuela)
        {
            foreach (var grado in escuela.Cursos)
            {
                foreach (var alumno in grado.Alumnos)
                {
                    foreach (var examen in alumno.Evaluaciones)
                    {
                        Console.WriteLine($"Examen: {examen.Nombre} Nota: {examen.Nota.ToString("0.00")} Materia: {examen.Asignatura.Nombre} Alumno: {examen.Alumno.Nombre} ");
                    }
                }
            }
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");
            
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}
