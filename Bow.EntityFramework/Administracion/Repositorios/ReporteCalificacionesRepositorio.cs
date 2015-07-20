using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Administracion.Repositorios
{
    public class ReporteCalificacionesRepositorio : BowRepositoryBase<ReporteCalificaciones>, IReporteCalificacionesRepositorio
    {
        public ReporteCalificacionesRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<ReporteCalificaciones> GetAllReporteCalificacionesWithTipo()
        {
            return GetAll().Include(m => m.TipoVehiculoReporte).OrderBy(m => m.TipoVehiculoId).ToList();
        }

        public ReporteCalificaciones GetWithTipo(int reporteId)
        {
            return GetAll().Where(r => r.Id == reporteId).Include(m => m.TipoVehiculoReporte).FirstOrDefault();
        }
    }
}
