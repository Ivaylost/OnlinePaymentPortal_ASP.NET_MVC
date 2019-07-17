using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePaymentPortal.Areas.Administration.Mappers.Interfaces;
using OnlinePaymentPortal.Areas.Administration.Models;
using OnlinePaymentPortal.Data.Models;
using OnlinePaymentPortal.Services.DTOs;
using OnlinePaymentPortal.Services.Interfaces;

//TODO: Refactoring

namespace OnlinePaymentPortal.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class BannerController : Controller
    {
        private readonly IViewModelMapper<IReadOnlyCollection<BannerDTO>, AdminViewModel> bannerViewModelMapper;
        private readonly IViewModelMapper<Banner, DetailsViewModels> detailViewModelMapper;
        private readonly IBannerService bannerService;
        private readonly IHostingEnvironment hostingEnv;

        public BannerController(IViewModelMapper<IReadOnlyCollection<BannerDTO>, AdminViewModel> bannerViewModelMapper,
            IViewModelMapper<Banner, DetailsViewModels> detailViewModelMapper,
            IBannerService bannerService,
            IHostingEnvironment hostingEnv)
        {
            this.bannerViewModelMapper = bannerViewModelMapper ?? throw new ArgumentNullException(nameof(bannerViewModelMapper));
            this.detailViewModelMapper = detailViewModelMapper ?? throw new ArgumentNullException(nameof(detailViewModelMapper));
            this.bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
            this.hostingEnv = hostingEnv ?? throw new ArgumentNullException(nameof(hostingEnv));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddBanner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBanner(BannerViewModel model, IFormFile pic)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return RedirectToAction("Index", "ErrorHandler");
            //}

            var picturePath = OptimizeImage(pic, hostingEnv);

            var banner = await this.bannerService.AddBannerAsync(picturePath, model.StartDate.ToString(),
                model.EndDate.ToString(), model.BannerLink);

            return RedirectToAction("AllBanners", "Banner");
        }

        public async Task<IActionResult> AllBanners(int currentPage = 1)
        {
            var banners = await this.bannerService.GetAllBannersAsync(currentPage);

            var model = this.bannerViewModelMapper.MapFrom(banners.Banners);
            var totalpages = await this.bannerService.GetPageCount();

            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.TotalPages = totalpages;

            return View("AllBanners", model);
        }

        public IActionResult Details()
        {
            return View();
        }

        [Route("Banner/Details/{id?}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var banner = await this.bannerService.GetBannerByIdAsync(id);

            var model = this.detailViewModelMapper.MapFrom(banner);

            return View("Details", model);
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var banner = await this.bannerService.GetBannerByIdAsync(Id);

            var model = this.detailViewModelMapper.MapFrom(banner);

            return View("Edit", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DetailsViewModels model, IFormFile pic)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Index", "ErrorHandler");
            }

            var bannerToUpdate = await this.bannerService.GetBannerByIdAsync(model.Id);
            var picturePath = OptimizeImage(pic, hostingEnv);
            if (picturePath != null)
            {
                var imagePath = Path.Combine(this.hostingEnv.WebRootPath, "images", bannerToUpdate.ImagePath.Remove(0, 9));
                System.IO.File.Delete(imagePath);
                var banner = await this.bannerService.UpdateBannerAsync(model.Id, picturePath, model.BannerLink, model.StartDate, model.EndDate);
                return RedirectToAction("AllBanners", "Banner");
            }
            var editedBanner = await this.bannerService.UpdateBannerAsync(model.Id, bannerToUpdate.ImagePath, model.BannerLink, model.StartDate, model.EndDate);

            return RedirectToAction("AllBanners", "Banner");
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            var banner = await this.bannerService.GetBannerByIdAsync(Id);
            var imagePath = Path.Combine(this.hostingEnv.WebRootPath, "images", banner.ImagePath.Remove(0, 9));

            System.IO.File.Delete(imagePath);

            await this.bannerService.DeleteBannerAsync(banner);

            return RedirectToAction("AllBanners", "Banner");
        }

        private static string OptimizeImage(IFormFile pic, IHostingEnvironment he)
        {
            
            if (pic == null)
            {
                return null;
            }
            var filename = Path.Combine(he.WebRootPath, "images/", Path.GetFileName(pic.FileName));
            var stream = new FileStream(filename, FileMode.Create);
            pic.CopyTo(stream);
            var picturePath = "~/images/" + Path.GetFileName(pic.FileName);
            stream.Close();
            return picturePath;
        }

    }
}