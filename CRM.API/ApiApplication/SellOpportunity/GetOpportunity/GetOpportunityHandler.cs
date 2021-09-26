using ApiApplication.DTO;
using ApiApplication.Helpers;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.GetOpportunity
{
    public class GetOpportunityHandler : IRequestHandler<GetOpportunityQuery, IActionResult>
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILeadRepository _leadRepository;

        public GetOpportunityHandler(IOpportunityRepository opportunityRepository, IUserRepository userRepository,
                                     IProductRepository productRepository,ILeadRepository leadRepository)
        {
            _opportunityRepository = opportunityRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _leadRepository = leadRepository;
        }
        public async Task<IActionResult> Handle(GetOpportunityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tradersList = await _userRepository.GetCompanyTraders(request.CompanyId);
                var products = await _productRepository.GetAllProductsAsync(request.CompanyId, new List<string> { "", "", "" });
                var leads = await _leadRepository.GetAllLeadsAsync(request.CompanyId, new DateTime(2000, 01, 01), new DateTime(2999, 12, 31));
                var opportunity = await _opportunityRepository.GetOpportunityAsync(request.OpportunityId, request.CompanyId);

                var oppoRes = new SellOpportunityDetailsDto();

                if (request.OpportunityId > 0)
                {
                    oppoRes = new SellOpportunityDetailsDto
                    {
                        Id=opportunity.Id,
                        Lead = opportunity.Lead.Name+","+opportunity.Lead.NIP,
                        Positions = new List<SellOpportunityPositionDetailsDto>(),
                        Status = opportunity.Status.Name,
                        Trader = opportunity.Trader.FirstName + " " + opportunity.Trader.LastName
                    };

                    var counter = 1;
                    foreach (var position in opportunity.Positions)
                    {
                        oppoRes.Positions.Add(new SellOpportunityPositionDetailsDto
                        {
                            Deleted = false,
                            GrossValue = position.GrossValue,
                            Id = position.Id,
                            LocalId = counter,
                            Markup = position.Markup,
                            NetValue = position.NetValue,
                            Product = position.Product.Name,
                            Quantity = position.Quantity,
                            VatValue = position.VatValue,
                            UnitOfMeasurement = position.Product.UnitOfMeasurement
                        });
                        counter += 1;
                    }
                }
                else
                {
                    oppoRes = new SellOpportunityDetailsDto
                    {
                        Lead = "",
                        Positions = new List<SellOpportunityPositionDetailsDto>(),
                        Status = "",
                        Trader = ""
                    };
                }

                var response = new SellOpportunityDetailsResponseDto
                {
                    CompanyTraders = tradersList.Select(x => (x.FirstName + " " + x.LastName)).ToList(),
                    Leads = leads.Select(x => (x.Name+","+x.NIP)).ToList(),
                    Products = products.Select(x => new ProductForListDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        MarkupRate = x.MarkupRate,
                        UnitOfMeasurement = x.UnitOfMeasurement,
                        UnitValue = x.UnitValue,
                        VatRate = x.VatRate
                    }).ToList(),
                    SellOpportunity = oppoRes
                };

                return new JsonResult(new ApiResponse<SellOpportunityDetailsResponseDto>
                {
                    Data = response,
                    Code = 200,
                    ErrorMessage = ""
                });
            }
            catch
            {
                return new JsonResult(new ApiResponse<object>
                {
                    Data = null,
                    Code = 500,
                    ErrorMessage = "Nastąpił problem z serwerem, skontaktuj się z działem IT."
                });
            }
        }
    }
}
