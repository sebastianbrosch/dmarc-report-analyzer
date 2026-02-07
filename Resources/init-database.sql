CREATE TABLE IF NOT EXISTS feedback (
  id VARCHAR(32) NOT NULL,
  created DATETIME NOT NULL,
  data TEXT NOT NULL,
  message_id VARCHAR(255) NULL,
  received DATETIME NULL,
  sender VARCHAR(255) NULL,
  version VARCHAR(10) NULL,
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS file (
  feedback_id VARCHAR(32) NOT NULL,
  receiver VARCHAR(255) NOT NULL,
  domain VARCHAR(255) NOT NULL,
  report_begin DATETIME NOT NULL,
  report_end DATETIME NOT NULL,
  unique_id VARCHAR(255) NULL,
  PRIMARY KEY (feedback_id),
  FOREIGN KEY (feedback_id) REFERENCES feedback(id)  
);

CREATE TABLE IF NOT EXISTS metadata (
  feedback_id VARCHAR(32) NOT NULL,
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
);

CREATE TABLE IF NOT EXISTS policy_published (
  feedback_id VARCHAR(32) NOT NULL,
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
);

CREATE TABLE IF NOT EXISTS record (
  id VARCHAR(32) NOT NULL,
  feedback_id VARCHAR(32) NOT NULL,
  source_ip VARCHAR(39) NOT NULL,
  count INTEGER NOT NULL,
  envelope_to VARCHAR(255) NULL,
  envelope_from VARCHAR(255) NULL,
  header_from VARCHAR(255) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (feedback_id) REFERENCES feedback(id)
);

CREATE TABLE IF NOT EXISTS policy_evaluated (
  record_id VARCHAR(32) NOT NULL,
  disposition VARCHAR(10) NOT NULL,
  dkim VARCHAR(4) NOT NULL,
  spf VARCHAR(4) NOT NULL,
  FOREIGN KEY (record_id) REFERENCES record(id)
);

CREATE TABLE IF NOT EXISTS reason (
  record_id VARCHAR(32) NOT NULL,
  type VARCHAR(17) NOT NULL,
  comment VARCHAR(255) NULL,
  FOREIGN KEY (record_id) REFERENCES record(id)
);

CREATE TABLE IF NOT EXISTS auth_result_dkim (
  record_id VARCHAR(32) NOT NULL,
  domain VARCHAR(255) NOT NULL,
  selector VARCHAR(255) NULL,
  result VARCHAR(9) NOT NULL,
  human_result VARCHAR(255) NULL,
  FOREIGN KEY (record_id) REFERENCES record(id)
);

CREATE TABLE IF NOT EXISTS auth_result_spf (
  record_id VARCHAR(32) NOT NULL,
  domain VARCHAR(255) NOT NULL,
  scope VARCHAR(5) NULL,
  result VARCHAR(9) NOT NULL,
  human_result VARCHAR(255) NULL,
  FOREIGN KEY (record_id) REFERENCES record(id)
);

CREATE TABLE IF NOT EXISTS mailbox (
  id VARCHAR(32) NOT NULL,
  name VARCHAR(60) NOT NULL,
  server VARCHAR(100) NOT NULL,
  port INTEGER NOT NULL,
  username VARCHAR(100) NOT NULL,
  encryption TINYINT NOT NULL,
  source VARCHAR(100) NOT NULL,
  archive VARCHAR(100) NULL,
  mark_as_read BIT NOT NULL DEFAULT 0,
  delete_message BIT NOT NULL DEFAULT 0,
  PRIMARY KEY (id)
);

CREATE VIEW metadata_expansion
AS
SELECT feedback_id,
  CASE
    WHEN DATE(report_begin) = DATE(report_end) THEN
      DATE(report_begin)
    WHEN CAST(strftime('%H', report_begin) AS INTEGER) = 0 AND CAST(strftime('%H', report_end) AS INTEGER) = 0 THEN
      DATE(report_end)
    WHEN CAST(strftime('%H', report_begin) AS INTEGER) >= 19 THEN
      DATE(report_end)
    WHEN CAST(strftime('%H', report_begin) AS INTEGER) < 19 THEN
      DATE(report_begin)
  ELSE
    NULL
  END report_date
FROM metadata;