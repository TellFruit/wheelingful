export const SHARED_CONFIG = {
  api: {
    // url: '#{SERVER_API_URL}#',
    url: 'https://wheelingfulapi.azurewebsites.net/',
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
