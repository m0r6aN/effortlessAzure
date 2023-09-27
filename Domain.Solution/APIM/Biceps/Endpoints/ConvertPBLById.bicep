param environment string
param apiName string
param service string
//Edit the xml file
var raw = loadTextContent('./XML/ConvertPBLById.xml')
var policy = replace(raw, '{1}', environment)

resource endpoint 'Microsoft.ApiManagement/service/apis/operations@2021-12-01-preview' = {
  name: '${service}/${apiName}/ConvertPrebuiltLoadsByIds'
  properties: {
    description: 'Convert PreBuilt Load by its ID Number'
    displayName: 'ConvertPrebuiltLoadsByIds'
    method: 'PATCH'
    urlTemplate: '/loads'
    request: {
      representations: [
        {
          contentType: 'application/json'
          typeName: 'PrebuiltLoadDTO'
          schemaId: 'InvoicesSchemas'
          examples: {
            default: {
              value: {
                PostingId: '1MpF43XQzPJM'
                PartnerCarrierId: '397091d4-ff40-4090-a5d8-01a8583444f0'
                OriginCity: 'South Jeramy'
                OriginState: 'MN'
                OriginZip: 'n/a'
                DestinationCity: 'Ornville'
                DestinationState: 'MN'
                DestinationZip: 'n/a'
                Rate: json('500.50')
                InvoicePkey: 2
                Fees: [
                  {
                    PKey: 1
                    FeeType: 'LUMPER'
                    Description: 'fee description'
                    Amount: json('10.50')
                  }
                  {
                    PKey: 2
                    FeeType: 'FUEL-ADVANCE'
                    Description: 'fee2 description'
                    Amount: json('-12.00')
                  }
                ]
              }
            }
          }
        }
      ]
    }
    responses: [
      {
        statusCode: 200
        representations: [
          {
            contentType: 'application/json'
            typeName: 'NumberOfRowsAffected'
            schemaId: 'InvoicesSchemas'
            examples: {
              default: {
                value: 3
              }
            }
          }
        ]
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
  properties: {
    value: policy
  }
}
