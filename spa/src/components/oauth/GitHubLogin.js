import React, { Component } from "react";
import PopupWindow from "./PopupWindow";
import { toQuery } from "./utils";
import { Button } from "semantic-ui-react";

class GitHubLogin extends Component {
  static defaultProps = {
    buttonText: "Sign in with GitHub",
    redirectUri: "",
    scope: "read:user",
    onRequest: () => {},
    onSuccess: () => {},
    onFailure: () => {}
  };

  onBtnClick = () => {
    const { clientId, scope, redirectUri } = this.props;
    const search = toQuery({
      client_id: clientId,
      scope,
      redirect_uri: redirectUri,
      login: ""
    });

    const y = window.top.outerHeight / 2 + window.top.screenY - 750 / 2;
    const x = window.top.outerWidth / 2 + window.top.screenX - 500 / 2;

    const popup = (this.popup = PopupWindow.open(
      "github-oauth-authorize",
      `https://github.com/login/oauth/authorize?${search}`,

      { height: 750, width: 500, left: x, top: y }
    ));

    this.onRequest();
    popup.then(data => this.onSuccess(data), error => this.onFailure(error));
  };

  onRequest = () => {
    this.props.onRequest();
  };

  onSuccess = data => {
    if (!data.code) {
      return this.onFailure(new Error("'code' not found"));
    }

    this.props.onSuccess(data);
  };

  onFailure = error => {
    this.props.onFailure(error);
  };

  render() {
    const { className, buttonText, children } = this.props;
    const attrs = { onClick: this.onBtnClick };

    if (className) {
      attrs.className = className;
    }

    return (
      <Button
        content="Sign in with GitHub"
        icon="github"
        fluid
        {...attrs}
      ></Button>
    );
  }
}

export default GitHubLogin;
