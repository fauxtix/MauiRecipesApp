## Setting Up the Spoonacular API Key

To use this project, you need to provide your own Spoonacular API key. There are two ways to do this: using an environment variable or configuring it via **User Secrets** (for Visual Studio users).

### Option 1: Environment Variable

Set the `SpoonacularApiKey` environment variable on your system with your Spoonacular API key.

#### Instructions:

- **Windows**:
  Open PowerShell and enter:
  ```powershell
  [System.Environment]::SetEnvironmentVariable("SpoonacularApiKey", "YOUR_API_KEY_HERE", "User")

- **Mac/Linux**:
  Open the terminal and enter (bach):
  export SpoonacularApiKey="YOUR_API_KEY_HERE"

  Replace YOUR_API_KEY_HERE with your actual Spoonacular API key.

  ### Option 2: User Secrets (for Visual Studio Users)
  
  If you are using Visual Studio, you can configure the key securely using User Secrets.
  
  Instructions:
  1. In Solution Explorer, right-click on your main project and select Manage User Secrets.
  2. In the secrets.json file that opens, add the following:
  {
    "SpoonacularApiKey": "YOUR_API_KEY_HERE"
  }
  3. Save and close the file.
 
  Replace YOUR_API_KEY_HERE with your actual Spoonacular API key. The project will now have access to the key without it being included in source control.

  ## Important
  If neither option is configured, the application will throw an error indicating that the API key is missing. Please ensure you set one of these options before running the application. 


  This provides clear instructions for setting up the key in a secure way for anyone who uses this project.
