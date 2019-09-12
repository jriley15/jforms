import React, { Component, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import constants from "../constants/authConstants";
import jwt_decode from "jwt-decode";

export default function useAuth() {
  const dispatch = useDispatch();

  const authState = useSelector(state => state.auth);

  const getAuthFromStorage = () => {
    const token = localStorage.getItem(constants.LOCAL_STORAGE_ID);

    if (token) {
      //check if expired??
      //refresh??
      set(token);
    }
  };

  const setAuth = authToken => {
    set(authToken);
  };

  const set = token => {
    dispatch({
      type: constants.SET_AUTH,
      authToken: token,
      identity: jwt_decode(token)
    });

    localStorage.setItem(constants.LOCAL_STORAGE_ID, token);
  };

  const logout = () => {
    dispatch({
      type: constants.REMOVE_AUTH
    });

    localStorage.removeItem(constants.LOCAL_STORAGE_ID);
  };

  return { getAuthFromStorage, setAuth, authState, logout };
}
