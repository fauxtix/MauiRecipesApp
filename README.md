# **Cooking Recipes - Mobile Application**

This application allows users to search, view, and store cooking recipes locally, using the Spoonacular API. Recipes can be filtered by region and ingredient, with features for local caching and date expiration.
The users may also view the recipes marked as favorites, and the previously searched recipes by Region and/or Ingredient (see the screenshots).
When viewing a recipe, there is the possibility to mark it as a Favorite; the app has an option to list and consult them.

## **Features**

- **Recipe Search**:
  - Allows searching for recipes by region and ingredient. Users can choose from different regions of the world (e.g., Italian, American, Japanese, etc.);
  - Search results can be filtered by the number of recipes (10, 20, or 30 recipes);
 
- **Other options** on the main page:
  - List of the last 10 searches saved in the database;
  - List of the last recipes viewed, with option for individual viewing;
  - List of the best rated recipes, according to the search selected by the usesr. They can also view the details of a selected recipe.

- **Local Storage**:
  - Recipes already researched, are stored locally using an SQLite database.
    This feature is used to provide faster, optimized responses to the user.

- **Cleanup**:
  - There's an option to manually clean all recipes stored locally, via the app's interface.

- **Choose Number of Recipes**:
  - Users can select the number of recipes to load, with options for 10, 20, or 30 recipes.

- **Search Interface**:
  - The text search field and the dropdown, allows users to search by region and/or ingredient, by clicking a **search** button.
  
- **Recipe Details View**:
  - By selecting a recipe, users can view detailed information, including ingredients, preparation instructions, and additional information about the recipe.
    They also can mark the recipe as Favorite. 

- **Notifications and Alerts**:
  - The app shows notifications and alerts to inform the user about the status of loaded recipes (e.g., "Recipes loaded successfully", "Recipes loaded from database", "Error loading recipes", etc.).

## **Technologies Used**

- **C#**
- **.NET MAUI** - Framework for building cross-platform mobile applications for Android, iOS, MacOS, and Windows.
- **SQLite** - Local database for storing recipes.
- **Spoonacular API** - External API for fetching information on cooking recipes.
- **CommunityToolkit.Maui/MVVM** - Libraries to simplify mobile app development with MAUI.

## **How to Run**

1. Clone the repository:

    ```bash
    git clone https://github.com/fauxtix/MauiRecipesApp
    ```

2. Open the project in Visual Studio or Visual Studio Code.

3. Run the app on an emulator or real device.

4. The SQLite database will be automatically created in the app's data directory.

## **Future Features**

- Implement functionality to sort recipes by different criteria such as preparation time, popularity, etc.
- UI improvements, including additional search filters.

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
![RecipesMainPage](https://github.com/user-attachments/assets/1a72310a-f2a2-4fdd-8d4e-c25a833857b8)
![RecipesMainPageWithIngredientSearch](https://github.com/user-attachments/assets/942675e7-d576-4c79-b5d5-bfca5a42f43d)
![ViewRecipe](https://github.com/user-attachments/assets/c4b810e8-529d-4393-a7ef-2cf7224afdf0)
![RecipesMainPageDeleteSavedSearches](https://github.com/user-attachments/assets/107a0781-1b6e-4953-981a-3814d9eaac8f)
![ViewRecipe_Summary](https://github.com/user-attachments/assets/23b7fdfe-656b-47b0-b1fa-acc66d732841)
![ViewRecipe_Instructions](https://github.com/user-attachments/assets/e58a59c4-d234-4a45-a330-2fd647538aef)
![ViewRecipe_Ingredients](https://github.com/user-attachments/assets/d05162b3-9ddc-48e8-964c-43b0763634c9)
![Favorites](https://github.com/user-attachments/assets/3932ba20-228a-4fad-982a-473136f9a200)
![ViewFavorite](https://github.com/user-attachments/assets/d189f2e2-8bd8-4198-aa60-a0e6175ad9f5)
![SavedSearches](https://github.com/user-attachments/assets/dd04b016-08d2-4ccd-b1f4-9ae313c0b5ff)

### Contributions

Contributions are welcome! If you have suggestions for new features or improvements, feel free to submit a pull request or open an issue on GitHub.

### Disclaimer

This project is a work in progress and may undergo changes and updates without prior notice. It is being developed for educational purposes and may include experimental features and components.
It does not serve any commercial or production purposes.

