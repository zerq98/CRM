using ApiDomain.Entity;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IAddressRepository
    {
        Task<Address> CreateAddressAsync(Address address);

        Task DeleteAddressAsync(int addressId);
        Task<Address> UpdateAddressAsync(Address userAddress);
    }
}