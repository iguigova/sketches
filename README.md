# sketches

Small programs from 2008 to 2021, one folder each.

| Folder | Year | Language | What it shows |
|---|---|---|---|
| [logging](logging/) | 2008 | Delphi, C# | A cross-cutting logging library: what and where to log set in one XML file, with pluggable formats and filters |
| [euler](euler/) | 2008 | C# | Helpers for Project Euler problems 1–23 |
| [regular-polygons](regular-polygons/) | 2010 | C# | A timed hiring test: perimeter and area of shapes read from CSV, with a test harness |
| [kloc](kloc/) | 2011 | Delphi | A line counter over a directory, by file mask |
| [outline](outline/) | 2011 | JavaScript | Indented notes turned into a collapsible outline; `rework.html` is notes on the book *Rework* |
| [css-in-10-lines](css-in-10-lines/) | 2011 | CSS | A fixed page styled in ten lines of CSS |
| [paypal-checkout](paypal-checkout/) | 2011 | JavaScript, Haml | PayPal Website Payments Pro: direct card payment and Express Checkout |
| [token-parser](token-parser/) | 2014 | JavaScript | Counting characters and tokens in a text, and splitting a string into dictionary words |
| [sliding-window](sliding-window/) | 2014 | JavaScript | Sliding-window percentage changes, naive O(n·k), with manual, system and load tests |
| [frisby-ridge](frisby-ridge/) | 2015 | JavaScript | A REST API test harness on Frisby: specs as data, run per user type and environment |
| [rules-engine](rules-engine/) | 2015 | Python | Email notification rules kept as table rows; stored queries run read-only |
| [identity-providers](identity-providers/) | 2021 | Python | Sign-in through many identity providers, one small adapter each |

`rules-engine` and `identity-providers` are patterns from work systems,
written fresh here in Python with tests. The year is when the pattern
was built. `paypal-checkout/payment-config.js` holds placeholders where
the API credentials go.

MIT licensed.
