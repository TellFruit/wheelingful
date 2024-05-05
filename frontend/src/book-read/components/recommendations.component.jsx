import { RecommendationPaginatedList } from '../../shared';
import { useParams } from 'react-router-dom';
import { useFetchRecommendationsByBookQuery } from '../store/apis/recommendation';
import { READ_CONFIG } from '../configuration/read.config';

export default function Recommendations() {
  const { bookId } = useParams();

  const { data, isFetching, isError, error } =
    useFetchRecommendationsByBookQuery(bookId);

  let errorMessage;
  if (isError) {
    errorMessage = error.error;
  }

  return (
    <RecommendationPaginatedList
      data={data}
      redirectByGroup={`${READ_CONFIG.routes.group}`}
      isFetching={isFetching}
      isError={isError}
      error={errorMessage}
    />
  );
}
