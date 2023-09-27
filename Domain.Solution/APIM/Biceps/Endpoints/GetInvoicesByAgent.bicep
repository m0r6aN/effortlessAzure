param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/InvoicesByAgent.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetInvoicesByAgent'
  properties:{
    description: 'endpoint for getting invoices by agent id'
    displayName: 'GetInvoicesByAgent'
    method: 'GET'
    urlTemplate: '/invoices-by-agent'
    request:{
      queryParameters:[
        {
          name: 'agentPkeys'
          type: 'string'
          required: true
        }
        {
          name: 'statoos'
          type: 'string'
          required: true
        }
        {
          name: 'skip'
          type: 'int'
          required: true
        }
         {
          name: 'take'
          type: 'int'
          required: true
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