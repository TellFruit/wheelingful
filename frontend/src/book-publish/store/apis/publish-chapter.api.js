import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQueryWithReauth } from '../../../auth';

export const publishChapterApi = createApi({
  reducerPath: 'publishChapterApi',
  baseQuery: baseQueryWithReauth,
  endpoints(builder) {
    return {
      fetchChaptersByBook: builder.query({
        providesTags: ['Chapters'],
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
        providesTags: ['Chapter'],
        query: (chapterId) => {
          return {
            url: `/books/chapters/${chapterId}`,
            method: 'GET',
          };
        },
      }),
      createChapter: builder.mutation({
        invalidatesTags: ['Chapters'],
        query: (newChapter) => {
          return {
            url: `/books/${newChapter.bookId}/chapters`,
            body: {
              title: newChapter.title,
              text: newChapter.text,
            },
            method: 'POST',
          };
        },
      }),
      updateChapter: builder.mutation({
        invalidatesTags: ['Chapters', 'Chapter'],
        query: (updatedChapter) => {
          return {
            url: `/books/${updatedChapter.bookId}/chapters/${updatedChapter.chapterId}`,
            body: {
              title: updatedChapter.title,
              text: updatedChapter.text,
            },
            method: 'PUT',
          };
        },
      }),
      deleteChapter: builder.mutation({
        invalidatesTags: ['Chapters'],
        query: (deletedChapter) => {
          return {
            url: `/books/${deletedChapter.bookId}/chapters/${deletedChapter.chapterId}`,
            method: 'DELETE',
          };
        },
      }),
    };
  },
});

export const {
  useFetchChaptersByBookQuery,
  useFetchChapterByIdQuery,
  useCreateChapterMutation,
  useUpdateChapterMutation,
  useDeleteChapterMutation,
} = publishChapterApi;
