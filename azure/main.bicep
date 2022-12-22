@description('Specifies the location for resources.')
param location string = 'westeurope'

@description('The name of the resource group.')

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
  name: 'apiAppservice'
  location: location
  tags: {}
  properties: {
    siteConfig: {
      linuxFxVersion: 'DOCKER'
      appSettings: {}
    }
    serverFarmId: appServicePlan.id
  }
}

resource webApp 'Microsoft.Web/sites@2021-01-01' = {
  name: 'frontendAppservice'
  location: location
  tags: {}
  properties: {
    siteConfig: {
      linuxFxVersion: 'DOCKER'
    }
    serverFarmId: appServicePlan.id
  }
}
