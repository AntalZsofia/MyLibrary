import React, { useState } from "react";
import { Route, 
  createBrowserRouter, 
  createRoutesFromElements, 
  RouterProvider } from "react-router-dom";
import reportWebVitals from "./reportWebVitals";


import AuthContext from './Context/AuthProvider';

import Layout from "./Layout/Layout";
import Home from "./Pages/Home/Home.jsx"
import Login from "./Pages/Login/Login.jsx";
import Signup from "./Pages/SignUp/Signup.jsx";
import MyBooks from "./Pages/MyBooks/MyBooks.jsx";
import AddBook from "./Pages/AddBook/AddBook.jsx";
import SelectedBook from "./Pages/SelectedBook/SelectedBook.jsx";
import DeleteBook from "./Pages/DeleteBook/DeleteBook.jsx";


//import ErrorPage from "./Pages/ErrorPage/ErrorPage";


import "./index.css";
import AddBookSearch from "./Pages/AddBook/AddBookSearch";
import AddBookManual from "./Pages/AddBook/AddBookManual";
import UpdateBook from "./Pages/UpdateBook/UpdateBook";
const router = createBrowserRouter(
  createRoutesFromElements(
  <Route path='/' element={<Layout />}>
      <Route index element={< Home />} />
      <Route path='/mybooks' element={< MyBooks />} />
      <Route path='/addbook' element={< AddBook />} />
      <Route path='/addbook-search' element={<AddBookSearch />} />
      <Route path='/addbook-manual' element={<AddBookManual />} />
      <Route path='/selected-book/:id' element={<SelectedBook />}/>
      <Route path='/update-book/:id' element={<UpdateBook />} />
      <Route path='/delete-book/:id' element={<DeleteBook />} />
      <Route path='/login' element={< Login />} />
      <Route path='/signup' element={< Signup />} />
  </Route>
    
      
  )
)
export default function App(){
  const [user, setUser] = useState(null);
  
  

return(
  
    <AuthContext.Provider value={{ user, setUser }}>
    <RouterProvider router={router} />
    </AuthContext.Provider>
  
)
}


reportWebVitals();
