import React from "react";
import { Form, Radio, Checkbox } from "semantic-ui-react";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";

export default function InputField({
  field,
  onChange,
  onCheckboxChange,
  onDateChange,
  submission,
  errors
}) {
  switch (field.formFieldType.name) {
    case "String":
      return (
        <Form.Input
          placeholder={field.name}
          value={submission[field.name] ? submission[field.name] : ""}
          onChange={onChange(field.name)}
          error={errors[field.name] ? true : false}
        />
      );

    case "Number":
      return (
        <Form.Input
          placeholder={field.name}
          style={{ maxWidth: 75 }}
          value={submission[field.name] ? submission[field.name] : ""}
          onChange={onChange(field.name)}
          error={errors[field.name] ? true : false}
        />
      );

    case "CheckBox":
      return (
        <>
          {field.options.map((option, index) => (
            <Form.Field key={index}>
              <Checkbox
                label={option.value}
                name={option.value}
                value={option.value}
                checked={submission[field.name + "-" + option.value]}
                onChange={onCheckboxChange(field.name + "-" + option.value)}
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
              <Radio
                label={option.value}
                name={option.value}
                value={option.value}
                checked={submission[field.name] === option.value}
                onChange={onChange(field.name)}
              />
            </Form.Field>
          ))}
        </>
      );
    case "DropDown":
      return (
        <Form.Select
          fluid
          options={field.options.map((option, index) => ({
            key: option.value,
            text: option.value,
            value: option.value
          }))}
          value={submission[field.name]}
          onChange={onChange(field.name)}
          placeholder={field.name}
        />
      );
    case "Date":
      return (
        <DatePicker
          selected={submission[field.name]}
          onChange={onDateChange(field.name)}
        />
      );

    default:
      break;
  }
}
