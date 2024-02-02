import AppWrapper from './app-wrapper';
import { ThemeProvider } from '@mui/material';
import { theme } from '../material-ui/theme';
import { Provider } from 'react-redux';
import { persistor, store } from '../store';
import { PersistGate } from 'redux-persist/integration/react';
import AppRouter from '../router/app-router';

export default function App() {
  return (
    <AppWrapper>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <PersistGate loading={null} persistor={persistor}>
            <AppRouter />
          </PersistGate>
        </Provider>
      </ThemeProvider>
    </AppWrapper>
  );
}
