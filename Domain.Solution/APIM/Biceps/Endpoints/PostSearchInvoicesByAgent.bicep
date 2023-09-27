param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostSearchInvoicesByAgent.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostSearchInvoicesByAgent'
  properties:{
    description: 'search invoices by agent for f2'
    displayName: 'PostSearchInvoicesByAgent'
    method: 'POST'
    urlTemplate: '/invoices-by-agent-search'
    request: {
      queryParameters:[
        {
          name: 'skip'
          type: 'int'
          required: false
        }
        {
          name: 'take'
          type: 'int'
          required: false
        }
      ]
      representations: [
        {
          contentType: 'application/json'
          typeName: 'PostSearchInvoicesByAgent'
          schemaId: 'InvoicesSchemas'
        }
      ]
    }
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
