param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostAddInvoiceNote.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostAddInvoiceNote'
  properties:{
    description: 'Add Invoice Note'
    displayName: 'PostAddInvoiceNote'
    method: 'POST'
    urlTemplate: '/add-note'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
