import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout";
import AppWrapper from "./components/app-wrapper";
import { authRootRoutes } from "../auth";
import { ThemeProvider } from "@mui/material";
import { theme } from "./material-ui";

const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: Layout,
    children: [...authRootRoutes],
  },
]);

function App() {
  return (
    <AppWrapper>
      <ThemeProvider theme={theme}>
        <RouterProvider
          router={router}
          fallbackElement={<p>Initial Load...</p>}
        />
      </ThemeProvider>
    </AppWrapper>
  );
}

export default App;
