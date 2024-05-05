/* eslint-disable no-unused-vars */

import {
  Box,
  Stack,
  Typography,
  Rating,
  CardMedia,
  Card,
  CardActionArea,
  Divider,
  Pagination,
  Alert,
} from '@mui/material';
import { Link } from 'react-router-dom';
import RecommendIcon from '@mui/icons-material/Recommend';
import { useState, useEffect } from 'react';

function RecommendationItem({ book, redirectByGroup }) {
  return (
    <Box sx={{ marginRight: '10px', padding: 1 }}>
      <Card elevation={2}>
        <CardActionArea
          component={Link}
          to={`/${redirectByGroup}/${book.id}`}
          sx={{ padding: 1 }}
        >
          <Stack
            spacing={2}
            flexDirection={'column'}
            justifyContent={'space-between'}
            alignItems={'center'}
          >
            <CardMedia
              component="img"
              image={book.coverUrl}
              alt={book.title}
              sx={{ width: 200 }}
            />
            <Typography variant="body2" color="textSecondary">
              {book.title}
            </Typography>
            <Rating value={book.averageScore} precision={0.5} readOnly />
          </Stack>
        </CardActionArea>
      </Card>
    </Box>
  );
}

const PAGE_SIZE = 3;

export function RecommendationPaginatedList({
  data,
  redirectByGroup,
  isFetching,
  isError,
  error,
}) {
  const [pagination, setPagination] = useState({
    pageCount: data ? Math.ceil(data.length / PAGE_SIZE) : null,
    pageNumber: 1,
  });

  useEffect(() => {
    setPagination({
      ...pagination,
      pageCount: data ? Math.ceil(data.length / PAGE_SIZE) : null,
    });
  }, [data]);

  console.log(data?.length, pagination);

  const items = data
    ? data.slice(
        (pagination.pageNumber - 1) * PAGE_SIZE,
        (pagination.pageNumber - 1) * PAGE_SIZE + PAGE_SIZE
      )
    : null;

  const onPageChange = (event, page) => {
    setPagination({
      ...pagination,
      pageNumber: page,
    });
  };

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
      <Stack spacing={1} direction={'row'} alignItems={'center'} marginTop={2}>
        <RecommendIcon />
        <Typography variant="h5" sx={{ textAlign: 'left' }}>
          Recommendations
        </Typography>
      </Stack>
      <Box marginTop={2}>
        <Divider sx={{ mb: 2 }} />
        <Stack
          spacing={1}
          direction={'row'}
          alignItems={'center'}
          justifyContent={'space-evenly'}
          marginTop={2}
        >
          {items
            ? items.map((book) => (
                <RecommendationItem
                  key={book.id}
                  book={book}
                  redirectByGroup={redirectByGroup}
                />
              ))
            : null}
        </Stack>
      </Box>
      <Box display="flex" justifyContent="center" alignItems="center">
        <Pagination
          count={pagination.pageCount}
          page={pagination.pageNumber}
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
