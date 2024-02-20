import {
  useDeleteBookMutation,
  useFetchBooksByIdQuery,
  useUpdateBookMutation,
} from '../store/apis/publish.api';
import { Typography, Alert, Stack } from '@mui/material';
import BookMutationComponent from './shared/book-mutation.component';
import { renderValidationErrorsObject } from '../../shared';
import { useParams } from 'react-router-dom';

export default function BookUpdatePage() {
  const { bookId } = useParams();

  const {
    data: book,
    error: fetchingError,
    isFetching,
    isError: isFailedFetching,
  } = useFetchBooksByIdQuery(bookId);

  const [updateBook, updateResults] = useUpdateBookMutation();
  const [deleteBook, deleteResults] = useDeleteBookMutation();

  const isSuccess = updateResults.isSuccess || deleteResults.isSuccess;
  const isLoading = updateResults.isLoading || deleteResults.isLoading;
  const isError = updateResults.isError || deleteResults.isError;

  const handleSubmit = (updatedBook) => {
    updateBook(updatedBook);
  };

  const handleDelete = (id) => {
    deleteBook(id);
  };

  let error;
  if (updateResults.isError) {
    error = renderValidationErrorsObject(updateResults.error.data.errors);
  } else if (deleteResults.isError) {
    error = renderValidationErrorsObject(deleteResults.error.data.errors);
  }

  if (isFetching) {
    return (
      <Typography variant="h5" component="div" sx={{ marginTop: 2 }}>
        Pending...
      </Typography>
    );
  } else if (isFailedFetching) {
    error = renderValidationErrorsObject(fetchingError.data.errors);

    return (
      <Stack sx={{ marginTop: 2 }}>
        <Alert severity="error">{error}</Alert>
      </Stack>
    );
  }

  return (
    <BookMutationComponent
      headerTitle="Update a book"
      mutationTitle="Update"
      error={error}
      book={book}
      onSubmit={handleSubmit}
      onDelete={handleDelete}
      isLoading={isLoading}
      isSuccess={isSuccess}
      isError={isError}
    />
  );
}
