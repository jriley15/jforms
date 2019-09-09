import React, { useEffect, useState } from "react";
import {
  Header,
  Form,
  Container,
  Icon,
  Button,
  Segment,
  Message
} from "semantic-ui-react";
import styled from "styled-components";
import Axios from "axios";
import { apiUrl } from "../config";
import Collapsible from "../components/common/Collapse";

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

const RuleContainer = styled.div`
  padding-top: 8px;
  padding-bottom: 8px;
`;

export default function CreateForm() {
  const [formFieldTypes, setFormFieldTypes] = useState([]);
  const [formFields, setFormFields] = useState([]);
  const [validationTypes, setValidationTypes] = useState([]);
  const [formName, setFormName] = useState("");
  const [formType, setFormType] = useState(0);

  const submitForm = () => {
    const form = {
      Name: formName,
      Type: formType,
      Fields: formFields
    };

    Axios.post(apiUrl + "/Form/Create", form)
      .then(response => {
        console.log(response);
      })
      .catch(error => {});
  };

  const addFormField = () => {
    const field = {
      Name: "",
      Type: 0,
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

  const fieldTypeChange = fieldIndex => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].Type = value;
    setFormFields(fields);
    resetRules(fieldIndex);
    resetValidationType(fieldIndex);
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

  const resetForm = () => {
    setFormName("");
    setFormFields([]);
    setFormType(0);
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
    <Form inverted>
      <Header as="h3" style={{ fontSize: "2em" }} inverted>
        Create a new Form
      </Header>
      <Indent>
        <Group>
          <p style={{ fontSize: "1.33em" }}>
            <b>Step 1.</b> Name your form
          </p>
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
        {formName && (
          <Group>
            <p style={{ fontSize: "1.33em" }}>
              <b>Step 2.</b> Select a type of form
            </p>
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
        )}
        {formType > 0 && (
          <Group>
            <p style={{ fontSize: "1.33em" }}>
              <b>Step 3.</b> Create & Configure your fields
            </p>
            <Message color="yellow" style={{ maxWidth: 457 }}>
              <Message.Header>
                Fields cannot be changed once the form is created
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
                    style={{ borderLeft: "1px solid white", marginLeft: "8px" }}
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
                          {formField.Validation.Rules.map((rule, ruleIndex) => (
                            <RuleContainer key={ruleIndex}>
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
                                  {formField.Validation.Rules[ruleIndex].Type >
                                    1 && (
                                    <FormField>
                                      <Form.Field label="Rule Value" />
                                      <Form.Input
                                        placeholder="Constraint"
                                        onChange={ruleValueChange(
                                          fieldIndex,
                                          ruleIndex
                                        )}
                                        value={
                                          formField.Validation.Rules[ruleIndex]
                                            .Value
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
                                    color="yellow"
                                    onClick={() => addRule(fieldIndex)}
                                    inverted
                                  >
                                    <Icon name="add circle" />
                                    Add Rule
                                  </Button>
                                  {formField.Validation.Rules.length > 1 && (
                                    <Button
                                      inverted
                                      color="red"
                                      onClick={() => removeRule(fieldIndex)}
                                    >
                                      <Icon name="x" /> Remove
                                    </Button>
                                  )}
                                </div>
                              )}
                            </RuleContainer>
                          ))}
                        </>
                      )}
                  </Indent>
                </Collapsible>
                {fieldIndex === formFields.length - 1 && (
                  <div style={{ marginTop: 32 }}>
                    <Button color="blue" onClick={addFormField} inverted>
                      <Icon name="add circle" />
                      Add Field
                    </Button>
                    {formFields.length > 1 && (
                      <Button inverted color="red" onClick={removeFormField}>
                        <Icon name="x" /> Remove
                      </Button>
                    )}
                  </div>
                )}
              </FieldContainer>
            ))}
          </Group>
        )}
        {formFields.length > 0 && (
          <Group>
            <p style={{ fontSize: "1.33em" }}>
              <b>Step 4.</b> All Done
            </p>

            <Indent>
              <Button color="green" inverted size="large" onClick={submitForm}>
                Submit Form
              </Button>
              <Button color="grey" inverted size="large" onClick={resetForm}>
                Clear
              </Button>
            </Indent>
          </Group>
        )}
      </Indent>
    </Form>
  );
}
