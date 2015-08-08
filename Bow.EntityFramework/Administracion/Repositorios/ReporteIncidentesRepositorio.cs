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
    public class ReporteIncidentesRepositorio : BowRepositoryBase<ReporteIncidentes>, IReporteIncidentesRepositorio
    {
        public ReporteIncidentesRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<ReporteIncidentes> GetAllReporteIncidentesWithTipo()
        {
            return GetAll().Include(m => m.TipoReporteIncidente).OrderBy(m => m.TipoReporteId).ToList();
        }

        public ReporteIncidentes GetWithTipo(int reporteId)
        {
            return GetAll().Where(r => r.Id == reporteId).Include(m => m.TipoReporteIncidente).FirstOrDefault();
        }

        public List<ReporteIncidentes> GetAllRiesgosCercanos(decimal latitudMinima, decimal latitudMaxima, decimal longitudMinima, decimal longitudMaxima)
        {
            return GetAll().Where(r => r.EsActivo 
                && r.Latitud >= latitudMinima
                && r.Latitud <= latitudMaxima
                && r.Longitud >= longitudMinima
                && r.Longitud <= longitudMaxima)
                .Include(m => m.TipoReporteIncidente).ToList();
        }
    }
}
