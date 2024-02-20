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
import { Link, Outlet } from 'react-router-dom';
import { READ_CONFIG } from '../configuration/read.config';

const ListItemStyle = {
  textDecoration: 'none',
  color: 'inherit',
};

export default function ReadDrawer() {
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
              to={`/${READ_CONFIG.routes.group}/${READ_CONFIG.routes.browseBooks}`}
            >
              <ListItemButton>
                <ListItemIcon>
                  <ListAltIcon />
                </ListItemIcon>
                <ListItemText primary={'Browse books'} />
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
