param location string

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

output appServicePlanName string = appServicePlan.name
