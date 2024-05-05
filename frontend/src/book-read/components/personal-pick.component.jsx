import { BookList } from '../../shared';
import { useFetchRecommendationsByUserQuery } from '../store/apis/recommendation';
import { useFetchReviewsByUserQuery } from '../store/apis/review';
import { READ_CONFIG } from '../configuration/read.config';
import { Alert, Stack, Button } from '@mui/material';

import CachedIcon from '@mui/icons-material/Cached';

function RecommendationItems() {
  const { data, error, isFetching, isError, refetch } =
    useFetchRecommendationsByUserQuery();

  let errorMessage;
  if (isError) {
    errorMessage = error.error;
  }

  const onReload = () => {
    refetch();
  };

  return (
    <Stack direction={'column'} alignItems={'center'} sx={{ width: '100%' }}>
      <Button
        variant="contained"
        onClick={onReload}
        sx={{ marginBottom: '30px', width: '50%' }}
      >
        <CachedIcon sx={{ marginRight: '0.5rem' }} /> Next pick
      </Button>
      <BookList
        isFetching={isFetching}
        isError={isError}
        error={errorMessage}
        redirectByGroup={READ_CONFIG.routes.group}
        data={data}
      />
    </Stack>
  );
}

export default function PersonalPick() {
  const { data, isFetching, isError } = useFetchReviewsByUserQuery({
    pageNumber: 1,
    pageSize: 1,
  });

  if (!isError && !isFetching && data.items.length) {
    return <RecommendationItems />;
  }

  console.log(data);
  return (
    <Alert severity="info">
      Leave more reviews to generate a selection of recommendations
    </Alert>
  );
}
