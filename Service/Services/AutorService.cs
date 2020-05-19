using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AutorService<T> : IAutorService<T> where T : Autor
    {
        private AutorRepository<T> autorRepository = new AutorRepository<T>();
        string[] parentescosArray = { "FILHO", "FILHA", "NETO", "NETA", "SOBRINHO", "SOBRINHA", "JUNIOR" };
        string[] preposicoesArray = { "DA", "DE", "DO", "DAS", "DOS" };

        public async Task<IList<Autor>> SelectByQuantidadeAsync(int quantidadeListaAutores)
        {
            if (quantidadeListaAutores <= 0)
                throw new ArgumentException("Parâmetro 'tamanhoListaAutores' deve ser maior que zero.");

            var listaAutores = await autorRepository.SelectByQuantidadeAsync(quantidadeListaAutores);
            
            if (listaAutores != null)
            {
                foreach (var itemListaAutores in listaAutores)
                {
                    var nomeSplitted = itemListaAutores.Nome.Split(" ");

                    if (nomeSplitted.Count() == 1)
                    { //1ª regra: se houver apenas uma parte no nome, ela deve ser apresentada em letras maiúsculas (sem vírgula)
                        itemListaAutores.Nome = itemListaAutores.Nome.ToUpper();
                    }
                    else if (nomeSplitted.Count() >= 2 &&
                                //(nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("FILHO") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("FILHA") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("NETO") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("NETA") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("SOBRINHO") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("SOBRINHA") ||
                                // nomeSplitted[nomeSplitted.Count() - 1].ToUpper().Contains("JUNIOR"))
                                parentescosArray.Any(x => x == nomeSplitted[nomeSplitted.Count() - 1])
                                 )
                    {
                        if (nomeSplitted.Count() == 2)//se a entrada for "Joao Neto" , a saída deve ser "NETO, Joao"
                        {
                            itemListaAutores.Nome = (nomeSplitted[1].ToUpper() + ", " + PrimeiraLetraMaiuscula(nomeSplitted[0].ToLower()));
                        }
                        else
                        {                            
                            List<string> restoNome = new List<string>();
                            for (int index = 0; index < nomeSplitted.Count() - 2; index++)
                            {
                                if (
                                    //nomeSplitted[index].ToUpper().Contains("DA") || nomeSplitted[index].ToUpper().Contains("DE") ||
                                    //nomeSplitted[index].ToUpper().Contains("DO") || nomeSplitted[index].ToUpper().Contains("DAS") ||
                                    //nomeSplitted[index].ToUpper().Contains("DOS")
                                    preposicoesArray.Any(x => x == nomeSplitted[index].ToUpper())
                                    )
                                {
                                    restoNome.Add(nomeSplitted[index].ToLower());
                                }
                                else
                                    restoNome.Add(PrimeiraLetraMaiuscula(nomeSplitted[index].ToLower()));
                            }                            
                            itemListaAutores.Nome =  (nomeSplitted[nomeSplitted.Count() - 2].ToUpper() + " " + nomeSplitted[nomeSplitted.Count() - 1].ToUpper() + ", " + string.Join(" ", restoNome));
                        }
                    }
                    else
                    { //regra: nome >= 2 index e não possui sufixos DA, DOS....
                        
                        List<string> restoNome = new List<string>();
                        for (int index = 0; index < nomeSplitted.Count() - 1; index++)
                        {
                            if (
                                 //nomeSplitted[index].ToUpper().Contains("DA") || nomeSplitted[index].ToUpper().Contains("DE") ||
                                 //nomeSplitted[index].ToUpper().Contains("DO") || nomeSplitted[index].ToUpper().Contains("DAS") ||
                                 //nomeSplitted[index].ToUpper().Contains("DOS")
                                 preposicoesArray.Any(x => x == nomeSplitted[index].ToUpper())
                                 )
                            {
                                restoNome.Add(nomeSplitted[index].ToLower());
                            }
                            else
                                restoNome.Add(PrimeiraLetraMaiuscula(nomeSplitted[index].ToLower()));
                        }
                        itemListaAutores.Nome = nomeSplitted[nomeSplitted.Count() - 1].ToUpper() + ", " + string.Join(" ", restoNome);
                    }

                }
            }

            return listaAutores;
        }

        private string PrimeiraLetraMaiuscula(string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };
    }
}
