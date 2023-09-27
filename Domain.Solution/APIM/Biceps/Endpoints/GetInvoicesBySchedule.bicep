param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/BasePolicy.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetInvoicesBySchedule'
  properties:{
    description: 'Retrieve invoices by schedule pkey'
    displayName: 'GetInvoicesBySchedule'
    method: 'GET'
    urlTemplate: '/invoices-by-schedule'
    request:{
      queryParameters:[
        {
          name: 'schedulePKey'
          type: 'string'
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