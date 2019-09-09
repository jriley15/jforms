import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import CreateForm from "../pages/CreateForm";
import ViewForms from "../pages/ViewForms";
import ViewForm from "../pages/ViewForm";

export default function Form({ match: { url } }) {
  console.log(url);
  return (
    <Switch>
      <Route exact path={url} component={ViewForms} />
      <Route path={`${url}/create`} component={CreateForm} />
      <Route path={`${url}/:formId`} component={ViewForm} />
    </Switch>
  );
}
