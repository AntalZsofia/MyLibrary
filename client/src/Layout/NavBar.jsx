import React from 'react'
import { NavLink } from "react-router-dom";
import useAuth from '../Hooks/useAuth';

export default function NavBar() {
  const { user } = useAuth();
  console.log(user);
   return (
    
     <nav>
    <ul className="link-list">
        <li><NavLink to="/">Home</NavLink></li>
        <li><NavLink to="/mybooks">My Books</NavLink></li>
        <li><NavLink to="/addbook">Add Book</NavLink></li>
         {!user ? <li className="right-align"><NavLink to="/login">Login</NavLink></li> 
         : <li className="right-align"><NavLink to="/profile">Profile</NavLink></li>}
        {user ? <li><NavLink to="/logout">Log Out</NavLink></li> : <li><NavLink to="/signup">Sign Up</NavLink></li>}
      </ul>
    </nav>
    
  )
}
