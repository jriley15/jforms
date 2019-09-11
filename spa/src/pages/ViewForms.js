import React from "react";
import { Button, Table, Search, Breadcrumb, Divider } from "semantic-ui-react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const ButtonGroup = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;

export default function ViewForms() {
  /**
   *
   * Table showing a paginated list of forms
   * Button above to go to create form page
   * Search / sort fields
   *
   */

  return (
    <>
      <Breadcrumb size="massive">
        <Breadcrumb.Section active>Forms</Breadcrumb.Section>
      </Breadcrumb>
      <Divider inverted />
      <ButtonGroup>
        <Button
          icon="edit"
          content="Create Form"
          as={Link}
          to="/form/create"
          color="blue"
        />
        <Search />
      </ButtonGroup>
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
            <Table.HeaderCell>Form name</Table.HeaderCell>
            <Table.HeaderCell>Submissions</Table.HeaderCell>
            <Table.HeaderCell>Created on</Table.HeaderCell>
            <Table.HeaderCell>Options</Table.HeaderCell>
          </Table.Row>
        </Table.Header>

        <Table.Body>
          <Table.Row>
            <Table.Cell>John</Table.Cell>
            <Table.Cell>1,243</Table.Cell>
            <Table.Cell>None</Table.Cell>
            <Table.Cell>
              <Button as={Link} to="/form/dashboard/1" compact>
                View
              </Button>
            </Table.Cell>
          </Table.Row>
          <Table.Row>
            <Table.Cell>Jamie</Table.Cell>
            <Table.Cell>2,345</Table.Cell>
            <Table.Cell>Requires call</Table.Cell>
            <Table.Cell>
              <Button as={Link} to="/form/dashboard/1" compact>
                View
              </Button>
            </Table.Cell>
          </Table.Row>
          <Table.Row>
            <Table.Cell>Jill</Table.Cell>
            <Table.Cell>656,534</Table.Cell>
            <Table.Cell>None</Table.Cell>
            <Table.Cell>
              <Button as={Link} to="/form/dashboard/1" compact>
                View
              </Button>
            </Table.Cell>
          </Table.Row>
        </Table.Body>
      </Table>
    </>
  );
}
