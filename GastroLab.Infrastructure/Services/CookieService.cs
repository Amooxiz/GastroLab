using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value);
        }

        public string GetCookie(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
        
    }
}
