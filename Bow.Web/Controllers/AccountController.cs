using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bow.Seguridad.Usuarios;
using Abp.UI;
using Abp.Web.Mvc.Models;
using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Bow.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bow.Seguridad.MultiTenancy;
using Abp.Runtime.Session;
using Bow.Administracion.Entidades;
using Bow.Administracion.Repositorios;

namespace Bow.Web.Controllers
{
    public class AccountController : BowControllerBase
    {
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;

        public IAbpSession _abpSession { get; set; }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(UserManager userManager, TenantManager tenantManager)
        {
            _userManager = userManager;
            _tenantManager = tenantManager;

            _abpSession = NullAbpSession.Instance;
        }

        public async Task<ActionResult> Login(string tenantName = "", string returnUrl = "")
        {
            Tenant existeTenant = await _tenantManager.FindByTenancyNameAsync(tenantName);

            if (existeTenant == null)
                return Content("No Existe");
            else
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    returnUrl = Request.ApplicationPath;
                }

                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "")
        {
            var tenantname = RouteData.Values["tenant"];
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Debe indicar todos los datos");
            }

            var loginResult = await _userManager.LoginAsync(loginModel.Username, loginModel.Password, "Default");

            switch(loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    break;
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    User usuario = new User();
                    usuario.TenantId = BowConsts.TENANT_ID_ACR;
                    usuario.UserName = loginModel.Username;
                    usuario.Name = "Profesional Reintegrador";
                    usuario.Surname = "ACR";
                    usuario.EmailAddress = "acr@gmail.com";
                    usuario.IsEmailConfirmed = true;
                    var result = await _userManager.CreateAsync(usuario, loginModel.Password);
                    throw new UserFriendlyException("Es la primera vez que ingresa al sitio web administrativo, por favor vuelva a ingresar el nombre de usuario y contraseña");
                case AbpLoginResultType.InvalidTenancyName:
                    throw new UserFriendlyException("No hay tenant con nombre: " + loginModel.TenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    throw new UserFriendlyException("El Tenant no está activo: " + loginModel.TenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    throw new UserFriendlyException("El Usuario no está activo: " + loginModel.Username);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    throw new UserFriendlyException("El Email no está confirmado!");
                default: //Can not fall to default for now. But other result types can be added in the future and we may forget to handle it
                    throw new UserFriendlyException("Ocurrió un problema con el usuario: " + loginResult.Result);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = loginModel.RememberMe }, loginResult.Identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return Json(new MvcAjaxResponse { TargetUrl = returnUrl });
        }

        public async Task<ActionResult> Logout()
        {
            Tenant tenant = await _tenantManager.GetByIdAsync(_abpSession.GetTenantId());
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account", new { tenantName = tenant.TenancyName});
        }

    }
}
