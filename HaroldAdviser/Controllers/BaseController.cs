﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using HaroldAdviser.ViewModels;

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
                Url = User.FindFirst(c => c.Type == "urn:github:url")?.Value,
                Avatar = User.FindFirst(c => c.Type == "urn:github:avatar")?.Value
            };

            return model;
        }

        protected async Task<IReadOnlyList<Octokit.Repository>> GetRepositories()
        {
            var user = GetUser();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var github = new GitHubClient(new ProductHeaderValue("AspNetCoreGitHubAuth"),
                new InMemoryCredentialStore(new Credentials(accessToken)));
            return await github.Repository.GetAllForUser(user.Login);
        }

        protected static string Encode(Guid guid)
        {
            var encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        protected static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");
            var buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }
    }
}