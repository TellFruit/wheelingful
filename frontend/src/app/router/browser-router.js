import { createBrowserRouter } from 'react-router-dom';
import { authRootRoutes } from '../../auth';
import Layout from '../components/layout';

export const router = createBrowserRouter([
  {
    id: 'root',
    path: '/',
    Component: Layout,
    children: [...authRootRoutes],
  },
]);