import React from "react";
import { Header, Icon, Button } from "semantic-ui-react";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import js from "react-syntax-highlighter/dist/esm/languages/hljs/javascript";
import dark from "react-syntax-highlighter/dist/esm/styles/hljs/a11y-dark";
import styled from "styled-components";

SyntaxHighlighter.registerLanguage("javascript", js);

const Code = styled.div`
  position: relative;
`;

export default function SnippetsTab() {
  var ajaxCode = `var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function() {
  if (this.readyState == 4 && this.status == 200) {
    document.getElementById("demo").innerHTML = this.responseText;
  }
};
xhttp.open("GET", "ajax_info.txt", true);
xhttp.send();`;

  const fetchCode = `fetch('http://example.com/movies.json')
.then(function(response) {
  return response.json();
})
.then(function(myJson) {
  console.log(JSON.stringify(myJson));
});`;

  return (
    <>
      <Header as="h2">Embedded Form</Header>
      <p style={{ fontSize: "1.1em" }}>
        Send <a href="">this</a> link to your users for them to fill your form
        out using our UI if you don't have your own website or don't want to
        deal with code.
      </p>

      <Header as="h2">HTML Form</Header>
      <p style={{ fontSize: "1.1em" }}>
        Send <a href="">this</a> link to your users for them to fill your form
        out on our UI if you don't have your own website or don't want to deal
        with code.
      </p>
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
          customStyle={{ borderRadius: 5, padding: 16 }}
        >
          {`<form>
  <input type="text" name="name">
</form>`}
        </SyntaxHighlighter>
      </Code>

      <Header as="h2">JavaScript AJAX</Header>
      <p style={{ fontSize: "1.1em" }}>
        Send <a href="">this</a> link to your users for them to fill your form
        out on our UI if you don't have your own website or don't want to deal
        with code.
      </p>
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
          customStyle={{ borderRadius: 5, padding: 16 }}
        >
          {ajaxCode}
        </SyntaxHighlighter>
      </Code>

      <Header as="h2">JavaScript Fetch</Header>
      <p style={{ fontSize: "1.1em" }}>
        Send <a href="">this</a> link to your users for them to fill your form
        out on our UI if you don't have your own website or don't want to deal
        with code.
      </p>
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
          customStyle={{ borderRadius: 5, padding: 16 }}
        >
          {fetchCode}
        </SyntaxHighlighter>
      </Code>
    </>
  );
}
