# **MAUI Recipes App - Receitas de Cozinha - Aplicação Móvel**

- [Inglês](./README.md)

Esta aplicação permite aos utilizadores pesquisar, visualizar e armazenar receitas de cozinha localmente, utilizando a API Spoonacular. As receitas podem ser filtradas por região e ingrediente, com funcionalidades de cache local e expiração de dados.  
Os utilizadores podem também visualizar as receitas marcadas como favoritas e as pesquisas anteriores por Região e/ou Ingrediente (ver capturas de ecrã).  
Ao visualizar uma receita, é possível marcá-la como Favorita; a aplicação tem uma opção separada (barra de separadores) para listar e consultar essas receitas individualmente.

## **Funcionalidades**

- **Pesquisa de Receitas**:
  - Permite pesquisar receitas por região e ingrediente. Os utilizadores podem escolher entre várias regiões do mundo (ex: Italiana, Americana, Japonesa, etc.);
  - Os resultados da pesquisa podem ser filtrados pelo número de receitas (10, 20 ou 30 receitas);

- **Outras opções**
  
  - Lista das últimas pesquisas guardadas (em cache) na base de dados;
  
  - Lista das últimas receitas visualizadas (em cache);
  
  - Lista das receitas mais bem avaliadas (populares), de acordo com os critérios de pesquisa.

- **Armazenamento Local**:
  - As receitas já pesquisadas são armazenadas localmente utilizando uma base de dados SQLite.
    Esta funcionalidade é utilizada para fornecer respostas mais rápidas e otimizadas ao utilizador (offline, sem necessidade de acesso à internet).

- **Escolher Número de Receitas**:
  - Os utilizadores podem selecionar o número de receitas a carregar, com opções para 10, 20 ou 30 receitas.

- **Interface de Pesquisa**:
  - O campo de pesquisa por texto e o menu suspenso permitem aos utilizadores pesquisar por região e/ou ingrediente, clicando no botão **pesquisar**.

- **Visualização de Detalhes da Receita**:
  - Ao selecionar uma receita, os utilizadores podem ver informações detalhadas, incluindo ingredientes, instruções de preparação e informações adicionais sobre a receita.
    Eles também podem marcar a receita como Favorita.

- **Favoritos**
  - A aplicação permite ao utilizador marcar uma receita como favorita; existe uma opção dedicada (separador) para listar e visualizar os detalhes das receitas.

- **Notificações e Alertas**:
  - A aplicação mostra notificações e alertas para informar o utilizador sobre o estado das receitas carregadas (ex: "Receitas carregadas com sucesso", "Receitas carregadas da base de dados", "Erro ao carregar receitas", etc.).

## **Tecnologias Utilizadas**

- **C#**
- **.NET MAUI** - Framework para construir aplicações móveis multiplataforma para Android, iOS, MacOS e Windows.
- **SQLite** - Base de dados local para armazenar receitas.
- **Spoonacular API** - API externa para obter informações sobre receitas de cozinha.
- **CommunityToolkit.Maui/MVVM** - Bibliotecas para simplificar o desenvolvimento de aplicações móveis com MAUI.

## **Como Executar Localmente**

1. Clone o repositório => git clone https://github.com/fauxtix/MauiRecipesApp
2. Obtenha uma chave de API gratuita em https://spoonacular.com/food-api/console
3. Abra o projeto no Visual Studio ou Visual Studio Code.
4. Vá ao arquivo da classe SpoonacularService.cs em Services/Implementations
5. Altere esta linha:
   
   private readonly string? _apiKey = "871cc9ddc1ea4733830dd2c30e3d691a";

   Substitua _apiKey pela chave que obteve do site do Spoonacular.
6. Restaure os pacotes (Reconstrua a solução)
7. Execute a aplicação num emulador ou dispositivo real.

A base de dados SQLite será automaticamente criada no diretório de dados da aplicação.

## **Funcionalidades Futuras**

- Implementar funcionalidade para ordenar receitas por diferentes critérios, como tempo de preparação, popularidade, etc.
- Melhorias na interface, incluindo filtros adicionais de pesquisa.

