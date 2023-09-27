param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetDocPathByDocId.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetDocPathByDocId'
  properties:{
    description: 'get doc path by its pkey'
    displayName: 'GetDocPathByDocId'
    method: 'GET'
    urlTemplate: '/doc-path-by-doc-id'
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties:{
    value: policy
  }
}
