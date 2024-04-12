import ReviewMutationComponent from './shared/review-mutation';
import {
  useUpdateReviewMutation,
  useFetchReviewByBookQuery,
} from '../store/apis/review';
import { useParams } from 'react-router-dom';
import { renderValidationErrorsObject } from '../../shared';

export default function ReviewCreatePage() {
  const { bookId } = useParams();
  const { data: review } = useFetchReviewByBookQuery(bookId);

  const [updateReview, updateResults] = useUpdateReviewMutation();

  const handleSubmit = (review) => {
    updateReview(review);
  };

  let error;
  if (updateResults.isError) {
    error = renderValidationErrorsObject(updateResults.error.data.errors);
  }

  return (
    <ReviewMutationComponent
      mutationTitle="Update"
      onSubmit={handleSubmit}
      review={review}
      isLoading={updateResults.isLoading}
      error={error}
      isSuccess={updateResults.isSuccess}
      isError={updateResults.isError}
    />
  );
}
