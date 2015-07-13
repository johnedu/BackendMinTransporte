using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetDiagnosticoVialOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public bool EsActivo { get; set; }
    }
}
