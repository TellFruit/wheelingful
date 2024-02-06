export const PUBLISH_CONFIG = {
  pagination: {
    pageSizeDefault: 8
  },
  routes: {
    router: {
      group: 'publish',
      booksByCurrentUser: 'list',
      publishBook: 'new',
      updateBook: 'update/:bookId',
    },
    api: {
      fetchBooks: '/book-reader',
      countPaginationPages: '/book-reader/pages',
    },
  },
};
