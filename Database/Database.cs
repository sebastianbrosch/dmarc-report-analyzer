using Dapper;
using System.Data;

namespace DMARCReportAnalyzer.Database
{
    public class Database
    {
        IDbConnection Connection;

        public Database(IDbConnection connection)
        {
            this.Connection = connection;
        }

        public void CreateFeedback(DTO.Feedback feedback)
        {
            Connection.Execute("INSERT INTO feedback (id, data, created, sender, received, version) VALUES (@Id, @Data, @Created, @Sender, @Received, @Version)", feedback);
        }

        public void CreateMetadata(DTO.Metadata metadata)
        {
            Connection.Execute("INSERT INTO metadata (feedback_id, report_id, organization, email, extra_contact_info, report_begin, report_end, errors, generator) VALUES (@FeedbackId, @ReportId, @Organization, @Email, @ExtraContactInfo, @ReportBegin, @ReportEnd, @Errors, @Generator)", metadata);
        }

        public void CreateAuthResultSPF(DTO.AuthResultSPF authResultSPF)
        {
            Connection.Execute("INSERT INTO auth_result_spf (record_id, domain, scope, result, human_result) VALUES (@RecordId, @Domain, @Scope, @Result, @HumanResult)", authResultSPF);
        }

        public void CreateAuthResultDKIM(DTO.AuthResultDKIM authResultDKIM)
        {
            Connection.Execute("INSERT INTO auth_result_dkim (record_id, domain, selector, result, human_result) VALUES (@RecordId, @Domain, @Selector, @Result, @HumanResult)", authResultDKIM);
        }

        public void CreatePolicyEvaluated(DTO.PolicyEvaluated policyEvaluated)
        {
            Connection.Execute("INSERT INTO policy_evaluated (record_id, disposition, dkim, spf) VALUES (@RecordId, @Disposition, @DKIM, @SPF)", policyEvaluated);
        }

        public void CreatePolicyPublished(DTO.PolicyPublished policyPublished)
        {
            Connection.Execute("INSERT INTO policy_published (feedback_id, domain, adkim, aspf, p, sp, np, pct, discovery_method, fo, testing) VALUES (@FeedbackId, @Domain, @AlignmentDKIM, @AlignmentSPF, @Policy, @SubPolicy, @NonExistentPolicy, @Percentage, @DiscoveryMethod, @FailureOptions, @Testing)", policyPublished);
        }

        public void CreateRecord(DTO.Record record)
        {
            Connection.Execute("INSERT INTO record (id, feedback_id, source_ip, count, envelope_to, envelope_from, header_from) VALUES (@Id, @FeedbackId, @SourceIp, @Count, @EnvelopeTo, @EnvelopeFrom, @HeaderFrom)", record);
        }

        public void CreateReason(DTO.Reason reason)
        {
            Connection.Execute("INSERT INTO reason (record_id, type, comment) VALUES (@FeedbackId, @Type, @Comment)", reason);
        }

        public void InitializeDatabase()
        {
            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS feedback (
                  id VARCHAR(36) NOT NULL,
                  data TEXT NOT NULL,
                  created DATETIME NOT NULL,
                  sender VARCHAR(255) NULL,
                  received DATETIME NULL,
                  version VARCHAR(10) NULL,
                  PRIMARY KEY (id)
              );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS metadata (
                  feedback_id VARCHAR(36) NOT NULL,
                  report_id VARCHAR(255) NOT NULL,
                  organization VARCHAR(255) NOT NULL,
                  email VARCHAR(255) NOT NULL,
                  extra_contact_info VARCHAR(255) NULL,
                  report_begin DATETIME NOT NULL,
                  report_end DATETIME NOT NULL,
                  errors VARCHAR(255) NULL,
                  generator VARCHAR(255) NULL,
                  PRIMARY KEY (feedback_id),
                  FOREIGN KEY (feedback_id) REFERENCES feedback(id)
              );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS policy_published (
                  feedback_id VARCHAR(36) NOT NULL,
                  domain VARCHAR(255) NOT NULL,
                  adkim VARCHAR(1) NULL,
                  aspf VARCHAR(1) NULL,
                  p VARCHAR(10) NOT NULL,
                  sp VARCHAR(10) NULL,
                  np VARCHAR(10) NULL,
                  pct INTEGER NULL,
                  discovery_method VARCHAR(8) NULL,
                  fo VARCHAR(255) NULL,
                  testing VARCHAR(1) NULL,
                  PRIMARY KEY (feedback_id),
                  FOREIGN KEY (feedback_id) REFERENCES feedback(id)
              );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS record (
                  id VARCHAR(36) NOT NULL,
                  feedback_id VARCHAR(36) NOT NULL,
                  source_ip VARCHAR(15) NOT NULL,
                  count INTEGER NOT NULL,
                  envelope_to VARCHAR(255) NULL,
                  envelope_from VARCHAR(255) NULL,
                  header_from VARCHAR(255) NOT NULL,
                  PRIMARY KEY (id),
                  FOREIGN KEY (feedback_id) REFERENCES feedback(id)
                );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS policy_evaluated (
                  record_id VARCHAR(36) NOT NULL,
                  disposition VARCHAR(10) NOT NULL,
                  dkim VARCHAR(4) NOT NULL,
                  spf VARCHAR(4) NOT NULL,
                  FOREIGN KEY (record_id) REFERENCES record(id)
                );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS reason (
                  record_id VARCHAR(36) NOT NULL,
                  type VARCHAR(17) NOT NULL,
                  comment VARCHAR(255) NULL,
                  FOREIGN KEY (record_id) REFERENCES record(id)
                );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS auth_result_dkim (
                  record_id VARCHAR(36) NOT NULL,
                  domain VARCHAR(255) NOT NULL,
                  selector VARCHAR(255) NULL,
                  result VARCHAR(9) NOT NULL,
                  human_result VARCHAR(255) NULL,
                  FOREIGN KEY (record_id) REFERENCES record(id)
                );"
            );

            Connection.Execute(
              @"CREATE TABLE IF NOT EXISTS auth_result_spf (
                  record_id VARCHAR(36) NOT NULL,
                  domain VARCHAR(255) NOT NULL,
                  scope VARCHAR(5) NULL,
                  result VARCHAR(9) NOT NULL,
                  human_result VARCHAR(255) NULL,
                  FOREIGN KEY (record_id) REFERENCES record(id)
                );"
            );
        }
    }
}
