import React from 'react';
import './BookCardColl.css';
import { NavLink } from "react-router-dom";


const BookCardColl = ({title, author, genre, imageUrl, publishYear, id}) => {
  

  const handleDeleteBook = async () => {
    try{
      const bookToDelete = {
        title,
        author,
        genre,
        publishYear,
        imageUrl
      }
      const response = await fetch(`https://localhost:7276/delete-book`, {
          method: 'DELETE',
          credentials:'include',
          headers: {
              'Content-Type': 'application/json',
          },
          body: JSON.stringify(bookToDelete),
  } );
  if (response.ok) {
    const responseData = await response.json();
    console.log('Book deleted from the collection:', responseData); // Log the response data
} else {
    console.error('Error deleting book from collection:', response.statusText);
}
}
  catch(err){
  console.error("Error deleting book from collection", err);
  }
};
  
return(
  

  <div className="book-card">
    <img src={imageUrl} alt={`${title} cover`} className="book-image" />

    <div className='book-details'>
    <h2 className="book-title">{title}</h2>
    <p className="book-author">Author: {author} </p>
    <p className="book-genre">Genre: {genre}</p>
    <p className="book-publish-year">Published: {publishYear}</p>
    <div className='buttons-container'>
      <NavLink to={`/update-book/${id}`}>
    <button className="update-book-button">Update</button>
      </NavLink>
    <button className="delete-book-button" onClick={handleDeleteBook}>Delete</button>
    </div>

    </div>
  </div>

 
);
}
export default BookCardColl;