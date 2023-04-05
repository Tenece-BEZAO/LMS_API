using AutoMapper;
using LMS.BLL.DTOs.Request;
using LMS.BLL.DTOs.Response;
using LMS.DAL.Entities;

namespace LMS.BLL.MappingProfiles;

public class AssessmentProfile : Profile
{
    public AssessmentProfile()
    {
        CreateMap<AssessmentRequestDto, Assessment>();
        CreateMap<Assessment, AssessmentResponseDto>();
        CreateMap<Assessment, AssessmentRequestDto>();
    }
}
