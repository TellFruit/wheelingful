import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { ChapterPaginatedList, SHARED_CONFIG } from '../../shared';
import { useFetchChaptersByBookQuery } from '../store/apis/publish-chapter.api';
import { PUBLISH_CONFIG } from '../configuration/publish.config';

export default function ChaptersByBook() {
  const { bookId } = useParams();

  const [pageNumber, setPageNumber] = useState(1);

  const pageSize = SHARED_CONFIG.pagination.defaultPageSize;

  const { data, error, isFetching, isError } = useFetchChaptersByBookQuery({
    bookId,
    pageNumber,
    pageSize,
  });

  const handlePageChange = (event, value) => {
    setPageNumber(value);
  };

  let errorMessage;
  if (isError) {
    errorMessage = error.error;
  }

  return (
    <ChapterPaginatedList
      isFetching={isFetching}
      isError={isError}
      error={errorMessage}
      redirectByGroup={PUBLISH_CONFIG.routes.group}
      bookId={bookId}
      serverPaginated={data}
      pageNumber={pageNumber}
      onPageChange={handlePageChange}
    />
  );
}
