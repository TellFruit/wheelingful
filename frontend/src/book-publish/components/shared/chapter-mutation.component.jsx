import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { PUBLISH_CONFIG } from '../../configuration/publish.config';
import {
  Box,
  Container,
  Stack,
  Typography,
  Button,
  TextField,
  Alert,
  Divider,
  Paper,
} from '@mui/material';
import Markdown from 'react-markdown';

export default function ChapterMutationComponent({
  onSubmit,
  onDelete,
  headerTitle,
  mutationTitle,
  error,
  chapter,
  bookId,
  isLoading,
  isSuccess,
  isError,
}) {
  const [title, setTitle] = useState('');
  const [text, setText] = useState('');
  const [showMarkdown, setShowMarkdown] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (chapter) {
      setTitle(chapter.title);
      setText(chapter.text);
    }
  }, [chapter]);

  useEffect(() => {
    if (isSuccess) {
      navigate(
        `/${PUBLISH_CONFIG.routes.group}/${bookId}`
      );
    }
  }, [isSuccess, navigate]);

  const handleShowMarkdown = () => {
    setShowMarkdown(!showMarkdown);
  };

  const handleSubmit = () => {
    onSubmit({
      chapterId: onDelete ? chapter.id : null,
      bookId: bookId,
      title,
      text,
    });
  };

  const handleDelete = () => {
    onDelete(bookId, chapter.id);
  };

  return (
    <Container maxWidth="md">
      <Box>
        <Typography variant="h3" sx={{ textAlign: 'left', marginBottom: 2 }}>
          {headerTitle}
        </Typography>
        <Stack spacing={2} marginTop={2}>
          <TextField
            label="Title"
            variant="outlined"
            fullWidth
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <Button variant="outlined" size="small" onClick={handleShowMarkdown}>
            {' '}
            Switch text view
          </Button>
          {showMarkdown ? (
            <Paper
              elevation={0}
              sx={{ border: '1px grey solid', paddingLeft: 2, paddingRight: 2 }}
            >
              <Typography variant="body2" component="div">
                {text === '' ? (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    *No text provided*
                  </Markdown>
                ) : (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    {text}
                  </Markdown>
                )}
              </Typography>
            </Paper>
          ) : (
            <TextField
              label="Text"
              variant="outlined"
              type="password"
              fullWidth
              multiline
              minRows={2}
              value={text}
              onChange={(e) => setText(e.target.value)}
            />
          )}
        </Stack>
        {isError && (
          <Alert severity="error" sx={{ marginTop: 2 }}>
            {error}
          </Alert>
        )}
        <Box marginTop={2}>
          <Divider variant="middle" />
        </Box>
        <Stack
          direction={'row'}
          justifyContent={onDelete ? 'space-between' : 'center'}
          marginTop={2}
        >
          {onDelete && (
            <Button
              variant="outlined"
              color="error"
              size="large"
              onClick={handleDelete}
              disabled={isLoading}
            >
              Delete
            </Button>
          )}
          <Button
            variant="contained"
            color="primary"
            size="large"
            onClick={handleSubmit}
            disabled={isLoading}
          >
            {mutationTitle}
          </Button>
        </Stack>
      </Box>
    </Container>
  );
}
