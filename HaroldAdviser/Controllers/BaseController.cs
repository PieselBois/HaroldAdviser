using HaroldAdviser.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Octokit.Internal;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

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

        protected async Task<IReadOnlyList<Octokit.Repository>> GetRepositories()
        {
            var user = GetUser();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var github = new GitHubClient(new ProductHeaderValue("AspNetCoreGitHubAuth"),
                new InMemoryCredentialStore(new Credentials(accessToken)));
            var repositories = await github.Repository.GetAllForUser(user.Login);
            return repositories;
        }
    }
}