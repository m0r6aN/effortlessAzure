param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/BasePolicy.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostInvoiceAssigneeByPKey'
  properties:{
    description: 'update invoice assignee by invoice pkey'
    displayName: 'PostInvoiceAssigneeByPKey'
    method: 'POST'
    urlTemplate: '/invoice/{invoice_pkey}'
    templateParameters:[
      {
        name: 'invoice_pkey'
        type: 'string'
      }
    ]
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
