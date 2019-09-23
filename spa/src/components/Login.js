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
import useAuth from "../hooks/useAuth";
import GitHubLogin from "./oauth/GitHubLogin";
import GoogleLogin from "react-google-login";

export default function Login({ open, close }) {
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
        email: "",
        password: ""
      }}
      onSubmit={async values => {
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
                <Form.Field disabled>
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
                <Form.Field disabled>
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
                type="submit"
                color="blue"
                onClick={handleSubmit}
                disabled={true /*isSubmitting*/}
              >
                Login
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
