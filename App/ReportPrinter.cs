using EscuelaCore.Entidades;
using EscuelaCore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace EscuelaCore.App
{
    public static class ReportPrinter
    {
        public static void ShowSchoolReport(IEnumerable<Escuela> schoolList) 
        {
            Printer.WriteTitle("-- School Report --");
            foreach (var school in schoolList) 
            {
                Console.WriteLine($"School name: { school.Nombre }");
                Console.WriteLine($"School type: { school.TipoEscuela }");
                Console.WriteLine($"School address: { school.Address }");
                Console.WriteLine($"Founded year: {school.AnioDeCreacion}");

            }
        }

        public static void ShowAssessmentReport(IEnumerable<Evaluacion> assessmentList, int displayMax = 5)
        {
            int displayCount = 0;
            int displayTotal = assessmentList.Count();
            int leftToDisplay = displayTotal;

            Printer.WriteTitle("-- Assessment Report --");
            foreach (var assessment in assessmentList)
            {
                Printer.DrawLine(10);
                Console.WriteLine($"Assessment subject: { assessment.Asignatura.Nombre }");
                Console.WriteLine($"Assessment name: { assessment.Nombre }");
                Console.WriteLine($"Student name: { assessment.Alumno.Nombre }");
                Console.WriteLine($"Assessment grade: { assessment.Nota }");

                displayCount++;
                if (displayCount == displayMax) 
                {
                    var selection = string.Empty;
                    Console.WriteLine($"Displaying 5 results out of total { displayTotal }. Pending to show {leftToDisplay -= displayMax}");
                    Printer.PressEnter();
                    Console.WriteLine("Press C to cancel report");
                    selection = Console.ReadLine();

                    if (!string.IsNullOrEmpty(selection) && selection[0].ToString().ToUpper() == "C") 
                    {
                        break;
                    }

                    displayCount = 0;
                }
            }
        }

        internal static void ShowAssessmentsPerSubject(Dictionary<string, IEnumerable<Evaluacion>> assessmentPerSubjectDict, int displayMax = 5)
        {

            Printer.WriteTitle("-- Assessment Per Subject Report --");
            foreach (var keyValuePair in assessmentPerSubjectDict) 
            {
                int displayCount = 0;
                int displayTotal = keyValuePair.Value.Count();
                int leftToDisplay = displayTotal;

                Printer.WriteTitle($"-- Subject {keyValuePair.Key} --");
                foreach (var assessment in keyValuePair.Value) 
                {
                    Printer.DrawLine(10);
                    Console.WriteLine($"Assessment subject: {assessment.Asignatura.Nombre}");
                    Console.WriteLine($"Assessment name: {assessment.Nombre}");
                    Console.WriteLine($"Student name: {assessment.Alumno.Nombre}");
                    Console.WriteLine($"Assessment grade: {assessment.Nota}");

                    displayCount++;
                    if (displayCount == displayMax)
                    {
                        var selection = string.Empty;
                        Console.WriteLine($"Displaying 5 results out of total {displayTotal}. Pending to show for this Subject {leftToDisplay -= displayMax}");
                        Printer.PressEnter();
                        Console.WriteLine("Press S to skip subject");
                        Console.WriteLine("Press C to cancel report");
                        selection = Console.ReadLine();

                        if (!string.IsNullOrEmpty(selection) && selection[0].ToString().ToUpper() == "S")
                        {
                            break;
                        }

                        if (!string.IsNullOrEmpty(selection) && selection[0].ToString().ToUpper() == "C")
                        {
                            return;
                        }

                        displayCount = 0;
                    }
                }
                Printer.DrawLine(20);
            }
        }
    }
}
