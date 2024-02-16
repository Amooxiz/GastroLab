using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface ICookieService
    {
        void SetCookie(string key, string value);
        string GetCookie(string key);
        void RemoveCookie(string key);
    }
}
