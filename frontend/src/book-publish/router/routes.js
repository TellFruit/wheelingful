import { Routes, Route } from 'react-router';
import { PUBLISH_CONFIG } from '../configuration/publish.config';
import BooksByCurrentUser from '../components/books-by-current-user.page';
import ProtectedWrapper from '../../auth/components/protected.wrapper';
import BookCreatePage from '../components/book-create.page';
import BookUpdatePage from '../components/book-update.page';
import PublishDrawer from '../components/publish-drawer.component';
import ChapterCreatePage from '../components/chapter-create.page';
import ChapterUpdatePage from '../components/chapter-update.page';

export default function PublishRouter() {
  return (
    <Routes>
      <Route element={<PublishDrawer />}>
        <Route
          path={PUBLISH_CONFIG.routes.booksByCurrentUser}
          element={
            <ProtectedWrapper>
              <BooksByCurrentUser />
            </ProtectedWrapper>
          }
        />
        <Route
          path={PUBLISH_CONFIG.routes.publishBook}
          element={
            <ProtectedWrapper>
              <BookCreatePage />
            </ProtectedWrapper>
          }
        />
        <Route
          path={PUBLISH_CONFIG.routes.updateBook}
          element={
            <ProtectedWrapper>
              <BookUpdatePage />
            </ProtectedWrapper>
          }
        />
        <Route
          path={PUBLISH_CONFIG.routes.publishChapter}
          element={
            <ProtectedWrapper>
              <ChapterCreatePage />
            </ProtectedWrapper>
          }
        />
        <Route
          path={PUBLISH_CONFIG.routes.updateChapter}
          element={
            <ProtectedWrapper>
              <ChapterUpdatePage />
            </ProtectedWrapper>
          }
        />
      </Route>
    </Routes>
  );
}
