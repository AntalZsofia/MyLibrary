import React, { useState } from 'react';
import './SearchBar.css';

function SearchBar({ onSearch }) {
  const [query, setQuery] = useState('');

  const handleSearch = () => {
    onSearch(query);
  };

  return (
    <div className='search-book-container'>
      <input
      className='search-book-input'
        type="text"
        placeholder="Search for..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <button className='search-book-button' onClick={handleSearch}>Search</button>
    </div>
  );
}

export default SearchBar;