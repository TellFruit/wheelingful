import {
  Paper,
  Box,
  Stack,
  Typography,
  Alert,
  Button,
  Rating,
} from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import { useParams, useNavigate } from 'react-router';
import Markdown from 'react-markdown';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import NotificationAddIcon from '@mui/icons-material/NotificationAdd';
import FavoriteIcon from '@mui/icons-material/Favorite';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import ChaptersByBook from './chapters-by-book.component';
import { SHARED_CONFIG, renderValidationErrorsObject } from '../../shared';
import {
  useFetchBooksByIdQuery,
  useFetchChaptersByBookQuery,
} from '../store/apis/read';
import Reviews from './reviews.component';
import { READ_CONFIG } from '../configuration/read.config';

export default function ViewBook() {
  const { bookId } = useParams();

  const navigate = useNavigate();

  const {
    data: book,
    error,
    isFetching: bookFetching,
    isError: bookError,
  } = useFetchBooksByIdQuery(bookId);

  const pageNumber = 1;
  const pageSize = SHARED_CONFIG.pagination.defaultPageSize;
  const {
    data: firstChapters,
    isFetching: chapterFetching,
    isError: chapterError,
  } = useFetchChaptersByBookQuery({
    bookId,
    pageNumber,
    pageSize,
  });

  const handleReadNow = () => {
    navigate(
      `/${READ_CONFIG.routes.group}/${bookId}/chapters/${firstChapters.items[0].id}`
    );
  };

  if (bookFetching) {
    return (
      <Typography variant="h5" component="div" sx={{ marginTop: 2 }}>
        Pending...
      </Typography>
    );
  } else if (bookError) {
    let errorMessage = renderValidationErrorsObject(error.data.errors);

    return (
      <Stack sx={{ marginTop: 2 }}>
        <Alert severity="error">{errorMessage}</Alert>
      </Stack>
    );
  }

  return (
    <Box sx={{ flexGrow: 1 }}>
      <Grid container spacing={2}>
        <Grid lg={3}>
          <Paper elevation={6} sx={{ padding: 1 }}>
            <Box
              sx={{ overflow: 'hidden', textAlign: 'center', paddingTop: 1 }}
            >
              <img
                src={book.coverUrl}
                alt="[Book cover image is displayed here]"
                style={{ width: '90%', height: 'auto' }}
              ></img>
              <Rating
                value={book.averageScore}
                precision={0.5}
                size={'large'}
                readOnly
              />
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
                <Box>
                  <Typography variant="body2" component="div">
                    <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                      {book.description}
                    </Markdown>
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
                disabled={chapterFetching && chapterError}
                fullWidth
                onClick={handleReadNow}
              >
                Read now
              </Button>
            </Stack>
          </Paper>
        </Grid>
        <Grid lg={8}>
          <Paper
            elevation={6}
            sx={{
              paddingLeft: 3,
              paddingRight: 3,
              paddingTop: 1,
              paddingBottom: 2,
            }}
          >
            <ChaptersByBook />
          </Paper>
        </Grid>
        <Grid lg={8}>
          <Paper
            elevation={6}
            sx={{
              paddingLeft: 3,
              paddingRight: 3,
              paddingTop: 1,
              paddingBottom: 2,
            }}
          >
            <Reviews />
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
