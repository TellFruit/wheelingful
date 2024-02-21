/* eslint-disable no-unused-vars */
import { Paper, Box, Stack, Typography, Alert, Button } from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import { useParams } from 'react-router';
import { renderValidationErrorsObject } from '../../shared';
import { useFetchBooksByIdQuery } from '../store/apis/read.api';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import NotificationAddIcon from '@mui/icons-material/NotificationAdd';
import FavoriteIcon from '@mui/icons-material/Favorite';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';

export default function ViewBook() {
  const { bookId } = useParams();

  const {
    data: book,
    error: fetchingError,
    isFetching,
    isError,
  } = useFetchBooksByIdQuery(bookId);

  if (isFetching) {
    return (
      <Typography variant="h5" component="div" sx={{ marginTop: 2 }}>
        Pending...
      </Typography>
    );
  } else if (isError) {
    let error = renderValidationErrorsObject(fetchingError.data.errors);

    return (
      <Stack sx={{ marginTop: 2 }}>
        <Alert severity="error">{error}</Alert>
      </Stack>
    );
  }

  return (
    <Box sx={{ flexGrow: 1 }}>
      <Grid container spacing={2}>
        <Grid lg={3}>
          <Paper elevation={6}>
            <Box
              sx={{ overflow: 'hidden', textAlign: 'center', paddingTop: 1 }}
            >
              <img
                src={book.coverUrl}
                alt="[Book cover image is displayed here]"
                style={{ width: '90%', height: 'auto' }}
              ></img>
            </Box>
          </Paper>
        </Grid>
        <Grid lg={5}>
          <Stack height={'100%'} spacing={2}>
            <Paper elevation={6} sx={{ padding: 2 }}>
              <Typography variant="h4">{book.title}</Typography>
            </Paper>
            <Paper elevation={6} sx={{ flexGrow: 1, padding: 2 }}>
              <Stack height={'100%'}>
                <Stack direction={'row'} spacing={1}>
                  <Typography variant="body2" color="textSecondary">
                    {book.category}
                  </Typography>
                  <Typography variant="body2" color="textSecondary">
                    {book.status}
                  </Typography>
                </Stack>
                <Box marginTop={1}>
                  <Typography variant="body2" color="textPrimary">
                    {book.description}
                  </Typography>
                </Box>
              </Stack>
            </Paper>
          </Stack>
        </Grid>
        <Grid lg={4}>
          <Paper elevation={6} sx={{ padding: 2 }}>
            <Stack spacing={2}>
              <Button
                variant="outlined"
                color="primary"
                size="large"
                startIcon={<VisibilityOffIcon />}
                fullWidth
              >
                Mark as not interested
              </Button>
              <Stack direction={'row'} spacing={2}>
                <Button
                  variant="outlined"
                  color="primary"
                  startIcon={<NotificationAddIcon />}
                  fullWidth
                >
                  Follow
                </Button>
                <Button
                  variant="outlined"
                  color="primary"
                  startIcon={<FavoriteIcon />}
                  fullWidth
                >
                  Favorite
                </Button>
                <Button
                  variant="outlined"
                  color="primary"
                  startIcon={<AutoStoriesIcon />}
                  fullWidth
                >
                  Later
                </Button>
              </Stack>
              <Button
                variant="contained"
                color="primary"
                size="large"
                startIcon={<AutoStoriesIcon />}
                endIcon={<AutoStoriesIcon />}
                fullWidth
              >
                Read now
              </Button>
            </Stack>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
