import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQueryWithReauth } from '../../../auth';

export const publishBookApi = createApi({
  reducerPath: 'publishBookApi',
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
      fetchBooksById: builder.query({
        providesTags: ['Book'],
        query: (id) => {
          return {
            url: `/books/${id}`,
            method: 'GET',
          };
        },
      }),
      createBook: builder.mutation({
        invalidatesTags: ['Books'],
        query: (newBook) => {
          return {
            url: '/books',
            body: {
              title: newBook.title,
              description: newBook.description,
              category: newBook.category,
              status: newBook.status,
              coverBase64: newBook.coverBase64,
            },
            method: 'POST',
          };
        },
      }),
      updateBook: builder.mutation({
        invalidatesTags: ['Books', 'Book'],
        query: (updatedBook) => {
          return {
            url: `/books/${updatedBook.id}`,
            body: {
              title: updatedBook.title,
              description: updatedBook.description,
              category: updatedBook.category,
              status: updatedBook.status,
              coverBase64: updatedBook.coverBase64,
            },
            method: 'PUT',
          };
        },
      }),
      deleteBook: builder.mutation({
        invalidatesTags: ['Books'],
        query: (id) => {
          return {
            url: `/books/${id}`,
            method: 'DELETE',
          };
        },
      }),
    };
  },
});

export const {
  useFetchBooksByCurrentUserQuery,
  useFetchBooksByIdQuery,
  useCreateBookMutation,
  useUpdateBookMutation,
  useDeleteBookMutation,
} = publishBookApi;
