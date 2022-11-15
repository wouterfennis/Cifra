param webAppName string = uniqueString(resourceGroup().id) // Generate unique String for web app name
param location string = resourceGroup().location // Location for all resources
param webSiteName string

var appServicePlanName = toLower('AppServicePlan-${webAppName}')

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' existing = {
  name: appServicePlanName
}

resource webApp 'Microsoft.Web/sites@2021-01-01' = {
  name: webSiteName
  location: location
  tags: {}
  properties: {
    siteConfig: {
      linuxFxVersion: 'DOCKER'
    }
    serverFarmId: appServicePlan.id
  }
}
