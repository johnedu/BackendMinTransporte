﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class UpdatePasoHistoriaVialInput : EntityDto, IInputDto
    {
        [Required]
        public int HistoriaVialId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(2048)]
        public string Descripcion { get; set; }
        [MaxLength(512)]
        public int URLImagen { get; set; }
    }
}