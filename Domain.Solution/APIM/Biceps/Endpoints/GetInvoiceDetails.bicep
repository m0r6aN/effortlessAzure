param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetInvoiceDetails.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetInvoiceDetails'
  properties:{
    description: 'get invoice details for f2'
    displayName: 'GetInvoiceDetails'
    method: 'GET'
    urlTemplate: '/invoice-details'
    request:{
      queryParameters:[
        {
          name: 'invoicePkey'
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
