import { useState } from "react";
import { useParams } from "react-router-dom";
import { useFetchReviewsByBookQuery } from "../store/apis/review";
import { SHARED_CONFIG } from "../../shared";
import { ReviewPaginatedList } from "../../shared/components/lists/reviews-paginated-list.jsx";

export default function Reviews() {
  const [currentPage, setCurrentPage] = useState(1);
  const { bookId } = useParams();
  // eslint-disable-next-line no-unused-vars
  const { data, error, isFetching, isError } = useFetchReviewsByBookQuery({
    bookId,
    pageNumber: currentPage,
    pageSize: SHARED_CONFIG.pagination.defaultPageSize,
  });

  function handlePageChange(event, value) {
    setCurrentPage(value);
  }

  return (
    <ReviewPaginatedList
      isFetching={isFetching}
      serverPaginated={data}
      pageNumber={currentPage}
      onPageChange={handlePageChange}
    />
  );
}
