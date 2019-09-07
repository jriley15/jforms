import React, { useEffect } from "react";
import { Provider } from "react-redux";
import store from "./store/configureStore";
import Home from "./pages/Home";
import Template from "./containers/Template";

export default function App() {
  return (
    <Provider store={store}>
      <Template />
    </Provider>
  );
}
