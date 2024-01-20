import { fetchBaseQuery } from '@reduxjs/toolkit/query';
import { SHARED_CONFIG } from '../../configuration/shared-config';

export const baseQuery = fetchBaseQuery({
  baseUrl: SHARED_CONFIG.serverApiUrl,
});
