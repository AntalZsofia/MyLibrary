
import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import "./AddBook.css";
import { ThemeContext } from '../../Context/ThemeProvider.jsx';
import { useContext } from 'react';

const AddBook = () => {
    const { darkMode } = useContext(ThemeContext);
    
    return (
    <>
     <div className={`add-book-links ${darkMode ? 'dark-mode' : ''}`}>
                <Link to="/addbook" className={`link ${darkMode ? 'dark-mode' : ''}`}>Search</Link>
                <Link to="addbook-manual" className={`link ${darkMode ? 'dark-mode' : ''}`}>Manual entry</Link>
      </div>
                <Outlet />
      
     
   
    </>
    )
}
export default AddBook;