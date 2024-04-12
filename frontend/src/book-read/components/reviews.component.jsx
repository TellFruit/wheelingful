import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetchReviewsByBookQuery } from '../store/apis/review';
import { SHARED_CONFIG, ReviewPaginatedList } from '../../shared';

export default function Reviews() {
  const [currentPage, setCurrentPage] = useState(1);
  const { bookId } = useParams();
  const { data, isFetching } = useFetchReviewsByBookQuery({
    bookId,
    pageNumber: currentPage,
    pageSize: SHARED_CONFIG.pagination.defaultPageSize,
  });

  const handlePageChange = (event, value) => {
    setCurrentPage(value);
  };

  return (
    <ReviewPaginatedList
      isFetching={isFetching}
      serverPaginated={data}
      pageNumber={currentPage}
      onPageChange={handlePageChange}
    />
  );
}
