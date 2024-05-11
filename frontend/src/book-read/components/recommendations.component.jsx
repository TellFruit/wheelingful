import { BookPaginatedSlick } from '../../shared';
import { useParams } from 'react-router-dom';
import { useFetchRecommendationsByBookQuery } from '../store/apis/recommendation';
import { READ_CONFIG } from '../configuration/read.config';

import RecommendIcon from '@mui/icons-material/Recommend';
import { Stack, Typography } from '@mui/material';

export default function SimilarBooks() {
  const { bookId } = useParams();

  const { data, isFetching, isError, error } =
    useFetchRecommendationsByBookQuery(bookId);

  let errorMessage;
  if (isError) {
    errorMessage = error.error;
  }

  return (
    <>
      <Stack spacing={1} direction={'row'} alignItems={'center'} marginTop={2}>
        <RecommendIcon />
        <Typography variant="h5" sx={{ textAlign: 'left' }}>
          Recommendations
        </Typography>
      </Stack>
      <BookPaginatedSlick
        data={data}
        redirectByGroup={`${READ_CONFIG.routes.group}`}
        isFetching={isFetching}
        isError={isError}
        error={errorMessage}
      />
    </>
  );
}
