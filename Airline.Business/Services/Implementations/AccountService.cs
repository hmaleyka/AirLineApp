using Airline.Business.Exceptions;
using Airline.Business.Exceptions.Common;
using Airline.Business.Helpers;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.AccountVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
                }
            }
        }

        public async Task Login(LoginVM vm)
        {
            var exists = vm.UsernameOrEmail == null || vm.Password == null;

            if (exists) throw new NullFieldException("All fields are required!", nameof(vm.UsernameOrEmail));

            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

                if (user is null) throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.UsernameOrEmail));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);

            if (!result.Succeeded)
            {
                throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.Password));
            }

            await _signInManager.SignInAsync(user, true);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterVM vm)
        {
            var exists = vm.Name == null || vm.Password == null || vm.Surname == null
               || vm.Username == null || vm.Email == null || vm.ConfirmPassword == null;
            if (exists) throw new NullFieldException("all fields are required", nameof(vm.Name));

            var usedEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (usedEmail is null)
            {
                AppUser user = new()
                {
                    Name = vm.Name,
                    Surname = vm.Surname,
                    Email = vm.Email,
                    UserName = vm.Username
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        throw new UserRegistrationException($"{item.Description}", nameof(item));
                    }
                }
            }
            else
            {
                throw new UsedEmailException("This email address used before, try another!", nameof(vm.Email));
            }
            //await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());



        }

        public async Task Subscription(SubscribeVM vm)
        {
            var existsEmail = await _context.subscribe.FirstOrDefaultAsync(x => x.EmailAddress == vm.EmailAddress);

            if (existsEmail is null)
            {
                Subscribe newSubscription = new()
                {
                    EmailAddress = vm.EmailAddress,
                    CreatedDate = DateTime.Now,
                    //UpdatedDate = DateTime.Now,
                };

                await _context.subscribe.AddAsync(newSubscription);
                await _context.SaveChangesAsync();
                SendEmailService.SendEmail(to: vm.EmailAddress, name: "Airline Team");
            }
            else
            {
                throw new UsedEmailException("This email address used before, try another!", nameof(vm.EmailAddress));
            }

        }
    }
}
