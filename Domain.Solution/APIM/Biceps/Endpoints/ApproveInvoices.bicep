param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/ApproveInvoices.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/ApproveInvoices'
  properties:{
    description: 'endpoint for approve all invoices'
    displayName: 'ApproveInvoices'
    method: 'POST'
    urlTemplate: '/approve'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
