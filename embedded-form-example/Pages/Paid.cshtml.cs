using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace embedded_form_example.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class PaidModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            ViewData["Payment"] = HttpUtility.HtmlDecode(Request.Form["kr-answer"]);
        }
    }
}