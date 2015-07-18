﻿using Bow.Utilidades.AutoMapper;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    public class AutoMapperAdministracionProfile : AutoMapperBaseProfile
    {
        public AutoMapperAdministracionProfile()
            : base("AutoMapperAdministracionProfile")
        {
        }

        protected override void CrearMappings()
        {
            //  Preguntas Frecuentes
            CreateMap<SavePreguntaFrecuenteInput, PreguntaFrecuente>();
            CreateMap<UpdatePreguntaFrecuenteInput, PreguntaFrecuente>();
            CreateMap<PreguntaFrecuente, GetPreguntaFrecuenteOutput>();
            CreateMap<PreguntaFrecuente, PreguntaFrecuenteOutput>();

            //  Reporte Incidentes

            CreateMap<SaveTipoInput, TipoReporte>();
            CreateMap<UpdateTipoInput, TipoReporte>();
            CreateMap<TipoReporte, GetTipoReporteOutput>();
            CreateMap<TipoReporte, TipoReporteOutput>();

            CreateMap<ReporteIncidentes, ReporteIncidenteOutput>()
                .ForMember(dest => dest.TipoReporteIncidente, opt => opt.MapFrom(src => src.TipoReporteIncidente.Nombre))
                .ForMember(dest => dest.TipoReporteImagen, opt => opt.MapFrom(src => src.TipoReporteIncidente.UrlImagen));
            CreateMap<SaveReporteIncidentesInput, ReporteIncidentes>();
            CreateMap<SaveReporteCalificacionInput, ReporteCalificaciones>();

            //  Noticias
            CreateMap<SaveNoticiasInput, Noticias>();
            CreateMap<UpdateNoticiasInput, Noticias>();
            CreateMap<Noticias, GetNoticiasOutput>();
            CreateMap<Noticias, NoticiasOutput>();

            CreateMap<Deslizador, DeslizadorOutput>();

            //  Historia Vial
            CreateMap<SaveHistoriasVialInput, HistoriaVial>();
            CreateMap<UpdateHistoriasVialInput, HistoriaVial>();
            CreateMap<HistoriaVial, GetHistoriaVialOutput>();
            CreateMap<HistoriaVial, HistoriaVialOutput>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.CategoriaHistoria.Nombre))
                .ForMember(dest => dest.CategoriaImage, opt => opt.MapFrom(src => src.CategoriaHistoria.UrlImagen));

            //  Diagnostico Vial
            CreateMap<SaveItemDiagnosticoVialInput, ItemDiagnostico>();
            CreateMap<UpdateItemDiagnosticoVialInput, ItemDiagnostico>();
            CreateMap<ItemDiagnostico, GetItemByDiagnosticoVialOutput>();
            CreateMap<ItemDiagnostico, ItemByDiagnosticoVialOutput>();

        }
    }
}
