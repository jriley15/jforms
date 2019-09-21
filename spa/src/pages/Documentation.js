import React, { useState } from "react";
import {
  Icon,
  Menu,
  Sidebar,
  Segment,
  Header,
  Image,
  Container,
  List
} from "semantic-ui-react";
import Panel from "../components/documentation/Panel";
import Introduction from "../components/documentation/Introduction";

export default function Documentation() {
  const [index, setIndex] = useState(0);

  return (
    <>
      <Sidebar
        as={Menu}
        animation="overlay"
        inverted
        direction="left"
        vertical
        visible
        style={{ top: 64, backgroundColor: "#222", width: 200 }}
      >
        <Menu.Item>
          <Header as="h3" inverted textAlign="center">
            Documentation
          </Header>
        </Menu.Item>
        <Menu.Item>
          <Menu.Header>Getting Started</Menu.Header>
          <Menu.Menu
            header
            style={{ color: "white", backgroundColor: "transparent" }}
          >
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(0);
              }}
              active={index === 0}
            >
              Introduction
            </Menu.Item>
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(1);
              }}
              active={index === 1}
            >
              Get Started
            </Menu.Item>
          </Menu.Menu>
        </Menu.Item>
        <Menu.Item>
          <Menu.Header>Forms</Menu.Header>
          <Menu.Menu
            header
            style={{ color: "white", backgroundColor: "transparent" }}
          >
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(2);
              }}
              active={index === 2}
            >
              Creating a form
            </Menu.Item>
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(3);
              }}
              active={index === 3}
            >
              Types
            </Menu.Item>
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(4);
              }}
              active={index === 4}
            >
              Validation
            </Menu.Item>
            <Menu.Item
              as="a"
              onClick={() => {
                setIndex(5);
              }}
              active={index === 5}
            >
              Submission
            </Menu.Item>
          </Menu.Menu>
        </Menu.Item>
      </Sidebar>
      <div style={{ marginLeft: 200, padding: 32, color: "white" }}>
        <Panel index={index}>
          <Introduction tabIndex={0} />
          <div tabIndex={1}>Test 1</div>
          <div tabIndex={2}>Test 2</div>
          <div tabIndex={3}>Test 3</div>
          <div tabIndex={4}>Test 4</div>
          <div tabIndex={5}>Test 5</div>
        </Panel>
      </div>
    </>
  );
}
