import React, { useState, useEffect } from 'react';
import './BookCardColl.css';
import { NavLink } from "react-router-dom";

import Modal from './../Modal/Modal'; 


const BookCardColl = ({ title, author, genre, imageUrl, publishYear, description, id}) => {
  const [isDeleteConfirmationVisible, setDeleteConfirmationVisible] = useState(false);
  const [bookDeleted, setBookDeleted] = useState(false);
  

  const handleDeleteBook = async () => {
    try {
      const bookToDelete = {
        title,
        author,
        genre,
        publishYear,
        description,
        imageUrl,
      };
      const response = await fetch(`https://localhost:7276/delete-book/${id}`, {
        method: 'DELETE',
        credentials: 'include',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bookToDelete),
      });
      if (response.ok) {
        const responseData = await response.json();
        setBookDeleted(true);
        setDeleteConfirmationVisible(false);
       
        console.log('Book deleted from the collection:', responseData);

      } else {
        console.error('Error deleting book from collection:', response.statusText);
      }
    } catch (err) {
      console.error('Error deleting book from collection', err);
    }
  };
  useEffect(() => {
setBookDeleted(true);
  }, []);

  const openDeleteConfirmation = () => {
    setDeleteConfirmationVisible(true);
  };

  const closeDeleteConfirmation = () => {
    setDeleteConfirmationVisible(false);
  };

  return (
    <div className="book-card">
      <img src={imageUrl} alt={`${title} cover`} className="book-image" />

      <div className="book-details">
        <h2 className="book-title">{title}</h2>
        <p className="book-author">Author: {author}</p>
        <p className="book-genre">Genre: {genre}</p>
        <p className="book-publish-year">Published: {publishYear}</p>
        <div className="buttons-container">
          <NavLink to={`/update-book/${id}`}>
            <button className="update-book-button">Update</button>
          </NavLink>
          <button className="delete-book-button" onClick={openDeleteConfirmation}>
            Delete
          </button>
        </div>
      </div>

      {isDeleteConfirmationVisible && (
        <Modal onClose={closeDeleteConfirmation}>
        <h3>Are you sure you want to delete this book?</h3>
        <button className="yesButton" onClick={handleDeleteBook}>Yes</button>
        <button className="noButton" onClick={closeDeleteConfirmation}>No</button>
      </Modal>
      )}
     
    </div>
  );
};

export default BookCardColl;


