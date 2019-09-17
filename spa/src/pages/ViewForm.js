import React, { useEffect, useState } from "react";
import {
  Header,
  Form,
  Segment,
  Checkbox,
  Divider,
  Radio,
  List,
  Modal,
  Dimmer,
  Loader,
  Button
} from "semantic-ui-react";
import styled from "styled-components";
import useRequest from "../hooks/useRequest";
import InputField from "../components/forms/InputField";
import { Link } from "react-router-dom";

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
  const [errors, setErrors] = useState([]);
  const [success, setSuccess] = useState(false);

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
  };

  const onCheckboxChange = fieldName => (e, { value }) => {
    const submissionState = { ...submission };
    if (submissionState[fieldName]) {
      submissionState[fieldName] = false;
    } else {
      submissionState[fieldName] = true;
    }
    setSubmission(submissionState);
  };

  const onDateChange = fieldName => date => {
    const submissionState = { ...submission, [fieldName]: date };
    setSubmission(submissionState);
  };

  const submitForm = async () => {
    setSubmitting(true);

    let response = await post("/Submit/" + form.formId, submission);

    if (response.success) {
      //show success modal?
      setSuccess(true);
    } else {
      setErrors(response.errors);
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
      <div
        style={{
          opacity: submitting ? 0.5 : 1,
          pointerEvents: submitting ? "none" : "auto"
        }}
      >
        <Segment inverted>
          <Form inverted size="large" onSubmit={submitForm}>
            {form.fields &&
              form.fields.map((field, index) => (
                <FormField key={index}>
                  <Form.Field
                    label={
                      field.name.charAt(0).toUpperCase() +
                      field.name.substring(1) +
                      (field.validation.rules.filter(
                        r => r.formFieldValidationRuleTypeId === 1
                      ).length > 0
                        ? " *"
                        : "")
                    }
                  />
                  <Indent>
                    <InputField
                      onChange={onChange}
                      onCheckboxChange={onCheckboxChange}
                      onDateChange={onDateChange}
                      submission={submission}
                      field={field}
                      errors={errors}
                    />
                    <List>
                      {errors[field.name] &&
                        errors[field.name].map((error, index) => (
                          <List.Item style={{ color: "red" }}>
                            {error}
                          </List.Item>
                        ))}
                    </List>
                  </Indent>
                </FormField>
              ))}

            <Form.Button type="submit">Submit</Form.Button>
          </Form>
        </Segment>
      </div>

      <Dimmer active={submitting} page>
        <Loader active={submitting} size="big">
          Loading
        </Loader>
      </Dimmer>
      <Modal
        size="tiny"
        open={success}
        onClose={() => {
          setSubmission({});
        }}
      >
        <Header icon="check" content="Success" />
        <Modal.Content>
          <p>Your submission was created.</p>
        </Modal.Content>
        <Modal.Actions>
          <Button
            as={Link}
            to={"/"}
            color="green"
            icon="home"
            labelPosition="right"
            content="Home"
          />
        </Modal.Actions>
      </Modal>
    </>
  );
}
