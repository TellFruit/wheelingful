import AuthComponent from './shared/auth.component';
import { useSignUpMutation } from '../store/apis/authApi';
import { AUTH_CONFIG } from '../configuration/auth.config';
import { renderValidationErrorsObject } from '../../shared';

export default function RegisterPage() {
  const [signUp, results] = useSignUpMutation();

  let error;
  if (results.isError) {
    error = renderValidationErrorsObject(results.error.data.errors);
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
