@description('Specifies the location for resources.')
param location string = 'westeurope'

targetScope = 'subscription'

resource cifraResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'cifra-rg'
  location: location
}

module appservicePlan 'appservicePlan.bicep' = {
  name: 'appservicePlan'
  scope: resourceGroup(cifraResourceGroup.id)
  params: {
    location: location
  }
}

module apiAppservice 'appservice.bicep' = {
  name: 'apiAppservice'
  scope: resourceGroup(cifraResourceGroup.id)
  params: {
    webSiteName: 'cifra-api'
    location: location
  }
}

module frontendAppservice 'appservice.bicep' = {
  name: 'frontendAppservice'
  scope: resourceGroup(cifraResourceGroup.id)
  params: {
    webSiteName: 'cifra'
    location: location
  }
}
