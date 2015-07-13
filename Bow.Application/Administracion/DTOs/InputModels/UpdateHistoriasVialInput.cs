﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class UpdateHistoriasVialInput : EntityDto, IInputDto
    {
        [Required]
        [MaxLength(512)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(2048)]
        public string Descripcion { get; set; }
        [Required]
        [MaxLength(512)]
        public string NombrePersona { get; set; }
        [Required]
        public int EdadPersona { get; set; }
        public bool EstadoActiva { get; set; }
    }
}