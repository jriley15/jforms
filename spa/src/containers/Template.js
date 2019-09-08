import React, { useEffect } from "react";
import Routes from "./Routes";
import NavBar from "../components/Navbar";
import { Segment } from "semantic-ui-react";

export default function Template() {
  return (
    <Segment
      inverted
      vertical
      style={{ minHeight: "100vh", backgroundColor: "#303030", padding: 0 }}
    >
      <Routes>
        <NavBar />
      </Routes>
    </Segment>
  );
}
