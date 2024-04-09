/* eslint-disable no-unused-vars */
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
} from "@mui/material";
import { useState } from "react";
import ListAltIcon from "@mui/icons-material/ListAlt";
import { Link, Outlet, useParams } from "react-router-dom";
import { READ_CONFIG } from "../configuration/read.config";
import { useSelector } from "react-redux";
import { useGetReviewByBookQuery, useDeleteReviewByBookMutation } from "../store/apis/review";

const ListItemStyle = {
  textDecoration: "none",
  color: "inherit",
};

export default function ReadDrawer() {
  const { bookId } = useParams();
  const { isSignedIn } = useSelector((state) => state.authSlice);

  const { data, error, isFetching, isError } = useGetReviewByBookQuery(bookId);
  const [deleteReview] = useDeleteReviewByBookMutation();

  if (!(bookId && isSignedIn)) {
    return (
      <Stack direction={"row"}>
        <Drawer
          variant="permanent"
          sx={{
            width: 240,
            flexShrink: 0,
            [`& .MuiDrawer-paper`]: { width: 240, boxSizing: "border-box" },
          }}
        >
          <Toolbar />
          <Box sx={{ overflow: "auto" }}>
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
                  <ListItemText primary={"Browse books"} />
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

  function handleReviewDelete() {
    deleteReview(bookId)
  }

  return (
    <Stack direction={"row"}>
      <Drawer
        variant="permanent"
        sx={{
          width: 240,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: 240, boxSizing: "border-box" },
        }}
      >
        <Toolbar />
        <Box sx={{ overflow: "auto" }}>
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
                <ListItemText primary={"Browse books"} />
              </ListItemButton>
            </ListItem>
            {!data ? (
              <ListItem
                component={Link}
                sx={ListItemStyle}
                to={`/${READ_CONFIG.routes.group}/${bookId}/review`}
              >
                <ListItemButton>
                  <ListItemIcon>
                    <ListAltIcon />
                  </ListItemIcon>
                  <ListItemText primary={"Review book"} />
                </ListItemButton>
              </ListItem>
            ) : (
              <>
                {" "}
                <ListItem
                  component={Link}
                  sx={ListItemStyle}
                  to={`/${READ_CONFIG.routes.group}/${bookId}/review`}
                >
                  <ListItemButton>
                    <ListItemIcon>
                      <ListAltIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Update review"} />
                  </ListItemButton>
                </ListItem>
                <ListItem
                  sx={ListItemStyle}
                >
                  <ListItemButton onClick={handleReviewDelete}>
                    <ListItemIcon>
                      <ListAltIcon />
                    </ListItemIcon>
                    <ListItemText primary={"Delete review"} />
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
