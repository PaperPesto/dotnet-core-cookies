using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITransientSalutante _transientsalutante;
        private readonly IScopedSalutante _scopedsalutante;
        private readonly ISingletonSalutante _singletonsalutante;
        private readonly IService _service;

        private readonly IAntiforgery _antiforgery;



        // COSTRUTTORE
        public ValuesController(ITransientSalutante transientsalutante, IScopedSalutante scopedsalutante, ISingletonSalutante singletonsalutante, IService service, IAntiforgery antiforgery)
        {
            _transientsalutante = transientsalutante;
            _scopedsalutante = scopedsalutante;
            _singletonsalutante = singletonsalutante;
            _service = service;

            _antiforgery = antiforgery;
        }




        [HttpGet("saluta")]
        public ActionResult<IEnumerable<string>> Saluta()
        {
            Response.Cookies.Append("ciccio", "baliccio");

            return new JsonResult(new {
                controller = new { transient = _transientsalutante.SayHello(), scoped = _scopedsalutante.SayHello(), singleton = _singletonsalutante.SayHello() },
                service = new { transient = _service.GetTransient(), scoped = _service.GetScoped(), singleton = _service.GetSingleton() }
            });
        }

        [HttpGet("risaluta")]
        public ActionResult<string> Risaluta()
        {
            Response.Cookies.Append("alan", "gor");

            return "CIAO";
        }

        /// <summary>
        /// Chiamo questo endpoint (POST) che richiede l'esistenza di un Cookie antiforgery
        /// Da stacktrace "The required antiforgery cookie ".AspNetCore.Antiforgery.RujbfmogA64" is not present"
        /// </summary>
        /// <returns></returns>
        [HttpPost("forgery")]
        public ActionResult<string> Forgery()
        {
            Response.Cookies.Append("forgery", "giovanni");

            return "CIAO";
        }

        [HttpGet("login")]
        public ActionResult<string> Login()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("XSFR-REQUEST-TOKEN", tokens.RequestToken, new Microsoft.AspNetCore.Http.CookieOptions() { HttpOnly = false });

            return NoContent();
        }

        [HttpGet("getcookies")]
        public JsonResult GetCookies()
        {
            var cookies = Request.Cookies.ToList();
            return new JsonResult(cookies);
        }

        [HttpGet("addcookie")]
        public ActionResult<string> AddCookie(string key, string value)
        {
            Response.Cookies.Append(key, value, new Microsoft.AspNetCore.Http.CookieOptions() { Expires = DateTime.Now.AddSeconds(30) });
            return "ok";
        }
    }
}
