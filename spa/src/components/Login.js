import React from "react";
import { Modal, Button, Form, Header, Divider, List } from "semantic-ui-react";
import { Formik } from "formik";
import * as Yup from "yup";
import useRequest from "../hooks/useRequest";
import useAuth from "../hooks/useAuth";

export default function Login({ open, close }) {
  const { post } = useRequest();
  const { setAuth } = useAuth();

  return (
    <Formik
      initialValues={{
        email: "jordanr35@live.com",
        password: "Password123!"
      }}
      onSubmit={async (values, { setSubmitting, setErrors }) => {
        let response = await post("/Auth/Login", values);

        if (response.success) {
          setAuth(response.data);
          close();
        } else {
          setErrors(response.errors);
        }

        setSubmitting(false);
      }}
      validationSchema={Yup.object().shape({
        email: Yup.string()
          .email()
          .required("Required"),
        password: Yup.string().required("Required")
      })}
    >
      {props => {
        const {
          values,
          touched,
          errors,
          dirty,
          isSubmitting,
          handleChange,
          handleBlur,
          handleSubmit,
          handleReset
        } = props;
        return (
          <Modal open={open} onClose={close} size="tiny">
            <Header
              icon="lock"
              content="Sign in"
              style={{ backgroundColor: "rgb(48, 48, 48)", color: "white" }}
            />
            <Modal.Content style={{ backgroundColor: "rgb(48, 48, 48)" }}>
              <Form inverted onSubmit={handleSubmit}>
                {errors["*"] && (
                  <List>
                    {errors["*"].map((error, index) => (
                      <List.Item style={{ color: "red" }}>{error}</List.Item>
                    ))}
                  </List>
                )}
                <Form.Field>
                  <Form.Input
                    label="Email"
                    id="email"
                    value={values.email}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    placeholder="Email Address"
                    error={errors.email && touched.email ? errors.email : false}
                  />
                </Form.Field>
                <Form.Field>
                  <Form.Input
                    label="Password"
                    type="password"
                    id="password"
                    value={values.password}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    placeholder="Password"
                    error={
                      errors.password && touched.password
                        ? errors.password
                        : false
                    }
                  />
                </Form.Field>
              </Form>

              <Divider inverted horizontal>
                Or
              </Divider>
              <Button
                color="green"
                content="Sign in with Google"
                icon="google"
                fluid
              />
              <Divider inverted horizontal>
                Or
              </Divider>
              <Button content="Sign in with GitHub" icon="github" fluid />
            </Modal.Content>
            <Modal.Actions style={{ backgroundColor: "rgb(48, 48, 48)" }}>
              <Button onClick={close}>Cancel</Button>
              <Button
                type="submit"
                color="blue"
                onClick={handleSubmit}
                disabled={isSubmitting}
              >
                Login
              </Button>
            </Modal.Actions>
          </Modal>
        );
      }}
    </Formik>
  );
}
