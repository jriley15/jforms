import React, { useState, useEffect } from "react";
import useCollapse from "react-collapsed";
import styled from "styled-components";
import { Icon } from "semantic-ui-react";

const Indent = styled.div`
  padding-left: 32px;
`;

export default function Collapsible({ header, children }) {
  const [isOpen, setOpen] = useState(false);
  const { getCollapseProps, getToggleProps } = useCollapse({ isOpen });

  useEffect(() => {
    setOpen(true);
    return () => {};
  }, []);

  return (
    <Indent>
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          paddingBottom: "16px"
        }}
        onClick={() => setOpen(oldOpen => !oldOpen)}
      >
        <Icon name={isOpen ? "minus" : "plus"} />
        {header}
      </div>

      <div {...getCollapseProps()}>{children}</div>
    </Indent>
  );
}
