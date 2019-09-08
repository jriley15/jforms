import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import CreateForm from "../pages/CreateForm";
import ViewForms from "../pages/ViewForms";

export default function Form({ match: { url } }) {
  console.log(url);
  return (
    <>
      <Route exact path={url} component={ViewForms} />
      <Route path={url + "/create"} component={CreateForm} />
    </>
  );
}
