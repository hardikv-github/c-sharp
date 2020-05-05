
//Tracing in Plugins
ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
tracingService.Trace("Start");


//Tracing in Workflow
ITracingService tracingService = executionContext.GetExtension<ITracingService>();
tracingService.Trace("Start");

