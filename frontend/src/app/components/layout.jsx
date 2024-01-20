import Container from "@mui/material/Container";
import { Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <div>
      <nav>Navbar placeholder</nav>
      <Container maxWidth="lg">
        <Outlet />
      </Container>
    </div>
  );
}
