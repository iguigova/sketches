# identity-providers

Sign-in through many identity providers, with one small adapter each.

The base class, `OAuth2`, is the standard authorization-code flow, split
into steps that can each be overridden:

1. the authorize URL the user is sent to,
2. the token request that trades the returned code for a token,
3. the profile request made with that token,
4. the mapping from the profile into one shared `User`,
5. the refresh request.

A provider that follows the standard needs no code at all, only its
endpoints, which are data. A provider that departs from it overrides only
the step where it departs. What a provider can do follows from its
adapter: one that supplies no refresh request reports `can_refresh` as
false, and no list of capabilities can drift out of step with the code.

The two example adapters show the usual departures:

- **`Nested`** wraps the profile in an envelope with an `ok` flag, and
  issues no refresh tokens.
- **`SignedSecret`** has no fixed client secret. The client signs a
  short-lived one for each request, and the user comes back in the token
  response instead of from a profile endpoint.

The production system behind this pattern had adapters for 36
providers: 33 on OAuth 2.0, 2 on OAuth 1.0a and a payments API on client
credentials. It also had one more adapter for its own test provider, and
SAML beside them. Other departures it covered were a different authorization header,
an email address served from a second endpoint, and the OAuth 1.0a
request-token step.

Each step builds a `Request` and doesn't send it. `sign_in` hands each
one to a transport it is given, so the whole flow runs in tests against
a fake provider, with no network.

```sh
python3 -m unittest -v
```

This code was written fresh for this repo. It holds none of the original
system's code, names or libraries.
