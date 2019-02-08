using CarTransports.Models;
using CarTransports.ViewModels;

namespace CarTransports.Infrastructure
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Pickup, PickupViewModel>().ReverseMap();
            CreateMap<PickupPoint, PickupPointViewModel>().ReverseMap();
        }
    }
}
