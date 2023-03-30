using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Enums
{

    public enum UserType
    {
        Student = 1,
        Instructor, Admin
    }

    public static class UserTypeExtension
    {
        public static string? GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.Student => "Student",
                UserType.Admin => "Admin",
                UserType.Instructor => "Instructor",
                _ => null
            };
        }
    }
}
