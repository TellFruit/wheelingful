import AppWrapper from './app-wrapper';
import { ThemeProvider } from '@mui/material';
import { theme } from '../material-ui/theme';
import { Provider } from 'react-redux';
import { persistor, store } from '../store';
import { RouterProvider } from 'react-router-dom';
import { router } from '../router/browser-router';
import { PersistGate } from 'redux-persist/integration/react';

export default function App() {
  return (
    <AppWrapper>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <PersistGate loading={null} persistor={persistor}>
            <RouterProvider
              router={router}
              fallbackElement={<p>Initial Load...</p>}
            />
          </PersistGate>
        </Provider>
      </ThemeProvider>
    </AppWrapper>
  );
}
