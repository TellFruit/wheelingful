import { useFetchBooksByCurrentUserQuery } from '../store/apis/publish-book.api';
import { useState } from 'react';
import { BookPaginatedList, SHARED_CONFIG } from '../../shared';
import { PUBLISH_CONFIG } from '../configuration/publish.config';

export default function BooksByCurrentUser() {
  const [pageNumber, setPageNumber] = useState(1);

  const pageSize = SHARED_CONFIG.pagination.defaultPageSize;

  const { data, error, isFetching, isError } = useFetchBooksByCurrentUserQuery({
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
    <BookPaginatedList
      isFetching={isFetching}
      isError={isError}
      error={errorMessage}
      redirectByGroup={PUBLISH_CONFIG.routes.group}
      serverPaginated={data}
      pageNumber={pageNumber}
      onPageChange={handlePageChange}
    />
  );
}
