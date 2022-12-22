@description('Specifies the location for resources.')
param location string = 'westeurope'

@description('The name of the resource group.')

module appservicePlan 'appservicePlan.bicep' = {
  name: 'appservicePlan'
  params: {
    location: location
  }
}

module apiAppservice 'appservice.bicep' = {
  name: 'apiAppservice'
  params: {
    webSiteName: 'cifra-api'
    location: location
    appServicePlanName: appservicePlan.outputs.appServicePlanName
  }
}

module frontendAppservice 'appservice.bicep' = {
  name: 'frontendAppservice'
  params: {
    webSiteName: 'cifra-dev'
    location: location
    appServicePlanName: appservicePlan.outputs.appServicePlanName
  }
}
