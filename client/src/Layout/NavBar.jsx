import React, { useState, useContext } from 'react'
import { NavLink } from "react-router-dom";
import useAuth from '../Hooks/useAuth';
import { ThemeContext } from '../Context/ThemeProvider';
import Switch from '../Components/Switch/Switch';
import './Layout.css';


export default function NavBar() {
  const { user } = useAuth();
  const [menuOpen, setMenuOpen] = useState(false);
  const {darkMode, toggleDarkMode} = useContext(ThemeContext);
 
  
  const onToggle = () => {
    toggleDarkMode(!darkMode);
  };

  const handleClick = () => {
    setMenuOpen(!menuOpen);
  }

   return (
    
     <nav className={`Layout nav ${darkMode ? 'dark-mode' : ''}`}>
      
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
        {user ? <li className="right-align"><NavLink to="/admin">Admin</NavLink></li> 
         : <li className="right-align"><NavLink to="/"></NavLink></li>}
         {!user ? <li className="right-align"><NavLink to="/login">Login</NavLink></li> 
         : <li className="right-align"><NavLink to="/profile">Profile</NavLink></li>}
        {user ? <li><NavLink to="/logout">Log Out</NavLink></li> : <li><NavLink to="/signup">Sign Up</NavLink></li>}
        <Switch isToggled={darkMode} onToggle={onToggle} />
      </ul>
    </nav>
    
  )
}
