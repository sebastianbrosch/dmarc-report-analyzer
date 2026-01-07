namespace DMARCReportAnalyzer.Database.DTO
{
    public record AuthResultSPF
    {
        public string RecordId { get; init; }

        public string Domain { get; init; }
        public string? Scope { get; init; }
        public string Result { get; init; }
        public string? HumanResult { get; init; }

        public AuthResultSPF(string recordId, string domain, string? scope, string result, string? humanResult)
        {
            this.RecordId = recordId;
            this.Domain = domain;
            this.Scope = scope;
            this.Result = result;
            this.HumanResult = humanResult;
        }
    }
}
