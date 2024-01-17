import {
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";
import Layout from "./components/layout";
import AppWrapper from "./components/app-wrapper";

const router = createBrowserRouter([
  {
    id: "root",
    path: "/",
    Component: Layout,
    children: [
        
    ],
  }
]);

export default function App() {
  return (
    <AppWrapper>
      <RouterProvider
        router={router}
        fallbackElement={<p>Initial Load...</p>}
      />
    </AppWrapper>
  );
}
