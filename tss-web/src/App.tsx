import React from 'react';
import './App.scss';

import { createBrowserRouter, RouterProvider } from "react-router-dom";
import LoginPage from './pages/login/login.page'
import Projects from './pages/dashboard/projects/projects';


const router = createBrowserRouter([
  {
    path: "/",
    element: (<LoginPage />),
  },
  {
    path: "/project",
    element: (<Projects />),
  },
]);

const App = (props: any): JSX.Element => {
  return (
    <>
      <RouterProvider router={router} />
    </>
  )
}

export default App;
