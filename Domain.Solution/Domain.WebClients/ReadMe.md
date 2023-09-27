# Introduction 
WebClient project of Schedule micro services. 
WebClient project holds:
	Response classes, Request classes, DTOs;
	URL services of all endpoints;
	Web services to make all to Schedules MS endpoints;

#Implementation
To use web clients:
1. Download OTR.Serverless.Schedules Nuget from OTR nuget feeds
2. Register WebClient in domain project with line: 
		SchedulesServiceCollection.RegisterSchedules(services); services => IServiceCollection
3. Call WebClients endpoint method.
		Example: var result = await _schedulesWebClient.GetScheduleByPKey(schedulePKey);

#Publish Nuget
Through pipeline. No extra work needed.

# Getting Started

# Build and Test

# Contribute

