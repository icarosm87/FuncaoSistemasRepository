using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarCpf(this WebViewPage page, string cpf)
        {
            return  Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
    }
}