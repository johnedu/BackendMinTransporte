using Abp.Domain.Repositories;
using Bow.Seguridad.MultiTenancy;
using Bow.Seguridad.Usuarios;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Seguridad
{
    public class SeguridadService : ISeguridadService
    {
        #region Repositorios
        private TenantManager _tenantManager;
        private UserManager _userManager;
        #endregion

        public SeguridadService(TenantManager tenantManager, UserManager userManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
        }
    }
}
