param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/DeleteInvoiceDocs.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/DeleteInvoiceDocs'
  properties:{
    description: 'endpoint for deleting invoices'
    displayName: 'DeleteInvoiceDocs'
    method: 'DELETE'
    urlTemplate: '/docs-soft-delete-by-doc-ids'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
