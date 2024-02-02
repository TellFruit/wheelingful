import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { AUTH_CONFIG } from '../../configuration/auth-config';
import { SHARED_CONFIG } from '../../../app';
import { setTokens, signOut, switchExpired } from '../slices/authSlice';

const baseAuthorizedQuery = fetchBaseQuery({
  baseUrl: SHARED_CONFIG.serverApiUrl,
  prepareHeaders: (headers, { getState }) => {
    const { accessToken, refreshToken, tokenType, isExpired } =
      getState().authSlice;

    if (accessToken) {
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
  let result = await baseAuthorizedQuery(args, api, extraOptions);

  if (result.error && result.error.status === 401) {
    api.dispatch(switchExpired());
    const refreshResult = await baseAuthorizedQuery(
      AUTH_CONFIG.routes.api.refresh,
      api,
      extraOptions
    );
    if (refreshResult.error.status === 401) {
      api.dispatch(signOut());
    } else {
      api.dispatch(setTokens(refreshResult.data));

      result = await baseAuthorizedQuery(args, api, extraOptions);
    }
  }

  return result;
};
