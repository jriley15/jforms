import React, { useEffect, useState } from "react";
import { Header, Icon, Button, Loader } from "semantic-ui-react";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import js from "react-syntax-highlighter/dist/esm/languages/hljs/javascript";
import dark from "react-syntax-highlighter/dist/esm/styles/hljs/a11y-dark";
import styled from "styled-components";
import Axios from "axios";
import { apiUrl } from "../../config";
import { Link } from "react-router-dom";

SyntaxHighlighter.registerLanguage("javascript", js);

const Code = styled.div`
  position: relative;
`;

export default function SnippetsTab({ formId }) {
  const [snippets, setSnippets] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Axios.get(apiUrl + "/Form/GetSnippets", {
      params: {
        formId: formId
      }
    }).then(response => {
      setSnippets(response.data);
      setLoading(false);
    });

    return () => {};
  }, []);

  return (
    <>
      <Header as="h2">Embedded Form</Header>
      <p style={{ fontSize: "1.1em" }}>
        Send this link below to your users for them to fill your form out using
        our UI if you don't have your own website or don't want to deal with
        code.
      </p>
      <p style={{ fontSize: "1.2em" }}>
        <Link to={"/form/" + formId}>
          {"https://localhost:3000/form/" + formId}
        </Link>
      </p>

      <Header as="h2">HTML Form</Header>
      <p style={{ fontSize: "1.1em" }}>Copy this into an html file</p>
      <Code>
        <Button
          content="Copy"
          icon="copy"
          inverted
          style={{ position: "absolute", right: 5, top: 10 }}
          size="small"
        />
        <SyntaxHighlighter
          style={dark}
          customStyle={{ borderRadius: 5, padding: 16, minHeight: 100 }}
        >
          {snippets.length > 0 ? snippets.find(s => s.type === 0).code : ""}
        </SyntaxHighlighter>
        <Loader active={loading} />
      </Code>

      <Header as="h2">JavaScript AJAX</Header>
      <p style={{ fontSize: "1.1em" }}>Copy this into your JavaScript</p>
      <Code>
        <Button
          content="Copy"
          icon="copy"
          inverted
          style={{ position: "absolute", right: 5, top: 10 }}
          size="small"
        />

        <SyntaxHighlighter
          style={dark}
          customStyle={{ borderRadius: 5, padding: 16, minHeight: 100 }}
        >
          {snippets.length > 0 ? snippets.find(s => s.type === 1).code : ""}
        </SyntaxHighlighter>
        <Loader active={loading} />
      </Code>

      <Header as="h2">JavaScript Fetch</Header>
      <p style={{ fontSize: "1.1em" }}>Copy this into your JavaScript</p>
      <Code>
        <Button
          content="Copy"
          icon="copy"
          inverted
          style={{ position: "absolute", right: 5, top: 10 }}
          size="small"
        />

        <SyntaxHighlighter
          style={dark}
          customStyle={{ borderRadius: 5, padding: 16, minHeight: 100 }}
        >
          {snippets.length > 0 ? snippets.find(s => s.type === 2).code : ""}
        </SyntaxHighlighter>
        <Loader active={loading} />
      </Code>
    </>
  );
}
