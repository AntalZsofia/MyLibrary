
import React, { useEffect, useState } from 'react';
import './MyBooks.css';
import BookCardColl from './../../Components/BookCardColl/BookCardColl.jsx'
import SearchBar from '../../Components/SearchBar/SearchBar';
import SearchBooks from '../../Components/SearchBooks/SearchBooks';

const MyBooks = () => {
  const [userBooks, setUserBooks] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    fetch('https://localhost:7276/all-books', {credentials: "include"})
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setIsLoading(false);
        setUserBooks(data.books);
      })
      .catch((err) => console.error("Error fetching user books", err));
  }, []);

  const handleSearch = (query) => {
    setSearchQuery(query);
  };

  if(isLoading){
    <div>Loading...</div>
  }
  return (
    <>
      <div className="my-books-container">My Collection</div>
      <SearchBar onSearch={handleSearch} />
      {searchQuery ? (
        <SearchBooks searchQuery={searchQuery} />
      ) : (
        <div className="my-books-list">
          {userBooks.map((book, index) => (
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
    </>
  );
}

export default MyBooks;