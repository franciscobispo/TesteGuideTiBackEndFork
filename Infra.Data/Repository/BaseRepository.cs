using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.Repository
{
	public class BaseRepository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly DbContextOptions<SQLContext> _OptionsBuilder;
		public BaseRepository()
		{
			_OptionsBuilder = new DbContextOptions<SQLContext>();
		}

		public void Insert(T obj)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				data.Set<T>().Add(obj);
				data.SaveChanges();
			}
		}

		public void Update(T obj)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				data.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				data.SaveChanges();
			}
		}

		public void Delete(int id)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				data.Set<T>().Remove(Select(id));
				data.SaveChanges();
			}
		}

		public T Select(int id)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				return data.Set<T>().Find(id);
			}
		}

		public IList<T> SelectByQuantidade(int tamanhoListaAutores)
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				return data.Set<T>().Take(tamanhoListaAutores).ToList();
			}
		}

		public IList<T> SelectAll()
		{
			using (var data = new SQLContext(_OptionsBuilder))
			{
				return data.Set<T>().ToList();
			}
		}
	}
}
