
import React from 'react';
import { Link, Outlet } from 'react-router-dom';
import "./AddBook.css";


const AddBook = () => {
    
    return (
    <>
     <div className="add-book-links">
                <Link to="/addbook" className='link'>Search</Link>
                <Link to="addbook-manual" className='link'>Manual entry</Link>
      </div>
                <Outlet />
      
     
   
    </>
    )
}
export default AddBook;