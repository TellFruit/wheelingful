import { RecommendationPaginatedList } from '../../shared';
import { useParams } from 'react-router-dom';
import { useFetchRecommendationsByBookQuery } from '../store/apis/recommendation';
import { READ_CONFIG } from '../configuration/read.config';

export default function Recommendations() {
  const { bookId } = useParams();

  // eslint-disable-next-line no-unused-vars
  const { data, isFetching } = useFetchRecommendationsByBookQuery(bookId);
  return (
    <RecommendationPaginatedList
      data={data}
      redirectByGroup={`${READ_CONFIG.routes.group}`}
    />
  );
}
