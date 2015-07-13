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
    public class DiagnosticoVialRepositorio : BowRepositoryBase<DiagnosticoVial>, IDiagnosticoVialRepositorio
    {
        public DiagnosticoVialRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<ReporteIncidentes> GetAllReporteIncidentesWithTipo()
        {
            return GetAll().Include(m => m.TipoReporteIncidente).OrderBy(m => m.TipoReporteId).ToList();
        }
    }
}
