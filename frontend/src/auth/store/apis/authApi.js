import { createApi } from '@reduxjs/toolkit/query/react';
import { baseQuery } from '../../../shared';
import { setTokens } from '../slices/authSlice';

const onAuthQueryStarted = async (arg, { dispatch, queryFulfilled }) => {
  try {
    const { data } = await queryFulfilled;
    dispatch(setTokens(data));
  } catch (error) {
    // TODO: Remove throwing error in production
    console.error(error);
  }
};

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: baseQuery,
  endpoints(builder) {
    return {
      signUp: builder.mutation({
        query: (credentials) => {
          return {
            url: '/auth/register',
            body: {
              email: credentials.email,
              password: credentials.password,
            },
            method: 'POST',
          };
        },
        onQueryStarted: onAuthQueryStarted,
      }),
      signIn: builder.mutation({
        query: (credentials) => {
          return {
            url: '/auth/login',
            body: {
              email: credentials.email,
              password: credentials.password,
            },
            method: 'POST',
          };
        },
        onQueryStarted: onAuthQueryStarted,
      }),
    };
  },
});

export const { useSignUpMutation, useSignInMutation } = authApi;
