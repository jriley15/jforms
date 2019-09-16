import React, { useState } from "react";
import useAuth from "../hooks/useAuth";
import { Route } from "react-router-dom";
import Login from "../components/Login";

export default function PrivateRoute({ component: Component, ...rest }) {
  const { getAuth, authState } = useAuth();
  const [showLogin, setShowLogin] = useState(!authState.authenticated);

  return (
    <>
      <Route
        {...rest}
        render={props =>
          getAuth() ? <Component {...props} /> : <>Not logged in</>
        }
      />
      <Login open={showLogin} close={() => setShowLogin(false)} />
    </>
  );
}
