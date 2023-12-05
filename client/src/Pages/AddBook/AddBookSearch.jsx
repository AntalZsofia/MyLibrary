import React, { useState} from 'react'
import './AddBookSearch.css';
import './../../Components/BookCard/BookCard.css';
import BookCard from '../../Components/BookCard/BookCard';
import { ThemeContext } from '../../Context/ThemeProvider.jsx';
import { useContext } from 'react';


export default function AddBookSearch() {
const [search, setSearch] = useState('');
const [searchResults, setSearchResults] = useState([]);
const { darkMode } = useContext(ThemeContext);

const handleSearch = async () => {
  
try {
  const apiUrl = `https://localhost:7276/search-book-with-google?query=${search}`;

  const response = await fetch(apiUrl, {credentials: "include"});

  if (response.status === 200) {
    const {data} = await response.json();
    console.log(data);
    setSearchResults(data);
  } else {
    console.error('Error searching for books:', response.statusText);
  }
} catch (err) {
  console.error('Error searching for books:', err);
  }
};

return (
  <>

  <div className={`add-book-container ${darkMode ? 'dark-mode' : ''}`}>
      <h2>Search Books</h2>
      <input
        className={`add-book-input ${darkMode ? 'dark-mode' : ''}`}
        type="text"
        placeholder="Enter a search term"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
      />
      <button className={`search-book-button ${darkMode ? 'dark-mode' : ''}`} onClick={handleSearch}>Search</button>

      </div>

      <div className={`book-card-container ${darkMode ? 'dark-mode' : ''}`}>
       
        {searchResults.map((result, index) => (
          <BookCard
            key={index}
            title={result.title}
            author={result.author}
            genre={result.genre}
            publishDate={result.publishDate}
            description ={result.description}
            smallCoverImage={result.smallCoverImage}
          />
        ))}
      </div>
    
      </>   
);

}