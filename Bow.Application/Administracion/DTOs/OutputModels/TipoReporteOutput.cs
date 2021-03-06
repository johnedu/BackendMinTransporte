﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class TipoReporteOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string TipoCategoria { get; set; }
        public string UrlImagen { get; set; }
    }
}
