import FormatQuoteIcon from '@mui/icons-material/FormatQuote';
import {
  Box,
  Stack,
  Typography,
  Pagination,
  Divider,
  Avatar,
  Rating,
  CircularProgress,
} from '@mui/material';
import Markdown from 'react-markdown';

function ReviewItem({ title, text, score, userName, createdAt }) {
  return (
    <Box>
      <Stack spacing={2} direction={'row'} alignItems={'center'} sx={{ mb: 2 }}>
        <Stack spacing={2} direction={'column'} alignItems={'center'}>
          <Avatar>{userName[0].toUpperCase()}</Avatar>
          <Typography component="legend">Overall</Typography>
          <Rating value={score} precision={0.5} readOnly />
        </Stack>
        <Stack
          spacing={2}
          direction={'column'}
          alignItems={'left'}
          sx={{ width: '100%' }}
        >
          <Typography
            variant="h6"
            sx={{ textAlign: 'left', fontWeight: 'bold' }}
          >
            {title}
          </Typography>
          <Stack direction={'row'} justifyContent={'space-between'}>
            <Typography variant="subtitle1" sx={{ textAlign: 'left' }}>
              <span style={{ color: '#1E40AF', fontWeight: 'bold' }}>BY:</span>{' '}
              {userName}
            </Typography>
            <Typography
              variant="subtitle1"
              sx={{ textAlign: 'left' }}
              color="primary"
            >
              {createdAt}
            </Typography>
          </Stack>
          <Typography variant="body1" sx={{ textAlign: 'left' }}>
            <Markdown components={{ h1: 'h3', h2: 'h3' }}>{text}</Markdown>
          </Typography>
        </Stack>
      </Stack>
      <Divider sx={{ mb: 2 }} />
    </Box>
  );
}

export function ReviewPaginatedList({
  isFetching,
  serverPaginated,
  pageNumber,
  onPageChange,
}) {
  if (isFetching) {
    return (
      <Box>
        <Stack
          spacing={1}
          direction={'row'}
          alignItems={'center'}
          marginTop={2}
        >
          <FormatQuoteIcon />
          <Typography variant="h5" sx={{ textAlign: 'left' }}>
            Reviews
          </Typography>
        </Stack>
        <Box marginTop={2}>
          <Divider sx={{ mb: 2 }} />
          <CircularProgress />
        </Box>
      </Box>
    );
  }

  return (
    <Box>
      <Stack spacing={1} direction={'row'} alignItems={'center'} marginTop={2}>
        <FormatQuoteIcon />
        <Typography variant="h5" sx={{ textAlign: 'left' }}>
          Reviews
        </Typography>
      </Stack>
      <Box marginTop={2}>
        <Divider sx={{ mb: 2 }} />
        {serverPaginated.items.map((review, idx) => (
          <ReviewItem key={idx} {...review} />
        ))}
      </Box>
      <Box display="flex" justifyContent="center" alignItems="center">
        <Pagination
          count={serverPaginated.pageCount}
          page={pageNumber}
          onChange={onPageChange}
          showFirstButton
          showLastButton
          color="primary"
          size="large"
          sx={{ marginTop: 3 }}
        />
      </Box>
    </Box>
  );
}
