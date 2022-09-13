﻿using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount
{
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, Account?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            Address? searchedAddress = await _unitOfWork.AddressRepository.GetById(request.AccountAddressId);

            if (searchedAddress == null)
            {
                return null;
            }

            Account? searchedAccountByAddressId = await _unitOfWork.AccountRepository.GetAccountByAddressId(request.AccountAddressId);

            if (searchedAccountByAddressId != null)
            {
                return null;
            }

            Account? searchedAccountByEmail = await _unitOfWork.AccountRepository.GetAccountByEmail(request.AccountEmail);

            if (searchedAccountByEmail != null)
            {
                return null;
            }

            Account account = new Account
            {
                Email = request.AccountEmail,
                Password = request.AccountPassword,
                FirstName = request.AccountFirstName,
                LastName = request.AccountLastName,
                AddressId = request.AccountAddressId,
                IsArchived = false
            };

            await _unitOfWork.AccountRepository.Create(account);
            await _unitOfWork.Save();

            return account;
        }
    }
}
