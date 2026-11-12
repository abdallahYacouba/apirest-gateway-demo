using AutoMapper;
using ToDoApiDemo.Model.Entities;
using ToDoApiDemo.Model.ModelWrapper;

namespace ToDoApiDemo.Model.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ForMember(dst => dst.StartDate, 
                       opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dst => dst.DueDate, 
                       opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dst => dst.Title,
                       opt => opt.MapFrom(src => src.Title));
    }
}
