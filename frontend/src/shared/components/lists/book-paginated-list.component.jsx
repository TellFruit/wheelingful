import {
  Box,
  Grid,
  Card,
  CardActionArea,
  CardMedia,
  CardContent,
  Stack,
  Typography,
  Alert,
  Pagination,
} from '@mui/material';
import { Link } from 'react-router-dom';
import Markdown from 'react-markdown';

export function BookPaginatedList({
  isFetching,
  isError,
  error,
  redirectByGroup,
  serverPaginated,
  pageNumber,
  onPageChange,
}) {
  if (isFetching) {
    return (
      <Typography variant="h5" component="div">
        Pending...
      </Typography>
    );
  } else if (isError) {
    return (
      <Stack>
        <Alert severity="error">{error}</Alert>
      </Stack>
    );
  }

  return (
    <Box>
      <Grid container spacing={3}>
        {serverPaginated.items.map((book) => (
          <Grid item key={book.id} xs={12} sm={12} md={6} lg={6}>
            <Card>
              <CardActionArea
                component={Link}
                to={`/${redirectByGroup}/${book.id}`}
              >
                <Stack direction={'row'}>
                  <CardMedia
                    component="img"
                    image={book.coverUrl}
                    alt={book.title}
                    sx={{ width: 200 }}
                  />
                  <Stack direction={'column'} spacing={1}>
                    <CardContent sx={{ flex: '1 0 auto' }}>
                      <Typography variant="h6">{book.title}</Typography>
                      <Stack direction={'row'} spacing={1}>
                        <Typography variant="body2" color="textSecondary">
                          {book.category}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                          {book.status}
                        </Typography>
                      </Stack>
                      <Box
                        marginTop={1}
                        sx={{ height: 200, overflow: 'hidden' }}
                      >
                        <Typography variant="body2">
                          <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                            {book.description}
                          </Markdown>
                        </Typography>
                      </Box>
                    </CardContent>
                  </Stack>
                </Stack>
              </CardActionArea>
            </Card>
          </Grid>
        ))}
      </Grid>
      <Box display="flex" justifyContent="center" alignItems="center">
        <Pagination
          count={serverPaginated.pageCount}
          page={pageNumber}
          onChange={onPageChange}
          showFirstButton
          showLastButton
          color="primary"
          size="large"
          sx={{ marginTop: '20px' }}
        />
      </Box>
    </Box>
  );
}
