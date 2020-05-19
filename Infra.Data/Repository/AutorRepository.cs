using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
	public class AutorRepository<T> : IAutorRepository<T> where T : Autor
	{
		private readonly DbContextOptions<SQLContext> _OptionsBuilder;
		public AutorRepository()
		{
			_OptionsBuilder = new DbContextOptions<SQLContext>();
		}		

		public async Task<IList<Autor>> SelectByQuantidadeAsync(int quantidadeListaAutores)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				return await data.Set<Autor>().Take(quantidadeListaAutores).ToListAsync();
			}
		}		
	}
}
