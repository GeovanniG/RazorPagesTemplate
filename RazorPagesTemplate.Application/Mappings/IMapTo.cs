﻿using AutoMapper;

namespace RazorPagesTemplate.Application.Mappings;

public interface IMapTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}
