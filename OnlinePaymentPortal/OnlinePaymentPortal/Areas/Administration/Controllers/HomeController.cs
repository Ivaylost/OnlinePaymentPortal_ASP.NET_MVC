//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OnlinePaymentPortal.Areas.Administration.Mappers;
//using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
//using OnlinePaymentPortal.Areas.Administration.Models;
//using OnlinePaymentPortal.Data.Models;
//using OnlinePaymentPortal.Services.Interfaces;

//namespace OnlinePaymentPortal.Areas.Administration.Controllers
//{
//    [Area("Administration")]
//    [Authorize(Roles = "admin")]
//    public class HomeController : Controller
//    {
//        private readonly IUserService userService;
//        private readonly IClientService clientService;
//        private readonly IViewModelMapper<Client, ClientViewModel> clientMapper;
//        private readonly IViewModelMapper<User, UserViewModel> userMapper;

//        public HomeController(IUserService userService,
//            IClientService clientService,
//            IViewModelMapper<Client, ClientViewModel> clientMapper,
//            IViewModelMapper<User, UserViewModel> userMapper)
//        {
//            this.userService = userService;
//            this.clientService = clientService;
//            this.clientMapper = clientMapper;
//            this.userMapper = userMapper;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var users =await this.userService.GetTopUsersAsync();
//            var clients =await this.clientService.GetTopClients();

//            var model = new AdminViewModel
//            {
//                Users = users.Select(u => userMapper.MapFrom(u)).ToList(),
//                Clients = clients.Select(u => clientMapper.MapFrom(u)).ToList()

//            };

//            return View(model);

//        }

//        public async Task<IActionResult> ShowAllUsers()
//        {
//            var users = await this.userService.GetTopUsersAsync();
//            var vn = users.Select(u => userMapper.MapFrom(u)).ToList();

//            return PartialView("AllUsers",vn);
//        }

//    }
//}