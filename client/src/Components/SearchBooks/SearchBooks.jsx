import React, { useState, useEffect } from 'react';
import BookCardColl from './../../Components/BookCardColl/BookCardColl.jsx';

function SearchBooks({ searchQuery }) {
  const [searchResults, setSearchResults] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    fetch(`https://localhost:7276/search-book/?query=${searchQuery}`, {credentials: "include"})
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setSearchResults(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error(error);
        setLoading(false);
      });
  }, [searchQuery]);

  return (
    <div>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul className="my-books-list">
          {searchResults.map((book, index) => (
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
        </ul>
      )}
    </div>
  );
}

export default SearchBooks;