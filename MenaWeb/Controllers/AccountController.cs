﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MenaWeb.SecurityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace MenaWeb.Controllers
{
    public class AccountController : Controller  
    {
        private IConfiguration config;
        public AccountController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var connectionStringTemplate = "server=" + config.GetValue<string>("Server") + ";" +
                "port=" + config.GetValue<string>("Port") + ";" +
                "user={0};password={1};" +
                "database=" + config.GetValue<string>("Database") + ";";

            var connectionString = string.Format(connectionStringTemplate, "mena", "mena81");

            try
            {
                var conn = new MySqlConnection(connectionString);
                conn.Open();
                var userName = User.Identity.Name.ToUpper();
                if (userName == "PWR\\IGNATOV")
                {
                    userName = "PWR\\IGNVV";
                }
                var query = new MySqlCommand("SELECT password FROM acl_users WHERE LOWER(user_name) = LOWER(@userName)", conn);
                query.Parameters.AddWithValue("@userName", userName);
                var password = (string)query.ExecuteScalar();
                conn.Close();
                var passwordBlank = AccountHelper.DecryptPassword(password);


                connectionString = string.Format(connectionStringTemplate, userName, passwordBlank);

                var connPersonal = new MySqlConnection(connectionString);
                connPersonal.Open();
                connPersonal.Close();

                await Authenticate(userName, connectionString);
            }
            catch (Exception)
            {
                ViewData["Controller"] = "Account";
                ViewData["TextError"] = new HtmlString("Ошибка при соединении с базой данных. " +
                    "Возможно у вас нет прав доступа к данной программе. Нажмите кнопку \"Повторить\". " +
                    "В случае повтора ошибки обратитесь к администратору по телефону 349-671");
                return View("Error");
            }
            if (returnUrl == null)
                return RedirectToAction("Index", "Contract");
            else
                return RedirectPermanent(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult HashPassword(string password)
        {
            if (password == null)
                return Content("Не указан параметр password");
            return Content(AccountHelper.EncryptPassword(password));
        }

        public async Task<IActionResult> Refresh()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string userName, string connString)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim("connString", connString, ClaimValueTypes.String)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}