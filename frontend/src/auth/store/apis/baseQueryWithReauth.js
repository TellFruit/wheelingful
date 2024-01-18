import { fetchBaseQuery } from '@reduxjs/toolkit/query';
const baseQuery = fetchBaseQuery({ baseUrl: 'SHARED_CONFIG.serverApiUrl' });
import { AUTH_CONFIG } from '../../configuration/auth-config';
import { setTokens, signOut } from '../slices/authSlice';

// TODO: Fix possible multiple calls of refresh backend API endpoint (use mutex)
export const baseQueryWithReauth = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  if (result.error && result.error.status === 401) {
    const refreshResult = await baseQuery(
      AUTH_CONFIG.routes.refresh,
      api,
      extraOptions
    );
    if (refreshResult.data) {
      api.dispatch(setTokens(refreshResult.data));

      result = await baseQuery(args, api, extraOptions);
    } else {
      api.dispatch(signOut());
    }
  }
  return result;
};
