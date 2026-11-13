"""A table-driven notification rules engine.

A rule is four rows, not code: a stored query that finds who the rule is
about, the values for that query's parameters, an email template, and the
tags that group it. Adding a rule is an INSERT. The engine runs every
active rule, or the rules with one tag, binds the rule's parameters into
its query, and sends one email per row the query returns, filled from that
row's columns.

A stored query runs read-only: an authorizer refuses anything but reading,
so a bad rule can find the wrong people but cannot change the data.
"""

from __future__ import annotations

import sqlite3
from string import Template
from typing import Callable, NamedTuple

SCHEMA = """
CREATE TABLE query      (id INTEGER PRIMARY KEY, description TEXT, sql TEXT NOT NULL);
CREATE TABLE rule       (id INTEGER PRIMARY KEY, description TEXT,
                         query_id INTEGER NOT NULL REFERENCES query(id),
                         subject TEXT NOT NULL, body TEXT NOT NULL,
                         active INTEGER NOT NULL DEFAULT 1);
CREATE TABLE rule_param (rule_id INTEGER NOT NULL REFERENCES rule(id),
                         name TEXT NOT NULL, value TEXT, PRIMARY KEY (rule_id, name));
CREATE TABLE rule_tag   (rule_id INTEGER NOT NULL REFERENCES rule(id),
                         tag TEXT NOT NULL, PRIMARY KEY (rule_id, tag));
"""


class Email(NamedTuple):
    to: str
    subject: str
    body: str


class Report(NamedTuple):
    rules: int
    sent: int


_READ = {sqlite3.SQLITE_SELECT, sqlite3.SQLITE_READ, sqlite3.SQLITE_FUNCTION}


def _read_only(action, *_):
    return sqlite3.SQLITE_OK if action in _READ else sqlite3.SQLITE_DENY


def run(db: sqlite3.Connection, send: Callable[[Email], None], tag: str | None = None) -> Report:
    """Run the active rules, or those tagged TAG; send each match its email.

    Every row a rule's query returns must carry an `email` column: it is
    the recipient, and every column is a $placeholder in the template."""
    rules = db.execute(
        """SELECT r.id, q.sql, r.subject, r.body FROM rule r JOIN query q ON q.id = r.query_id
           WHERE r.active AND (? IS NULL OR r.id IN (SELECT rule_id FROM rule_tag WHERE tag = ?))
           ORDER BY r.id""",
        (tag, tag),
    ).fetchall()
    sent = 0
    for rule_id, sql, subject, body in rules:
        params = dict(db.execute("SELECT name, value FROM rule_param WHERE rule_id = ?", (rule_id,)))
        db.set_authorizer(_read_only)
        try:
            cursor = db.execute(sql, params)
            columns = [c[0] for c in cursor.description]
            rows = [dict(zip(columns, row)) for row in cursor]
        finally:
            db.set_authorizer(None)
        for row in rows:
            values = {**params, **row}
            send(Email(row["email"], Template(subject).substitute(values), Template(body).substitute(values)))
            sent += 1
    return Report(len(rules), sent)
