import React from "react";
import { Header, Form, Container } from "semantic-ui-react";
import styled from "styled-components";

const FormField = styled(Form.Field)`
  max-width: 300px;
`;
const FormSelect = styled(Form.Select)`
  max-width: 300px;
`;
const Group = styled.div`
  padding-top: 16px;
  padding-bottom: 16px;
`;

export default function CreateForm() {
  return (
    <Form inverted>
      <Header as="h3" style={{ fontSize: "2em" }} inverted>
        Create a new Form
      </Header>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 1.</b> Select a name for your form
        </p>
        <FormField>
          <label>Form Name</label>
          <input placeholder="Form Name" />
        </FormField>
      </Group>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 2.</b> Select a type of form
        </p>
        <FormSelect
          fluid
          label="Form Type"
          options={[
            { key: 0, text: "JSON Body Post", value: "0" },
            { key: 1, text: "HTML Form Post", value: "0" }
          ]}
          placeholder="Form Type"
        />
      </Group>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 3.</b> Create & Configure your fields
        </p>
        <FormField>
          <label>Field Name</label>
          <input placeholder="Field Name" />
        </FormField>
      </Group>
    </Form>
  );
}
