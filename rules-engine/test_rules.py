import sqlite3
import unittest

from rules import SCHEMA, Email, Report, run

DATA = """
CREATE TABLE person (email TEXT, name TEXT, registered TEXT, invited TEXT);
INSERT INTO person VALUES
  ('ana@example.org', 'Ana', date('now', '-5 days'), NULL),
  ('ben@example.org', 'Ben', date('now', '-2 days'), NULL),
  ('cy@example.org',  'Cy',  NULL, date('now', '-9 days'));

INSERT INTO query VALUES
  (1, 'registered N days ago',
      'SELECT email, name FROM person WHERE registered = date(''now'', ''-'' || :days || '' days'')'),
  (2, 'invited, not registered, for N days',
      'SELECT email FROM person WHERE registered IS NULL AND invited <= date(''now'', ''-'' || :days || '' days'')');

INSERT INTO rule VALUES
  (1, 'welcome check-in', 1, 'How is it going, $name?', 'You joined $days days ago.', 1),
  (2, 'invite reminder', 2, 'Your invitation is waiting', 'Sign up at $link.', 1),
  (3, 'switched off', 1, 'never', 'never', 0);
INSERT INTO rule_param VALUES (1, 'days', '5'), (2, 'days', '7'), (2, 'link', 'https://example.org/join'), (3, 'days', '5');
INSERT INTO rule_tag VALUES (1, 'onboarding'), (2, 'invites'), (3, 'onboarding');
"""


class RulesTest(unittest.TestCase):
    def setUp(self):
        self.db = sqlite3.connect(":memory:")
        self.db.executescript(SCHEMA + DATA)
        self.sent = []

    def test_every_active_rule_sends_one_email_per_matching_row(self):
        report = run(self.db, self.sent.append)
        self.assertEqual(report, Report(rules=2, sent=2))
        self.assertEqual(self.sent, [
            Email("ana@example.org", "How is it going, Ana?", "You joined 5 days ago."),
            Email("cy@example.org", "Your invitation is waiting", "Sign up at https://example.org/join."),
        ])

    def test_a_tag_runs_only_its_rules(self):
        run(self.db, self.sent.append, tag="invites")
        self.assertEqual([e.to for e in self.sent], ["cy@example.org"])

    def test_a_new_rule_is_rows_not_code(self):
        self.db.executescript("""
            INSERT INTO rule VALUES (4, 'second-day nudge', 1, 'Day $days', 'Hi $name', 1);
            INSERT INTO rule_param VALUES (4, 'days', '2');""")
        run(self.db, self.sent.append)
        self.assertIn(Email("ben@example.org", "Day 2", "Hi Ben"), self.sent)

    def test_a_stored_query_cannot_write(self):
        self.db.execute("UPDATE query SET sql = 'DELETE FROM person' WHERE id = 1")
        with self.assertRaises(sqlite3.DatabaseError):
            run(self.db, self.sent.append, tag="onboarding")
        self.assertEqual(self.db.execute("SELECT count(*) FROM person").fetchone(), (3,))

    def test_a_placeholder_with_no_value_fails_loudly(self):
        self.db.execute("UPDATE rule SET body = 'Hello $nickname' WHERE id = 1")
        with self.assertRaises(KeyError):
            run(self.db, self.sent.append, tag="onboarding")


if __name__ == "__main__":
    unittest.main()
