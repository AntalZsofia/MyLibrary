import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";


import Layout from "./Layout/Layout";
import Home from "./Pages/Home/Home.jsx"
import Login from "./Pages/Login/Login.jsx";
import Signup from "./Pages/SignUp/Signup.jsx";
import MyBooks from "./Pages/MyBooks/MyBooks.jsx";
import AddBook from "./Pages/AddBook/AddBook.jsx";

import ErrorPage from "./Pages/ErrorPage/ErrorPage";


import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout/>,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/addbook",
        element: <AddBook />,
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
        path:"/home",
        element: <Home />,
        errorElement: <ErrorPage/>
      },
      {
        path: "/mybooks",
        element: <MyBooks />,
        errorElement: <ErrorPage />,
      },
    ]
  },
  
 
  

])
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);


reportWebVitals();
