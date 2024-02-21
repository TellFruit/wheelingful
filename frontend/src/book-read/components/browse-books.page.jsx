import { useFetchBooksFilteredQuery } from '../store/apis/read.api';
import { useState } from 'react';
import { BookPaginatedList, SHARED_CONFIG } from '../../shared';
import { READ_CONFIG } from '../configuration/read.config';

export default function BrowseBooks() {
  const [pageNumber, setPageNumber] = useState(1);

  const pageSize = SHARED_CONFIG.pagination.defaultPageSize;

  const { data, error, isFetching, isError } = useFetchBooksFilteredQuery({
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
      redirectByGroup={READ_CONFIG.routes.group}
      serverPaginated={data}
      pageNumber={pageNumber}
      onPageChange={handlePageChange}
    />
  );
}
