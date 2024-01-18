import { createSlice } from '@reduxjs/toolkit';
import { AUTH_CONFIG } from '../../configuration/auth-config';

const authSlice = createSlice({
  name: 'authSlice',
  initialState: {
    accessToken: '',
    refreshToken: '',
    tokenType: '',
    isSignedIn: false,
  },
  reducers: {
    setTokens(state, action) {
      state.accessToken = action.payload.accessToken;
      state.refreshToken = action.payload.refreshToken;
      state.tokenType = action.payload.tokenType;
      state.isSignedIn = true;
    },
    signOut(state) {
      state.accessToken = '';
      state.refreshToken = '';
      state.tokenType = '';
      state.isSignedIn = false;
      window.location.href = AUTH_CONFIG.routes.login;
    },
  },
});

export const { setTokens, signOut } = authSlice.actions;
export const authReducer = authSlice.reducer;
