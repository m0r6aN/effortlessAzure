param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetAutomationCheck.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetAutoChecks'
  properties:{
    description: 'Invoice Automation checks'
    displayName: 'AutomationChecks'
    method: 'GET'
    urlTemplate: '/get-invoice-autochecks'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
