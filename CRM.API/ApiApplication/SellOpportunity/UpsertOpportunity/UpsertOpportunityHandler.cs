using ApiApplication.Helpers;
using ApiDomain.Entity;
using ApiDomain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.SellOpportunity.UpsertOpportunity
{
    public class UpsertOpportunityHandler : IRequestHandler<UpsertOpportunityCommand, IActionResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly ILeadRepository _leadRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOpportunityStatusRepository _opportunityStatusRepository;

        public UpsertOpportunityHandler(IProductRepository productRepository, IOpportunityRepository opportunityRepository,
                                        ILeadRepository leadRepository,IUserRepository userRepository,IOpportunityStatusRepository opportunityStatusRepository)
        {
            _productRepository = productRepository;
            _opportunityRepository = opportunityRepository;
            _leadRepository = leadRepository;
            _userRepository = userRepository;
            _opportunityStatusRepository = opportunityStatusRepository;
        }
        public async Task<IActionResult> Handle(UpsertOpportunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var opportunity = await _opportunityRepository.GetOpportunityAsync(request.SellOpportunity.Id, request.CompanyId);

                if (opportunity == null)
                {
                    opportunity = new SellOpportunityHeader();
                    opportunity.Id = 0;
                    opportunity.CreateDate = DateTime.Now;
                    opportunity.ModificationDate = opportunity.CreateDate;
                    opportunity.Positions = new List<SellOpportunityPosition>();
                    opportunity.CompanyId = request.CompanyId;
                }
                if(opportunity.Id>0) opportunity.ModificationDate = DateTime.Now;

                opportunity.Status = (await _opportunityStatusRepository.GetOpportunityStatusesAsync()).FirstOrDefault(x => x.Name == request.SellOpportunity.Status);
                opportunity.Lead = await _leadRepository.GetLeadByNameAsync(request.SellOpportunity.Lead,request.CompanyId);
                opportunity.SumGrossValue = 0;
                opportunity.SumMarkupValue = 0;
                opportunity.SumNetValue = 0;
                opportunity.SumVatValue = 0;
                opportunity.Trader = await _userRepository.GetUserByNameAsync(request.SellOpportunity.Trader,request.CompanyId);

                foreach(var positionDto in request.SellOpportunity.Positions)
                {
                    if (!positionDto.Deleted)
                    {
                        if (positionDto.Id > 0)
                        {
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].ModificationDate = opportunity.ModificationDate;
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].NetValue = positionDto.NetValue;
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].Product = await _productRepository.GetProductByNameAsync(positionDto.Product, request.CompanyId);
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].Quantity = positionDto.Quantity;
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].VatValue = positionDto.VatValue;
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].Markup = positionDto.Markup;
                            opportunity.Positions[opportunity.Positions.FindIndex(x => x.Id == positionDto.Id)].GrossValue = positionDto.GrossValue;
                        }
                        else
                        {
                            var position = new SellOpportunityPosition
                            {
                                Id = 0,
                                CreateDate = opportunity.CreateDate,
                                GrossValue = positionDto.GrossValue,
                                Markup = positionDto.Markup,
                                VatValue = positionDto.VatValue,
                                ModificationDate = opportunity.CreateDate,
                                NetValue = positionDto.NetValue,
                                OpportunityHeader = opportunity,
                                Product = await _productRepository.GetProductByNameAsync(positionDto.Product, request.CompanyId),
                                Quantity = positionDto.Quantity
                            };
                            opportunity.Positions.Add(position);
                        }

                        opportunity.SumNetValue += positionDto.NetValue;
                        opportunity.SumGrossValue += positionDto.GrossValue;
                        opportunity.SumMarkupValue += positionDto.Markup;
                        opportunity.SumVatValue += positionDto.VatValue;
                    }
                    else
                    {
                        await _opportunityRepository.RemoveOpportunityPositionAsync(positionDto.Id);
                    }
                }


                if (opportunity.Id == 0)
                {
                    opportunity = await _opportunityRepository.AddOpportunityAsync(opportunity);
                }
                else
                {
                    opportunity = await _opportunityRepository.UpdateAsync(opportunity);
                }

                if (opportunity != null)
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 201,
                        ErrorMessage = ""
                    });
                }
                else
                {
                    return new JsonResult(new ApiResponse<object>
                    {
                        Data = null,
                        Code = 400,
                        ErrorMessage = "Coś poszło nie tak, sprawdź dane i spróbuj ponownie."
                    });
                }
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
