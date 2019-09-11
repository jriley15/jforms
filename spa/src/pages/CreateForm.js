import React, { useEffect, useState } from "react";
import {
  Header,
  Form,
  Container,
  Icon,
  Button,
  Segment,
  Message,
  Loader,
  Modal,
  Dimmer,
  Breadcrumb,
  Divider
} from "semantic-ui-react";
import styled from "styled-components";
import Axios from "axios";
import { apiUrl } from "../config";
import Collapsible from "../components/common/Collapse";
import { Link } from "react-router-dom";

const FormField = styled(Form.Field)`
  max-width: 300px;
`;
const FormTextArea = styled(Form.TextArea)`
  max-width: 300px;
`;
const FormSelect = styled(Form.Select)`
  max-width: 300px;
`;
const Group = styled.div`
  padding-top: 16px;
  padding-bottom: 16px;
`;

const Indent = styled.div`
  padding-left: 32px;
`;

const FieldContainer = styled.div`
  padding-top: 8px;
  padding-bottom: 8px;
`;

export default function CreateForm() {
  const [formFieldTypes, setFormFieldTypes] = useState([]);
  const [formFields, setFormFields] = useState([]);
  const [validationTypes, setValidationTypes] = useState([]);
  const [formName, setFormName] = useState("");
  const [formType, setFormType] = useState(0);
  const [submitting, setSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [formId, setFormId] = useState(0);

  const submitForm = () => {
    const form = {
      Name: formName,
      Type: formType,
      Fields: formFields
    };

    setSubmitting(true);

    Axios.post(apiUrl + "/Form/Create", form)
      .then(response => {
        setSuccess(true);
        setSubmitting(false);
      })
      .catch(error => {
        setSubmitting(false);
        setSuccess(false);
        //set errors
      });
  };

  const addFormField = () => {
    const field = {
      Name: "",
      Type: 0,
      Options: [],
      Validation: {
        Type: 0,
        Script: "",
        Rules: []
      }
    };
    setFormFields(formFields => [...formFields, field]);
  };

  const removeFormField = () => {
    if (formFields.length > 1) {
      const fields = [...formFields];
      fields.splice(fields.length - 1, 1);
      setFormFields(fields);
    }
  };

  const addRule = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Rules.push({
      Type: 0,
      Value: ""
    });
    setFormFields(fields);
  };

  const removeRule = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Rules.splice(
      fields[fieldIndex].Validation.Rules.length - 1,
      1
    );
    setFormFields(fields);
  };

  const resetRules = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Rules = [];
    setFormFields(fields);
  };

  const resetValidationType = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Type = 0;
    setFormFields(fields);
  };

  const setValidationType = (fieldIndex, type) => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Type = type;
    setFormFields(fields);
  };

  const addOption = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Options.push({
      Value: ""
    });
    setFormFields(fields);
  };

  const removeOption = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].Options.splice(fields[fieldIndex].Options.length - 1, 1);
    setFormFields(fields);
  };

  const fieldTypeChange = fieldIndex => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Type = value;
    setFormFields(fields);
    resetRules(fieldIndex);
    resetValidationType(fieldIndex);
    if (formFieldTypes[formFields[fieldIndex].Type].multipleOptions) {
      addOption(fieldIndex);
    }
  };

  const validationTypeChange = fieldIndex => (e, { value }) => {
    setValidationType(fieldIndex, value);

    if (value === 1) {
      resetRules(fieldIndex);
      addRule(fieldIndex);
      const types = [...validationTypes];

      Axios.get(apiUrl + "/FormField/GetValidationTypes", {
        params: {
          fieldType: formFields[fieldIndex].Type
        }
      })
        .then(response => {
          let validTypes = response.data;

          types[formFields[fieldIndex].Type] = validTypes;

          setValidationTypes(types);
        })
        .catch(error => {});
    }
  };

  const ruleTypeChange = (fieldIndex, ruleIndex) => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Rules[ruleIndex].Type = value;
    setFormFields(fields);
  };

  const formNameChange = (e, { value }) => {
    setFormName(value);
  };

  const fieldNameChange = fieldIndex => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Name = value;
    setFormFields(fields);
  };

  const formTypeChange = (e, { value }) => {
    setFormType(value);
    if (formFields.length === 0) addFormField();
  };

  const ruleValueChange = (fieldIndex, ruleIndex) => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Rules[ruleIndex].Value = value;
    setFormFields(fields);
  };

  const optionValueChange = (fieldIndex, optionIndex) => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Options[optionIndex].Value = value;
    setFormFields(fields);
  };

  const resetForm = () => {
    setFormName("");
    setFormFields([]);
    setFormType(0);
    setSubmitting(false);
    setSuccess(false);
  };

  const validationScriptChange = fieldIndex => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Validation.Script = value;
    setFormFields(fields);
  };

  useEffect(() => {
    Axios.get(apiUrl + "/FormField/GetTypes")
      .then(response => {
        setFormFieldTypes(response.data);
      })
      .catch(error => {});

    return () => {};
  }, []);

  return (
    <>
      <Breadcrumb size="massive">
        <Breadcrumb.Section link as={Link} to="/form">
          Forms
        </Breadcrumb.Section>
        <Breadcrumb.Divider icon="right chevron" />
        <Breadcrumb.Section active>Create Form</Breadcrumb.Section>
      </Breadcrumb>
      <Form
        inverted
        style={{
          position: "relative",
          paddingTop: 32
        }}
      >
        <div
          style={{
            opacity: submitting ? 0.5 : 1,
            pointerEvents: submitting ? "none" : "auto"
          }}
        >
          <Segment inverted>
            <Group>
              <p style={{ fontSize: "1.33em" }}>
                <b>Step 1.</b> Name your form
              </p>
              <Divider inverted />
              <Indent>
                <FormField>
                  <Form.Field label="Form Name" />
                  <Form.Input
                    placeholder="Form Name"
                    onChange={formNameChange}
                    value={formName}
                  />
                </FormField>
              </Indent>
            </Group>
          </Segment>
          {formName && (
            <Segment inverted>
              <Group>
                <p style={{ fontSize: "1.33em" }}>
                  <b>Step 2.</b> Select a type of form
                </p>
                <Divider inverted />
                <Indent>
                  <FormSelect
                    fluid
                    label="Form Type"
                    options={[
                      { key: 0, text: "JSON Body Post", value: 1 },
                      { key: 1, text: "HTML Form Post", value: 2 }
                    ]}
                    placeholder="Form Type"
                    onChange={formTypeChange}
                    value={formType}
                  />
                </Indent>
              </Group>
            </Segment>
          )}
          {formType > 0 && (
            <Segment inverted>
              <Group>
                <p style={{ fontSize: "1.33em" }}>
                  <b>Step 3.</b> Create & Configure your fields
                </p>
                <Divider inverted />
                <Message color="yellow" style={{ maxWidth: 464 }}>
                  <Message.Header>
                    Warning: fields cannot be changed once the form is created
                  </Message.Header>
                  <p>
                    Make sure you configure them correctly before submitting the
                    form.
                  </p>
                </Message>
                {formFields.map((formField, fieldIndex) => (
                  <FieldContainer key={fieldIndex}>
                    <Collapsible
                      header={
                        <p style={{ fontSize: "1.16em" }}>
                          <b>Field {fieldIndex + 1}</b>
                        </p>
                      }
                    >
                      <Indent
                        style={{
                          borderLeft: "1px solid white",
                          marginLeft: "8px"
                        }}
                      >
                        <FormField>
                          <Form.Field label="Field Name" />
                          <Form.Input
                            placeholder="Field Name"
                            onChange={fieldNameChange(fieldIndex)}
                            value={formField.Name}
                          />
                        </FormField>

                        <FormSelect
                          fluid
                          label="Field Type"
                          onChange={fieldTypeChange(fieldIndex)}
                          options={formFieldTypes.map(
                            (fieldType, fieldTypeIndex) => ({
                              key: fieldTypeIndex,
                              text: fieldType.name,
                              value: fieldType.formFieldTypeId
                            })
                          )}
                          placeholder="Field Type"
                        />
                        {formField.Type > 0 &&
                          formFieldTypes[formField.Type].multipleOptions &&
                          formField.Options.map(
                            (formFieldOptions, optionIndex) => (
                              <FieldContainer key={optionIndex}>
                                <Collapsible
                                  header={
                                    <p style={{ fontSize: "1em" }}>
                                      <b>Option {optionIndex + 1}</b>
                                    </p>
                                  }
                                  key={optionIndex}
                                >
                                  <Indent
                                    style={{
                                      borderLeft: "1px solid white",
                                      marginLeft: "8px"
                                    }}
                                  >
                                    <FormField>
                                      <Form.Field label="Option Value" />
                                      <Form.Input
                                        placeholder="Option Value"
                                        onChange={optionValueChange(
                                          fieldIndex,
                                          optionIndex
                                        )}
                                        value={
                                          formField.Options[optionIndex].Value
                                        }
                                      />
                                    </FormField>
                                  </Indent>
                                </Collapsible>
                                {optionIndex ===
                                  formField.Options.length - 1 && (
                                  <div
                                    style={{ marginTop: 32, marginBottom: 32 }}
                                  >
                                    <Button
                                      color="teal"
                                      onClick={() => addOption(fieldIndex)}
                                      icon="add circle"
                                      content="Add Option"
                                      labelPosition="left"
                                    />
                                    {formField.Options.length > 1 && (
                                      <Button
                                        color="red"
                                        onClick={() => removeOption(fieldIndex)}
                                        icon="x"
                                        content="Remove Option"
                                        labelPosition="left"
                                      />
                                    )}
                                  </div>
                                )}
                              </FieldContainer>
                            )
                          )}

                        {formField.Type > 0 && (
                          <FormSelect
                            fluid
                            label="Validation Type"
                            onChange={validationTypeChange(fieldIndex)}
                            value={formField.Validation.Type}
                            options={[
                              {
                                key: 0,
                                text: "None",
                                value: 0
                              },
                              {
                                key: 1,
                                text: "Basic Rules",
                                value: 1
                              },
                              {
                                key: 2,
                                text: "Custom Javascript",
                                value: 2
                              }
                            ]}
                            placeholder="Validation Type"
                          />
                        )}
                        {formField.Validation.Type === 2 && (
                          <Indent>
                            <FormTextArea
                              label="Custom validation script"
                              placeholder="JavaScript"
                              onChange={validationScriptChange(fieldIndex)}
                            ></FormTextArea>
                          </Indent>
                        )}
                        {formField.Validation.Type === 1 &&
                          validationTypes[formField.Type] && (
                            <>
                              {formField.Validation.Rules.map(
                                (rule, ruleIndex) => (
                                  <FieldContainer key={ruleIndex}>
                                    <Collapsible
                                      header={
                                        <p style={{ fontSize: "1.1em" }}>
                                          <b>Rule {ruleIndex + 1}</b>
                                        </p>
                                      }
                                    >
                                      <Indent
                                        style={{
                                          borderLeft: "1px solid white",
                                          marginLeft: "8px"
                                        }}
                                      >
                                        <FormSelect
                                          fluid
                                          label="Rule Type"
                                          onChange={ruleTypeChange(
                                            fieldIndex,
                                            ruleIndex
                                          )}
                                          options={validationTypes[
                                            formField.Type
                                          ].map((validationType, index) => ({
                                            key: index,
                                            text: validationType.name,
                                            value:
                                              validationType.formFieldValidationRuleTypeId
                                          }))}
                                          placeholder="Rule Type"
                                        />
                                        {formField.Validation.Rules[ruleIndex]
                                          .Type > 1 && (
                                          <FormField>
                                            <Form.Field label="Rule Value" />
                                            <Form.Input
                                              placeholder="Constraint"
                                              onChange={ruleValueChange(
                                                fieldIndex,
                                                ruleIndex
                                              )}
                                              value={
                                                formField.Validation.Rules[
                                                  ruleIndex
                                                ].Value
                                              }
                                            />
                                          </FormField>
                                        )}
                                      </Indent>
                                    </Collapsible>
                                    {ruleIndex ===
                                      formField.Validation.Rules.length - 1 && (
                                      <div style={{ marginTop: 32 }}>
                                        <Button
                                          color="orange"
                                          onClick={() => addRule(fieldIndex)}
                                          icon="add circle"
                                          content="Add Rule"
                                          labelPosition="left"
                                        />
                                        {formField.Validation.Rules.length >
                                          1 && (
                                          <Button
                                            color="red"
                                            icon="x"
                                            content="Remove Rule"
                                            labelPosition="left"
                                            onClick={() =>
                                              removeRule(fieldIndex)
                                            }
                                          />
                                        )}
                                      </div>
                                    )}
                                  </FieldContainer>
                                )
                              )}
                            </>
                          )}
                      </Indent>
                    </Collapsible>
                    {fieldIndex === formFields.length - 1 && (
                      <div style={{ marginTop: 32 }}>
                        <Button
                          color="blue"
                          onClick={addFormField}
                          icon="add circle"
                          content="Add Field"
                          labelPosition="left"
                        />
                        {formFields.length > 1 && (
                          <Button
                            color="red"
                            onClick={removeFormField}
                            icon="x"
                            content="Remove Field"
                            labelPosition="left"
                          />
                        )}
                      </div>
                    )}
                  </FieldContainer>
                ))}
              </Group>
            </Segment>
          )}
          {formFields.length > 0 && (
            <Segment inverted>
              <Group>
                <p style={{ fontSize: "1.33em" }}>
                  <b>Step 4.</b> Done
                </p>
                <Divider inverted />

                <Button
                  color="green"
                  size="large"
                  icon="check"
                  labelPosition="left"
                  content="Submit Form"
                  onClick={submitForm}
                />
                <Button
                  size="large"
                  icon="x"
                  labelPosition="left"
                  content="Clear Form"
                  onClick={resetForm}
                />
              </Group>
            </Segment>
          )}
        </div>
      </Form>
      <Dimmer active={submitting} page>
        <Loader active={submitting} size="big">
          Loading
        </Loader>
      </Dimmer>
      <Modal size="tiny" open={success}>
        <Header icon="check" content="Success" />
        <Modal.Content>
          <p>Your form was created.</p>
        </Modal.Content>
        <Modal.Actions>
          <Button
            icon="edit"
            labelPosition="right"
            content="Create Another"
            onClick={resetForm}
          />
          <Button
            as={Link}
            to={"/form/dashboard/" + formId}
            color="green"
            icon="file code"
            labelPosition="right"
            content="Form Dashboard"
          />
        </Modal.Actions>
      </Modal>
    </>
  );
}
