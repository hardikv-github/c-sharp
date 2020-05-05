
//Tracing in Plugins
ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
tracingService.Trace("Start");

