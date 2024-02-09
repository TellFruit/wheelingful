import AuthComponent from './shared/auth.component';
import { useSignUpMutation } from '../store/apis/authApi';
import { AUTH_CONFIG } from '../configuration/auth.config';

export default function RegisterPage() {
  const [signUp, results] = useSignUpMutation();

  let error;
  if (results.isError) {
    error = Object.values(results.error.data.errors).map((error, index) => (
      <div key={index}>{error}</div>
    ));
  }

  return (
    <AuthComponent
      onSubmit={({ email, password }) => signUp({ email, password })}
      title="Please, register your credentials."
      authTitle="Sign Up"
      redirectTitle="Sign In"
      redirectTo={`/${AUTH_CONFIG.routes.group}/${AUTH_CONFIG.routes.login}`}
      error={error}
      isLoading={results.isLoading}
      isSuccess={results.isSuccess}
      isError={results.isError}
    />
  );
}
