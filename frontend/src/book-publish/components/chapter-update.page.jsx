import { useParams } from 'react-router-dom';
import {
  useDeleteChapterMutation,
  useFetchChapterByIdQuery,
  useUpdateChapterMutation,
} from '../store/apis/publish-chapter.api';
import { Typography, Alert, Stack } from '@mui/material';
import { renderValidationErrorsObject } from '../../shared';
import ChapterMutationComponent from './shared/chapter-mutation.component';

export default function ChapterUpdatePage() {
  const { bookId, chapterId } = useParams();

  const {
    data: chapter,
    error: fetchingError,
    isFetching,
    isError: isFailedFetching,
  } = useFetchChapterByIdQuery(chapterId);

  const [updateChapter, updateResults] = useUpdateChapterMutation();
  const [deleteChapter, deleteResults] = useDeleteChapterMutation();

  const isSuccess = updateResults.isSuccess || deleteResults.isSuccess;
  const isLoading = updateResults.isLoading || deleteResults.isLoading;
  const isError = updateResults.isError || deleteResults.isError;

  const handleSubmit = (updatedChapter) => {
    updateChapter(updatedChapter);
  };

  const handleDelete = (bookId, chapterId) => {
    deleteChapter({
      bookId,
      chapterId,
    });
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
    <ChapterMutationComponent
      headerTitle="Update the chapter"
      mutationTitle="Update"
      error={error}
      chapter={chapter}
      bookId={bookId}
      onSubmit={handleSubmit}
      onDelete={handleDelete}
      isLoading={isLoading}
      isSuccess={isSuccess}
      isError={isError}
    />
  );
}
