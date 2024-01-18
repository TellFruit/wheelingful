import {
  Box,
  Button,
  Container,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useState } from "react";

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = () => {};

  const handleRegisterRedirect = () => {};

  return (
    <Container maxWidth="xs">
      <Box sx={{ marginTop: 10 }}>
        <Typography variant="h3" sx={{ textAlign: "left", marginBottom: 2 }}>
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
        <Stack direction={"row"} spacing={2}>
          <Button variant="contained" color="primary" onClick={handleLogin}>
            Sign in
          </Button>
          <Button
            variant="outlined"
            color="primary"
            onClick={handleRegisterRedirect}
          >
            Sign up
          </Button>
        </Stack>
      </Box>
    </Container>
  );
}

export default LoginPage;
