using ApiDomain.Entity;
using ApiDomain.Interface;
using System;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            try
            {
                await _context.Addresses.AddAsync(address);

                await _context.SaveChangesAsync();

                return address;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "AddressRepository/CreateAddressAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }

        public Task DeleteAddressAsync(int addressId)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> UpdateAddressAsync(Address userAddress)
        {
            try
            {
                _context.Addresses.Update(userAddress);
                await _context.SaveChangesAsync();
                return userAddress;
            }
            catch (Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "AddressRepository/UpdateAddressAsync"
                });
                await _context.SaveChangesAsync();

                throw;
            }
        }
    }
}