## **Objetivo Educacional**

Este projeto foi desenvolvido como uma forma de aprender e explorar as capacidades do .Net MAUI.  
O principal objetivo é conhecer e experimentar os seus vários componentes e padrões.  
Adicionalmente, a API Spoonacular foi incorporada para ampliar o escopo da exploração, permitindo a experimentação no acesso a APIs externas na aplicação.  
No MauiRecipesApp, a API Spoonacular foi utilizada para obter dados sobre receitas de várias regiões do mundo.

### Objetivos de Aprendizagem

- Familiarizar-se com os componentes do .Net MAUI e o seu uso;
- Compreender como integrar APIs externas em aplicações MAUI;
- Aprender o padrão arquitetural MVVM, utilizando Maui.Community.Toolkit;
- Explorar técnicas de visualização e apresentação de dados.

## **Trabalho em Progresso**

Este projeto está atualmente em desenvolvimento. Embora forneça funcionalidades básicas para pesquisa de receitas e acesso à API Spoonacular, espera-se que evolua com o tempo.

# **Capturas de Ecrã**

![RecipesMainPage](https://github.com/user-attachments/assets/df7902cd-5188-456c-9456-7fc21d33bfc7)
![RecipesMainPage3](https://github.com/user-attachments/assets/57f01f46-d42a-4517-8a97-01afc9577e3d)
![RecipesMainPage2](https://github.com/user-attachments/assets/7c246101-73e4-45cc-8ba7-e22f1595c1bf)
![RecipesMainPage4](https://github.com/user-attachments/assets/383186db-6378-45c4-bb79-70902895ee87)
![BottomSheet](https://github.com/user-attachments/assets/39470b04-c34c-4086-80c0-e823e557e3f3)
![RecipeRegionsPicker](https://github.com/user-attachments/assets/d935c6c8-b9d5-4aa0-ba2b-11749c93b666)
![RecipesList](https://github.com/user-attachments/assets/b61327a6-6397-45ad-b1ec-113c6235e53f)
![ViewRecipe](https://github.com/user-attachments/assets/c4b810e8-529d-4393-a7ef-2cf7224afdf0)
![ViewRecipe_Summary](https://github.com/user-attachments/assets/23b7fdfe-656b-47b0-b1fa-acc66d732841)
![ViewRecipe_Instructions](https://github.com/user-attachments/assets/e58a59c4-d234-4a45-a330-2fd647538aef)
![ViewRecipe_Ingredients](https://github.com/user-attachments/assets/d05162b3-9ddc-48e8-964c-43b0763634c9)
![Favorites](https://github.com/user-attachments/assets/3932ba20-228a-4fad-982a-473136f9a200)

### 🌟 **Contribuindo**

Contribuições são bem-vindas! Se tiver sugestões de novas funcionalidades ou melhorias, sinta-se à vontade para submeter um pull request ou abrir uma issue no GitHub.

1. Faça um fork do projeto (https://github.com/fauxtix/MauiRecipesApp/fork)
2. Crie um branch para a sua modificação (git checkout -b fauxtix/MauiRecipesApp)
3. Faça o commit (git commit -am 'Adicionar algo como fooBar')
4. Faça o push (git push origin fauxtix/MauiRecipesApp)
5. Crie um novo Pull Request

Ao contribuir com código, siga o estilo de código existente e submeta um pull request com as suas alterações.

### **Aviso Legal**

Este projeto está em andamento e pode passar por alterações e atualizações sem aviso prévio. Está a ser desenvolvido para fins educacionais e pode incluir funcionalidades e componentes experimentais.  
Não tem fins comerciais nem é destinado à produção.

## ⚖ **Licença**

O projeto MauiRecipes está licenciado sob a [Licença MIT](./LICENSE.md).

## 📞 **Contactos**

Se tiver alguma dúvida ou necessitar de assistência, pode contactar o mantenedor do projeto:

- 👨‍💻 Mantenedor: Fausto Luís
- ✉ E-mail: fauxtix.luix@hotmail.com

Sinta-se à vontade para contactar-me com qualquer feedback ou dúvida.
