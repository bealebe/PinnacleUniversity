using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinnacleUniversity.DataModels
{
    [Table("StudentOverview")]
    public class StudentOverview : StudentDetails
    {
        public float GPA { get; set; }
        public float EnrolledCredit { get; set; }
        [Browsable(false)]
        public bool FullTime { get; set; }
        [Browsable(false)]
        public int StudentId { get; set; }

        public StudentOverview() { }

        public StudentOverview(StudentDetails details)
        {
            Courses = details.Courses;
            Email = details.Email;
            Id = details.Id;
            StudentId = details.Id;
            Name = details.Name;

            foreach(EnrolledCourse c in Courses)
            {
                c.CourseId = c.Id;
            }

            CalculateCreditAndGPA();
        }
        
        public void CalculateCreditAndGPA()
        {
            if (Courses.Count > 0)
            {
                float currentGPA = 0.0f;
                float currentCredit = 0.0f;

                foreach (EnrolledCourse c in Courses)
                {
                    if (c.Status == EnrolledStudent.ENROLLED)
                    {
                        currentCredit += (float)c.CreditHours;
                        currentGPA += (float)c.Grade;
                    }
                }

                currentGPA /= Courses.Count;

                currentGPA /= 20;
                currentGPA -= 1;

                GPA = currentGPA;
                EnrolledCredit = currentCredit;

                if (EnrolledCredit >= 10)
                {
                    FullTime = true;
                }
            }
        }

        internal bool ClassOkayToDrop(EnrolledCourse c)
        {
            Console.WriteLine(EnrolledCredit - c.CreditHours);
            Console.WriteLine((EnrolledCredit - c.CreditHours) >= 10);

            return (EnrolledCredit - c.CreditHours) >= 10;
        }
    }
}
