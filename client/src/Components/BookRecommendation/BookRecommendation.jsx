import React from 'react'
import BookCard from '../BookCard/BookCard';
import { useState, useEffect } from 'react';
import './BookRecommendation.css';

export default function BookRecommendation( {author, title}) {
    const [recommendedBooks, setRecommendedBooks] = useState([]);

useEffect(() => {
    fetch(`https://localhost:7276/same-author/${author}-${title}`, { 
        credentials: 'include',
    headers: { 
        'Content-Type': 'application/json',
    }
    })
    .then(res => {
        if (!res.ok) {
            throw new Error(`HTTP error! status: ${res.status}`);
        }
        return res.json();
    })
    .then(data => {
      console.log(data);
      console.log(author);
      setRecommendedBooks(data);
      
    })
    .catch(err => console.log(err))
  

}, [author, title]);




  return (
    <>
        <h3 className='book-recommendation-header'>More books by this author:</h3>
    <div className='book-recommendation-container'>
        {recommendedBooks.map((book, index) => (
            <BookCard
            key={index}
            title={book.title}
            author={book.author}
            genre={book.genre}
            publishDate={book.publishDate}
            description ={book.description}
            smallCoverImage={book.smallCoverImage}
            />
        ))}
    </div>
    </>
  )
}

