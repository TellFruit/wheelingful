import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQuery } from '../../../shared';

export const readApi = createApi({
  reducerPath: 'readApi',
  baseQuery: baseQuery,
  endpoints(builder) {
    return {
      fetchBooksFiltered: builder.query({
        providesTags: ['Books'],
        query: (pagination) => {
          return {
            url: '/books',
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
            },
            method: 'GET',
          };
        },
      }),
      fetchBooksById: builder.query({
        providesTags: ['Book'],
        query: (id) => {
          return {
            url: `/books/${id}`,
            method: 'GET',
          };
        },
      }),
    };
  },
});

export const {
  useFetchBooksFilteredQuery,
  useFetchBooksByIdQuery,
} = readApi;
