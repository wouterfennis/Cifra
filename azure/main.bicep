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

module apiAppservice 'appservice.bicep' = {
  name: 'apiAppservice'
  params: {
    webSiteName: 'cifraApi'
    location: location
    appServicePlanName: appServicePlan.name
  }
}

module frontendAppservice 'appservice.bicep' = {
  name: 'frontendAppservice'
  params: {
    webSiteName: 'cifraFrontend'
    location: location
    appServicePlanName: appServicePlan.name
  }
}
