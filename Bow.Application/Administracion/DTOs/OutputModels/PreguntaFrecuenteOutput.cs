﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class PreguntaFrecuenteOutput : EntityDto, IOutputDto
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public bool EsActiva { get; set; }
    }
}
