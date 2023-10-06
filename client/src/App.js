import React, { useState } from "react";
import { Route, 
  createBrowserRouter, 
  createRoutesFromElements, 
  RouterProvider } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";


import { AuthProvider } from './Context/AuthContext';

import Layout from "./Layout/Layout";
import Home from "./Pages/Home/Home.jsx"
import Login from "./Pages/Login/Login.jsx";
import Signup from "./Pages/SignUp/Signup.jsx";
import MyBooks from "./Pages/MyBooks/MyBooks.jsx";
import AddBook from "./Pages/AddBook/AddBook.jsx";

//import ErrorPage from "./Pages/ErrorPage/ErrorPage";


import "./index.css";
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path='/' element={<Layout />}>
      <Route index element={< Home />} />
      <Route path='/mybooks' element={< MyBooks />} />
      <Route path='/addbook' element={< AddBook />} />
      <Route path='/login' element={< Login />} />
      <Route path='/signup' element={< Signup />} />
    </Route>
  )
)
export default function App(){
  const [user, setUser] = useState(null)

return(
  <React.StrictMode>
    <AuthProvider value={{ user, setUser }}>
    <RouterProvider router={router} />
    </AuthProvider>
  </React.StrictMode>
)
}


reportWebVitals();
