import { createApi } from '@reduxjs/toolkit/query/react';
import { dynamicBaseQuery } from '../../../shared';

export const recommendationApi = createApi({
  reducerPath: 'recommendationApi',
  baseQuery: dynamicBaseQuery,
  endpoints(builder) {
    return {
      fetchRecommendationsByBook: builder.query({
        query: (bookId) => {
          return {
            url: `/books/${bookId}/recommendations`,
            method: 'GET',
          };
        },
      }),
      fetchRecommendationsByUser: builder.query({
        query: () => {
          return {
            url: `/users/current/recommendations`,
            method: 'GET',
          };
        },
      }),
    };
  },
});

export const {
  useFetchRecommendationsByBookQuery,
  useFetchRecommendationsByUserQuery,
} = recommendationApi;
