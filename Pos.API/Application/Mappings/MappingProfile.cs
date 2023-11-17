using AutoMapper;
using Pos.API.Domain.Entities;
using Pos.API.Models;
using static User.API.Application.Features.DonVi.Commands.AddDonViCommand;


namespace Pos.API.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<M_User, UserModelResponse>().ReverseMap();
            CreateMap<AddDonViRequest, M_DonVi>()
            .ForMember(destination => destination.DienThoaiDonVi, options => options.MapFrom(source => source.DienThoaiLienHe));

            CreateMap<AddDonViRequest, M_User>()
            .ForMember(destination => destination.Phone, options => options.MapFrom(source => source.DienThoaiLienHe))
            .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.DienThoaiLienHe))
            .ForMember(destination => destination.FullName, options => options.MapFrom(source => source.TenDonVi));
           

            CreateMap<BillResponse, T_DonHang>().ReverseMap();
            CreateMap<ItemMatHangDH, T_DonHangChiTiet>().ReverseMap();
        }
    }
}
