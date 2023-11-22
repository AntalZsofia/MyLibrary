
import React, { useEffect, useState } from 'react';
import './MyBooks.css';
import BookCardColl from './../../Components/BookCardColl/BookCardColl.jsx'
import SearchBar from '../../Components/SearchBar/SearchBar';
import SearchBooks from '../../Components/SearchBooks/SearchBooks';
import { Outlet } from 'react-router';
import { Link } from 'react-router-dom';

const MyBooks = () => {
  const [userBooks, setUserBooks] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState('');
  

  useEffect(() => {
    setIsLoading(true);
    fetch('https://localhost:7276/all-books', {credentials: "include"})
      .then((res) => res.json())
      .then((data) => {
        //console.log(data);
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
    <div>
      <div className='reading-status-links'>
        <Link to="/reading-status/Finished" className='link'>Reviews</Link>
      </div>
      <Outlet />

      <SearchBar onSearch={handleSearch} />
      {searchQuery ? (
        <SearchBooks searchQuery={searchQuery} />
      ) : (
        !userBooks || userBooks.length === 0 ? (
          <div className='my-books-message'>You don't have any books in your collection.</div>
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
                readingStatus={book.readingStatus}
              />
            ))}
          </div>
        )
      )}
    </div>
  );
}

export default MyBooks;