import { AppBar, Toolbar, Typography, Button, Divider } from '@mui/material';
import { Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { signOut } from '../../auth';
import { PUBLISH_CONFIG } from '../../book-publish';
import { AUTH_CONFIG } from '../../auth';

export default function Header() {
  const dispatch = useDispatch();
  const { isSignedIn } = useSelector((state) => state.authSlice);

  const handleLogOut = () => {
    dispatch(signOut());
  };

  return (
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
          to={`/${PUBLISH_CONFIG.routes.group}/${PUBLISH_CONFIG.routes.booksByCurrentUser}`}
        >
          Write
        </Button>
        <Divider
          orientation="vertical"
          flexItem
          variant="middle"
          sx={{ marginLeft: 2, marginRight: 2, border: '1px solid' }}
        />
        {isSignedIn ? (
          <Button color="inherit" onClick={handleLogOut}>
            Log Out
          </Button>
        ) : (
          <Button
            color="inherit"
            component={Link}
            to={`/${AUTH_CONFIG.routes.group}/${AUTH_CONFIG.routes.login}`}
          >
            Sign Up
          </Button>
        )}
      </Toolbar>
    </AppBar>
  );
}
