param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetInvoices.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetInvoices'
  properties:{
    description: 'Retrieve invoicesby invoice pkey'
    displayName: 'GetInvoices'
    method: 'GET'
    urlTemplate: '/invoice-data'
    request:{
      queryParameters:[
        {
          name: 'invoicePkeys'
          type: 'string'
          required: true
        }
        {
          name: 'includeItems'
          type: 'bool'
          required: true
        }
        {
          name: 'showCarrierItems'
          type: 'bool'
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