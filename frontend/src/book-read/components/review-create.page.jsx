import ReviewMutationComponent from './shared/review-mutation';
import { usePublishReviewMutation } from '../store/apis/review';
import { renderValidationErrorsObject } from '../../shared';

export default function ReviewCreatePage() {
  const [publishReview, publishResults] = usePublishReviewMutation();

  const handleSubmit = (review) => {
    publishReview(review);
  };

  let error;
  if (publishResults.isError) {
    error = renderValidationErrorsObject(publishResults.error.data.errors);
  }

  return (
    <ReviewMutationComponent
      mutationTitle="Publish"
      onSubmit={handleSubmit}
      isLoading={publishResults.isLoading}
      error={error}
      isSuccess={publishResults.isSuccess}
      isError={publishResults.isError}
    />
  );
}
