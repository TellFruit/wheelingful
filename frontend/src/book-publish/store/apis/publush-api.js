import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQueryWithReauth } from '../../../auth';
import { PUBLISH_CONFIG } from '../../configuration/publish-config';

export const publishApi = createApi({
  reducerPath: 'publishApi',
  baseQuery: baseQueryWithReauth,
  endpoints(builder) {
    return {
      fetchBooksByCurrentUser: builder.query({
        providesTags: ['Books'],
        query: (pagination) => {
          return {
            url: PUBLISH_CONFIG.routes.api.fetchBooks,
            params: {
              pageNumber: pagination.pageNumber,
              pageSize: pagination.pageSize,
              doFetchByCurrentUser: true,
            },
            method: 'GET',
          };
        },
      }),
      countBooksPaginationPages: builder.query({
        query: (pagination) => {
          return {
            url: PUBLISH_CONFIG.routes.api.countPaginationPages,
            params: {
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

export const {
  useFetchBooksByCurrentUserQuery,
  useCountBooksPaginationPagesQuery,
} = publishApi;
