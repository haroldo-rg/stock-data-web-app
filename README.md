# Descrição do projeto

Aplicação web para consulta de ações negociadas na B#3. Lista as informações, permite aplicar múltiplos filtros e acessar links para consultar mais detalhes das ações.

As informações são extraídas do site: https://www.fundamentus.com.br

### Pré-requisitos
Requer SDK dotnet 8.0.302 ou superior: [.NET Downloads (Linux, macOS, and Windows)](https://dotnet.microsoft.com/en-us/download/dotnet)


### Dependências externas (pacotes NuGet)

 - HtmlAgilityPack
 
*É baixado de forma automática ao efetuar o build*

### Executar o projeto
No prompt de comandos, na pasta raiz do projeto, digitar o comando: `dotnet run`

    Compilando...
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: **http://localhost:5189**
    info: Microsoft.Hosting.Lifetime[0]
          Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
          Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
          Content root path: ...\stock-data-web-app

Print da aplicação

![teste](https://github.com/haroldo-rg/stock-data-web-app/blob/main/images/print.png)