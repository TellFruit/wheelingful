import LoginPage from '../components/login.page'
import RegisterPage from '../components/register.page';
import { AUTH_CONFIG } from '../configuration/auth-config';

export const authRootRoutes = [
  {
    path: AUTH_CONFIG.routes.router.register,
    Component: RegisterPage,
  },
  {
    path: AUTH_CONFIG.routes.router.login,
    Component: LoginPage
  }
];
