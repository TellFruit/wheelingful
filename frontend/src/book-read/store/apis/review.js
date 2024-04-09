import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithReauth } from '../../../auth';

// eslint-disable-next-line no-unused-vars
// import { setReviewed, setNotReviewed } from '../slices/reviewSlice'

// async function onPublishReviewQueryStarted(arg, { dispatch, queryFulfilled }) {
//   try {
//     const { data } = await queryFulfilled;
//     if (data) {
//       dispatch(setReviewed());
//     }
//     dispatch(setReviewed());
//   } catch (error) {
//     // TODO: Remove throwing error in production
//     console.error(error);
//   }
// }

export const publishReviewApi = createApi({
  reducerPath: "publishReviewApi",
  baseQuery: baseQueryWithReauth,
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
            method: "POST",
          };
        },
        // onQueryStarted: onPublishReviewQueryStarted,
      }),
      getReviewByBook: builder.query({
        providesTags: ['Review'],
        query: (bookId) => {
          return {
            url: `/users/current/books/${bookId}/reviews`,
            method: "GET"
          }
        }
      }),
      deleteReviewByBook: builder.mutation({
        invalidatesTags: ['Review'],
        query: (bookId) => {
          return {
            url: `/users/current/books/${bookId}/reviews`,
            method: "DELETE",
          };
        },
      })
    };
  },
});

export const { usePublishReviewMutation, useGetReviewByBookQuery, useDeleteReviewByBookMutation } = publishReviewApi;
