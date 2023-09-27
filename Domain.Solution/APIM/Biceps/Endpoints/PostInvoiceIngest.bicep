param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostInvoiceIngest.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostInvoiceIngest'
  properties:{
    description: 'Upsert an invoice from the Camunda ingest process'
    displayName: 'PostInvoiceIngest'
    method: 'POST'
    urlTemplate: '/invoice-ingest'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
