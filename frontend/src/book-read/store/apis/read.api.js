import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQuery } from '../../../shared';

export const readApi = createApi({
  reducerPath: 'readApi',
  baseQuery: baseQuery,
  endpoints(builder) {
    return {
      fetchBooksFiltered: builder.query({
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
        query: (id) => {
          return {
            url: `/books/${id}`,
            method: 'GET',
          };
        },
      }),
      fetchChaptersByBook: builder.query({
        query: (pagination) => {
          return {
            url: `/books/${pagination.bookId}/chapters`,
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
            },
            method: 'GET',
          };
        },
      }),
      fetchChapterById: builder.query({
        query: (chapterId) => {
          return {
            url: `/books/chapters/${chapterId}`,
            method: 'GET',
          };
        },
      }),
      fetchReviewsByBook: builder.query({
        providesTags: ['Review'],
        query: (pagination) => {
          return {
            url: `/books/${pagination.bookId}/reviews`,
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
            },
            method: 'GET',
          };
        },
      })
    };
  },
});

export const {
  useFetchBooksFilteredQuery,
  useFetchBooksByIdQuery,
  useFetchChaptersByBookQuery,
  useFetchChapterByIdQuery,
  useFetchReviewsByBookQuery,
} = readApi;
