
import React, { useEffect, useState } from 'react';
import './MyBooks.css';
//import BookCard from './../../Components/BookCard/BookCard'; // Import the BookCard component
import BookCardColl from './../../Components/BookCardColl/BookCardColl.jsx'

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
    <>
    <div className="my-books-container">
        My Collection</div>
      <div className="my-books-list">
        {userBooks.map((book, index) => (
          <BookCardColl key={index}
          id={book.id}
          title={book.title}
          author={book.author.name}
          genre={book.genre}
          publishYear={book.publishDate}
          imageUrl={book.smallCoverImage} />
        ))}
      </div>
    </>
    
  );
};

export default MyBooks;