import React, { useState } from 'react'
import { NavLink } from "react-router-dom";
import useAuth from '../Hooks/useAuth';


export default function NavBar() {
  const { user } = useAuth();
  const [menuOpen, setMenuOpen] = useState(false);
  console.log(user);
  
  const handleClick = () => {
    setMenuOpen(!menuOpen);
  }

   return (
    
     <nav className='nav'>
      
      <div className='menu' onClick={handleClick}>
        <span></span>
        <span></span>
        <span></span>
      </div>
     
    <ul className={menuOpen ? "open" : ""}>
        <li><NavLink to="/">Home</NavLink></li>
        <li><NavLink to="/mybooks">My Books</NavLink></li>
        <li><NavLink to="/addbook">Add Book</NavLink></li>
        <li><NavLink to="/forum">Forum</NavLink></li>
         {!user ? <li className="right-align"><NavLink to="/login">Login</NavLink></li> 
         : <li className="right-align"><NavLink to="/profile">Profile</NavLink></li>}
        {user ? <li><NavLink to="/logout">Log Out</NavLink></li> : <li><NavLink to="/signup">Sign Up</NavLink></li>}
      </ul>
    </nav>
    
  )
}
