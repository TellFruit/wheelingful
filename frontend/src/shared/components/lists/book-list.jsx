import {
  Box,
  Grid,
  Card,
  CardActionArea,
  CardMedia,
  CardContent,
  Stack,
  Typography,
  Alert,
  Rating,
} from '@mui/material';
import { Link } from 'react-router-dom';
import Markdown from 'react-markdown';

export function BookList({
  isFetching,
  isError,
  error,
  redirectByGroup,
  data,
}) {
  if (isFetching) {
    return (
      <Typography variant="h5" component="div">
        Pending...
      </Typography>
    );
  } else if (isError) {
    return (
      <Stack>
        <Alert severity="error">{error}</Alert>
      </Stack>
    );
  }

  return (
    <Box sx={{ width: '100%' }}>
      <Grid container spacing={3}>
        {data.map((book) => (
          <Grid item key={book.id} xs={12} sm={12} md={6} lg={6}>
            <Card>
              <CardActionArea
                component={Link}
                to={`/${redirectByGroup}/${book.id}`}
              >
                <Stack direction={'row'}>
                  <CardMedia
                    component="img"
                    image={book.coverUrl}
                    alt={book.title}
                    sx={{ width: 200 }}
                  />

                  <CardContent sx={{ width: '100%' }}>
                    <Stack
                      direction={'column'}
                      spacing={1}
                      sx={{ width: '100%' }}
                    >
                      <Typography variant="h6">{book.title}</Typography>
                      <Stack direction={'row'} spacing={1}>
                        <Typography variant="body2" color="textSecondary">
                          {book.category}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                          {book.status}
                        </Typography>
                      </Stack>
                      <Box
                        marginTop={1}
                        sx={{ height: 200, overflow: 'hidden' }}
                      >
                        <Typography variant="body2" component="div">
                          <Markdown components={{ h1: 'h3', h2: 'h3' }}>
                            {book.description}
                          </Markdown>
                        </Typography>
                      </Box>
                      <Rating
                        value={book.averageScore}
                        precision={0.5}
                        size={'large'}
                        readOnly
                        sx={{ alignSelf: 'end' }}
                      />
                    </Stack>
                  </CardContent>
                </Stack>
              </CardActionArea>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
