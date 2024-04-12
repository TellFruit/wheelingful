import {
  Paper,
  Rating,
  TextField,
  Stack,
  Button,
  Typography,
  Container,
  Alert,
} from '@mui/material';
import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Markdown from 'react-markdown';

const initialFormState = {
  score: 0,
  title: '',
  text: '',
};

export default function ReviewMutationComponent({
  onSubmit,
  mutationTitle,
  error,
  review,
  isLoading,
  isSuccess,
  isError,
}) {
  const [formState, setFormState] = useState(initialFormState);
  const [showMarkdown, setShowMarkdown] = useState(false);
  const { bookId } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      navigate(`/read/books/${bookId}`);
    }
  }, [isSuccess, navigate]);

  if (review) {
    const { score, title, text } = review;
    useEffect(() => {
      setFormState({
        score,
        title,
        text,
      });
    }, [score, title, text]);
  }

  const handleRatingChange = (event, value) => {
    setFormState({
      ...formState,
      score: value,
    });
  };

  const handleTitleChange = (event) => {
    setFormState({
      ...formState,
      title: event.target.value,
    });
  };

  const handleTextChange = (event) => {
    setFormState({
      ...formState,
      text: event.target.value,
    });
  };

  const handleSubmit = () => {
    onSubmit({ ...formState, bookId });
  };

  const handleShowMarkdown = () => {
    setShowMarkdown(!showMarkdown);
  };

  return (
    <Container sx={{ width: '55%' }}>
      <Paper
        sx={{
          paddingLeft: 4,
          paddingRight: 4,
          paddingTop: 2,
          paddingBottom: 3,
        }}
      >
        <Stack direction={'column'} spacing={3} alignItems={'center'}>
          <Typography component="legend" sx={{ fontWeight: 'bold' }}>
            Score
          </Typography>
          <Rating
            name={'score'}
            size="large"
            value={formState.score}
            onChange={handleRatingChange}
          />
          <TextField
            required
            name={'title'}
            id="outlined-basic"
            label="Title"
            variant="outlined"
            size="large"
            onChange={handleTitleChange}
            value={formState.title}
            fullWidth
          />
          <Button
            variant="outlined"
            size="small"
            onClick={handleShowMarkdown}
            sx={{ width: '100%' }}
          >
            Switch text view
          </Button>
          {showMarkdown ? (
            <Paper
              elevation={0}
              sx={{
                border: '1px grey solid',
                paddingLeft: 2,
                paddingRight: 2,
                width: '100%',
              }}
            >
              <Typography variant="body2" component="div">
                {formState.text === '' ? (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    *No text provided*
                  </Markdown>
                ) : (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    {formState.text}
                  </Markdown>
                )}
              </Typography>
            </Paper>
          ) : (
            <TextField
              required
              id="outlined-multiline-static"
              name={'text'}
              label="Summary"
              multiline
              rows={5}
              size="large"
              onChange={handleTextChange}
              value={formState.text}
              fullWidth
            />
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
          {isError ? (
            <Alert severity="error" sx={{ marginTop: 2 }}>
              {error}
            </Alert>
          ) : null}
        </Stack>
      </Paper>
    </Container>
  );
}
