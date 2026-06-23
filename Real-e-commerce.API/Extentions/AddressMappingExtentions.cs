using Real_e_commerce.Core.Dtos.AddressFeatuer;
using Real_e_commerce.Core.Entities;

namespace Real_e_commerce.API.Extentions
{
    public static class AddressMappingExtentions
    {
        public static AddressDto ToDto(this Address? address)
        {
            if (address == null) return null;
            return new AddressDto
            {
                Line1= address.Line1,
                Line2= address.Line2,
                City= address.City,
                Country= address.Country,
                PostalCode= address.PostalCode,
                State= address.State
            };
        }
        public static Address ToEntity(this AddressDto address)
        {
            if (address == null) throw new ArgumentNullException("address");
            return new Address
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                State = address.State
            };
        }
        public static void UpdateFromDto(this Address address, AddressDto dto)
        {
            if(address== null) throw new ArgumentNullException("address");
            if (dto == null) throw new ArgumentNullException("AddressDto");
            address.Line1 = dto.Line1;
            address.Line2 = dto.Line2;
            address.City = dto.City;
            address.Country = dto.Country;
            address.PostalCode = dto.PostalCode;
            address.State = dto.State;
           
        }
    }
}
