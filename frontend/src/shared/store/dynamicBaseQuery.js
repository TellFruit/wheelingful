import { baseQuery } from './baseQuery';
import { baseQueryWithReauth, selectIsSignedIn } from '../../auth';

export const dynamicBaseQuery = async (args, api, extraOptions) => {
  const isSignedIn = selectIsSignedIn(api.getState());

  if (isSignedIn) {
    return baseQueryWithReauth(args, api, extraOptions);
  }

  return baseQuery(args, api, extraOptions);
};
