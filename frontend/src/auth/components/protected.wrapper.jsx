import { Navigate, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { AUTH_CONFIG } from '../configuration/auth.config';

export default function ProtectedWrapper({ children }) {
  const { isSignedIn } = useSelector((state) => state.authSlice);

  const location = useLocation();

  return isSignedIn ? (
    children
  ) : (
    <Navigate
      to={`/${AUTH_CONFIG.routes.group}/${AUTH_CONFIG.routes.login}`}
      state={{ from: location }}
      replace
    />
  );
}
