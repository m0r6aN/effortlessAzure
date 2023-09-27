param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostUpdateOpsInvoiceDetails.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostUpdateOpsInvoiceDetails'
  properties:{
    description: 'update invoice details for f2'
    displayName: 'PostUpdateOpsInvoiceDetails'
    method: 'POST'
    urlTemplate: '/ops-invoice-details'
    request: {
      representations: [
        {
          contentType: 'application/json'
          typeName: 'UpdateOpsInvoiceDetails'
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
