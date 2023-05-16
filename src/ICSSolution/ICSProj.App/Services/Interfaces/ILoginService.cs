using ICSProj.BL.Models;

namespace ICSProj.App.Services;

public interface ILoginService
{
    Guid CurrentUserId { get; set; }
    UserDetailModel CurrentUser { get; set; }
}
