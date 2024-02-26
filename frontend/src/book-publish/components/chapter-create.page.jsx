import { useParams } from 'react-router-dom';
import { useCreateChapterMutation } from '../store/apis/publish-chapter.api';
import { renderValidationErrorsObject } from '../../shared';
import ChapterMutationComponent from './shared/chapter-mutation.component';

export default function ChapterCreatePage() {
  const { bookId } = useParams();

  const [createChapter, results] = useCreateChapterMutation();

  const handleSubmit = (newChapter) => {
    createChapter(newChapter);
  };

  let error;
  if (results.isError) {
    error = renderValidationErrorsObject(results.error.data.errors);
  }

  return (
    <ChapterMutationComponent
      headerTitle="Publish a new chapter"
      mutationTitle="Publish"
      bookId={bookId}
      error={error}
      onSubmit={handleSubmit}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
      isError={results.isError}
    />
  );
}
