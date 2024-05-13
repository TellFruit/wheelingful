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
import AddCommentIcon from '@mui/icons-material/AddComment';
import RemoveCircleIcon from '@mui/icons-material/RemoveCircle';
import UpdateIcon from '@mui/icons-material/Update';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';

import { Link, Outlet, useParams } from 'react-router-dom';
import { READ_CONFIG } from '../configuration/read.config';
import {
  useFetchReviewByBookQuery,
  useDeleteReviewByBookMutation,
} from '../store/apis/review';

const ListItemStyle = {
  textDecoration: 'none',
  color: 'inherit',
};

function DefaultDrawer() {
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
            <ListItem
              component={Link}
              sx={ListItemStyle}
              to={`/${READ_CONFIG.routes.group}/${READ_CONFIG.routes.personalPick}`}
            >
              <ListItemButton>
                <ListItemIcon>
                  <LocalLibraryIcon />
                </ListItemIcon>
                <ListItemText primary={'Personal Pick'} />
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

function BookDrawer({ bookId }) {
  const [deleteReview] = useDeleteReviewByBookMutation();
  const { isSuccess } = useFetchReviewByBookQuery(bookId);

  const handleReviewDelete = () => {
    deleteReview(bookId);
  };

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

            <ListItem
              component={Link}
              sx={ListItemStyle}
              to={`/${READ_CONFIG.routes.group}/${READ_CONFIG.routes.personalPick}`}
            >
              <ListItemButton>
                <ListItemIcon>
                  <LocalLibraryIcon />
                </ListItemIcon>
                <ListItemText primary={'Personal Pick'} />
              </ListItemButton>
            </ListItem>

            {!isSuccess ? (
              <ListItem
                component={Link}
                sx={ListItemStyle}
                to={`/${READ_CONFIG.routes.group}/${bookId}/review/new`}
              >
                <ListItemButton>
                  <ListItemIcon>
                    <AddCommentIcon />
                  </ListItemIcon>
                  <ListItemText primary={'Review book'} />
                </ListItemButton>
              </ListItem>
            ) : (
              <>
                <ListItem
                  component={Link}
                  sx={ListItemStyle}
                  to={`/${READ_CONFIG.routes.group}/${bookId}/review/update`}
                >
                  <ListItemButton>
                    <ListItemIcon>
                      <UpdateIcon />
                    </ListItemIcon>
                    <ListItemText primary={'Update review'} />
                  </ListItemButton>
                </ListItem>
                <ListItem sx={ListItemStyle}>
                  <ListItemButton onClick={handleReviewDelete}>
                    <ListItemIcon>
                      <RemoveCircleIcon />
                    </ListItemIcon>
                    <ListItemText primary={'Delete review'} />
                  </ListItemButton>
                </ListItem>
              </>
            )}
          </List>
          <Divider />
        </Box>
      </Drawer>
      <Outlet />
    </Stack>
  );
}

export default function ReadDrawer() {
  const { bookId } = useParams();

  if (!bookId) {
    return <DefaultDrawer />;
  }

  return <BookDrawer bookId={bookId} />;
}
