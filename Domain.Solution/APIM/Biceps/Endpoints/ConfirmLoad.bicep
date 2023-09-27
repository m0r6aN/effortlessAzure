param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/ConfirmLoad.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/ConfirmLoad'
  properties: {
    description: 'Gets invoice details by postingId and carrierId'
    displayName: 'Confirm Load'
    method: 'GET'
    urlTemplate: '/load'
    request: {
      queryParameters: [
        {
          name: 'posting-id'
          type: 'string'
          required: true
        }
        {
          name: 'carrier-id'
          type: 'string'
          required: true
        }
      ]
    }
    responses: [
      {
        statusCode: 200
        representations: [
          {
            contentType: 'application/json'
            schemaId: 'InvoicesSchemas'
            typeName: 'ConfirmLoadResponse'
            examples: {
              default: {
                value: { 
                  '$id': '1'
                  ShipmentId: '2a1b67bb-f74a-4c48-9b30-954d2df45588'
                  PostingId: 'Test986871'
                  BrokerRefNumber: 'my-id-01'
                  EarliestPickup: '2022-05-05T10:05:03.861Z'
                  LatestPickup: '2022-05-05T10:05:03.861Z'
                  OtrInvoiceStatus: 'INCOMPLETE'
                  InvoiceDetails: {
                    '$id': '2'
                    OtrNumber: 123456
                    PoNumber: '1111111'
                    CarrierInvoiceNumber: '1111111'
                    ShipDate: '2022-05-05T10:05:03.861Z'
                    DeliveryDate: '2022-05-05T10:05:03.861Z'
                    ActualRate: json('500.56')
                  }
                  Lane: {
                    '$id': '3'
                    Origin: {
                      '$id': '4'
                      Address1: '2197 North State Street'
                      Address2: '7162 Highway 1'
                      City: 'Oklahoma City'
                      State: 'OK'
                      Zip: '30078'
                      Country: 'United States'
                    }
                    Destination: {
                      '$id': '5'
                      Address1: '615 SAINT GEORGE SQUARE COURT'
                      Address2: 'SUITE 300 #4002'
                      City: 'WINSTON SALEM'
                      State: 'NC'
                      Zip: '27103'
                      Country: 'United States'
                    }
                    PostedRate: json('600.56')
                    NumberOfStops: 10
                    EquipmentType: 'AC'
                    CommodityType: 'Fresh Produce'
                    Length: json('45.33')
                    Weight: json('525.25')
                  }
                  Fees: [ 
                    {
                      '$id': '6'
                      FeeType: 'LUMPER'
                      FeeAmount: 100
                      Description: 'Lumper fee'
                      OtrNumber: 12333
                    }
                  ]
                  BrokerInfo: {
                    '$id': '7'
                    McNumber: 'MC1395417'
                    Name: 'SD BROKERAGE INC'
                  }
                  CarrierInfo: {
                    '$id': '8'
                    CarrierId: '9596a36f-3dd2-4539-a972-0e56b898aae6'
                    McNumber: 'MC1111185'
                    DotNumber: '3419087'
                    Name: 'BH_TEST_C2'
                  }                  
                }
              }
            }
          }
        ]

      }
      {
        statusCode: 400
        representations: [
          {
            contentType: 'application/json'
            schemaId: 'InvoicesSchemas'
            typeName: 'error'
            examples: {
              default: {
                value: {
                  '$id': '1'
                  StatusCode: 400
                  Message: 'Missing parameter: postingId'
                }
              }
            }
          }
        ]
      }
      {
        statusCode: 401
        representations: [
          {
            contentType: 'application/json'
            schemaId: 'InvoicesSchemas'
            typeName: 'error'
            examples: {
              default: {
                value: {
                  '$id': '1'
                  StatusCode: 401
                  Message: 'Access denied due to missing subscription key. Make sure to include subscription key when making requests to an API.'
                }
              }
            }
          }
        ]
      }
      {
        statusCode: 500
        representations: [
          {
            contentType: 'application/json'
            schemaId: 'InvoicesSchemas'
            typeName: 'error'
            examples: {
              default: {
                value: {
                  '$id': '1'
                  StatusCode: 500
                  Message: 'Internal Server Error'
                }
              }
            }
          }
        ]
      }
    ]
  }
}

resource endpointPolicy 'Microsoft.ApiManagement/service/apis/operations/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: endpoint
  properties: {
    value: policy
  }
}
