import React, { useEffect, useState } from "react";
import qs from "query-string";
import { List, Header, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";

//form submits get redirected to here to display errors or success and a button to return to the origin page

export default function FormSubmit({ location }) {
  const [response, setResponse] = useState(
    JSON.parse(qs.parse(location.search, { ignoreQueryPrefix: true }).response)
  );

  console.log(response);

  return (
    <>
      {!response.Success ? (
        <>
          <Header color="red">Submission Failed</Header>
          <List>
            {Object.keys(response.Errors).length > 0 &&
              Object.keys(response.Errors).map((key, index) => (
                <List.Item key={index}>
                  {key}:{" "}
                  {response.Errors[key].map((error, errorIndex) => (
                    <span key={errorIndex} style={{ color: "red" }}>
                      {(errorIndex > 0 ? ", " : "") + error}
                    </span>
                  ))}
                </List.Item>
              ))}
          </List>
        </>
      ) : (
        <Header color="green">Submission Success</Header>
      )}

      <Button as="a" href={response.Data}>
        Return to previous page
      </Button>
    </>
  );
}
