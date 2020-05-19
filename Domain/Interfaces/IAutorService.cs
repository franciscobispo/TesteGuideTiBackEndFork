using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAutorService<T> where T : Autor
    {
        Task<IList<Autor>> SelectByQuantidadeAsync(int quantidadeListaAutores);
    }
}
