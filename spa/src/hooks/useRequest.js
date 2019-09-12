import useAuth from "./useAuth";
import Axios from "axios";
import { formatErrorResponse } from "../helpers/errorHelper";
import { apiUrl } from "../config";

export default function useRequest() {
  const { authState } = useAuth();

  const post = async (url, body) => {
    let headers = {
      "Content-Type": "application/json"
    };

    if (authState.authenticated) {
      headers = { ...headers, Authorization: "Bearer " + authState.authToken };
    }

    let response = {};

    await Axios.post(apiUrl + url, body, {
      headers: {
        headers
      }
    })
      .then(res => {
        response = res.data;
      })
      .catch(error => {
        response = formatErrorResponse(error);
      });

    return response;
  };

  const get = async (url, params) => {
    let headers = {};

    if (authState.authenticated) {
      headers = { Authorization: "Bearer " + authState.authToken };
    }

    let response = {};

    await Axios.get(
      apiUrl + url,
      { params: params },
      {
        headers: {
          headers
        }
      }
    )
      .then(res => {
        response = res.data;
      })
      .catch(error => {
        response = formatErrorResponse(error);
      });

    return response;
  };

  return { get, post };
}
