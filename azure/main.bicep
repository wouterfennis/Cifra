@description('Specifies the location for resources.')
param location string = resourceGroup().location

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: 'AppServicePlan'
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: 'F1'
  }
  kind: 'linux'
}

resource webApi 'Microsoft.Web/sites@2021-01-01' = {
  name: 'cifraApi2023'
  location: location
  tags: {}
  properties: {
    siteConfig: {
      linuxFxVersion: 'DOCKER'
    }
    serverFarmId: appServicePlan.id
  }
}

resource webApp 'Microsoft.Web/sites@2021-01-01' = {
  name: 'cifraFrontend2023'
  location: location
  tags: {}
  properties: {
    siteConfig: {
      linuxFxVersion: 'DOCKER'
      appSettings:  [
        {
          name: 'CifraApiBaseUrl'
          value: 'https://${webApi.properties.defaultHostName}'
        }
      ]
    }
    serverFarmId: appServicePlan.id
  }
}
