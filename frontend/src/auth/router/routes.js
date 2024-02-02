import { Routes, Route } from 'react-router-dom';
import { AUTH_CONFIG } from '../configuration/auth-config';
import LoginPage from '../components/login.page';
import RegisterPage from '../components/register.page';

export default function AuthRouter() {
  return (
    <Routes>
      <Route path={AUTH_CONFIG.routes.router.login} element={<LoginPage />} />
      <Route path={AUTH_CONFIG.routes.router.register} element={<RegisterPage/>} />
    </Routes>
  );
}
