//Parameters start
param apiName string
param apimName string
param env string
param backendName string
param backendUrl string
param nv string
param newOrExisting string = 'new'
param nvKey string
param version string
param slot string
//Parameters end
var raw = loadTextContent('./Endpoints/XML/AllEndpoints.xml')
var policy = replace(raw, '{slot}', slot)

//resources begin
resource AzureAPIm 'Microsoft.ApiManagement/service@2021-12-01-preview' existing = {
  name: apimName
}

resource apiVersion 'Microsoft.ApiManagement/service/apiVersionSets@2021-12-01-preview' = {
  name: 'invoices'
  parent: AzureAPIm
  properties:{
    displayName: 'invoices'
    versioningScheme: 'Segment'
  }
}

resource AzureApi 'Microsoft.ApiManagement/service/apis@2021-12-01-preview'= {
  name: '${apiName}${version}'
  parent: AzureAPIm
  properties: {
    apiType: 'http'
    apiVersion: version
    apiVersionSet: apiVersion
    apiVersionSetId: apiVersion.id
    displayName: 'invoices'
    path: 'invoices'
    format: 'openapi'
    protocols: [
      'https'
    ]
    type: 'http'
    subscriptionKeyParameterNames: {
      header: 'Ocp-Apim-Subscription-Key'
      query: 'subscription-key'
    }
  } 
}
resource backend 'Microsoft.ApiManagement/service/backends@2021-12-01-preview' = if(newOrExisting == 'new') {
  name: backendName
  parent: AzureAPIm
  properties:{
    description: 'Function app that runs the invoices microservice'
    protocol: 'http'
    url: backendUrl
    resourceId: '${environment().authentication.audiences[1]}${resourceId('Microsoft.Web/sites', backendName)}'
    credentials:{
      header:{
        'x-functions-key':[
          '{{${nv}}}'
        ] 
      }
    }
  }
  dependsOn:[
    namedValue
  ]
}

resource namedValue 'Microsoft.ApiManagement/service/namedValues@2021-12-01-preview' = if(newOrExisting == 'new') {
  name: nv
  parent: AzureAPIm
  properties:{
    displayName: '${env}-factorhawk-invoices-func-eastus-key'
    secret: true
    value: nvKey
    tags:[
      'key'
      'function'
      'auto'
    ]
  }
}

resource allEndpointPolicy 'Microsoft.ApiManagement/service/apis/policies@2021-12-01-preview' = {
  name: 'policy'
  parent: AzureApi
  properties:{
    format: 'rawxml'
    value: policy
  }
}
//resources end

//Modules start
module approveInvoices 'Endpoints/ApproveInvoices.bicep' = {
  name: 'approve'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module blockInvoices 'Endpoints/BlockInvoices.bicep' = {
  name: 'block'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module deleteDocs 'Endpoints/DeleteInvoiceDocs.bicep' = {
  name: 'docs-soft-delete-by-doc-ids'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module Load 'Endpoints/PostLoad.bicep' = {
  name: 'load'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
    schemas
  ]
}
module GetRecentPrebuiltLoadsByMcNumbers 'Endpoints/GetRecentPrebuiltLoadsByMcNumbers.bicep' = {
  name: 'GetRecentPrebuiltLoadsByMcNumbers'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module convertPBL 'Endpoints/ConvertPBLById.bicep' = {
  name: 'ConvertPrebuiltLoadsByIds'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
    schemas
  ]
}
module unblockInvoices  'Endpoints/UnblockInvoices.bicep' = {
  name: 'unblock'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module upsertInvoices  'Endpoints/UpsertInvoices.bicep' = {
  name: 'invoice-upsert'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module smartVerification  'Endpoints/GetSmartVerification.bicep' = {
  name: 'smart-verification-score'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module invoicesByAgent  'Endpoints/GetInvoicesByAgent.bicep' = {
  name: 'invoices-by-agent'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module invoicesByClientId  'Endpoints/GetInvoicesByClientId.bicep' = {
  name: 'invoices'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetCreditCheckF2 'Endpoints/GetInvoices.bicep' = {
  name: 'GetInvoices'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn: [
    backend
    AzureApi
  ]
}
module docByPath  'Endpoints/GetDocByPath.bicep' = {
  name: 'document-by-path'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module docsByIds  'Endpoints/GetDocsByDocIds.bicep' = {
  name: 'docs-by-document-ids'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module pathById  'Endpoints/GetDocPathByDocId.bicep' = {
  name: 'doc-path-by-doc-id'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module docsByInvoiceIds  'Endpoints/GetDocsByInvoiceIds.bicep' = {
  name: 'docs-by-invoice-ids'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module PostAutoCheck  'Endpoints/PostAutomationCheck.bicep' = {
  name: 'PostAutomationCheck'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetAutoCheck  'Endpoints/GetAutomationCheck.bicep' = {
  name: 'GetAutomationCheck'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module IntegrationInvoiceDetails  'Endpoints/ConfirmLoad.bicep' = {
  name: 'ConfirmLoad'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetInvoiceDetails  'Endpoints/GetInvoiceDetails.bicep' = {
  name: 'GetInvoiceDetails'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetInvoiceDetailNotes  'Endpoints/GetInvoiceDetailNotes.bicep' = {
  name: 'GetInvoiceDetailNotes'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetIsFirstFactored  'Endpoints/GetIsFirstFactored.bicep' = {
  name: 'GetIsFirstFactored'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module PostUpdateOpsInvoiceDetails  'Endpoints/PostUpdateOpsInvoiceDetails.bicep' = {
  name: 'PostUpdateInvoiceDetails'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module PostSearchInvoicesByAgent  'Endpoints/PostSearchInvoicesByAgent.bicep' = {
  name: 'PostSearchInvoicesByAgent'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetInvoiceTermsList  'Endpoints/GetInvoiceTermsList.bicep' = {
  name: 'GetInvoiceTermsList'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module PostAddInvoiceNote  'Endpoints/PostAddInvoiceNote.bicep' = {
  name: 'PostAddInvoiceNote'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetHeartbeat 'Endpoints/Getheartbeat.bicep' = {
  name: 'GetHeartbeat'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module PostInvoiceIngest 'Endpoints/PostInvoiceIngest.bicep' = {
  name: 'PostInvoiceIngest'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetArInvoicesBySchedule 'Endpoints/GetArInvoicesBySchedule.bicep' = {
  name: 'GetArInvoicesBySchedule'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetVendorPayableBySchedule 'Endpoints/GetVendorPayableBySchedule.bicep' = {
  name: 'GetVendorPayableBySchedule'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetInvoicesBySchedule 'Endpoints/GetInvoicesBySchedule.bicep' = {
  name: 'GetInvoicesBySchedule'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module GetInvoiceStatuses 'Endpoints/GetInvoiceStatuses.bicep' = {
  name: 'GetInvoiceStatuses'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
module schemas 'Endpoints/Schema.bicep' = {
  name: 'InvoicesSchemas'
  params:{
    service: apimName
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    AzureApi
  ]
}
module PostInvoiceAssigneeByPkey 'Endpoints/PostInvoiceAssigneeByPKey.bicep' = {
  name: 'PostInvoiceAssigneeByPkey'
  params: {
    service: apimName
    environment: env
    apiName: '${apiName}${version}'
  }
  dependsOn:[
    backend
    AzureApi
  ]
}
//Modules end
