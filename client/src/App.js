import React, { useState, useContext } from "react";
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
import Profile from "./Pages/Profile/Profile";
import { ThemeContext } from "./Context/ThemeProvider.jsx";

//import ErrorPage from "./Pages/ErrorPage/ErrorPage";


import "./index.css";
import AddBookSearch from "./Pages/AddBook/AddBookSearch";
import AddBookManual from "./Pages/AddBook/AddBookManual";
import UpdateBook from "./Pages/UpdateBook/UpdateBook";
import UpdateProfile from "./Pages/Profile/UpdateProfile";
import Logout from "./Pages/Logout/Logout";
import ChangePassword from "./Pages/Profile/ChangePassword";
import Forum from "./Pages/Forum/Forum.jsx";
import PostCard from "./Components/PostCard/PostCard.jsx";
import Admin from "./Pages/Admin/Admin.jsx";
import UserAllPosts from "./Pages/Admin/UserAllPosts.jsx";
import UpdatePost from "./Pages/Forum/UpdatePost.jsx";
import FinishedBooks from "./Pages/FinishedBooks/FinishedBooks.jsx";

const router = createBrowserRouter(
  createRoutesFromElements(
  <Route path='/' element={<Layout />}>
      <Route index element={< Home />} />
      <Route path='/mybooks' element={< MyBooks />} />
      <Route path='/addbook' element={< AddBook />} >
      <Route index element={<AddBookSearch />} />
      <Route path='addbook-manual' element={<AddBookManual />} />
         </Route> 
      <Route path='/forum' element={< Forum />} />
      <Route path='/forum/:id' element={< PostCard />} />
      <Route path='/update-post/:id' element={< UpdatePost />} />
      <Route path='/admin' element={<Admin />} />
      <Route path='/admin/posts/:id' element={< UserAllPosts />} />
      <Route path='/profile' element={<Profile />} />
      <Route path='/profile/update' element={<UpdateProfile />} />
      <Route path='/profile/password' element={<ChangePassword />} />
      <Route path='/selected-book/:id' element={<SelectedBook />}/>
      <Route path='/reading-status/Finished' element={<FinishedBooks />} />
      <Route path='/update-book/:id' element={<UpdateBook />} />
      <Route path='/delete-book/:id' element={<DeleteBook />} />
      <Route path='/login' element={< Login />} />
      <Route path='/logout' element={< Logout />} />
      <Route path='/signup' element={< Signup />} />
  </Route>
    
      
  )
)
export default function App(){
  const [user, setUser] = useState(null);
  const { darkMode } = useContext(ThemeContext);
  

return(
  <div className={darkMode ? "dark-mode" : "light-mode"}>
    <AuthContext.Provider value={{ user, setUser }}>
    <RouterProvider router={router} />
    </AuthContext.Provider>
  </div>
)
}


reportWebVitals();
