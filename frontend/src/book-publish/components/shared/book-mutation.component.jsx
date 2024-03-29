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
import { BasicSelect, FileUpload, SHARED_CONFIG } from '../../../shared';
import ChaptersByBook from '../chapters-by-book.component';

export default function BookMutationComponent({
  onSubmit,
  onDelete,
  headerTitle,
  mutationTitle,
  error,
  book,
  isLoading,
  isSuccess,
  isError,
}) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [category, setCategory] = useState(
    SHARED_CONFIG.select.book.category[0]
  );
  const [status, setStatus] = useState(SHARED_CONFIG.select.book.status[0]);
  const [coverBase64, setCoverBase64] = useState(null);
  const [coverDataUrl, setCoverDataUrl] = useState(null);
  const [showMarkdown, setShowMarkdown] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (book) {
      setTitle(book.title);
      setDescription(book.description);
      setCategory(book.category);
      setStatus(book.status);
    }
  }, [book]);

  useEffect(() => {
    if (isSuccess) {
      navigate(
        `/${PUBLISH_CONFIG.routes.group}/${PUBLISH_CONFIG.routes.booksByCurrentUser}`
      );
    }
  }, [isSuccess, navigate]);

  const handleShowMarkdown = () => {
    setShowMarkdown(!showMarkdown);
  };

  const handleSubmit = () => {
    onSubmit({
      id: book ? book.id : null,
      title,
      description,
      category,
      status,
      coverBase64,
    });
  };

  const handleDelete = () => {
    onDelete(book.id);
  };

  const handleCoverChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setCoverBase64(reader.result.split(',')[1]);
        setCoverDataUrl(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };

  let coverSource;
  if (coverDataUrl) {
    coverSource = coverDataUrl;
  } else if (book) {
    coverSource = book.coverUrl;
  }

  return (
    <Container maxWidth="sm">
      <Box>
        <Typography variant="h3" sx={{ textAlign: 'left', marginBottom: 2 }}>
          {headerTitle}
        </Typography>
        <Stack direction={'row'} justifyContent={'space-around'}>
          <Box sx={{ maxWidth: 200, overflow: 'hidden' }}>
            <img
              src={coverSource}
              alt="[Book cover image is displayed here]"
              style={{ width: '70%', height: 'auto' }}
            ></img>
          </Box>
          <Stack justifyContent={'center'}>
            <Typography
              variant="h6"
              sx={{ textAlign: 'center', marginBottom: 2 }}
            >
              Select book cover
            </Typography>
            <FileUpload onChange={handleCoverChange} />
          </Stack>
        </Stack>
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
            Switch description view
          </Button>
          {showMarkdown ? (
            <Paper
              elevation={0}
              sx={{ border: '1px grey solid', paddingLeft: 2, paddingRight: 2 }}
            >
              <Typography variant="body2" component="div">
                {description === '' ? (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    *No description provided*
                  </Markdown>
                ) : (
                  <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                    {description}
                  </Markdown>
                )}
              </Typography>
            </Paper>
          ) : (
            <TextField
              label="Description"
              variant="outlined"
              type="password"
              fullWidth
              multiline
              minRows={2}
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          )}
        </Stack>
        <Stack
          direction={'row'}
          spacing={2}
          justifyContent={'space-around'}
          marginTop={2}
        >
          <Box minWidth={120}>
            <BasicSelect
              label="Category"
              items={SHARED_CONFIG.select.book.category}
              value={category}
              onChange={(e) => {
                setCategory(e.target.value);
              }}
            />
          </Box>
          <Box minWidth={120}>
            <BasicSelect
              label="Status"
              items={SHARED_CONFIG.select.book.status}
              value={status}
              onChange={(e) => {
                setStatus(e.target.value);
              }}
            />
          </Box>
        </Stack>
        {isError && (
          <Alert severity="error" sx={{ marginTop: 2 }}>
            {error}
          </Alert>
        )}
        {book && (
          <Box marginTop={2}>
            <Divider />
            <ChaptersByBook />
          </Box>
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
