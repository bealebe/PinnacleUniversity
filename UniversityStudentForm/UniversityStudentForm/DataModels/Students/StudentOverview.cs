using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinnacleUniversity.DataModels
{
    /// <summary>
    /// This class is to hold the student details in the database.
    ///
    /// This is what will be displayed in the View.
    /// </summary>
    [Table("StudentOverview")]
    public class StudentOverview : StudentDetails
    {
        //These properties are derived because they depend on all of the courses they are enrolled in.

        #region Derived Properties

        public float GPA { get; set; }
        public float EnrolledCredit { get; set; }

        [Browsable(false)]
        public bool FullTime { get; set; }

        #endregion Derived Properties

        #region Properties

        [Browsable(false)]
        public int StudentId { get; set; }

        #endregion Properties

        #region Constructors

        public StudentOverview()
        {
        }

        public StudentOverview(StudentDetails details)
        {
            Courses = details.Courses;
            Email = details.Email;
            Id = details.Id;
            StudentId = details.Id;
            Name = details.Name;

            foreach (EnrolledCourse c in Courses)
            {
                c.CourseId = c.Id;
            }

            CalculateDerivedProperties();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// This method will calculate the GPA, CreditHours, and FullTime Status.
        /// </summary>
        public void CalculateDerivedProperties()
        {
            //Reset Credits and GPA
            EnrolledCredit = 0f;
            GPA = 0f;

            //If there aren't any courses, leave as is.
            if (Courses.Count > 0)
            {
                //Begin calculating credit and GPA
                foreach (EnrolledCourse c in Courses)
                {
                    if (c.Status == EnrolledStudent.ENROLLED)
                    {
                        EnrolledCredit += (float)c.CreditHours;
                        GPA += (float)c.Grade;
                    }
                }
                //Calculate GPA from all grades
                GPA = (float)Math.Round(Convert.ToDecimal((((GPA /= Courses.Count) / 20) - 1)), 2);

                //Check if FullTime Student.
                FullTime = EnrolledCredit >= 10;
            }
        }

        /// <summary>
        /// Makes sure that the class that is passed in is okay to drop
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool ClassOkayToDrop(EnrolledCourse c)
        {
            Console.WriteLine(EnrolledCredit - c.CreditHours);
            Console.WriteLine((EnrolledCredit - c.CreditHours) >= 10);
            //Check to see if we were to subtract the courses credit hours.
            return (EnrolledCredit - c.CreditHours) >= 10;
        }

        #endregion Public Methods
    }
}