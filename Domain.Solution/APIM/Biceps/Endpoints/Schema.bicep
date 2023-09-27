param service string
param apiName string

resource invoicesSchemas 'Microsoft.ApiManagement/service/apis/schemas@2021-12-01-preview' = {
  name: '${service}/${apiName}/InvoicesSchemas'
  properties: {
    contentType: 'application/vnd.oai.openapi.components+json'
    document: {
      components: {
        schemas: {
          ShipmentDetails: {
            type: 'object'
            required: [
              'PostingId'
              'EarliestPickup'
              'LatestPickup'
              'Lane'
              'BrokerInfo'
              'CarrierInfo'
            ]
            properties: {
              ShipmentId: {
                type: 'string'
                format: 'uuid'
              }
              PostingId: {
                type: 'string'
              }
              BrokerRefNumber: {
                type: 'string'
              }
              EarliestPickup: {
                type: 'string'
                format: 'date-time'
              }
              LatestPickup: {
                type: 'string'
                format: 'date-time'
              }
              Lane: {
                '$ref': '#/components/schemas/Lane'
              }
              Fees: {
                type: 'array'
                items: {
                  '$ref': '#/components/schemas/Fee'
                }
              }
              BrokerInfo: {
                type: 'object'
                required: [
                  'McNumber'
                ]
                properties: {
                  McNumber: {
                    type: 'string'
                  }
                  Name: {
                    type: 'string'
                  }
                  OfficeLocation: {
                    '$ref': '#/components/schemas/Address'
                  }
                }
              }
              CarrierInfo: {
                '$ref': '#/components/schemas/CarrierInfo'
              }
            }
          }
          Lane: {
            type: 'object'
            required: [
              'Origin'
              'Destination'
            ]
            properties: {
              Origin: {
                '$ref': '#/components/schemas/Address'
              }
              Destination: {
                '$ref': '#/components/schemas/Address'
              }
              NumberOfStops: {
                type: 'integer'
              }
              PostedRate: {
                type: 'number'
                format: 'float'
              }
              EquipmentType: {
                type: 'string'
              }
              Weight: {
                type: 'number'
                format: 'float'
              }
              Length: {
                type: 'number'
                format: 'float'
              }
              CommodityType: {
                type: 'string'
              }
            }
          }
          Address: {
            type: 'object'
            required: [
              'City'
              'State'
            ]
            properties: {
              Address1: {
                type: 'string'
              }
              Address2: {
                type: 'string'
              }
              City: {
                type: 'string'
              }
              State: {
                type: 'string'
              }
              Zip: {
                type: 'string'
              }
              Country: {
                type: 'string'
              }
              Latitude: {
                type: 'number'
                format: 'float'
              }
              Longitude: {
                type: 'number'
                format: 'float'
              }
            }
          }
          CarrierInfo: {
            type: 'object'
            required: [
              'CarrierId'
            ]
            properties: {
              CarrierId: {
                type: 'string'
                format: 'uuid'
              }
              Name: {
                type: 'string'
              }
              McNumber: {
                type: 'string'
              }
              DotNumber: {
                type: 'string'
              }
            }
          }
          Fee: {
            type: 'object'
            properties: {
              FeeType: {
                '$ref': '#/components/schemas/FeeType'
              }
              FeeAmount: {
                type: 'number'
              }
              Description: {
                type: 'string'
              }
            }
          }
          FeeType: {
            type: 'string'
            enum: [
              'LUMPER'
              'FUEL-ADVANCE'
              'DETENTION'
              'FEES'
              'ASSESORIAL'
              'DRAYAGE'
              'CHASSIS'
            ]
          }
          PostSearchInvoicesByAgent: {
            type: 'object'
            properties: {
              search: {
                type: 'string'
              }
              statuses: {
                type: 'array'
              }
              agentPkeys: {
                type: 'array'
              }
              sort: {
                type: 'array'
              }
            }
          }
          UpdateOpsInvoiceDetails: {
            type: 'object'
            required: [
              'PKey'
            ]
            properties: {
              PKey: {
                type: 'integer'
              }
              Status: {
                type: 'integer'
              }
              TermPKey: {
                type: 'integer'
              }
              InvoiceNo: {
                type: 'string'
              }
              InvoiceAmount: {
                type: 'number'
                format: 'double'
              }
              ClientPKey: {
                type: 'integer'
              }
              CustomerPKey: {
                type: 'integer'
              }
            }
          }
          PrebuiltLoadDTO: {
            type: 'object'
            required: [
              'PostingId'
              'OriginCity'
              'OriginState'
              'DestinationCity'
              'DestinationState'
            ]
            properties: {
              PostingId: {
                type: 'string'
              }
              PartnerCarrierId: {
                type: 'string'
              }
              OriginCity: {
                type: 'string'
              }
              OriginState: {
                type: 'string'
              }
              OriginZip: {
                type: 'string'
              }
              DestinationCity: {
                type: 'string'
              }
              DestinationState: {
                type: 'string'
              }
              DestinationZip: {
                type: 'string'
              }
              EarliestPickupDate: {
                type: 'string'
                format: 'date-time'
              }
              BrokerRefNumber: {
                type: 'string'
              }
              Status: {
                type: 'string'
              }
              Rate: {
                type: 'number'
                format: 'double'
              }
              InvoicePkey: {
                type: 'integer'
              }
              Fees: {
                type: 'array'
                items: {
                  type: 'object'
                  properties: {
                    PKey: {
                      type: 'integer'
                    }
                    FeeType: {
                      '$ref': '#/components/schemas/FeeType'
                    }
                    Amount: {
                      type: 'number'
                    }
                    Description: {
                      type: 'string'
                    }
                  }
                }
              }
            }
          }
          ConfirmLoadResponse: {
            type: 'object'
            properties: {
              '$id': {
                type: 'string'
              }
              ShipmentId: {
                type: 'string'
                format: 'uuid'
              }
              PostingId: {
                type: 'string'
              }
              BrokerRefNumber: {
                type: 'string'
              }
              EarliestPickup: {
                type: 'string'
                format: 'date-time'
              }
              LatestPickup: {
                type: 'string'
                format: 'date-time'
              }
              OtrInvoiceStatus: {
                type: 'string'
                enum: [
                  'INCOMPLETE'
                  'PENDING'
                  'FACTORED'
                  'REJECTED'
                ]
              }
              InvoiceDetails: {
                type: 'object'
                properties: {
                  '$id': {
                    type: 'string'
                  }
                  OtrNumber: {
                    type: 'integer'
                    nullable: 'true'
                  }
                  PoNumber: {
                    type: 'string'
                    nullable: 'true'
                  }
                  CarrierInvoiceNumber: {
                    type: 'string'
                    nullable: 'true'
                  }
                  ShipDate: {
                    type: 'string'
                    format: 'date-time'
                  }
                  DeliveryDate: {
                    type: 'string'
                    format: 'date-time'
                  }
                  ActualRate: {
                    type: 'number'
                    format: 'decimal'
                    nullable: 'true'
                  }
                }
              }
              Lane: {
                type: 'object'
                properties: {
                  '$id': {
                    type: 'string'
                  }
                  Origin: {
                    type: 'object'
                    properties: {
                      '$id': {
                        type: 'string'
                      }
                      Address1: {
                        type: 'string'
                      }
                      Address2: {
                        type: 'string'
                      }
                      City: {
                        type: 'string'
                      }
                      State: {
                        type: 'string'
                      }
                      Zip: {
                        type: 'string'
                      }
                      Country: {
                        type: 'string'
                      }
                    }
                  }
                  Destination: {
                    type: 'object'
                    properties: {
                      '$id': {
                        type: 'string'
                      }
                      Address1: {
                        type: 'string'
                      }
                      Address2: {
                        type: 'string'
                      }
                      City: {
                        type: 'string'
                      }
                      State: {
                        type: 'string'
                      }
                      Zip: {
                        type: 'string'
                      }
                      Country: {
                        type: 'string'
                      }
                    }
                  }
                  NumberOfStops: {
                    type: 'integer'
                  }
                  PostedRate: {
                    type: 'number'
                    format: 'double'
                  }
                  EquipmentType: {
                    type: 'string'
                  }
                  CommodityType: {
                    type: 'string'
                  }
                  Length: {
                    type: 'number'
                    format: 'double'
                  }
                  Weight: {
                    type: 'number'
                    format: 'double'
                  }
                }
              }
              Fees: {
                type: 'array'
                items: {
                  type: 'object'
                  properties: {
                    FeeType: {
                      '$ref': '#/components/schemas/FeeType'
                    }
                    FeeAmount: {
                      type: 'number'
                    }
                    Description: {
                      type: 'string'
                    }
                    OtrNumber: {
                      type: 'integer'
                    }
                  }     
                }
              }
              BrokerInfo: {
                type: 'object'
                properties: {
                  '$id': {
                    type: 'string'
                  }
                  Name: {
                    type: 'string'
                  }
                  McNumber: {
                    type: 'string'
                  }
                }
              }
              CarrierInfo: {
                type: 'object'
                properties: {
                  '$id': {
                    type: 'string'
                  }
                  Name: {
                    type: 'string'
                  }
                  McNumber: {
                    type: 'string'
                  }
                  DotNumber: {
                    type: 'string'
                  }
                  CarrierId: {
                    type: 'string'
                    format: 'uuid'
                  }
                }
              }
            }
          }
          error: {
            required: [
              'code'
              'message'
            ]
            type: 'object'
            properties: {
              '$id': {
                type: 'string'
              }
              StatusCode: {
                type: 'number'
              }
              Message: {
                type: 'string'
              }
            }
          }
          NumberOfRowsAffected: {
            type: 'integer'
          }
        }
      }
    }
  }
}
