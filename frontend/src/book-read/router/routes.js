import { Routes, Route } from 'react-router';
import { READ_CONFIG } from '../configuration/read.config';
import ReadDrawer from '../components/read-drawer.component';
import BrowseBooks from '../components/browse-books.page';
import ViewBook from '../components/view-book.page';

export default function ReadRouter() {
  return (
    <Routes>
      <Route element={<ReadDrawer />}>
        <Route
          path={READ_CONFIG.routes.browseBooks}
          element={<BrowseBooks />}
        />
        <Route path={READ_CONFIG.routes.readBook} element={<ViewBook />} />
      </Route>
    </Routes>
  );
}
