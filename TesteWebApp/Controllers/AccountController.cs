﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TesteWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(ILogger<AccountController> logger, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string returnUrl)
        {
            string decodeUrl = string.Empty;
            if (string.IsNullOrEmpty(returnUrl))
            {
                decodeUrl = WebUtility.UrlDecode(returnUrl);
            }
            var client = new RestClient(_config.GetSection("ApiSettings")["ApiTeste"]);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            var user = new UserModel { UserName = username, Password = password };
            request.AddHeader("Content-Type", "applicattion/json");
            request.AddJsonBody(user);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var tkn = JsonConvert.DeserializeObject<TokenModel>(response.Content);
                GravaCookie(user, tkn).Wait();
                if (Url.IsLocalUrl(decodeUrl))
                {
                    return Redirect(decodeUrl);
                }
                else
                {
                    return RedirectToAction("Search", "Home");
                }
            }
            else
            {
                ViewBag.Erro = "Usuario e/ou senha inválidos!";
                return View();
            }
        }

        private async Task GravaCookie(UserModel user, TokenModel tkn)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("TesteWebApp", tkn.Token),
                new Claim(ClaimTypes.Expiration, tkn.Expiration)
            };
            var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.Parse(tkn.Expiration)
                });
        }

        public IActionResult AlterarSenha(string userId, string code)
        {
            ViewBag.Code = code;
            ViewBag.Id = userId;
            return View();
        }
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }

    internal class TokenModel
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
    }

    internal class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
