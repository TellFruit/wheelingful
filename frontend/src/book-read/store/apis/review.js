import { createApi } from '@reduxjs/toolkit/query/react';
import { dynamicBaseQuery } from '../../../shared';

export const publishReviewApi = createApi({
  reducerPath: 'publishReviewApi',
  baseQuery: dynamicBaseQuery,
  endpoints(builder) {
    return {
      publishReview: builder.mutation({
        invalidatesTags: ['Review'],
        query: (review) => {
          return {
            url: `/users/current/books/${review.bookId}/reviews`,
            body: {
              score: review.score,
              title: review.title,
              text: review.text,
            },
            method: 'POST',
          };
        },
      }),
      updateReview: builder.mutation({
        invalidatesTags: ['Review'],
        query: (review) => {
          return {
            url: `/users/current/books/${review.bookId}/reviews`,
            body: {
              score: review.score,
              title: review.title,
              text: review.text,
            },
            method: 'PUT',
          };
        },
      }),
      fetchReviewByBook: builder.query({
        providesTags: ['Review'],
        query: (bookId) => {
          return {
            url: `/users/current/books/${bookId}/reviews`,
            method: 'GET',
          };
        },
      }),
      deleteReviewByBook: builder.mutation({
        invalidatesTags: ['Review'],
        query: (bookId) => {
          return {
            url: `/users/current/books/${bookId}/reviews`,
            method: 'DELETE',
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
      }),
      fetchReviewsByUser: builder.query({
        providesTags: ['Review'],
        query: (pagination) => {
          return {
            url: `/users/current/reviews`,
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
            },
            method: 'GET',
          };
        },
      }),
    };
  },
});

export const {
  usePublishReviewMutation,
  useUpdateReviewMutation,
  useFetchReviewByBookQuery,
  useDeleteReviewByBookMutation,
  useFetchReviewsByBookQuery,
  useFetchReviewsByUserQuery,
} = publishReviewApi;
