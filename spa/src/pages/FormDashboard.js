import React, { useState, useEffect } from "react";
import { Header, Tab, Breadcrumb, Divider } from "semantic-ui-react";
import { Link } from "react-router-dom";
import SnippetsTab from "../components/forms/SnippetsTab";
import useRequest from "../hooks/useRequest";

export default function FormDashboard({ match: { params } }) {
  const [form, setForm] = useState("");
  const { get } = useRequest();

  useEffect(() => {
    async function getForm() {
      let response = await get("/Form/Get", { formId: params.formId });
      setForm(response.data);
    }
    getForm();
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
