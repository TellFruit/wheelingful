import { useCreateBookMutation } from '../store/apis/publish-book.api';
import BookMutationComponent from './shared/book-mutation.component';
import { renderValidationErrorsObject } from '../../shared';

export default function BookCreatePage() {
  const [createBook, results] = useCreateBookMutation();

  const handleSubmit = (newBook) => {
    createBook(newBook);
  };

  let error;
  if (results.isError) {
    error = renderValidationErrorsObject(results.error.data.errors);
  }

  return (
    <BookMutationComponent
      headerTitle="Publish a new book"
      mutationTitle="Publish"
      error={error}
      onSubmit={handleSubmit}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
      isError={results.isError}
    />
  );
}
