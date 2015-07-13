using Abp.Application.Services;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    /// <summary>
    ///     Definición de los servicios ofrecidos por el módulo de Administración
    /// </summary>
    public interface IAdministracionService : IApplicationService
    {
        // Preguntas Frecuentes

        GetPreguntaFrecuenteOutput GetPreguntaFrecuente(GetPreguntaFrecuenteInput paisInput);

        GetAllPreguntasFrecuentesOutput GetAllPreguntasFrecuentes();

        GetAllPreguntasFrecuentesActivasOutput GetAllPreguntasFrecuentesActivas();

        void SavePreguntaFrecuente(SavePreguntaFrecuenteInput nuevoPais);

        void UpdatePreguntaFrecuente(UpdatePreguntaFrecuenteInput paisUpdate);

        void DeletePreguntaFrecuente(DeletePreguntaFrecuenteInput paisEliminar);

        //  Reporte Incidencias

        GetAllTiposReporteOutput GetAllTiposReporte();

        GetAllReporteIncidentesOutput GetAllReporteIncidentes();

        void SaveReporteIncidentes(SaveReporteIncidentesInput nuevoReporte);

        //  Noticias

        GetAllNoticiasOutput GetAllNoticias();

       GetNoticiasOutput GetNoticias(GetNoticiasInput noticiasInput)
       

        void SaveNoticias(SaveNoticiasInput nuevaNoticias)
      

        void UpdateNoticias(UpdateNoticiasInput noticiaUpdate)
        

        void DeleteNoticias(DeleteNoticiasInput noticiaEliminar)
       

        //  Historia Vial

        GetAllHistoriasVialesOutput GetAllHistoriasViales();

        GetHistoriaVialOutput GetHistoriaVial(GetHistoriaVialInput historiaInput);

        void SaveHistoriasVial(SaveHistoriasVialInput nuevaHistoria);

        void UpdateHistoriasVial(UpdateHistoriasVialInput historiaUpdate);

        void DeleteHistoriasVial(DeleteHistoriasVialInput historiaEliminar);

        GetAllPasosByHistoriaVialOutput GetAllPasosByHistoriaVial(GetAllPasosByHistoriaVialInput historiaVial);

        GetPasoByHistoriaVialOutput GetPasoByHistoriaVial(GetPasoByHistoriaVialInput pasoHistoriaInput);

        void SavePasoHistoriaVial(SavePasoHistoriaVialInput nuevoPaso);

        void UpdatePasoHistoriaVial(UpdatePasoHistoriaVialInput pasoHistoriaUpdate);

        void DeletePasoHistoriaVial(DeletePasoHistoriaVialInput pasoHistoriaEliminar);

        //  Diagnostico Vial

        GetAllDiagnosticosVialesOutput GetAllDiagnosticosViales();

        GetDiagnosticoVialOutput GetDiagnosticoVial(GetDiagnosticoVialInput diagnosticoInput);

        void SaveDiagnosticoVial(SaveDiagnosticoVialInput nuevoDiagnostico);

        void UpdateDiagnosticoVial(UpdateDiagnosticoVialInput diagnosticoUpdate);

        void DeleteDiagnosticoVial(DeleteDiagnosticoVialInput diagnosticoEliminar);

        GetAllItemsByDiagnosticoVialOutput GetAllItemsByDiagnosticoVial(GetAllItemsByDiagnosticoVialInput itemDiagnostico);

        GetItemByDiagnosticoVialOutput GetItemByDiagnosticoVial(GetItemByDiagnosticoVialInput itemDiagnosticoInput);

        void SaveItemDiagnosticoVial(SaveItemDiagnosticoVialInput nuevoItem);

        void UpdateItemDiagnosticoVial(UpdateItemDiagnosticoVialInput itemDiagnosticoUpdate);

        void DeleteItemDiagnosticoVial(DeleteItemDiagnosticoVialInput itemDiagnosticoEliminar);

    }
}


