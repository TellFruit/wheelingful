import { useParams, useNavigate } from 'react-router-dom';
import {
  Container,
  Stack,
  Typography,
  Paper,
  Alert,
  Button,
  Box,
} from '@mui/material';
import Markdown from 'react-markdown';
import { useFetchChapterByIdQuery } from '../store/apis/read';
import { READ_CONFIG } from '../configuration/read.config';

export default function ViewChapter() {
  const { bookId, chapterId } = useParams();

  const navigate = useNavigate();

  const { data, isFetching, isError } = useFetchChapterByIdQuery(chapterId);

  const handleNextChapter = () => {
    navigate(
      `/${READ_CONFIG.routes.group}/${bookId}/chapters/${data.nextChapterId}`
    );
  };

  const handlePreviousChapter = () => {
    navigate(
      `/${READ_CONFIG.routes.group}/${bookId}/chapters/${data.previousChapterId}`
    );
  };

  const handleBackToBook = () => {
    navigate(`/${READ_CONFIG.routes.group}/${bookId}`);
  };

  if (isFetching) {
    return (
      <Typography variant="h5" component="div">
        Pending...
      </Typography>
    );
  } else if (isError) {
    return (
      <Stack>
        <Alert severity="error">Fetching failed!</Alert>
      </Stack>
    );
  }

  return (
    <Container maxWidth="md">
      <Stack spacing={2}>
        <Paper
          elevation={8}
          sx={{
            paddingLeft: 2,
            paddingRight: 2,
            paddingTop: 1,
            paddingBottom: 1,
          }}
        >
          <Stack
            direction={'row'}
            justifyContent={'space-between'}
            alignItems={'center'}
          >
            <Box>
              <Typography variant="h3" sx={{ textAlign: 'left' }}>
                {data.title}
              </Typography>
            </Box>
            <Button variant="outlined" onClick={handleBackToBook}>
              Back to book
            </Button>
          </Stack>
        </Paper>
        <Paper elevation={8} sx={{ paddingLeft: 2, paddingRight: 2 }}>
          <Typography variant="body2" component="div">
            <Markdown components={{ h1: 'h3', h2: 'h3' }}>{data.text}</Markdown>
          </Typography>
        </Paper>
        <Paper elevation={8} sx={{ padding: 1 }}>
          <Stack spacing={2} direction={'row'}>
            <Button
              variant="outlined"
              size="large"
              fullWidth
              onClick={handlePreviousChapter}
              disabled={data.previousChapterId === null}
            >
              Previous chapter
            </Button>
            <Button
              variant="contained"
              size="large"
              fullWidth
              onClick={handleNextChapter}
              disabled={data.nextChapterId === null}
            >
              Next chapter
            </Button>
          </Stack>
        </Paper>
      </Stack>
    </Container>
  );
}
