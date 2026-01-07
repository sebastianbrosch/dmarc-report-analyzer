namespace DMARCReportAnalyzer.Database.DTO
{
    public record PolicyPublished
    {
        public string FeedbackId { get; init; }
        public string Domain { get; init; }
        public string? AlignmentDKIM { get; init; }
        public string? AlignmentSPF { get; init; }
        public string Policy { get; init; }
        public string? SubPolicy { get; init; }
        public string? NonExistentPolicy { get; init; }
        public int? Percentage { get; init; }
        public string? DiscoveryMethod { get; init; }
        public string? FailureOptions { get; init; }
        public string? Testing { get; init; }

        public PolicyPublished(string feedbackId, string domain, string? alignmentDKIM, string? alignmentSPF, string policy, string? subPolicy, string? nonExistentPolicy, int? percentage, string? dicoveryMethod, string? failureOptions, string? testing)
        {
            this.FeedbackId = feedbackId;
            this.Domain = domain;
            this.AlignmentDKIM = alignmentDKIM;
            this.AlignmentSPF = alignmentSPF;
            this.Policy = policy;
            this.SubPolicy = subPolicy;
            this.NonExistentPolicy = nonExistentPolicy;
            this.Percentage = percentage;
            this.DiscoveryMethod = dicoveryMethod;
            this.FailureOptions = failureOptions;
            this.Testing = testing;
        }
    }
}
