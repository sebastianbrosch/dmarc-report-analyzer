namespace DMARCReportAnalyzer.Database.DTO
{
    public record Record
    {
        public string Id { get; init; }
        public string FeedbackId { get; init; }
        public string SourceIp { get; init; }
        public int Count { get; init; }
        public string? EnvelopeTo { get; init; }
        public string? EnvelopeFrom { get; init; }
        public string HeaderFrom { get; init; }

        public Record(string id, string feedbackId, string sourceIp, int count, string? envelopeTo, string? envelopeFrom, string headerFrom)
        {
            this.Id = id;
            this.FeedbackId = feedbackId;
            this.SourceIp = sourceIp;
            this.Count = count;
            this.EnvelopeTo = envelopeTo;
            this.EnvelopeFrom = envelopeFrom;
            this.HeaderFrom = headerFrom;
        }
    }
}
