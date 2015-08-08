using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class GetAllRiesgosCercanosInput : IInputDto
    {
        [Required]
        public string LatitudMinima { get; set; }
        [Required]
        public string LatitudMaxima { get; set; }
        [Required]
        public string LongitudMinima { get; set; }
        [Required]
        public string LongitudMaxima { get; set; }
    }
}
