import AuthComponent from './shared/auth.component';
import { useSignInMutation } from '../store/apis/authApi';
import { AUTH_CONFIG } from '../configuration/auth-config';

export default function LoginPage() {
  const [signIn, results] = useSignInMutation();

  let error;
  if (results.isError) {
    error = <div>{results.error.data.detail}</div>
  }

  return (
    <AuthComponent
      onSubmit={({ email, password }) => signIn({ email, password })}
      title="Please, enter your credentials."
      authTitle="Sign In"
      redirectTitle="Sign Up"
      redirectTo={`/${AUTH_CONFIG.routes.router.register}`}
      error={error}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
      isError={results.isError}
    />
  );
}
