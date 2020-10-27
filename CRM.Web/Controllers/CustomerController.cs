using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Application.Dto;
using CRM.Application.Dto.Customer;
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
        public async Task<IActionResult> IndexAsync(string username)
        {
            var customers = await _customerService.GetAllActiveCustomers(username);
            ViewData["StatusList"] = new List<string>() { "Verification" };
            return View(customers);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusAsync(int id,int statusId)
        {
            await _customerService.ChangeCustomerStatus(id, statusId);
            return null;
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
