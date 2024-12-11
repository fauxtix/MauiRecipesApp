# **Cooking Recipes - Aplicação Móvel**

Esta aplicação permite aos utilizadores pesquisar, visualizar e guardar receitas de culinária localmente, utilizando a API Spoonacular. As receitas podem ser filtradas por região e ingrediente, com funcionalidades de cache local e expiração de dados.  
Os utilizadores também podem visualizar as receitas marcadas como favoritas e as receitas pesquisadas anteriormente por Região e/ou Ingrediente (ver capturas de ecrã).  
Ao visualizar uma receita, existe a possibilidade de a marcar como Favorita; a aplicação possui uma opção separada (barra de separadores) para listar e consultar estas receitas individualmente.

## **Funcionalidades**

- **Pesquisa de Receitas**:
  - Permite pesquisar receitas por região e ingrediente. Os utilizadores podem escolher entre diferentes regiões do mundo (ex.: Italiana, Americana, Japonesa, etc.);
  - Os resultados da pesquisa podem ser filtrados pelo número de receitas (10, 20 ou 30 receitas).

- **Outras opções**:
  - Lista das últimas pesquisas guardadas (em cache) na base de dados;
  - Lista das últimas receitas visualizadas (em cache);
  - Lista das receitas mais bem avaliadas (populares), de acordo com os critérios de pesquisa.

- **Armazenamento Local**:
  - As receitas já pesquisadas são armazenadas localmente utilizando uma base de dados SQLite.  
    Esta funcionalidade permite respostas mais rápidas e otimizadas para o utilizador (modo offline, sem necessidade de acesso à internet).

- **Escolher Número de Receitas**:
  - Os utilizadores podem selecionar o número de receitas a carregar, com opções de 10, 20 ou 30 receitas.

- **Interface de Pesquisa**:
  - O campo de texto de pesquisa e o menu dropdown permitem aos utilizadores pesquisar por região e/ou ingrediente, ao clicar num botão **Pesquisar**.

- **Visualização de Detalhes da Receita**:
  - Ao selecionar uma receita, os utilizadores podem visualizar informações detalhadas, incluindo ingredientes, instruções de preparação e informações adicionais sobre a receita.  
    Também podem marcar a receita como Favorita.

- **Favoritos**:
  - A aplicação permite que os utilizadores marquem receitas como favoritas; existe uma opção dedicada (separador) para listar e visualizar os detalhes dessas receitas.

- **Notificações e Alertas**:
  - A aplicação apresenta notificações e alertas para informar o utilizador sobre o estado das receitas carregadas (ex.: "Receitas carregadas com sucesso", "Receitas carregadas da base de dados", "Erro ao carregar receitas", etc.).

## **Tecnologias Utilizadas**

- **C#**
- **.NET MAUI** - Framework para criar aplicações móveis multiplataforma para Android, iOS, MacOS e Windows.
- **SQLite** - Base de dados local para armazenamento de receitas.
- **Spoonacular API** - API externa para obter informações sobre receitas culinárias.
- **CommunityToolkit.Maui/MVVM** - Bibliotecas para simplificar o desenvolvimento de aplicações móveis com MAUI.

## **Como Executar Localmente**

1. Clone o Repositório:
   ```bash
   git clone https://github.com/fauxtix/MauiRecipesApp
Obtenha uma chave de API gratuita em https://spoonacular.com/food-api/console.
Abra o projeto no Visual Studio ou Visual Studio Code.
Aceda ao ficheiro SpoonacularService.cs na pasta Services/Implementations.
Substitua esta linha:
csharp
Copiar código
private readonly string? _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a";
Altere o valor de _apiKey pela chave que obteve no site da Spoonacular.
Restaure os pacotes (Reconstrua a solução).
Execute a aplicação num emulador ou dispositivo real.
A base de dados SQLite será criada automaticamente no diretório de dados da aplicação.

Funcionalidades Futuras
Implementar funcionalidade para ordenar receitas por diferentes critérios, como tempo de preparação, popularidade, etc.
Melhorias na interface de utilizador, incluindo filtros de pesquisa adicionais.
Objetivo Educativo
Este projeto foi desenvolvido como uma forma de aprender e explorar as capacidades do .Net MAUI.
O principal objetivo é conhecer e experimentar os seus vários componentes e padrões.
Adicionalmente, a API Spoonacular foi incorporada para ampliar o âmbito de exploração, permitindo a experimentação com o acesso a APIs externas numa aplicação.
No MauiRecipesApp, a API Spoonacular foi utilizada para obter dados de receitas de várias regiões do mundo.

Objetivos de Aprendizagem
Familiarizar-se com os componentes do .Net MAUI e o seu uso;
Compreender como integrar APIs externas em aplicações MAUI;
Aprender o padrão arquitetural MVVM, utilizando o Maui.Community.Toolkit;
Explorar técnicas de visualização e apresentação de dados.
Estado do Projeto
Este projeto está atualmente em desenvolvimento. Embora forneça funcionalidades básicas para a pesquisa de receitas e acesso à API Spoonacular, espera-se que evolua ao longo do tempo.

Capturas de Ecrã













🌟 Contribuir
Contribuições são bem-vindas! Se tiver sugestões para novas funcionalidades ou melhorias, sinta-se à vontade para submeter um pull request ou abrir uma issue no GitHub.

Faça um fork do projeto:
https://github.com/fauxtix/MauiRecipesApp/fork
Crie uma branch para as suas alterações:
bash
Copiar código
git checkout -b fauxtix/MauiRecipesApp
Faça um commit:
bash
Copiar código
git commit -am 'Adicionar alguma funcionalidade'
Envie as alterações:
bash
Copiar código
git push origin fauxtix/MauiRecipesApp
Crie um novo Pull Request.
Aviso Legal
Este projeto está em desenvolvimento e pode sofrer alterações e atualizações sem aviso prévio. Está a ser desenvolvido para fins educacionais e pode incluir funcionalidades e componentes experimentais.
Não é destinado a fins comerciais ou de produção.

⚖ Licença
O projeto MauiRecipes está licenciado sob a Licença MIT.

📞 Contacto
Se tiver alguma questão ou precisar de assistência adicional, pode entrar em contacto com o responsável pelo projeto:

👨‍💻 Responsável: Fausto Luís
✉ Email: fauxtix.luix@hotmail.com
Sinta-se à vontade para me contactar com qualquer feedback ou questões.
