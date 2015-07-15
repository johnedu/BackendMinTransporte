using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class SaveTipoInput : IInputDto
    {
        [Required]
        [MaxLength(512)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(512)]
        public string TipoCategoria { get; set; }

        [Required]
        [MaxLength(2048)]
        public string UrlImagen { get; set; }
    }
}
