param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/GetRecentPrebuiltLoadsByMcNumbers.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/GetRecentPrebuiltLoadsByMcNumbers'
  properties:{
    description: 'Gets recent loads by client and customer MC numbers. GET request parameters: clientMcNumber, customerMcNumber, customerName, skip, take, sortSelector, sortDescending'
    displayName: 'GetRecentPrebuiltLoadsByMcNumbers'
    method: 'GET'
    urlTemplate: '/recent-prebuilt-loads-by-mcNumbers'
    request: {
      queryParameters: [
        {
          name: 'clientMcNumber'
          type: 'string'
          required: true
        }
        {
          name: 'customerMcNumber'
          type: 'string'
          required: true
        }
        {
          name: 'skip'
          type: 'integer'
          required: false
        }
        {
          name: 'take'
          type: 'integer'
          required: false
        }
        {
          name: 'sortSelector'
          type: 'string'
          values: [
            'Origin'
            'Destination'
            'EarliestPickUpDate'
          ]
          required: false
        }
        {
          name: 'sortDecending'
          type: 'boolean'
          required: false
        }
      ]
    }
    responses: [
        {
            statusCode: 200
        }
        {
            statusCode: 400
        }
        {
            statusCode: 500
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
