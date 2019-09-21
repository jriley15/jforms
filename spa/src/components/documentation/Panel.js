import React from "react";

export default function Panel({ index, children }) {
  return (
    <>{children.filter(({ props: { tabIndex } }) => tabIndex === index)}</>
  );
}
