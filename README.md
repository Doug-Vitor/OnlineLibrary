<h1 align="center">Online Library</h1>
<h4 align="center">Simulação de uma biblioteca online, com possibilidades de criação de contas, criação de livros e carrinho de compras utilizável.</h3>

<h3>:warning: ATENÇÃO:</h3>
<p><strong>TODOS</strong> os livros contidos nessa aplicação foram retirados do site oficial da <a href="https://www.amazon.com.br/Livros/b/?ie=UTF8&node=6740748011&ref_=topnav_storetab_b">Amazon</a> e todos possuem seus respectivos autores visando manter seus direitos autorais. É válido lembrar que, essa aplicação é um <strong>PROJETO PARA PORTFÓLIO</strong> e não possui nenhum fim lucrativo. Portanto, não solicitamos nenhum dado pessoal dos usuários que eventualmente queiram testar esse projeto. Fique atento!<p>

<br/>
<h3>:computer: Tecnologias utilizadas:</h3>
<h4>
 <ul>
  <li>DotNET 5.0</li>
  <li>SQL Server</li>
  <li>Entity Framework Core</li>
  </ul>
</h4>

<br/>
<h3>:wrench: Quer rodar o projeto? Siga os passos:</h3>
<h4>
 <ul><li>É necessário instalar o Visual Studio 2019 ou Visual Studio Code e SQL Server</li></ul>
 
 <br/>
 <ol>
  <li>Faça o download ou clone o projeto.</li>
  <li>Abra o arquivo de solução chamado OnlineLibrary.sln</li>
  <li>No arquivo appsettings.json, altere o endereço de conexão em "Default" para sua conexão local. Queira utilizar:
   <blockquote>
    "ConnectionStrings": { 
     <p>"Default": "Server=NomeDoSeuServidor;DATABASE=MyLibrary;Trusted_Connection=True;MultipleActiveResultSets=True"</p>
    }
   </blockquote>
  </li>
  <li>Restaure os pacotes NuGet da solução:
   <ul>
    <p>Pelo CLI: <blockquote>dotnet restore</blockquote></p>
    <p>Pelo CLI do NuGet: <blockquote>nuget restore OnlineLibrary.sln</blockquote></p>
   </ul>
  </li>
  
  <li>Abra o Console de Gerenciador de Pacotes do Nuget e execute o seguinte comando para criar e restaurar as tabelas do banco de dados:<blockquote>Update-Database</blockquote></li>
 </ol>
</h4>

<h3>O que aprendi neste projeto:</h3>
<h4>
 <ul>
  <li>Autenticação e autorização com Identity Framework.</li>
  <li>Configuração e utilização de áreas.</li>
  <li>Paginação de dados utilizando funcionalidades próprias do SQL Server e C#</li>
 </ul>
</h4>

<br/>
<h3>Referências:</h3>
<ul>
 <li>Obter o usuário autenticado fora dos controladores: <a href="https://stackoverflow.com/questions/57990635/get-current-user-outside-of-controller">Clique aqui.</a></li>
 <li>Evitar ataques com o atributo ValidateAntiForgeryToken: <a href="https://docs.microsoft.com/pt-br/aspnet/core/security/anti-request-forgery?view=aspnetcore-5.0">Clique aqui.</a></li>
 </ul>
 
 <br/>
<h4>É válido ressaltar que o autor deste projeto foca seu aprendizado em desenvolvimento back-end. Portanto, diversos elementos do sistema OnlineLibrary que estejam atrelados ao desenvolvimento front-end podem estar desalinhados, mal formatados ou mal posicionados.
</h4>
