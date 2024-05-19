using EscuelaCore.Entidades;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Reflection;
using System.ComponentModel.Design;
using System.Collections.Immutable;

namespace EscuelaCore.App
{
    public class Reporter
    {
        Dictionary<DictionaryKey, IEnumerable<EscuelaBaseObj>> _dictionary;
        public Reporter(Dictionary<DictionaryKey,IEnumerable<EscuelaBaseObj>> escObjDic)
        {
            if (escObjDic == null)
                throw new ArgumentNullException(nameof(escObjDic));
            
            _dictionary = escObjDic;
        }

        public IEnumerable<Escuela> GetSchool()
        {
            IEnumerable<Escuela> response;
            if (_dictionary.TryGetValue(DictionaryKey.School, out IEnumerable<EscuelaBaseObj> list))
            {
                response = list.Cast<Escuela>();
            }
            else 
            {
                response = null;
                //Log: School = null error
            }

            return response; ;
        }

        public IEnumerable<Evaluacion> GetAssessmentList()
        {
            IEnumerable<Evaluacion> response;
            if (_dictionary.TryGetValue(DictionaryKey.Assessments, out IEnumerable<EscuelaBaseObj> list))
            {
                response = list.Cast<Evaluacion>();
            }
            else
            {
                response = new List<Evaluacion>();
            }

            return response; ;
        }

        public IEnumerable<Evaluacion> EvilGetAssessmentList() 
        {
            //Bad Example for accessing data
            return _dictionary[DictionaryKey.Assessments].Cast<Evaluacion>();
        }

        public IEnumerable<Student> GetStudentList() 
        {
            var list = _dictionary.GetValueOrDefault(DictionaryKey.Students).Cast<Student>();
            return list;
        }

        public IEnumerable<string> GetSubjectList()
        {
            return GetSubjectList(out _);
        }

        public IEnumerable<string> GetSubjectList(out IEnumerable<Evaluacion> assessmentList) 
        {
            assessmentList = GetAssessmentList();

            //NOTE: we select by String not by Asignatura because Asignatura is going to group by Hash not by Name
            //Comparer could be an option to determine what Asignatura is different from the other
            //Another option for below example would be something like .GroupBy(x => x.Nombre) imagine the rest

            return (from Evaluacion ev in assessmentList
                   //where ev.Nota >= 3.00f
                   select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetAssessmenstPerSubjectDict() 
        {
            var responseDict = new Dictionary<string, IEnumerable<Evaluacion>>();

            var subjectList = GetSubjectList(out var assessmentList);

            foreach (var subject in subjectList) 
            {
                var assessmentsCourse = from ev in assessmentList
                                        where ev.Asignatura.Nombre == subject
                                        select ev;

                responseDict.Add(subject, assessmentsCourse);
            }

            return responseDict;
        }

        public Dictionary<string, IEnumerable<object>> GetStudentGPAPerSubject() 
        {
            var response = new Dictionary<string, IEnumerable<object>>();

            var evalPerSubjectDict = GetAssessmenstPerSubjectDict();

            foreach (var subjectWithEval in evalPerSubjectDict) 
            {
                var studentsGPAs = from eval in subjectWithEval.Value
                            group eval by new { 
                                eval.Alumno.UniqueId, 
                                eval.Alumno.Nombre 
                            }
                            into evalPerStudentGroup
                            select new StudentGPA
                            {
                                studentId = evalPerStudentGroup.Key.UniqueId,
                                studentName = evalPerStudentGroup.Key.Nombre,
                                GPA = evalPerStudentGroup.Average(ev => ev.Nota)
                            };

                response.Add(subjectWithEval.Key, studentsGPAs);
            }

            return response;
        }

        /// <summary>
        /// Second Challenge
        /// </summary>
        /// <param name="topNo"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public IEnumerable<StudentGPA> TopXStudentsHighestGPA(int topNo, string subject) 
        {
            var students = new List<Student>();
            var studentGPAPerSubject = GetStudentGPAPerSubject();

            //Approach 1
            //var studentsForSpecificSubject = (from studentGPABySubject in studentGPAPerSubject
            //                                 where studentGPABySubject.Key == subject
            //                                 select studentGPABySubject.Value).FirstOrDefault().Cast<StudentGPA>();

            //Approach 2
            var studentsForSpecificSubject = studentGPAPerSubject.GetValueOrDefault(subject).Cast<StudentGPA>();

            var topStudentsForSubject = (from studentGPA in studentsForSpecificSubject
                                         orderby studentGPA.GPA descending
                                         select studentGPA).Take(topNo);

            return topStudentsForSubject;
        }
    }
}
