import { createSlice, createSelector } from '@reduxjs/toolkit';
import { AUTH_CONFIG } from '../../configuration/auth.config';

const authSlice = createSlice({
  name: 'authSlice',
  initialState: {
    accessToken: '',
    refreshToken: '',
    tokenType: '',
    expiresInMilliseconds: 0,
    lastSignIn: 0,
    isSignedIn: false,
  },
  reducers: {
    setTokens(state, action) {
      state.accessToken = action.payload.accessToken;
      state.refreshToken = action.payload.refreshToken;
      state.tokenType = action.payload.tokenType;
      state.expiresInMilliseconds = action.payload.expiresIn * 1000;
      state.lastSignIn = new Date().getTime();
      state.isSignedIn = true;
    },
    signOut(state) {
      state.accessToken = '';
      state.refreshToken = '';
      state.tokenType = '';
      state.expiresInMilliseconds = 0;
      state.lastSignIn = 0;
      state.isSignedIn = false;
      window.location.href = `/${AUTH_CONFIG.routes.group}/${AUTH_CONFIG.routes.login}`;
    },
  },
});

export const { setTokens, signOut } = authSlice.actions;
export const authReducer = authSlice.reducer;

const selectAuthSlice = (state) => state.authSlice;

export const selectIsExpired = createSelector(
  [selectAuthSlice],
  (authSlice) => {
    const { lastSignIn, expiresInMilliseconds } = authSlice;
    const currentTime = new Date().getTime();
    const elapsedTimeSinceSignIn = currentTime - lastSignIn;

    return elapsedTimeSinceSignIn >= expiresInMilliseconds;
  }
);

export const selectIsSignedIn = (state) => state.authSlice.isSignedIn;
