import React from "react";
import { Modal, Button, Form, Header, Divider } from "semantic-ui-react";

export default function Register({ open, close }) {
  return (
    <Modal open={open} onClose={close} size="tiny">
      <Header
        icon="lock"
        content="Sign up"
        style={{ backgroundColor: "rgb(48, 48, 48)", color: "white" }}
      />
      <Modal.Content style={{ backgroundColor: "rgb(48, 48, 48)" }}>
        <Form inverted>
          <Form.Field>
            <Form.Field label="Email" />
            <Form.Input placeholder="Email Address" />
          </Form.Field>
          <Form.Field>
            <Form.Field label="Password" />
            <Form.Input placeholder="Password" type="password" />
          </Form.Field>
          <Form.Field>
            <Form.Field label="Confirm Password" />
            <Form.Input placeholder="Password" type="password" />
          </Form.Field>
        </Form>
        <Divider inverted horizontal>
          Or
        </Divider>
        <Button
          color="green"
          content="Sign in with Google"
          icon="google"
          fluid
        />
        <Divider inverted horizontal>
          Or
        </Divider>
        <Button content="Sign in with GitHub" icon="github" fluid />
      </Modal.Content>
      <Modal.Actions style={{ backgroundColor: "rgb(48, 48, 48)" }}>
        <Button>Cancel</Button>
        <Button color="blue">Submit</Button>
      </Modal.Actions>
    </Modal>
  );
}
