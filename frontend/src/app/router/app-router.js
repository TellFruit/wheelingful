import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from '../components/layout';
import AuthRouter from '../../auth/router/routes';
import ProtectedWrapper from '../../auth/components/protected.wrapper';

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<div>Home placeholder</div>} />
          <Route
            path="/protected"
            element={
              <ProtectedWrapper>
                <div>Protected placeholder</div>
              </ProtectedWrapper>
            }
          />
          <Route path="/auth/*" element={<AuthRouter />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
