import React, { useState, useEffect } from "react";
import {
  Modal,
  Button,
  Form,
  Header,
  Divider,
  List,
  Dimmer,
  Loader
} from "semantic-ui-react";
import { Formik } from "formik";
import * as Yup from "yup";
import useRequest from "../hooks/useRequest";
import GitHubLogin from "./oauth/GitHubLogin";
import useAuth from "../hooks/useAuth";
import GoogleLogin from "react-google-login";

export default function Register({ open, close }) {
  const { post } = useRequest();
  const { setAuth } = useAuth();
  const { get } = useRequest();
  const [errors, setErrors] = useState([]);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    setErrors([]);

    return () => {};
  }, []);

  const onGithubSuccess = async res => {
    setSubmitting(true);
    var response = await get("/Auth/GithubLogin", { code: res.code });

    if (response.success) {
      setAuth(response.data);
      close();
    } else {
      setErrors(response.errors);
    }
    setSubmitting(false);
  };
  const onGithubFailure = response => {
    console.error(response);
  };
  const responseGoogle = async res => {
    if (res.code) {
      setSubmitting(true);
      var response = await get("/Auth/GoogleLogin", { code: res.code });

      if (response.success) {
        setAuth(response.data);
        close();
      } else {
        setErrors(response.errors);
      }
      setSubmitting(false);
    }
  };

  return (
    <Formik
      initialValues={{
        email: "jordanr35@live.com",
        password: "Password123!",
        confirmPassword: "Password123!"
      }}
      onSubmit={async values => {
        let response = await post("/Auth/Register", values);

        if (response.success) {
        } else {
          setErrors(response.errors);
        }

        setSubmitting(false);
      }}
      validationSchema={Yup.object().shape({
        email: Yup.string()
          .email()
          .required("Required"),
        password: Yup.string().required("Required"),
        confirmPassword: Yup.string().required("Required")
      })}
    >
      {props => {
        const {
          values,
          touched,
          dirty,
          isSubmitting,
          handleChange,
          handleBlur,
          handleSubmit,
          handleReset
        } = props;
        return (
          <Modal
            open={open}
            onClose={close}
            size="tiny"
            style={{
              pointerEvents: submitting ? "none" : "auto"
            }}
          >
            <Header
              icon="lock"
              content="Sign up"
              style={{ backgroundColor: "rgb(48, 48, 48)", color: "white" }}
            />
            <Modal.Content style={{ backgroundColor: "rgb(48, 48, 48)" }}>
              <Form inverted onSubmit={handleSubmit}>
                {errors["*"] && (
                  <List>
                    {errors["*"].map((error, index) => (
                      <List.Item key={index} style={{ color: "red" }}>
                        {error}
                      </List.Item>
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
                    disabled
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
                    disabled
                  />
                </Form.Field>
                <Form.Field>
                  <Form.Input
                    label="Confirm Password"
                    placeholder="Confirm Password"
                    type="password"
                    id="confirmPassword"
                    value={values.confirmPassword}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    error={
                      errors.confirmPassword && touched.confirmPassword
                        ? errors.confirmPassword
                        : false
                    }
                    disabled
                  />
                </Form.Field>
              </Form>

              <Divider inverted horizontal>
                Or
              </Divider>
              <GoogleLogin
                clientId="470701960790-unn86aca3ofmh7388132gev7vinrq7ba.apps.googleusercontent.com"
                render={renderProps => (
                  <Button
                    color="green"
                    content="Sign in with Google"
                    icon="google"
                    fluid
                    onClick={renderProps.onClick}
                  />
                )}
                buttonText="Login"
                onSuccess={responseGoogle}
                onFailure={responseGoogle}
                cookiePolicy={"single_host_origin"}
                redirectUri="http://localhost:3000"
                responseType="code"
              />
              <Divider inverted horizontal>
                Or
              </Divider>
              <GitHubLogin
                clientId="9b290dccc70c0fe5f259"
                redirectUri="http://localhost:3000"
                onSuccess={onGithubSuccess}
                onFailure={onGithubFailure}
              />
            </Modal.Content>
            <Modal.Actions style={{ backgroundColor: "rgb(48, 48, 48)" }}>
              <Button onClick={close}>Cancel</Button>
              <Button
                color="blue"
                type="submit"
                onClick={handleSubmit}
                disabled={true /*isSubmitting*/}
              >
                Submit
              </Button>
            </Modal.Actions>
            <Dimmer active={submitting}>
              <Loader active={submitting} size="big">
                Loading
              </Loader>
            </Dimmer>
          </Modal>
        );
      }}
    </Formik>
  );
}
