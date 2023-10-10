
import React, { useState, useEffect } from 'react';
import { Link, Route } from 'react-router-dom';
import "./AddBook.css";
import AddBookSearch from './AddBookSearch';
import AddBookManual from './AddBookManual';

const AddBook = () => {
    const [bookTitle, setBookTitle] = useState ('');
    const [bookAuthor, setBookAuthor] = useState('');
    const [bookGenre, setBookGenre] = useState('');
    const [bookDescription, setBookDescription] = useState('');
    const [bookPublished, setBookPublished] = useState('');
    const[bookCover, setBookCover] = useState('');

    const [genreOptions, setGenreOptions] = useState([]);
    
    const handleAddBook = async () => {
        try{
            const response = await fetch('https://localhost:7276/add-to-collection', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    title: bookTitle,
                    author: bookAuthor,
                    genre: bookGenre,
                    description: bookDescription,
                    published: bookPublished,
                    cover: bookCover
                }),
        } );}
        catch(err){
        console.error("Error adding book to collection", err);
        }
    };
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