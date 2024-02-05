import {
  Typography,
  Grid,
  Box,
  Card,
  CardMedia,
  CardContent,
  Alert,
  Stack,
  Pagination,
} from '@mui/material';
import { styled } from '@mui/material/styles';
import {
  useCountBooksPaginationPagesQuery,
  useFetchBooksByCurrentUserQuery,
} from '../store/apis/publush-api';
import { useState } from 'react';
import { PUBLISH_CONFIG } from '../configuration/publish-config';

const CardContentNoPadding = styled(CardContent)(`
  padding: 8px;
  &:last-child {
    padding-bottom: 8px;
  }
`);

export default function BooksByCurrentUser() {
  const [pageNumber, setPageNumber] = useState(1);

  const pageSize = PUBLISH_CONFIG.pagination.pageSizeDefault;

  const {
    data: books,
    error,
    isFetching,
  } = useFetchBooksByCurrentUserQuery({
    pageNumber,
    pageSize,
  });

  const { data: pageCount } = useCountBooksPaginationPagesQuery({
    pageSize,
  });

  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  if (isFetching) {
    return (
      <Typography gutterBottom variant="h5" component="div">
        Pending...
      </Typography>
    );
  } else if (error) {
    return (
      <Stack>
        <Alert severity="error">{error.error}</Alert>
      </Stack>
    );
  }

  return (
    <div>
      <Grid container spacing={3} sx={{ marginTop: 1 }}>
        {books.map((book) => (
          <Grid item key={book.id} xs={12} sm={6} md={4} lg={3}>
            <Card sx={{ maxWidth: 250 }}>
              <CardMedia
                component="img"
                image={book.coverUrl}
                alt={book.title}
              />
              <CardContentNoPadding>
                <Typography gutterBottom variant="h6" color="textSecondary">
                  {book.title}
                </Typography>
                <Stack direction={'row'} spacing={1}>
                  <Typography variant="body2" color="textSecondary">
                    {book.category}
                  </Typography>
                  <Typography variant="body2" color="textSecondary">
                    {book.status}
                  </Typography>
                </Stack>
              </CardContentNoPadding>
            </Card>
          </Grid>
        ))}
      </Grid>
      <Box display="flex" justifyContent="center" alignItems="center">
        <Pagination
          count={pageCount}
          page={pageNumber}
          onChange={handlePageChange}
          showFirstButton
          showLastButton
          color="primary"
          size="large"
          sx={{ marginTop: '20px' }}
        />
      </Box>
    </div>
  );
}
