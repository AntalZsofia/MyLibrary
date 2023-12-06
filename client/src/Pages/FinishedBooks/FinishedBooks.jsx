import React, { useEffect } from 'react'
import { useState } from 'react';
import BookReview from '../../Components/BookReview/BookReview';
import './FinishedBooks.css';
import AllReviews from '../../Components/AllReviews/AllReviews';
import { useContext } from 'react';
import { ThemeContext } from '../../Context/ThemeProvider';

export default function FinishedBook() {
    const [currentBook, setCurrentBook] = useState([]);
    const { darkMode } = useContext(ThemeContext);

    useEffect(() => {
        fetch('https://localhost:7276/reading-status/Finished', { credentials: "include" })
            .then((res) => res.json())
            .then((data) => {
                console.log(data);
                setCurrentBook(data.books);
            })
            .catch((err) => console.error("Error fetching user books", err));
    }, []);



    return (
        <div>
            <div className={`review-books-header ${darkMode ? 'dark-mode' : ''}`}>Books to review</div>
            {!currentBook || currentBook.length === 0 ? (
                <div className={`my-books-message ${darkMode ? 'dark-mode' : ''}`}>You don't have any books in your finished reading collection.</div>
            ) : (
                <div className="my-books-list">
                    {currentBook.map((book, index) => (
                        <BookReview
                            key={index}
                            id={book.id}
                            title={book.title}
                            author={book.author.name}
                            genre={book.genre}
                            publishDate={book.publishDate}
                            description={book.description}
                            imageUrl={book.smallCoverImage}
                            readingStatus={book.readingStatus}
                        />
                    ))}

                </div>
            )}
            <div className={`reviewed-books-header ${darkMode ? 'dark-mode' : ''}`}>My Reviews</div>
            <AllReviews />
        </div>
    )
}
