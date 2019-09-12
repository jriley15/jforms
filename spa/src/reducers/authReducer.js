import constants from "../constants/authConstants.js";

const initialState = {
  authenticated: false,
  authToken: "",
  identity: {}
};

const reducer = (state, action) => {
  state = state || initialState;

  switch (action.type) {
    case constants.SET_AUTH:
      return {
        ...state,
        authenticated: true,
        authToken: action.authToken,
        identity: action.identity
      };

    case constants.REMOVE_AUTH:
      return {
        initialState
      };

    default:
      return state;
  }
};

export default reducer;
