import React from "react";
import useCollapse from "react-collapsed";
import styled from "styled-components";
import { Icon } from "semantic-ui-react";

const Indent = styled.div`
  padding-left: 32px;
`;

export default function Collapsible({ header, children }) {
  const { getCollapseProps, getToggleProps, isOpen } = useCollapse({
    defaultOpen: true
  });

  return (
    <Indent>
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          paddingBottom: "8px"
        }}
      >
        <Icon name={isOpen ? "minus" : "plus"} {...getToggleProps()} />
        {header}
      </div>

      <div {...getCollapseProps()}>{children}</div>
    </Indent>
  );
}
