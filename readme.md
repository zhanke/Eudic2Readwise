# Eudic 2 Readwise
Azure function to sync words from Eudic (生词本) to Readwise (Quotes).

## Setup
1. Get API Keys from Eudic and Readwise
  1. Eudic
    * Documentation: http://my.eudic.net/OpenAPI/Doc_Index 
    * API Key: http://my.eudic.net/OpenAPI/Authorization 
  1. Readwise
    * Documentation: https://readwise.io/api_deets (note: only Highlight CREATE is working)
    * API Token: https://readwise.io/access_token 
1. Checkout repo
1. Ensure .Net core 3 and Azure Function SDK are installed. This is using Visual Studio. In theory VS code should also work, haven't tried
1. Rename local.settings.json.example to local.settings.json and fill in all the settings
  * Do the same for Test project too if you want to run the inetgration tests
  * TimerSchedule format can be referenced here: https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp#ncrontab-expressions
1. Run Eudic2Readwise locally

## Deploy to Auzre Function
1. Right click on Eudic2Readwise and select "Publish" then follow the wizard
1. After it's deployed. Add all the settings in "Application settings" of the Azure Function app in Azure
3. Recommend to enable Application Insights for easier log viewing

## Postman
Postman collection and enviornment can be imported using files under /Postman folder

## Note
1. No pagination in Eudic API call, it just gets 2000 max at the moment.
1. Recommend to do setup to add words automatically when quering on Eudic app so no need to manually adding them to 生词本.