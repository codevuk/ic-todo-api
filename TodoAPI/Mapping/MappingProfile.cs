using AutoMapper;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoViewModel>();
    }
}