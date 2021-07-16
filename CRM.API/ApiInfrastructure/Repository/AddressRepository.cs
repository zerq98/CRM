using ApiDomain.Entity;
using ApiDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Repository
{
    public class AddressRepository : BaseRepository,IAddressRepository
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
            catch(Exception ex)
            {
                await _context.Logs.AddAsync(new Log
                {
                    LogMessage = ex.Message,
                    ModuleName = "AddressRepository/CreateAddressAsync"
                });
                await _context.SaveChangesAsync();

                return null;
            }
        }
    }
}
