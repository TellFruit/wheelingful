export const READ_CONFIG = {
  routes: {
    group: 'read/books',
    browseBooks: 'list',
    readBook: ':bookId',
    readChapter: ':bookId/chapters/:chapterId',
    publishReview: ':bookId/review/new',
    updateReview: ':bookId/review/update',
    personalPick: 'recommendations',
  },
};
