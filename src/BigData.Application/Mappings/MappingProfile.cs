using AutoMapper;
using BigData.Application.DTOs;
using BigData.Domain.Entities;

namespace BigData.Application.Mappings;

/// <summary>
/// AutoMapper profile for entity to DTO mappings
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateProductDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // AuditLog mappings
        CreateMap<AuditLog, AuditLogDto>();
        CreateMap<CreateAuditLogDto, AuditLog>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow));
    }
}
