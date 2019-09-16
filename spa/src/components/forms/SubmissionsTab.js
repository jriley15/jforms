import React, { useEffect, useState } from "react";
import {
  Header,
  Icon,
  Button,
  Loader,
  Divider,
  Table
} from "semantic-ui-react";
import styled from "styled-components";
import useRequest from "../../hooks/useRequest";

export default function SubmissionsTab({ formId }) {
  const [form, setForm] = useState({});
  const [loading, setLoading] = useState(true);
  const { get } = useRequest();

  useEffect(() => {
    async function getSubmissions() {
      let response = await get("/Form/GetWithSubmissions", { formId: formId });

      if (response.success) {
        setForm(response.data);
        setLoading(false);
      }
    }
    getSubmissions();
    return () => {};
  }, []);

  return (
    <>
      {form.formId ? (
        <>
          <Header as="h3">Total: {form.submissions.length}</Header>

          <Table
            celled
            selectable
            inverted
            style={{
              boxShadow:
                "0px 1px 3px 0px rgba(0,0,0,0.2), 0px 1px 1px 0px rgba(0,0,0,0.14), 0px 2px 1px -1px rgba(0,0,0,0.12)",
              textAlign: "center"
            }}
          >
            <Table.Header style={{ backgroundColor: "#1b1c1d" }}>
              <Table.Row>
                <Table.HeaderCell>Date</Table.HeaderCell>
                {form.fields.map((field, index) => (
                  <Table.HeaderCell key={index}>{field.name}</Table.HeaderCell>
                ))}
              </Table.Row>
            </Table.Header>

            <Table.Body>
              {form.submissions.map((submission, index) => (
                <Table.Row key={index}>
                  <Table.Cell key={index}>
                    {new Date(submission.createdOn).toLocaleDateString()}
                  </Table.Cell>
                  {form.fields.map((field, index) => {
                    const value = submission.values.find(
                      v => v.field.formFieldId === field.formFieldId
                    );
                    return <Table.Cell key={index}>{value.value}</Table.Cell>;
                  })}
                </Table.Row>
              ))}
            </Table.Body>
          </Table>
        </>
      ) : (
        <div style={{ height: 100 }}>
          <Loader active={loading} />
        </div>
      )}
    </>
  );
}
