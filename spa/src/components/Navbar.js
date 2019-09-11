import React, { useState } from "react";
import {
  Menu,
  Image,
  Icon,
  Button,
  Container,
  Segment,
  Dropdown
} from "semantic-ui-react";
import { Link } from "react-router-dom";
import styled from "styled-components";
import Login from "./Login";
import Register from "./Register";

export default function Navbar() {
  const [loginOpen, setLoginOpen] = useState(false);
  const [registerOpen, setRegisterOpen] = useState(false);

  return (
    <Segment
      inverted
      textAlign="center"
      vertical
      style={{ padding: 0, height: 64 }}
    >
      <Menu fixed="top" inverted size="large" style={{ height: "inherit" }}>
        <Menu.Item>
          <Image size="mini" src="https://react.semantic-ui.com/logo.png" />
        </Menu.Item>
        <Menu.Item as={Link} to="/">
          Home
        </Menu.Item>
        <Menu.Item as={Link} to="/">
          About
        </Menu.Item>
        <Menu.Item position="right">
          <Button
            icon="file alternate"
            content="Forms"
            toggle={false}
            as={Link}
            to="/form"
            color="teal"
            inverted
          />
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
        </Menu.Item>
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
