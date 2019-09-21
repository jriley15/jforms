import React, { useState } from "react";
import { Header, Button, Divider, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";

export default function Fields() {
  return (
    <div>
      <Header as="h1" inverted>
        Form Fields
      </Header>
      <Divider />
      <Header as="h3" inverted>
        Strings
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Strings are used for basic text field inputs. They will accept any type
        of text up to the length you specify.
      </p>
      <Image size="medium" src="https://images.jrdn.tech/stringfield.JPG" />
      <Header as="h3" inverted>
        Numbers
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Numbers are used for any type of number input including decimals and
        whole numbers.
      </p>
      <Image size="small" src="https://images.jrdn.tech/numberfield.JPG" />
      <Header as="h3" inverted>
        Dates
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Dates are used for any date input.
      </p>
      <Image size="medium" src="https://images.jrdn.tech/datefield.JPG" />
      <Header as="h3" inverted>
        Radio Buttons
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Radio buttons are used for fields that have multiple options but only
        allow the user to pick one for submission.
      </p>
      <Image
        size="medium"
        src="https://images.jrdn.tech/radiobuttonfield.JPG"
      />
      <Header as="h3" inverted>
        Drop downs
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Like radio buttons, drop downs have multiple options but only let you
        select one. The difference is the way it's displayed.
      </p>
      <Image size="medium" src="https://images.jrdn.tech/dropdownfield.JPG" />
      <Header as="h3" inverted>
        Check boxes
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Check boxes have multiple options and also let the user select multiple
        options.
      </p>
      <Image size="medium" src="https://images.jrdn.tech/checkboxfield.JPG" />
    </div>
  );
}
