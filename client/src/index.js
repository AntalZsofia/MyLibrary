import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";

import Layout from "./Layout";
import ErrorPage from "./Pages/ErrorPage/ErrorPage";


import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout/>,
    errorElement: <ErrorPage />,
    
  },
  {
    path: "/login",
    element: <Login/>,
    errorElement: <ErrorPage/>,
  },
  {
    path: "/signup",
    element: <Signup />,
    errorElement: <ErrorPage/>,
  },
  {
    path:"/Home",
    element: <Home />,
    errorElement: <ErrorPage/>
  },
  {
    path: "/MyBooks",
    element: <MyBooks />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/AddBook",
    element: <AddBook />,
    errorElement: <ErrorPage />,

  },
  

])
