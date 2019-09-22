import React, { useState } from "react";
import { Header, Button, Divider, Image } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import js from "react-syntax-highlighter/dist/esm/languages/hljs/javascript";
import dark from "react-syntax-highlighter/dist/esm/styles/hljs/a11y-dark";
SyntaxHighlighter.registerLanguage("javascript", js);

const jsSnippet = `fetch('https://forms.jrdn.tech:44333/Submit/31', {
    method: 'POST',
    body: JSON.stringify({
        date: '', // Type: Date
        radio: '', // options: (1, 2, 3, 4)
        dropdown: '', // options: (1, 2, 3, 4)
        checkbox: '', // options: (1, 2, 3, 4)
    }),
    headers:{
        'Content-Type': 'application/json'
    }
}).then(res => res.json())
.then(response => console.log('Success:', JSON.stringify(response)))
.catch(error => console.error('Error:', error));`;

const errorJson = `{
  success: false,
  errors: {
    date: ["Required"],
    radio: ["Required"],
    "drop down": ["Required"],
    "check box": ["Required"]
  }
}`;

export default function Submissions() {
  return (
    <div>
      <Header as="h1" inverted>
        Form Submissions
      </Header>
      <Divider />
      <Header as="h3" inverted>
        How to create submissions
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        All submissions are created via our back-end POST action. This end-point
        is open for you to create submissions from your website progamatically
        with an HTTP Post request, or via our embedded interface which renders
        your form on our website. Once you create a form, we generate several
        snippets for you to get started with including an example using plain
        HTML markup, an example using a vanilla JavaScript XMLHTTPRequest, and
        an example using the vanilla JavaScript Fetch api.
      </p>
      <Header as="h4" inverted>
        JavaScript Example
      </Header>
      <SyntaxHighlighter
        style={dark}
        customStyle={{ borderRadius: 5, padding: 16, minHeight: 100 }}
      >
        {jsSnippet}
      </SyntaxHighlighter>
      <Header as="h3" inverted>
        Validation Errors
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        If the submission fails validation, an errors response will be sent back
        from the server. Here's an example of an error response in JSON so that
        you can prepare to display it on your front-end.
      </p>
      <SyntaxHighlighter
        style={dark}
        customStyle={{ borderRadius: 5, padding: 16, minHeight: 100 }}
      >
        {errorJson}
      </SyntaxHighlighter>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Each field with errors will be a key in the error object with an array
        of strings that represent the error messages.
      </p>
      <Header as="h3" inverted>
        Viewing submissions
      </Header>
      <p style={{ fontSize: "1.2em", lineHeight: "1.5" }}>
        Submissions to your form can be viewed from the form dashboard.
      </p>
      <Image src="https://images.jrdn.tech/formsubmissions.JPG" />
    </div>
  );
}
