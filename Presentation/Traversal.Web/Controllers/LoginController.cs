﻿using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Customers;
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Web.Models;

namespace Traversal.Web.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly ICustomerService customerService;

		public LoginController(UserManager<User> userManager, ICustomerService customerService, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.customerService = customerService;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
                    ModelState.AddModelError("Unauthorized", "Email veya şifre hatalı!");
                }
			}
            return View(model);
		}

		//[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterModel model)
		{
			User user = new()
			{
				Email = model.Email,
				UserName = model.Email,
				PhoneNumber = model.PhoneNumber
			};
			if (model.Password == model.ConfirmPassword)
			{
				var result = await userManager.CreateAsync(user: user, password: model.Password);
				if (result.Succeeded && ModelState.IsValid)
				{
					var customer = new AddCustomerDto()
					{
						UserId = user.Id,
						FirstName = model.FirstName,
						LastName = model.LastName,
						Email = model.Email,
						CellPhone = model.PhoneNumber,
						BirthDate = model.BirthDate
					};
					await customerService.AddCustomer(customer);
					return RedirectToAction("Login");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}
			}
			return View(model);
		}
	}
}
