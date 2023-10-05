
import React from 'react';
import {useEffect, useState } from 'react';
import "./MyBooks.css";

const MyBooks = () => {
    const [userBooks, setUserBooks ] = useState ([]);

    useEffect(() => {
fetch('http://localhost:7276/all-books')
.then((res) => res.json())
.then((data) => {
    setUserBooks(data);
})
.catch((err) => console.error("Error fetching user books", err));
    },[]);

    return (
        <div className="my-books-container">
          <ul className="my-books-list">
            {userBooks.map((book) => (
              <li key={book.id} className="my-books-item">
                {book.title}
              </li>
            ))}
          </ul>
        </div>
      );
    };

export default MyBooks;