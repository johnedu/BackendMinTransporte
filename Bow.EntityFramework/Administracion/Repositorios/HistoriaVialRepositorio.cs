﻿using Abp.EntityFramework;
using Bow.EntityFramework;
using Bow.EntityFramework.Repositories;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bow.Administracion.Repositorios
{
    public class HistoriaVialRepositorio : BowRepositoryBase<HistoriaVial>, IHistoriaVialRepositorio
    {
        public HistoriaVialRepositorio(IDbContextProvider<BowDbContext> dbContextProvider)
           : base(dbContextProvider)
        {

        }

        public List<HistoriaVial> GetAllHistoriasWithTipo()
        {
            return GetAll().Include(m => m.CategoriaHistoria).OrderBy(m => m.CategoriaId).ToList();
        }

        public List<HistoriaVial> GetAllHistoriasActivasWithTipo()
        {
            return GetAll().Where(h => h.EsActiva).Include(m => m.CategoriaHistoria).OrderBy(m => m.CategoriaId).ToList();
        }
    }
}
