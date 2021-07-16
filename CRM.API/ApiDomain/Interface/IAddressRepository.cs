﻿using ApiDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDomain.Interface
{
    public interface IAddressRepository
    {
        Task<Address> CreateAddressAsync(Address address);
    }
}
