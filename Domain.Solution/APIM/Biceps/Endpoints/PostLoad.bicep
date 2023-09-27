param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/PostLoad.xml')
var policy = replace(raw, '{1}', environment)



resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/PostLoad'
  properties:{
    description: 'Create Shipment Load Record.   Loads function as a precursor to invoices in Factorhawk.'
    displayName: 'PostLoad'
    method: 'POST'
    urlTemplate: '/load'
    request: {
      representations: [
        {
          contentType: 'application/json'
          typeName: 'ShipmentDetails'
          schemaId: 'InvoicesSchemas'
        }
      ]
  }
  responses: [
    {
      statusCode: 200
    }
    {
        statusCode: 201
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
