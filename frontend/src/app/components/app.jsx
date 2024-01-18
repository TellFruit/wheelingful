import AppWrapper from './app-wrapper';
import { ThemeProvider } from '@mui/material';
import { theme } from '../material-ui/theme';
import { Provider } from 'react-redux';
import { store } from '../store';
import { RouterProvider } from 'react-router-dom';
import { router } from '../router/browser-router';

export default function App() {
  return (
    <AppWrapper>
      <ThemeProvider theme={theme}>
        <Provider store={store}>
          <RouterProvider
            router={router}
            fallbackElement={<p>Initial Load...</p>}
          />
        </Provider>
      </ThemeProvider>
    </AppWrapper>
  );
}
