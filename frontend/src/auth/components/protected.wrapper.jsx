import { Navigate, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { AUTH_CONFIG } from '../configuration/auth-config';

export default function ProtectedWrapper({ children }) {
  const { isSignedIn } = useSelector((state) => {
    return {
      isSignedIn: state.authSlice.isSignedIn,
    };
  });
  
  const location = useLocation();

  return isSignedIn ? (
    children
  ) : (
    <Navigate
      to={`/${AUTH_CONFIG.routes.router.login}`}
      state={{ from: location }}
      replace
    />
  );
}
