using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class PasoByHistoriaVialOutput : EntityDto, IOutputDto
    {
        public int HistoriaVialId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string URLImagen { get; set; }
    }
}
