import React, { useState, useEffect } from "react";
import Axios from "axios";
import { apiUrl } from "../config";
import { Header, Tab, Breadcrumb, Divider } from "semantic-ui-react";
import { Link } from "react-router-dom";
import SnippetsTab from "../components/forms/SnippetsTab";

export default function FormDashboard({ match: { params } }) {
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
        <Tab.Pane inverted>
          <pre>{JSON.stringify(form, null, 2)}</pre>
        </Tab.Pane>
      )
    },
    {
      menuItem: "Snippets",
      render: () => (
        <Tab.Pane inverted>
          <SnippetsTab formId={params.formId} />
        </Tab.Pane>
      )
    },
    {
      menuItem: "Submissions",
      render: () => <Tab.Pane inverted>Tab 2 Content</Tab.Pane>
    },
    {
      menuItem: "Hooks",
      render: () => <Tab.Pane inverted>Tab 1 Content</Tab.Pane>
    },
    {
      menuItem: "Edit Form",
      render: () => <Tab.Pane inverted>Tab 3 Content</Tab.Pane>
    },
    {
      menuItem: "Options",
      render: () => <Tab.Pane inverted>Tab 1 Content</Tab.Pane>
    },
    {
      menuItem: "Help",
      render: () => <Tab.Pane inverted>Tab 1 Content</Tab.Pane>
    }
  ];

  return (
    <>
      <Breadcrumb size="massive">
        <Breadcrumb.Section link as={Link} to="/form">
          Forms
        </Breadcrumb.Section>
        <Breadcrumb.Divider icon="right chevron" />
        <Breadcrumb.Section active>
          {form.name &&
            form.name.charAt(0).toUpperCase() + form.name.substring(1)}
        </Breadcrumb.Section>
      </Breadcrumb>
      <Divider inverted />
      <Tab
        panes={panes}
        style={{ marginTop: 32 }}
        defaultActiveIndex={1}
        menu={{
          attached: true,
          tabular: true,
          inverted: true,
          style: { border: "1px solid #555", borderRadius: 3 }
        }}
      />
    </>
  );
}
