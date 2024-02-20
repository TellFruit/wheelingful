import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from '../components/layout';
import AuthRouter from '../../auth/router/routes';
import PublishRouter from '../../book-publish/router/routes';
import ReadRouter from '../../book-read/router/routes';
import { AUTH_CONFIG } from '../../auth/';
import { PUBLISH_CONFIG } from '../../book-publish/';
import { READ_CONFIG } from '../../book-read';

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" />
          <Route
            path={`/${AUTH_CONFIG.routes.group}/*`}
            element={<AuthRouter />}
          />
          <Route
            path={`/${PUBLISH_CONFIG.routes.group}/*`}
            element={<PublishRouter />}
          />
          <Route
            path={`/${READ_CONFIG.routes.group}/*`}
            element={<ReadRouter />}
          />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
