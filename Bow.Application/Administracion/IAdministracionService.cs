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

        //  Tipos y Categorías

        GetAllTiposReporteOutput GetAllTiposReporte();

        GetAllCategoriasOutput GetAllCategorias();

        GetAllTiposReporteOutput GetAllTiposVehiculo();

        GetAllTiposReporteOutput GetAllTipos();

        GetTipoReporteOutput GetTipoReporte(GetTipoReporteInput categoriaInput);

        void SaveTipo(SaveTipoInput nuevoTipo);

        void UpdateTipo(UpdateTipoInput tipoUpdate);

        void DeleteTipo(DeleteTipoInput tipoEliminar);

        //  Reporte Incidencias

        GetReporteIncidentesOutput GetReporteIncidentes(GetReporteIncidentesInput reporteInput);

        GetAllReporteIncidentesOutput GetAllReporteIncidentes();

        void SaveReporteIncidentes(SaveReporteIncidentesInput nuevoReporte);

        void UpdateStateReporteIncidentes(UpdateStateReporteIncidentesInput reporteUpdate);

        //  Reporte Calificaciones

        GetReporteCalificacionesOutput GetReporteCalificaciones(GetReporteCalificacionesInput reporteInput);

        GetAllReportesCalificacionesOutput GetAllReporteCalificaciones();

        void SaveReporteCalificacion(SaveReporteCalificacionInput nuevaCalificacion);

        void UpdateStateCalificacionIncidentes(UpdateStateCalificacionIncidentesInput reporteUpdate);

        //  Deslizador

        GetAllDeslizadorOutput GetAllDeslizador();

        GetDeslizadorOutput GetDeslizador(GetDeslizadorInput deslizadorInput);
       

        void SaveDeslizador(SaveDeslizadorInput nuevaDeslizador);
      

        void UpdateDeslizador(UpdateDeslizadorInput deslizadorUpdate);
    

        void DeleteDeslizador(DeleteDeslizadorInput deslizadorEliminar);
       

        //  Noticias

        GetAllNoticiasOutput GetAllNoticias();

        GetNoticiasOutput GetNoticias(GetNoticiasInput noticiasInput);

        void SaveNoticias(SaveNoticiasInput nuevaNoticias);

        void UpdateNoticias(UpdateNoticiasInput noticiaUpdate);

        void DeleteNoticias(DeleteNoticiasInput noticiaEliminar);
       

        //  Historia Vial

        GetAllHistoriasVialesOutput GetAllHistoriasViales();

        GetAllHistoriasVialesActivasOutput GetAllHistoriasVialesActivas();

        GetHistoriaVialOutput GetHistoriaVial(GetHistoriaVialInput historiaInput);

        void SaveHistoriasVial(SaveHistoriasVialInput nuevaHistoria);

        void UpdateHistoriasVial(UpdateHistoriasVialInput historiaUpdate);

        void DeleteHistoriasVial(DeleteHistoriasVialInput historiaEliminar);
        
        //  Diagnostico Vial

        GetAllItemsByDiagnosticoVialOutput GetAllItemsDiagnosticoVial();

        GetItemByDiagnosticoVialOutput GetItemByDiagnosticoVial(GetItemByDiagnosticoVialInput itemDiagnosticoInput);

        void SaveItemDiagnosticoVial(SaveItemDiagnosticoVialInput nuevoItem);

        void UpdateItemDiagnosticoVial(UpdateItemDiagnosticoVialInput itemDiagnosticoUpdate);

        void DeleteItemDiagnosticoVial(DeleteItemDiagnosticoVialInput itemDiagnosticoEliminar);


    }
}


