import React, { useState, useEffect } from "react";
import Axios from "axios";
import { apiUrl } from "../config";
import { Header, Tab, Breadcrumb } from "semantic-ui-react";
import { Link } from "react-router-dom";
import SnippetsTab from "../components/forms/SnippetsTab";

export default function ViewForm({ match: { params } }) {
  console.log(params);

  const [form, setForm] = useState("");

  useEffect(() => {
    Axios.get(apiUrl + "/Form/Get", { params: { formId: params.formId } })
      .then(response => {
        setForm(response.data);
      })
      .catch(error => {});
    return () => {};
  }, []);

  const panes = [
    {
      menuItem: "JSON",
      render: () => (
        <Tab.Pane>
          <pre>{JSON.stringify(form, null, 2)}</pre>
        </Tab.Pane>
      )
    },
    {
      menuItem: "Snippets",
      render: () => (
        <Tab.Pane>
          <SnippetsTab />
        </Tab.Pane>
      )
    },
    {
      menuItem: "Submissions",
      render: () => <Tab.Pane>Tab 2 Content</Tab.Pane>
    },
    {
      menuItem: "Hooks",
      render: () => <Tab.Pane>Tab 1 Content</Tab.Pane>
    },
    {
      menuItem: "Edit Form",
      render: () => <Tab.Pane>Tab 3 Content</Tab.Pane>
    },
    {
      menuItem: "Options",
      render: () => <Tab.Pane>Tab 1 Content</Tab.Pane>
    },
    {
      menuItem: "Help",
      render: () => <Tab.Pane>Tab 1 Content</Tab.Pane>
    }
  ];

  return (
    <>
      <Breadcrumb size="massive">
        <Breadcrumb.Section link as={Link} to="/form">
          Forms
        </Breadcrumb.Section>
        <Breadcrumb.Divider icon="right chevron" />
        <Breadcrumb.Section active>{form.name}</Breadcrumb.Section>
      </Breadcrumb>

      <Tab
        panes={panes}
        style={{ marginTop: 32 }}
        defaultActiveIndex={1}
        menu={{
          attached: true,
          tabular: true,
          inverted: true
        }}
      />
    </>
  );
}
