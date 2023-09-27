param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/BasePolicy.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetVendorPayableBySchedule'
  properties:{
    description: 'Retrieve endorPayable by schedule pkey'
    displayName: 'GetVendorPayableBySchedule'
    method: 'GET'
    urlTemplate: '/vendorPayable-by-schedule'
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