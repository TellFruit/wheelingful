import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQueryWithReauth } from '../../../auth';

export const publishApi = createApi({
  reducerPath: 'publishApi',
  baseQuery: baseQueryWithReauth,
  endpoints(builder) {
    return {
      fetchBooksByCurrentUser: builder.query({
        providesTags: ['Books'],
        query: (pagination) => {
          return {
            url: '/books',
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
              doFetchByCurrentUser: true,
            },
            method: 'GET',
          };
        },
      }),
    };
  },
});

export const { useFetchBooksByCurrentUserQuery } = publishApi;
