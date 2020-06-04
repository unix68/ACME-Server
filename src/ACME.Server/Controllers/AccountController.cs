﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TGIT.ACME.Protocol.HttpModel.Requests;
using TGIT.ACME.Protocol.Infrastructure;
using TGIT.ACME.Protocol.Model.Exceptions;
using TGIT.ACME.Protocol.Services;
using TGIT.ACME.Server.Filters;

namespace TGIT.ACME.Server.Controllers
{
    [ApiController]
    [AddNextNonce]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("/new-account", Name = "NewAccount")]
        [HttpPost]
        public async Task<ActionResult<Protocol.HttpModel.Account>> CreateOrGetAccount(AcmePostRequest<CreateOrGetAccount> request)
        {
            if(request.Payload!.Value.OnlyReturnExisting)
                return await FindAccountAsync(request);

            return await CreateAccountAsync(request);
        }

        private async Task<ActionResult<Protocol.HttpModel.Account>> CreateAccountAsync(AcmePostRequest<CreateOrGetAccount> request)
        {
            if (request.Payload == null)
                throw new MalformedRequestException("Payload was empty or could not be read.");

            var account = await _accountService.CreateAccountAsync(
                request.Header.Value.Jwk!, //Post requests are validated, JWK exists.
                request.Payload.Value.Contact,
                request.Payload.Value.TermsOfServiceAgreed,
                HttpContext.RequestAborted);

            var ordersUrl = Url.RouteUrl("OrderList", new { accountId = account.AccountId }, "https");
            var accountResponse = new Protocol.HttpModel.Account(account, ordersUrl);

            var accountUrl = Url.RouteUrl("Account", new { accountId = account.AccountId }, "https");
            return new CreatedResult(accountUrl, accountResponse);
        }

        private Task<ActionResult<Protocol.HttpModel.Account>> FindAccountAsync(AcmePostRequest<CreateOrGetAccount> request)
        {
            throw new NotImplementedException();
        }

        [Route("/account/{accountId}", Name = "Account")]
        [HttpPost, HttpPut]
        public Task<ActionResult<Protocol.HttpModel.Account>> SetAccount(string accountId)
        {
            throw new NotImplementedException();
        }

        [Route("/account/{accountId}/orders", Name = "OrderList")]
        [HttpPost]
        public Task<ActionResult<Protocol.HttpModel.OrdersList>> GetOrdersList(string accountId, AcmePostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
