
param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetIsFirstFactored.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetIsFirstFactored'
  properties:{
    description: 'get whether an invoice has been factored between client, customer combo'
    displayName: 'GetIsFirstFactored'
    method: 'GET'
    urlTemplate: '/is-first-facotred'
    request:{
      queryParameters:[
        {
          name: 'clientPKey'
          type: 'int'
          required: true
        }
        {
          name: 'customerPKey'
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
