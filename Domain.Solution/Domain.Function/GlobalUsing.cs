//********************************************************************************************
// BP:1 - All using statements should reside in a Global.cs file in the project root.

// BP:2 - All using statements should be sorted alphabetically (use the CodeMaid extension)
//********************************************************************************************

//************
// 3RD PARTY
//************
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Net.Http;
global using System.Net;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using System.Text;
global using System.Threading.Tasks;
global using System.Threading;
global using Microsoft.Azure.Functions.Worker.Http;
global using Microsoft.Azure.Functions.Worker.Middleware;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;

global using Newtonsoft.Json;
global using Quickwire.Attributes;

//********************************
// INTERNAL (THIS ASSEMBLY)
//********************************
global using DomainName.Domain.Value.Constant;
global using DomainName.Function.Domain.Repository.API;
global using DomainName.Function.Domain.Value.Request;
global using DomainName.Function.Domain.Value.Response;
global using DomainName.Function.Services.Get.API;
global using DomainName.Interface.Repository.API;
global using DomainName.Interface.Value.Request;
global using DomainName.Interface.Value.Response;
global using DomainName.Utility.Exception;
global using DomainName.Utility.Helpers;
global using DomainName.Utility.Logging;
global using DomainName.Function.Domain.Repository.DB;

//********************************
// PROPRIETARY LIBRARIES AND PROJECTS
//********************************
//global using MF.Libraries.Data.Tableau.Entity;
global using MF.Libraries.Data.Tableau.Context;
global using MF.Libraries.Data.Tableau.Entity;

//********************************
// ALIASES (FOR CLARITY)
//********************************
global using JsonSerializer = System.Text.Json.JsonSerializer;

// BP:3 -Use System.Text.Json instead of NewtonSoft. It is more efficient than NewtonSoft as it is
// faster, produces smaller payloads, and is simpler to use.