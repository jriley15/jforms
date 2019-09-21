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
      <Header as="h3" inverted>
        Strings
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Strings are used for basic text field inputs. They will accept any type
        of text up to the length you specify.
      </p>
    </div>
  );
}
