import AuthComponent from './shared/auth.component';
import { useSignUpMutation } from '../store/apis/authApi';
import { AUTH_CONFIG } from '../configuration/auth-config';

export default function RegisterPage() {
  const [signUp, results] = useSignUpMutation();

  return (
    <AuthComponent
      onSubmit={({ email, password }) => signUp({ email, password })}
      title="Please, register your credentials."
      authTitle="Sign up"
      redirectTitle="Sign In"
      redirectTo={`/${AUTH_CONFIG.routes.router.login}`}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
    />
  );
}
