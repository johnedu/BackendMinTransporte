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
using System.IO;
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
        private IReporteCalificacionesRepositorio _reporteCalificacionesRepositorio;
        private INoticiasRepositorio _noticiasRepositorio;
        private IItemDiagnosticoRepositorio _itemDiagnosticoRepositorio;
        private IHistoriaVialRepositorio _historiaVialRepositorio;
        private IDeslizadorRepositorio _deslizadorRepositorio;

        public IAbpSession AbpSession { get; set; }

        #endregion

        //Inyección de Dependencia en el Servicio
        public AdministracionService(
            IPreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio,
            IReporteIncidentesRepositorio reporteIncidentesRepositorio,
            ITipoReporteRepositorio tipoReporteRepositorio,
            IReporteCalificacionesRepositorio reporteCalificacionesRepositorio,
            INoticiasRepositorio noticiasRepositorio,
            IItemDiagnosticoRepositorio itemDiagnosticoRepositorio,
            IHistoriaVialRepositorio historiaVialRepositorio,
            IDeslizadorRepositorio deslizadorRepositorio)
        {
            _preguntaFrecuenteRepositorio = preguntaFrecuenteRepositorio;
            _reporteIncidentesRepositorio = reporteIncidentesRepositorio;
            _tipoReporteRepositorio = tipoReporteRepositorio;
            _reporteCalificacionesRepositorio = reporteCalificacionesRepositorio;
            _noticiasRepositorio = noticiasRepositorio;
            _itemDiagnosticoRepositorio = itemDiagnosticoRepositorio;
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
                preguntaFrecuente.FechaPublicacion = DateTime.Now;
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
            var listaTiposReporte = _tipoReporteRepositorio.GetAll().Where(t => t.TipoCategoria.Equals(BowConsts.CATEGORIA_REPORTE)).ToList().OrderBy(p => p.Nombre);
            return new GetAllTiposReporteOutput { TiposReporte = Mapper.Map<List<TipoReporteOutput>>(listaTiposReporte) };
        }

        public GetAllTiposReporteOutput GetAllTiposVehiculo()
        {
            var listaTiposVehiculo = _tipoReporteRepositorio.GetAll().Where(t => t.TipoCategoria.Equals(BowConsts.CATEGORIA_VEHICULO)).ToList().OrderBy(p => p.Nombre);
            return new GetAllTiposReporteOutput { TiposReporte = Mapper.Map<List<TipoReporteOutput>>(listaTiposVehiculo) };
        }

        public GetAllCategoriasOutput GetAllCategorias()
        {
            var listaCategorias = _tipoReporteRepositorio.GetAll().Where(t => t.TipoCategoria.Equals(BowConsts.CATEGORIA_HISTORIA)).ToList().OrderBy(p => p.Nombre);
            return new GetAllCategoriasOutput { TiposReporte = Mapper.Map<List<TipoReporteOutput>>(listaCategorias) };
        }

        public GetAllTiposReporteOutput GetAllTipos()
        {
            var listaTiposReporte = _tipoReporteRepositorio.GetAll().ToList().OrderBy(p => p.TipoCategoria);
            return new GetAllTiposReporteOutput { TiposReporte = Mapper.Map<List<TipoReporteOutput>>(listaTiposReporte) };
        }

        public GetTipoReporteOutput GetTipoReporte(GetTipoReporteInput categoriaInput)
        {
            return Mapper.Map<GetTipoReporteOutput>(_tipoReporteRepositorio.Get(categoriaInput.Id));
        }

        public void SaveTipo(SaveTipoInput nuevoTipo)
        {
            TipoReporte existeTipo = _tipoReporteRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoTipo.Nombre.ToLower() && p.TipoCategoria.ToLower() == nuevoTipo.TipoCategoria.ToLower());

            if (existeTipo == null)
            {
                TipoReporte tipo = Mapper.Map<TipoReporte>(nuevoTipo);
                tipo.TenantId = BowConsts.TENANT_ID_ACR;
                _tipoReporteRepositorio.Insert(tipo);
            }
            else
            {
                var mensajeError = "Ya existe el tipo o categoría.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateTipo(UpdateTipoInput tipoUpdate)
        {
            TipoReporte existeTipo = _tipoReporteRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == tipoUpdate.Nombre.ToLower() && p.TipoCategoria.ToLower() == tipoUpdate.TipoCategoria.ToLower() && p.Id != tipoUpdate.Id);

            if (existeTipo == null)
            {
                TipoReporte tipo = _tipoReporteRepositorio.Get(tipoUpdate.Id);
                Mapper.Map(tipoUpdate, tipo);
                _tipoReporteRepositorio.Update(tipo);
            }
            else
            {
                var mensajeError = "Ya existe el tipo o categoría.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteTipo(DeleteTipoInput tipoEliminar)
        {
            _tipoReporteRepositorio.Delete(tipoEliminar.Id);
        }

        /*********************************************************************************************
        ************************************  Reporte de Incidentes  *********************************
        *********************************************************************************************/

        public GetReporteIncidentesOutput GetReporteIncidentes(GetReporteIncidentesInput reporteInput)
        {
            return Mapper.Map<GetReporteIncidentesOutput>(_reporteIncidentesRepositorio.GetWithTipo(reporteInput.Id));
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

        public void UpdateStateReporteIncidentes(UpdateStateReporteIncidentesInput reporteUpdate)
        {
            ReporteIncidentes reporte = _reporteIncidentesRepositorio.Get(reporteUpdate.Id);
            reporte.EsActivo = false;
            _reporteIncidentesRepositorio.Update(reporte);
        }

        /*********************************************************************************************
        *********************************  Calificación de Conductores  ******************************
        *********************************************************************************************/

        public GetReporteCalificacionesOutput GetReporteCalificaciones(GetReporteCalificacionesInput reporteInput)
        {
            return Mapper.Map<GetReporteCalificacionesOutput>(_reporteCalificacionesRepositorio.GetWithTipo(reporteInput.Id));
        }

        public GetAllReportesCalificacionesOutput GetAllReporteCalificaciones()
        {
            var listaReporteCalificaciones = _reporteCalificacionesRepositorio.GetAllReporteCalificacionesWithTipo();
            return new GetAllReportesCalificacionesOutput { ReportesCalificaciones = Mapper.Map<List<ReporteCalificacionesOutput>>(listaReporteCalificaciones) };
        }

        public void SaveReporteCalificacion(SaveReporteCalificacionInput nuevaCalificacion)
        {
            ReporteCalificaciones reporte = Mapper.Map<ReporteCalificaciones>(nuevaCalificacion);
            reporte.EsActiva = true;
            reporte.TenantId = BowConsts.TENANT_ID_ACR;
            _reporteCalificacionesRepositorio.Insert(reporte);
        }

        public void UpdateStateCalificacionIncidentes(UpdateStateCalificacionIncidentesInput reporteUpdate)
        {
            ReporteCalificaciones reporte = _reporteCalificacionesRepositorio.Get(reporteUpdate.Id);
            reporte.EsActiva = false;
            _reporteCalificacionesRepositorio.Update(reporte);
        }

        /*********************************************************************************************
        ******************************************  Deslizador  ***************************************
        *********************************************************************************************/

        public GetAllDeslizadorOutput GetAllDeslizador()
        {
            var listaDeslizador = _deslizadorRepositorio.GetAllList().OrderBy(p => p.Nombre);
            return new GetAllDeslizadorOutput { Deslizador = Mapper.Map<List<DeslizadorOutput>>(listaDeslizador) };
        }

        public GetDeslizadorOutput GetDeslizador(GetDeslizadorInput deslizadorInput)
        {
            return Mapper.Map<GetDeslizadorOutput>(_deslizadorRepositorio.Get(deslizadorInput.Id));
        }

        public void SaveDeslizador(SaveDeslizadorInput nuevaDeslizador)
        {
            Deslizador existeDeslizador = _deslizadorRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevaDeslizador.Nombre.ToLower());

            if (existeDeslizador == null)
            {
                Deslizador deslizador = Mapper.Map<Deslizador>(nuevaDeslizador);

                deslizador.TenantId = BowConsts.TENANT_ID_ACR;

                _deslizadorRepositorio.Insert(deslizador);
            }
            else
            {
                var mensajeError = "Ya existe la imagen en el slider.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateDeslizador(UpdateDeslizadorInput deslizadorUpdate)
        {
            Deslizador existeDeslizador = _deslizadorRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == deslizadorUpdate.Nombre.ToLower() && p.Id != deslizadorUpdate.Id);

            if (existeDeslizador == null)
            {
                Deslizador desizador = _deslizadorRepositorio.Get(deslizadorUpdate.Id);

                //  Eliminamos la imagen anterior
                string rutaCompletaArchivo = System.AppDomain.CurrentDomain.BaseDirectory + desizador.UrlImagen;
                File.Delete(rutaCompletaArchivo); 

                Mapper.Map(deslizadorUpdate, desizador);
                _deslizadorRepositorio.Update(desizador);
            }
            else
            {
                var mensajeError = "Ya existe la imagen en el slider.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteDeslizador(DeleteDeslizadorInput deslizadorEliminar)
        {
            Deslizador desizador = _deslizadorRepositorio.Get(deslizadorEliminar.Id);

            //  Eliminamos la imagen
            string rutaCompletaArchivo = System.AppDomain.CurrentDomain.BaseDirectory + desizador.UrlImagen;
            File.Delete(rutaCompletaArchivo); 

            _deslizadorRepositorio.Delete(deslizadorEliminar.Id);
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
                noticia.Url = "www.google.com";
                noticia.Fecha = DateTime.Now;
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
            var listaHistorias = _historiaVialRepositorio.GetAllHistoriasWithTipo();
            return new GetAllHistoriasVialesOutput { HistoriasViales = Mapper.Map<List<HistoriaVialOutput>>(listaHistorias) };
        }

        public GetAllHistoriasVialesActivasOutput GetAllHistoriasVialesActivas()
        {
            var listaHistorias = _historiaVialRepositorio.GetAllHistoriasActivasWithTipo();
            return new GetAllHistoriasVialesActivasOutput { HistoriasViales = Mapper.Map<List<HistoriaVialOutput>>(listaHistorias) };
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
                historia.EsActiva = false;
                historia.FechaPublicacion = DateTime.Now;
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

        ///*********************************************************************************************
        // *************************************  Diagnostico Vial  ************************************
        // *********************************************************************************************/

        public GetAllItemsByDiagnosticoVialOutput GetAllItemsDiagnosticoVial()
        {
            var listaItemsDiagnostico = _itemDiagnosticoRepositorio.GetAllList().OrderBy(p => p.Id);
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
