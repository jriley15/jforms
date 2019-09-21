import React, { useState } from "react";
import { Header, Button, Divider, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function CreateAForm() {
  return (
    <div>
      <Header as="h1" inverted>
        Create a Form
      </Header>
      <Divider />
      <Header as="h3" inverted>
        Navigate to the Form Panel
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        To get started with JForms, you must create and sign into an account. At
        the moment, we only support signing in with GitHub OAuth. If you have a
        GitHub account, the sign in process takes a few seconds and then you're
        ready to start making forms. Once you sign in you'll see the{" "}
        <Link to="/form">form panel button</Link> appear in the navigation bar.
      </p>
      <Header as="h3" inverted>
        Click on the '<Link to="/form/create">Create Form</Link>' button
      </Header>
      <Image src="https://images.jrdn.tech/createbutton.JPG" size="big" />

      <Header as="h3" inverted>
        Follow the steps
      </Header>
      <Image src="https://images.jrdn.tech/createform.JPG" size="big" />
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Follow the steps to create and configure your form to your
        specifications. Take a look at some of the other following documentation
        sections for more detailed specifics and help on these steps.
      </p>
    </div>
  );
}
