﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Accounts.Commands.CreateAccount;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList;
using BoardgamesEShopManagement.Application.Accounts.Queries.GetAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetOrderByAccount;
using BoardgamesEShopManagement.Application.Categories.Queries.GetOrdersListPerAccount;
using BoardgamesEShopManagement.Application.Wishlists.Queries.GetWishlistByAccount;
using BoardgamesEShopManagement.Application.Categories.Queries.GetWishlistsListPerAccount;
using BoardgamesEShopManagement.Application.Accounts.Commands.DeleteAccount;
using BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveAccount;
using BoardgamesEShopManagement.Application.Orders.Commands.DeleteWishlistItem;
using BoardgamesEShopManagement.Application.Wishlists.Commands.DeleteWishlist;
using BoardgamesEShopManagement.Application.Reviews.Queries.GetReviewsListPerAccount;

namespace BoardgamesEShopManagement.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountPostDto account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateAccountRequest? command = new CreateAccountRequest
            {
                AccountFirstName = account.AccountFirstName,
                AccountLastName = account.AccountLastName,
                AccountEmail = account.AccountEmail,
                AccountPassword = account.AccountPassword,
                AccountAddressId = account.AccountAddressId,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return CreatedAtAction(nameof(GetAccount), new { id = mappedResult.AccountId }, mappedResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts([BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetAccountsListQuery query = new GetAccountsListQuery
            {
                AccountPageIndex = pageIndex,
                AccountPageSize = pageSize
            };

            List<Account> result = await _mediator.Send(query);

            List<AccountGetDto> mappedResult = _mapper.Map<List<AccountGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            GetAccountQuery? query = new GetAccountQuery { AccountId = id };

            Account? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            AccountGetDto mappedResult = _mapper.Map<AccountGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetReviewsListPerAccountQuery? query = new GetReviewsListPerAccountQuery
            {
                ReviewAccountId = id,
                ReviewPageIndex = pageIndex,
                ReviewPageSize = pageSize
            };

            List<Review>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<ReviewGetDto> mappedResult = _mapper.Map<List<ReviewGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{accountId}/orders/{orderId}")]
        public async Task<IActionResult> GetOrderByAccount(int accountId, int orderId)
        {
            GetOrderByAccountQuery? query = new GetOrderByAccountQuery { AccountId = accountId, OrderId = orderId };

            Order? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            OrderGetDto mappedResult = _mapper.Map<OrderGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/orders")]
        public async Task<IActionResult> GetOrdersPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetOrdersListPerAccountQuery? query = new GetOrdersListPerAccountQuery
            {
                OrderAccountId = id,
                OrderPageIndex = pageIndex,
                OrderPageSize = pageSize
            };

            List<Order>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<OrderGetDto> mappedResult = _mapper.Map<List<OrderGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{accountId}/wishlists/{wishlistId}")]
        public async Task<IActionResult> GetWishlistByAccount(int accountId, int wishlistId)
        {
            GetWishlistByAccountQuery? query = new GetWishlistByAccountQuery
            {
                WishlistAccountId = accountId,
                WishlistId = wishlistId
            };

            Wishlist? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            WishlistGetDto mappedResult = _mapper.Map<WishlistGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/wishlists")]
        public async Task<IActionResult> GetWishlistsPerAccount(int id, [BindRequired] int pageIndex, [BindRequired] int pageSize)
        {
            GetWishlistsListPerAccountQuery? query = new GetWishlistsListPerAccountQuery
            {
                WishlistAccountId = id,
                WishlistPageIndex = pageIndex,
                WishlistPageSize = pageSize
            };

            List<Wishlist>? result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            List<WishlistGetDto> mappedResult = _mapper.Map<List<WishlistGetDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPatch]
        [Route("{id}/change-name")]
        public async Task<IActionResult> UpdateAccountName(int id, [FromBody] AccountNamePatchDto updatedAccount)
        {
            UpdateAccountRequest? command = new UpdateAccountRequest
            {
                AccountId = id,
                AccountFirstName = updatedAccount.AccountFirstName,
                AccountLastName = updatedAccount.AccountLastName,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/change-mail")]
        public async Task<IActionResult> UpdateAccountMail(int id, [FromBody] AccountEmailPatchDto updatedAccount)
        {
            UpdateAccountRequest? command = new UpdateAccountRequest
            {
                AccountId = id,
                AccountEmail = updatedAccount.AccountEmail,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/change-password")]
        public async Task<IActionResult> UpdateAccountPassword(int id, [FromBody] AccountPasswordPatchDto updatedAccount)
        {
            UpdateAccountRequest? command = new UpdateAccountRequest
            {
                AccountId = id,
                AccountPassword = updatedAccount.AccountPassword,
            };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            DeleteAccountRequest? command = new DeleteAccountRequest { AccountId = id };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}/archive")]
        public async Task<IActionResult> ArchiveAccount(int id)
        {
            ArchiveAccountRequest? command = new ArchiveAccountRequest { AccountId = id };

            Account? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{accountId}/wishlists/{wishlistId}/boardgames/{boardgameId}")]
        public async Task<IActionResult> DeleteWishlistItem(int accountId, int wishlistId, int boardgameId)
        {
            DeleteWishlistItemRequest command = new DeleteWishlistItemRequest
            {
                WishlistAccountId = accountId,
                WishlistId = wishlistId,
                WishlistBoardgameId = boardgameId
            };

            bool result = await _mediator.Send(command);

            if (result == false)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{accountId}/wishlists/{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(int accountId, int wishlistId)
        {
            DeleteWishlistRequest? command = new DeleteWishlistRequest
            {
                WishlistAccountId = accountId,
                WishlistId = wishlistId
            };

            Wishlist? result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
