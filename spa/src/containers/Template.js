import React, { useEffect } from "react";
import Routes from "./Routes";
import NavBar from "../components/Navbar";
import { Segment } from "semantic-ui-react";
import useAuth from "../hooks/useAuth";

export default function Template() {
  const { getAuthFromStorage } = useAuth();

  useEffect(() => {
    getAuthFromStorage();
    return () => {};
  }, []);

  return (
    <Segment
      inverted
      vertical
      style={{
        minHeight: "100vh",
        backgroundColor: "#303030",
        paddingTop: 0,
        paddingBottom: 64
      }}
    >
      <Routes>
        <NavBar />
      </Routes>
    </Segment>
  );
}
