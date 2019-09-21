import React from "react";
import { Header, Divider } from "semantic-ui-react";

export default function Introduction() {
  return (
    <div>
      <Header as="h1" inverted>
        Introduction
      </Header>
      <Divider />
      <Header as="h3" inverted>
        What is JForms?
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        JForms is a platform that allows you to design your own forms with
        custom fields and validation without having to worry about the back-end.
        You may progamatically submit forms to our back-end API using any
        language you'd like via a POST end-point. We generate HTML markup and
        JavaScript code snippets for you to copy and paste into your website for
        easy integration. If you don't have a website, we also provide an
        embedded form for users to fill out your form on our website as well.
        You can view all the form submissions / data all in our simplistic
        dashboard.
      </p>
      <Header as="h3" inverted>
        How can I get started?
      </Header>
      <p style={{ fontSize: "1.2em" }}>
        Click on 'Get started' in the menu on the left.
      </p>
    </div>
  );
}
