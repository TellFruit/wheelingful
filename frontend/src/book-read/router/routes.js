import { Routes, Route } from 'react-router';
import { READ_CONFIG } from '../configuration/read.config';
import ReadDrawer from '../components/read-drawer.component';
import BrowseBooks from '../components/browse-books.page';
import ViewBook from '../components/view-book.page';
import ViewChapter from '../components/view-chapter';

import PublishReview from '../components/review-create.page';
import UpdateReview from '../components/review-update.page';

export default function ReadRouter() {
  return (
    <Routes>
      <Route element={<ReadDrawer />}>
        <Route
          path={READ_CONFIG.routes.browseBooks}
          element={<BrowseBooks />}
        />
        <Route path={READ_CONFIG.routes.readBook} element={<ViewBook />} />
        <Route
          path={READ_CONFIG.routes.publishReview}
          element={<PublishReview />}
        />
        <Route
          path={READ_CONFIG.routes.updateReview}
          element={<UpdateReview />}
        />
        <Route
          path={READ_CONFIG.routes.readChapter}
          element={<ViewChapter />}
        />
      </Route>
    </Routes>
  );
}
