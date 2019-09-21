import React, { useState } from "react";
import {
  Menu,
  Image,
  Icon,
  Button,
  Container,
  Segment,
  Dropdown,
  Header
} from "semantic-ui-react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import Login from "./Login";
import Register from "./Register";
import useAuth from "../hooks/useAuth";

export default function Navbar() {
  const [loginOpen, setLoginOpen] = useState(false);
  const [registerOpen, setRegisterOpen] = useState(false);
  const { getAuth, logout, authState } = useAuth();

  //should prob be in a side effect to avoid blocking before first render
  const auth = getAuth();

  return (
    <Segment
      inverted
      textAlign="center"
      vertical
      style={{ padding: 0, height: 64 }}
    >
      <Menu fixed="top" inverted size="large" style={{ height: "inherit" }}>
        <Menu.Item as={Link} to="/" style={{ backgroundColor: "transparent" }}>
          <div
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              alignContent: "center"
            }}
          >
            <Image size="tiny" src="https://images.jrdn.tech/jformslogo.png" />
            <Header inverted style={{ margin: 0, paddingRight: 10 }}>
              JForms
            </Header>
          </div>
        </Menu.Item>

        <Menu.Item as={Link} to="/documentation">
          Documentation
        </Menu.Item>
        <Menu.Item position="right">
          {!auth ? (
            <>
              <Button
                inverted
                style={{ marginLeft: "0.5em" }}
                onClick={() => setLoginOpen(true)}
              >
                Log in
              </Button>
              <Button
                inverted
                style={{ marginLeft: "0.5em" }}
                onClick={() => setRegisterOpen(true)}
              >
                Sign up
              </Button>
            </>
          ) : (
            <>
              <Button
                icon="file alternate"
                content="Forms"
                toggle={false}
                as={Link}
                to="/form"
                color="teal"
                inverted
              />
              {/*<Button inverted style={{ marginLeft: "0.5em" }} onClick={logout}>
                Log out
          </Button>*/}
            </>
          )}
        </Menu.Item>
        {auth && (
          <Dropdown
            item
            text={
              authState.identity.avatar ? (
                <Image src={authState.identity.avatar} avatar />
              ) : (
                "Account"
              )
            }
          >
            <Dropdown.Menu>
              <Dropdown.Header>Profile</Dropdown.Header>
              <Dropdown.Item onClick={logout}>Logout</Dropdown.Item>
              <Dropdown.Item>Settings</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>
        )}
      </Menu>
      <Login
        open={loginOpen}
        close={() => {
          setLoginOpen(false);
        }}
      />
      <Register
        open={registerOpen}
        close={() => {
          setRegisterOpen(false);
        }}
      />
    </Segment>
  );
}
