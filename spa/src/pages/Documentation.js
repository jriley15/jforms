import React, { useState, useEffect } from "react";
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
import GetStarted from "../components/documentation/GetStarted";
import { Link } from "react-router-dom";
import CreateAForm from "../components/documentation/CreateAForm";
import Fields from "../components/documentation/Fields";
import Validation from "../components/documentation/Validation";
import Submissions from "../components/documentation/Submissions";
import Hooks from "../components/documentation/Hooks";

export default function Documentation({ match: { params } }) {
  const [index, setIndex] = useState(params.id ? parseInt(params.id, 10) : 0);

  useEffect(() => {
    setIndex(params.id ? parseInt(params.id, 10) : 0);
  }, [params.id]);

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
          <Menu.Menu style={{ color: "white", backgroundColor: "transparent" }}>
            <Menu.Item
              header
              as={Link}
              to="/documentation/0"
              active={index === 0}
            >
              Introduction
            </Menu.Item>
            <Menu.Item
              header
              as={Link}
              to="/documentation/1"
              active={index === 1}
            >
              Get Started
            </Menu.Item>
          </Menu.Menu>
        </Menu.Item>
        <Menu.Item>
          <Menu.Header>Forms</Menu.Header>
          <Menu.Menu style={{ color: "white", backgroundColor: "transparent" }}>
            <Menu.Item
              header
              as={Link}
              to="/documentation/2"
              active={index === 2}
            >
              Creating a form
            </Menu.Item>
            <Menu.Item
              header
              as={Link}
              to="/documentation/3"
              active={index === 3}
            >
              Fields
            </Menu.Item>
            <Menu.Item
              header
              as={Link}
              to="/documentation/4"
              active={index === 4}
            >
              Validation
            </Menu.Item>
            <Menu.Item
              header
              as={Link}
              to="/documentation/5"
              active={index === 5}
            >
              Submissions
            </Menu.Item>
            <Menu.Item
              header
              as={Link}
              to="/documentation/6"
              active={index === 6}
            >
              Hooks
            </Menu.Item>
          </Menu.Menu>
        </Menu.Item>
      </Sidebar>
      <div
        style={{ marginLeft: 200, padding: 32, color: "white", maxWidth: 900 }}
      >
        <Panel index={index}>
          <Introduction tabIndex={0} />
          <GetStarted tabIndex={1} />
          <CreateAForm tabIndex={2} />
          <Fields tabIndex={3} />
          <Validation tabIndex={4} />
          <Submissions tabIndex={5} />
          <Hooks tabIndex={6} />
        </Panel>
      </div>
    </>
  );
}
