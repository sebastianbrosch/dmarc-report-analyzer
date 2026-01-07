namespace DMARCReportAnalyzer.Database.DTO
{
    public record Reason
    {
        public string RecordId { get; init; }
        public string Type { get; init; }
        public string? Comment { get; init; }

        public Reason(string recordId, string type, string? comment)
        {
            this.RecordId = recordId;
            this.Type = type;
            this.Comment = comment;
        }
    }
}
