using System;
using System.Text;
using System.Security.Cryptography;
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

        private String CheckHMACSHA256()
        {
            String hashKey = "38453613e7f44dc58732bad3dca2bca3";
            String rawAnswer = Request.Form["kr-answer"];
            String rawAnswerHash = Request.Form["kr-hash"];

            byte[] hashKeyByte = new UTF8Encoding().GetBytes(hashKey);
            byte[] rawAnswerByte = new UTF8Encoding().GetBytes(rawAnswer);
            byte[] calculatedHashByte = new HMACSHA256(hashKeyByte).ComputeHash(rawAnswerByte);
            String calculatedHash = BitConverter.ToString(calculatedHashByte).Replace("-", "").ToLower();

            if (calculatedHash == rawAnswerHash) return "OK";
            return "Invalid hash";
        }

        public void OnPost()
        {
            ViewData["Payment"] = HttpUtility.HtmlDecode(Request.Form["kr-answer"]);
            ViewData["SHACheck"] = this.CheckHMACSHA256();
        }
    }
}