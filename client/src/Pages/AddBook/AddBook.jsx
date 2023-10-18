
import React from 'react';
import { Link } from 'react-router-dom';
import "./AddBook.css";


const AddBook = () => {
    
    return (
    <>
     <div className="add-book-links">
                <Link to="/addbook-search" className='link'>Search</Link>
                <Link to="/addbook-manual" className='link'>Manual entry</Link>
      </div>
      
     
   
    </>
    )
}
export default AddBook;