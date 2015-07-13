using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetHistoriaVialOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombrePersona { get; set; }
        public int EdadPersona { get; set; }
        public bool EsActiva { get; set; }
    }
}
