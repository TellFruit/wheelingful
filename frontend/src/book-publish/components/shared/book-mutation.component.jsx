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
} from '@mui/material';
import { BasicSelect, FileUpload, SHARED_CONFIG } from '../../../shared';

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
  const [category, setCategory] = useState(SHARED_CONFIG.select.book.category[0]);
  const [status, setStatus] = useState(SHARED_CONFIG.select.book.status[0]);
  const [coverBase64, setCoverBase64] = useState(null);
  const [coverDataUrl, setCoverDataUrl] = useState(null);
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
    console.log(isSuccess + " In effect")
    if (isSuccess) {
      console.log(isSuccess + " In effect if")
      navigate(
        `/${PUBLISH_CONFIG.routes.group}/${PUBLISH_CONFIG.routes.booksByCurrentUser}`
      );
    }
  }, [isSuccess, navigate]);

  const handleSubmit = () => {
    onSubmit({
      id: onDelete ? book.id : null,
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
      <Box sx={{ marginTop: 10 }}>
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
                console.log(e.target.value);
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
                console.log(e.target.value);
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
