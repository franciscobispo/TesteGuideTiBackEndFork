| BackEnd espeficicações |
| ------ |
| WebAPI desenvolvido em .NET Core 3.1 |
| Modelagem padrão DDD (Domain Driven Design) |
| Projeto de testes chamado 'AutorTests' |
| Package 'Swagger' para exposição, documentação e testes nos métodos da API |
| EntityFrameworkCore para persistência dos dados |

# Como executar:

* Executar solução no Visual Studio (utilizei a versão 2019)
Obs.: a primeira execução provavelmente será mais lenta, pois será necessário a criação do banco de dados (criei um 
mock de dados para já inserir os primeiros dados na tabela 'Autor').
* A execução estará finalizada quando surgir a tela inicial do Swagger, expondo o método '/api/Autor/ObterAutoresAsync/'
  
  Obs.: Neste momento é possível executar testes na API pelo próprio Swagger, por exemplo:
  
  1- clicar no método 'ObterAutoresAsync';
  
  2- após executar o passo 1, surgirá do lado direito um botão chamado 'Try it out', clicar neste botão 'Try it out';
  
  3 - preencher o parâmetro 'quantidadeListaAutores';
  
  4 - clicar no botão 'Execute';
  
  5 - surgirá mensagem de 'Loading', indicando que o método está sendo executado;
  
  6 - após desaparecer a mensagem de 'Loading', é possível visualizar o response body do método, trazendo a lista de autores;
  

  Obs.: Eu gravei um vídeo que está neste repositório, o nome do vídeo é 'Execucao_BackEnd'
