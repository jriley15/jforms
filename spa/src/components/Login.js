import React from "react";
import { Modal, Button, Form, Header } from "semantic-ui-react";

export default function Login({ open, close }) {
  return (
    <Modal open={open} onClose={close} size="tiny">
      <Header icon="lock" content="Sign in" />
      <Modal.Content>
        <Form>
          <Form.Field>
            <Form.Field label="Email" />
            <Form.Input placeholder="Email Address" />
          </Form.Field>
          <Form.Field>
            <Form.Field label="Password" />
            <Form.Input placeholder="Password" type="password" />
          </Form.Field>
        </Form>
      </Modal.Content>
      <Modal.Actions>
        <Button>Cancel</Button>
        <Button color="blue">Login</Button>
      </Modal.Actions>
    </Modal>
  );
}
