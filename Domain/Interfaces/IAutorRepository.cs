using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAutorRepository<T> where T : Autor
    {
        Task<IList<Autor>> SelectByQuantidadeAsync(int quantidadeListaAutores);
    }
}
