namespace DomainName.Domain.Value.Constant
{
    internal static class FunctionEvents
    {
        public static string DomainNameFunctionInvalidRequest { get; set; } = "DOMAINNAME_FUNCTION_INAVALID_REQUEST";
        public static string DomainNameFunctionRequestCompleted { get; set; } = "DOMAINNAME_FUNCTION_REQUEST_COMPLETED";
        public static string DomainNameFunctionRequestStarted { get; set; } = "DOMAINNAME_FUNCTION_REQUEST_STARTED";
        public static string OperationCanceled { get; set; } = "OPERAION_CANCELED";
        public static string KnownExceptionOccured { get; set; } = "KNOWN_EXCEPTION";
        public static string UnknownExceptionOccured { get; set; } = "UNKNOWN_EXCEPTION";
    }
}