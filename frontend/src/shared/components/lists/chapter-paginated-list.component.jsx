/* eslint-disable no-unused-vars */
import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Stack,
  Typography,
  Alert,
  Pagination,
  Divider,
  useTheme,
} from '@mui/material';
import { Link } from 'react-router-dom';

const ListItemStyle = {
  textDecoration: 'none',
  color: 'inherit',
};

export function ChapterPaginatedList({
  isFetching,
  isError,
  redirectByGroup,
  bookId,
  serverPaginated,
  pageNumber,
  onPageChange,
}) {
  const theme = useTheme();

  if (isFetching) {
    return (
      <Typography variant="h5" component="div">
        Pending...
      </Typography>
    );
  } else if (isError) {
    return (
      <Stack>
        <Alert severity="error">Error occured!</Alert>
      </Stack>
    );
  }

  return (
    <Box>
      <Typography variant="h5" sx={{ textAlign: 'left', marginTop: 2 }}>
        Chapters
      </Typography>
      <Box marginTop={2}>
        <Divider />
      </Box>
      <List>
        {serverPaginated.items.map((chapter, index) => (
          <ListItem
            key={chapter.id}
            component={Link}
            sx={{
              ...ListItemStyle,
              backgroundColor:
                index % 2 === 0 ? 'inherit' : theme.palette.action.hover,
            }}
            to={`/${redirectByGroup}/${bookId}/chapters/${chapter.id}`}
          >
            <ListItemButton>
              <ListItemText primary={chapter.title} secondary={chapter.date} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Box display="flex" justifyContent="center" alignItems="center">
        <Pagination
          count={serverPaginated.pageCount}
          page={pageNumber}
          onChange={onPageChange}
          showFirstButton
          showLastButton
          color="primary"
          size="large"
          sx={{ marginTop: 3 }}
        />
      </Box>
    </Box>
  );
}
