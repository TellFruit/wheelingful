import {
  AppBar,
  Toolbar,
  Container,
  Box,
  Typography,
  Button,
} from '@mui/material';
import { Outlet, Link } from 'react-router-dom';
import { PUBLISH_CONFIG } from '../../book-publish';

export default function Layout() {
  return (
    <Box flexGrow={1}>
      <AppBar position="fixed">
        <Toolbar>
          <Typography
            variant="h5"
            component={Link}
            to="/"
            color="primary.contrastText"
            sx={{ flexGrow: 1, textDecoration: 'none' }}
          >
            Wheelingful
          </Typography>
          <Button color="inherit">Read</Button>
          <Button
            color="inherit"
            component={Link}
            to={`/${PUBLISH_CONFIG.routes.router.group}/${PUBLISH_CONFIG.routes.router.booksByCurrentUser}`}
          >
            Write
          </Button>
        </Toolbar>
      </AppBar>
      <Container
        component="main"
        maxWidth="lg"
        sx={{ marginTop: 8, minHeight: '100vh' }}
      >
        <Outlet />
      </Container>
      <Box
        component="footer"
        sx={{
          padding: 2,
          marginTop: 'auto',
          textAlign: 'center',
        }}
      >
        <Typography variant="body2" color="textSecondary">
          © {new Date().getFullYear()} Wheelingful project. Released under the
          MIT License.
        </Typography>
      </Box>
    </Box>
  );
}
