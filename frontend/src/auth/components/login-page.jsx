import {
  Box,
  Button,
  Container,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { useSignInMutation } from '../store/apis/authApi';

function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [signIn, results] = useSignInMutation();
  const location = useLocation();
  const navigate = useNavigate();

  const params = new URLSearchParams(location.search);
  const from = params.get('from') || '/';
  
  useEffect(() => {
    if (results.isSuccess) {
      navigate(from);
    }
  }, [navigate, results]);

  const handleLogin = () => {
    signIn({
      email,
      password,
    });
  };

  const handleRegisterRedirect = () => {};

  return (
    <Container maxWidth="xs">
      <Box sx={{ marginTop: 10 }}>
        <Typography variant="h3" sx={{ textAlign: 'left', marginBottom: 2 }}>
          Please, enter your credentials.
        </Typography>
        <TextField
          label="Email"
          variant="outlined"
          fullWidth
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          sx={{ marginBottom: 2 }}
        />
        <TextField
          label="Password"
          variant="outlined"
          type="password"
          fullWidth
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          sx={{ marginBottom: 2 }}
        />
        <Stack direction={'row'} spacing={2}>
          <Button
            variant="contained"
            color="primary"
            onClick={handleLogin}
            disabled={results.isLoading}
          >
            Sign in
          </Button>
          <Button
            variant="outlined"
            color="primary"
            onClick={handleRegisterRedirect}
            disabled={results.isLoading}
          >
            Sign up
          </Button>
        </Stack>
      </Box>
    </Container>
  );
}

export default LoginPage;
