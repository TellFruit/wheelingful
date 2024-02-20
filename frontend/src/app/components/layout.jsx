import {
  Container,
  Box,
} from '@mui/material';
import { Outlet } from 'react-router-dom';
import Header from './header.component';
import Footer from './footer.component';

export default function Layout() {
  return (
    <Box flexGrow={1}>
      <Header />
      <Container
        component="main"
        maxWidth="lg"
        sx={{ marginTop: 8, minHeight: '100vh' }}
      >
        <Outlet />
      </Container>
      <Footer />
    </Box>
  );
}
