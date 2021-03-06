import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "../pages/Home";
import styled from "styled-components";
import CreateForm from "../pages/CreateForm";
import Form from "./Form";
import { Container } from "semantic-ui-react";
import Documentation from "../pages/Documentation";

const Wrapper = styled(Container)`
  padding-top: 24px;
`;

export default function Routes({ children }) {
  return (
    <Router>
      {children}
      <Route path="/documentation/:id" component={Documentation} />
      <Route exact path="/documentation" component={Documentation} />
      <Wrapper>
        <Route exact path="/" component={Home} />
        <Route path="/form" component={Form} />
      </Wrapper>
    </Router>
  );
}
