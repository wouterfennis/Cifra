@description('Specifies the location for resources.')
param location string = 'westeurope'

@description('The name of the resource group.')
param resourceGroupName string = 'cifra-rg'

module appservicePlan 'appservicePlan.bicep' = {
  name: 'appservicePlan'
  scope: resourceGroup(resourceGroupName)
  params: {
    location: location
  }
}

module apiAppservice 'appservice.bicep' = {
  name: 'apiAppservice'
  scope: resourceGroup(resourceGroupName)
  params: {
    webSiteName: 'cifra-api'
    location: location
  }
}

module frontendAppservice 'appservice.bicep' = {
  name: 'frontendAppservice'
  scope: resourceGroup(resourceGroupName)
  params: {
    webSiteName: 'cifra-dev'
    location: location
    appSettings: { appSettings: [
        {
          name: 'CifraApiBaseAddress'
          value: apiAppservice.outputs.url
        }
      ]
    }
  }
}
