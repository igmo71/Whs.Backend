﻿using AutoMapper;
using Whs.Application.Common.Mappings;
using Whs.Application.Services.Notes.Commands.CreateNote;
using System.ComponentModel.DataAnnotations;

namespace Whs.WebApi.Models
{
    public class CreateNoteDto : IMapWith<CreateNoteCommand>
    {
        [Required]
        public string? Title { get; set; }
        public string? Details { get; set; }

        public void Mapping(Profile profile) { 
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Title, opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details, opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
