
param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetInvoiceDetailNotes.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetInvoiceDetailNotes'
  properties:{
    description: 'get invoice detail notes for f2'
    displayName: 'GetInvoiceDetailNotes'
    method: 'GET'
    urlTemplate: '/invoice-detail-notes'
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
