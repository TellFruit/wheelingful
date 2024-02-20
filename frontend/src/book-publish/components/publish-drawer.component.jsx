import {
  Box,
  Stack,
  Drawer,
  Toolbar,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Divider,
} from '@mui/material';
import ListAltIcon from '@mui/icons-material/ListAlt';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { Link, Outlet } from 'react-router-dom';
import { PUBLISH_CONFIG } from '../configuration/publish.config';

const ListItemStyle = {
  textDecoration: 'none',
  color: 'inherit'
};

export default function PublishDrawer() {
  return (
    <Stack direction={'row'}>
      <Drawer
        variant="permanent"
        sx={{
          width: 240,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: 240, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <Box sx={{ overflow: 'auto' }}>
          <List>
            <ListItem
              component={Link}
              sx={ListItemStyle}
              to={`/${PUBLISH_CONFIG.routes.group}/${PUBLISH_CONFIG.routes.booksByCurrentUser}`}
            >
              <ListItemButton>
                <ListItemIcon>
                  <ListAltIcon />
                </ListItemIcon>
                <ListItemText primary={'Your published books'} />
              </ListItemButton>
            </ListItem>
            <ListItem
              component={Link}
              sx={ListItemStyle}
              to={`/${PUBLISH_CONFIG.routes.group}/${PUBLISH_CONFIG.routes.publishBook}`}
            >
              <ListItemButton>
                <ListItemIcon>
                  <AddCircleOutlineIcon />
                </ListItemIcon>
                <ListItemText primary={'Publish book'} />
              </ListItemButton>
            </ListItem>
          </List>
          <Divider />
        </Box>
      </Drawer>
      <Outlet />
    </Stack>
  );
}
