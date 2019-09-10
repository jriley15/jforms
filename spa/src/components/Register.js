import React from "react";
import { Modal, Button, Form, Header, Divider } from "semantic-ui-react";

export default function Register({ open, close }) {
  return (
    <Modal open={open} onClose={close} size="tiny">
      <Header icon="lock" content="Sign up" />
      <Modal.Content>
        <Button
          color="green"
          content="Sign in with Google"
          icon="google"
          fluid
        />
        <Divider horizontal>Or</Divider>
        <Button content="Sign in with GitHub" icon="github" fluid />
        <Divider horizontal>Or</Divider>
        <Form>
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
      </Modal.Content>
      <Modal.Actions>
        <Button>Cancel</Button>
        <Button color="blue">Submit</Button>
      </Modal.Actions>
    </Modal>
  );
}
