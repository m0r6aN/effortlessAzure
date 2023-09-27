﻿//********************************************************************************************
// <a href="www.google.com">OTRS:1</a> - All using statements should reside in a Global.cs file in the project/src folder
// OTRS:2 - All using statements should be sorted alphabetically (use the CodeMaid extension)
//********************************************************************************************

//***********************************************************************
// 3RD PARTY (SEPERATED BY TOP-LEVEL NAMESPACE AND SORTED ALPHABETICALLY)
//***********************************************************************
global using Xunit;

//********************************
// OTR LIBRARIES AND PROJECTS
//********************************

//********************************
// INTERNAL (THIS ASSEMBLY)
//********************************

//********************************
// ALIASES (FOR CLARITY)
//********************************

// use System.Text.Json instead of NewtonSoft. It is more efficient than NewtonSoft as it is faster,
// produces smaller payloads, and is simpler to use.