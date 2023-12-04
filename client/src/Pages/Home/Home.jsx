import React, { useContext } from 'react'
import './Home.css';
import Logo from './../../Icons/logo.jpg'
import useAuth from '../../Hooks/useAuth';
import { ThemeContext } from '../../Context/ThemeProvider';

export default function Home() {
  const { user } = useAuth();
  const {darkMode, toggleDarkMode} = useContext(ThemeContext);

  console.log(user);
  return (
    <>
   <div className={darkMode ? 'dark-mode' : ''}> {/* Use darkMode state to conditionally apply class */}
     
      <div className='logo-container'>
        <img src={Logo} alt='logo' className='logo'></img>
      </div>
      <div className='welcome'>
        <h2>Welcome to MyLibrary App</h2>
        <div>a book management application</div>
      </div>
    </div>
    </>
  )
}
