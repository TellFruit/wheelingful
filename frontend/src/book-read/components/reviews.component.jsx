import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetchReviewsByBookQuery } from '../store/apis/review';
import { SHARED_CONFIG, ReviewPaginatedList } from '../../shared';

import FormatQuoteIcon from '@mui/icons-material/FormatQuote';
import { Stack, Typography } from '@mui/material';

export default function Reviews() {
  const [currentPage, setCurrentPage] = useState(1);
  const { bookId } = useParams();
  const { data, isFetching } = useFetchReviewsByBookQuery({
    bookId,
    pageNumber: currentPage,
    pageSize: SHARED_CONFIG.pagination.defaultPageSize,
  });

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  return (
    <>
      <Stack spacing={1} direction={'row'} alignItems={'center'} marginTop={2}>
        <FormatQuoteIcon />
        <Typography variant="h5" sx={{ textAlign: 'left' }}>
          Reviews
        </Typography>
      </Stack>
      <ReviewPaginatedList
        isFetching={isFetching}
        serverPaginated={data}
        pageNumber={currentPage}
        onPageChange={handlePageChange}
      />
    </>
  );
}
