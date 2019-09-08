import React, { useEffect, useState } from "react";
import {
  Header,
  Form,
  Container,
  Icon,
  Button,
  Segment
} from "semantic-ui-react";
import styled from "styled-components";
import Axios from "axios";
import { apiUrl } from "../config";

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

  const addFormField = () => {
    const field = {
      name: "",
      formFieldTypeId: 0,
      collapsed: false,
      validation: {
        type: 0,
        script: "",
        rules: []
      }
    };
    setFormFields(formFields => [...formFields, field]);
  };

  const addRule = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules.push({
      type: 0,
      value: "",
      collapsed: false
    });
    setFormFields(fields);
  };

  const resetRules = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules = [];
    setFormFields(fields);
  };

  const resetValidationType = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].validation.type = 0;
    setFormFields(fields);
  };

  const setValidationType = (fieldIndex, type) => {
    const fields = [...formFields];
    fields[fieldIndex].validation.type = type;
    setFormFields(fields);
  };

  const fieldTypeChange = fieldIndex => (e, type) => {
    const fields = [...formFields];
    fields[fieldIndex].formFieldTypeId = type.value;
    setFormFields(fields);
    resetRules(fieldIndex);
    resetValidationType(fieldIndex);
  };

  const collapseField = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].collapsed = !fields[fieldIndex].collapsed;
    setFormFields(fields);
  };

  const collapseRule = (fieldIndex, ruleIndex) => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules[ruleIndex].collapsed = !fields[
      fieldIndex
    ].validation.rules[ruleIndex].collapsed;
    setFormFields(fields);
  };

  const validationTypeChange = fieldIndex => (e, type) => {
    setValidationType(fieldIndex, type.value);

    if (type.value === 1) {
      resetRules(fieldIndex);
      addRule(fieldIndex);
      //get validation types for field

      const types = [...validationTypes];

      //axios
      Axios.get(apiUrl + "/FormField/GetValidationTypes", {
        params: {
          fieldType: formFields[fieldIndex].formFieldTypeId
        }
      })
        .then(response => {
          let validTypes = response.data;

          types[formFields[fieldIndex].formFieldTypeId] = validTypes;

          setValidationTypes(types);
        })
        .catch(error => {});

      //add validation type with this type as the key
    }
  };

  const ruleTypeChange = (fieldIndex, ruleIndex) => (e, type) => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules[ruleIndex].type = type.value;
    setFormFields(fields);
  };

  useEffect(() => {
    Axios.get(apiUrl + "/FormField/GetTypes")
      .then(response => {
        setFormFieldTypes(response.data);
      })
      .catch(error => {});

    addFormField();

    return () => {};
  }, []);

  return (
    <Form inverted>
      <Header as="h3" style={{ fontSize: "2em" }} inverted>
        Create a new Form
      </Header>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 1.</b> Select a name for your form
        </p>
        <Indent>
          <FormField>
            <label>Form Name</label>
            <input placeholder="Form Name" />
          </FormField>
        </Indent>
      </Group>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 2.</b> Select a type of form
        </p>
        <Indent>
          <FormSelect
            fluid
            label="Form Type"
            options={[
              { key: 0, text: "JSON Body Post", value: "0" },
              { key: 1, text: "HTML Form Post", value: "0" }
            ]}
            placeholder="Form Type"
          />
        </Indent>
      </Group>
      <Group>
        <p style={{ fontSize: "1.33em" }}>
          <b>Step 3.</b> Create & Configure your fields
        </p>
        {formFields.map((formField, fieldIndex) => (
          <FieldContainer>
            <Indent key={fieldIndex}>
              <p style={{ fontSize: "1.16em" }}>
                <Icon
                  name={formField.collapsed ? "plus" : "minus"}
                  onClick={() => collapseField(fieldIndex)}
                />
                <b>Field {fieldIndex + 1}</b>
              </p>
              <Indent
                style={{
                  display: formField.collapsed ? "none" : "block"
                }}
              >
                <FormField>
                  <label>Field Name</label>
                  <input placeholder="Field Name" />
                </FormField>

                <FormSelect
                  fluid
                  label="Field Type"
                  onChange={fieldTypeChange(fieldIndex)}
                  options={formFieldTypes.map((fieldType, fieldTypeIndex) => ({
                    key: fieldTypeIndex,
                    text: fieldType.name,
                    value: fieldType.formFieldTypeId
                  }))}
                  placeholder="Field Type"
                />

                {formField.formFieldTypeId > 0 && (
                  <FormSelect
                    fluid
                    label="Validation Type"
                    onChange={validationTypeChange(fieldIndex)}
                    value={formField.validation.type}
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
                {formField.validation.type === 2 && (
                  <Indent>
                    <FormTextArea
                      label="Custom validation script"
                      placeholder="JavaScript"
                    ></FormTextArea>
                  </Indent>
                )}
                {formField.validation.type === 1 &&
                  validationTypes[formField.formFieldTypeId] && (
                    <>
                      {formField.validation.rules.map((rule, ruleIndex) => (
                        <RuleContainer>
                          <Indent key={ruleIndex}>
                            <p style={{ fontSize: "1.1em" }}>
                              <Icon
                                name={
                                  formField.validation.rules[ruleIndex]
                                    .collapsed
                                    ? "plus"
                                    : "minus"
                                }
                                onClick={() =>
                                  collapseRule(fieldIndex, ruleIndex)
                                }
                              />
                              <b>Rule {ruleIndex + 1}</b>
                            </p>
                            <Indent
                              style={{
                                display: formField.validation.rules[ruleIndex]
                                  .collapsed
                                  ? "none"
                                  : "block"
                              }}
                            >
                              <FormSelect
                                fluid
                                label="Rule Type"
                                onChange={ruleTypeChange(fieldIndex, ruleIndex)}
                                options={validationTypes[
                                  formField.formFieldTypeId
                                ].map((validationType, index) => ({
                                  key: index,
                                  text: validationType.name,
                                  value: validationType.formValidationRuleTypeId
                                }))}
                                placeholder="Rule Type"
                              />
                              {formField.validation.rules[ruleIndex].type >
                                1 && (
                                <FormField>
                                  <label>Rule Value</label>
                                  <input placeholder="Value" />
                                </FormField>
                              )}
                            </Indent>
                            {ruleIndex ===
                              formField.validation.rules.length - 1 && (
                              <Button
                                color="green"
                                style={{ marginTop: 16 }}
                                onClick={() => addRule(fieldIndex)}
                              >
                                <Icon name="add circle" />
                                Add Rule
                              </Button>
                            )}
                          </Indent>
                        </RuleContainer>
                      ))}
                    </>
                  )}
              </Indent>
              {fieldIndex === formFields.length - 1 && (
                <Button
                  color="blue"
                  style={{ marginTop: 16 }}
                  onClick={addFormField}
                >
                  <Icon name="add circle" />
                  Add Field
                </Button>
              )}
            </Indent>
          </FieldContainer>
        ))}
      </Group>
    </Form>
  );
}
