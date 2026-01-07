namespace DMARCReportAnalyzer.Database.DTO
{
    public record Metadata
    {
        public string FeedbackId { get; init; }
        public string ReportId { get; init; }
        public string Organization { get; init; }
        public string Email { get; init; }
        public string? ExtraContactInfo { get; init; }
        public DateTime ReportBegin { get; init; }
        public DateTime ReportEnd { get; init; }
        public string? Errors { get; init; }
        public string? Generator { get; init; }

        public Metadata(string feedbackId, string reportId, string organization, string email, string? extraContactInfo, DateTime reportBegin, DateTime reportEnd, string? errors, string? generator)
        {
            this.FeedbackId = feedbackId;
            this.ReportId = reportId;
            this.Organization = organization;
            this.Email = email;
            this.ExtraContactInfo = extraContactInfo;
            this.ReportBegin = reportBegin;
            this.ReportEnd = reportEnd;
            this.Errors = errors;
            this.Generator = generator;
        }
    }
}
