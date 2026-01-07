namespace DMARCReportAnalyzer.Database.DTO
{
    public record PolicyEvaluated
    {
        public string RecordId { get; init; }
        public string Disposition { get; init; }
        public string DKIM { get; init; }
        public string SPF { get; init; }

        public PolicyEvaluated(string recordId, string disposition, string dkim, string spf)
        {
            this.RecordId = recordId;
            this.Disposition = disposition;
            this.DKIM = dkim;
            this.SPF = spf;
        }
    }
}
