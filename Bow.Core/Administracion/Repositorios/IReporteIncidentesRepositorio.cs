using Abp.Domain.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Repositorios
{
    public interface IReporteIncidentesRepositorio : IRepository<ReporteIncidentes>
    {
        List<ReporteIncidentes> GetAllReporteIncidentesWithTipo();
        ReporteIncidentes GetWithTipo(int reporteId);
    }
}
