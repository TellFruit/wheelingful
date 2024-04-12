import ProtectedWrapper from '../../auth/components/protected.wrapper';
import { useFetchReviewByBookQuery } from '../store/apis/review';
import { useParams, Navigate, useLocation } from 'react-router-dom';
import { READ_CONFIG } from '../configuration/read.config';

export default function ProtectedReviewWrapper({ children }) {
  const { bookId } = useParams();
  const { isSuccess } = useFetchReviewByBookQuery(bookId);
  const location = useLocation();

  return (
    <ProtectedWrapper>
      {!isSuccess ? (
        children
      ) : (
        <Navigate
          to={`/${READ_CONFIG.routes.group}/${bookId}`}
          state={{ from: location }}
          replace
        />
      )}
    </ProtectedWrapper>
  );
}
