import React from 'react'
import './Home.css';
import Logo from './../../Icons/logo.jpg'
import useAuth from '../../Hooks/useAuth';

export default function Home() {
  const { user } = useAuth();
  console.log(user);
  return (
    <>
    <div className='logo-container'>
    <img src={Logo} alt='logo' className='logo'></img>
    </div>
    <div className='welcome'>
    <h2>Welcome to MyLibrary App</h2>
    <div>a book management application</div>
    </div>
    </>
  )
}
