﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class NoticiasOutput : EntityDto, IOutputDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Url { get; set; }
        public string UrlImagen { get; set; }
        public bool EsActiva { get; set; }
    }
}
