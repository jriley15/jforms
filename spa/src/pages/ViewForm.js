import React, { useEffect, useState } from "react";
import { Header, Form, Segment, Checkbox, Divider } from "semantic-ui-react";
import styled from "styled-components";
import { DateInput } from "semantic-ui-calendar-react";
import useRequest from "../hooks/useRequest";

const FormField = styled(Form.Field)`
  max-width: 300px;
  padding: 16px;
`;

const Indent = styled.div`
  padding-left: 8px;
`;

export default function ViewForm({ match: { params } }) {
  const [form, setForm] = useState({});
  const { get } = useRequest();

  useEffect(() => {
    async function getForm() {
      let response = await get("/Form/Get", { formId: params.formId });
      setForm(response.data);
    }
    getForm();
    return () => {};
  }, []);

  const renderInput = field => {
    switch (field.formFieldType.name) {
      case "String":
        return <Form.Input placeholder={field.name} />;

      case "Number":
        return <Form.Input placeholder={field.name} style={{ maxWidth: 75 }} />;

      case "CheckBox":
        return (
          <>
            {field.options.map((option, index) => (
              <Form.Field key={index}>
                <Checkbox
                  label={option.value}
                  name={option.value}
                  value={option.value}
                />
              </Form.Field>
            ))}
          </>
        );

      case "RadioButton":
        return (
          <>
            {field.options.map((option, index) => (
              <Form.Field key={index}>
                <Checkbox
                  radio
                  label={option.value}
                  name={option.value}
                  value={option.value}
                />
              </Form.Field>
            ))}
          </>
        );
      case "DropDown":
        return (
          <Form.Select
            fluid
            label="Gender"
            options={field.options.map((option, index) => ({
              key: option.value,
              text: option.value,
              value: option.value
            }))}
            placeholder="Gender"
          />
        );
      case "Date":
        return (
          <DateInput
            name={field.name}
            placeholder="Date"
            iconPosition="left"
            popupPosition="bottom center"
          />
        );

      default:
        break;
    }
  };

  return (
    <>
      <Header as="h1" inverted>
        {form.name &&
          form.name.charAt(0).toUpperCase() + form.name.substring(1)}
      </Header>
      <Divider />

      <Form inverted size="large">
        {form.fields &&
          form.fields.map((field, index) => (
            <FormField key={index}>
              <Form.Field
                label={
                  field.name.charAt(0).toUpperCase() + field.name.substring(1)
                }
              />
              <Indent>{renderInput(field)}</Indent>
            </FormField>
          ))}

        <Form.Button>Submit</Form.Button>
      </Form>
    </>
  );
}
