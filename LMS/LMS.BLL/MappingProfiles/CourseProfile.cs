using AutoMapper;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.MappingProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile() {
            CreateMap<Course, CourseDto>();
        
        }
    }
}
