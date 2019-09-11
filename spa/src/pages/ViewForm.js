import React, { useEffect, useState } from "react";
import Axios from "axios";
import { apiUrl } from "../config";
import { Header, Form, Segment, Checkbox } from "semantic-ui-react";
import styled from "styled-components";

const FormField = styled(Form.Field)`
  max-width: 300px;
`;

export default function ViewForm({ match: { params } }) {
  const [form, setForm] = useState({});

  useEffect(() => {
    Axios.get(apiUrl + "/Form/Get", { params: { formId: params.formId } }).then(
      response => {
        setForm(response.data);
      }
    );

    return () => {};
  }, []);

  const renderInput = field => {
    switch (field.formFieldType.name) {
      case "String":
        return <Form.Input placeholder={field.name} />;

      case "CheckBox":
        return (
          <>
            {field.options.map((option, index) => (
              <Form.Field>
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
              <Form.Field>
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

      <Form inverted>
        {form.fields &&
          form.fields.map((field, index) => (
            <FormField key={index}>
              <Form.Field
                label={
                  field.name.charAt(0).toUpperCase() + field.name.substring(1)
                }
              />
              {renderInput(field)}
            </FormField>
          ))}
      </Form>
    </>
  );
}
