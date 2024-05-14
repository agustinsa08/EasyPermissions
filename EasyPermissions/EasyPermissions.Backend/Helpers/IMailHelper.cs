using EasyPermissions.Shared.Responses;

namespace EasyPermissions.Backend.Helpers
{
    public interface IMailHelper
    {
        ActionResponse<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}