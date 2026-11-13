import base64
import hashlib
import hmac
import json
import unittest
from urllib.parse import parse_qs, urlsplit

from providers import ADAPTERS, Endpoints, Nested, OAuth2, SignedSecret, SignInError, User, sign_in

ENDPOINTS = Endpoints("https://idp.example/authorize", "https://idp.example/token", "https://idp.example/me")
ARGS = (ENDPOINTS, "client-1", "s3cret", "openid email", "https://app.example/back")


class FakeIdP:
    """Answers each request by URL, and records what it was sent."""

    def __init__(self, answers):
        self.answers = answers
        self.seen = []

    def __call__(self, request):
        self.seen.append(request)
        return self.answers[request.url]


class FlowTest(unittest.TestCase):
    def test_the_standard_flow_needs_only_endpoints(self):
        idp = FakeIdP({ENDPOINTS.token: {"access_token": "t1"},
                       ENDPOINTS.profile: {"sub": 42, "email": "ana@example.org", "name": "Ana"}})
        user = sign_in(OAuth2(*ARGS), "code-1", idp)
        self.assertEqual(user, User("oauth2", "42", "ana@example.org", "Ana"))
        self.assertEqual(idp.seen[0].form["code"], "code-1")
        self.assertEqual(idp.seen[1].headers, {"Authorization": "Bearer t1"})

    def test_the_authorize_url_carries_the_state(self):
        url = urlsplit(OAuth2(*ARGS).authorize_url("xyz"))
        query = parse_qs(url.query)
        self.assertEqual(url.netloc + url.path, "idp.example/authorize")
        self.assertEqual(query["state"], ["xyz"])
        self.assertEqual(query["redirect_uri"], ["https://app.example/back"])

    def test_an_adapter_maps_its_own_profile_shape(self):
        idp = FakeIdP({ENDPOINTS.token: {"access_token": "t2"},
                       ENDPOINTS.profile: {"ok": True, "user": {"id": "U7", "name": "Ben"}}})
        self.assertEqual(sign_in(Nested(*ARGS), "c", idp), User("nested", "U7", None, "Ben"))

    def test_a_refused_profile_is_an_error_not_an_empty_user(self):
        idp = FakeIdP({ENDPOINTS.token: {"access_token": "t2"},
                       ENDPOINTS.profile: {"ok": False, "error": "token_revoked"}})
        with self.assertRaisesRegex(SignInError, "token_revoked"):
            sign_in(Nested(*ARGS), "c", idp)

    def test_a_refused_code_stops_the_flow(self):
        idp = FakeIdP({ENDPOINTS.token: {"error": "invalid_grant"}})
        with self.assertRaisesRegex(SignInError, "invalid_grant"):
            sign_in(OAuth2(*ARGS), "stale", idp)
        self.assertEqual(len(idp.seen), 1)

    def test_what_a_provider_can_do_follows_from_its_adapter(self):
        self.assertTrue(OAuth2(*ARGS).can_refresh)
        self.assertFalse(Nested(*ARGS).can_refresh)

    def test_a_signed_secret_is_made_per_request_and_verifies(self):
        provider = SignedSecret(*ARGS, key=b"k", now=lambda: 1000)
        idp = FakeIdP({ENDPOINTS.token: {"access_token": "t3", "user": {"sub": "9", "email": "cy@example.org"}}})
        self.assertEqual(sign_in(provider, "c", idp), User("signed-secret", "9", "cy@example.org", None))
        body, signature = idp.seen[0].form["client_secret"].split(".")
        self.assertEqual(signature, hmac.new(b"k", body.encode(), hashlib.sha256).hexdigest())
        self.assertEqual(json.loads(base64.urlsafe_b64decode(body)), {"exp": 1300, "sub": "client-1"})
        self.assertEqual(len(idp.seen), 1)

    def test_adapters_are_found_by_name(self):
        self.assertEqual(set(ADAPTERS), {"oauth2", "nested", "signed-secret"})


if __name__ == "__main__":
    unittest.main()
