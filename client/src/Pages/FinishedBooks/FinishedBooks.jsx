import React, { useEffect } from 'react'
import { useState } from 'react';
import BookCardColl from '../../Components/BookCardColl/BookCardColl';

export default function FinishedBook() {
    const [currentBook, setCurrentBook] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7276/reading-status/Finished', {credentials: "include"})
        .then((res) => res.json())
        .then((data) => {
            console.log(data);
            setCurrentBook(data.books);
        })
        .catch((err) => console.error("Error fetching user books", err));
    }, []);

   

  return (
    <div>
        <h1>Finished Books</h1>
        {!currentBook || currentBook.length === 0 ? (
            <div className='my-books-message'>You don't have any books in your finished reading collection.</div>
        ) : (
            <div className="my-books-list">
                {currentBook.map((book, index) => (
                <BookCardColl
                    key={index}
                    id={book.id}
                    title={book.title}
                    author={book.author.name}
                    genre={book.genre}
                    publishYear={book.publishDate}
                    description={book.description}
                    imageUrl={book.smallCoverImage}
                />
                ))}
            </div>
        )}
    </div>
  )
}
