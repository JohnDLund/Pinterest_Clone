import Axios from "axios";

let baseUrl = window.location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

export const api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 5000,
  withCredentials: true
});


