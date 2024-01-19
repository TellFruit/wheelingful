import AuthComponent from './shared/auth.component';
import { useSignInMutation } from '../store/apis/authApi';
import { AUTH_CONFIG } from '../configuration/auth-config';

export default function LoginPage() {
  const [signIn, results] = useSignInMutation();

  return (
    <AuthComponent
      onSubmit={({ email, password }) => signIn({ email, password })}
      title="Please, enter your credentials."
      authTitle="Sign In"
      redirectTitle="Sign up"
      redirectTo={`/${AUTH_CONFIG.routes.router.register}`}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
    />
  );
}
