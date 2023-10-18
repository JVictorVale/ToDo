using AutoMapper;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;
using ToDo.Domain.Filter;
using ToDo.Domain.Models;

namespace ToDo.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Auth

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<LoginDto, User>().ReverseMap();
        CreateMap<RegisterDto, User>().ReverseMap();
        CreateMap<UserDto, RegisterDto>().ReverseMap();

        #endregion

        #region Assignment

        CreateMap<Assignment, AssignmentDto>().ReverseMap();
        CreateMap<AddAssignmentDto, Assignment>().ReverseMap();
        CreateMap<Assignment, UpdateAssignmentDto>().ReverseMap();
        CreateMap<AssignmentSearchDto, AssignmentFilter>().ReverseMap();
        CreateMap<UpdateAssignmentDto, AssignmentDto>().ReverseMap();

        #endregion

        #region AssignmentList

        CreateMap<AssignmentList, AssignmentListDto>().ReverseMap();
        CreateMap<AddAssignmentListDto, AssignmentList>().ReverseMap();
        CreateMap<UpdateAssignmentListDto, AssignmentList>().ReverseMap();

        #endregion
    }
}