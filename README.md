# Playing with .Net MAUI by using the Spoonacular Api

## Functionalities

- Search recipes by Region
- Search for an ingredient (within Region, or not)

  You may select 10, 20 or 30 recipes from the Api

## Educational Purpose

This project was developed as a way of learning and exploring the capabilities of .Net MAUI. 
The main objective is to get to know and experiment its various components and standards. 
Additionally, the Spoonacular Api was incorporated to broaden the scope of exploration, allowing for experimentation with accessing external Apis into the application.
In the MauiRecipesApp, the Spoonacular Api was used to get recipe data from various regions of the world.

### Learning Objectives

- Gain familiarity with .Net MAUI components and their usage;
- Understand how to integrate external APIs into MAUI apps;
- Learn the MVVM architectural pattern, by using Maui.Community.Toolkit;
- Explore data visualization and presentation techniques.

## Work in Progress

This project is currently a work in progress. While it provides basic functionality for recipe search and access to the Spoonacular Api, it is expected to evolve over time.

# Screenshots

![RecipesMainPage](https://github.com/user-attachments/assets/c1f0550c-4f01-4184-9c53-7985dd00faaa)
![RecipesMainPageWithIngredientSearch](https://github.com/user-attachments/assets/d08acb4a-4ffa-4109-a7ba-7ca0c945e194)
![ViewRecipe](https://github.com/user-attachments/assets/13334ef1-cd10-48b9-85e2-9810f424ffcd)
![ViewRecipe_Summary](https://github.com/user-attachments/assets/b4d1751c-9dae-49c1-a22e-97a9279e2cb0)
![ViewRecipe_Instructions](https://github.com/user-attachments/assets/e58a59c4-d234-4a45-a330-2fd647538aef)
![ViewRecipe_Ingredients](https://github.com/user-attachments/assets/6bb6b93a-7826-43cb-8551-81864f375232)



### Contributions

Contributions are welcome! If you have suggestions for new features or improvements, feel free to submit a pull request or open an issue on GitHub.

### Disclaimer

This project is a work in progress and may undergo changes and updates without prior notice. It is being developed for educational purposes and may include experimental features and components.
It does not serve any commercial or production purposes.

# **Receitas de Cozinha - Aplicação Mobile**

Esta aplicação permite aos usuários pesquisar, visualizar e armazenar receitas de cozinha localmente, utilizando a API Spoonacular. As receitas podem ser filtradas por região e ingrediente, além de oferecer funcionalidades de cache local e expiração de dados.

## **Funcionalidades**

- **Pesquisa de Receitas**:
  - Permite pesquisar receitas por região e ingrediente. O usuário pode escolher entre diferentes regiões do mundo (ex: italiana, americana, japonesa, etc.).
  - A pesquisa pode ser filtrada por número de receitas (10, 20 ou 30 receitas).

- **Armazenamento Local**:
  - As receitas podem ser armazenadas localmente usando um banco de dados SQLite.
  - As receitas são associadas a uma chave única (`RecipeKey`) para garantir que não sejam duplicadas.
  - O cache local é utilizado para armazenar as receitas e permitir uma resposta mais rápida e otimizada ao usuário.

- **Expiração e Limpeza de Dados**:
  - As receitas armazenadas localmente têm um campo **`ExpirationDate`** que define o tempo de validade.
  - Quando a data de expiração de uma receita é anterior à data atual, a receita é **removida** da base de dados.
  - A opção para limpar receitas expiradas pode ser acionada manualmente através da interface da aplicação.

- **Escolha de Número de Receitas**:
  - O usuário pode selecionar o número de receitas a serem carregadas, com as opções de 10, 20 ou 30 receitas.

- **Interface de Pesquisa**:
  - O campo de pesquisa permite ao usuário buscar por receitas usando um botão de pesquisa ou pressionando **Enter**.
  - O campo de pesquisa também pode ser usado para pesquisar receitas por ingredientes ou palavras-chave específicas.

- **Exibição de Detalhes da Receita**:
  - Ao selecionar uma receita, o usuário pode visualizar os detalhes completos, incluindo ingredientes, instruções de preparo e informações adicionais sobre a receita.

- **Notificações e Alertas**:
  - A aplicação exibe notificações e alertas ao usuário para informar sobre o status das receitas carregadas (ex: "Receitas carregadas com sucesso", "Erro ao carregar receitas", etc.).

## **Tecnologias Utilizadas**

- **.NET MAUI** - Framework para construção de aplicações móveis nativas para Android, iOS, MacOS e Windows.
- **SQLite** - Banco de dados local para armazenamento de receitas.
- **Spoonacular API** - API externa para buscar informações sobre receitas de cozinha.
- **CommunityToolkit.Maui** - Biblioteca para simplificar a criação de aplicações móveis no MAUI.

## **Como Executar**

1. Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/receitas-app.git
    ```

2. Abra o projeto no Visual Studio ou Visual Studio Code.

3. Execute o aplicativo no emulador ou dispositivo real.

4. O banco de dados SQLite será criado automaticamente no diretório de dados da aplicação.

## **Funcionalidades Futuras**

- Implementação de uma funcionalidade para ordenar as receitas por diferentes critérios, como tempo de preparo, popularidade, etc.
- Melhorias na interface do usuário, incluindo filtros adicionais de pesquisa.

