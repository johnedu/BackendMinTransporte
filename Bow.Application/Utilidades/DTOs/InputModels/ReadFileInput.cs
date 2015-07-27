using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class ReadFileInput : IInputDto
    {
        public string Ruta { get; set; }
        public string RealFileName { get; set; }
    }
}
