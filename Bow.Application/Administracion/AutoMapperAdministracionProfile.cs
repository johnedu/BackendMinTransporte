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

            CreateMap<TipoReporte, TipoReporteOutput>();
            CreateMap<ReporteIncidentes, ReporteIncidenteOutput>()
                .ForMember(dest => dest.TipoReporteIncidente, opt => opt.MapFrom(src => src.TipoReporteIncidente.Nombre));
            CreateMap<SaveReporteIncidentesInput, ReporteIncidentes>();

            //  Noticias
            CreateMap<SaveNoticiasInput, Noticias>();
            CreateMap<UpdateNoticiasInput, Noticias>();
            CreateMap<Noticias, GetNoticiasInput>();
            CreateMap<Noticias, NoticiasOutput>();

            //  Historia Vial
            CreateMap<SaveHistoriasVialInput, HistoriaVial>();
            CreateMap<UpdateHistoriasVialInput, HistoriaVial>();
            CreateMap<HistoriaVial, GetHistoriaVialInput>();
            CreateMap<HistoriaVial, HistoriaVialOutput>();

            CreateMap<SavePasoHistoriaVialInput, PasoHistoriaVial>();
            CreateMap<UpdatePasoHistoriaVialInput, PasoHistoriaVial>();
            CreateMap<PasoHistoriaVial, GetPasoByHistoriaVialInput>();
            CreateMap<PasoHistoriaVial, PasoByHistoriaVialOutput>();

            //  Diagnostico Vial
            CreateMap<SaveDiagnosticoVialInput, DiagnosticoVial>();
            CreateMap<UpdateDiagnosticoVialInput, DiagnosticoVial>();
            CreateMap<DiagnosticoVial, GetDiagnosticoVialInput>();
            CreateMap<DiagnosticoVial, DiagnosticoVialOutput>();

            CreateMap<SaveItemDiagnosticoVialInput, ItemDiagnostico>();
            CreateMap<UpdateItemDiagnosticoVialInput, ItemDiagnostico>();
            CreateMap<ItemDiagnostico, GetItemByDiagnosticoVialInput>();
            CreateMap<ItemDiagnostico, ItemByDiagnosticoVialOutput>();
        }
    }
}
