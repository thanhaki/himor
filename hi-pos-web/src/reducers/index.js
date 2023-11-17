import { combineReducers } from "redux";
import message from "./message";
import auth from "./auth";
import donvi from "./donvi";
import mdata from "./mdata";
import customer from "./customer";
export default combineReducers({
  message,
  auth,
  donvi,
  mdata,
  customer,
});