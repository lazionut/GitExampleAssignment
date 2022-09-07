﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Utils;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, Address?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address?> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
        {
            Address? updatedAddress = await _unitOfWork.AddressRepository.GetById(request.AddressId);

            if (updatedAddress == null)
            {
                return null;
            }

            updatedAddress.Details = request.AddressDetails;
            updatedAddress.City = request.AddressCity;
            updatedAddress.County = request.AddressCounty;
            updatedAddress.Country = request.AddressCountry;
            updatedAddress.Phone = request.AddressPhone;

            updatedAddress.UpdatedAt = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();

            await _unitOfWork.AddressRepository.Update(updatedAddress);
            await _unitOfWork.Save();

            return updatedAddress;
        }
    }
}
