param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/UnblockInvoices.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/UnblockInvoices'
  properties:{
    description: 'Undo a previous block of an invoice'
    displayName: 'UnblockInvoices'
    method: 'POST'
    urlTemplate: '/unblock'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
