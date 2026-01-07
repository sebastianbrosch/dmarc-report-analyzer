namespace DMARCReportAnalyzer.Database.DTO
{
    public record AuthResultDKIM
    {
        public string RecordId { get; init; }

        public string Domain { get; init; }

        public string? Selector { get; init; }

        public string Result { get; init; }

        public string? HumanResult { get; init; }

        public AuthResultDKIM(string recordId, string domain, string? selector, string result, string? humanResult)
        {
            this.RecordId = recordId;
            this.Domain = domain;
            this.Selector = selector;
            this.Result = result;
            this.HumanResult = humanResult;
        }
    }
}
