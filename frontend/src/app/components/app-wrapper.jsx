import React from "react";
import { APP_CONFIG } from "../configuration/app-config";

function AppWrapper({ children }) {
  return APP_CONFIG.isDevelopment ? (
    <React.StrictMode>{children}</React.StrictMode>
  ) : (
    children
  );
}

export default AppWrapper;
