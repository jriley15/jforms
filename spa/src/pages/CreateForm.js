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

  const removeFormField = () => {
    if (formFields.length > 1) {
      const fields = [...formFields];
      fields.splice(fields.length - 1, 1);
      setFormFields(fields);
    }
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

  const removeRule = fieldIndex => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules.splice(
      fields[fieldIndex].validation.rules.length - 1,
      1
    );
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

  const fieldTypeChange = fieldIndex => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].formFieldTypeId = value;
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

  const validationTypeChange = fieldIndex => (e, { value }) => {
    setValidationType(fieldIndex, value);

    if (value === 1) {
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

  const ruleTypeChange = (fieldIndex, ruleIndex) => (e, { value }) => {
    const fields = [...formFields];
    fields[fieldIndex].validation.rules[ruleIndex].type = value;
    setFormFields(fields);
  };

  const formNameChange = (e, { value }) => {
    setFormName(value);
  };

  const formTypeChange = (e, { value }) => {
    setFormType(value);
    addFormField();
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
            <b>Step 1.</b> Select a name for your form
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
            {formFields.map((formField, fieldIndex) => (
              <FieldContainer key={fieldIndex}>
                <Collapsible
                  header={
                    <p style={{ fontSize: "1.16em" }}>
                      <b>Field {fieldIndex + 1}</b>
                    </p>
                  }
                >
                  <Indent>
                    <FormField>
                      <label>Field Name</label>
                      <input placeholder="Field Name" />
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
                            <RuleContainer key={ruleIndex}>
                              <Indent>
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
                                    display: formField.validation.rules[
                                      ruleIndex
                                    ].collapsed
                                      ? "none"
                                      : "block"
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
                                      formField.formFieldTypeId
                                    ].map((validationType, index) => ({
                                      key: index,
                                      text: validationType.name,
                                      value:
                                        validationType.formValidationRuleTypeId
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
                                  <div style={{ marginTop: 16 }}>
                                    <Button
                                      color="yellow"
                                      style={{ marginTop: 16 }}
                                      onClick={() => addRule(fieldIndex)}
                                      inverted
                                    >
                                      <Icon name="add circle" />
                                      Add Rule
                                    </Button>
                                    <Button
                                      inverted
                                      color="red"
                                      onClick={() => removeRule(fieldIndex)}
                                    >
                                      <Icon name="x" /> Remove
                                    </Button>
                                  </div>
                                )}
                              </Indent>
                            </RuleContainer>
                          ))}
                        </>
                      )}
                  </Indent>
                </Collapsible>
                {fieldIndex === formFields.length - 1 && (
                  <div style={{ marginTop: 16 }}>
                    <Button color="blue" onClick={addFormField} inverted>
                      <Icon name="add circle" />
                      Add Field
                    </Button>
                    <Button inverted color="red" onClick={removeFormField}>
                      <Icon name="x" /> Remove
                    </Button>
                  </div>
                )}
              </FieldContainer>
            ))}
          </Group>
        )}
      </Indent>

      {formFields.length > 0 && (
        <div style={{ marginTop: 32 }}>
          <Button color="green" inverted size="large">
            Submit Form
          </Button>
          <Button color="grey" inverted size="large">
            Clear
          </Button>
        </div>
      )}
    </Form>
  );
}
