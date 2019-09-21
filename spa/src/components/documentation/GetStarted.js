import React, { useState } from "react";
import { Header, Button, Divider } from "semantic-ui-react";
import Login from "../Login";

export default function GetStarted() {
  const [loginOpen, setLoginOpen] = useState(false);

  return (
    <div>
      <Header as="h1" inverted>
        Get started
      </Header>
      <Divider />
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        To get started with JForms, you must create and sign into an account. At
        the moment, we only support signing in with GitHub OAuth. If you have a
        GitHub account, the sign in process takes a few seconds and then you're
        ready to start making forms. Once you sign in you'll see the form
        dashboard button appear in the navigation bar.
      </p>
      <Button color="teal" onClick={() => setLoginOpen(true)}>
        Sign in
      </Button>

      <Login open={loginOpen} close={() => setLoginOpen(false)} />
    </div>
  );
}
