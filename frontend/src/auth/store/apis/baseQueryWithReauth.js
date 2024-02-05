import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { AUTH_CONFIG } from '../../configuration/auth-config';
import { SHARED_CONFIG } from '../../../app';
import { selectIsExpired, setTokens, signOut } from '../slices/authSlice';

const baseAuthorizedQuery = fetchBaseQuery({
  baseUrl: SHARED_CONFIG.serverApiUrl,
  prepareHeaders: (headers, { getState }) => {
    const state = getState();

    const { accessToken, refreshToken, tokenType } = state.authSlice;

    if (accessToken) {
      const isExpired = selectIsExpired(state);

      if (isExpired) {
        headers.set('Refreshing', refreshToken);
      } else {
        headers.set('Authorization', `${tokenType} ${accessToken}`);
      }
    }

    return headers;
  },
});

// TODO: Fix possible multiple calls of refresh backend API endpoint (use mutex)
export const baseQueryWithReauth = async (args, api, extraOptions) => {
  let result;

  const isExpired = selectIsExpired(api.getState());

  if (isExpired) {
    result = await baseAuthorizedQuery(
      AUTH_CONFIG.routes.api.refresh,
      api,
      extraOptions
    );
    if (result.error?.status === 401) {
      api.dispatch(signOut());
    } else {
      api.dispatch(setTokens(result.data));

      result = await baseAuthorizedQuery(args, api, extraOptions);
    }
  } else {
    result = await baseAuthorizedQuery(args, api, extraOptions);
  }

  return result;
};
