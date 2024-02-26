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
import storage from 'redux-persist/lib/storage';
import { authApi, authReducer } from '../../auth';
import { publishBookApi, publishChapterApi } from '../../book-publish';
import { readApi } from '../../book-read';

const rootReducer = combineReducers({
  authSlice: authReducer,
  [authApi.reducerPath]: authApi.reducer,
  [publishBookApi.reducerPath]: publishBookApi.reducer,
  [publishChapterApi.reducerPath]: publishChapterApi.reducer,
  [readApi.reducerPath]: readApi.reducer,
});

const persistedReducer = persistReducer(
  {
    key: 'root',
    version: 1,
    storage,
    blacklist: [
      authApi.reducerPath,
      publishBookApi.reducerPath,
      publishChapterApi.reducerPath,
      readApi.reducerPath,
    ],
  },
  rootReducer
);

export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    })
      .concat(authApi.middleware)
      .concat(publishBookApi.middleware)
      .concat(publishChapterApi.middleware)
      .concat(readApi.middleware);
  },
});

export const persistor = persistStore(store);

setupListeners(store.dispatch);
