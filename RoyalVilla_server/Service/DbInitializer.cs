﻿using Common;
using DataAcess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_server.Service.IService;

namespace RoyalVilla_server.Service
{
    public class DbInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public DbInitializer(ApplicationDbContext db,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
         
            if (_db.Roles.Any(x => x.Name == SD.Role_Admin)) return;
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult(); _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,

            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
