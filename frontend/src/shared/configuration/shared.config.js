export const SHARED_CONFIG = {
  api: {
    url: '#{SERVER_API_URL}#',
  },
  pagination: {
    defaultPageSize: 8,
  },
  select: {
    book: {
      category: ['Original', 'Fanfiction'],
      status: ['Ongoing', 'Finished', 'Hiatus', 'Suspended'],
    },
  },
};
