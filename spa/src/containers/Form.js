import React from "react";
import { Switch, Route } from "react-router-dom";
import CreateForm from "../pages/CreateForm";
import ViewForms from "../pages/ViewForms";
import FormDashboard from "../pages/FormDashboard";
import ViewForm from "../pages/ViewForm";
import PrivateRoute from "./PrivateRoute";

export default function Form({ match: { url } }) {
  return (
    <Switch>
      <PrivateRoute exact path={url} component={ViewForms} />
      <PrivateRoute path={`${url}/create`} component={CreateForm} />
      <PrivateRoute
        path={`${url}/dashboard/:formId`}
        component={FormDashboard}
      />
      <Route path={`${url}/:formId`} component={ViewForm} />
    </Switch>
  );
}
