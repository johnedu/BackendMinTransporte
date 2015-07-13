using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using Bow.Administracion.Entidades;
using Bow.Administracion.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    public class AdministracionService : IAdministracionService
    {
        #region Repositorios
        private IPreguntaFrecuenteRepositorio _preguntaFrecuenteRepositorio;
        private IReporteIncidentesRepositorio _reporteIncidentesRepositorio;
        private ITipoReporteRepositorio _tipoReporteRepositorio;
        private ITipoVehiculoRepositorio _tipoVehiculoRepositorio;
        private INoticiasRepositorio _noticiasRepositorio;
        private IItemDiagnosticoRepositorio _itemDiagnosticoRepositorio;
        private IDiagnosticoVialRepositorio _diagnosticoVialRepositorio;
        private IPasoHistoriaVialRepositorio _pasoHistoriaVialRepositorio;
        private IHistoriaVialRepositorio _historiaVialRepositorio;
        private IDeslizadorRepositorio _deslizadorRepositorio;

        public IAbpSession AbpSession { get; set; }

        #endregion

        //Inyección de Dependencia en el Servicio
        public AdministracionService(
            IPreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio,
            IReporteIncidentesRepositorio reporteIncidentesRepositorio,
            ITipoReporteRepositorio tipoReporteRepositorio, 
            ITipoVehiculoRepositorio tipoVehiculoRepositorio,
            INoticiasRepositorio noticiasRepositorio,
            IItemDiagnosticoRepositorio itemDiagnosticoRepositorio,
            IDiagnosticoVialRepositorio diagnosticoVialRepositorio,
            IPasoHistoriaVialRepositorio pasoHistoriaVialRepositorio,
            IHistoriaVialRepositorio historiaVialRepositorio,
            IDeslizadorRepositorio deslizadorRepositorio)
        {
            _preguntaFrecuenteRepositorio = preguntaFrecuenteRepositorio;
            _reporteIncidentesRepositorio = reporteIncidentesRepositorio;
            _tipoReporteRepositorio = tipoReporteRepositorio;
            _tipoVehiculoRepositorio = tipoVehiculoRepositorio;
            _noticiasRepositorio = noticiasRepositorio;
            _itemDiagnosticoRepositorio = itemDiagnosticoRepositorio;
            _diagnosticoVialRepositorio = diagnosticoVialRepositorio;
            _pasoHistoriaVialRepositorio = pasoHistoriaVialRepositorio;
            _historiaVialRepositorio = historiaVialRepositorio;
            _deslizadorRepositorio = deslizadorRepositorio;
            AbpSession = NullAbpSession.Instance;
        }

        /*********************************************************************************************
         ***********************************  Preguntas Frecuentes  **********************************
         *********************************************************************************************/

        public GetPreguntaFrecuenteOutput GetPreguntaFrecuente(GetPreguntaFrecuenteInput faqInput)
        {
            return Mapper.Map<GetPreguntaFrecuenteOutput>(_preguntaFrecuenteRepositorio.Get(faqInput.Id));
        }

        public GetAllPreguntasFrecuentesOutput GetAllPreguntasFrecuentes()
        {
            var listaFAQs = _preguntaFrecuenteRepositorio.GetAllList().OrderBy(p => p.Pregunta);
            return new GetAllPreguntasFrecuentesOutput { PreguntasFrecuentes = Mapper.Map<List<PreguntaFrecuenteOutput>>(listaFAQs) };
        }

        public GetAllPreguntasFrecuentesActivasOutput GetAllPreguntasFrecuentesActivas()
        {
            var listaFAQs = _preguntaFrecuenteRepositorio.GetAll().Where(p => p.EsActiva).OrderBy(p => p.Pregunta);
            return new GetAllPreguntasFrecuentesActivasOutput { PreguntasFrecuentes = Mapper.Map<List<PreguntaFrecuenteOutput>>(listaFAQs) };
        }

        public void SavePreguntaFrecuente(SavePreguntaFrecuenteInput nuevaFaq)
        {
            PreguntaFrecuente existeFAQ = _preguntaFrecuenteRepositorio.FirstOrDefault(p => p.Pregunta.ToLower() == nuevaFaq.Pregunta.ToLower());

            if (existeFAQ == null)
            {
                PreguntaFrecuente preguntaFrecuente = Mapper.Map<PreguntaFrecuente>(nuevaFaq);
                preguntaFrecuente.EsActiva = true;
                preguntaFrecuente.TenantId = BowConsts.TENANT_ID_ACR;
                _preguntaFrecuenteRepositorio.Insert(preguntaFrecuente);
            }
            else
            {
                //var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                var mensajeError = "Ya existe la pregunta frecuente";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeletePreguntaFrecuente(DeletePreguntaFrecuenteInput faqEliminar)
        {
            _preguntaFrecuenteRepositorio.Delete(faqEliminar.Id);
        }

        public void UpdatePreguntaFrecuente(UpdatePreguntaFrecuenteInput faqUpdate)
        {
            PreguntaFrecuente existeFAQ = _preguntaFrecuenteRepositorio.FirstOrDefault(p => p.Pregunta.ToLower() == faqUpdate.Pregunta.ToLower() && p.Id != faqUpdate.Id);

            if (existeFAQ == null)
            {
                PreguntaFrecuente preguntaFrecuente = _preguntaFrecuenteRepositorio.Get(faqUpdate.Id);
                Mapper.Map(faqUpdate, preguntaFrecuente);
                _preguntaFrecuenteRepositorio.Update(preguntaFrecuente);
            }
            else
            {
                var mensajeError = "Ya existe la pregunta frecuente.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        /*********************************************************************************************
         ***********************************  Reporte de Incidentes  *********************************
         *********************************************************************************************/

        public GetAllTiposReporteOutput GetAllTiposReporte()
        {
            var listaTiposReporte = _tipoReporteRepositorio.GetAllList().OrderBy(p => p.Nombre);
            return new GetAllTiposReporteOutput { TiposReporte = Mapper.Map<List<TipoReporteOutput>>(listaTiposReporte) };
        }

        public GetAllReporteIncidentesOutput GetAllReporteIncidentes()
        {
            var listaReporteIncidentes = _reporteIncidentesRepositorio.GetAllReporteIncidentesWithTipo();
            return new GetAllReporteIncidentesOutput { ReportesIncidentes = Mapper.Map<List<ReporteIncidenteOutput>>(listaReporteIncidentes) };
        }

        public void SaveReporteIncidentes(SaveReporteIncidentesInput nuevoReporte)
        {
            ReporteIncidentes reporte = Mapper.Map<ReporteIncidentes>(nuevoReporte);
            reporte.EsActivo = true;
            reporte.TenantId = BowConsts.TENANT_ID_ACR;
            _reporteIncidentesRepositorio.Insert(reporte);
        }

        /*********************************************************************************************
        ******************************************  Deslizador  ***************************************
        *********************************************************************************************/

        public GetAllDeslizadorOutput GetAllDeslizador()
        {
            var listaDeslizador = _deslizadorRepositorio.GetAllList().OrderBy(p => p.Nombre);
            return new GetAllDeslizadorOutput { Deslizador = Mapper.Map<List<DeslizadorOutput>>(listaDeslizador) };
        }

        /*********************************************************************************************
         ******************************************  Noticias  ***************************************
         *********************************************************************************************/

        public GetAllNoticiasOutput GetAllNoticias()
        {
            var listaNoticias = _noticiasRepositorio.GetAllList().OrderBy(p => p.Fecha);
            return new GetAllNoticiasOutput { Noticias = Mapper.Map<List<NoticiasOutput>>(listaNoticias) };
        }

        public GetNoticiasOutput GetNoticias(GetNoticiasInput noticiasInput)
        {
            return Mapper.Map<GetNoticiasOutput>(_noticiasRepositorio.Get(noticiasInput.Id));
        }

        public void SaveNoticias(SaveNoticiasInput nuevaNoticias)
        {
            Noticias existeNoticia = _noticiasRepositorio.FirstOrDefault(p => p.Titulo.ToLower() == nuevaNoticias.Titulo.ToLower());

            if (existeNoticia == null)
            {
                Noticias noticia = Mapper.Map<Noticias>(nuevaNoticias);
                noticia.EsActiva = true;
                noticia.TenantId = BowConsts.TENANT_ID_ACR;
                _noticiasRepositorio.Insert(noticia);
            }
            else
            {
                var mensajeError = "Ya existe la noticia.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateNoticias(UpdateNoticiasInput noticiaUpdate)
        {
            Noticias existeNoticia = _noticiasRepositorio.FirstOrDefault(p => p.Titulo.ToLower() == noticiaUpdate.Titulo.ToLower() && p.Id != noticiaUpdate.Id);

            if (existeNoticia == null)
            {
                Noticias noticia = _noticiasRepositorio.Get(noticiaUpdate.Id);
                Mapper.Map(noticiaUpdate, noticia);
                _noticiasRepositorio.Update(noticia);
            }
            else
            {
                var mensajeError = "Ya existe la noticia.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteNoticias(DeleteNoticiasInput noticiaEliminar)
        {
            _noticiasRepositorio.Delete(noticiaEliminar.Id);
        }

        /*********************************************************************************************
         ***************************************  Historia Vial  *************************************
         *********************************************************************************************/

        public GetAllHistoriasVialesOutput GetAllHistoriasViales()
        {
            var listaHistorias = _historiaVialRepositorio.GetAllList().OrderByDescending(h => h.Id);
            return new GetAllHistoriasVialesOutput { HistoriasViales = Mapper.Map<List<HistoriaVialOutput>>(listaHistorias) };
        }

        public GetHistoriaVialOutput GetHistoriaVial(GetHistoriaVialInput historiaInput)
        {
            return Mapper.Map<GetHistoriaVialOutput>(_historiaVialRepositorio.Get(historiaInput.Id));
        }

        public void SaveHistoriasVial(SaveHistoriasVialInput nuevaHistoria)
        {
            HistoriaVial existeHistoria = _historiaVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevaHistoria.Nombre.ToLower());

            if (existeHistoria == null)
            {
                HistoriaVial historia = Mapper.Map<HistoriaVial>(nuevaHistoria);
                historia.EsActiva = true;
                historia.TenantId = BowConsts.TENANT_ID_ACR;
                _historiaVialRepositorio.Insert(historia);
            }
            else
            {
                var mensajeError = "Ya existe la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateHistoriasVial(UpdateHistoriasVialInput historiaUpdate)
        {
            HistoriaVial existeHistoria = _historiaVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == historiaUpdate.Nombre.ToLower() && p.Id != historiaUpdate.Id);

            if (existeHistoria == null)
            {
                HistoriaVial historia = _historiaVialRepositorio.Get(historiaUpdate.Id);
                Mapper.Map(historiaUpdate, historia);
                _historiaVialRepositorio.Update(historia);
            }
            else
            {
                var mensajeError = "Ya existe la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteHistoriasVial(DeleteHistoriasVialInput historiaEliminar)
        {
            _historiaVialRepositorio.Delete(historiaEliminar.Id);
        }

        public GetAllPasosByHistoriaVialOutput GetAllPasosByHistoriaVial(GetAllPasosByHistoriaVialInput historiaVial)
        {
            var listaPasosHistoria = _pasoHistoriaVialRepositorio.GetAll().Where(p => p.HistoriaVialId == historiaVial.Id).OrderBy(p => p.Id);
            return new GetAllPasosByHistoriaVialOutput { PasosHistoriaVial = Mapper.Map<List<PasoByHistoriaVialOutput>>(listaPasosHistoria) };
        }

        public GetPasoByHistoriaVialOutput GetPasoByHistoriaVial(GetPasoByHistoriaVialInput pasoHistoriaInput)
        {
            return Mapper.Map<GetPasoByHistoriaVialOutput>(_pasoHistoriaVialRepositorio.Get(pasoHistoriaInput.Id));
        }

        public void SavePasoHistoriaVial(SavePasoHistoriaVialInput nuevoPaso)
        {
            PasoHistoriaVial existePasoHistoria = _pasoHistoriaVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoPaso.Nombre.ToLower());

            if (existePasoHistoria == null)
            {
                PasoHistoriaVial pasoHistoria = Mapper.Map<PasoHistoriaVial>(nuevoPaso);
                pasoHistoria.TenantId = BowConsts.TENANT_ID_ACR;
                _pasoHistoriaVialRepositorio.Insert(pasoHistoria);
            }
            else
            {
                var mensajeError = "Ya existe el paso de la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdatePasoHistoriaVial(UpdatePasoHistoriaVialInput pasoHistoriaUpdate)
        {
            PasoHistoriaVial existePasoHistoria = _pasoHistoriaVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == pasoHistoriaUpdate.Nombre.ToLower() && p.Id != pasoHistoriaUpdate.Id);

            if (existePasoHistoria == null)
            {
                PasoHistoriaVial pasoHistoria = _pasoHistoriaVialRepositorio.Get(pasoHistoriaUpdate.Id);
                Mapper.Map(pasoHistoriaUpdate, pasoHistoria);
                _pasoHistoriaVialRepositorio.Update(pasoHistoria);
            }
            else
            {
                var mensajeError = "Ya existe el paso de la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeletePasoHistoriaVial(DeletePasoHistoriaVialInput pasoHistoriaEliminar)
        {
            _pasoHistoriaVialRepositorio.Delete(pasoHistoriaEliminar.Id);
        }

        ///*********************************************************************************************
        // *************************************  Diagnostico Vial  ************************************
        // *********************************************************************************************/

        public GetAllDiagnosticosVialesOutput GetAllDiagnosticosViales()
        {
            var listaDiagnosticos = _diagnosticoVialRepositorio.GetAllList().OrderBy(d => d.Nombre);
            return new GetAllDiagnosticosVialesOutput { DiagnosticosViales = Mapper.Map<List<DiagnosticoVialOutput>>(listaDiagnosticos) };
        }

        public GetDiagnosticoVialOutput GetDiagnosticoVial(GetDiagnosticoVialInput diagnosticoInput)
        {
            return Mapper.Map<GetDiagnosticoVialOutput>(_diagnosticoVialRepositorio.Get(diagnosticoInput.Id));
        }

        public void SaveDiagnosticoVial(SaveDiagnosticoVialInput nuevoDiagnostico)
        {
            DiagnosticoVial existeDiagnostico = _diagnosticoVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoDiagnostico.Nombre.ToLower());

            if (existeDiagnostico == null)
            {
                DiagnosticoVial diagnostico = Mapper.Map<DiagnosticoVial>(nuevoDiagnostico);
                diagnostico.EsActivo = true;
                diagnostico.TenantId = BowConsts.TENANT_ID_ACR;
                _diagnosticoVialRepositorio.Insert(diagnostico);
            }
            else
            {
                var mensajeError = "Ya existe el diagnóstico vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateDiagnosticoVial(UpdateDiagnosticoVialInput diagnosticoUpdate)
        {
            DiagnosticoVial existeDiagnostico = _diagnosticoVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == diagnosticoUpdate.Nombre.ToLower() && p.Id != diagnosticoUpdate.Id);

            if (existeDiagnostico == null)
            {
                DiagnosticoVial diagnostico = _diagnosticoVialRepositorio.Get(diagnosticoUpdate.Id);
                Mapper.Map(diagnosticoUpdate, diagnostico);
                _diagnosticoVialRepositorio.Update(diagnostico);
            }
            else
            {
                var mensajeError = "Ya existe el diagnóstico vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteDiagnosticoVial(DeleteDiagnosticoVialInput diagnosticoEliminar)
        {
            _diagnosticoVialRepositorio.Delete(diagnosticoEliminar.Id);
        }

        public GetAllItemsByDiagnosticoVialOutput GetAllItemsByDiagnosticoVial(GetAllItemsByDiagnosticoVialInput itemDiagnostico)
        {
            var listaItemsDiagnostico = _itemDiagnosticoRepositorio.GetAll().Where(p => p.DiagnosticoVialId == itemDiagnostico.Id).OrderBy(p => p.Id);
            return new GetAllItemsByDiagnosticoVialOutput { ItemsDiagnosticoVial = Mapper.Map<List<ItemByDiagnosticoVialOutput>>(listaItemsDiagnostico) };
        }

        public GetItemByDiagnosticoVialOutput GetItemByDiagnosticoVial(GetItemByDiagnosticoVialInput itemDiagnosticoInput)
        {
            return Mapper.Map<GetItemByDiagnosticoVialOutput>(_itemDiagnosticoRepositorio.Get(itemDiagnosticoInput.Id));
        }

        public void SaveItemDiagnosticoVial(SaveItemDiagnosticoVialInput nuevoItem)
        {
            ItemDiagnostico existeItemDiagnostico = _itemDiagnosticoRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoItem.Nombre.ToLower());

            if (existeItemDiagnostico == null)
            {
                ItemDiagnostico itemDiagnostico = Mapper.Map<ItemDiagnostico>(nuevoItem);
                itemDiagnostico.EsActivo = true;
                itemDiagnostico.TenantId = BowConsts.TENANT_ID_ACR;
                _itemDiagnosticoRepositorio.Insert(itemDiagnostico);
            }
            else
            {
                var mensajeError = "Ya existe el item del diagnóstico vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateItemDiagnosticoVial(UpdateItemDiagnosticoVialInput itemDiagnosticoUpdate)
        {
            ItemDiagnostico existeItemDiagnostico = _itemDiagnosticoRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == itemDiagnosticoUpdate.Nombre.ToLower() && p.Id != itemDiagnosticoUpdate.Id);

            if (existeItemDiagnostico == null)
            {
                ItemDiagnostico itemDiagnostico = _itemDiagnosticoRepositorio.Get(itemDiagnosticoUpdate.Id);
                Mapper.Map(itemDiagnosticoUpdate, itemDiagnostico);
                _itemDiagnosticoRepositorio.Update(itemDiagnostico);
            }
            else
            {
                var mensajeError = "Ya existe el item del diagnóstico vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteItemDiagnosticoVial(DeleteItemDiagnosticoVialInput itemDiagnosticoEliminar)
        {
            _itemDiagnosticoRepositorio.Delete(itemDiagnosticoEliminar.Id);
        }

    }
}
