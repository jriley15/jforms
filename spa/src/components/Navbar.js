import React from "react";
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

export default function Navbar() {
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
          <Dropdown
            item
            text={
              <>
                <Icon name="file alternate" size="large" />
                Forms
              </>
            }
          >
            <Dropdown.Menu>
              <Dropdown.Item as={Link} to="/form/create">
                <Icon name="add circle" /> Create Form
              </Dropdown.Item>
              <Dropdown.Item as={Link} to="/form">
                <Icon name="wpforms" /> View Forms
              </Dropdown.Item>
              <Dropdown.Item>Test</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>
          <Button as={Link} to="/" inverted style={{ marginLeft: "1em" }}>
            Log in
          </Button>
          <Button as={Link} to="/" inverted style={{ marginLeft: "0.5em" }}>
            Sign up
          </Button>
        </Menu.Item>
      </Menu>
    </Segment>
  );
}
