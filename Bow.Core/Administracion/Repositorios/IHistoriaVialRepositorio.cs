﻿using Abp.Domain.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Repositorios
{
    public interface IHistoriaVialRepositorio : IRepository<HistoriaVial>
    {
        List<HistoriaVial> GetAllHistoriasWithTipo();
        List<HistoriaVial> GetAllHistoriasActivasWithTipo();
    }
}
