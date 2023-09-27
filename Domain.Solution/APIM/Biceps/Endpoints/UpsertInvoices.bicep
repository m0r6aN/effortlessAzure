param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/UpsertInvoices.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/UpsertInvoices'
  properties:{
    description: 'Upsert an invoice'
    displayName: 'UpsertInvoices'
    method: 'POST'
    urlTemplate: '/invoice-upsert'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
