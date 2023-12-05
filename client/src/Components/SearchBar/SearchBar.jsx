import React, { useState } from 'react';
import './SearchBar.css';
import { ThemeContext } from '../../Context/ThemeProvider.jsx';
import { useContext } from 'react';

function SearchBar({ onSearch }) {
  const [query, setQuery] = useState('');
  const { darkMode } = useContext(ThemeContext);

  const handleSearch = () => {
    onSearch(query);
  };

  return (
    <div className='search-book-container'>
      <input
      className={`search-book-input ${darkMode ? 'dark-mode' : ''}`}
        type="text"
        placeholder="Search for..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <button className={`search-book-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleSearch}>Search</button>
    </div>
  );
}

export default SearchBar;