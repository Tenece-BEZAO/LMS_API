using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Enums
{
    public enum AssessmentType
    {
        Quiz = 1,
        Test,
        Exam
    }


    public static class AssessmentTypeExtension
    {
        public static string? GetStringValue(this AssessmentType assType)
        {
            return assType switch
            {
                AssessmentType.Quiz => "Quiz",
                AssessmentType.Test => "Test",
                AssessmentType.Exam => "Exam",
                _ => null
            };
        }
    }
}

