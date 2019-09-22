import React, { useState } from "react";
import { Header, Button, Divider, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function Validation() {
  return (
    <div>
      <Header as="h1" inverted>
        Validation
      </Header>
      <Divider />
      <Header as="h2" inverted>
        Types of Validation
      </Header>
      <Header as="h3" inverted>
        Basic Rules
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Basic validation rules consist of the built in validation that JForms
        provides for you to configure when creating your form. We enforce these
        rules for you on the back-end. Details of the different validation rules
        are outlined below.
      </p>
      <Header as="h3" inverted>
        Custom JavaScript
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>Not supported yet.</p>
      <Header as="h2" inverted style={{ paddingTop: 16 }}>
        Basic Validation Rules
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        These can all be set and configured via the create form wizard. These
        rules are all enforced by our back-end and a formatted error response
        will be returned back. More details on this error format in the
        submissions section.
      </p>
      <Header as="h3" inverted>
        Required
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        This rule can be set for any field type. It will require the submission
        to have a value for the field.
      </p>
      <Header as="h3" inverted>
        Minimum / Maximum Length
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        These rules dictate the min and max length that a string input can have.
      </p>
      <Header as="h3" inverted>
        Minimum / Maximum Value
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        These rules dictate the min and max value that a number input can have.
      </p>
      <Header as="h3" inverted>
        Minimum / Maximum Date
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        These rules dictate the min and max date value that a date input can
        have.
      </p>
      <Header as="h3" inverted>
        Regex
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>Not supported yet.</p>
    </div>
  );
}
