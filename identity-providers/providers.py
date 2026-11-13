"""Sign-in through many identity providers, one small adapter each.

The base class is the standard OAuth 2.0 authorization-code flow, broken
into steps: the authorize URL, the token request, the profile request, the
profile's fields, and the refresh request. Most providers follow the
standard and need only their endpoints, which are data. A provider that
departs from it overrides only the step where it departs, and maps its
profile into one shared User. What a provider can do follows from what its
adapter supplies: no refresh request, no refresh.

Requests are built, not sent: each step returns a Request, and `sign_in`
hands it to whatever transport it is given, so the flow runs and tests
without a network.
"""

from __future__ import annotations

import base64
import hashlib
import hmac
import json
import time
from dataclasses import dataclass, field
from typing import Callable
from urllib.parse import urlencode


@dataclass(frozen=True)
class Endpoints:
    authorize: str
    token: str
    profile: str


@dataclass(frozen=True)
class Request:
    method: str
    url: str
    headers: dict = field(default_factory=dict)
    form: dict | None = None


@dataclass(frozen=True)
class User:
    provider: str
    uid: str
    email: str | None
    name: str | None


class OAuth2:
    """A provider that follows the standard: endpoints are all it needs."""

    name = "oauth2"

    def __init__(self, endpoints: Endpoints, client_id: str, client_secret: str, scope: str, redirect: str):
        self.endpoints = endpoints
        self.client_id = client_id
        self.client_secret = client_secret
        self.scope = scope
        self.redirect = redirect

    def authorize_url(self, state: str) -> str:
        query = {"response_type": "code", "client_id": self.client_id, "redirect_uri": self.redirect,
                 "scope": self.scope, "state": state}
        return f"{self.endpoints.authorize}?{urlencode(query)}"

    def secret(self) -> str:
        return self.client_secret

    def token_request(self, code: str) -> Request:
        return Request("POST", self.endpoints.token, form={
            "grant_type": "authorization_code", "code": code, "redirect_uri": self.redirect,
            "client_id": self.client_id, "client_secret": self.secret()})

    def refresh_request(self, refresh_token: str) -> Request | None:
        return Request("POST", self.endpoints.token, form={
            "grant_type": "refresh_token", "refresh_token": refresh_token,
            "client_id": self.client_id, "client_secret": self.secret()})

    @property
    def can_refresh(self) -> bool:
        return self.refresh_request("") is not None

    def profile_request(self, access_token: str) -> Request:
        return Request("GET", self.endpoints.profile, headers={"Authorization": f"Bearer {access_token}"})

    def user(self, profile: dict) -> User:
        return User(self.name, str(profile["sub"]), profile.get("email"), profile.get("name"))


class Nested(OAuth2):
    """A provider that wraps the profile in an envelope with an ok flag,
    and issues no refresh tokens."""

    name = "nested"

    def refresh_request(self, refresh_token: str) -> Request | None:
        return None

    def user(self, profile: dict) -> User:
        if not profile.get("ok"):
            raise SignInError(profile.get("error", "the provider refused the profile request"))
        person = profile["user"]
        return User(self.name, person["id"], person.get("email"), person.get("name"))


class SignedSecret(OAuth2):
    """A provider whose client secret is a short-lived token the client
    signs itself, with no profile endpoint: the token response carries
    the user."""

    name = "signed-secret"
    lifetime = 300

    def __init__(self, *args, key: bytes, now: Callable[[], float] = time.time, **kwargs):
        super().__init__(*args, **kwargs)
        self.key = key
        self.now = now

    def secret(self) -> str:
        claims = json.dumps({"sub": self.client_id, "exp": int(self.now()) + self.lifetime}, sort_keys=True)
        body = base64.urlsafe_b64encode(claims.encode()).decode()
        signature = hmac.new(self.key, body.encode(), hashlib.sha256).hexdigest()
        return f"{body}.{signature}"

    def profile_request(self, access_token: str) -> Request | None:
        return None


class SignInError(Exception):
    pass


ADAPTERS: dict[str, type[OAuth2]] = {a.name: a for a in (OAuth2, Nested, SignedSecret)}


def sign_in(provider: OAuth2, code: str, send: Callable[[Request], dict]) -> User:
    """Trade the code for a token, then the token for the user."""
    token = send(provider.token_request(code))
    if "error" in token:
        raise SignInError(token["error"])
    request = provider.profile_request(token["access_token"])
    return provider.user(send(request) if request else token["user"])
