﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class UpdateNoticiasInput : EntityDto, IInputDto
    {
        [Required]
        [MaxLength(512)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(512)]
        public string URLImagen { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
        public bool EsActiva { get; set; }

    }
}