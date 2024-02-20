import { CssBaseline, Container, Box, Toolbar } from '@mui/material';
import { Outlet } from 'react-router-dom';
import Header from './header.component';
import Footer from './footer.component';

export default function Layout() {
  return (
    <Box flexGrow={1}>
      <CssBaseline />
      <Header />
      <Container
        component="main"
        maxWidth="lg"
        sx={{ minHeight: '100vh', marginTop: 4 }}
      >
        <Toolbar />
        <Outlet />
      </Container>
      <Footer />
    </Box>
  );
}
