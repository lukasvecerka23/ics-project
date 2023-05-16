using ICSProj.BL.Models;

namespace ICSProj.App.Services;

public class LoginService: ILoginService
{
    public Guid CurrentUserId { get; set; }

    public UserDetailModel CurrentUser { get; set; } = UserDetailModel.Empty;
}
