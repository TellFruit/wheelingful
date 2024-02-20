import { Box, Typography } from '@mui/material';

export default function Footer() {
  return (
    <Box
      component="footer"
      sx={{
        padding: 2,
        marginTop: 'auto',
        textAlign: 'center',
      }}
    >
      <Typography variant="body2" color="textSecondary">
        © {new Date().getFullYear()} Wheelingful project. Released under the MIT
        License.
      </Typography>
    </Box>
  );
}
