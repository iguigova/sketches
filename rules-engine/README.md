# rules-engine

Email notification rules kept as table rows, not code.

A rule says who to email and what to say:

- **query**: a stored SQL query that finds the people the rule is about.
  Each row it returns is one email, sent to the row's `email` column.
- **rule**: the query to use, a subject and body template, and an
  `active` switch.
- **rule_param**: the values bound into the query, such as `days = 5`.
  One query serves many rules: "registered 5 days ago" and "registered
  2 days ago" are two rules over one query.
- **rule_tag**: labels that group rules, so a scheduler can run one
  group at a time.

`run(db, send, tag)` runs the active rules, binds each rule's parameters
into its query, and fills the templates from the parameters and the
row's columns. A new notification is an `INSERT`, with no deploy.

Two guards keep the stored SQL safe:

- **Parameters are bound, never pasted into the SQL.**
- **A stored query runs read-only.** An SQLite authorizer refuses
  anything but reading while it runs. A wrong rule can match the wrong
  people, but it can't change data.

A template naming a value no row or parameter supplies raises an error
instead of sending a blank.

```sh
python3 -m unittest -v
```

The tests build a small in-memory database and check four things: each
active rule sends one email per match, a tag narrows the run, a new rule
is only rows, and a stored `DELETE` is refused.

The pattern comes from a notification system designed in 2015. This
code was written fresh for this repo.
