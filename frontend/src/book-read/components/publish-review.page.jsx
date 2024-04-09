import {
  Box,
  Paper,
  Rating,
  TextField,
  Stack,
  Button,
  Typography,
} from "@mui/material";
import { useState } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import { usePublishReviewMutation } from '../store/apis/review';

const initialFormState = {
  score: 0,
  title: "",
  text: ""
};

export default function PublishReview() {
  const navigate = useNavigate();
  const [formState, setFormState] = useState(initialFormState);
  // eslint-disable-next-line no-unused-vars
  const [publishReview, results] = usePublishReviewMutation();
  const { bookId } = useParams();

  function handleRatingChange(event, value) {
    setFormState({
      ...formState,
      score: value
    })
  }

  function handleTitleChange(event) {
    setFormState({
      ...formState,
      title: event.target.value
    })
  }

  function handleTextChange(event) {
    setFormState({
      ...formState,
      text: event.target.value
    })
  }

  function handleSubmit() {
    publishReview({...formState, bookId});
    navigate(`/read/books/${bookId}`);
  }

  return (
    <Box>
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
            <Rating name={"score"} size="large" value={formState.score} onChange={handleRatingChange}/>
            <TextField
              required
              name={"title"}
              id="outlined-basic"
              label="Title"
              variant="outlined"
              size="large"
              onChange={handleTitleChange}
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
            />
            <Button
              variant="contained"
              color="primary"
              size="large"
              onClick={handleSubmit}
            >
              Submit
            </Button>
          </Stack>
      </Paper>
    </Box>
  );
}
