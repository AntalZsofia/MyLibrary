import React, { useState, useEffect } from 'react';
import './AddBookManual.css';

const AddBookManual = () => {
    const [bookTitle, setBookTitle] = useState ('');
    const [bookAuthor, setBookAuthor] = useState('');
    const [bookGenre, setBookGenre] = useState('');
    const [bookDescription, setBookDescription] = useState('');
    const [bookPublished, setBookPublished] = useState('');
    const[bookCover, setBookCover] = useState('');

    const handleAddBook = async () => {
        try{
            const response = await fetch('https://localhost:7276/add-to-collection', {
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
        } );
        if (response.ok) {
          const responseData = await response.json();
          console.log('Book added successfully:', responseData); // Log the response data
      } else {
          console.error('Error adding book to collection:', response.statusText);
      }
      }
        catch(err){
        console.error("Error adding book to collection", err);
        }
    };

  return (
    <>
  
     <div className='add-book-container'>
     <h2 className="add-book-header">Add a New Book to Your Collection</h2>

     <div>
       <label className="add-book-label" htmlFor="bookTitle">Title:</label>
       <input
       className="add-book-input"
         type="text"
         id="bookTitle"
         value={bookTitle}
         onChange={(e) => setBookTitle(e.target.value)}
       />
     </div>
     <div>
       <label htmlFor="bookAuthor">Author:</label>
       <input
       className="add-book-input"
         type="text"
         id="bookAuthor"
         value={bookAuthor}
         onChange={(e) => setBookAuthor(e.target.value)}
       />
     </div>
     <div>
       <label htmlFor="bookGenre">Genre:</label>
       <input
       className="add-book-input"
         id="bookGenre"
         value={bookGenre}
         onChange={(e) => setBookGenre(e.target.value)}
       />
     </div>
     <div>
       <label htmlFor="bookDescription">Description:</label>
       <textarea
       className="add-book-textarea"
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
       className="add-book-input"
         type="text"
         id="bookPublished"
         value={bookPublished}
         onChange={(e) => setBookPublished(e.target.value)}
       />
     </div>
     <div>
       <label htmlFor="bookCover">Cover:</label>
       <input
       className="add-book-cover-input"
         type="file"
         id="bookCover"
         value={bookCover}
         onChange={(e) => setBookCover(e.target.files[0])}
       />
     </div>
     <button className="add-book-button" onClick={handleAddBook}>Add Book</button>
     </div>
     </>
  );
};

export default AddBookManual;