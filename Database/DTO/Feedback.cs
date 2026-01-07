namespace DMARCReportAnalyzer.Database.DTO
{
    public record Feedback
    {
        public string Id { get; init; }
        public string Data { get; init; }
        public DateTime Created { get; init; }
        public string? Sender { get; init; }
        public DateTime? Received { get; init; }
        public string? Version { get; init; }

        public Feedback(string id, string data, DateTime created, string? sender, DateTime? received, string? version)
        {
            this.Id = id;
            this.Data = data;
            this.Created = created;
            this.Sender = sender;
            this.Received = received;
            this.Version = version;
        }
    }
}
