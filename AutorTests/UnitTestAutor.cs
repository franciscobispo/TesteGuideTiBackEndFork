using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Services;

namespace AutorTests
{
    [TestClass]
    public class UnitTestAutor
    {
        private AutorService<Autor> autorService = new AutorService<Autor>();

        [TestMethod]
        public async System.Threading.Tasks.Task TestObterListaAutoresAsync()
        {
            var listaAutores = await autorService.SelectByQuantidadeAsync(2);
            Assert.AreEqual(2, listaAutores.Count);
        }
    }
}
