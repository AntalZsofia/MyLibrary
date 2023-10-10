
import React, { useEffect, useState } from 'react';
import './MyBooks.css';
import BookCard from './../../Components/BookCard/BookCard'; // Import the BookCard component

const MyBooks = () => {
  const [userBooks, setUserBooks] = useState([]);

  useEffect(() => {
    fetch('https://localhost:7276/all-books', {credentials: "include"})
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setUserBooks(data.books);
      })
      .catch((err) => console.error("Error fetching user books", err));
  }, []);

  return (
    <div className="my-books-container">
      <div className="my-books-list">
        {userBooks.map((book) => (
          <BookCard key={book.id} book={book} />
        ))}
      </div>
    </div>
  );
};

export default MyBooks;