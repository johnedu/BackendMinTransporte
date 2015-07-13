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
        private INoticiasRepositorio _noticiasRepositorio;
        private IItemDiagnosticoRepositorio _itemDiagnosticoRepositorio;
        private IDiagnosticoVialRepositorio _diagnosticoVialRepositorio;
        private IPasoHistoriaVialRepositorio _pasoHistoriaVialRepositorio;
        private IHistorialVialRepositorio _historialVialRepositorio;

        public IAbpSession AbpSession { get; set; }

        #endregion

        //Inyección de Dependencia en el Servicio
        public AdministracionService(
            IPreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio,
            IReporteIncidentesRepositorio reporteIncidentesRepositorio,
            ITipoReporteRepositorio tipoReporteRepositorio, 
            INoticiasRepositorio noticiasRepositorio,
            IItemDiagnosticoRepositorio itemDiagnosticoRepositorio,
            IDiagnosticoVialRepositorio diagnosticoVialRepositorio,
            IPasoHistoriaVialRepositorio pasoHistoriaVialRepositorio,
            IHistorialVialRepositorio historialVialRepositorio)
        {
            _preguntaFrecuenteRepositorio = preguntaFrecuenteRepositorio;
            _reporteIncidentesRepositorio = reporteIncidentesRepositorio;
            _tipoReporteRepositorio = tipoReporteRepositorio;
            _noticiasRepositorio = noticiasRepositorio;
            _itemDiagnosticoRepositorio = itemDiagnosticoRepositorio;
            _diagnosticoVialRepositorio = diagnosticoVialRepositorio;
            _pasoHistoriaVialRepositorio = pasoHistoriaVialRepositorio;
            _historialVialRepositorio = historialVialRepositorio;
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
         ******************************************  Noticias  ***************************************
         *********************************************************************************************/

        public GetAllNoticiasOutput GetAllNoticias()
        {
            var listaNoticias = _noticiasRepositorio.GetAllList().OrderBy(p => p.Fecha);
            return new GetAllNoticiasOutput { Noticias = Mapper.Map<List<NoticiasOutput>>(listaNoticias) };
        }

        /*********************************************************************************************
         ***************************************  Historia Vial  *************************************
         *********************************************************************************************/

        public GetAllHistoriasVialesOutput GetAllHistoriasViales()
        {
            var listaHistorias = _noticiasRepositorio.GetAllList().OrderBy(p => p.Fecha);
            return new GetAllHistoriasVialesOutput { HistoriasViales = Mapper.Map<List<HistoriaVialOutput>>(listaHistorias) };
        }

        public GetHistoriaVialOutput GetHistoriaVial(GetHistoriaVialInput historiaInput)
        {
            return Mapper.Map<GetHistoriaVialOutput>(_historialVialRepositorio.Get(historiaInput.Id));
        }

        public void SaveHistoriasVial(SaveHistoriasVialInput nuevaHistoria)
        {
            HistoriaVial existeHistoria = _historialVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevaHistoria.Nombre.ToLower());

            if (existeHistoria == null)
            {
                HistoriaVial historia = Mapper.Map<HistoriaVial>(nuevaHistoria);
                historia.EsActiva = true;
                historia.TenantId = BowConsts.TENANT_ID_ACR;
                _historialVialRepositorio.Insert(historia);
            }
            else
            {
                var mensajeError = "Ya existe la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateHistoriasVial(UpdateHistoriasVialInput historiaUpdate)
        {
            HistoriaVial existeHistoria = _historialVialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == historiaUpdate.Nombre.ToLower() && p.Id != historiaUpdate.Id);

            if (existeHistoria == null)
            {
                HistoriaVial historia = _historialVialRepositorio.Get(historiaUpdate.Id);
                Mapper.Map(historiaUpdate, historia);
                _historialVialRepositorio.Update(historia);
            }
            else
            {
                var mensajeError = "Ya existe la historial vial.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteHistoriasVial(DeleteHistoriasVialInput historiaEliminar)
        {
            _historialVialRepositorio.Delete(historiaEliminar.Id);
        }

        ///*********************************************************************************************
        // *****************************************  Mensajes  ****************************************
        // *********************************************************************************************/

        //public void EnviarMensaje(EnviarMensajeInput mensaje)
        //{
        //    Mensaje mensajeEditado = Mapper.Map<Mensaje>(mensaje);
        //    mensajeEditado.UsuarioEmisorId = _usuarioRepositorio.GetAll().Where(u => u.Coda == mensaje.CodaEmisor).FirstOrDefault().Id;
        //    //  Validamos si el receptor aún no ha iniciado sesión en la App
        //    ReporteIncidentes usuarioReceptor = _usuarioRepositorio.GetAll().Where(u => u.Coda == mensaje.CodaReceptor).FirstOrDefault();
        //    TipoReporte tipoTemporal = _tipoRepositorio.GetAll().FirstOrDefault();
        //    int usuarioId = 0;
        //    if (usuarioReceptor == null)
        //    {
        //        usuarioReceptor = new ReporteIncidentes { Coda = mensaje.CodaReceptor, Nombre = BowConsts.USUARIO_TEMPORAL_ACR, TipoId = tipoTemporal.Id, TenantId = BowConsts.TENANT_ID_ACR };
        //        usuarioId = _usuarioRepositorio.InsertAndGetId(usuarioReceptor);
        //    }
        //    else
        //    {
        //        usuarioId = usuarioReceptor.Id;
        //    }
        //    mensajeEditado.UsuarioReceptorId = usuarioId;
        //    mensajeEditado.TenantId = BowConsts.TENANT_ID_ACR;
        //    _mensajeRepositorio.Insert(mensajeEditado);
        //}

        //public GetMensajeOutput GetMensaje(GetMensajeInput mensaje)
        //{
        //    Mensaje mensajeRespuesta = _mensajeRepositorio.GetMensajeByIdWithReceptorAndEmisor(mensaje.Id);
        //    if (mensajeRespuesta.UsuarioReceptor.Coda == mensaje.Receptor)
        //    {
        //        mensajeRespuesta.FueLeido = true;
        //        _mensajeRepositorio.Update(mensajeRespuesta);
        //    }
        //    return Mapper.Map<GetMensajeOutput>(mensajeRespuesta);
        //}

        //public GetAllMensajesByEmisorOutput GetAllMensajesByEmisor(GetAllMensajesByEmisorInput mensajeEmisor)
        //{
        //    return new GetAllMensajesByEmisorOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(_mensajeRepositorio.GetAllMensajesByEmisor(mensajeEmisor.Emisor)) };
        //}

        //public GetAllMensajesByReceptorOutput GetAllMensajesByReceptor(GetAllMensajesByReceptorInput mensajeEmisor)
        //{
        //    return new GetAllMensajesByReceptorOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(_mensajeRepositorio.GetAllMensajesByReceptor(mensajeEmisor.Receptor)) };
        //}

        //public void DeleteMensaje(DeleteMensajeInput mensajeEliminar)
        //{
        //    _mensajeRepositorio.Delete(mensajeEliminar.Id);
        //}

        //public GetMensajesSinLeerByUsuarioOutput GetMensajesSinLeerByUsuario(GetMensajesSinLeerByUsuarioInput usuario)
        //{
        //    var listaMensajes = _mensajeRepositorio.GetAll().Where(m => m.UsuarioReceptor.Coda == usuario.Usuario && m.FueLeido == false).OrderBy(m => m.Id);
        //    return new GetMensajesSinLeerByUsuarioOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(listaMensajes) };
        //}

        ///*********************************************************************************************
        // ******************************************  Usuario  ****************************************
        // *********************************************************************************************/

        //public GetUsuarioByCODAOutput GetUsuarioByCODA(GetUsuarioByCODAInput usuario)
        //{
        //    return Mapper.Map<GetUsuarioByCODAOutput>(_usuarioRepositorio.GetAll().Where(u => u.Coda == usuario.Coda).FirstOrDefault());
        //}

        //public GetPuntajeUsuarioOutput GetPuntajeUsuario(GetPuntajeUsuarioInput usuario)
        //{
        //    return new GetPuntajeUsuarioOutput { PuntajeTotal = _puntajeRepositorio.GetAll().Where(p => p.UsuarioPuntaje.Coda == usuario.Usuario).Sum(p => p.PuntajeValor) };
        //}

        //public void SaveUsuario(SaveUsuarioInput usuario)
        //{
        //    ReporteIncidentes usuarioRegistrado = _usuarioRepositorio.GetAll().Where(u => u.Coda == usuario.Coda).FirstOrDefault();
        //    if (usuarioRegistrado == null)
        //    {
        //        if (usuario.Tipo.ToLower().Equals(BowConsts.TIPO_USUARIO_PPR.ToLower()))
        //        {
        //            usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PPR).FirstOrDefault().Id;
        //        }
        //        else if (usuario.Tipo.ToLower().Equals(BowConsts.TIPO_USUARIO_PROFESIONAL.ToLower()))
        //        {
        //            usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault().Id;
        //        }
        //        else
        //        {
        //            usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault().Id;
        //        }
        //        ReporteIncidentes usuarioEditado = Mapper.Map<ReporteIncidentes>(usuario);
        //        usuarioEditado.TenantId = BowConsts.TENANT_ID_ACR;
        //        _usuarioRepositorio.Insert(usuarioEditado);
        //    }
        //    else
        //    {
        //        if (usuarioRegistrado.Nombre == BowConsts.USUARIO_TEMPORAL_ACR)
        //        {
        //            if (usuario.Tipo.ToLower().Equals(BowConsts.TIPO_USUARIO_PPR.ToLower()))
        //            {
        //                usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PPR).FirstOrDefault().Id;
        //            }
        //            else if (usuario.Tipo.ToLower().Equals(BowConsts.TIPO_USUARIO_PROFESIONAL.ToLower()))
        //            {
        //                usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault().Id;
        //            }
        //            else
        //            {
        //                usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault().Id;
        //            }
        //            usuarioRegistrado.Nombre = usuario.Nombre;
        //            _usuarioRepositorio.Update(usuarioRegistrado);
        //        }
        //    }
        //}

        //public GetHistorialPuntajesUsuarioOutput GetHistorialPuntajesUsuario(GetHistorialPuntajesUsuarioInput usuario)
        //{
        //    return new GetHistorialPuntajesUsuarioOutput { Puntajes = Mapper.Map<List<PuntajeUsuarioOutput>>(_puntajeRepositorio.GetAllHistorialPuntajesByUsuario(usuario.Usuario)),
        //                                                   PuntajeTotal = _puntajeRepositorio.GetAll().Where(p => p.UsuarioPuntaje.Coda == usuario.Usuario).Sum(p => p.PuntajeValor)
        //    };
        //}

        ///*********************************************************************************************
        // ****************************************  Preguntas  ****************************************
        // *********************************************************************************************/

        //public GetPreguntaOutput GetPregunta(GetPreguntaInput preguntaInput)
        //{
        //    return Mapper.Map<GetPreguntaOutput>(_preguntaRepositorio.Get(preguntaInput.Id));
        //}

        //public GetAllPreguntasByDimensionOutput GetAllPreguntasByDimension(GetAllPreguntasByDimensionInput dimension)
        //{
        //    var listaPreguntas = _preguntaRepositorio.GetAllWithJuego(dimension.DimensionId, dimension.JuegoId);
        //    return new GetAllPreguntasByDimensionOutput { Preguntas = Mapper.Map<List<PreguntasByDimensionOutput>>(listaPreguntas) };
        //}

        //public void SavePregunta(SavePreguntaInput nuevaPregunta)
        //{
        //    Pregunta existePregunta = _preguntaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == nuevaPregunta.Texto.ToLower() && p.DimensionId == nuevaPregunta.DimensionId && p.JuegoId == nuevaPregunta.JuegoId);

        //    if (existePregunta == null)
        //    {
        //        Pregunta pregunta = Mapper.Map<Pregunta>(nuevaPregunta);
        //        pregunta.FechaCreacion = DateTime.Now.ToString();
        //        pregunta.UsuarioIdCreacion = AbpSession.GetUserId();
        //        pregunta.TenantId = BowConsts.TENANT_ID_ACR;
        //        _preguntaRepositorio.Insert(pregunta);
        //    }
        //    else
        //    {
        //        var mensajeError = "Ya existe la pregunta";
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

        //public void DeletePregunta(DeletePreguntaInput preguntaEliminar)
        //{
        //    _respuestaRepositorio.Delete(r => r.PreguntaId == preguntaEliminar.Id);
        //    _preguntaRepositorio.Delete(preguntaEliminar.Id);
        //}

        //public PuedeEliminarPreguntaOutput PuedeEliminarPreguntaOutput(PuedeEliminarPreguntaOutputInput preguntaEliminar)
        //{
        //    var listaPuntajes = _puntajeRepositorio.GetAll().Where(op => op.PreguntaId == preguntaEliminar.Id);
        //    PuedeEliminarPreguntaOutput puede = new PuedeEliminarPreguntaOutput();

        //    if (listaPuntajes.Count() == 0)
        //    {
        //        puede.PuedeEliminar = true;
        //    }
        //    else
        //    {
        //        puede.PuedeEliminar = false;
        //    }
        //    return puede;
        //}

        //public void UpdatePregunta(UpdatePreguntaInput preguntaUpdate)
        //{
        //    Pregunta existePregunta = _preguntaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == preguntaUpdate.Texto.ToLower() && p.DimensionId == preguntaUpdate.DimensionId && p.JuegoId == preguntaUpdate.JuegoId && p.Id != preguntaUpdate.Id);

        //    if (existePregunta == null)
        //    {
        //        Pregunta pregunta = _preguntaRepositorio.Get(preguntaUpdate.Id);
        //        Mapper.Map(preguntaUpdate, pregunta);
        //        pregunta.FechaModificacion = DateTime.Now.ToString();
        //        pregunta.UsuarioIdModificacion = AbpSession.GetUserId();
        //        _preguntaRepositorio.Update(Mapper.Map<Pregunta>(pregunta));
        //    }
        //    else
        //    {
        //        var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

        //public GetPreguntaAleatoriaByDimensionAndJuegoOutput GetPreguntaAleatoriaByDimensionAndJuego(GetPreguntaAleatoriaByDimensionAndJuegoInput dimensionAndJuego)
        //{
        //    List<Pregunta> preguntasYaResueltas = _puntajeRepositorio.GetAllPreguntasByUsuario(dimensionAndJuego.CodaUsuario);
        //    List<Pregunta> preguntas = _preguntaRepositorio.GetAll().Where(p => p.DimensionId == dimensionAndJuego.DimensionId && p.JuegoId == dimensionAndJuego.JuegoId).ToList();
        //    if (preguntas.Count() > 0)
        //    {
        //        preguntas = preguntas.Except(preguntasYaResueltas).ToList();
        //        if (preguntas.Count() > 0)
        //        {
        //            var rand = new Random();
        //            GetPreguntaAleatoriaByDimensionAndJuegoOutput preguntaSeleccionada = Mapper.Map<GetPreguntaAleatoriaByDimensionAndJuegoOutput>(preguntas[rand.Next(preguntas.Count())]);
        //            preguntaSeleccionada.Respuestas = GetAllRespuestasByPregunta(new GetAllRespuestasByPreguntaInput { PreguntaId = preguntaSeleccionada.Id }).Respuestas;
        //            return preguntaSeleccionada;
        //        }
        //    }
        //    return new GetPreguntaAleatoriaByDimensionAndJuegoOutput();
        //}

        ///*********************************************************************************************
        // ****************************************  Respuestas  ***************************************
        // *********************************************************************************************/

        //public GetRespuestaOutput GetRespuesta(GetRespuestaInput respuestaInput)
        //{
        //    return Mapper.Map<GetRespuestaOutput>(_respuestaRepositorio.Get(respuestaInput.Id));
        //}

        //public GetAllRespuestasByPreguntaOutput GetAllRespuestasByPregunta(GetAllRespuestasByPreguntaInput pregunta)
        //{
        //    var listaRespuestas = _respuestaRepositorio.GetAllList().Where(p => p.PreguntaId == pregunta.PreguntaId).OrderBy(p => p.Texto);
        //    return new GetAllRespuestasByPreguntaOutput { Respuestas = Mapper.Map<List<RespuestasByPreguntaOutput>>(listaRespuestas), Comodines5050 = listaRespuestas.Count(m => m.Comodin50_50 == true) };
        //}

        //public void SaveRespuesta(SaveRespuestaInput nuevaRespuesta)
        //{
        //    Respuesta existeRespuesta = _respuestaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == nuevaRespuesta.Texto.ToLower() && p.PreguntaId == nuevaRespuesta.PreguntaId);

        //    if (existeRespuesta == null)
        //    {
        //        if (nuevaRespuesta.RespuestaVerdadera)
        //        {
        //            Respuesta respuestaCorrectaActual = _respuestaRepositorio.FirstOrDefault(r => r.PreguntaId == nuevaRespuesta.PreguntaId && r.RespuestaVerdadera);
        //            if (respuestaCorrectaActual != null)
        //            {
        //                respuestaCorrectaActual.RespuestaVerdadera = false;
        //                _respuestaRepositorio.Update(respuestaCorrectaActual);
        //            }
        //        }
        //        Respuesta respuesta = Mapper.Map<Respuesta>(nuevaRespuesta);
        //        respuesta.FechaCreacion = DateTime.Now.ToString();
        //        respuesta.UsuarioIdCreacion = AbpSession.GetUserId();
        //        respuesta.TenantId = BowConsts.TENANT_ID_ACR;
        //        _respuestaRepositorio.Insert(respuesta);
        //    }
        //    else
        //    {
        //        var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

        //public void DeleteRespuesta(DeleteRespuestaInput respuestaEliminar)
        //{

        //    _respuestaRepositorio.Delete(respuestaEliminar.Id);
        //}

        //public void UpdateRespuesta(UpdateRespuestaInput respuestaUpdate)
        //{
        //    Respuesta existeRespuesta = _respuestaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == respuestaUpdate.Texto.ToLower() && p.PreguntaId == respuestaUpdate.PreguntaId && p.Id != respuestaUpdate.Id);

        //    if (existeRespuesta == null)
        //    {
        //        if (respuestaUpdate.RespuestaVerdadera)
        //        {
        //            Respuesta respuestaCorrectaActual = _respuestaRepositorio.FirstOrDefault(r => r.PreguntaId == respuestaUpdate.PreguntaId && r.RespuestaVerdadera && r.Id != respuestaUpdate.Id);
        //            if (respuestaCorrectaActual != null)
        //            {
        //                respuestaCorrectaActual.RespuestaVerdadera = false;
        //                _respuestaRepositorio.Update(respuestaCorrectaActual);
        //            }
        //        }
        //        if (respuestaUpdate.Comodin50_50)
        //        {
        //            int conteo = _respuestaRepositorio.GetAll().Where(r => r.PreguntaId == respuestaUpdate.PreguntaId && r.Comodin50_50 && r.Id != respuestaUpdate.Id).Count();
        //            if (conteo == 2)
        //            {
        //                respuestaUpdate.Comodin50_50 = false;
        //            }
        //        }
        //        Respuesta respuesta = _respuestaRepositorio.Get(respuestaUpdate.Id);
        //        Mapper.Map(respuestaUpdate, respuesta);
        //        respuesta.FechaModificacion = DateTime.Now.ToString();
        //        respuesta.UsuarioIdModificacion = AbpSession.GetUserId();
        //        _respuestaRepositorio.Update(respuesta);
        //    }
        //    else
        //    {
        //        var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

        ///*********************************************************************************************
        // ***************************************  Tipos Usuario  *************************************
        // *********************************************************************************************/

        //public GetTipoPPROutput GetTipoPPR()
        //{
        //    return Mapper.Map<GetTipoPPROutput>(_tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PPR).FirstOrDefault());
        //}

        //public GetTipoProfesionalReintegradorOutput GetTipoProfesionalReintegrador()
        //{
        //    return Mapper.Map<GetTipoProfesionalReintegradorOutput>(_tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault());
        //}

        ///*********************************************************************************************
        // *************************************  Puntajes Usuario  ************************************
        // *********************************************************************************************/

        //public void SavePuntaje(SavePuntajeInput puntaje)
        //{
        //    puntaje.UsuarioId = _usuarioRepositorio.GetAll().Where(u => u.Coda == puntaje.Usuario).FirstOrDefault().Id;
        //    Puntaje puntajeEditado = Mapper.Map<Puntaje>(puntaje);
        //    puntajeEditado.TenantId = 1;
        //    var p = _puntajeRepositorio.Insert(puntajeEditado);
        //}

        //public GetAllJuegosOutput GetAllJuegos()
        //{
        //    List<Juego> listaJuegos = _juegoRepositorio.GetAll().OrderBy(p => p.Nombre).ToList();
        //    return new GetAllJuegosOutput { Juegos = Mapper.Map<List<JuegoOutput>>(listaJuegos) };
        //}

        //public GetAllDimensionesOutput GetAllDimensiones()
        //{
        //    List<Dimension> listaDimensiones = _dimensionRepositorio.GetAll().OrderBy(p => p.Nombre).ToList();
        //    return new GetAllDimensionesOutput { Dimensiones = Mapper.Map<List<DimensionOutput>>(listaDimensiones) };
        //}

        ///*********************************************************************************************
        // ****************************************  Entidades  ****************************************
        // *********************************************************************************************/

        //public GetEntidadOutput GetEntidad(GetEntidadInput entidadInput)
        //{
        //    return Mapper.Map<GetEntidadOutput>(_entidadRepositorio.Get(entidadInput.Id));
        //}

        //public GetAllEntidadesByDimensionOutput GetAllEntidadesByDimension(GetAllEntidadesByDimensionInput dimension)
        //{
        //    var listaEntidades = _entidadRepositorio.GetAll().Where(e => e.DimensionId == dimension.DimensionId).ToList();
        //    return new GetAllEntidadesByDimensionOutput { Entidades = Mapper.Map<List<EntidadByDimensionOutput>>(listaEntidades) };
        //}

        //public void SaveEntidad(SaveEntidadInput nuevaEntidad)
        //{
        //    Entidad existeEntidad = _entidadRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevaEntidad.Nombre.ToLower() && p.DimensionId == nuevaEntidad.DimensionId);

        //    if (existeEntidad == null)
        //    {
        //        Entidad entidad = Mapper.Map<Entidad>(nuevaEntidad);
        //        entidad.FechaCreacion = DateTime.Now.ToString();
        //        entidad.UsuarioIdCreacion = AbpSession.GetUserId();
        //        entidad.TenantId = BowConsts.TENANT_ID_ACR;
        //        _entidadRepositorio.Insert(entidad);
        //    }
        //    else
        //    {
        //        var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

        //public void DeleteEntidad(DeleteEntidadInput entidadEliminar)
        //{

        //    _entidadRepositorio.Delete(entidadEliminar.Id);
        //}

        //public void UpdateEntidad(SaveEntidadInput entidadUpdate)
        //{
        //    Entidad existeEntidad = _entidadRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == entidadUpdate.Nombre.ToLower() && p.DimensionId == entidadUpdate.DimensionId && p.Id != entidadUpdate.Id);

        //    if (existeEntidad == null)
        //    {
        //        Entidad entidad = _entidadRepositorio.Get(entidadUpdate.Id);
        //        Mapper.Map(entidadUpdate, entidad);
        //        entidad.FechaModificacion = DateTime.Now.ToString();
        //        entidad.UsuarioIdModificacion = AbpSession.GetUserId();
        //        _entidadRepositorio.Update(entidad);
        //    }
        //    else
        //    {
        //        var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
        //        throw new UserFriendlyException(mensajeError);
        //    }
        //}

    }
}
