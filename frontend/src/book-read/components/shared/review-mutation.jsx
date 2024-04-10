import {
  //   Box,
  Paper,
  Rating,
  TextField,
  Stack,
  Button,
  Typography,
  Container,
  Alert,
} from "@mui/material";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";

const initialFormState = {
  score: 0,
  title: "",
  text: "",
};

export default function ReviewMutationComponent({
  onSubmit,
  //   onDelete,
  //   headerTitle,
  mutationTitle,
  error,
  review,
  //   isLoading,
  isSuccess,
  isError,
}) {
  const [formState, setFormState] = useState(initialFormState);
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

  function handleRatingChange(event, value) {
    setFormState({
      ...formState,
      score: value,
    });
  }

  function handleTitleChange(event) {
    setFormState({
      ...formState,
      title: event.target.value,
    });
  }

  function handleTextChange(event) {
    setFormState({
      ...formState,
      text: event.target.value,
    });
  }

  function handleSubmit() {
    onSubmit({ ...formState, bookId });
  }

  return (
    <Container sx={{ width: "55%" }}>
      <Paper
        sx={{
          paddingLeft: 4,
          paddingRight: 4,
          paddingTop: 2,
          paddingBottom: 3,
        }}
      >
        <Stack direction={"column"} spacing={3} alignItems={"center"}>
          <Typography component="legend" sx={{ fontWeight: "bold" }}>
            Score
          </Typography>
          <Rating
            name={"score"}
            size="large"
            value={formState.score}
            onChange={handleRatingChange}
          />
          <TextField
            required
            name={"title"}
            id="outlined-basic"
            label="Title"
            variant="outlined"
            size="large"
            onChange={handleTitleChange}
            value={formState.title}
            fullWidth
          />
          <TextField
            required
            id="outlined-multiline-static"
            name={"text"}
            label="Summary"
            multiline
            rows={5}
            size="large"
            onChange={handleTextChange}
            value={formState.text}
            fullWidth
          />
          <Button
            variant="contained"
            color="primary"
            size="large"
            onClick={handleSubmit}
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
