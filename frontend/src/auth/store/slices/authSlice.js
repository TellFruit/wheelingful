import { createSlice } from '@reduxjs/toolkit';
import { AUTH_CONFIG } from '../../configuration/auth-config';

const authSlice = createSlice({
  name: 'authSlice',
  initialState: {
    accessToken: '',
    refreshToken: '',
    tokenType: '',
    isSignedIn: false,
    isExpired: true,
  },
  reducers: {
    setTokens(state, action) {
      state.accessToken = action.payload.accessToken;
      state.refreshToken = action.payload.refreshToken;
      state.tokenType = action.payload.tokenType;
      state.isSignedIn = true;
      state.isExpired = false;
    },
    signOut(state) {
      state.accessToken = '';
      state.refreshToken = '';
      state.tokenType = '';
      state.isSignedIn = false;
      state.isExpired = true;
      window.location.href = AUTH_CONFIG.routes.api.login;
    },
    switchExpired(state) {
      state.isExpired = !state.isExpired;
    }
  },
});

export const { setTokens, signOut, switchExpired } = authSlice.actions;
export const authReducer = authSlice.reducer;
