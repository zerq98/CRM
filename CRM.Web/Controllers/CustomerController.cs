using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Application.Dto;
using CRM.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            //var customers = await _customerService.GetAllActiveCustomers();
            ViewData["StatusList"] = new List<string>() { "Verification" };
            return View(new CustomersListDto
            {
                Customers = new List<CustomerViewDto>() {
                new CustomerViewDto{Id=1,Name="Test",NIP= "123-456-32-18", REGON="12-345-678-90",KRSNumber="", DealSize=1231,Status="Verification", Description="Test company" }
            },
                Count = 1
            });
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(CustomerCreateDto model)
        {
            await _customerService.AddNewCustomer(model);
            return RedirectToAction("Index");
        }
    }
}
