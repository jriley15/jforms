import React, { useEffect, useState } from "react";
import {
  Header,
  Form,
  Segment,
  Checkbox,
  Divider,
  Radio
} from "semantic-ui-react";
import styled from "styled-components";
import useRequest from "../hooks/useRequest";
import InputField from "../components/forms/InputField";

const FormField = styled(Form.Field)`
  max-width: 300px;
  padding: 16px;
`;

const Indent = styled.div`
  padding-left: 8px;
`;

export default function ViewForm({ match: { params } }) {
  const [form, setForm] = useState({});
  const { get, post } = useRequest();
  const [submission, setSubmission] = useState({});
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    async function getForm() {
      let response = await get("/Form/GetForSubmit", {
        formId: params.formId
      });
      setForm(response.data);
    }
    getForm();
    return () => {};
  }, []);

  const onChange = fieldName => (e, { value }) => {
    const submissionState = { ...submission, [fieldName]: value };
    setSubmission(submissionState);
    console.log(submissionState);
  };

  const onCheckboxChange = fieldName => (e, { value }) => {
    const submissionState = { ...submission };
    if (submissionState[fieldName]) {
      submissionState[fieldName] = false;
    } else {
      submissionState[fieldName] = true;
    }
    setSubmission(submissionState);
    console.log(submissionState);
  };

  const onDateChange = fieldName => date => {
    const submissionState = { ...submission, [fieldName]: date };
    setSubmission(submissionState);
    console.log(submissionState);
  };

  const submitForm = async () => {
    setSubmitting(true);

    let response = await post("/Submit/", submission);

    if (response.success) {
    } else {
    }
    setSubmitting(false);
  };

  return (
    <>
      <Header as="h1" inverted>
        {form.name &&
          form.name.charAt(0).toUpperCase() + form.name.substring(1)}
      </Header>
      <Divider />
      <Segment inverted>
        <Form inverted size="large">
          {form.fields &&
            form.fields.map((field, index) => (
              <FormField key={index}>
                <Form.Field
                  label={
                    field.name.charAt(0).toUpperCase() + field.name.substring(1)
                  }
                />
                <Indent>
                  <InputField
                    onChange={onChange}
                    onCheckboxChange={onCheckboxChange}
                    onDateChange={onDateChange}
                    submission={submission}
                    field={field}
                  />
                </Indent>
              </FormField>
            ))}

          <Form.Button>Submit</Form.Button>
        </Form>
      </Segment>
    </>
  );
}
