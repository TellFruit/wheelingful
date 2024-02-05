import { Routes, Route } from 'react-router';
import { PUBLISH_CONFIG } from '../configuration/publish-config';
import BooksByCurrentUser from '../components/books-by-current-user.page';
import ProtectedWrapper from '../../auth/components/protected.wrapper';

export default function PublishRouter() {
  return (
    <Routes>
      <Route
        path={PUBLISH_CONFIG.routes.router.booksByCurrentUser}
        element={
          <ProtectedWrapper>
            <BooksByCurrentUser />
          </ProtectedWrapper>
        }
      />
    </Routes>
  );
}
