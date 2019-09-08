import React from "react";
import { Container, Header, Button, Icon } from "semantic-ui-react";

export default function Home() {
  return (
    <div>
      <Container text textAlign="center">
        <Header
          as="h1"
          content="JForms"
          inverted
          style={{
            fontSize: "4em",
            fontWeight: "normal",
            marginBottom: 0,
            marginTop: "3em"
          }}
        />
        <Header
          as="h2"
          content="HTML forms made easy."
          inverted
          style={{
            fontSize: "1.7em",
            marginTop: "1em",
            fontWeight: "normal"
          }}
        />
        <Button
          primary
          size="huge"
          style={{
            marginTop: "1em"
          }}
        >
          Get Started
          <Icon name="right arrow" />
        </Button>
      </Container>
    </div>
  );
}
