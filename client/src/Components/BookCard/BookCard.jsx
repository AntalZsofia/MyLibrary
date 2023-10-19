import React from 'react';
import { useNavigate } from 'react-router-dom';
import './BookCard.css';

const BookCard = ({title, author, genre, smallCoverImage, publishDate, description}) => {
  const navigate = useNavigate();

  const handleAddBook = async () => {
    try{
      const bookToSave = {
        title,
        author,
        genre,
        publishDate,
        description,
        smallCoverImage
      }
      const response = await fetch('https://localhost:7276/add-to-collection', {
          method: 'POST',
          credentials:'include',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(bookToSave),
  } );
  if (response.ok) {
    const responseData = await response.json();
    console.log('Book added successfully:', responseData); // Log the response data
    navigate('/mybooks');
} else {
    console.error('Error adding book to collection:', response.statusText);
}
}
  catch(err){
  console.error("Error adding book to collection", err);
  }
};
  
return(
  

  <div className="book-card">
    <img src={smallCoverImage} alt={`${title} cover`} className="book-image" />

    <div className='book-details'>
    <h2 className="book-title">{title}</h2>
    <p className="book-author">Author: {author} </p>
    <p className="book-genre">Genre: {genre}</p>
    <p className="book-publish-year">Published: {publishDate}</p>
    <button className="add-book-button" onClick={handleAddBook}>Add</button>
    </div>
  </div>

 
);
}
export default BookCard;