
import React, { useState, useEffect } from 'react';

const AddBook = () => {
    const [bookTitle, setBookTitle] = useState ('');
    const [bookAuthor, setBookAuthor] = useState('');
    const [bookGenre, setBookGenre] = useState('');
    const [bookDescription, setBookDescription] = useState('');
    const [bookPublished, setBookPublished] = useState('');
    const[bookCover, setBookCover] = useState('');

    const [genreOptions, setGenreOptions] = useState([]);
    
    const fetchGenreOptions = () => {
        fetch('http://localhost:5000/book/genres')
          .then((res) => res.json())
          .then((data) => {
            setGenreOptions(data);
          })
          .catch((err) => {
            console.error('Error fetching genre options:', err);
          });
      };
    useEffect(() => {
    fetchGenreOptions();
    }, []);
    const handleAddBook = async () => {
        try{
            const response = await fetch('http://localhost:5000/book/add-to-collection', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    title: bookTitle,
                    author: bookAuthor,
                    genre: bookGenre,
                    description: bookDescription,
                    published: bookPublished,
                    cover: bookCover
                }),
        } );}
        catch(err){
        console.error("Error adding book to collection", err);
        }
    };
    return <>
    <div>
      <h2>Add a New Book to Your Collection</h2>
      <div>
        <label htmlFor="bookTitle">Title:</label>
        <input
          type="text"
          id="bookTitle"
          value={bookTitle}
          onChange={(e) => setBookTitle(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="bookAuthor">Author:</label>
        <input
          type="text"
          id="bookAuthor"
          value={bookAuthor}
          onChange={(e) => setBookAuthor(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="bookGenre">Genre:</label>
        <select
          id="bookGenre"
          value={bookGenre}
          onChange={(e) => setBookGenre(e.target.value)}
        >
          <option value="">Select</option>
          {genreOptions.map((genre) => (
            <option key={genre.id} value={genre.id}>
              {genre.name}
            </option>
          ))}
        </select>
      </div>
      <div>
        <label htmlFor="bookDescription">Description:</label>
        <textarea
          type="text"
          id="bookDescription"
          value={bookDescription}
          onChange={(e) => setBookDescription(e.target.value)}
          rows={4}
        />
      </div>
      <div>
        <label htmlFor="bookPublished">Published:</label>
        <input
          type="text"
          id="bookPublished"
          value={bookPublished}
          onChange={(e) => setBookPublished(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="bookCover">Cover:</label>
        <input
          type="file"
          id="bookCover"
          value={bookCover}
          onChange={(e) => setBookCover(e.target.files[0])}
        />
      </div>
      <button onClick={handleAddBook}>Add Book</button>
    </div>
    </>
}
export default AddBook;