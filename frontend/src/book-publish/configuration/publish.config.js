export const PUBLISH_CONFIG = {
  routes: {
    group: 'publish/books',
    booksByCurrentUser: 'list',
    publishBook: 'new',
    publishChapter: ':bookId/chapters/new',
    updateBook: ':bookId',
    updateChapter: ':bookId/chapters/:chapterId',
  },
};
