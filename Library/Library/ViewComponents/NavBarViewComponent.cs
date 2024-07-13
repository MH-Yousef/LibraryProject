using Data.DTOs.Notification;
using Microsoft.AspNetCore.Mvc;
using Service.FrenidShipServices;
using Service.User;
using System.Runtime.Intrinsics.X86;

namespace Library.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly IFreindShipService _freindShipService;
        public NavBarViewComponent(IAppUserService appUserService, IFreindShipService freindShipService)
        {
            _appUserService = appUserService;
            _freindShipService = freindShipService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var IsSignIn = _appUserService.IsSignIn();
            if (IsSignIn)
            {
                var user = await _appUserService.GetUser();
                ViewBag.Notifications = await _freindShipService.GetFreindRequestsReceive(user.Id);
                ViewBag.Name = user.FullName;
                ViewBag.UserImage = user.Image;
                return View(user);
            }
            return View();
        }
    }
}
