@description('Specifies the location for resources.')
param location string = 'westeurope'

targetScope = 'subscription'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'cifra-rg'
  location: location
}

module appservicePlan 'appservicePlan.bicep' = {
  name: 'appservicePlan'
  scope: resourceGroup
  params: {
    location: resourceGroup.location
  }
}

module apiAppservice 'appservice.bicep' = {
  name: 'apiAppservice'
  scope: resourceGroup
  params: {
    webSiteName: 'cifra-api'
    location: resourceGroup.location
  }
}

module frontendAppservice 'appservice.bicep' = {
  name: 'frontendAppservice'
  scope: resourceGroup
  params: {
    webSiteName: 'cifra'
    location: resourceGroup.location
  }
}
