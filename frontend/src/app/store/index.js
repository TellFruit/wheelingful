import { configureStore, combineReducers } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from 'redux-persist';
import storage from 'redux-persist/lib/storage'
import { authApi, authReducer } from '../../auth';
import { publishApi } from '../../book-publish/store/apis/publush-api';

const rootReducer = combineReducers({
  authSlice: authReducer,
  [authApi.reducerPath]: authApi.reducer,
  [publishApi.reducerPath]: publishApi.reducer
});

const persistedReducer = persistReducer({
  key: 'root',
  version: 1,
  storage,
  blacklist: [authApi.reducerPath, publishApi.reducerPath]
}, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    })
    .concat(authApi.middleware)
    .concat(publishApi.middleware);
  },
});

export const persistor = persistStore(store);

setupListeners(store.dispatch);
