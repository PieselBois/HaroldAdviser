using HaroldAdviser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace HaroldAdviser.Controllers
{
    public class BaseController : Controller
    {
        protected GithubUser GetUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("User not autheticated");
            }

            var model = new GithubUser
            {
                Id = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Name = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
                Login = User.FindFirst(c => c.Type == "urn:github:login")?.Value,
                Url = User.FindFirst(c => c.Type == "urn:github:url")?.Value
            };

            return model;
        }
    }
}