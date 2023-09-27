param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostAutomationCheck.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostAutoCheck'
  properties:{
    description: 'Automation check'
    displayName: 'AutomationCheck'
    method: 'POST'
    urlTemplate: '/post-auto-check'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
